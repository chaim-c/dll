using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000081 RID: 129
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreateAgentVisuals : GameNetworkMessage
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00008ED0 File Offset: 0x000070D0
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00008ED8 File Offset: 0x000070D8
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00008EE1 File Offset: 0x000070E1
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00008EE9 File Offset: 0x000070E9
		public int VisualsIndex { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00008EF2 File Offset: 0x000070F2
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00008EFA File Offset: 0x000070FA
		public BasicCharacterObject Character { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00008F03 File Offset: 0x00007103
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00008F0B File Offset: 0x0000710B
		public Equipment Equipment { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00008F14 File Offset: 0x00007114
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00008F1C File Offset: 0x0000711C
		public int BodyPropertiesSeed { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00008F25 File Offset: 0x00007125
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00008F2D File Offset: 0x0000712D
		public bool IsFemale { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00008F36 File Offset: 0x00007136
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00008F3E File Offset: 0x0000713E
		public int SelectedEquipmentSetIndex { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00008F47 File Offset: 0x00007147
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00008F4F File Offset: 0x0000714F
		public int TroopCountInFormation { get; private set; }

		// Token: 0x060004F2 RID: 1266 RVA: 0x00008F58 File Offset: 0x00007158
		public CreateAgentVisuals(NetworkCommunicator peer, AgentBuildData agentBuildData, int selectedEquipmentSetIndex, int troopCountInFormation = 0)
		{
			this.Peer = peer;
			this.VisualsIndex = agentBuildData.AgentVisualsIndex;
			this.Character = agentBuildData.AgentCharacter;
			this.BodyPropertiesSeed = agentBuildData.AgentEquipmentSeed;
			this.IsFemale = agentBuildData.AgentIsFemale;
			this.Equipment = new Equipment();
			this.Equipment.FillFrom(agentBuildData.AgentOverridenSpawnEquipment, true);
			this.SelectedEquipmentSetIndex = selectedEquipmentSetIndex;
			this.TroopCountInFormation = troopCountInFormation;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00008FCE File Offset: 0x000071CE
		public CreateAgentVisuals()
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00008FD8 File Offset: 0x000071D8
		protected override bool OnRead()
		{
			bool flag = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref flag, false);
			this.VisualsIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentOffsetCompressionInfo, ref flag);
			this.Character = (BasicCharacterObject)GameNetworkMessage.ReadObjectReferenceFromPacket(MBObjectManager.Instance, CompressionBasic.GUIDCompressionInfo, ref flag);
			this.Equipment = new Equipment();
			bool flag2 = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < (flag2 ? EquipmentIndex.NumEquipmentSetSlots : EquipmentIndex.ArmorItemEndSlot); equipmentIndex++)
			{
				EquipmentElement itemRosterElement = ModuleNetworkData.ReadItemReferenceFromPacket(MBObjectManager.Instance, ref flag);
				if (!flag)
				{
					break;
				}
				this.Equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, itemRosterElement);
			}
			this.BodyPropertiesSeed = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.RandomSeedCompressionInfo, ref flag);
			this.IsFemale = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			this.SelectedEquipmentSetIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref flag);
			this.TroopCountInFormation = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref flag);
			return flag;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000090AC File Offset: 0x000072AC
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteIntToPacket(this.VisualsIndex, CompressionMission.AgentOffsetCompressionInfo);
			GameNetworkMessage.WriteObjectReferenceToPacket(this.Character, CompressionBasic.GUIDCompressionInfo);
			bool flag = this.Equipment[EquipmentIndex.ArmorItemEndSlot].Item != null;
			GameNetworkMessage.WriteBoolToPacket(flag);
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < (flag ? EquipmentIndex.NumEquipmentSetSlots : EquipmentIndex.ArmorItemEndSlot); equipmentIndex++)
			{
				ModuleNetworkData.WriteItemReferenceToPacket(this.Equipment.GetEquipmentFromSlot(equipmentIndex));
			}
			GameNetworkMessage.WriteIntToPacket(this.BodyPropertiesSeed, CompressionBasic.RandomSeedCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.IsFemale);
			GameNetworkMessage.WriteIntToPacket(this.SelectedEquipmentSetIndex, CompressionBasic.MissionObjectIDCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.TroopCountInFormation, CompressionBasic.PlayerCompressionInfo);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00009163 File Offset: 0x00007363
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000916B File Offset: 0x0000736B
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Create AgentVisuals for peer: ",
				this.Peer.UserName,
				", and with Index: ",
				this.VisualsIndex
			});
		}
	}
}
