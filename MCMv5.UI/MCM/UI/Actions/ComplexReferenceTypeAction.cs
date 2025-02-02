using System;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.UI.Actions
{
	// Token: 0x02000035 RID: 53
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ComplexReferenceTypeAction<T> : IAction where T : class
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008FD3 File Offset: 0x000071D3
		public IRef Context { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008FDC File Offset: 0x000071DC
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				this.DoFunction(this.Context.Value as T);
				return this.Context.Value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000901C File Offset: 0x0000721C
		[Nullable(2)]
		public object Original
		{
			[NullableContext(2)]
			get
			{
				this.UndoFunction(this.Context.Value as T);
				return this.Context.Value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000905A File Offset: 0x0000725A
		[Nullable(new byte[]
		{
			1,
			2
		})]
		private Action<T> DoFunction { [return: Nullable(new byte[]
		{
			1,
			2
		})] get; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009062 File Offset: 0x00007262
		[Nullable(new byte[]
		{
			1,
			2
		})]
		private Action<T> UndoFunction { [return: Nullable(new byte[]
		{
			1,
			2
		})] get; }

		// Token: 0x060001C9 RID: 457 RVA: 0x0000906A File Offset: 0x0000726A
		public ComplexReferenceTypeAction(IRef context, [Nullable(new byte[]
		{
			1,
			2
		})] Action<T> doFunction, [Nullable(new byte[]
		{
			1,
			2
		})] Action<T> undoFunction)
		{
			this.Context = context;
			this.DoFunction = doFunction;
			this.UndoFunction = undoFunction;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009089 File Offset: 0x00007289
		public void DoAction()
		{
			this.Context.Value = this.Value;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000909D File Offset: 0x0000729D
		public void UndoAction()
		{
			this.Context.Value = this.Original;
		}
	}
}
