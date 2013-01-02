﻿
using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO.Pem;
using Stump.Core.Attributes;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses.Tool;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.DofusProtocol.Types.Extensions;
using Stump.ORM;
using Stump.Server.AuthServer.Database;
using Stump.Server.AuthServer.IO;
using Stump.Server.AuthServer.IPC;
using Stump.Server.AuthServer.Managers;
using Stump.Server.AuthServer.Network;
using Stump.Server.BaseServer;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Handler;
using Stump.Server.BaseServer.IPC;
using Stump.Server.BaseServer.Network;
using Stump.Server.BaseServer.Plugins;
using DatabaseConfiguration = Stump.ORM.DatabaseConfiguration;

namespace Stump.Server.AuthServer
{
    public class AuthServer : ServerBase<AuthServer>
    {
        [Variable]
        public static readonly bool HostAutoDefined = true;

        /// <summary>
        /// Current server address. Used if HostAutoDefined = false
        /// </summary>
        [Variable]
        public static readonly string CustomHost = "127.0.0.1";

        /// <summary>
        /// Server port
        /// </summary>
        [Variable]
        public static readonly int Port = 443;

        [Variable]
        public static string IpcAddress = "net.tcp://localhost:9100";

        public static string Host;

        [Variable(Priority = 10)]
        public static DatabaseConfiguration DatabaseConfiguration = new DatabaseConfiguration
        {
            ProviderName = "MySql.Data.MySqlClient",
            Host = "localhost",
            DbName = "stump_auth",
            User = "root",
            Password = "",
        };

        public IpcHost IpcHost
        {
            get;
            private set;
        }

        public AuthPacketHandler HandlerManager
        {
            get;
            private set;
        }

        public AuthServer() :
            base(Definitions.ConfigFilePath, Definitions.SchemaFilePath)
        {
        }

        public override void Initialize()
        {
            try
            {
                base.Initialize();
                ConsoleInterface = new AuthConsole();
                ConsoleBase.SetTitle("#Stump Authentification Server");

                logger.Info("Initializing Database...");
                DBAccessor = new DatabaseAccessor(DatabaseConfiguration);
                DataManager<DatabaseAccessor>.DefaultDatabase = DBAccessor.Database;

                logger.Info("Opening Database...");
                DBAccessor.OpenConnection();

                logger.Info("Register Messages...");
                MessageReceiver.Initialize();
                ProtocolTypeManager.Initialize();

                logger.Info("Register Packets Handlers...");
                HandlerManager = AuthPacketHandler.Instance;
                HandlerManager.RegisterAll(Assembly.GetExecutingAssembly());

                logger.Info("Register Commands...");
                CommandManager.RegisterAll(Assembly.GetExecutingAssembly());

                logger.Info("Start World Servers Manager");
                WorldServerManager.Instance.Initialize();

                logger.Info("Initialize IPC Server..");
                IpcHost = new IpcHost(typeof(IpcOperations), typeof(IRemoteAuthOperations), IpcAddress);

                InitializationManager.InitializeAll();
                IsInitialized = true;
            }
            catch (Exception ex)
            {
                HandleCrashException(ex);
                Shutdown();
            }
        }

        protected override void OnPluginAdded(PluginContext plugincontext)
        {
            base.OnPluginAdded(plugincontext);
        }

        public override void Start()
        {
            base.Start();

            logger.Info("Start Ipc Server");
            IpcHost.Open();

            logger.Info("Starting Console Handler Interface...");
            ConsoleInterface.Start();

            logger.Info("Start listening on port : " + Port + "...");
            Host = HostAutoDefined ? IPAddress.Loopback.ToString() : CustomHost;
            ClientManager.Start(Host, Port);

            StartTime = DateTime.Now;
        }


        protected override void OnShutdown()
        {
            DBAccessor.CloseConnection();
        }

        protected override BaseClient CreateClient(Socket s)
        {
            return new AuthClient(s);
        }

        public IEnumerable<AuthClient> FindClients(Predicate<AuthClient> predicate)
        {
            return ClientManager.FindAll(predicate);
        }
    }
}