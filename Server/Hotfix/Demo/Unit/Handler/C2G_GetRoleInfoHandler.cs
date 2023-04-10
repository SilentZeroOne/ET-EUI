

using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public class C2G_GetRoleInfoHandler : AMRpcHandler<C2G_GetRoleInfo, G2C_GetRoleInfo>
    {
        protected override async ETTask Run(Session session, C2G_GetRoleInfo request, G2C_GetRoleInfo response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                session.Dispose();
                return;
            }

            var roleInfoList = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                    .Query<RoleInfo>(r => r.Id == request.UnitId);

            RoleInfoProto roleInfo = null;
            if (roleInfoList != null && roleInfoList.Count > 0)
            {
                roleInfo = roleInfoList[0].ToMessage();
            }

            response.RoleInfo = roleInfo;

            reply();
        }
    }
}