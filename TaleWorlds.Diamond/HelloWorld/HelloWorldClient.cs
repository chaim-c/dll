using System;
using System.Threading.Tasks;
using TaleWorlds.Diamond.ClientApplication;

namespace TaleWorlds.Diamond.HelloWorld
{
	// Token: 0x02000058 RID: 88
	public class HelloWorldClient : SessionlessClient<HelloWorldClient>
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00005CF8 File Offset: 0x00003EF8
		public HelloWorldClient(DiamondClientApplication diamondClientApplication, ISessionlessClientDriverProvider<HelloWorldClient> driverProvider) : base(diamondClientApplication, driverProvider)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005D02 File Offset: 0x00003F02
		public void SendTestMessage(HelloWorldTestMessage message)
		{
			base.SendMessage(message);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005D0C File Offset: 0x00003F0C
		public async Task<HelloWorldTestFunctionResult> CallTestFunction(HelloWorldTestFunctionMessage message)
		{
			return await base.CallFunction<HelloWorldTestFunctionResult>(message);
		}
	}
}
