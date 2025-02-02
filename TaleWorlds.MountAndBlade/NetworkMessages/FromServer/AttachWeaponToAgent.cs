using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000076 RID: 118
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AttachWeaponToAgent : GameNetworkMessage
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00007D1F File Offset: 0x00005F1F
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00007D27 File Offset: 0x00005F27
		public MissionWeapon Weapon { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00007D30 File Offset: 0x00005F30
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00007D38 File Offset: 0x00005F38
		public int AgentIndex { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00007D41 File Offset: 0x00005F41
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00007D49 File Offset: 0x00005F49
		public sbyte BoneIndex { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00007D52 File Offset: 0x00005F52
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00007D5A File Offset: 0x00005F5A
		public MatrixFrame AttachLocalFrame { get; private set; }

		// Token: 0x06000443 RID: 1091 RVA: 0x00007D63 File Offset: 0x00005F63
		public AttachWeaponToAgent(MissionWeapon weapon, int agentIndex, sbyte boneIndex, MatrixFrame attachLocalFrame)
		{
			this.Weapon = weapon;
			this.AgentIndex = agentIndex;
			this.BoneIndex = boneIndex;
			this.AttachLocalFrame = attachLocalFrame;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00007D88 File Offset: 0x00005F88
		public AttachWeaponToAgent()
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00007D90 File Offset: 0x00005F90
		protected override void OnWrite()
		{
			ModuleNetworkData.WriteWeaponReferenceToPacket(this.Weapon);
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.BoneIndex, CompressionMission.BoneIndexCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.AttachLocalFrame.origin, CompressionBasic.LocalPositionCompressionInfo);
			GameNetworkMessage.WriteRotationMatrixToPacket(this.AttachLocalFrame.rotation);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00007DE8 File Offset: 0x00005FE8
		protected override bool OnRead()
		{
			bool flag = true;
			this.Weapon = ModuleNetworkData.ReadWeaponReferenceFromPacket(MBObjectManager.Instance, ref flag);
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref flag);
			this.BoneIndex = (sbyte)GameNetworkMessage.ReadIntFromPacket(CompressionMission.BoneIndexCompressionInfo, ref flag);
			Vec3 o = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.LocalPositionCompressionInfo, ref flag);
			Mat3 rot = GameNetworkMessage.ReadRotationMatrixFromPacket(ref flag);
			if (flag)
			{
				this.AttachLocalFrame = new MatrixFrame(rot, o);
			}
			return flag;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00007E4F File Offset: 0x0000604F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Items | MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00007E58 File Offset: 0x00006058
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"AttachWeaponToAgent with name: ",
				(!this.Weapon.IsEmpty) ? this.Weapon.Item.Name : TextObject.Empty,
				" to bone index: ",
				this.BoneIndex,
				" on agent agent-index: ",
				this.AgentIndex
			});
		}
	}
}
