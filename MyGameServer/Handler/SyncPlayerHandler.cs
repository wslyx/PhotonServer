using Photon.SocketServer;
using Common;
using System.Collections.Generic;

namespace MyGameServer.Handler
{
    class SyncPlayerHandler : BaseHandler
    {
        public SyncPlayerHandler()
        {
            this.opCode = OperationCode.SyncPlayer;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            //取得所有已登录的用户名
            List<string> usernameList = new List<string>();
            foreach(ClientPeer tempPeer in MyGameServer.Instance.peerList)
            {
                if(string.IsNullOrEmpty(tempPeer.Username)==false && tempPeer!=peer)
                {
                    usernameList.Add(tempPeer.Username);
                }
            }
            usernameList.Add("UsernameForTest");//测试时人为添加
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.UsernameList, usernameList);
            OperationResponse operationResponse = new OperationResponse(operationRequest.OperationCode);
            operationResponse.Parameters = data;
            peer.SendOperationResponse(operationResponse, sendParameters);
        }
    }
}
