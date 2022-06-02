using ExitGames.Logging;
using Photon.SocketServer;
using System.IO;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MyGameServer.Manager;
using Common;
using MyGameServer.Handler;
using System.Collections.Generic;

namespace MyGameServer
{
    class MyGameServer : ApplicationBase//服务器启动初始化，客户端链接时使用此类，具体请求由ClientPeer处理
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public static new MyGameServer Instance
        {
            get;
            private set;
        }

        public List<ClientPeer> peerList = new List<ClientPeer>();//通过这个列表可以访问所有客户端的peer，从而向任何一个客户端发送数据
        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();

        //当一个客户端请求链接时
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接进来了");
            ClientPeer clientPeer = new ClientPeer(initRequest);//返回添加到客户端连接链表
            peerList.Add(clientPeer);
            return clientPeer;
        }

        //初始化
        protected override void Setup()
        {//总初始化，日志，请求处理器，
            Instance = this;
            IUserManager userManager = new UserManager();
            //日志的初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(this.ApplicationRootPath,"bin_Win64"), "log");
            // PATH:ApplicationRootPath\\bin_Win64\\log 日志输出路径
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));//配置文件路径配置
            if(configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(configFileInfo);
            }
            log.Info(userManager.VerifyUser("dsffsf", "sfgs"));
            log.Info(userManager.VerifyUser("safsdf", "fssfseg"));
            log.Info("Setup completed 初始化完成");

            InitHandler();
        }

        public void InitHandler()
        {//初始化请求处理器
            LoginHandler loginHandler = new LoginHandler();
            HandlerDict.Add(loginHandler.opCode, loginHandler);
            RegisterHandler registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.opCode, registerHandler);
            SyncPositionHandler syncPositionHandler = new SyncPositionHandler();
            HandlerDict.Add(syncPositionHandler.opCode, syncPositionHandler);
            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            HandlerDict.Add(syncPlayerHandler.opCode, syncPlayerHandler);
            DefaultHandler defaultHandler = new DefaultHandler();
            HandlerDict.Add(defaultHandler.opCode, defaultHandler);
            //log.Info("获取注册处理类:");
            //log.Info(DictTool.GetValue<OperationCode, BaseHandler>(HandlerDict,(OperationCode)OperationCode.Register).opCode);
        }

        //server关闭
        protected override void TearDown()
        {
            log.Info("Server TearDown 服务器应用关闭");
        }
    }
}
