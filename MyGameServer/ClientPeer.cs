using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Tools;
using MyGameServer.Handler;
using Common;

namespace MyGameServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer//此类处理所有具体的请求
    {
        public ClientPeer(InitRequest initRequest):base(initRequest)
        {//客户端建立连接时被调用

        }
        
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {//处理客户端断开链接之后的后续工作，客户端断开连接之后此函数被调用
            MyGameServer.log.Info("一个客户端断开连接");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {//客户端 OpCustom((byte)opCode, data, true) 时此函数被调用
         //OperationRequest中包含OperationCode，以及一个字典
         //SendParameters中包含Channel ID，（用以区别多个客户端），以及与对应客户端通信的参数
            BaseHandler handler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            //根据操作码从字典中获取处理类
            if(handler!=null)
            {//如果有操作码对应的处理类
                MyGameServer.log.Info("收到请求且找到处理类,处理代码为:");
                MyGameServer.log.Info((OperationCode)operationRequest.OperationCode);
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {//如果没有对应的类，就使用默认的处理类
                MyGameServer.log.Info("收到请求,未找到处理类:");
                MyGameServer.log.Info(operationRequest.OperationCode);
                BaseHandler defalutHandler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.HandlerDict, OperationCode.Default);
                defalutHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }
    }
}
