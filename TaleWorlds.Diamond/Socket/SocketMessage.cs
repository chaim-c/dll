using System;
using TaleWorlds.Library;
using TaleWorlds.Network;

namespace TaleWorlds.Diamond.Socket
{
	// Token: 0x02000036 RID: 54
	[MessageId(1)]
	public class SocketMessage : MessageContract
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003DDA File Offset: 0x00001FDA
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00003DE2 File Offset: 0x00001FE2
		public Message Message { get; private set; }

		// Token: 0x0600011B RID: 283 RVA: 0x00003DEB File Offset: 0x00001FEB
		public SocketMessage()
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003DF3 File Offset: 0x00001FF3
		public SocketMessage(Message message)
		{
			this.Message = message;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003E04 File Offset: 0x00002004
		public override void SerializeToNetworkMessage(INetworkMessageWriter networkMessage)
		{
			byte[] array = Common.SerializeObject(this.Message);
			networkMessage.Write(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				networkMessage.Write(array[i]);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003E40 File Offset: 0x00002040
		public override void DeserializeFromNetworkMessage(INetworkMessageReader networkMessage)
		{
			byte[] array = new byte[networkMessage.ReadInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = networkMessage.ReadByte();
			}
			this.Message = (Message)Common.DeserializeObject(array);
		}
	}
}
