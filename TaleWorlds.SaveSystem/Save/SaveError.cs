using System;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x0200002E RID: 46
	public class SaveError
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000082BC File Offset: 0x000064BC
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000082C4 File Offset: 0x000064C4
		public string Message { get; private set; }

		// Token: 0x060001AA RID: 426 RVA: 0x000082CD File Offset: 0x000064CD
		internal SaveError(string message)
		{
			this.Message = message;
		}
	}
}
