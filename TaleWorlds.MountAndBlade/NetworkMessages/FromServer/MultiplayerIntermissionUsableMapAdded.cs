using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200005D RID: 93
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerIntermissionUsableMapAdded : GameNetworkMessage
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000062EF File Offset: 0x000044EF
		// (set) Token: 0x0600034B RID: 843 RVA: 0x000062F7 File Offset: 0x000044F7
		public string MapId { get; private set; }

		// Token: 0x0600034C RID: 844 RVA: 0x00006300 File Offset: 0x00004500
		public MultiplayerIntermissionUsableMapAdded()
		{
			this.CompatibleGameTypes = new List<string>();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00006313 File Offset: 0x00004513
		public MultiplayerIntermissionUsableMapAdded(string mapId, bool isCompatibleWithAllGameTypes, int compatibleGameTypeCount, List<string> compatibleGameTypes)
		{
			this.MapId = mapId;
			this.IsCompatibleWithAllGameTypes = isCompatibleWithAllGameTypes;
			this.CompatibleGameTypeCount = compatibleGameTypeCount;
			this.CompatibleGameTypes = compatibleGameTypes;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00006338 File Offset: 0x00004538
		protected override bool OnRead()
		{
			bool result = true;
			this.MapId = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.IsCompatibleWithAllGameTypes = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.CompatibleGameTypeCount = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.IntermissionMapVoteItemCountCompressionInfo, ref result);
			for (int i = 0; i < this.CompatibleGameTypeCount; i++)
			{
				this.CompatibleGameTypes.Add(GameNetworkMessage.ReadStringFromPacket(ref result));
			}
			return result;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00006398 File Offset: 0x00004598
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.MapId);
			GameNetworkMessage.WriteBoolToPacket(this.IsCompatibleWithAllGameTypes);
			GameNetworkMessage.WriteIntToPacket(this.CompatibleGameTypeCount, CompressionBasic.IntermissionMapVoteItemCountCompressionInfo);
			for (int i = 0; i < this.CompatibleGameTypeCount; i++)
			{
				GameNetworkMessage.WriteStringToPacket(this.CompatibleGameTypes[i]);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000063ED File Offset: 0x000045ED
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000063F5 File Offset: 0x000045F5
		protected override string OnGetLogFormat()
		{
			return "Adding usable map with id: " + this.MapId + ".";
		}

		// Token: 0x0400009F RID: 159
		public bool IsCompatibleWithAllGameTypes;

		// Token: 0x040000A0 RID: 160
		public int CompatibleGameTypeCount;

		// Token: 0x040000A1 RID: 161
		public List<string> CompatibleGameTypes;
	}
}
