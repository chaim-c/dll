using System;
using System.Collections.Generic;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200001E RID: 30
	internal class SelectionExpression : TextExpression
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004A90 File Offset: 0x00002C90
		public SelectionExpression(TextExpression selection, List<TextExpression> selectionExpressions)
		{
			this._selection = selection;
			this._selectionExpressions = selectionExpressions;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004AA8 File Offset: 0x00002CA8
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			int num = base.EvaluateAsNumber(this._selection, context, parent);
			if (num >= 0 && num < this._selectionExpressions.Count)
			{
				return this._selectionExpressions[num].EvaluateString(context, parent);
			}
			return "";
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004AEF File Offset: 0x00002CEF
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.SelectionExpression;
			}
		}

		// Token: 0x0400004B RID: 75
		private TextExpression _selection;

		// Token: 0x0400004C RID: 76
		private List<TextExpression> _selectionExpressions;
	}
}
