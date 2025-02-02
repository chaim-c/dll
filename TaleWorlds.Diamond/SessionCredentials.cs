using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000023 RID: 35
	[DataContract]
	[Serializable]
	public sealed class SessionCredentials
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000320E File Offset: 0x0000140E
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003216 File Offset: 0x00001416
		[DataMember]
		public PeerId PeerId { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000321F File Offset: 0x0000141F
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003227 File Offset: 0x00001427
		[DataMember]
		public SessionKey SessionKey { get; private set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x00003230 File Offset: 0x00001430
		[JsonConstructor]
		public SessionCredentials(PeerId peerId, SessionKey sessionKey)
		{
			this.PeerId = peerId;
			this.SessionKey = sessionKey;
		}
	}
}
