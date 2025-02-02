using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Nameplate.Notifications
{
	// Token: 0x0200007B RID: 123
	public class NameplateNotificationListPanel : ListPanel
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x00014CDD File Offset: 0x00012EDD
		public NameplateNotificationListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00014D0C File Offset: 0x00012F0C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._isFirstFrame)
			{
				switch (this.RelationType)
				{
				case -1:
					this.RelationVisualWidget.Color = NameplateNotificationListPanel.NegativeRelationColor;
					break;
				case 0:
					this.RelationVisualWidget.Color = NameplateNotificationListPanel.NeutralRelationColor;
					break;
				case 1:
					this.RelationVisualWidget.Color = NameplateNotificationListPanel.PositiveRelationColor;
					break;
				}
				this._isFirstFrame = false;
			}
			this._totalDt += dt;
			if (base.AlphaFactor <= 0f || this._totalDt > this._stayAmount + this._fadeTime)
			{
				base.EventFired("OnRemove", Array.Empty<object>());
				return;
			}
			if (this._totalDt > this._stayAmount)
			{
				float alphaFactor = 1f - (this._totalDt - this._stayAmount) / this._fadeTime;
				this.SetGlobalAlphaRecursively(alphaFactor);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00014DF1 File Offset: 0x00012FF1
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00014DF9 File Offset: 0x00012FF9
		public Widget RelationVisualWidget
		{
			get
			{
				return this._relationVisualWidget;
			}
			set
			{
				if (this._relationVisualWidget != value)
				{
					this._relationVisualWidget = value;
					base.OnPropertyChanged<Widget>(value, "RelationVisualWidget");
				}
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00014E17 File Offset: 0x00013017
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00014E1F File Offset: 0x0001301F
		public int RelationType
		{
			get
			{
				return this._relationType;
			}
			set
			{
				if (this._relationType != value)
				{
					this._relationType = value;
					base.OnPropertyChanged(value, "RelationType");
				}
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00014E3D File Offset: 0x0001303D
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00014E45 File Offset: 0x00013045
		public float StayAmount
		{
			get
			{
				return this._stayAmount;
			}
			set
			{
				if (this._stayAmount != value)
				{
					this._stayAmount = value;
					base.OnPropertyChanged(value, "StayAmount");
				}
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00014E63 File Offset: 0x00013063
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00014E6B File Offset: 0x0001306B
		public float FadeTime
		{
			get
			{
				return this._fadeTime;
			}
			set
			{
				if (this._fadeTime != value)
				{
					this._fadeTime = value;
					base.OnPropertyChanged(value, "FadeTime");
				}
			}
		}

		// Token: 0x0400030E RID: 782
		private static readonly Color NegativeRelationColor = Color.ConvertStringToColor("#D6543BFF");

		// Token: 0x0400030F RID: 783
		private static readonly Color NeutralRelationColor = Color.ConvertStringToColor("#ECB05BFF");

		// Token: 0x04000310 RID: 784
		private static readonly Color PositiveRelationColor = Color.ConvertStringToColor("#98CA3AFF");

		// Token: 0x04000311 RID: 785
		private float _totalDt;

		// Token: 0x04000312 RID: 786
		private bool _isFirstFrame = true;

		// Token: 0x04000313 RID: 787
		private Widget _relationVisualWidget;

		// Token: 0x04000314 RID: 788
		private float _stayAmount = 2f;

		// Token: 0x04000315 RID: 789
		private float _fadeTime = 1f;

		// Token: 0x04000316 RID: 790
		private int _relationType = -2;
	}
}
