using System;
using TaleWorlds.Diamond.Rest;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x02000060 RID: 96
	public class GenericRestSessionProvider<T> : IClientSessionProvider<T> where T : Client<T>
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00006953 File Offset: 0x00004B53
		public GenericRestSessionProvider(string address, ushort port, bool isSecure, IHttpDriver httpDriver)
		{
			this._address = address;
			this._port = port;
			this._isSecure = isSecure;
			this._httpDriver = httpDriver;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006978 File Offset: 0x00004B78
		public IClientSession CreateSession(T session)
		{
			return new ClientRestSession(session, this._address, this._port, this._isSecure, this._httpDriver);
		}

		// Token: 0x040000D1 RID: 209
		private string _address;

		// Token: 0x040000D2 RID: 210
		private ushort _port;

		// Token: 0x040000D3 RID: 211
		private bool _isSecure;

		// Token: 0x040000D4 RID: 212
		private IHttpDriver _httpDriver;
	}
}
