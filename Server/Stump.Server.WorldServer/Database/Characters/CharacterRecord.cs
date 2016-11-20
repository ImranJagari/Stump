using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.Breeds;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Maps;
using Stump.DofusProtocol.Enums.Custom;

namespace Stump.Server.WorldServer.Database.Characters
{
    public class CharacterRelator
    {
        public static string FetchQuery =
            "SELECT * FROM characters";

        public static string FetchById =
            "SELECT * FROM characters WHERE Id = {0}";

        /// <summary>
        /// Use SQL parameters
        /// </summary>
        public static string FetchByName =
            "SELECT * FROM characters WHERE Name = @0";
        /// <summary>
        /// Use string.Format(ToCSV(","))
        /// </summary>
        public static string FetchByMultipleId =
            "SELECT * FROM characters WHERE Id IN ({0})";
    }

    [TableName("characters")]
    public class CharacterRecord : IAutoGeneratedRecord, ISaveIntercepter
    {
        private ActorLook m_entityLook;

        public CharacterRecord()
        {
            TitleParam = string.Empty;
        }

        public CharacterRecord(Breed breed)
          : this()
        {
            Breed = (PlayableBreedEnum)breed.Id;

            BaseHealth = (ushort)(breed.StartHealthPoint + breed.StartLevel * 5);
            AP = breed.StartActionPoints;
            MP = breed.StartMovementPoints;
            Prospection = breed.StartProspection;
            SpellsPoints = breed.StartSpellsPoints;
            StatsPoints = breed.StartStatsPoints;
            Strength = breed.StartStrength;
            Vitality = breed.StartVitality;
            Wisdom = breed.StartWisdom;
            Chance = breed.StartChance;
            Intelligence = breed.StartIntelligence;
            Agility = breed.StartAgility;

            MapId = breed.StartMap;
            CellId = breed.StartCell;
            Direction = breed.StartDirection;

            SpellsPoints = (ushort)(breed.StartLevel - 1);
            StatsPoints = (ushort)((breed.StartLevel - 1) * 5);
            Kamas = breed.StartKamas;

            if (breed.StartLevel > 100)
                AP++;

            PlayerLifeStatus = PlayerLifeStatusEnum.STATUS_ALIVE_AND_KICKING;
            Emotes = new List<EmotesEnum> { EmotesEnum.EMOTE_S_ASSEOIR };
            SmileyPacks = new List<SmileyPacksEnum> { SmileyPacksEnum.BASIC_PACK };
        }

        #region Record Properties

        private string m_customEntityLookString;
        private string m_entityLookString;
        private byte[] m_knownZaapsBin;
        private int? m_spawnMapId;


        // Primitive properties

        public int Id
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public DateTime? LastUsage
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public PlayableBreedEnum Breed
        {
            get;
            set;
        }

        public SexTypeEnum Sex
        {
            get;
            set;
        }

        public int Head
        {
            get;
            set;
        }

        [NullString]
        public string EntityLookString
        {
            get { return m_entityLookString; }
            set
            {
                m_entityLookString = value;
                m_entityLook = !string.IsNullOrEmpty(EntityLookString) ? ActorLook.Parse(m_entityLookString) : null;
            }
        }

        [NullString]
        public string CustomEntityLookString
        {
            get { return m_customEntityLookString; }
            set
            {
                m_customEntityLookString = value;
                m_customEntityLook = !string.IsNullOrEmpty(m_customEntityLookString)
                                         ? ActorLook.Parse(m_customEntityLookString)
                                         : null;
            }
        }

        public bool CustomLookActivated
        {
            get;
            set;
        }

        public short? TitleId
        {
            get;
            set;
        }

        public string TitleParam
        {
            get;
            set;
        }

        [Ignore]
        public List<short> Titles
        {
            get
            {
                return m_titles;
            }
            set
            {
                m_titles = value;
                m_titlesCSV = m_titles.ToCSV(",");
            }
        }

        [NullString]
        public string TitlesCSV
        {
            get
            {
                return m_titlesCSV;
            }
            set
            {
                m_titlesCSV = value;
                m_titles = !string.IsNullOrEmpty(m_titlesCSV) ? m_titlesCSV.FromCSV<short>(",").ToList() : new List<short>();
            }
        }

        public short? Ornament
        {
            get;
            set;
        }

        [Ignore]
        public List<short> Ornaments
        {
            get
            {
                return m_ornaments;
            }
            set
            {
                m_ornaments = value;
                m_ornamentsCSV = m_ornaments.ToCSV(",");
            }
        }

