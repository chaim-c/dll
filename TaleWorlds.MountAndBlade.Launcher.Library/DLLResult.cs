using System;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000013 RID: 19
	public class DLLResult
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000438A File Offset: 0x0000258A
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004392 File Offset: 0x00002592
		public string DLLName { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000439B File Offset: 0x0000259B
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000043A3 File Offset: 0x000025A3
		public bool IsSafe { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000043AC File Offset: 0x000025AC
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000043B4 File Offset: 0x000025B4
		public string Information { get; set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x000043BD File Offset: 0x000025BD
		public DLLResult(string dLLName, bool isSafe, string information)
		{
			this.DLLName = dLLName;
			this.IsSafe = isSafe;
			this.Information = information;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000043DA File Offset: 0x000025DA
		public DLLResult()
		{
			this.DLLName = "";
			this.IsSafe = false;
			this.Information = "";
		}
	}
}
