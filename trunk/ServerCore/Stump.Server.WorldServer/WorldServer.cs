// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using Stump.BaseCore.Framework.Attributes;
using Stump.Database;
using Stump.DofusProtocol;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initializing;
using Stump.Server.BaseServer.IPC;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Actions;
using Stump.Server.WorldServer.Commands;
using Stump.Server.WorldServer.Global;
using Stump.Server.WorldServer.IPC;

namespace Stump.Server.WorldServer
{
    public class WorldServer : ServerBase<WorldServer>
    {
        public static WorldServerInformation ServerInformation;

        public WorldServer()
            : base(Definitions.ConfigFilePath, Definitions.SchemaFilePath)
        {
        }

        /// <summary>
        ///   ID of current world server
        /// </summary>
        [Variable]
        public static int ServerId
        {
            get { return ServerInformation.Id; }
            set
            {
                if (ServerInformation == null)
                    ServerInformation = new WorldServerInformation();

                ServerInformation.Id = value;
            }
        }

        /// <summary>
        ///   Name of current world server
        /// </summary>
        [Variable]
        public static string ServerName
        {
            get { return ServerInformation.Name; }
            set
            {
                if (ServerInformation == null)
                    ServerInformation = new WorldServerInformation();

                ServerInformation.Name = value;
            }
        }

        /// <summary>
        ///   Adress of current world server
        /// </summary>
        [Variable]
        public static string ServerAddress
        {
            get { return ServerInformation.Address; }
            set
            {
                if (ServerInformation == null)
                    ServerInformation = new WorldServerInformation();

                ServerInformation.Address = value;
            }
        }

        /// <summary>
        ///   Port of current world server
        /// </summary>
        [Variable]
        public static ushort ServerPort
        {
            get { return ServerInformation.Port; }
            set
            {
                if (ServerInformation == null)
                    ServerInformation = new WorldServerInformation();

                ServerInformation.Port = value;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            ConsoleInterface = new WorldConsole();
            ConsoleBase.SetTitle("#Stump World Server : " + ServerName);


            logger.Info("Initializing Database...");
            DatabaseAccessor.Initialize(
                Assembly.GetExecutingAssembly(),
                Definitions.DatabaseRevision,
                DatabaseType.MySQL,
                DatabaseService.WorldServer);

            logger.Info("Opening Database...");
            DatabaseAccessor.OpenDatabase();

            logger.Info("Register Messages...");
            MessageReceiver.Initialize();
            ProtocolTypeManager.Initialize();

            logger.Info("Register Packet Handlers...");
            HandlerManager.RegisterAll(typeof (WorldServer).Assembly);

            logger.Info("Register Commands...");
            CommandManager.RegisterAll<WorldCommand, WorldSubCommand>();

            logger.Info("Start Parallel Initialization Procedure...");
            StageManager.Initialize(GetType().Assembly);

            logger.Info("Build World...");
            World.Instance.Initialize();

            logger.Info("Initializing IPC Client..");
            IpcAccessor.Instance.Initialize();
        }

        public override void Start()
        {
            base.Start();

            logger.Info("Starting Console Handler Interface...");
            ConsoleInterface.Start();

            logger.Info("Starting Authenfication Server Communication on " + IpcAccessor.IpcAuthPort + "/" +
                        IpcAccessor.IpcWorldPort + "...");
            IpcAccessor.Instance.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override BaseClient CreateClient(Socket s)
        {
            return new WorldClient(s);
        }

        public override void OnShutdown()
        {
        }

        public IEnumerable<WorldClient> GetClients()
        {
            return MessageListener.ClientList.OfType<WorldClient>();
        }

        public IEnumerable<WorldClient> GetClientsUsingAccount(AccountRecord account)
        {
            IEnumerable<WorldClient> clients = (from WorldClient entry in MessageListener.ClientList
                                                where entry.Account.Id == account.Id
                                                select entry);

            return clients;
        }

        public IEnumerable<WorldClient> GetCharacters()
        {
            return GetClients().Where(c => c.ActiveCharacter != null);
        }

    }
}