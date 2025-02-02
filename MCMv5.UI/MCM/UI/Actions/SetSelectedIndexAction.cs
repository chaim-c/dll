using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.UI.Actions
{
	// Token: 0x02000038 RID: 56
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class SetSelectedIndexAction : IAction
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000091C5 File Offset: 0x000073C5
		[Nullable(1)]
		public IRef Context { [NullableContext(1)] get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000091CD File Offset: 0x000073CD
		public object Value { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000091D5 File Offset: 0x000073D5
		public object Original { get; }

		// Token: 0x060001DD RID: 477 RVA: 0x000091E0 File Offset: 0x000073E0
		[NullableContext(1)]
		public SetSelectedIndexAction(IRef context, object value)
		{
			this.Context = context;
			this.Value = new SelectedIndexWrapper(value).SelectedIndex;
			this.Original = new SelectedIndexWrapper(context.Value).SelectedIndex;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009234 File Offset: 0x00007434
		public void DoAction()
		{
			SelectedIndexWrapper selectedIndexWrapper = new SelectedIndexWrapper(this.Context.Value);
			selectedIndexWrapper.SelectedIndex = ((int?)this.Value).GetValueOrDefault(-1);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009270 File Offset: 0x00007470
		public void UndoAction()
		{
			SelectedIndexWrapper selectedIndexWrapper = new SelectedIndexWrapper(this.Context.Value);
			selectedIndexWrapper.SelectedIndex = ((int?)this.Original).GetValueOrDefault(-1);
		}
	}
}
