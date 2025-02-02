using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000125 RID: 293
	public class KingdomDecisionOptionWidget : Widget
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00029DE5 File Offset: 0x00027FE5
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00029DED File Offset: 0x00027FED
		public Widget SealVisualWidget { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00029DF6 File Offset: 0x00027FF6
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00029DFE File Offset: 0x00027FFE
		public DecisionSupportStrengthListPanel StrengthWidget { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00029E07 File Offset: 0x00028007
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00029E0F File Offset: 0x0002800F
		public bool IsPlayerSupporter { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00029E18 File Offset: 0x00028018
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00029E20 File Offset: 0x00028020
		public bool IsAbstain { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00029E29 File Offset: 0x00028029
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00029E31 File Offset: 0x00028031
		public float SealStartWidth { get; set; } = 232f;

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00029E3A File Offset: 0x0002803A
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x00029E42 File Offset: 0x00028042
		public float SealStartHeight { get; set; } = 232f;

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00029E4B File Offset: 0x0002804B
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x00029E53 File Offset: 0x00028053
		public float SealEndWidth { get; set; } = 140f;

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00029E5C File Offset: 0x0002805C
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00029E64 File Offset: 0x00028064
		public float SealEndHeight { get; set; } = 140f;

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00029E6D File Offset: 0x0002806D
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x00029E75 File Offset: 0x00028075
		public float SealAnimLength { get; set; } = 0.2f;

		// Token: 0x06000F28 RID: 3880 RVA: 0x00029E80 File Offset: 0x00028080
		public KingdomDecisionOptionWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00029ED8 File Offset: 0x000280D8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.StrengthWidget.IsVisible = (!this.IsAbstain && this.IsPlayerSupporter && this.IsOptionSelected && !this.IsKingsOption && !this._isKingsDecisionDone);
			if (this._animStartTime != -1f && base.EventManager.Time - this._animStartTime < this.SealAnimLength)
			{
				this.SealVisualWidget.IsVisible = true;
				float amount = (base.EventManager.Time - this._animStartTime) / this.SealAnimLength;
				this.SealVisualWidget.SuggestedWidth = Mathf.Lerp(this.SealStartWidth, this.SealEndWidth, amount);
				this.SealVisualWidget.SuggestedHeight = Mathf.Lerp(this.SealStartHeight, this.SealEndHeight, amount);
				this.SealVisualWidget.SetGlobalAlphaRecursively(Mathf.Lerp(0f, 1f, amount));
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00029FC8 File Offset: 0x000281C8
		internal void OnKingsDecisionDone()
		{
			this._isKingsDecisionDone = true;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00029FD1 File Offset: 0x000281D1
		internal void OnFinalDone()
		{
			this._isKingsDecisionDone = false;
			this._animStartTime = -1f;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00029FE5 File Offset: 0x000281E5
		private void OnSelectionChange(bool value)
		{
			if (!this.IsPlayerSupporter)
			{
				this.SealVisualWidget.IsVisible = value;
				this.SealVisualWidget.SetGlobalAlphaRecursively(0.2f);
				return;
			}
			this.SealVisualWidget.IsVisible = false;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002A018 File Offset: 0x00028218
		private void HandleKingsOption()
		{
			this._animStartTime = base.EventManager.Time;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0002A02B File Offset: 0x0002822B
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x0002A033 File Offset: 0x00028233
		[Editor(false)]
		public bool IsOptionSelected
		{
			get
			{
				return this._isOptionSelected;
			}
			set
			{
				if (this._isOptionSelected != value)
				{
					this._isOptionSelected = value;
					base.OnPropertyChanged(value, "IsOptionSelected");
					this.OnSelectionChange(value);
					base.GamepadNavigationIndex = (value ? -1 : 0);
				}
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0002A065 File Offset: 0x00028265
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x0002A06D File Offset: 0x0002826D
		[Editor(false)]
		public bool IsKingsOption
		{
			get
			{
				return this._isKingsOption;
			}
			set
			{
				if (this._isKingsOption != value)
				{
					this._isKingsOption = value;
					base.OnPropertyChanged(value, "IsKingsOption");
					this.HandleKingsOption();
				}
			}
		}

		// Token: 0x040006F4 RID: 1780
		private float _animStartTime = -1f;

		// Token: 0x040006F5 RID: 1781
		private bool _isKingsDecisionDone;

		// Token: 0x040006F6 RID: 1782
		private bool _isOptionSelected;

		// Token: 0x040006F7 RID: 1783
		public bool _isKingsOption;
	}
}
