using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.UI.Actions
{
	// Token: 0x02000037 RID: 55
	[NullableContext(2)]
	internal interface IAction
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D5 RID: 469
		[Nullable(1)]
		IRef Context { [NullableContext(1)] get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001D6 RID: 470
		object Original { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001D7 RID: 471
		object Value { get; }

		// Token: 0x060001D8 RID: 472
		void DoAction();

		// Token: 0x060001D9 RID: 473
		void UndoAction();
	}
}
