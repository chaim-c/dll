using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000608 RID: 1544
	public struct RegisterEventParamDef
	{
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x0003B2DC File Offset: 0x000394DC
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x0003B2E4 File Offset: 0x000394E4
		public Utf8String ParamName { get; set; }

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x0003B2ED File Offset: 0x000394ED
		// (set) Token: 0x060027B3 RID: 10163 RVA: 0x0003B2F5 File Offset: 0x000394F5
		public AntiCheatCommonEventParamType ParamType { get; set; }

		// Token: 0x060027B4 RID: 10164 RVA: 0x0003B2FE File Offset: 0x000394FE
		internal void Set(ref RegisterEventParamDefInternal other)
		{
			this.ParamName = other.ParamName;
			this.ParamType = other.ParamType;
		}
	}
}
