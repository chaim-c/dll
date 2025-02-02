using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000B7 RID: 183
	public class CodeGenerationContext
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00015006 File Offset: 0x00013206
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0001500E File Offset: 0x0001320E
		public List<NamespaceCode> Namespaces { get; private set; }

		// Token: 0x060006A1 RID: 1697 RVA: 0x00015017 File Offset: 0x00013217
		public CodeGenerationContext()
		{
			this.Namespaces = new List<NamespaceCode>();
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001502C File Offset: 0x0001322C
		public NamespaceCode FindOrCreateNamespace(string name)
		{
			foreach (NamespaceCode namespaceCode in this.Namespaces)
			{
				if (namespaceCode.Name == name)
				{
					return namespaceCode;
				}
			}
			NamespaceCode namespaceCode2 = new NamespaceCode();
			namespaceCode2.Name = name;
			this.Namespaces.Add(namespaceCode2);
			return namespaceCode2;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000150A8 File Offset: 0x000132A8
		public void GenerateInto(CodeGenerationFile codeGenerationFile)
		{
			foreach (NamespaceCode namespaceCode in this.Namespaces)
			{
				namespaceCode.GenerateInto(codeGenerationFile);
				codeGenerationFile.AddLine("");
			}
		}
	}
}
