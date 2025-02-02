using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000BA RID: 186
	public class ConstructorCode
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x000152DC File Offset: 0x000134DC
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x000152E4 File Offset: 0x000134E4
		public string Name { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x000152ED File Offset: 0x000134ED
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x000152F5 File Offset: 0x000134F5
		public string MethodSignature { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x000152FE File Offset: 0x000134FE
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x00015306 File Offset: 0x00013506
		public string BaseCall { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001530F File Offset: 0x0001350F
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00015317 File Offset: 0x00013517
		public bool IsStatic { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00015320 File Offset: 0x00013520
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00015328 File Offset: 0x00013528
		public MethodCodeAccessModifier AccessModifier { get; set; }

		// Token: 0x060006B4 RID: 1716 RVA: 0x00015331 File Offset: 0x00013531
		public ConstructorCode()
		{
			this.Name = "UnassignedConstructorName";
			this.MethodSignature = "()";
			this.BaseCall = "";
			this._lines = new List<string>();
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00015368 File Offset: 0x00013568
		public void GenerateInto(CodeGenerationFile codeGenerationFile)
		{
			string text = "";
			if (this.AccessModifier == MethodCodeAccessModifier.Public)
			{
				text += "public ";
			}
			else if (this.AccessModifier == MethodCodeAccessModifier.Protected)
			{
				text += "protected ";
			}
			else if (this.AccessModifier == MethodCodeAccessModifier.Private)
			{
				text += "private ";
			}
			else if (this.AccessModifier == MethodCodeAccessModifier.Internal)
			{
				text += "internal ";
			}
			if (this.IsStatic)
			{
				text += "static ";
			}
			text = text + this.Name + this.MethodSignature;
			if (!string.IsNullOrEmpty(this.BaseCall))
			{
				text = text + " : base" + this.BaseCall;
			}
			codeGenerationFile.AddLine(text);
			codeGenerationFile.AddLine("{");
			foreach (string line in this._lines)
			{
				codeGenerationFile.AddLine(line);
			}
			codeGenerationFile.AddLine("}");
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00015480 File Offset: 0x00013680
		public void AddLine(string line)
		{
			this._lines.Add(line);
		}

		// Token: 0x04000207 RID: 519
		private List<string> _lines;
	}
}
