using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000013 RID: 19
	internal class MultiStatement : TextExpression
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004596 File Offset: 0x00002796
		public MultiStatement(IEnumerable<TextExpression> subStatements)
		{
			this._subStatements = subStatements.ToMBList<TextExpression>();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000045B5 File Offset: 0x000027B5
		public MBReadOnlyList<TextExpression> SubStatements
		{
			get
			{
				return this._subStatements;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000045BD File Offset: 0x000027BD
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.MultiStatement;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000045C1 File Offset: 0x000027C1
		public void AddStatement(TextExpression s2)
		{
			this._subStatements.Add(s2);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000045D0 File Offset: 0x000027D0
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "EvaluateString");
			foreach (TextExpression textExpression in this._subStatements)
			{
				if (textExpression != null)
				{
					mbstringBuilder.Append<string>(textExpression.EvaluateString(context, parent));
				}
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x0400002E RID: 46
		private MBList<TextExpression> _subStatements = new MBList<TextExpression>();
	}
}
