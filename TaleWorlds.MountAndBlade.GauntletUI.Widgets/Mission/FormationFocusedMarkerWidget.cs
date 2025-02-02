using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D4 RID: 212
	public class FormationFocusedMarkerWidget : BrushWidget
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001ED1A File Offset: 0x0001CF1A
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0001ED22 File Offset: 0x0001CF22
		public int NormalSize { get; set; } = 55;

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001ED2B File Offset: 0x0001CF2B
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0001ED33 File Offset: 0x0001CF33
		public int FocusedSize { get; set; } = 60;

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001ED3C File Offset: 0x0001CF3C
		public FormationFocusedMarkerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001ED55 File Offset: 0x0001CF55
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.UpdateVisibility();
			if (base.IsVisible)
			{
				this.UpdateSize();
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001ED72 File Offset: 0x0001CF72
		private void UpdateVisibility()
		{
			base.IsVisible = (this.IsTargetingAFormation || (this.IsFormationTargetRelevant && this.IsCenterOfFocus));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001ED98 File Offset: 0x0001CF98
		private void UpdateSize()
		{
			float num4;
			if (this.IsCenterOfFocus)
			{
				float num = (float)(this.IsTargetingAFormation ? (this.FocusedSize + 3) : this.FocusedSize);
				float num2 = MathF.Sin(base.EventManager.Time * 5f);
				num2 = (num2 + 1f) / 2f;
				float num3 = (num - (float)this.NormalSize) * num2;
				num4 = (float)this.NormalSize + num3;
			}
			else
			{
				num4 = (float)this.NormalSize;
			}
			base.ScaledSuggestedHeight = num4 * base._scaleToUse;
			base.ScaledSuggestedWidth = num4 * base._scaleToUse;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001EE25 File Offset: 0x0001D025
		private void UpdateState()
		{
			this.SetState(this.IsTargetingAFormation ? "Targeting" : "Default");
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001EE41 File Offset: 0x0001D041
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0001EE49 File Offset: 0x0001D049
		public bool IsCenterOfFocus
		{
			get
			{
				return this._isCenterOfFocus;
			}
			set
			{
				if (this._isCenterOfFocus != value)
				{
					this._isCenterOfFocus = value;
					base.OnPropertyChanged(value, "IsCenterOfFocus");
				}
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001EE67 File Offset: 0x0001D067
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0001EE6F File Offset: 0x0001D06F
		public bool IsFormationTargetRelevant
		{
			get
			{
				return this._isFormationTargetRelevant;
			}
			set
			{
				if (this._isFormationTargetRelevant != value)
				{
					this._isFormationTargetRelevant = value;
					base.OnPropertyChanged(value, "IsFormationTargetRelevant");
				}
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0001EE8D File Offset: 0x0001D08D
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0001EE95 File Offset: 0x0001D095
		public bool IsTargetingAFormation
		{
			get
			{
				return this._isTargetingAFormation;
			}
			set
			{
				if (this._isTargetingAFormation != value)
				{
					this._isTargetingAFormation = value;
					base.OnPropertyChanged(value, "IsTargetingAFormation");
					this.UpdateState();
				}
			}
		}

		// Token: 0x040004FC RID: 1276
		private bool _isCenterOfFocus;

		// Token: 0x040004FD RID: 1277
		private bool _isFormationTargetRelevant;

		// Token: 0x040004FE RID: 1278
		private bool _isTargetingAFormation;
	}
}
