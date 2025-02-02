using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000018 RID: 24
	internal class ParanthesisExpression : TextExpression
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004654 File Offset: 0x00002854
		public ParanthesisExpression(TextExpression innerExpression)
		{
			this._innerExp = innerExpression;
			base.RawValue = "(" + innerExpression.RawValue + ")";
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000467E File Offset: 0x0000287E
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return this._innerExp.EvaluateString(context, parent);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000468D File Offset: 0x0000288D
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.ParenthesisExpression;
			}
		}

		// Token: 0x0400003F RID: 63
		private readonly TextExpression _innerExp;
	}
}
