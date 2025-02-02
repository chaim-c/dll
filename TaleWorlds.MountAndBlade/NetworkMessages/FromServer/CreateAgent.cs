using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000080 RID: 128
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CreateAgent : GameNetworkMessage
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00008912 File Offset: 0x00006B12
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0000891A File Offset: 0x00006B1A
		public int AgentIndex { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00008923 File Offset: 0x00006B23
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0000892B File Offset: 0x00006B2B
		public int MountAgentIndex { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00008934 File Offset: 0x00006B34
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000893C File Offset: 0x00006B3C
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00008945 File Offset: 0x00006B45
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000894D File Offset: 0x00006B4D
		public BasicCharacterObject Character { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00008956 File Offset: 0x00006B56
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000895E File Offset: 0x00006B5E
		public Monster Monster { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00008967 File Offset: 0x00006B67
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0000896F File Offset: 0x00006B6F
		public MissionEquipment MissionEquipment { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00008978 File Offset: 0x00006B78
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00008980 File Offset: 0x00006B80
		public Equipment SpawnEquipment { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00008989 File Offset: 0x00006B89
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00008991 File Offset: 0x00006B91
		public BodyProperties BodyPropertiesValue { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000899A File Offset: 0x00006B9A
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x000089A2 File Offset: 0x00006BA2
		public int BodyPropertiesSeed { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000089AB File Offset: 0x00006BAB
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x000089B3 File Offset: 0x00006BB3
		public bool IsFemale { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000089BC File Offset: 0x00006BBC
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x000089C4 File Offset: 0x00006BC4
		public int TeamIndex { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000089CD File Offset: 0x00006BCD
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x000089D5 File Offset: 0x00006BD5
		public Vec3 Position { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000089DE File Offset: 0x00006BDE
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x000089E6 File Offset: 0x00006BE6
		public Vec2 Direction { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000089EF File Offset: 0x00006BEF
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x000089F7 File Offset: 0x00006BF7
		public int FormationIndex { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00008A00 File Offset: 0x00006C00
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00008A08 File Offset: 0x00006C08
		public bool IsPlayerAgent { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00008A11 File Offset: 0x00006C11
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00008A19 File Offset: 0x00006C19
		public uint ClothingColor1 { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00008A22 File Offset: 0x00006C22
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00008A2A File Offset: 0x00006C2A
		public uint ClothingColor2 { get; private set; }

		// Token: 0x060004DC RID: 1244 RVA: 0x00008A34 File Offset: 0x00006C34
		public CreateAgent(int agentIndex, BasicCharacterObject character, Monster monster, Equipment spawnEquipment, MissionEquipment missionEquipment, BodyProperties bodyPropertiesValue, int bodyPropertiesSeed, bool isFemale, int agentTeamIndex, int agentFormationIndex, uint clothingColor1, uint clothingColor2, int mountAgentIndex, Equipment mountAgentSpawnEquipment, bool isPlayerAgent, Vec3 position, Vec2 direction, NetworkCommunicator peer)
		{
			this.AgentIndex = agentIndex;
			this.MountAgentIndex = mountAgentIndex;
			this.Peer = peer;
			this.Character = character;
			this.Monster = monster;
			this.SpawnEquipment = new Equipment();
			this.MissionEquipment = new MissionEquipment();
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				this.MissionEquipment[equipmentIndex] = missionEquipment[equipmentIndex];
			}
			for (EquipmentIndex equipmentIndex2 = EquipmentIndex.NumAllWeaponSlots; equipmentIndex2 < EquipmentIndex.ArmorItemEndSlot; equipmentIndex2++)
			{
				this.SpawnEquipment[equipmentIndex2] = spawnEquipment.GetEquipmentFromSlot(equipmentIndex2);
			}
			if (this.MountAgentIndex >= 0)
			{
				this.SpawnEquipment[EquipmentIndex.ArmorItemEndSlot] = mountAgentSpawnEquipment[EquipmentIndex.ArmorItemEndSlot];
				this.SpawnEquipment[EquipmentIndex.HorseHarness] = mountAgentSpawnEquipment[EquipmentIndex.HorseHarness];
			}
			else
			{
				this.SpawnEquipment[EquipmentIndex.ArmorItemEndSlot] = default(EquipmentElement);
				this.SpawnEquipment[EquipmentIndex.HorseHarness] = default(EquipmentElement);
			}
			this.BodyPropertiesValue = bodyPropertiesValue;
			this.BodyPropertiesSeed = bodyPropertiesSeed;
			this.IsFemale = isFemale;
			this.TeamIndex = agentTeamIndex;
			this.Position = position;
			this.Direction = direction;
			this.FormationIndex = agentFormationIndex;
			this.ClothingColor1 = clothingColor1;
			this.ClothingColor2 = clothingColor2;
			this.IsPlayerAgent = isPlayerAgent;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00008B76 File Offset: 0x00006D76
		public CreateAgent()
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00008B80 File Offset: 0x00006D80
		protected override bool OnRead()
		{
			bool result = true;
			this.Character = (BasicCharacterObject)GameNetworkMessage.ReadObjectReferenceFromPacket(MBObjectManager.Instance, CompressionBasic.GUIDCompressionInfo, ref result);
			this.Monster = (Monster)GameNetworkMessage.ReadObjectReferenceFromPacket(MBObjectManager.Instance, CompressionBasic.GUIDCompressionInfo, ref result);
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.MountAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.SpawnEquipment = new Equipment();
			this.MissionEquipment = new MissionEquipment();
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				this.MissionEquipment[equipmentIndex] = ModuleNetworkData.ReadWeaponReferenceFromPacket(MBObjectManager.Instance, ref result);
			}
			for (EquipmentIndex equipmentIndex2 = EquipmentIndex.NumAllWeaponSlots; equipmentIndex2 < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex2++)
			{
				this.SpawnEquipment.AddEquipmentToSlotWithoutAgent(equipmentIndex2, ModuleNetworkData.ReadItemReferenceFromPacket(MBObjectManager.Instance, ref result));
			}
			this.IsPlayerAgent = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.BodyPropertiesSeed = ((!this.IsPlayerAgent) ? GameNetworkMessage.ReadIntFromPacket(CompressionBasic.RandomSeedCompressionInfo, ref result) : 0);
			this.BodyPropertiesValue = GameNetworkMessage.ReadBodyPropertiesFromPacket(ref result);
			this.IsFemale = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.TeamIndex = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			this.Position = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			this.Direction = GameNetworkMessage.ReadVec2FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref result).Normalized();
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			this.ClothingColor1 = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref result);
			this.ClothingColor2 = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00008D04 File Offset: 0x00006F04
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteObjectReferenceToPacket(this.Character, CompressionBasic.GUIDCompressionInfo);
			GameNetworkMessage.WriteObjectReferenceToPacket(this.Monster, CompressionBasic.GUIDCompressionInfo);
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteAgentIndexToPacket(this.MountAgentIndex);
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				ModuleNetworkData.WriteWeaponReferenceToPacket(this.MissionEquipment[equipmentIndex]);
			}
			for (EquipmentIndex equipmentIndex2 = EquipmentIndex.NumAllWeaponSlots; equipmentIndex2 < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex2++)
			{
				ModuleNetworkData.WriteItemReferenceToPacket(this.SpawnEquipment.GetEquipmentFromSlot(equipmentIndex2));
			}
			GameNetworkMessage.WriteBoolToPacket(this.IsPlayerAgent);
			if (!this.IsPlayerAgent)
			{
				GameNetworkMessage.WriteIntToPacket(this.BodyPropertiesSeed, CompressionBasic.RandomSeedCompressionInfo);
			}
			GameNetworkMessage.WriteBodyPropertiesToPacket(this.BodyPropertiesValue);
			GameNetworkMessage.WriteBoolToPacket(this.IsFemale);
			GameNetworkMessage.WriteTeamIndexToPacket(this.TeamIndex);
			GameNetworkMessage.WriteVec3ToPacket(this.Position, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec2ToPacket(this.Direction, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
			GameNetworkMessage.WriteUintToPacket(this.ClothingColor1, CompressionBasic.ColorCompressionInfo);
			GameNetworkMessage.WriteUintToPacket(this.ClothingColor2, CompressionBasic.ColorCompressionInfo);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00008E21 File Offset: 0x00007021
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00008E2C File Offset: 0x0000702C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Create an agent with index: ",
				this.AgentIndex,
				(this.Peer != null) ? string.Concat(new object[]
				{
					", belonging to peer with Name: ",
					this.Peer.UserName,
					", and peer-index: ",
					this.Peer.Index
				}) : "",
				(this.MountAgentIndex == -1) ? "" : (", owning a mount with index: " + this.MountAgentIndex)
			});
		}
	}
}
