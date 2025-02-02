using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCM.Common;

namespace MCM.UI.Actions
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class UndoRedoStack
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009382 File Offset: 0x00007582
		private Stack<IAction> UndoStack { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000938A File Offset: 0x0000758A
		private Stack<IAction> RedoStack { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00009392 File Offset: 0x00007592
		public bool CanUndo
		{
			get
			{
				return this.UndoStack.Count > 0;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000093A2 File Offset: 0x000075A2
		public bool CanRedo
		{
			get
			{
				return this.RedoStack.Count > 0;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000093B2 File Offset: 0x000075B2
		public bool ChangesMade
		{
			get
			{
				return this.UndoStack.Count > 0;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000093C2 File Offset: 0x000075C2
		public UndoRedoStack()
		{
			this.UndoStack = new Stack<IAction>();
			this.RedoStack = new Stack<IAction>();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000093E4 File Offset: 0x000075E4
		public bool RefChanged(IRef @ref)
		{
			List<IAction> stack = (from s in this.UndoStack
			where object.Equals(s.Context, @ref)
			select s).ToList<IAction>();
			bool flag = stack.Count == 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IAction firstChange = stack.First<IAction>();
				IAction lastChange = stack.Last<IAction>();
				object originalValue = firstChange.Original;
				object currentValue = lastChange.Value;
				bool flag2 = originalValue == null && currentValue == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = originalValue == null && currentValue != null;
					if (flag3)
					{
						result = true;
					}
					else
					{
						bool flag4 = originalValue != null && currentValue == null;
						result = (flag4 || !originalValue.Equals(currentValue));
					}
				}
			}
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000094A5 File Offset: 0x000076A5
		public void Do(IAction action)
		{
			action.DoAction();
			this.UndoStack.Push(action);
			this.RedoStack.Clear();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000094C8 File Offset: 0x000076C8
		public void Undo()
		{
			bool canUndo = this.CanUndo;
			if (canUndo)
			{
				IAction a = this.UndoStack.Pop();
				a.UndoAction();
				this.RedoStack.Push(a);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009504 File Offset: 0x00007704
		public void Redo()
		{
			bool canRedo = this.CanRedo;
			if (canRedo)
			{
				IAction a = this.RedoStack.Pop();
				a.DoAction();
				this.UndoStack.Push(a);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009540 File Offset: 0x00007740
		public void UndoAll()
		{
			bool canUndo = this.CanUndo;
			if (canUndo)
			{
				while (this.UndoStack.Count > 0)
				{
					IAction a = this.UndoStack.Pop();
					a.UndoAction();
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009583 File Offset: 0x00007783
		public void ClearStack()
		{
			this.UndoStack.Clear();
			this.RedoStack.Clear();
		}
	}
}
