using System;
using TaleWorlds.Library;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200001F RID: 31
	internal class VariableExpression : TextExpression
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004AF3 File Offset: 0x00002CF3
		public string IdentifierName
		{
			get
			{
				return this._identifierName;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004AFB File Offset: 0x00002CFB
		public VariableExpression(string identifierName, VariableExpression innerExpression)
		{
			base.RawValue = identifierName;
			this._identifierName = identifierName;
			this._innerVariable = innerExpression;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004B18 File Offset: 0x00002D18
		internal MultiStatement GetValue(TextProcessingContext context, TextObject parent)
		{
			if (this._innerVariable == null)
			{
				return context.GetVariableValue(this._identifierName, parent);
			}
			MultiStatement value = this._innerVariable.GetValue(context, parent);
			if (value != null && value != null)
			{
				foreach (TextExpression textExpression in value.SubStatements)
				{
					FieldExpression fieldExpression = textExpression as FieldExpression;
					if (fieldExpression != null && fieldExpression.FieldName == this._identifierName)
					{
						if (fieldExpression.InnerExpression is MultiStatement)
						{
							return fieldExpression.InnerExpression as MultiStatement;
						}
						return new MultiStatement(new TextExpression[]
						{
							fieldExpression.InnerExpression
						});
					}
				}
			}
			return null;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004BE0 File Offset: 0x00002DE0
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			MultiStatement value = this.GetValue(context, parent);
			if (value != null)
			{
				MBStringBuilder mbstringBuilder = default(MBStringBuilder);
				mbstringBuilder.Initialize(16, "EvaluateString");
				foreach (TextExpression textExpression in value.SubStatements)
				{
					if (textExpression != null)
					{
						mbstringBuilder.Append<string>(textExpression.EvaluateString(context, parent));
					}
				}
				return mbstringBuilder.ToStringAndRelease();
			}
			return "";
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004C70 File Offset: 0x00002E70
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.Identifier;
			}
		}

		// Token: 0x0400004D RID: 77
		private VariableExpression _innerVariable;

		// Token: 0x0400004E RID: 78
		private string _identifierName;
	}
}
