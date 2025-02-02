using System;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200000F RID: 15
	public interface IClientSessionProvider<T> where T : Client<T>
	{
		// Token: 0x0600004F RID: 79
		IClientSession CreateSession(T session);
	}
}
