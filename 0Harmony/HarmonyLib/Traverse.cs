using System;

namespace HarmonyLib
{
	// Token: 0x02000053 RID: 83
	public class Traverse<T>
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00013B88 File Offset: 0x00011D88
		private Traverse()
		{
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00013B90 File Offset: 0x00011D90
		public Traverse(Traverse traverse)
		{
			this.traverse = traverse;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00013B9F File Offset: 0x00011D9F
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00013BAC File Offset: 0x00011DAC
		public T Value
		{
			get
			{
				return this.traverse.GetValue<T>();
			}
			set
			{
				this.traverse.SetValue(value);
			}
		}

		// Token: 0x040000EC RID: 236
		private readonly Traverse traverse;
	}
}
