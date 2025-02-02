using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000B4 RID: 180
	public class TauntCircleActionSelectorWidget : CircleActionSelectorWidget
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x0001AF76 File Offset: 0x00019176
		public TauntCircleActionSelectorWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001AF90 File Offset: 0x00019190
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._currentSelectedIndex != -1)
			{
				Widget child = base.GetChild(this._currentSelectedIndex);
				object obj;
				if (child == null)
				{
					obj = null;
				}
				else
				{
					obj = child.Children.FirstOrDefault((Widget c) => c is ButtonWidget);
				}
				ButtonWidget buttonWidget = obj as ButtonWidget;
				Widget widget = (buttonWidget != null) ? buttonWidget.FindChild("InputKeyContainer", true) : null;
				if (widget != null && !widget.IsVisible)
				{
					base.EventManager.SetHoveredView(null);
					base.EventManager.SetHoveredView(buttonWidget);
				}
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001B028 File Offset: 0x00019228
		protected override void OnSelectedIndexChanged(int selectedIndex)
		{
			if (this._currentSelectedIndex == selectedIndex)
			{
				return;
			}
			this._currentSelectedIndex = selectedIndex;
			bool flag = false;
			for (int i = 0; i < base.ChildCount; i++)
			{
				Widget child = base.GetChild(i);
				ButtonWidget buttonWidget = child.Children.FirstOrDefault((Widget c) => c is ButtonWidget) as ButtonWidget;
				if (child.GamepadNavigationIndex != -1 && buttonWidget != null)
				{
					bool flag2 = buttonWidget.IsEnabled && this._currentSelectedIndex == i;
					child.DoNotAcceptNavigation = !flag2;
					if (flag2)
					{
						this.SetCurrentNavigationTarget(child);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				this.SetCurrentNavigationTarget(this.FallbackNavigationWidget);
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001B0DA File Offset: 0x000192DA
		private void SetCurrentNavigationTarget(Widget target)
		{
			if (this._tauntSlotNavigationTrialCount == -1)
			{
				this._currentNavigationTarget = target;
				this._tauntSlotNavigationTrialCount = 0;
				base.EventManager.AddLateUpdateAction(this, new Action<float>(this.NavigationUpdate), 1);
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001B10C File Offset: 0x0001930C
		private void NavigationUpdate(float dt)
		{
			if (this._currentNavigationTarget != null)
			{
				if (GauntletGamepadNavigationManager.Instance.TryNavigateTo(this._currentNavigationTarget))
				{
					this._currentNavigationTarget = null;
					this._tauntSlotNavigationTrialCount = -1;
					return;
				}
				if (this._tauntSlotNavigationTrialCount < 5)
				{
					this._tauntSlotNavigationTrialCount++;
					base.EventManager.AddLateUpdateAction(this, new Action<float>(this.NavigationUpdate), 1);
					return;
				}
				this._tauntSlotNavigationTrialCount = -1;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001B17A File Offset: 0x0001937A
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x0001B182 File Offset: 0x00019382
		public Widget FallbackNavigationWidget
		{
			get
			{
				return this._fallbackNavigationWidget;
			}
			set
			{
				if (value != this._fallbackNavigationWidget)
				{
					this._fallbackNavigationWidget = value;
					base.OnPropertyChanged<Widget>(value, "FallbackNavigationWidget");
				}
			}
		}

		// Token: 0x04000454 RID: 1108
		private Widget _currentNavigationTarget;

		// Token: 0x04000455 RID: 1109
		private int _currentSelectedIndex = -1;

		// Token: 0x04000456 RID: 1110
		private int _tauntSlotNavigationTrialCount = -1;

		// Token: 0x04000457 RID: 1111
		private Widget _fallbackNavigationWidget;
	}
}
