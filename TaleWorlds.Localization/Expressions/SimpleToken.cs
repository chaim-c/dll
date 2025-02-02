using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000012 RID: 18
	internal class SimpleToken : TextExpression
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004504 File Offset: 0x00002704
		public SimpleToken(TokenType tokenType, string value)
		{
			base.RawValue = value;
			this._tokenType = tokenType;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000451C File Offset: 0x0000271C
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			switch (this.TokenType)
			{
			case TokenType.FunctionParam:
				return context.GetFunctionParam(base.RawValue).ToStringWithoutClear();
			case TokenType.ParameterWithMarkerOccurance:
				return context.GetParameterWithMarkerOccurance(base.RawValue, parent);
			case TokenType.ParameterWithMultipleMarkerOccurances:
				return context.GetParameterWithMarkerOccurances(base.RawValue, parent);
			default:
				return base.RawValue;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000457B File Offset: 0x0000277B
		internal override TokenType TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x0400002C RID: 44
		public static readonly SimpleToken SequenceTerminator = new SimpleToken(TokenType.SequenceTerminator, ".");

		// Token: 0x0400002D RID: 45
		private readonly TokenType _tokenType;
	}
}
