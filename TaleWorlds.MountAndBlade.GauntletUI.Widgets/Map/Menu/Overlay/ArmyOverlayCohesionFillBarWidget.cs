using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Menu.Overlay
{
	// Token: 0x02000114 RID: 276
	public class ArmyOverlayCohesionFillBarWidget : FillBarWidget
	{
		// Token: 0x06000E87 RID: 3719 RVA: 0x00028756 File Offset: 0x00026956
		public ArmyOverlayCohesionFillBarWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00028766 File Offset: 0x00026966
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isWarningDirty)
			{
				this.DetermineBarAnimState();
				this._isWarningDirty = false;
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00028784 File Offset: 0x00026984
		private void DetermineBarAnimState()
		{
			BrushWidget brushWidget;
			if (base.FillWidget != null && (brushWidget = (base.FillWidget as BrushWidget)) != null)
			{
				brushWidget.RegisterBrushStatesOfWidget();
				if (this.IsCohesionWarningEnabled)
				{
					if (brushWidget.CurrentState == "WarningLeader")
					{
						brushWidget.BrushRenderer.RestartAnimation();
						return;
					}
					if (this.IsArmyLeader)
					{
						brushWidget.SetState("WarningLeader");
						return;
					}
					brushWidget.SetState("WarningNormal");
					return;
				}
				else
				{
					if (brushWidget.CurrentState == "Default")
					{
						brushWidget.BrushRenderer.RestartAnimation();
						return;
					}
					brushWidget.SetState("Default");
				}
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00028820 File Offset: 0x00026A20
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x00028828 File Offset: 0x00026A28
		[Editor(false)]
		public bool IsCohesionWarningEnabled
		{
			get
			{
				return this._isCohesionWarningEnabled;
			}
			set
			{
				if (value != this._isCohesionWarningEnabled)
				{
					this._isCohesionWarningEnabled = value;
					base.OnPropertyChanged(value, "IsCohesionWarningEnabled");
					this.DetermineBarAnimState();
					this._isWarningDirty = true;
				}
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00028853 File Offset: 0x00026A53
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0002885B File Offset: 0x00026A5B
		[Editor(false)]
		public bool IsArmyLeader
		{
			get
			{
				return this._isArmyLeader;
			}
			set
			{
				if (value != this._isArmyLeader)
				{
					this._isArmyLeader = value;
					base.OnPropertyChanged(value, "IsArmyLeader");
					this.DetermineBarAnimState();
					this._isWarningDirty = true;
				}
			}
		}

		// Token: 0x040006B0 RID: 1712
		private bool _isWarningDirty = true;

		// Token: 0x040006B1 RID: 1713
		private bool _isCohesionWarningEnabled;

		// Token: 0x040006B2 RID: 1714
		private bool _isArmyLeader;
	}
}
