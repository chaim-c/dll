using System;
using System.Runtime.CompilerServices;

namespace HarmonyLib.BUTR.Extensions
{
	// Token: 0x02000069 RID: 105
	[NullableContext(2)]
	[Nullable(0)]
	internal class Traverse2<T>
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00012E21 File Offset: 0x00011021
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00012E2E File Offset: 0x0001102E
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

		// Token: 0x06000465 RID: 1125 RVA: 0x00012E42 File Offset: 0x00011042
		private Traverse2()
		{
			this._traverse = new Traverse2(null);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00012E58 File Offset: 0x00011058
		[NullableContext(1)]
		public Traverse2(Traverse2 traverse)
		{
			this._traverse = traverse;
		}

		// Token: 0x0400015C RID: 348
		[Nullable(1)]
		private readonly Traverse2 _traverse;
	}
}
