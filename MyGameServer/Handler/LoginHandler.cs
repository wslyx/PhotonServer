using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using MyGameServer.Manager;

namespace MyGameServer.Handler
{
    class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            opCode = Common.OperationCode.Login;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {//接收客户端的请求
            MyGameServer.log.Info("处理登录请求");
            string Username = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;//从发送请求的字典中获取用户名
            string Password = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;//从发送请求的字典中获取密码
            UserManager userManager = new UserManager();//数据库用户管理器
            bool isSuccess =  userManager.VerifyUser(Username, Password);

            OperationResponse operationResponse = new OperationResponse(operationRequest.OperationCode);
            if(isSuccess)
            {
                operationResponse.ReturnCode = (short)ReturnCode.Success;
                peer.Username = Username;
            }
            else
            {
                operationResponse.ReturnCode = (short)ReturnCode.Failed;
            }
            peer.SendOperationResponse(operationResponse, sendParameters);//发送结果到客户端
        }
    }
}
