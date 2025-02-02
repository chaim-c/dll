using System;
using System.Collections.Generic;
using System.Numerics;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200005B RID: 91
	public abstract class Container : Widget
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000184EA File Offset: 0x000166EA
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x000184F2 File Offset: 0x000166F2
		public ContainerItemDescription DefaultItemDescription { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005C5 RID: 1477
		// (set) Token: 0x060005C6 RID: 1478
		public abstract Predicate<Widget> AcceptDropPredicate { get; set; }

		// Token: 0x060005C7 RID: 1479
		public abstract Vector2 GetDropGizmoPosition(Vector2 draggedWidgetPosition);

		// Token: 0x060005C8 RID: 1480
		public abstract int GetIndexForDrop(Vector2 draggedWidgetPosition);

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000184FB File Offset: 0x000166FB
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x00018518 File Offset: 0x00016718
		public int IntValue
		{
			get
			{
				if (this._intValue >= base.ChildCount)
				{
					this._intValue = -1;
				}
				return this._intValue;
			}
			set
			{
				if (!this._currentlyChangingIntValue)
				{
					this._currentlyChangingIntValue = true;
					if (value != this._intValue && value < base.ChildCount)
					{
						this._intValue = value;
						this.UpdateSelected();
						foreach (Action<Widget> action in this.SelectEventHandlers)
						{
							action(this);
						}
						base.EventFired("SelectedItemChange", Array.Empty<object>());
						base.OnPropertyChanged(value, "IntValue");
					}
					this._currentlyChangingIntValue = false;
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005CB RID: 1483
		public abstract bool IsDragHovering { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x000185BC File Offset: 0x000167BC
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x000185C4 File Offset: 0x000167C4
		public int DragHoverInsertionIndex
		{
			get
			{
				return this._dragHoverInsertionIndex;
			}
			set
			{
				if (this._dragHoverInsertionIndex != value)
				{
					this._dragHoverInsertionIndex = value;
					base.SetMeasureAndLayoutDirty();
				}
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000185DC File Offset: 0x000167DC
		protected Container(UIContext context) : base(context)
		{
			this.DefaultItemDescription = new ContainerItemDescription();
			this._itemDescriptions = new List<ContainerItemDescription>();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001863C File Offset: 0x0001683C
		private void UpdateSelected()
		{
			for (int i = 0; i < base.ChildCount; i++)
			{
				ButtonWidget buttonWidget = base.GetChild(i) as ButtonWidget;
				if (buttonWidget != null)
				{
					bool isSelected = i == this.IntValue;
					buttonWidget.IsSelected = isSelected;
				}
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001867C File Offset: 0x0001687C
		protected internal override bool OnDrop()
		{
			if (base.AcceptDrop)
			{
				bool flag = true;
				if (this.AcceptDropHandler != null)
				{
					flag = this.AcceptDropHandler(this, base.EventManager.DraggedWidget);
				}
				if (flag)
				{
					Widget widget = base.EventManager.ReleaseDraggedWidget();
					int indexForDrop = this.GetIndexForDrop(base.EventManager.DraggedWidgetPosition);
					if (!base.DropEventHandledManually)
					{
						widget.ParentWidget = this;
						widget.SetSiblingIndex(indexForDrop, false);
					}
					base.EventFired("Drop", new object[]
					{
						widget,
						indexForDrop
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005D1 RID: 1489
		public abstract void OnChildSelected(Widget widget);

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001870C File Offset: 0x0001690C
		public ContainerItemDescription GetItemDescription(string id, int index)
		{
			bool flag = !string.IsNullOrEmpty(id);
			ContainerItemDescription containerItemDescription = null;
			ContainerItemDescription containerItemDescription2 = null;
			for (int i = 0; i < this._itemDescriptions.Count; i++)
			{
				ContainerItemDescription containerItemDescription3 = this._itemDescriptions[i];
				if (flag && containerItemDescription3.WidgetId == id)
				{
					containerItemDescription = containerItemDescription3;
				}
				if (index == containerItemDescription3.WidgetIndex)
				{
					containerItemDescription2 = containerItemDescription3;
				}
			}
			ContainerItemDescription result;
			if ((result = containerItemDescription) == null)
			{
				result = (containerItemDescription2 ?? this.DefaultItemDescription);
			}
			return result;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00018780 File Offset: 0x00016980
		protected override void OnChildAdded(Widget child)
		{
			foreach (Action<Widget, Widget> action in this.ItemAddEventHandlers)
			{
				action(this, child);
			}
			base.OnChildAdded(child);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000187DC File Offset: 0x000169DC
		protected override void OnChildRemoved(Widget child)
		{
			int childIndex = base.GetChildIndex(child);
			if (base.ChildCount == 1)
			{
				this.IntValue = -1;
			}
			else if (childIndex < this.IntValue && this.IntValue > 0)
			{
				this.IntValue--;
			}
			foreach (Action<Widget, Widget> action in this.ItemRemoveEventHandlers)
			{
				action(this, child);
			}
			base.OnChildRemoved(child);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00018870 File Offset: 0x00016A70
		protected override void OnAfterChildRemoved(Widget child)
		{
			foreach (Action<Widget> action in this.ItemAfterRemoveEventHandlers)
			{
				action(this);
			}
			base.OnAfterChildRemoved(child);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000188C8 File Offset: 0x00016AC8
		public void AddItemDescription(ContainerItemDescription itemDescription)
		{
			this._itemDescriptions.Add(itemDescription);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000188D8 File Offset: 0x00016AD8
		public ScrollablePanel FindParentPanel()
		{
			for (Widget parentWidget = base.ParentWidget; parentWidget != null; parentWidget = parentWidget.ParentWidget)
			{
				ScrollablePanel result;
				if ((result = (parentWidget as ScrollablePanel)) != null)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x040002B9 RID: 697
		public List<Action<Widget>> SelectEventHandlers = new List<Action<Widget>>();

		// Token: 0x040002BA RID: 698
		public List<Action<Widget, Widget>> ItemAddEventHandlers = new List<Action<Widget, Widget>>();

		// Token: 0x040002BB RID: 699
		public List<Action<Widget, Widget>> ItemRemoveEventHandlers = new List<Action<Widget, Widget>>();

		// Token: 0x040002BC RID: 700
		public List<Action<Widget>> ItemAfterRemoveEventHandlers = new List<Action<Widget>>();

		// Token: 0x040002BD RID: 701
		private int _intValue = -1;

		// Token: 0x040002BE RID: 702
		private bool _currentlyChangingIntValue;

		// Token: 0x040002BF RID: 703
		public bool ShowSelection;

		// Token: 0x040002C0 RID: 704
		private int _dragHoverInsertionIndex;

		// Token: 0x040002C1 RID: 705
		private List<ContainerItemDescription> _itemDescriptions;
	}
}
