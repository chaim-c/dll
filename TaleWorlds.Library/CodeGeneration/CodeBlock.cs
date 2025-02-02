using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000BC RID: 188
	public class CodeBlock
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001570A File Offset: 0x0001390A
		public List<string> Lines
		{
			get
			{
				return this._lines;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00015712 File Offset: 0x00013912
		public CodeBlock()
		{
			this._lines = new List<string>();
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00015725 File Offset: 0x00013925
		public void AddLine(string line)
		{
			this._lines.Add(line);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00015734 File Offset: 0x00013934
		public void AddLines(IEnumerable<string> lines)
		{
			foreach (string item in lines)
			{
				this._lines.Add(item);
			}
		}

		// Token: 0x04000210 RID: 528
		private List<string> _lines;
	}
}
