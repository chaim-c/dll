using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200007F RID: 127
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ConsumeWeaponAmount : GameNetworkMessage
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00008846 File Offset: 0x00006A46
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0000884E File Offset: 0x00006A4E
		public MissionObjectId SpawnedItemEntityId { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00008857 File Offset: 0x00006A57
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000885F File Offset: 0x00006A5F
		public short ConsumedAmount { get; private set; }

		// Token: 0x060004B4 RID: 1204 RVA: 0x00008868 File Offset: 0x00006A68
		public ConsumeWeaponAmount(MissionObjectId spawnedItemEntityId, short consumedAmount)
		{
			this.SpawnedItemEntityId = spawnedItemEntityId;
			this.ConsumedAmount = consumedAmount;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000887E File Offset: 0x00006A7E
		public ConsumeWeaponAmount()
		{
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00008886 File Offset: 0x00006A86
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.SpawnedItemEntityId);
			GameNetworkMessage.WriteIntToPacket((int)this.ConsumedAmount, CompressionBasic.ItemDataValueCompressionInfo);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000088A4 File Offset: 0x00006AA4
		protected override bool OnRead()
		{
			bool result = true;
			this.SpawnedItemEntityId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.ConsumedAmount = (short)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.ItemDataValueCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000088D4 File Offset: 0x00006AD4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.EquipmentDetailed;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000088D9 File Offset: 0x00006AD9
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Consumed ",
				this.ConsumedAmount,
				" from ",
				this.SpawnedItemEntityId
			});
		}
	}
}
