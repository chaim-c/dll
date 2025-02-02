using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000022 RID: 34
	internal class FunctionCall : TextExpression
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00004EAC File Offset: 0x000030AC
		public FunctionCall(string functionName, IEnumerable<TextExpression> functionParams)
		{
			this._functionName = functionName;
			this._functionParams = functionParams.ToList<TextExpression>();
			base.RawValue = this._functionName;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004ED3 File Offset: 0x000030D3
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return context.CallFunction(this._functionName, this._functionParams, parent).ToStringWithoutClear();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004EED File Offset: 0x000030ED
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.FunctionCall;
			}
		}

		// Token: 0x04000052 RID: 82
		private string _functionName;

		// Token: 0x04000053 RID: 83
		private List<TextExpression> _functionParams;
	}
}
