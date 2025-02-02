using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000609 RID: 1545
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterEventParamDefInternal : IGettable<RegisterEventParamDef>, ISettable<RegisterEventParamDef>, IDisposable
	{
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x0003B31C File Offset: 0x0003951C
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x0003B33D File Offset: 0x0003953D
		public Utf8String ParamName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ParamName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ParamName);
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x0003B350 File Offset: 0x00039550
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x0003B368 File Offset: 0x00039568
		public AntiCheatCommonEventParamType ParamType
		{
			get
			{
				return this.m_ParamType;
			}
			set
			{
				this.m_ParamType = value;
			}
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0003B372 File Offset: 0x00039572
		public void Set(ref RegisterEventParamDef other)
		{
			this.ParamName = other.ParamName;
			this.ParamType = other.ParamType;
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0003B390 File Offset: 0x00039590
		public void Set(ref RegisterEventParamDef? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ParamName = other.Value.ParamName;
				this.ParamType = other.Value.ParamType;
			}
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0003B3D4 File Offset: 0x000395D4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ParamName);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0003B3E3 File Offset: 0x000395E3
		public void Get(out RegisterEventParamDef output)
		{
			output = default(RegisterEventParamDef);
			output.Set(ref this);
		}

		// Token: 0x040011DA RID: 4570
		private IntPtr m_ParamName;

		// Token: 0x040011DB RID: 4571
		private AntiCheatCommonEventParamType m_ParamType;
	}
}
