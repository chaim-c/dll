using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200001B RID: 27
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestCultureChange : GameNetworkMessage
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000030DE File Offset: 0x000012DE
		public BasicCultureObject Culture { get; private set; }

		// Token: 0x060000D4 RID: 212 RVA: 0x000030E7 File Offset: 0x000012E7
		public RequestCultureChange()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000030EF File Offset: 0x000012EF
		public RequestCultureChange(BasicCultureObject culture)
		{
			this.Culture = culture;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000030FE File Offset: 0x000012FE
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteObjectReferenceToPacket(this.Culture, CompressionBasic.GUIDCompressionInfo);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003110 File Offset: 0x00001310
		protected override bool OnRead()
		{
			bool result = true;
			this.Culture = (BasicCultureObject)GameNetworkMessage.ReadObjectReferenceFromPacket(MBObjectManager.Instance, CompressionBasic.GUIDCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000313C File Offset: 0x0000133C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003144 File Offset: 0x00001344
		protected override string OnGetLogFormat()
		{
			return "Requested culture: " + this.Culture.Name;
		}
	}
}
