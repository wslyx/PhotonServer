using Photon.SocketServer;
using Common;
using Common.Tools;

namespace MyGameServer.Handler
{
    class SyncPositionHandler : BaseHandler
    {
        public SyncPositionHandler()
        {
            opCode = OperationCode.SyncPosition;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            float x = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.X);
            float y = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Y);
            float z = (float)DictTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Z);
            MyGameServer.log.Info("x:"+x+" y:"+y+" z:"+z);
            peer.x = x;peer.y = y;peer.z = z;
        }
    }
}
