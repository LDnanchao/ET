using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2M_AddNumHandler : MessageSessionHandler<C2M_AddNum, M2C_AddNum>
    {
        protected override async ETTask Run(Session session, C2M_AddNum request, M2C_AddNum response)
        {
            response.Message = "123";
            await ETTask.CompletedTask;
        }
    }
}
