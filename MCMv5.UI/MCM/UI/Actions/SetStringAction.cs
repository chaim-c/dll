using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.UI.Actions
{
	// Token: 0x02000039 RID: 57
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class SetStringAction : IAction
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000092AB File Offset: 0x000074AB
		[Nullable(1)]
		public IRef Context { [NullableContext(1)] get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000092B3 File Offset: 0x000074B3
		public object Value { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000092BB File Offset: 0x000074BB
		public object Original { get; }

		// Token: 0x060001E3 RID: 483 RVA: 0x000092C3 File Offset: 0x000074C3
		[NullableContext(1)]
		public SetStringAction(IRef context, string value)
		{
			this.Context = context;
			this.Value = value;
			this.Original = this.Context.Value;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000092EC File Offset: 0x000074EC
		public void DoAction()
		{
			this.Context.Value = this.Value;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009300 File Offset: 0x00007500
		public void UndoAction()
		{
			this.Context.Value = this.Original;
		}
	}
}
