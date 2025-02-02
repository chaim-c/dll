using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000052 RID: 82
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class GoldGain : GameNetworkMessage
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00005717 File Offset: 0x00003917
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000571F File Offset: 0x0000391F
		public List<KeyValuePair<ushort, int>> GoldChangeEventList { get; private set; }

		// Token: 0x060002D0 RID: 720 RVA: 0x00005728 File Offset: 0x00003928
		public GoldGain(List<KeyValuePair<ushort, int>> goldChangeEventList)
		{
			this.GoldChangeEventList = goldChangeEventList;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00005737 File Offset: 0x00003937
		public GoldGain()
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00005740 File Offset: 0x00003940
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.GoldChangeEventList.Count - 1, CompressionMission.TdmGoldGainTypeCompressionInfo);
			foreach (KeyValuePair<ushort, int> keyValuePair in this.GoldChangeEventList)
			{
				GameNetworkMessage.WriteIntToPacket((int)keyValuePair.Key, CompressionMission.TdmGoldGainTypeCompressionInfo);
				GameNetworkMessage.WriteIntToPacket(keyValuePair.Value, CompressionMission.TdmGoldChangeCompressionInfo);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000057C8 File Offset: 0x000039C8
		protected override bool OnRead()
		{
			bool result = true;
			this.GoldChangeEventList = new List<KeyValuePair<ushort, int>>();
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionMission.TdmGoldGainTypeCompressionInfo, ref result) + 1;
			for (int i = 0; i < num; i++)
			{
				ushort key = (ushort)GameNetworkMessage.ReadIntFromPacket(CompressionMission.TdmGoldGainTypeCompressionInfo, ref result);
				int value = GameNetworkMessage.ReadIntFromPacket(CompressionMission.TdmGoldChangeCompressionInfo, ref result);
				this.GoldChangeEventList.Add(new KeyValuePair<ushort, int>(key, value));
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000582D File Offset: 0x00003A2D
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00005835 File Offset: 0x00003A35
		protected override string OnGetLogFormat()
		{
			return "Gold change events synced.";
		}
	}
}
