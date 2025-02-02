using System;
using TaleWorlds.Library;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x02000021 RID: 33
	internal class ArrayReference : TextExpression
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004DEC File Offset: 0x00002FEC
		public ArrayReference(string rawValue, TextExpression indexExp)
		{
			base.RawValue = rawValue;
			this._indexExp = indexExp;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004E04 File Offset: 0x00003004
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			int index = base.EvaluateAsNumber(this._indexExp, context, parent);
			MultiStatement arrayAccess = context.GetArrayAccess(base.RawValue, index);
			if (arrayAccess != null)
			{
				MBStringBuilder mbstringBuilder = default(MBStringBuilder);
				mbstringBuilder.Initialize(16, "EvaluateString");
				foreach (TextExpression textExpression in arrayAccess.SubStatements)
				{
					mbstringBuilder.Append<string>(textExpression.EvaluateString(context, parent));
				}
				return mbstringBuilder.ToStringAndRelease();
			}
			return "";
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004EA8 File Offset: 0x000030A8
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.ArrayAccess;
			}
		}

		// Token: 0x04000051 RID: 81
		private TextExpression _indexExp;
	}
}
