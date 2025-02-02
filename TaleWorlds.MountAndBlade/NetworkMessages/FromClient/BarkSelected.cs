using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000028 RID: 40
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class BarkSelected : GameNetworkMessage
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000039DB File Offset: 0x00001BDB
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000039E3 File Offset: 0x00001BE3
		public int IndexOfBark { get; private set; }

		// Token: 0x0600014A RID: 330 RVA: 0x000039EC File Offset: 0x00001BEC
		public BarkSelected(int indexOfBark)
		{
			this.IndexOfBark = indexOfBark;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000039FB File Offset: 0x00001BFB
		public BarkSelected()
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003A04 File Offset: 0x00001C04
		protected override bool OnRead()
		{
			bool result = true;
			this.IndexOfBark = GameNetworkMessage.ReadIntFromPacket(CompressionMission.BarkIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003A26 File Offset: 0x00001C26
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.IndexOfBark, CompressionMission.BarkIndexCompressionInfo);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003A38 File Offset: 0x00001C38
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.None;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003A3C File Offset: 0x00001C3C
		protected override string OnGetLogFormat()
		{
			return "FromClient.BarkSelected: " + this.IndexOfBark;
		}
	}
}
