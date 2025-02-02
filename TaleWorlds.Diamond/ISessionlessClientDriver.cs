using System;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000015 RID: 21
	public interface ISessionlessClientDriver
	{
		// Token: 0x06000061 RID: 97
		void SendMessage(Message message);

		// Token: 0x06000062 RID: 98
		Task<T> CallFunction<T>(Message message) where T : FunctionResult;

		// Token: 0x06000063 RID: 99
		Task<bool> CheckConnection();
	}
}
