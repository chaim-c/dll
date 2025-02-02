using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C4 RID: 196
	public class MultiplayerClassLoadoutItemTabControllerButtonWidget : ButtonWidget
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x0001D35D File Offset: 0x0001B55D
		public MultiplayerClassLoadoutItemTabControllerButtonWidget(UIContext context) : base(context)
		{
			this._itemTabs = new List<MultiplayerItemTabButtonWidget>();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001D374 File Offset: 0x0001B574
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this._itemTabs.Clear();
			for (int i = 0; i < this.ItemTabList.ChildCount; i++)
			{
				MultiplayerItemTabButtonWidget multiplayerItemTabButtonWidget = (MultiplayerItemTabButtonWidget)this.ItemTabList.GetChild(i);
				multiplayerItemTabButtonWidget.boolPropertyChanged += this.TabWidgetPropertyChanged;
				this._itemTabs.Add(multiplayerItemTabButtonWidget);
			}
			this.ItemTabList.OnInitialized += this.ItemTabListInitialized;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			for (int i = 0; i < this._itemTabs.Count; i++)
			{
				this._itemTabs[i].boolPropertyChanged -= this.TabWidgetPropertyChanged;
			}
			this._itemTabs.Clear();
			this.ItemTabList.OnInitialized -= this.ItemTabListInitialized;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001D458 File Offset: 0x0001B658
		protected override void OnUpdate(float dt)
		{
			if (this.CursorWidget == null || float.IsNaN(this._targetPositionXOffset) || this.AnimationSpeed <= 0f || MathF.Abs(this.CursorWidget.PositionXOffset - this._targetPositionXOffset) <= 1E-05f)
			{
				return;
			}
			int num = MathF.Sign(this._targetPositionXOffset - this.CursorWidget.PositionXOffset);
			float amount = MathF.Min(this.AnimationSpeed * dt, 1f);
			this.CursorWidget.PositionXOffset = MathF.Lerp(this.CursorWidget.PositionXOffset, this._targetPositionXOffset, amount, 1E-05f);
			if ((num < 0 && this.CursorWidget.PositionXOffset < this._targetPositionXOffset) || (num > 0 && this.CursorWidget.PositionXOffset > this._targetPositionXOffset))
			{
				this.CursorWidget.PositionXOffset = this._targetPositionXOffset;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001D537 File Offset: 0x0001B737
		private void TabWidgetPropertyChanged(PropertyOwnerObject sender, string propertyName, bool value)
		{
			if (propertyName == "IsSelected" && value)
			{
				this.SelectedTabChanged(null);
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001D54F File Offset: 0x0001B74F
		private void ItemTabListInitialized()
		{
			this.SelectedTabChanged(null);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001D558 File Offset: 0x0001B758
		private void SelectedTabChanged(Widget widget)
		{
			if (this.CursorWidget == null || this.ItemTabList.IntValue < 0)
			{
				return;
			}
			int selectedIndex = -1;
			int num = 0;
			for (int i = 0; i < this.ItemTabList.ChildCount; i++)
			{
				ButtonWidget buttonWidget = (ButtonWidget)this.ItemTabList.GetChild(i);
				if (buttonWidget.IsVisible)
				{
					num++;
					if (buttonWidget.IsSelected)
					{
						selectedIndex = num - 1;
					}
				}
			}
			this.CalculateTargetPosition(selectedIndex, num);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001D5C8 File Offset: 0x0001B7C8
		private void CalculateTargetPosition(int selectedIndex, int activeTabCount)
		{
			float num = this.ItemTabList.Size.X / base._scaleToUse;
			float num2 = num / (float)activeTabCount;
			float num3 = (float)selectedIndex * num2 + num2 / 2f;
			this._targetPositionXOffset = num3 - num / 2f;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001D60E File Offset: 0x0001B80E
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0001D618 File Offset: 0x0001B818
		[DataSourceProperty]
		public MultiplayerClassLoadoutItemTabListPanel ItemTabList
		{
			get
			{
				return this._itemTabList;
			}
			set
			{
				if (value != this._itemTabList)
				{
					MultiplayerClassLoadoutItemTabListPanel itemTabList = this._itemTabList;
					if (itemTabList != null)
					{
						itemTabList.SelectEventHandlers.Remove(new Action<Widget>(this.SelectedTabChanged));
					}
					this._itemTabList = value;
					MultiplayerClassLoadoutItemTabListPanel itemTabList2 = this._itemTabList;
					if (itemTabList2 != null)
					{
						itemTabList2.SelectEventHandlers.Add(new Action<Widget>(this.SelectedTabChanged));
					}
					base.OnPropertyChanged<MultiplayerClassLoadoutItemTabListPanel>(value, "ItemTabList");
				}
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001D686 File Offset: 0x0001B886
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0001D68E File Offset: 0x0001B88E
		[DataSourceProperty]
		public Widget CursorWidget
		{
			get
			{
				return this._cursorWidget;
			}
			set
			{
				if (value != this._cursorWidget)
				{
					this._cursorWidget = value;
					base.OnPropertyChanged<Widget>(value, "CursorWidget");
				}
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		[DataSourceProperty]
		public float AnimationSpeed
		{
			get
			{
				return this._animationSpeed;
			}
			set
			{
				if (value != this._animationSpeed)
				{
					this._animationSpeed = value;
					base.OnPropertyChanged(value, "AnimationSpeed");
				}
			}
		}

		// Token: 0x040004BB RID: 1211
		private readonly List<MultiplayerItemTabButtonWidget> _itemTabs;

		// Token: 0x040004BC RID: 1212
		private float _targetPositionXOffset;

		// Token: 0x040004BD RID: 1213
		private MultiplayerClassLoadoutItemTabListPanel _itemTabList;

		// Token: 0x040004BE RID: 1214
		private Widget _cursorWidget;

		// Token: 0x040004BF RID: 1215
		private float _animationSpeed;
	}
}
