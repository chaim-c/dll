using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000023 RID: 35
	internal class QualifiedIdentifierExpression : TextExpression
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004EF1 File Offset: 0x000030F1
		public string IdentifierName
		{
			get
			{
				return this._identifierName;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004EF9 File Offset: 0x000030F9
		public QualifiedIdentifierExpression(string identifierName)
		{
			this._identifierName = identifierName;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004F08 File Offset: 0x00003108
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.QualifiedIdentifier;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004F0C File Offset: 0x0000310C
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return context.GetQualifiedVariableValue(this._identifierName, parent).Item1.ToStringWithoutClear();
		}

		// Token: 0x04000054 RID: 84
		private readonly string _identifierName;
	}
}
