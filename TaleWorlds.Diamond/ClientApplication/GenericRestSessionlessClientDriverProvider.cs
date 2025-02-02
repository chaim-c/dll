using System;
using TaleWorlds.Diamond.Rest;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x02000063 RID: 99
	public class GenericRestSessionlessClientDriverProvider<T> : ISessionlessClientDriverProvider<T> where T : SessionlessClient<T>
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00006A44 File Offset: 0x00004C44
		public GenericRestSessionlessClientDriverProvider(string address, ushort port, bool isSecure, IHttpDriver httpDriver)
		{
			this._address = address;
			this._port = port;
			this._isSecure = isSecure;
			this._httpDriver = httpDriver;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00006A69 File Offset: 0x00004C69
		public ISessionlessClientDriver CreateDriver(T sessionlessClient)
		{
			return new SessionlessClientRestDriver(this._address, this._port, this._isSecure, this._httpDriver);
		}

		// Token: 0x040000DB RID: 219
		private string _address;

		// Token: 0x040000DC RID: 220
		private ushort _port;

		// Token: 0x040000DD RID: 221
		private bool _isSecure;

		// Token: 0x040000DE RID: 222
		private IHttpDriver _httpDriver;
	}
}
