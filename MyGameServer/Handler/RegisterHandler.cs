using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Tools;
using MyGameServer.Manager;
using MyGameServer.Model;

namespace MyGameServer.Handler
{
    class RegisterHandler:BaseHandler
    {
        public RegisterHandler()
        {
            opCode = OperationCode.Register;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            MyGameServer.log.Info("处理注册请求");
            string username = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;//从发送请求的字典中获取用户名
            string password = DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;//从发送请求的字典中获取密码
            UserManager userManager = new UserManager();//数据库用户管理器
            User user = userManager.GetByUsername(username);//根据用户名获取用户

            OperationResponse operationResponse = new OperationResponse(operationRequest.OperationCode);//新建操作响应用来返回给客户端
            if(user==null)//对应的用户名是否有用户
            {//无用户
                user = new User() { Username = username, Password = password };
                userManager.Add(user);
                operationResponse.ReturnCode = (short)ReturnCode.Success;
            }
            else
            {//有用户
                operationResponse.ReturnCode = (short)ReturnCode.Failed;
            }
            MyGameServer.log.Info("返回给客户端结果");
            MyGameServer.log.Info((OperationCode)operationRequest.OperationCode);
            peer.SendOperationResponse(operationResponse, sendParameters);//结果发送到客户端
                                                                          //operationResponse包含要传送的信息，sendParameters包含网络通道信息
        }
    }
}
