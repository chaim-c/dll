using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x02000156 RID: 342
	public class CraftedWeaponDesignResultListPanel : ListPanel
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00031C56 File Offset: 0x0002FE56
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x00031C5E File Offset: 0x0002FE5E
		public CounterTextBrushWidget ChangeValueTextWidget { get; set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x00031C67 File Offset: 0x0002FE67
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x00031C6F File Offset: 0x0002FE6F
		public CounterTextBrushWidget ValueTextWidget { get; set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00031C78 File Offset: 0x0002FE78
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x00031C80 File Offset: 0x0002FE80
		public RichTextWidget GoldEffectorTextWidget { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00031C89 File Offset: 0x0002FE89
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x00031C91 File Offset: 0x0002FE91
		public Brush PositiveChangeBrush { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00031C9A File Offset: 0x0002FE9A
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x00031CA2 File Offset: 0x0002FEA2
		public Brush NegativeChangeBrush { get; set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00031CAB File Offset: 0x0002FEAB
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x00031CB3 File Offset: 0x0002FEB3
		public Brush NeutralBrush { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00031CBC File Offset: 0x0002FEBC
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x00031CC4 File Offset: 0x0002FEC4
		public float FadeInTimeIndexOffset { get; set; } = 2f;

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00031CCD File Offset: 0x0002FECD
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00031CD5 File Offset: 0x0002FED5
		public float FadeInTime { get; set; } = 0.5f;

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00031CDE File Offset: 0x0002FEDE
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x00031CE6 File Offset: 0x0002FEE6
		public float CounterStartTime { get; set; } = 2f;

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00031CEF File Offset: 0x0002FEEF
		private bool _hasChange
		{
			get
			{
				return this.ChangeAmount != 0f;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00031D01 File Offset: 0x0002FF01
		private float _valueTextStartFadeInTime
		{
			get
			{
				return (float)base.GetSiblingIndex() * this.FadeInTimeIndexOffset;
			}
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00031D11 File Offset: 0x0002FF11
		public CraftedWeaponDesignResultListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00031D3C File Offset: 0x0002FF3C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.ValueTextWidget.FloatTarget = this.InitValue;
				this.ValueTextWidget.ForceSetValue(this.InitValue);
				if (this._hasChange)
				{
					this.ValueTextWidget.Brush = ((this.ChangeAmount > 0f) ? this.PositiveChangeBrush : this.NegativeChangeBrush);
					this.ChangeValueTextWidget.Brush = ((this.ChangeAmount > 0f) ? this.PositiveChangeBrush : this.NegativeChangeBrush);
					this.ChangeValueTextWidget.IsVisible = true;
				}
				else
				{
					this.ChangeValueTextWidget.IsVisible = false;
					this.ValueTextWidget.Brush = this.NeutralBrush;
				}
				this.ChangeValueTextWidget.SetGlobalAlphaRecursively(0f);
				this.ValueTextWidget.SetGlobalAlphaRecursively(0f);
				this.ChangeValueTextWidget.ShowSign = true;
				if (this.InitValue == 0f)
				{
					this.LabelTextWidget.SetState(this._isExceedingBeneficial ? "Bonus" : "Penalty");
				}
				this._initialized = true;
			}
			if (this._totalTime > this._valueTextStartFadeInTime)
			{
				float num = (this._totalTime - this._valueTextStartFadeInTime) / this.FadeInTime;
				if (num >= 0f && num <= 1f)
				{
					float num2 = MathF.Lerp(0f, 1f, num, 1E-05f);
					if (num2 < 1f)
					{
						this.ValueTextWidget.SetGlobalAlphaRecursively(num2);
					}
				}
				if (this._hasChange && this._totalTime > this._valueTextStartFadeInTime + this.CounterStartTime)
				{
					this.ValueTextWidget.FloatTarget = this.InitValue + this.ChangeAmount;
					num = (this._totalTime - this._valueTextStartFadeInTime - this.FadeInTime) / this.FadeInTime;
					if (num >= 0f && num <= 1f)
					{
						float num3 = MathF.Lerp(0f, 1f, num, 1E-05f);
						if (num3 < 1f)
						{
							this.ChangeValueTextWidget.SetGlobalAlphaRecursively(num3);
						}
					}
					this.ChangeValueTextWidget.FloatTarget = this.ChangeAmount;
				}
			}
			this._totalTime += dt;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00031F68 File Offset: 0x00030168
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x00031F70 File Offset: 0x00030170
		public RichTextWidget LabelTextWidget
		{
			get
			{
				return this._labelTextWidget;
			}
			set
			{
				if (this._labelTextWidget != value)
				{
					this._labelTextWidget = value;
				}
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00031F82 File Offset: 0x00030182
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x00031F8A File Offset: 0x0003018A
		public float InitValue
		{
			get
			{
				return this._initValue;
			}
			set
			{
				if (this._initValue != value)
				{
					this._initValue = value;
				}
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00031F9C File Offset: 0x0003019C
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x00031FA4 File Offset: 0x000301A4
		public float ChangeAmount
		{
			get
			{
				return this._changeAmount;
			}
			set
			{
				if (this._changeAmount != value)
				{
					this._changeAmount = value;
				}
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00031FB6 File Offset: 0x000301B6
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00031FBE File Offset: 0x000301BE
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

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00031FD0 File Offset: 0x000301D0
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x00031FD8 File Offset: 0x000301D8
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00031FEA File Offset: 0x000301EA
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x00031FF2 File Offset: 0x000301F2
		public bool IsOrderResult
		{
			get
			{
				return this._isOrderResult;
			}
			set
			{
				if (value != this._isOrderResult)
				{
					this._isOrderResult = value;
				}
			}
		}

		// Token: 0x0400083E RID: 2110
		private bool _initialized;

		// Token: 0x0400083F RID: 2111
		private float _totalTime;

		// Token: 0x04000840 RID: 2112
		private RichTextWidget _labelTextWidget;

		// Token: 0x04000841 RID: 2113
		private float _initValue;

		// Token: 0x04000842 RID: 2114
		private float _changeAmount;

		// Token: 0x04000843 RID: 2115
		private float _targetValue;

		// Token: 0x04000844 RID: 2116
		private bool _isExceedingBeneficial;

		// Token: 0x04000845 RID: 2117
		private bool _isOrderResult;
	}
}
