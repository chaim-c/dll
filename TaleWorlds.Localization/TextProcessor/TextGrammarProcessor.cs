using System;
using TaleWorlds.Library;
using TaleWorlds.Localization.Expressions;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x0200002B RID: 43
	public static class TextGrammarProcessor
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00005F54 File Offset: 0x00004154
		public static string Process(MBTextModel dataRepresentation, TextProcessingContext textContext, TextObject parent = null)
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "Process");
			foreach (TextExpression textExpression in dataRepresentation.RootExpressions)
			{
				if (textExpression != null)
				{
					string value = textExpression.EvaluateString(textContext, parent).ToString();
					mbstringBuilder.Append<string>(value);
				}
				else
				{
					MBTextManager.ThrowLocalizationError("Exp should not be null!");
				}
			}
			return mbstringBuilder.ToStringAndRelease();
		}
	}
}