        [NullString]
        public string OrnamentsCSV
        {
            get
            {
                return m_ornamentsCSV;
            }
            set
            {
                m_ornamentsCSV = value;
                m_ornaments = !string.IsNullOrEmpty(m_ornamentsCSV) ? m_ornamentsCSV.FromCSV<short>(",").ToList() : new List<short>();
            }
        }

        [Ignore]
        public List<EmotesEnum> Emotes
        {
            get
            {
                return m_emotes;
            }
            set
            {
                m_emotes = value;
                m_emotesCSV = m_emotes.Select(x => (short)x).ToCSV(",");
            }
        }

        [NullString]
        public string EmotesCSV
        {
            get
            {
                return m_emotesCSV;
            }
            set
            {
                m_emotesCSV = value;
                m_emotes = !string.IsNullOrEmpty(m_emotesCSV) ? m_emotesCSV.FromCSV<short>(",").Select(x => (EmotesEnum)x).ToList() : new List<EmotesEnum>();
            }
        }

        [Ignore]
        public List<SmileyPacksEnum> SmileyPacks
        {
            get
            {
                return m_smileyPacks;
            }
            set
            {
                m_smileyPacks = value;
                m_smileyPacksCSV = m_smileyPacks.Select(x => (short)x).ToCSV(",");
            }
        }

        [NullString]
        public string SmileyPacksCSV
        {
            get
            {
                return m_smileyPacksCSV;
            }
            set
            {
                m_smileyPacksCSV = value;
                m_smileyPacks = !string.IsNullOrEmpty(m_smileyPacksCSV) ? m_smileyPacksCSV.FromCSV<short>(",").Select(x => (SmileyPacksEnum)x).ToList() : new List<SmileyPacksEnum>();
            }
        }

        public int MapId
        {
            get;
            set;
        }

        public short CellId
        {
            get;
            set;
        }

        public DirectionsEnum Direction
        {
            get;
            set;
        }

        public int BaseHealth
        {
            get;
            set;
        }

        public int DamageTaken
        {
            get;
            set;
        }

        public int AP
        {
            get;
            set;
        }

        public int MP
        {
            get;
            set;
        }

        public int Prospection
        {
            get;
            set;
        }

        public int Strength
        {
            get;
            set;
        }

        public int Chance
        {
            get;
            set;
        }

        public int Vitality
        {
            get;
            set;
        }

        public int Wisdom
        {
            get;
            set;
        }

        public int Intelligence
        {
            get;
            set;
        }

        public int Agility
        {
            get;
            set;
        }

        public short PermanentAddedStrength
        {
            get;
            set;
        }

        public short PermanentAddedChance
        {
            get;
            set;
        }

        public short PermanentAddedVitality
        {
            get;
            set;
        }

        public short PermanentAddedWisdom
        {
            get;
            set;
        }

        public short PermanentAddedIntelligence
        {
            get;
            set;
        }

        public short PermanentAddedAgility
        {
            get;
            set;
        }

        public int Kamas
        {
            get;
            set;
        }

        public long Experience
        {
            get;
            set;
        }

        public short EnergyMax
        {
            get;
            set;
        }

        public short Energy
        {
            get;
            set;
        }

        public ushort StatsPoints
        {
            get;
            set;
        }

        public ushort SpellsPoints
        {
            get;
            set;
        }

        public AlignmentSideEnum AlignmentSide
        {
            get;
            set;
        }

        public sbyte AlignmentValue
        {
            get;
            set;
        }

        public ushort Honor
        {
            get;
            set;
        }

        public ushort Dishonor
        {
            get;
            set;
        }

        public bool PvPEnabled
        {
            get;
            set;
        }


        public int? SpawnMapId
        {
            get { return m_spawnMapId; }
            set
            {
                m_spawnMapId = value;
                m_spawnMap = null;
            }
        }

        public bool WarnOnConnection
        {
            get;
            set;
        }

        public bool WarnOnGuildConnection
        {
            get;
            set;
        }

        public bool WarnOnLevel
        {
            get;
            set;
        }

        public DateTime? MuteUntil
        {
            get;
            set;
        }

        public sbyte MandatoryChanges
        {
            get;
            set;
        }

        public sbyte PossibleChanges
        {
            get;
            set;
        }

        public int? LeftFightId
        {
            get;
            set;
        }

        public int PrestigeRank
        {
            get;
            set;
        }

        public int ArenaRank
        {
            get;
            set;
        }

        public int ArenaMaxRank
        {
            get;
            set;
        }

