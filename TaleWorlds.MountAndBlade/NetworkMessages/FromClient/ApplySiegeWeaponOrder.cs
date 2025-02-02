using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000027 RID: 39
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplySiegeWeaponOrder : GameNetworkMessage
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000395E File Offset: 0x00001B5E
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00003966 File Offset: 0x00001B66
		public SiegeWeaponOrderType OrderType { get; private set; }

		// Token: 0x06000142 RID: 322 RVA: 0x0000396F File Offset: 0x00001B6F
		public ApplySiegeWeaponOrder(SiegeWeaponOrderType orderType)
		{
			this.OrderType = orderType;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000397E File Offset: 0x00001B7E
		public ApplySiegeWeaponOrder()
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00003988 File Offset: 0x00001B88
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (SiegeWeaponOrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000039AA File Offset: 0x00001BAA
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000039BC File Offset: 0x00001BBC
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed | MultiplayerMessageFilter.Orders;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000039C4 File Offset: 0x00001BC4
		protected override string OnGetLogFormat()
		{
			return "Apply siege order: " + this.OrderType;
		}
	}
}
