using System;
using TaleWorlds.Localization.TextProcessor;

namespace TaleWorlds.Localization.Expressions
{
	// Token: 0x0200001C RID: 28
	internal class FieldExpression : TextExpression
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000493F File Offset: 0x00002B3F
		public string FieldName
		{
			get
			{
				return base.RawValue;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004947 File Offset: 0x00002B47
		public TextExpression InnerExpression
		{
			get
			{
				return this.part2;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000494F File Offset: 0x00002B4F
		public FieldExpression(TextExpression innerExpression)
		{
			this._innerExpression = innerExpression;
			base.RawValue = innerExpression.RawValue;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000496A File Offset: 0x00002B6A
		public FieldExpression(TextExpression innerExpression, TextExpression part2) : this(innerExpression)
		{
			this.part2 = part2;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000497A File Offset: 0x00002B7A
		internal override string EvaluateString(TextProcessingContext context, TextObject parent)
		{
			return "";
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004981 File Offset: 0x00002B81
		internal override TokenType TokenType
		{
			get
			{
				return TokenType.FieldExpression;
			}
		}

		// Token: 0x04000047 RID: 71
		private TextExpression _innerExpression;

		// Token: 0x04000048 RID: 72
		private TextExpression part2;
	}
}
