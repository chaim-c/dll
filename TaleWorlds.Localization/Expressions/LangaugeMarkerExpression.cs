using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000010 RID: 16
	internal class LangaugeMarkerExpression : TextExpression
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000044CF File Offset: 0x000026CF
		public LangaugeMarkerExpression(string innerText)
		{
			base.RawValue = innerText;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000044DE File Offset: 0x000026DE
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return base.RawValue;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000044E6 File Offset: 0x000026E6
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.LanguageMarker;
			}
		}
	}
}
