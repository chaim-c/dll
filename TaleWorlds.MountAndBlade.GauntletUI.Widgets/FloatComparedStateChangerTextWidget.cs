using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200001D RID: 29
	public class FloatComparedStateChangerTextWidget : TextWidget
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005BED File Offset: 0x00003DED
		public FloatComparedStateChangerTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005BF8 File Offset: 0x00003DF8
		private void UpdateState()
		{
			if (string.IsNullOrEmpty(this.TrueState) || string.IsNullOrEmpty(this.FalseState))
			{
				return;
			}
			bool flag = false;
			if (this.ComparisonType == FloatComparedStateChangerTextWidget.ComparisonTypes.Equals)
			{
				flag = (this.FirstValue == this.SecondValue);
			}
			else if (this.ComparisonType == FloatComparedStateChangerTextWidget.ComparisonTypes.NotEquals)
			{
				flag = (this.FirstValue != this.SecondValue);
			}
			else if (this.ComparisonType == FloatComparedStateChangerTextWidget.ComparisonTypes.LessThan)
			{
				flag = (this.FirstValue < this.SecondValue);
			}
			else if (this.ComparisonType == FloatComparedStateChangerTextWidget.ComparisonTypes.GreaterThan)
			{
				flag = (this.FirstValue > this.SecondValue);
			}
			else if (this.ComparisonType == FloatComparedStateChangerTextWidget.ComparisonTypes.GreaterThanOrEqual)
			{
				flag = (this.FirstValue >= this.SecondValue);
			}
			else if (this.ComparisonType == FloatComparedStateChangerTextWidget.ComparisonTypes.LessThanOrEqual)
			{
				flag = (this.FirstValue <= this.SecondValue);
			}
			this.SetState(flag ? this.TrueState : this.FalseState);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005CDE File Offset: 0x00003EDE
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005CE6 File Offset: 0x00003EE6
		public FloatComparedStateChangerTextWidget.ComparisonTypes ComparisonType
		{
			get
			{
				return this._comparisonType;
			}
			set
			{
				if (value != this._comparisonType)
				{
					this._comparisonType = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005CFE File Offset: 0x00003EFE
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005D06 File Offset: 0x00003F06
		public float FirstValue
		{
			get
			{
				return this._firstValue;
			}
			set
			{
				if (value != this._firstValue)
				{
					this._firstValue = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005D1E File Offset: 0x00003F1E
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005D26 File Offset: 0x00003F26
		public float SecondValue
		{
			get
			{
				return this._secondValue;
			}
			set
			{
				if (value != this._secondValue)
				{
					this._secondValue = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005D3E File Offset: 0x00003F3E
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005D46 File Offset: 0x00003F46
		public string TrueState
		{
			get
			{
				return this._trueState;
			}
			set
			{
				if (value != this._trueState)
				{
					this._trueState = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005D63 File Offset: 0x00003F63
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00005D6B File Offset: 0x00003F6B
		public string FalseState
		{
			get
			{
				return this._falseState;
			}
			set
			{
				if (value != this._falseState)
				{
					this._falseState = value;
					this.UpdateState();
				}
			}
		}

		// Token: 0x0400009F RID: 159
		private FloatComparedStateChangerTextWidget.ComparisonTypes _comparisonType;

		// Token: 0x040000A0 RID: 160
		private float _firstValue;

		// Token: 0x040000A1 RID: 161
		private float _secondValue;

		// Token: 0x040000A2 RID: 162
		private string _trueState;

		// Token: 0x040000A3 RID: 163
		private string _falseState;

		// Token: 0x0200018E RID: 398
		public enum ComparisonTypes
		{
			// Token: 0x0400094B RID: 2379
			Equals,
			// Token: 0x0400094C RID: 2380
			NotEquals,
			// Token: 0x0400094D RID: 2381
			GreaterThan,
			// Token: 0x0400094E RID: 2382
			LessThan,
			// Token: 0x0400094F RID: 2383
			GreaterThanOrEqual,
			// Token: 0x04000950 RID: 2384
			LessThanOrEqual
		}
	}
}
