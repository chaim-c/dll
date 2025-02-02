using System;

namespace TaleWorlds.Diamond.HelloWorld
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	public class HelloWorldTestFunctionMessage : Message
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005D81 File Offset: 0x00003F81
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00005D89 File Offset: 0x00003F89
		public string Message { get; private set; }

		// Token: 0x06000215 RID: 533 RVA: 0x00005D92 File Offset: 0x00003F92
		public HelloWorldTestFunctionMessage()
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005D9A File Offset: 0x00003F9A
		public HelloWorldTestFunctionMessage(string message)
		{
			this.Message = message;
		}
	}
}
