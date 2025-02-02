using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000018 RID: 24
	[DataContract]
	[Serializable]
	public sealed class LoginResult : FunctionResult
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000029A2 File Offset: 0x00000BA2
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000029AA File Offset: 0x00000BAA
		[DataMember]
		public PeerId PeerId { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000029B3 File Offset: 0x00000BB3
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000029BB File Offset: 0x00000BBB
		[DataMember]
		public SessionKey SessionKey { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000029C4 File Offset: 0x00000BC4
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000029CC File Offset: 0x00000BCC
		[DataMember]
		public bool Successful { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000029D5 File Offset: 0x00000BD5
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000029DD File Offset: 0x00000BDD
		[DataMember]
		public string ErrorCode { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000029E6 File Offset: 0x00000BE6
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000029EE File Offset: 0x00000BEE
		[DataMember]
		public Dictionary<string, string> ErrorParameters { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000029F7 File Offset: 0x00000BF7
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000029FF File Offset: 0x00000BFF
		[DataMember]
		public string ProviderResponse { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002A08 File Offset: 0x00000C08
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002A10 File Offset: 0x00000C10
		[DataMember]
		public LoginResultObject LoginResultObject { get; private set; }

		// Token: 0x06000077 RID: 119 RVA: 0x00002A19 File Offset: 0x00000C19
		public LoginResult()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002A21 File Offset: 0x00000C21
		public LoginResult(PeerId peerId, SessionKey sessionKey, LoginResultObject loginResultObject)
		{
			this.PeerId = peerId;
			this.SessionKey = sessionKey;
			this.Successful = true;
			this.ErrorCode = "";
			this.LoginResultObject = loginResultObject;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002A50 File Offset: 0x00000C50
		public LoginResult(PeerId peerId, SessionKey sessionKey) : this(peerId, sessionKey, null)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002A5B File Offset: 0x00000C5B
		public LoginResult(string errorCode, Dictionary<string, string> parameters = null)
		{
			this.ErrorCode = errorCode;
			this.Successful = false;
			this.ErrorParameters = parameters;
		}
	}
}
