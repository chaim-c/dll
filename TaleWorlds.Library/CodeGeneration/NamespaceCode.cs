using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000BF RID: 191
	public class NamespaceCode
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00015784 File Offset: 0x00013984
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001578C File Offset: 0x0001398C
		public string Name { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00015795 File Offset: 0x00013995
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001579D File Offset: 0x0001399D
		public List<ClassCode> Classes { get; private set; }

		// Token: 0x060006D2 RID: 1746 RVA: 0x000157A6 File Offset: 0x000139A6
		public NamespaceCode()
		{
			this.Classes = new List<ClassCode>();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000157BC File Offset: 0x000139BC
		public void GenerateInto(CodeGenerationFile codeGenerationFile)
		{
			codeGenerationFile.AddLine("namespace " + this.Name);
			codeGenerationFile.AddLine("{");
			foreach (ClassCode classCode in this.Classes)
			{
				classCode.GenerateInto(codeGenerationFile);
				codeGenerationFile.AddLine("");
			}
			codeGenerationFile.AddLine("}");
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00015844 File Offset: 0x00013A44
		public void AddClass(ClassCode clasCode)
		{
			this.Classes.Add(clasCode);
		}
	}
}
