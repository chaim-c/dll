using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200000D RID: 13
	internal abstract class TextExpression
	{
		// Token: 0x06000090 RID: 144
		internal abstract string EvaluateString(TextProcessingContext context, TextObject parent);

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000091 RID: 145
		internal abstract TokenType TokenType { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004430 File Offset: 0x00002630
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004438 File Offset: 0x00002638
		internal string RawValue { get; set; }

		// Token: 0x06000094 RID: 148 RVA: 0x00004444 File Offset: 0x00002644
		internal int EvaluateAsNumber(TextExpression exp, TextProcessingContext context, TextObject parent)
		{
			NumeralExpression numeralExpression = exp as NumeralExpression;
			if (numeralExpression != null)
			{
				return numeralExpression.EvaluateNumber(context, parent);
			}
			int result;
			if (int.TryParse(exp.EvaluateString(context, parent), out result))
			{
				return result;
			}
			if (exp.RawValue == null)
			{
				return 0;
			}
			if (exp.RawValue.Length != 0)
			{
				return 1;
			}
			return 0;
		}
	}
}
