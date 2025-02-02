using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200000E RID: 14
	internal class SimpleText : TextExpression
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004499 File Offset: 0x00002699
		public SimpleText(string value)
		{
			base.RawValue = value;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000044A8 File Offset: 0x000026A8
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.Text;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000044AC File Offset: 0x000026AC
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return base.RawValue;
		}
	}
}
