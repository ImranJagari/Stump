﻿using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.BaseServer.IPC.Objects;

namespace Stump.Server.AuthServer.Database
{
      public static class UserGroupDefaultValues
    {
        [Initialization(InitializationPass.First, Silent = true)]
        public static void CreateDefaultValues()
        {
            var db = AuthServer.Instance.DBAccessor.Database;

            foreach (var role in from RoleEnum role in Enum.GetValues(typeof(RoleEnum)) where role != RoleEnum.None where !db.Query<UserGroupRecord>("SELECT * FROM groups WHERE groups.Role = " + (int) role).Any() select role)
            {
                db.Insert(new UserGroupRecord
                {
                    Name = role.ToString(),
                    IsGameMaster = role >= RoleEnum.GameMaster,
                    Role = role
                });
            }
        }
    }

    public class UserGroupRelator
    {
        public static string FetchQuery = "SELECT * FROM groups";

        /// <summary>
        ///     Use string.Format
        /// </summary>
        public static string FindUserById = "SELECT * FROM groups WHERE groups.Id = {0}";
    }

    [TableName("groups")]
    public class UserGroupRecord : IAutoGeneratedRecord, ISaveIntercepter
    {
        private List<int> m_availableServers;
        private string m_availableServersCSV;
        private string m_availableCommandsCSV;
        private List<string> m_availableCommands;

        public UserGroupRecord()
        {
            AvailableServers = new List<int>();
        }

        public UserGroupRecord(UserGroupData userGroup)
        {
            Id = userGroup.Id;
            Name = userGroup.Name;
            Role = userGroup.Role;
            IsGameMaster = userGroup.IsGameMaster;
            AvailableServers = userGroup.Servers == null ? new List<int>() : userGroup.Servers.ToList();
            AvailableCommands = userGroup.Commands == null ? new List<string>() : userGroup.Commands.ToList();
        }

        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public RoleEnum Role
        {
            get;
            set;
        }

        public bool IsGameMaster
        {
            get;
            set;
        }

        [NullString]
        public string AvailableServersCSV
        {
            get { return m_availableServersCSV; }
            set
            {
                m_availableServersCSV = value;
                m_availableServers = value.FromCSV<int>(",").ToList();
            }
        }

        [Ignore]
        public List<int> AvailableServers
        {
            get { return m_availableServers; }
            set { m_availableServers = value; }
        }

        [NullString]
        public string AvailableCommandsCSV
        {
            get { return m_availableCommandsCSV; }
            set
            {
                m_availableCommandsCSV = value;
                m_availableCommands = value.FromCSV<string>(",").ToList();
            }
        }

        [Ignore]
        public List<string> AvailableCommands
        {
            get { return m_availableCommands; }
            set { m_availableCommands = value; }
        }

        public void BeforeSave(bool insert)
        {
            m_availableServersCSV = m_availableServers != null ? m_availableServers.ToCSV(",") : null;
        }

        public bool CanAccessWorld(WorldServer world)
        {
            return world.RequiredRole <= Role || AvailableServers.Contains(world.Id);
        }

        public UserGroupData GetGroupData()
        {
            return new UserGroupData()
            {
                Id = Id,
                Name = Name,
                IsGameMaster = IsGameMaster,
                Role = Role,
                Servers = AvailableServers,
                Commands = AvailableCommands,
            };
        }
    }
}