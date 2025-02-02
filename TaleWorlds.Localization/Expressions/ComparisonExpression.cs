using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000019 RID: 25
	internal class ComparisonExpression : NumeralExpression
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00004691 File Offset: 0x00002891
		public ComparisonExpression(ComparisonOperation op, TextExpression exp1, TextExpression exp2)
		{
			this._op = op;
			this._exp1 = exp1;
			this._exp2 = exp2;
			base.RawValue = exp1.RawValue + op + exp2.RawValue;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000046CC File Offset: 0x000028CC
		internal bool EvaluateBoolean(TextProcessingContext context, TextObject parent)
		{
			switch (this._op)
			{
			case ComparisonOperation.Equals:
				return base.EvaluateAsNumber(this._exp1, context, parent) == base.EvaluateAsNumber(this._exp2, context, parent);
			case ComparisonOperation.NotEquals:
				return base.EvaluateAsNumber(this._exp1, context, parent) != base.EvaluateAsNumber(this._exp2, context, parent);
			case ComparisonOperation.GreaterThan:
				return base.EvaluateAsNumber(this._exp1, context, parent) > base.EvaluateAsNumber(this._exp2, context, parent);
			case ComparisonOperation.GreaterOrEqual:
				return base.EvaluateAsNumber(this._exp1, context, parent) >= base.EvaluateAsNumber(this._exp2, context, parent);
			case ComparisonOperation.LessThan:
				return base.EvaluateAsNumber(this._exp1, context, parent) < base.EvaluateAsNumber(this._exp2, context, parent);
			case ComparisonOperation.LessOrEqual:
				return base.EvaluateAsNumber(this._exp1, context, parent) <= base.EvaluateAsNumber(this._exp2, context, parent);
			default:
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000047C7 File Offset: 0x000029C7
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.ComparisonExpression;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000047CB File Offset: 0x000029CB
		internal override int EvaluateNumber(TextProcessingContext context, TextObject parent)
		{
			if (!this.EvaluateBoolean(context, parent))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000047DC File Offset: 0x000029DC
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return this.EvaluateNumber(context, parent).ToString();
		}

		// Token: 0x04000040 RID: 64
		private readonly ComparisonOperation _op;

		// Token: 0x04000041 RID: 65
		private readonly TextExpression _exp1;

		// Token: 0x04000042 RID: 66
		private readonly TextExpression _exp2;
	}
}
