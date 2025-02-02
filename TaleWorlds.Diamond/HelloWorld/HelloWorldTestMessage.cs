using System;

namespace TaleWorlds.Diamond.HelloWorld
{
	// Token: 0x02000059 RID: 89
	[Serializable]
	public class HelloWorldTestMessage : Message
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005D59 File Offset: 0x00003F59
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00005D61 File Offset: 0x00003F61
		public string Message { get; private set; }

		// Token: 0x06000211 RID: 529 RVA: 0x00005D6A File Offset: 0x00003F6A
		public HelloWorldTestMessage()
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005D72 File Offset: 0x00003F72
		public HelloWorldTestMessage(string message)
		{
			this.Message = message;
		}
	}
}
