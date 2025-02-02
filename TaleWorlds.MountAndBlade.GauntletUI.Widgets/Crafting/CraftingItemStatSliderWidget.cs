using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x02000158 RID: 344
	public class CraftingItemStatSliderWidget : SliderWidget
	{
		// Token: 0x06001223 RID: 4643 RVA: 0x00032062 File Offset: 0x00030262
		public CraftingItemStatSliderWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0003206C File Offset: 0x0003026C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			float num = 1f;
			float x = base.SliderArea.Size.X;
			if (MathF.Abs(base.MaxValueFloat - base.MinValueFloat) > 1E-45f)
			{
				num = (base.ValueFloat - base.MinValueFloat) / (base.MaxValueFloat - base.MinValueFloat) * x;
				if (base.ReverseDirection)
				{
					num = 1f - num;
				}
			}
			if (this.HasValidTarget && this.TargetFill != null && base.Handle != null && this.ValueText != null)
			{
				float num2 = base.SliderArea.Size.X / base.MaxValueFloat * this.TargetValue;
				int num3 = MathF.Ceiling(MathF.Min(num, num2));
				int num4 = MathF.Floor(MathF.Max(num, num2));
				base.Filler.ScaledSuggestedWidth = (float)num3;
				this.TargetFill.ScaledPositionXOffset = (float)num3;
				this.TargetFill.ScaledSuggestedWidth = (float)(num4 - num3);
				base.Handle.ScaledPositionXOffset = num2 - base.Handle.Size.X / 2f;
				string state = (this.IsExceedingBeneficial ? (base.ValueFloat >= this.TargetValue) : (base.ValueFloat <= this.TargetValue)) ? "Bonus" : "Penalty";
				this.TargetFill.SetState(state);
				this.ValueText.SetState(state);
				if (!this.HasValidValue)
				{
					this.LabelTextWidget.SetState(state);
					return;
				}
			}
			else
			{
				base.Filler.ScaledSuggestedWidth = num;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00032209 File Offset: 0x00030409
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x00032211 File Offset: 0x00030411
		[Editor(false)]
		public TextWidget ValueText
		{
			get
			{
				return this._valueText;
			}
			set
			{
				if (value != this._valueText)
				{
					this._valueText = value;
				}
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00032223 File Offset: 0x00030423
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x0003222B File Offset: 0x0003042B
		[Editor(false)]
		public TextWidget LabelTextWidget
		{
			get
			{
				return this._labelTextWidget;
			}
			set
			{
				if (value != this._labelTextWidget)
				{
					this._labelTextWidget = value;
				}
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0003223D File Offset: 0x0003043D
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x00032245 File Offset: 0x00030445
		[Editor(false)]
		public bool HasValidTarget
		{
			get
			{
				return this._hasValidTarget;
			}
			set
			{
				if (value != this._hasValidTarget)
				{
					this._hasValidTarget = value;
				}
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00032257 File Offset: 0x00030457
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x0003225F File Offset: 0x0003045F
		[Editor(false)]
		public bool HasValidValue
		{
			get
			{
				return this._hasValidValue;
			}
			set
			{
				if (value != this._hasValidValue)
				{
					this._hasValidValue = value;
				}
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00032271 File Offset: 0x00030471
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x00032279 File Offset: 0x00030479
		[Editor(false)]
		public bool IsExceedingBeneficial
		{
			get
			{
				return this._isExceedingBeneficial;
			}
			set
			{
				if (value != this._isExceedingBeneficial)
				{
					this._isExceedingBeneficial = value;
				}
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0003228B File Offset: 0x0003048B
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x00032293 File Offset: 0x00030493
		[Editor(false)]
		public float TargetValue
		{
			get
			{
				return this._targetValue;
			}
			set
			{
				if (value != this._targetValue)
				{
					this._targetValue = value;
				}
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x000322A5 File Offset: 0x000304A5
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x000322AD File Offset: 0x000304AD
		[Editor(false)]
		public BrushWidget TargetFill
		{
			get
			{
				return this._targetFill;
			}
			set
			{
				if (value != this._targetFill)
				{
					this._targetFill = value;
				}
			}
		}

		// Token: 0x04000848 RID: 2120
		private bool _hasValidTarget;

		// Token: 0x04000849 RID: 2121
		private bool _hasValidValue;

		// Token: 0x0400084A RID: 2122
		private bool _isExceedingBeneficial;

		// Token: 0x0400084B RID: 2123
		private float _targetValue;

		// Token: 0x0400084C RID: 2124
		private BrushWidget _targetFill;

		// Token: 0x0400084D RID: 2125
		private TextWidget _valueText;

		// Token: 0x0400084E RID: 2126
		private TextWidget _labelTextWidget;
	}
}
