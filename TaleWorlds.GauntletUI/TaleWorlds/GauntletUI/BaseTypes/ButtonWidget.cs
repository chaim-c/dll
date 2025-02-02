using System;
using System.Collections.Generic;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000059 RID: 89
	public class ButtonWidget : ImageWidget
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001804F File Offset: 0x0001624F
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00018057 File Offset: 0x00016257
		[Editor(false)]
		public ButtonType ButtonType
		{
			get
			{
				return this._buttonType;
			}
			set
			{
				if (this._buttonType != value)
				{
					this._buttonType = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001806F File Offset: 0x0001626F
		protected override bool OnPreviewMousePressed()
		{
			base.OnPreviewMousePressed();
			return true;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001807C File Offset: 0x0001627C
		protected override void RefreshState()
		{
			base.RefreshState();
			if (!base.OverrideDefaultStateSwitchingEnabled)
			{
				if (base.IsDisabled)
				{
					this.SetState("Disabled");
				}
				else if (this.IsSelected && this.DominantSelectedState)
				{
					this.SetState("Selected");
				}
				else if (base.IsPressed)
				{
					this.SetState("Pressed");
				}
				else if (base.IsHovered)
				{
					this.SetState("Hovered");
				}
				else if (this.IsSelected && !this.DominantSelectedState)
				{
					this.SetState("Selected");
				}
				else
				{
					this.SetState("Default");
				}
			}
			if (base.UpdateChildrenStates)
			{
				for (int i = 0; i < base.ChildCount; i++)
				{
					Widget child = base.GetChild(i);
					if (!(child is ImageWidget) || !((ImageWidget)child).OverrideDefaultStateSwitchingEnabled)
					{
						child.SetState(base.CurrentState);
					}
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00018160 File Offset: 0x00016360
		private void Refresh()
		{
			if (this.IsToggle)
			{
				this.ShowHideToggle();
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018170 File Offset: 0x00016370
		private void ShowHideToggle()
		{
			if (this.ToggleIndicator != null)
			{
				if (this._isSelected)
				{
					this.ToggleIndicator.Show();
					return;
				}
				this.ToggleIndicator.Hide();
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018199 File Offset: 0x00016399
		public ButtonWidget(UIContext context) : base(context)
		{
			base.FrictionEnabled = true;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000181BC File Offset: 0x000163BC
		protected internal override void OnMousePressed()
		{
			if (this._clickState == ButtonWidget.ButtonClickState.None)
			{
				this._clickState = ButtonWidget.ButtonClickState.HandlingClick;
				base.IsPressed = true;
				if (!base.DoNotPassEventsToChildren)
				{
					for (int i = 0; i < base.ChildCount; i++)
					{
						Widget child = base.GetChild(i);
						if (child != null)
						{
							child.IsPressed = true;
						}
					}
				}
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001820C File Offset: 0x0001640C
		protected internal override void OnMouseReleased()
		{
			if (this._clickState == ButtonWidget.ButtonClickState.HandlingClick)
			{
				this._clickState = ButtonWidget.ButtonClickState.None;
				base.IsPressed = false;
				if (!base.DoNotPassEventsToChildren)
				{
					for (int i = 0; i < base.ChildCount; i++)
					{
						Widget child = base.GetChild(i);
						if (child != null)
						{
							child.IsPressed = false;
						}
					}
				}
				if (this.IsPointInsideMeasuredAreaAndCheckIfVisible())
				{
					this.HandleClick();
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018269 File Offset: 0x00016469
		private bool IsPointInsideMeasuredAreaAndCheckIfVisible()
		{
			return base.IsPointInsideMeasuredArea(base.EventManager.MousePosition) && base.IsRecursivelyVisible();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001828C File Offset: 0x0001648C
		protected internal override void OnMouseAlternatePressed()
		{
			if (this._clickState == ButtonWidget.ButtonClickState.None)
			{
				this._clickState = ButtonWidget.ButtonClickState.HandlingAlternateClick;
				base.IsPressed = true;
				if (!base.DoNotPassEventsToChildren)
				{
					for (int i = 0; i < base.ChildCount; i++)
					{
						Widget child = base.GetChild(i);
						if (child != null)
						{
							child.IsPressed = true;
						}
					}
				}
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000182DC File Offset: 0x000164DC
		protected internal override void OnMouseAlternateReleased()
		{
			if (this._clickState == ButtonWidget.ButtonClickState.HandlingAlternateClick)
			{
				this._clickState = ButtonWidget.ButtonClickState.None;
				base.IsPressed = false;
				if (!base.DoNotPassEventsToChildren)
				{
					for (int i = 0; i < base.ChildCount; i++)
					{
						Widget child = base.GetChild(i);
						if (child != null)
						{
							child.IsPressed = false;
						}
					}
				}
				if (this.IsPointInsideMeasuredAreaAndCheckIfVisible())
				{
					this.HandleAlternateClick();
				}
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001833C File Offset: 0x0001653C
		protected virtual void HandleClick()
		{
			foreach (Action<Widget> action in this.ClickEventHandlers)
			{
				action(this);
			}
			bool isSelected = this.IsSelected;
			if (this.IsToggle)
			{
				this.IsSelected = !this.IsSelected;
			}
			else if (this.IsRadio)
			{
				this.IsSelected = true;
				if (this.IsSelected && !isSelected && base.ParentWidget is Container)
				{
					(base.ParentWidget as Container).OnChildSelected(this);
				}
			}
			this.OnClick();
			base.EventFired("Click", Array.Empty<object>());
			if (base.Context.EventManager.Time - this._lastClickTime < 0.5f)
			{
				base.EventFired("DoubleClick", Array.Empty<object>());
				return;
			}
			this._lastClickTime = base.Context.EventManager.Time;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018440 File Offset: 0x00016640
		private void HandleAlternateClick()
		{
			this.OnAlternateClick();
			base.EventFired("AlternateClick", Array.Empty<object>());
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018458 File Offset: 0x00016658
		protected virtual void OnClick()
		{
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001845A File Offset: 0x0001665A
		protected virtual void OnAlternateClick()
		{
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001845C File Offset: 0x0001665C
		public bool IsToggle
		{
			get
			{
				return this.ButtonType == ButtonType.Toggle;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00018467 File Offset: 0x00016667
		public bool IsRadio
		{
			get
			{
				return this.ButtonType == ButtonType.Radio;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00018472 File Offset: 0x00016672
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001847A File Offset: 0x0001667A
		[Editor(false)]
		public Widget ToggleIndicator
		{
			get
			{
				return this._toggleIndicator;
			}
			set
			{
				if (this._toggleIndicator != value)
				{
					this._toggleIndicator = value;
					this.Refresh();
				}
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00018492 File Offset: 0x00016692
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001849A File Offset: 0x0001669A
		[Editor(false)]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (this._isSelected != value)
				{
					this._isSelected = value;
					this.Refresh();
					this.RefreshState();
					base.OnPropertyChanged(value, "IsSelected");
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x000184C4 File Offset: 0x000166C4
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000184CC File Offset: 0x000166CC
		[Editor(false)]
		public bool DominantSelectedState
		{
			get
			{
				return this._dominantSelectedState;
			}
			set
			{
				if (this._dominantSelectedState != value)
				{
					this._dominantSelectedState = value;
					base.OnPropertyChanged(value, "DominantSelectedState");
				}
			}
		}

		// Token: 0x040002AC RID: 684
		protected const float _maxDoubleClickDeltaTimeInSeconds = 0.5f;

		// Token: 0x040002AD RID: 685
		protected float _lastClickTime;

		// Token: 0x040002AE RID: 686
		private ButtonWidget.ButtonClickState _clickState;

		// Token: 0x040002AF RID: 687
		private ButtonType _buttonType;

		// Token: 0x040002B0 RID: 688
		public List<Action<Widget>> ClickEventHandlers = new List<Action<Widget>>();

		// Token: 0x040002B1 RID: 689
		private Widget _toggleIndicator;

		// Token: 0x040002B2 RID: 690
		private bool _isSelected;

		// Token: 0x040002B3 RID: 691
		private bool _dominantSelectedState = true;

		// Token: 0x0200008D RID: 141
		private enum ButtonClickState
		{
			// Token: 0x04000470 RID: 1136
			None,
			// Token: 0x04000471 RID: 1137
			HandlingClick,
			// Token: 0x04000472 RID: 1138
			HandlingAlternateClick
		}
	}
}
