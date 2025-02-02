using System;
using System.Collections.Generic;
using System.Text;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000B8 RID: 184
	public class CodeGenerationFile
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x00015104 File Offset: 0x00013304
		public CodeGenerationFile(List<string> usingDefinitions = null)
		{
			this._lines = new List<string>();
			if (usingDefinitions != null && usingDefinitions.Count > 0)
			{
				foreach (string str in usingDefinitions)
				{
					this.AddLine("using " + str + ";");
				}
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00015180 File Offset: 0x00013380
		public void AddLine(string line)
		{
			this._lines.Add(line);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00015190 File Offset: 0x00013390
		public string GenerateText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (string text in this._lines)
			{
				if (text == "}" || text == "};")
				{
					num--;
				}
				string text2 = "";
				for (int i = 0; i < num; i++)
				{
					text2 += "\t";
				}
				text2 = text2 + text + "\n";
				if (text == "{")
				{
					num++;
				}
				stringBuilder.Append(text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000200 RID: 512
		private List<string> _lines;
	}
}
