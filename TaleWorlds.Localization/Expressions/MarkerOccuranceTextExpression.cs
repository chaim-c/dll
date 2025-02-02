using System;
using TaleWorlds.Library;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000020 RID: 32
	internal class MarkerOccuranceTextExpression : TextExpression
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004C74 File Offset: 0x00002E74
		public string IdentifierName
		{
			get
			{
				return this._identifierName;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004C7C File Offset: 0x00002E7C
		public MarkerOccuranceTextExpression(string identifierName, VariableExpression innerExpression)
		{
			base.RawValue = identifierName;
			this._identifierName = identifierName;
			this._innerVariable = innerExpression;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004C9C File Offset: 0x00002E9C
		private string MarkerOccuranceExpression(string identifierName, string text)
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(16, "MarkerOccuranceExpression");
			while (i < text.Length)
			{
				if (text[i] != '{')
				{
					if (num == 1 && num2 == 0)
					{
						mbstringBuilder.Append(text[i]);
					}
				}
				else
				{
					string text2 = TextProcessingContext.ReadFirstToken(text, ref i);
					if (TextProcessingContext.IsDeclarationFinalizer(text2))
					{
						num--;
						if (num2 > num)
						{
							num2 = num;
						}
					}
					else if (TextProcessingContext.IsDeclaration(text2))
					{
						string strB = text2.Substring(1);
						bool flag = num2 == num && string.Compare(identifierName, strB, StringComparison.InvariantCultureIgnoreCase) == 0;
						num++;
						if (flag)
						{
							num2 = num;
						}
					}
				}
				i++;
			}
			return mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004D4C File Offset: 0x00002F4C
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			MultiStatement value = this._innerVariable.GetValue(context, parent);
			if (value != null)
			{
				foreach (TextExpression textExpression in value.SubStatements)
				{
					if (textExpression.TokenType == TokenType.LanguageMarker && textExpression.RawValue.Substring(2, textExpression.RawValue.Length - 3) == this.IdentifierName)
					{
						return "1";
					}
				}
			}
			return "0";
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004DE8 File Offset: 0x00002FE8
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.MarkerOccuranceExpression;
			}
		}

		// Token: 0x0400004F RID: 79
		private VariableExpression _innerVariable;

		// Token: 0x04000050 RID: 80
		private string _identifierName;
	}
}
