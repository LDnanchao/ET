using UnityEngine;
using QFramework;
using ET;
using ET.Client;
using System.Net.Sockets;
using System.Net;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class AddControl : ViewController
	{
		//建立一个actorid,每一个请求体都是单独的id
		async void Start()
		{
            await Login1();
            ClickButton.onClick.AddListener(async () =>
            {
               await AddNum();
            });
        }
		public async ETTask AddNum()
		{
            var fiber = await World.Instance.Scene.GetComponent<ClientSenderComponent>().Call(C2M_AddNum.Create()) as M2C_AddNum;
            Debug.Log($"addnum {fiber.Message}");
        }
        public async ETTask Login1()
        {
            string account = "123";
            string password = "123";
            var root = World.Instance.Scene;
            root.RemoveComponent<ClientSenderComponent>();
            ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();
            long playerId = await clientSenderComponent.LoginAsync(account, password);

            root.GetComponent<PlayerComponent>().MyId = playerId;
        }
      
    }
    
}
