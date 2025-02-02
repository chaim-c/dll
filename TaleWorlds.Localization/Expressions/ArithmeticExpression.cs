using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200001A RID: 26
	internal class ArithmeticExpression : NumeralExpression
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000047F9 File Offset: 0x000029F9
		public ArithmeticExpression(ArithmeticOperation op, TextExpression exp1, TextExpression exp2)
		{
			this._op = op;
			this._exp1 = exp1;
			this._exp2 = exp2;
			base.RawValue = exp1.RawValue + op + exp2.RawValue;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004834 File Offset: 0x00002A34
		internal override int EvaluateNumber(TextProcessingContext context, TextObject parent)
		{
			switch (this._op)
			{
			case ArithmeticOperation.Add:
				return base.EvaluateAsNumber(this._exp1, context, parent) + base.EvaluateAsNumber(this._exp2, context, parent);
			case ArithmeticOperation.Subtract:
				return base.EvaluateAsNumber(this._exp1, context, parent) - base.EvaluateAsNumber(this._exp2, context, parent);
			case ArithmeticOperation.Multiply:
				return base.EvaluateAsNumber(this._exp1, context, parent) * base.EvaluateAsNumber(this._exp2, context, parent);
			case ArithmeticOperation.Divide:
				return base.EvaluateAsNumber(this._exp1, context, parent) / base.EvaluateAsNumber(this._exp2, context, parent);
			default:
				return 0;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000048DC File Offset: 0x00002ADC
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return this.EvaluateNumber(context, parent).ToString();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000048F9 File Offset: 0x00002AF9
		internal override TokenType TokenType
		{
			get
			{
				if (this._op != ArithmeticOperation.Add && this._op != ArithmeticOperation.Subtract)
				{
					return TokenType.ArithmeticProduct;
				}
				return TokenType.ArithmeticSum;
			}
		}

		// Token: 0x04000043 RID: 67
		private readonly ArithmeticOperation _op;

		// Token: 0x04000044 RID: 68
		private readonly TextExpression _exp1;

		// Token: 0x04000045 RID: 69
		private readonly TextExpression _exp2;
	}
}