        public DateTime ArenaDailyDate
        {
            get;
            set;
        }

        public int ArenaDailyMaxRank
        {
            get;
            set;
        }

        public int ArenaDailyMatchsCount
        {
            get;
            set;
        }

        public int ArenaDailyMatchsWon
        {
            get;
            set;
        }

        public DateTime ArenaPenalityDate
        {
            get;
            set;
        }

        public int? EquippedMount
        {
            get;
            set;
        }

        public bool IsRiding
        {
            get;
            set;
        }

        public PlayerLifeStatusEnum PlayerLifeStatus
        {
            get;
            set;
        }

        public int SmileyMoodId
        {
            get;
            set;
        }

        public DateTime? DeletedDate
        {
            get;
            set;
        }

        public bool IsDeleted => DeletedDate != null;

        #endregion

      


        [Ignore]
        public ActorLook EntityLook
        {
            get { return m_entityLook; }
            set
            {
                m_entityLook = value;
                m_entityLookString = value != null ? value.ToString() : string.Empty;
            }
        }

        [Ignore]
        public ActorLook CustomEntityLook
        {
            get { return m_customEntityLook; }
            set
            {
                m_customEntityLook = value;
                m_customEntityLookString = value != null ? value.ToString() : string.Empty;
            }
        }

        #region Zaaps

        ActorLook m_customEntityLook;
        string m_customLookAsString;
        List<Map> m_knownZaaps = new List<Map>();
        Map m_spawnMap;
        List<short> m_titles = new List<short>();
        string m_titlesCSV;
        List<short> m_ornaments = new List<short>();
        string m_ornamentsCSV;
        List<EmotesEnum> m_emotes = new List<EmotesEnum>();
        string m_emotesCSV;
        List<SmileyPacksEnum> m_smileyPacks = new List<SmileyPacksEnum>();
        string m_smileyPacksCSV;

        [Ignore]
        public List<Map> KnownZaaps
        {
            get { return m_knownZaaps; }
            set
            {
                m_knownZaaps = value;
                m_knownZaapsBin = SerializeZaaps(m_knownZaaps);
            }
        }

        public byte[] KnownZaapsBin
        {
            get
            {
                return m_knownZaapsBin;
            }
            set
            {
                m_knownZaapsBin = value;
                m_knownZaaps = UnSerializeZaaps(KnownZaapsBin);
            }
        }

        [Ignore]
        public Map SpawnMap
        {
            get
            {
                if (!SpawnMapId.HasValue)
                    return null;

                return m_spawnMap ?? (m_spawnMap = Game.World.Instance.GetMap(SpawnMapId.Value));
            }
            set
            {
                m_spawnMap = value;

                if (value == null)
                    SpawnMapId = null;
                else
                    SpawnMapId = value.Id;
            }
        }


        private static byte[] SerializeZaaps(IReadOnlyList<Map> knownZaaps)
        {
            var result = new byte[knownZaaps.Count*4];

            for (var i = 0; i < knownZaaps.Count; i++)
            {
                result[i*4] = (byte) (knownZaaps[i].Id >> 24);
                result[i*4 + 1] = (byte) ((knownZaaps[i].Id >> 16) & 0xFF);
                result[i*4 + 2] = (byte) ((knownZaaps[i].Id >> 8) & 0xFF);
                result[i*4 + 3] = (byte) (knownZaaps[i].Id & 0xFF);
            }

            return result;
        }

        private static List<Map> UnSerializeZaaps(IList<byte> serialized)
        {
            var result = new List<Map>();

            for (var i = 0; i < serialized.Count; i += 4)
            {
                var id = serialized[i] << 24 | serialized[i + 1] << 16 | serialized[i + 2] << 8 | serialized[i + 3];

                var map = Game.World.Instance.GetMap(id);

                if (map == null)
                    throw new Exception("Map " + id + " not found");

                result.Add(map);
            }

            return result;
        }

        #endregion

        public void BeforeSave(bool insert)
        {
            m_knownZaapsBin = SerializeZaaps(m_knownZaaps);
            m_customEntityLookString = m_customEntityLook == null ? null : m_customEntityLook.ToString();
            m_entityLookString = m_entityLook == null ? null : m_entityLook.ToString();
            m_titlesCSV = m_titles.ToCSV(",");
            m_ornamentsCSV = m_ornaments.ToCSV(",");
            m_emotesCSV = m_emotes.Select(x => (short)x).ToCSV(",");
            m_smileyPacksCSV = m_smileyPacks.Select(x => (short)x).ToCSV(",");
        }
    }
}