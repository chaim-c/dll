using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000177 RID: 375
	public class PerkSelectionBarWidget : Widget
	{
		// Token: 0x06001363 RID: 4963 RVA: 0x00035266 File Offset: 0x00033466
		public PerkSelectionBarWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00035284 File Offset: 0x00033484
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.PerksList != null)
			{
				for (int i = 0; i < this.PerksList.ChildCount; i++)
				{
					PerkItemButtonWidget perkItemButtonWidget = this.PerksList.GetChild(i) as PerkItemButtonWidget;
					if (this._perkWidgetWidth != perkItemButtonWidget.Size.X)
					{
						this._perkWidgetWidth = perkItemButtonWidget.Size.X;
					}
					perkItemButtonWidget.PositionXOffset = this.GetXPositionOfLevelOnBar((float)perkItemButtonWidget.Level) - this._perkWidgetWidth / 2f * base._inverseScaleToUse;
					if (perkItemButtonWidget.AlternativeType == 0)
					{
						perkItemButtonWidget.PositionYOffset = 45f;
					}
					else if (perkItemButtonWidget.AlternativeType == 1)
					{
						perkItemButtonWidget.PositionYOffset = 5f;
					}
					else if (perkItemButtonWidget.AlternativeType == 2)
					{
						perkItemButtonWidget.PositionYOffset = (float)((int)Mathf.Round(perkItemButtonWidget.Size.Y * base._inverseScaleToUse));
					}
				}
			}
			if (this.PercentageIndicatorWidget != null)
			{
				float xpositionOfLevelOnBar = this.GetXPositionOfLevelOnBar((float)this.Level);
				float num = xpositionOfLevelOnBar - this.PercentageIndicatorWidget.Size.X / 2f * base._inverseScaleToUse;
				this.PercentageIndicatorWidget.PositionXOffset = num;
				if (this.FullLearningRateClip != null)
				{
					float num2 = this.GetXPositionOfLevelOnBar((float)this.FullLearningRateLevel) - xpositionOfLevelOnBar;
					this.FullLearningRateClip.SuggestedWidth = ((num2 >= 0f) ? num2 : 0f);
					this.FullLearningRateClip.PositionXOffset = this.PercentageIndicatorWidget.PositionXOffset + this.PercentageIndicatorWidget.Size.X / 2f * base._inverseScaleToUse;
					this.FullLearningRateClipInnerContent.PositionXOffset = -this.FullLearningRateClip.PositionXOffset;
				}
				this.ProgressClip.SuggestedWidth = num + this.PercentageIndicatorWidget.Size.X / 2f * base._inverseScaleToUse;
			}
			using (List<Widget>.Enumerator enumerator = this.SeperatorContainer.Children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CharacterDeveloperSkillVerticalSeperatorWidget characterDeveloperSkillVerticalSeperatorWidget;
					if ((characterDeveloperSkillVerticalSeperatorWidget = (enumerator.Current as CharacterDeveloperSkillVerticalSeperatorWidget)) != null)
					{
						characterDeveloperSkillVerticalSeperatorWidget.PositionXOffset = this.GetXPositionOfLevelOnBar((float)characterDeveloperSkillVerticalSeperatorWidget.SkillValue);
					}
				}
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000354C4 File Offset: 0x000336C4
		private float GetXPositionOfLevelOnBar(float level)
		{
			return Mathf.Clamp(level / ((float)this.MaxLevel + 25f) * base.Size.X * base._inverseScaleToUse, 0f, base.Size.X * base._inverseScaleToUse);
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00035504 File Offset: 0x00033704
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x0003550C File Offset: 0x0003370C
		public Widget ProgressClip
		{
			get
			{
				return this._progressClip;
			}
			set
			{
				if (this._progressClip != value)
				{
					this._progressClip = value;
					base.OnPropertyChanged<Widget>(value, "ProgressClip");
				}
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0003552A File Offset: 0x0003372A
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x00035532 File Offset: 0x00033732
		public Widget PercentageIndicatorWidget
		{
			get
			{
				return this._percentageIndicatorWidget;
			}
			set
			{
				if (this._percentageIndicatorWidget != value)
				{
					this._percentageIndicatorWidget = value;
					base.OnPropertyChanged<Widget>(value, "PercentageIndicatorWidget");
				}
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00035550 File Offset: 0x00033750
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x00035558 File Offset: 0x00033758
		public Widget FullLearningRateClip
		{
			get
			{
				return this._fullLearningRateClip;
			}
			set
			{
				if (this._fullLearningRateClip != value)
				{
					this._fullLearningRateClip = value;
					base.OnPropertyChanged<Widget>(value, "FullLearningRateClip");
				}
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00035576 File Offset: 0x00033776
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x0003557E File Offset: 0x0003377E
		public Widget SeperatorContainer
		{
			get
			{
				return this._seperatorContainer;
			}
			set
			{
				if (this._seperatorContainer != value)
				{
					this._seperatorContainer = value;
					base.OnPropertyChanged<Widget>(value, "SeperatorContainer");
				}
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0003559C File Offset: 0x0003379C
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x000355A4 File Offset: 0x000337A4
		public Widget FullLearningRateClipInnerContent
		{
			get
			{
				return this._fullLearningRateClipInnerContent;
			}
			set
			{
				if (this._fullLearningRateClipInnerContent != value)
				{
					this._fullLearningRateClipInnerContent = value;
					base.OnPropertyChanged<Widget>(value, "FullLearningRateClipInnerContent");
				}
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x000355C2 File Offset: 0x000337C2
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x000355CA File Offset: 0x000337CA
		public Widget PerksList
		{
			get
			{
				return this._perksList;
			}
			set
			{
				if (this._perksList != value)
				{
					this._perksList = value;
					base.OnPropertyChanged<Widget>(value, "PerksList");
				}
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x000355E8 File Offset: 0x000337E8
		// (set) Token: 0x06001373 RID: 4979 RVA: 0x000355F0 File Offset: 0x000337F0
		public TextWidget PercentageIndicatorTextWidget
		{
			get
			{
				return this._percentageIndicatorTextWidget;
			}
			set
			{
				if (this._percentageIndicatorTextWidget != value)
				{
					this._percentageIndicatorTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "PercentageIndicatorTextWidget");
				}
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0003560E File Offset: 0x0003380E
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00035616 File Offset: 0x00033816
		public int MaxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				if (this._maxLevel != value)
				{
					this._maxLevel = value;
					base.OnPropertyChanged(value, "MaxLevel");
				}
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00035634 File Offset: 0x00033834
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x0003563C File Offset: 0x0003383C
		public int FullLearningRateLevel
		{
			get
			{
				return this._fullLearningRateLevel;
			}
			set
			{
				if (this._fullLearningRateLevel != value)
				{
					this._fullLearningRateLevel = value;
					base.OnPropertyChanged(value, "FullLearningRateLevel");
				}
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0003565A File Offset: 0x0003385A
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x00035662 File Offset: 0x00033862
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (this._level != value)
				{
					this._level = value;
					base.OnPropertyChanged(value, "Level");
				}
			}
		}

		// Token: 0x040008D5 RID: 2261
		private float _perkWidgetWidth = -1f;

		// Token: 0x040008D6 RID: 2262
		private Widget _perksList;

		// Token: 0x040008D7 RID: 2263
		private Widget _progressClip;

		// Token: 0x040008D8 RID: 2264
		private Widget _fullLearningRateClip;

		// Token: 0x040008D9 RID: 2265
		private Widget _fullLearningRateClipInnerContent;

		// Token: 0x040008DA RID: 2266
		private Widget _percentageIndicatorWidget;

		// Token: 0x040008DB RID: 2267
		private Widget _seperatorContainer;

		// Token: 0x040008DC RID: 2268
		private TextWidget _percentageIndicatorTextWidget;

		// Token: 0x040008DD RID: 2269
		private int _maxLevel;

		// Token: 0x040008DE RID: 2270
		private int _fullLearningRateLevel;

		// Token: 0x040008DF RID: 2271
		private int _level = -1;
	}
}
