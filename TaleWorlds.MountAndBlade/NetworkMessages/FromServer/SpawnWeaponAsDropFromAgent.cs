using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C1 RID: 193
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SpawnWeaponAsDropFromAgent : GameNetworkMessage
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0000D462 File Offset: 0x0000B662
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0000D46A File Offset: 0x0000B66A
		public int AgentIndex { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0000D473 File Offset: 0x0000B673
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0000D47B File Offset: 0x0000B67B
		public EquipmentIndex EquipmentIndex { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0000D484 File Offset: 0x0000B684
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0000D48C File Offset: 0x0000B68C
		public Vec3 Velocity { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0000D495 File Offset: 0x0000B695
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0000D49D File Offset: 0x0000B69D
		public Vec3 AngularVelocity { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		public Mission.WeaponSpawnFlags WeaponSpawnFlags { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0000D4B7 File Offset: 0x0000B6B7
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0000D4BF File Offset: 0x0000B6BF
		public int ForcedIndex { get; private set; }

		// Token: 0x060007DC RID: 2012 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public SpawnWeaponAsDropFromAgent(int agentIndex, EquipmentIndex equipmentIndex, Vec3 velocity, Vec3 angularVelocity, Mission.WeaponSpawnFlags weaponSpawnFlags, int forcedIndex)
		{
			this.AgentIndex = agentIndex;
			this.EquipmentIndex = equipmentIndex;
			this.Velocity = velocity;
			this.AngularVelocity = angularVelocity;
			this.WeaponSpawnFlags = weaponSpawnFlags;
			this.ForcedIndex = forcedIndex;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0000D4FD File Offset: 0x0000B6FD
		public SpawnWeaponAsDropFromAgent()
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0000D508 File Offset: 0x0000B708
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.EquipmentIndex = (EquipmentIndex)GameNetworkMessage.ReadIntFromPacket(CompressionMission.ItemSlotCompressionInfo, ref result);
			this.WeaponSpawnFlags = (Mission.WeaponSpawnFlags)GameNetworkMessage.ReadUintFromPacket(CompressionMission.SpawnedItemWeaponSpawnFlagCompressionInfo, ref result);
			if (this.WeaponSpawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithPhysics))
			{
				this.Velocity = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.SpawnedItemVelocityCompressionInfo, ref result);
				this.AngularVelocity = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.SpawnedItemAngularVelocityCompressionInfo, ref result);
			}
			else
			{
				this.Velocity = Vec3.Zero;
				this.AngularVelocity = Vec3.Zero;
			}
			this.ForcedIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.EquipmentIndex, CompressionMission.ItemSlotCompressionInfo);
			GameNetworkMessage.WriteUintToPacket((uint)this.WeaponSpawnFlags, CompressionMission.SpawnedItemWeaponSpawnFlagCompressionInfo);
			if (this.WeaponSpawnFlags.HasAnyFlag(Mission.WeaponSpawnFlags.WithPhysics))
			{
				GameNetworkMessage.WriteVec3ToPacket(this.Velocity, CompressionMission.SpawnedItemVelocityCompressionInfo);
				GameNetworkMessage.WriteVec3ToPacket(this.AngularVelocity, CompressionMission.SpawnedItemAngularVelocityCompressionInfo);
			}
			GameNetworkMessage.WriteIntToPacket(this.ForcedIndex, CompressionBasic.MissionObjectIDCompressionInfo);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0000D61E File Offset: 0x0000B81E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0000D624 File Offset: 0x0000B824
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Spawn Weapon from agent with agent-index: ",
				this.AgentIndex,
				" from equipment index: ",
				this.EquipmentIndex,
				", and with ID: ",
				this.ForcedIndex
			});
		}
	}
}
