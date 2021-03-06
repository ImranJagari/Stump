﻿using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Messages;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Dialogs.Guilds;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.TaxCollector;
using GuildMember = Stump.Server.WorldServer.Game.Guilds.GuildMember;

namespace Stump.Server.WorldServer.Handlers.Guilds
{
    public class GuildHandler : WorldHandlerContainer
    {
        [WorldHandler(GuildGetInformationsMessage.Id)]
        public static void HandleGuildGetInformationsMessage(WorldClient client, GuildGetInformationsMessage message)
        {
            if (client.Character.Guild == null)
                return;

            switch (message.infoType)
            {
                case (sbyte)GuildInformationsTypeEnum.INFO_GENERAL:
                    SendGuildInformationsGeneralMessage(client, client.Character.Guild);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_MEMBERS:
                    SendGuildInformationsMembersMessage(client, client.Character.Guild);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_BOOSTS:
                    SendGuildInfosUpgradeMessage(client, client.Character.Guild);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_PADDOCKS:
                    SendGuildInformationsPaddocksMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_HOUSES:
                    SendGuildHousesInformationMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_GUILD_ONLY:
                    TaxCollectorHandler.SendTaxCollectorListMessage(client, client.Character.Guild);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_LEAVE:
                    TaxCollectorHandler.SendTaxCollectorListMessage(client, client.Character.Guild);
                    break;
            }
        }

        [WorldHandler(GuildCharacsUpgradeRequestMessage.Id)]
        public static void HandleGuildCharacsUpgradeRequestMessage(WorldClient client, GuildCharacsUpgradeRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_GUILD_BOOSTS))
                return;

            switch (message.charaTypeTarget)
            {
                case 0: //Pods
                    client.Character.Guild.UpgradeTaxCollectorPods();
                    break;
                case 1: //Prospecting
                    client.Character.Guild.UpgradeTaxCollectorProspecting();
                    break;
                case 2: //Wisdom
                    client.Character.Guild.UpgradeTaxCollectorWisdom();
                    break;
                case 3: //MaxTaxCollectors
                    client.Character.Guild.UpgradeMaxTaxCollectors();
                    break;
            }

