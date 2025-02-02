using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000016 RID: 22
	public interface ISessionlessClientDriverProvider<T> where T : SessionlessClient<T>
	{
		// Token: 0x06000064 RID: 100
		ISessionlessClientDriver CreateDriver(T client);
	}
}
