using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000B9 RID: 185
	public class CommentSection
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00015258 File Offset: 0x00013458
		public CommentSection()
		{
			this._lines = new List<string>();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001526B File Offset: 0x0001346B
		public void AddCommentLine(string line)
		{
			this._lines.Add(line);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001527C File Offset: 0x0001347C
		public void GenerateInto(CodeGenerationFile codeGenerationFile)
		{
			foreach (string str in this._lines)
			{
				codeGenerationFile.AddLine("//" + str);
			}
		}

		// Token: 0x04000201 RID: 513
		private List<string> _lines;
	}
}