            SendGuildInfosUpgradeMessage(client.Character.Guild.Clients, client.Character.Guild);
        }

        [WorldHandler(GuildSpellUpgradeRequestMessage.Id)]
        public static void HandleGuildSpellUpgradeRequestMessage(WorldClient client, GuildSpellUpgradeRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (client.Character.Guild.UpgradeSpell(message.spellId))
                SendGuildInfosUpgradeMessage(client.Character.Guild.Clients, client.Character.Guild);
        }

        [WorldHandler(GuildCreationValidMessage.Id)]
        public static void HandleGuildCreationValidMessage(WorldClient client, GuildCreationValidMessage message)
        {
            (client.Character.Dialog as GuildCreationPanel)?.CreateGuild(message.guildName, message.guildEmblem);
        }

        [WorldHandler(GuildModificationValidMessage.Id)]
        public static void HandleGuildModificationValidMessage(WorldClient client, GuildModificationValidMessage message)
        {
            var panel = client.Character.Dialog as GuildModificationPanel;
            panel?.ModifyGuildName(message.guildName);
            panel?.ModifyGuildEmblem(message.guildEmblem);
        }

        [WorldHandler(GuildModificationNameValidMessage.Id)]
        public static void HandleGuildModificationNameValidMessage(WorldClient client, GuildModificationNameValidMessage message)
        {
            (client.Character.Dialog as GuildModificationPanel)?.ModifyGuildName(message.guildName);
        }

        [WorldHandler(GuildModificationEmblemValidMessage.Id)]
        public static void HandleGuildModificationEmblemValidMessage(WorldClient client, GuildModificationEmblemValidMessage message)
        {
            (client.Character.Dialog as GuildModificationPanel)?.ModifyGuildEmblem(message.guildEmblem);
        }

        [WorldHandler(GuildChangeMemberParametersMessage.Id)]
        public static void HandleGuildChangeMemberParametersMessage(WorldClient client, GuildChangeMemberParametersMessage message)
        {
            if (client.Character.Guild == null)
                return;

            var target = client.Character.Guild.TryGetMember((int) message.memberId);
            if (target == null)
                return;

            client.Character.Guild.ChangeParameters(client.Character, target, message.rank,
                                                    (byte) message.experienceGivenPercent, (uint)message.rights);
        }

        [WorldHandler(GuildKickRequestMessage.Id)]
        public static void HandleGuildKickRequestMessage(WorldClient client, GuildKickRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;

            var target = client.Character.Guild.TryGetMember((int) message.kickedId);
            if (target == null)
                return;

            target.Guild.KickMember(client.Character.GuildMember, target);
        }

        [WorldHandler(GuildInvitationMessage.Id)]
        public static void HandleGuildInvitationMessage(WorldClient client, GuildInvitationMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS))
            {
                // Vous n'avez pas le droit requis pour inviter des joueurs dans votre guilde.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 207);
                return;
            }

            var target = World.Instance.GetCharacter((int) message.targetId);
            if (target == null)
            {
                // Impossible d'inviter, ce joueur est inconnu ou non connecté.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                return;
            }

            if (target.Guild != null)
            {
                // Impossible, ce joueur est déjà dans une guilde
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                return;
            }

            if (target.IsBusy())
            {
                // Ce joueur est occupé. Impossible de l'inviter.                    
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                return;
            }

            if (!client.Character.Guild.CanAddMember())
            {
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 55, client.Character.Guild.MaxMembers);
                return;
            }

            var request = new GuildInvitationRequest(client.Character, target);
            request.Open();
        }

        [WorldHandler(GuildInvitationByNameMessage.Id)]
        public static void HandleGuildInvitationByNameMessage(WorldClient client, GuildInvitationByNameMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS))
            {
                // Vous n'avez pas le droit requis pour inviter des joueurs dans votre guilde.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 207);
                return;
            }

            var target = Singleton<World>.Instance.GetCharacter(message.name);
            if (target == null)
            {
                // Impossible d'inviter, ce joueur est inconnu ou non connecté.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                return;
            }

            if (target.Guild != null)
            {
                // Impossible, ce joueur est déjà dans une guilde
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                return;
            }

            if (target.IsBusy())
            {
                // Ce joueur est occupé. Impossible de l'inviter.                    
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                return;
            }

            var request = new GuildInvitationRequest(client.Character, target);
            request.Open();
        }

        [WorldHandler(GuildInvitationAnswerMessage.Id)]
        public static void HandleGuildInvitationAnswerMessage(WorldClient client, GuildInvitationAnswerMessage message)
        {
            var request = client.Character.RequestBox as GuildInvitationRequest;

            if (request == null)
                return;

            if (client.Character == request.Source && !message.accept)
                request.Cancel();
            else if (client.Character == request.Target)
            {
                if (message.accept)
                    request.Accept();
                else
                    request.Deny();
            }
        }

        [WorldHandler(GuildMemberSetWarnOnConnectionMessage.Id)]
        public static void HandleGuildMemberSetWarnOnConnectionMessage(WorldClient client, GuildMemberSetWarnOnConnectionMessage message)
        {
            client.Character.WarnOnGuildConnection = message.enable;
        }

        [WorldHandler(GuildMotdSetRequestMessage.Id)]
        public static void HandleGuildMotdSetRequestMessage(WorldClient client, GuildMotdSetRequestMessage message)
        {
            if (client.Character.GuildMember == null)
            {
                SendGuildMotdSetErrorMessage(client, GuildMotdErrorEnum.GUILD_MOTD_UNKNOWN_ERROR);
                return;
            }

            client.Character.Guild.UpdateMotd(client.Character.GuildMember, message.content);
        }

        [WorldHandler(GuildBulletinSetRequestMessage.Id)]
        public static void HandleGuildBulletinSetRequestMessage(WorldClient client, GuildBulletinSetRequestMessage message)
        {
            if (client.Character.GuildMember == null)
            {
                SendGuildBulletinSetErrorMessage(client, SocialNoticeErrorEnum.SOCIAL_NOTICE_UNKNOWN_ERROR);
                return;
            }

            if (client.Character.GuildMember.RankId > 2)
            {
                SendGuildBulletinSetErrorMessage(client, SocialNoticeErrorEnum.SOCIAL_NOTICE_INVALID_RIGHTS);
                return;
            }

            client.Character.Guild.UpdateBulletin(client.Character.GuildMember, message.content, message.notifyMembers);
        }

        [WorldHandler(GuildFactsRequestMessage.Id)]
        public static void HandleGuildFactsRequestMessage(WorldClient client, GuildFactsRequestMessage message)
        {
            var guild = GuildManager.Instance.TryGetGuild(message.guildId);

            if (guild == null)
            {
                SendGuildFactsErrorMessage(client, message.guildId);
                return;
            }

            client.Send(new GuildFactsMessage(guild.GetGuildFactSheetInformations(), guild.CreationDate.GetUnixTimeStamp(), (short)guild.TaxCollectors.Count, true, guild.Members.Select(x => x.GetCharacterMinimalInformations())));
        }

        public static void SendGuildMemberWarnOnConnectionStateMessage(IPacketReceiver client, bool state)
        {
            client.Send(new GuildMemberWarnOnConnectionStateMessage(state));
        }

        public static void SendGuildInvitedMessage(IPacketReceiver client, Character recruter)
        {
            client.Send(new GuildInvitedMessage(recruter.Id, recruter.Name, recruter.Guild.GetBasicGuildInformations()));
        }

        public static void SendGuildInvitationStateRecrutedMessage(IPacketReceiver client, GuildInvitationStateEnum state)
        {
            client.Send(new GuildInvitationStateRecrutedMessage((sbyte)state));
        }

        public static void SendGuildInvitationStateRecruterMessage(IPacketReceiver client, Character recruted, GuildInvitationStateEnum state)
        {
            client.Send(new GuildInvitationStateRecruterMessage(recruted.Name, (sbyte)state));
        }

        public static void SendGuildLeftMessage(IPacketReceiver client)
        {
            client.Send(new GuildLeftMessage());
        }

        public static void SendGuildCreationResultMessage(IPacketReceiver client, SocialGroupCreationResultEnum result)
        {
            client.Send(new GuildCreationResultMessage((sbyte)result));
        }

        public static void SendGuildMembershipMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildMembershipMessage(member.Guild.GetGuildInformations(), (int)member.Rights, true));
        }

        public static void SendGuildInformationsGeneralMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(new GuildInformationsGeneralMessage(true, false, guild.Level, guild.ExperienceLevelFloor, guild.Experience,
                guild.ExperienceNextLevelFloor, guild.CreationDate.GetUnixTimeStamp(), (short)guild.Members.Count(), (short)guild.Members.Count(x => x.IsConnected))); 
        }

        public static void SendGuildInformationsMembersMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(new GuildInformationsMembersMessage(guild.Members.Select(x => x.GetNetworkGuildMember())));
        }

        public static void SendGuildInformationsMemberUpdateMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildInformationsMemberUpdateMessage(member.GetNetworkGuildMember()));
        }

        public static void SendGuildInfosUpgradeMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(new GuildInfosUpgradeMessage((sbyte)guild.MaxTaxCollectors, (sbyte)guild.TaxCollectors.Count, (short)guild.TaxCollectorHealth, (short)guild.TaxCollectorDamageBonuses,
                (short)guild.TaxCollectorPods, (short)guild.TaxCollectorProspecting, (short)guild.TaxCollectorWisdom, (short)guild.Boost,
                Guild.TAX_COLLECTOR_SPELLS, guild.GetTaxCollectorSpellsLevels().Select(x => (sbyte)x)));
        }

        public static void SendGuildInformationsPaddocksMessage(IPacketReceiver client)
        {
            client.Send(new GuildInformationsPaddocksMessage(0, new PaddockContentInformations[0]));
        }

        public static void SendGuildHousesInformationMessage(IPacketReceiver client)
        {
            client.Send(new GuildHousesInformationMessage(new HouseInformationsForGuild[0]));
        }

        public static void SendGuildJoinedMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildJoinedMessage(member.Guild.GetGuildInformations(), (int)member.Rights, true));
        }

        public static void SendGuildMemberLeavingMessage(IPacketReceiver client, GuildMember member, bool kicked)
        {
            client.Send(new GuildMemberLeavingMessage(kicked, member.Id));
        }

        public static void SendGuildCreationStartedMessage(IPacketReceiver client)
        {
            client.Send(new GuildCreationStartedMessage());
        }

        public static void SendGuildModificationStartedMessage(IPacketReceiver client, bool changeName, bool changeEmblem)
        {
            client.Send(new GuildModificationStartedMessage(changeName, changeEmblem));
        }

        public static void SendGuildMotdMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(new GuildMotdMessage(guild.MotdContent, guild.MotdDate.GetUnixTimeStamp(), guild.MotdMember?.Id ?? 0, guild.MotdMember?.Name ?? "Unknown"));
        }

        public static void SendGuildBulletinMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(new GuildBulletinMessage(guild.BulletinContent, guild.BulletinDate.GetUnixTimeStamp(), guild.BulletinMember?.Id ?? 0, guild.BulletinMember?.Name ?? "Unknown", guild.LastNotifiedDate.GetUnixTimeStamp()));
        }

        public static void SendGuildMotdSetErrorMessage(IPacketReceiver client, GuildMotdErrorEnum error)
        {
            client.Send(new GuildMotdSetErrorMessage((sbyte)error));
        }

        public static void SendGuildBulletinSetErrorMessage(IPacketReceiver client, SocialNoticeErrorEnum error)
        {
            client.Send(new GuildBulletinSetErrorMessage((sbyte)error));
        }

        public static void SendGuildFactsErrorMessage(IPacketReceiver client, int guildId)
        {
            client.Send(new GuildFactsErrorMessage(guildId));
        }
    }
}
