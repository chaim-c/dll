using System;
using TaleWorlds.Diamond.Rest;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x02000061 RID: 97
	public class GenericThreadedRestSessionProvider<T> : IClientSessionProvider<T> where T : Client<T>
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000699D File Offset: 0x00004B9D
		public GenericThreadedRestSessionProvider(string address, ushort port, bool isSecure, IHttpDriver httpDriver)
		{
			this._address = address;
			this._port = port;
			this._isSecure = isSecure;
			this._httpDriver = httpDriver;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000069C4 File Offset: 0x00004BC4
		public IClientSession CreateSession(T client)
		{
			int threadSleepTime;
			if (!client.Application.Parameters.TryGetParameterAsInt("ThreadedClientSession.ThreadSleepTime", out threadSleepTime))
			{
				threadSleepTime = 100;
			}
			ThreadedClient threadedClient = new ThreadedClient(client);
			ClientRestSession session = new ClientRestSession(threadedClient, this._address, this._port, this._isSecure, this._httpDriver);
			return new ThreadedClientSession(threadedClient, session, threadSleepTime);
		}

		// Token: 0x040000D5 RID: 213
		public const int DefaultThreadSleepTime = 100;

		// Token: 0x040000D6 RID: 214
		private string _address;

		// Token: 0x040000D7 RID: 215
		private ushort _port;

		// Token: 0x040000D8 RID: 216
		private bool _isSecure;

		// Token: 0x040000D9 RID: 217
		private IHttpDriver _httpDriver;
	}
}
