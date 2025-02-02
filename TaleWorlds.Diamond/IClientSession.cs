using System;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200000E RID: 14
	public interface IClientSession
	{
		// Token: 0x06000048 RID: 72
		void Connect();

		// Token: 0x06000049 RID: 73
		void Disconnect();

		// Token: 0x0600004A RID: 74
		void Tick();

		// Token: 0x0600004B RID: 75
		Task<LoginResult> Login(LoginMessage message);

		// Token: 0x0600004C RID: 76
		void SendMessage(Message message);

		// Token: 0x0600004D RID: 77
		Task<T> CallFunction<T>(Message message) where T : FunctionResult;

		// Token: 0x0600004E RID: 78
		Task<bool> CheckConnection();
	}
}
