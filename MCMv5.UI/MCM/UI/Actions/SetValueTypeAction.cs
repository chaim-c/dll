using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.UI.Actions
{
	// Token: 0x0200003A RID: 58
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class SetValueTypeAction<[Nullable(0)] T> : IAction where T : struct
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009314 File Offset: 0x00007514
		[Nullable(1)]
		public IRef Context { [NullableContext(1)] get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000931C File Offset: 0x0000751C
		public object Value { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009324 File Offset: 0x00007524
		public object Original { get; }

		// Token: 0x060001E9 RID: 489 RVA: 0x0000932C File Offset: 0x0000752C
		[NullableContext(0)]
		public SetValueTypeAction([Nullable(1)] IRef context, T value)
		{
			this.Context = context;
			this.Value = value;
			this.Original = this.Context.Value;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000935A File Offset: 0x0000755A
		public void DoAction()
		{
			this.Context.Value = this.Value;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000936E File Offset: 0x0000756E
		public void UndoAction()
		{
			this.Context.Value = this.Original;
		}
	}
}
