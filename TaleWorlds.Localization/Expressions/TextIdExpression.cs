using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000011 RID: 17
	internal class TextIdExpression : TextExpression
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000044EA File Offset: 0x000026EA
		public TextIdExpression(string innerText)
		{
			base.RawValue = innerText;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000044F9 File Offset: 0x000026F9
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return "";
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004500 File Offset: 0x00002700
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.textId;
			}
		}
	}
}
