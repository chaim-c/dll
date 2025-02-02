using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000CD RID: 205
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class UseObject : GameNetworkMessage
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0000E313 File Offset: 0x0000C513
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0000E31B File Offset: 0x0000C51B
		public int AgentIndex { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0000E324 File Offset: 0x0000C524
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0000E32C File Offset: 0x0000C52C
		public MissionObjectId UsableGameObjectId { get; private set; }

		// Token: 0x0600086E RID: 2158 RVA: 0x0000E335 File Offset: 0x0000C535
		public UseObject(int agentIndex, MissionObjectId usableGameObjectId)
		{
			this.AgentIndex = agentIndex;
			this.UsableGameObjectId = usableGameObjectId;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000E34B File Offset: 0x0000C54B
		public UseObject()
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0000E354 File Offset: 0x0000C554
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.UsableGameObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0000E37E File Offset: 0x0000C57E
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteMissionObjectIdToPacket((this.UsableGameObjectId.Id >= 0) ? this.UsableGameObjectId : MissionObjectId.Invalid);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0000E3AB File Offset: 0x0000C5AB
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents | MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		protected override string OnGetLogFormat()
		{
			string text = "Use UsableMissionObject with ID: ";
			if (this.UsableGameObjectId != MissionObjectId.Invalid)
			{
				text += this.UsableGameObjectId;
			}
			else
			{
				text += "null";
			}
			text += " by Agent with name: ";
			if (this.AgentIndex >= 0)
			{
				text = text + "agent-index: " + this.AgentIndex;
			}
			else
			{
				text += "null";
			}
			return text;
		}
	}
}
