using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200000C RID: 12
	public class RichTextLinkGroup
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000045B6 File Offset: 0x000027B6
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000045BE File Offset: 0x000027BE
		public string Href { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000045C7 File Offset: 0x000027C7
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000045CF File Offset: 0x000027CF
		internal int StartIndex { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000045D8 File Offset: 0x000027D8
		internal int EndIndex
		{
			get
			{
				return this.StartIndex + this._tokens.Count;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000045EC File Offset: 0x000027EC
		internal RichTextLinkGroup(int startIndex, string href)
		{
			this.Href = href;
			this.StartIndex = startIndex;
			this._tokens = new List<TextToken>();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000460D File Offset: 0x0000280D
		internal void AddToken(TextToken textToken)
		{
			this._tokens.Add(textToken);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000461B File Offset: 0x0000281B
		internal bool Contains(TextToken textToken)
		{
			return this._tokens.Contains(textToken);
		}

		// Token: 0x04000051 RID: 81
		private List<TextToken> _tokens;
	}
}
