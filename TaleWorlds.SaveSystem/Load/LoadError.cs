using System;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x02000039 RID: 57
	public class LoadError
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009CC5 File Offset: 0x00007EC5
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00009CCD File Offset: 0x00007ECD
		public string Message { get; private set; }

		// Token: 0x06000208 RID: 520 RVA: 0x00009CD6 File Offset: 0x00007ED6
		internal LoadError(string message)
		{
			this.Message = message;
		}
	}
}
