using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    public abstract class BaseHandler//客户端请求处理接口
    {
        public OperationCode opCode;//操作代码，用来区分操作类型

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer);//处理函数接口
    }
}
