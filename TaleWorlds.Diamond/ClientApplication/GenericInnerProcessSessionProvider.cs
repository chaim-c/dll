using System;
using TaleWorlds.Diamond.InnerProcess;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x0200005F RID: 95
	public class GenericInnerProcessSessionProvider<T> : IClientSessionProvider<T> where T : Client<T>
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00006924 File Offset: 0x00004B24
		public GenericInnerProcessSessionProvider(InnerProcessManager innerProcessManager, ushort port)
		{
			this._innerProcessManager = innerProcessManager;
			this._port = port;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000693A File Offset: 0x00004B3A
		public IClientSession CreateSession(T session)
		{
			return new InnerProcessClient(this._innerProcessManager, session, (int)this._port);
		}

		// Token: 0x040000CF RID: 207
		private InnerProcessManager _innerProcessManager;

		// Token: 0x040000D0 RID: 208
		private ushort _port;
	}
}
