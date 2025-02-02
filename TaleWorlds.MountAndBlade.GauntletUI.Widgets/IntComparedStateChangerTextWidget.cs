using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000027 RID: 39
	public class IntComparedStateChangerTextWidget : TextWidget
	{
		// Token: 0x060001FB RID: 507 RVA: 0x000078DC File Offset: 0x00005ADC
		public IntComparedStateChangerTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000078E8 File Offset: 0x00005AE8
		private void UpdateState()
		{
			if (string.IsNullOrEmpty(this.TrueState) || string.IsNullOrEmpty(this.FalseState))
			{
				return;
			}
			bool flag = false;
			if (this.ComparisonType == IntComparedStateChangerTextWidget.ComparisonTypes.Equals)
			{
				flag = (this.FirstValue == this.SecondValue);
			}
			else if (this.ComparisonType == IntComparedStateChangerTextWidget.ComparisonTypes.NotEquals)
			{
				flag = (this.FirstValue != this.SecondValue);
			}
			else if (this.ComparisonType == IntComparedStateChangerTextWidget.ComparisonTypes.LessThan)
			{
				flag = (this.FirstValue < this.SecondValue);
			}
			else if (this.ComparisonType == IntComparedStateChangerTextWidget.ComparisonTypes.GreaterThan)
			{
				flag = (this.FirstValue > this.SecondValue);
			}
			else if (this.ComparisonType == IntComparedStateChangerTextWidget.ComparisonTypes.GreaterThanOrEqual)
			{
				flag = (this.FirstValue >= this.SecondValue);
			}
			else if (this.ComparisonType == IntComparedStateChangerTextWidget.ComparisonTypes.LessThanOrEqual)
			{
				flag = (this.FirstValue <= this.SecondValue);
			}
			this.SetState(flag ? this.TrueState : this.FalseState);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000079CE File Offset: 0x00005BCE
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000079D6 File Offset: 0x00005BD6
		public IntComparedStateChangerTextWidget.ComparisonTypes ComparisonType
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000079EE File Offset: 0x00005BEE
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000079F6 File Offset: 0x00005BF6
		public int FirstValue
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00007A0E File Offset: 0x00005C0E
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00007A16 File Offset: 0x00005C16
		public int SecondValue
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00007A2E File Offset: 0x00005C2E
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00007A36 File Offset: 0x00005C36
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00007A53 File Offset: 0x00005C53
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00007A5B File Offset: 0x00005C5B
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

		// Token: 0x040000F5 RID: 245
		private IntComparedStateChangerTextWidget.ComparisonTypes _comparisonType;

		// Token: 0x040000F6 RID: 246
		private int _firstValue;

		// Token: 0x040000F7 RID: 247
		private int _secondValue;

		// Token: 0x040000F8 RID: 248
		private string _trueState;

		// Token: 0x040000F9 RID: 249
		private string _falseState;

		// Token: 0x02000190 RID: 400
		public enum ComparisonTypes
		{
			// Token: 0x04000954 RID: 2388
			Equals,
			// Token: 0x04000955 RID: 2389
			NotEquals,
			// Token: 0x04000956 RID: 2390
			GreaterThan,
			// Token: 0x04000957 RID: 2391
			LessThan,
			// Token: 0x04000958 RID: 2392
			GreaterThanOrEqual,
			// Token: 0x04000959 RID: 2393
			LessThanOrEqual
		}
	}
}
