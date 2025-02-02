using System;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000016 RID: 22
	[NullableContext(2)]
	[Nullable(0)]
	internal class Traverse2<T>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005DE5 File Offset: 0x00003FE5
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005DF2 File Offset: 0x00003FF2
		public T Value
		{
			get
			{
				return this._traverse.GetValue<T>();
			}
			set
			{
				this._traverse.SetValue(value);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005E06 File Offset: 0x00004006
		private Traverse2()
		{
			this._traverse = new Traverse2(null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005E1C File Offset: 0x0000401C
		[NullableContext(1)]
		public Traverse2(Traverse2 traverse)
		{
			this._traverse = traverse;
		}

		// Token: 0x04000013 RID: 19
		[Nullable(1)]
		private readonly Traverse2 _traverse;
	}
}
