using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000070 RID: 112
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AddPrefabComponentToAgentBone : GameNetworkMessage
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000767B File Offset: 0x0000587B
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00007683 File Offset: 0x00005883
		public int AgentIndex { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000768C File Offset: 0x0000588C
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00007694 File Offset: 0x00005894
		public string PrefabName { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000769D File Offset: 0x0000589D
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x000076A5 File Offset: 0x000058A5
		public sbyte BoneIndex { get; private set; }

		// Token: 0x060003F7 RID: 1015 RVA: 0x000076AE File Offset: 0x000058AE
		public AddPrefabComponentToAgentBone(int agentIndex, string prefabName, sbyte boneIndex)
		{
			this.AgentIndex = agentIndex;
			this.PrefabName = prefabName;
			this.BoneIndex = boneIndex;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000076CB File Offset: 0x000058CB
		public AddPrefabComponentToAgentBone()
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000076D4 File Offset: 0x000058D4
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.PrefabName = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.BoneIndex = (sbyte)GameNetworkMessage.ReadIntFromPacket(CompressionMission.BoneIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00007711 File Offset: 0x00005911
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteStringToPacket(this.PrefabName);
			GameNetworkMessage.WriteIntToPacket((int)this.BoneIndex, CompressionMission.BoneIndexCompressionInfo);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00007739 File Offset: 0x00005939
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00007744 File Offset: 0x00005944
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Add prefab component: ",
				this.PrefabName,
				" on bone with index: ",
				this.BoneIndex,
				" on agent with agent-index: ",
				this.AgentIndex
			});
		}
	}
}
