using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Siege
{
	// Token: 0x0200010F RID: 271
	public class MapSiegeScreenWidget : Widget
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x00027683 File Offset: 0x00025883
		public MapSiegeScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002768C File Offset: 0x0002588C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			Widget latestMouseUpWidget = base.EventManager.LatestMouseUpWidget;
			if (this._currentSelectedButton != null && latestMouseUpWidget != null && !(latestMouseUpWidget is MapSiegeMachineButtonWidget) && !this._currentSelectedButton.CheckIsMyChildRecursive(latestMouseUpWidget) && this.IsWidgetChildOfType<MapSiegeMachineButtonWidget>(latestMouseUpWidget) == null)
			{
				this.SetCurrentButton(null);
			}
			if (base.EventManager.LatestMouseUpWidget == null)
			{
				this.SetCurrentButton(null);
			}
			if (this.DeployableSiegeMachinesPopup != null)
			{
				this.DeployableSiegeMachinesPopup.IsVisible = (this._currentSelectedButton != null);
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00027710 File Offset: 0x00025910
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._currentSelectedButton != null && this.DeployableSiegeMachinesPopup != null)
			{
				this.DeployableSiegeMachinesPopup.ScaledPositionXOffset = Mathf.Clamp(this._currentSelectedButton.GlobalPosition.X - this.DeployableSiegeMachinesPopup.Size.X / 2f + this._currentSelectedButton.Size.X / 2f, 0f, base.EventManager.PageSize.X - this.DeployableSiegeMachinesPopup.Size.X);
				this.DeployableSiegeMachinesPopup.ScaledPositionYOffset = Mathf.Clamp(this._currentSelectedButton.GlobalPosition.Y + this._currentSelectedButton.Size.Y + 10f * base._inverseScaleToUse, 0f, base.EventManager.PageSize.Y - this.DeployableSiegeMachinesPopup.Size.Y);
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00027812 File Offset: 0x00025A12
		public void SetCurrentButton(MapSiegeMachineButtonWidget button)
		{
			if (button == null)
			{
				this._currentSelectedButton = null;
				return;
			}
			if (this._currentSelectedButton == button || !button.IsDeploymentTarget)
			{
				this.SetCurrentButton(null);
				return;
			}
			this._currentSelectedButton = button;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002783F File Offset: 0x00025A3F
		protected override bool OnPreviewMousePressed()
		{
			this.SetCurrentButton(null);
			return false;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00027849 File Offset: 0x00025A49
		protected override bool OnPreviewDragEnd()
		{
			return false;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0002784C File Offset: 0x00025A4C
		protected override bool OnPreviewDragBegin()
		{
			return false;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0002784F File Offset: 0x00025A4F
		protected override bool OnPreviewDrop()
		{
			return false;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00027852 File Offset: 0x00025A52
		protected override bool OnPreviewDragHover()
		{
			return false;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00027855 File Offset: 0x00025A55
		protected override bool OnPreviewMouseMove()
		{
			return false;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00027858 File Offset: 0x00025A58
		protected override bool OnPreviewMouseReleased()
		{
			return false;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002785B File Offset: 0x00025A5B
		protected override bool OnPreviewMouseScroll()
		{
			return false;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0002785E File Offset: 0x00025A5E
		protected override bool OnPreviewMouseAlternatePressed()
		{
			return false;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00027861 File Offset: 0x00025A61
		protected override bool OnPreviewMouseAlternateReleased()
		{
			return false;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00027864 File Offset: 0x00025A64
		private T IsWidgetChildOfType<T>(Widget currentWidget) where T : Widget
		{
			while (currentWidget != null)
			{
				if (currentWidget is T)
				{
					return (T)((object)currentWidget);
				}
				currentWidget = currentWidget.ParentWidget;
			}
			return default(T);
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00027896 File Offset: 0x00025A96
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x0002789E File Offset: 0x00025A9E
		[Editor(false)]
		public Widget DeployableSiegeMachinesPopup
		{
			get
			{
				return this._deployableSiegeMachinesPopup;
			}
			set
			{
				if (value != this._deployableSiegeMachinesPopup)
				{
					this._deployableSiegeMachinesPopup = value;
					base.OnPropertyChanged<Widget>(value, "DeployableSiegeMachinesPopup");
				}
			}
		}

		// Token: 0x04000691 RID: 1681
		private Widget _deployableSiegeMachinesPopup;

		// Token: 0x04000692 RID: 1682
		private MapSiegeMachineButtonWidget _currentSelectedButton;
	}
}
