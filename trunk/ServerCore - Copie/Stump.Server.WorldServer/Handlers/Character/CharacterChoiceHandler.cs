
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Stump.DofusProtocol.Messages.Framework.IO;
using Stump.Database.WorldServer;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Entities;
using Stump.Server.WorldServer.Manager;
using Stump.Server.WorldServer.Threshold;

namespace Stump.Server.WorldServer.Handlers
{
    public partial class CharacterHandler
    {

        [WorldHandler(typeof(CharacterFirstSelectionMessage))]
        public static void HandleCharacterFirstSelectionMessage(WorldClient client, CharacterFirstSelectionMessage message)
        {
            // TODO ADD TUTORIAL EFFECTS
            HandleCharacterSelectionMessage(client, message);
        }

        [WorldHandler(typeof(CharacterSelectionMessage))]
        public static void HandleCharacterSelectionMessage(WorldClient client, CharacterSelectionMessage message)
        {
            CharacterRecord character = client.Characters.First(entry => entry.Id == message.id);

            /* Check null */
            if (character == null)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            /* Common selection */
            CommonCharacterSelection(client, character);
        }

        [WorldHandler(typeof(CharacterSelectionWithRecolorMessage))]
        public static void HandleCharacterSelectionWithRecolorMessage(WorldClient client, CharacterSelectionWithRecolorMessage message)
        {
            CharacterRecord character = client.Characters.First(entry => entry.Id == message.id);

            /* Check null */
            if (character == null)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            /* Set Colors */
            character.BaseLook.indexedColors = message.indexedColor;
            character.Save();

            /* Common selection */
            CommonCharacterSelection(client, character);
        }

        [WorldHandler(typeof(CharacterSelectionWithRenameMessage))]
        public static void HandleCharacterSelectionWithRenameMessage(WorldClient client, CharacterSelectionWithRenameMessage message)
        {
            CharacterRecord character = client.Characters.First(entry => entry.Id == message.id);

            /* Check null */
            if (character == null)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            /* Check if name is free */
            if (CharacterRecord.IsNameExists(message.name) || !Regex.IsMatch(StringExtensions.FirstLetterUpper(message.name.ToLower()),
                               "^[A-Z][a-z]{2,9}(?:-[A-Z][a-z]{2,9}|[a-z]{1,10})$", RegexOptions.Compiled))
            {
                client.Send(new CharacterCreationResultMessage((int)CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS));
                return;
            }

            /* Set new name */
            character.Name = StringExtensions.FirstLetterUpper(message.name.ToLower());
            character.Save();

            /* Common selection */
            CommonCharacterSelection(client, character);
        }

        public static void CommonCharacterSelection(WorldClient client, CharacterRecord character)
        {
            client.ActiveCharacter = new Character(character, client);

            /* Check if we also have a world account */
            if (client.WorldAccount == null)
            {
                if (!WorldAccountRecord.Exists(client.Account.Id))
                    client.WorldAccount = AccountManager.CreateWorldAccount(client);
                else
                    client.WorldAccount = WorldAccountRecord.FindWorldAccountById(client.Account.Id);
            }

            /* Update LastConnection and Last Ip */
            client.WorldAccount.LastConnection = DateTime.Now;
            client.WorldAccount.LastIp = client.IP;
            client.WorldAccount.Update();

            SendCharacterSelectedSuccessMessage(client);

            InventoryHandler.SendInventoryContentMessage(client);
            InventoryHandler.SendInventoryWeightMessage(client);

            InventoryHandler.SendSpellListMessage(client, true);
            ContextHandler.SendSpellForgottenMessage(client);
            ContextHandler.SendNotificationListMessage(client, new List<int>());

            ContextHandler.SendEmoteListMessage(client, new List<uint>());
            ChatHandler.SendEnabledChannelsMessage(client, new List<uint>(), new List<uint>());

            PvpHandler.SendAlignmentRankUpdateMessage(client);
            PvpHandler.SendAlignmentSubAreasListMessage(client);

            InitializationHandler.SendSetCharacterRestrictionsMessage(client);

            BasicHandler.SendTextInformationMessage(client, 1, 89);
            BasicHandler.SendTextInformationMessage(client, 0, 152,
                                                    client.Account.LastConnection.Year,
                                                    client.Account.LastConnection.Month,
                                                    client.Account.LastConnection.Day,
                                                    client.Account.LastConnection.Hour,
                                                    client.Account.LastConnection.Minute,
                                                    client.Account.LastIp ?? "(null)");

            InitializationHandler.SendOnConnectionEventMessage(client, 2);
        }

        [WorldHandler(typeof(CharactersListRequestMessage))]
        public static void HandleCharacterListRequest(WorldClient client, CharactersListRequestMessage message)
        {
            if (client.Account != null && client.Account.Login != "")
            {
                SendCharactersListMessage(client);
            }
            else
            {
                client.Send(new IdentificationFailedMessage((int)IdentificationFailureReasonEnum.KICKED));
                client.DisconnectLater(1000);
            }
        }

        public static void SendCharactersListMessage(WorldClient client)
        {
            List<CharacterBaseInformations> characters = client.Characters.Select(
                characterRecord =>
                new CharacterBaseInformations(
                    (uint)characterRecord.Id,
                    ThresholdManager.Thresholds["CharacterExp"].GetLevel(characterRecord.Experience),
                    characterRecord.Name,
                    CharacterManager.GetStuffedCharacterLook(characterRecord).EntityLook,
                    characterRecord.Breed,
                    characterRecord.Sex != SexTypeEnum.SEX_MALE)).ToList();

            client.Send(new CharactersListMessage(
                            client.Account.StartupActions.Count != 0,
                            characters
                            ));
        }

        public static void SendCharactersListWithModificationsMessage(WorldClient client)
        {
            var cbi = new List<CharacterBaseInformations>();
            var ctri = new List<CharacterToRecolorInformation>();
            var re = new List<int>();
            var oth = new List<int>();

            foreach (CharacterRecord c in client.Characters)
            {
                cbi.Add(new CharacterBaseInformations((uint)c.Id,
                                                      ThresholdManager.Thresholds["CharacterExp"].GetLevel(c.Experience),
                                                      c.Name, CharacterManager.GetStuffedCharacterLook(c).EntityLook,
                                                      c.Breed, c.Sex != SexTypeEnum.SEX_MALE));

                if (c.Rename)
                {
                    re.Add(c.Id);
                }

                if (c.Recolor)
                {
                    ctri.Add(new CharacterToRecolorInformation((uint)c.Id, c.BaseLook.indexedColors));
                }

                if (!(c.Recolor && c.Rename))
                {
                    oth.Add(c.Id);
                }
            }
            client.Send(new CharactersListWithModificationsMessage(client.Account.StartupActions.Count != 0, cbi, ctri,
                                                                   re, oth));
        }

        public static void SendCharacterSelectedSuccessMessage(WorldClient client)
        {
            client.Send(new CharacterSelectedSuccessMessage(client.ActiveCharacter.GetBaseInformations()));
        }
    }
}