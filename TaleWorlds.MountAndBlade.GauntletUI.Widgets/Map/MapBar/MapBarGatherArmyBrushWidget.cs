using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar
{
	// Token: 0x02000119 RID: 281
	public class MapBarGatherArmyBrushWidget : BrushWidget
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x00028CDC File Offset: 0x00026EDC
		public MapBarGatherArmyBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00028CE5 File Offset: 0x00026EE5
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.UpdateVisualState();
				this._initialized = true;
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00028D04 File Offset: 0x00026F04
		private void UpdateVisualState()
		{
			base.IsEnabled = this.IsGatherArmyVisible;
			if (!this.IsGatherArmyVisible)
			{
				this.SetState("Disabled");
				return;
			}
			if (this._isInfoBarExtended)
			{
				this.SetState("Extended");
				return;
			}
			this.SetState("Default");
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00028D50 File Offset: 0x00026F50
		private void OnMapInfoBarExtendStateChange(bool newState)
		{
			this._isInfoBarExtended = newState;
			this.UpdateVisualState();
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00028D5F File Offset: 0x00026F5F
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00028D67 File Offset: 0x00026F67
		public MapInfoBarWidget InfoBarWidget
		{
			get
			{
				return this._infoBarWidget;
			}
			set
			{
				if (this._infoBarWidget != value)
				{
					this._infoBarWidget = value;
					this._infoBarWidget.OnMapInfoBarExtendStateChange += this.OnMapInfoBarExtendStateChange;
				}
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00028D90 File Offset: 0x00026F90
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00028D98 File Offset: 0x00026F98
		public bool IsGatherArmyEnabled
		{
			get
			{
				return this._isGatherArmyEnabled;
			}
			set
			{
				if (this._isGatherArmyEnabled != value)
				{
					this._isGatherArmyEnabled = value;
					this.UpdateVisualState();
				}
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00028DB0 File Offset: 0x00026FB0
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x00028DB8 File Offset: 0x00026FB8
		public bool IsGatherArmyVisible
		{
			get
			{
				return this._isGatherArmyVisible;
			}
			set
			{
				if (this._isGatherArmyVisible != value)
				{
					this._isGatherArmyVisible = value;
					this.UpdateVisualState();
				}
			}
		}

		// Token: 0x040006BD RID: 1725
		private bool _isInfoBarExtended;

		// Token: 0x040006BE RID: 1726
		private bool _initialized;

		// Token: 0x040006BF RID: 1727
		private MapInfoBarWidget _infoBarWidget;

		// Token: 0x040006C0 RID: 1728
		private bool _isGatherArmyEnabled;

		// Token: 0x040006C1 RID: 1729
		private bool _isGatherArmyVisible;
	}
}
