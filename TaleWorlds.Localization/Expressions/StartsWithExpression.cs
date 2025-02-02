using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000025 RID: 37
	internal class StartsWithExpression : TextExpression
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004FA8 File Offset: 0x000031A8
		public StartsWithExpression(string identifierName)
		{
			int num = identifierName.IndexOf('(');
			int num2 = identifierName.IndexOf(')');
			this._parameter = identifierName.Remove(num);
			this._functionParams = identifierName.Substring(num + 1, num2 - num - 1).Split(new char[]
			{
				','
			});
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004FFE File Offset: 0x000031FE
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.StartsWith;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005004 File Offset: 0x00003204
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			TextObject functionParamWithoutEvaluate = context.GetFunctionParamWithoutEvaluate(this._parameter);
			ValueTuple<TextObject, bool> qualifiedVariableValue = context.GetQualifiedVariableValue(functionParamWithoutEvaluate.ToStringWithoutClear(), parent);
			TextObject item = qualifiedVariableValue.Item1;
			if (qualifiedVariableValue.Item2)
			{
				foreach (string text in this._functionParams)
				{
					if (item.ToStringWithoutClear().StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
					{
						return text;
					}
				}
			}
			return "";
		}

		// Token: 0x04000057 RID: 87
		private readonly string _parameter;

		// Token: 0x04000058 RID: 88
		private readonly string[] _functionParams;
	}
}
