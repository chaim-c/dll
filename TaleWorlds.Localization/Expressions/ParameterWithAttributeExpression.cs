using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000024 RID: 36
	internal class ParameterWithAttributeExpression : TextExpression
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004F25 File Offset: 0x00003125
		public ParameterWithAttributeExpression(string identifierName)
		{
			this._parameter = identifierName.Remove(identifierName.IndexOf('.'));
			this._attribute = identifierName.Substring(identifierName.IndexOf('.'));
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004F55 File Offset: 0x00003155
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.ParameterWithAttribute;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004F5C File Offset: 0x0000315C
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			TextObject functionParamWithoutEvaluate = context.GetFunctionParamWithoutEvaluate(this._parameter);
			ValueTuple<TextObject, bool> qualifiedVariableValue = context.GetQualifiedVariableValue(functionParamWithoutEvaluate.ToStringWithoutClear() + this._attribute, parent);
			TextObject item = qualifiedVariableValue.Item1;
			if (qualifiedVariableValue.Item2)
			{
				return item.ToStringWithoutClear();
			}
			return "";
		}

		// Token: 0x04000055 RID: 85
		private readonly string _parameter;

		// Token: 0x04000056 RID: 86
		private readonly string _attribute;
	}
}
