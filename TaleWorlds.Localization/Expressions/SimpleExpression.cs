using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200001B RID: 27
	internal class SimpleExpression : TextExpression
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00004911 File Offset: 0x00002B11
		public SimpleExpression(TextExpression innerExpression)
		{
			this._innerExpression = innerExpression;
			base.RawValue = innerExpression.RawValue;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000492C File Offset: 0x00002B2C
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return this._innerExpression.EvaluateString(context, parent);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000493B File Offset: 0x00002B3B
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.SimpleExpression;
			}
		}

		// Token: 0x04000046 RID: 70
		private TextExpression _innerExpression;
	}
}
