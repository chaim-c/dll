using System;

namespace TaleWorlds.Diamond.HelloWorld
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class HelloWorldTestFunctionResult : FunctionResult
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00005DA9 File Offset: 0x00003FA9
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00005DB1 File Offset: 0x00003FB1
		public string Message { get; private set; }

		// Token: 0x06000219 RID: 537 RVA: 0x00005DBA File Offset: 0x00003FBA
		public HelloWorldTestFunctionResult()
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00005DC2 File Offset: 0x00003FC2
		public HelloWorldTestFunctionResult(string message)
		{
			this.Message = message;
		}
	}
}
