using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard
{
	// Token: 0x0200008D RID: 141
	public class MultiplayerScoreboardStatsParentWidget : Widget
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0001664A File Offset: 0x0001484A
		public MultiplayerScoreboardStatsParentWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00016654 File Offset: 0x00014854
		private void RefreshActiveState()
		{
			float alphaFactor = this.IsActive ? this.ActiveAlpha : this.InactiveAlpha;
			foreach (Widget widget in base.AllChildren)
			{
				RichTextWidget widget2;
				TextWidget widget3;
				if ((widget2 = (widget as RichTextWidget)) != null)
				{
					widget2.SetAlpha(alphaFactor);
				}
				else if ((widget3 = (widget as TextWidget)) != null)
				{
					widget3.SetAlpha(alphaFactor);
				}
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000166D8 File Offset: 0x000148D8
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x000166E0 File Offset: 0x000148E0
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChanged(value, "IsActive");
					this.RefreshActiveState();
				}
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00016704 File Offset: 0x00014904
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0001670F File Offset: 0x0001490F
		public bool IsInactive
		{
			get
			{
				return !this.IsActive;
			}
			set
			{
				if (value == this.IsActive)
				{
					this.IsActive = !value;
					base.OnPropertyChanged(value, "IsInactive");
				}
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00016730 File Offset: 0x00014930
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00016738 File Offset: 0x00014938
		public float ActiveAlpha
		{
			get
			{
				return this._activeAlpha;
			}
			set
			{
				if (value != this._activeAlpha)
				{
					this._activeAlpha = value;
					base.OnPropertyChanged(value, "ActiveAlpha");
				}
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00016756 File Offset: 0x00014956
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001675E File Offset: 0x0001495E
		public float InactiveAlpha
		{
			get
			{
				return this._inactiveAlpha;
			}
			set
			{
				if (value != this._inactiveAlpha)
				{
					this._inactiveAlpha = value;
					base.OnPropertyChanged(value, "InactiveAlpha");
				}
			}
		}

		// Token: 0x04000371 RID: 881
		private bool _isActive;

		// Token: 0x04000372 RID: 882
		private float _activeAlpha;

		// Token: 0x04000373 RID: 883
		private float _inactiveAlpha;
	}
}
