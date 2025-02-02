using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200000F RID: 15
	internal class SimpleNumberExpression : TextExpression
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000044B4 File Offset: 0x000026B4
		public SimpleNumberExpression(string value)
		{
			base.RawValue = value;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000044C3 File Offset: 0x000026C3
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.Number;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000044C7 File Offset: 0x000026C7
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return base.RawValue;
		}
	}
}
