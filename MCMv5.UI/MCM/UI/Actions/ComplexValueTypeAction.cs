using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.UI.Actions
{
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ComplexValueTypeAction<[Nullable(0)] T> : IAction where T : struct
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000090B1 File Offset: 0x000072B1
		public IRef Context { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000090B9 File Offset: 0x000072B9
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				return this.DoFunction(this.Context.Value as T?);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000090E0 File Offset: 0x000072E0
		[Nullable(2)]
		public object Original
		{
			[NullableContext(2)]
			get
			{
				return this.UndoFunction(this.Context.Value as T?);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00009107 File Offset: 0x00007307
		[Nullable(new byte[]
		{
			1,
			0,
			0
		})]
		private Func<T?, T?> DoFunction { [return: Nullable(new byte[]
		{
			1,
			0,
			0
		})] get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000910F File Offset: 0x0000730F
		[Nullable(new byte[]
		{
			1,
			0,
			0
		})]
		private Func<T?, T?> UndoFunction { [return: Nullable(new byte[]
		{
			1,
			0,
			0
		})] get; }

		// Token: 0x060001D1 RID: 465 RVA: 0x00009118 File Offset: 0x00007318
		[Obsolete("Use the nullable overload instead!", true)]
		public ComplexValueTypeAction(IRef context, [Nullable(new byte[]
		{
			1,
			0,
			0
		})] Func<T, T> doFunction, [Nullable(new byte[]
		{
			1,
			0,
			0
		})] Func<T, T> undoFunction)
		{
			this.Context = context;
			this.DoFunction = ((T? obj) => obj);
			this.UndoFunction = ((T? obj) => obj);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000917E File Offset: 0x0000737E
		public ComplexValueTypeAction(IRef context, [Nullable(new byte[]
		{
			1,
			0,
			0
		})] Func<T?, T?> doFunction, [Nullable(new byte[]
		{
			1,
			0,
			0
		})] Func<T?, T?> undoFunction)
		{
			this.Context = context;
			this.DoFunction = doFunction;
			this.UndoFunction = undoFunction;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000919D File Offset: 0x0000739D
		public void DoAction()
		{
			this.Context.Value = this.Value;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000091B1 File Offset: 0x000073B1
		public void UndoAction()
		{
			this.Context.Value = this.Original;
		}
	}
}
