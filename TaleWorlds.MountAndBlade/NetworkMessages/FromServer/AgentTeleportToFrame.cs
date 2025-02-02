using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000074 RID: 116
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AgentTeleportToFrame : GameNetworkMessage
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00007AEF File Offset: 0x00005CEF
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00007AF7 File Offset: 0x00005CF7
		public int AgentIndex { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00007B00 File Offset: 0x00005D00
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00007B08 File Offset: 0x00005D08
		public Vec3 Position { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00007B11 File Offset: 0x00005D11
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00007B19 File Offset: 0x00005D19
		public Vec2 Direction { get; private set; }

		// Token: 0x0600042B RID: 1067 RVA: 0x00007B22 File Offset: 0x00005D22
		public AgentTeleportToFrame(int agentIndex, Vec3 position, Vec2 direction)
		{
			this.AgentIndex = agentIndex;
			this.Position = position;
			this.Direction = direction.Normalized();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00007B45 File Offset: 0x00005D45
		public AgentTeleportToFrame()
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00007B50 File Offset: 0x00005D50
		protected override bool OnRead()
		{
			bool result = true;
			this.AgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Position = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			this.Direction = GameNetworkMessage.ReadVec2FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00007B91 File Offset: 0x00005D91
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AgentIndex);
			GameNetworkMessage.WriteVec3ToPacket(this.Position, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec2ToPacket(this.Direction, CompressionBasic.UnitVectorCompressionInfo);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00007BBE File Offset: 0x00005DBE
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00007BC8 File Offset: 0x00005DC8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Teleporting agent with agent-index: ",
				this.AgentIndex,
				" to frame with position: ",
				this.Position,
				" and direction: ",
				this.Direction
			});
		}
	}
}
