using System;
using System.Collections.Generic;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200001D RID: 29
	internal class ConditionExpression : TextExpression
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00004985 File Offset: 0x00002B85
		public ConditionExpression(TextExpression condition, TextExpression part1, TextExpression part2)
		{
			this._conditionExpressions = new TextExpression[]
			{
				condition
			};
			this._resultExpressions = new TextExpression[]
			{
				part1,
				part2
			};
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000049B1 File Offset: 0x00002BB1
		public ConditionExpression(List<TextExpression> conditionExpressions, List<TextExpression> resultExpressions2)
		{
			this._conditionExpressions = conditionExpressions.ToArray();
			this._resultExpressions = resultExpressions2.ToArray();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000049D4 File Offset: 0x00002BD4
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			bool flag = false;
			int num = 0;
			TextExpression textExpression = null;
			while (!flag && num < this._conditionExpressions.Length)
			{
				TextExpression textExpression2 = this._conditionExpressions[num];
				string text = textExpression2.EvaluateString(context, parent);
				if (text.Length != 0)
				{
					if (textExpression2.TokenType == TokenType.ParameterWithAttribute || textExpression2.TokenType == TokenType.StartsWith)
					{
						flag = !string.IsNullOrEmpty(text);
					}
					else
					{
						flag = (base.EvaluateAsNumber(textExpression2, context, parent) != 0);
					}
				}
				if (flag)
				{
					if (num < this._resultExpressions.Length)
					{
						textExpression = this._resultExpressions[num];
					}
				}
				else
				{
					num++;
				}
			}
			if (textExpression == null && num < this._resultExpressions.Length)
			{
				textExpression = this._resultExpressions[num];
			}
			return ((textExpression != null) ? textExpression.EvaluateString(context, parent) : null) ?? "";
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004A8C File Offset: 0x00002C8C
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.ConditionalExpression;
			}
		}

		// Token: 0x04000049 RID: 73
		private TextExpression[] _conditionExpressions;

		// Token: 0x0400004A RID: 74
		private TextExpression[] _resultExpressions;
	}
}
