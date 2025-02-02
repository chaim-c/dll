using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000BB RID: 187
	public class MethodCode
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001548E File Offset: 0x0001368E
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00015496 File Offset: 0x00013696
		public string Comment { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001549F File Offset: 0x0001369F
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x000154A7 File Offset: 0x000136A7
		public string Name { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x000154B0 File Offset: 0x000136B0
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x000154B8 File Offset: 0x000136B8
		public string MethodSignature { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x000154C1 File Offset: 0x000136C1
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x000154C9 File Offset: 0x000136C9
		public string ReturnParameter { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x000154D2 File Offset: 0x000136D2
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x000154DA File Offset: 0x000136DA
		public bool IsStatic { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000154E3 File Offset: 0x000136E3
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x000154EB File Offset: 0x000136EB
		public MethodCodeAccessModifier AccessModifier { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000154F4 File Offset: 0x000136F4
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x000154FC File Offset: 0x000136FC
		public MethodCodePolymorphismInfo PolymorphismInfo { get; set; }

		// Token: 0x060006C5 RID: 1733 RVA: 0x00015505 File Offset: 0x00013705
		public MethodCode()
		{
			this.Name = "UnnamedMethod";
			this.MethodSignature = "()";
			this.PolymorphismInfo = MethodCodePolymorphismInfo.None;
			this.ReturnParameter = "void";
			this._lines = new List<string>();
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00015540 File Offset: 0x00013740
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
			if (this.PolymorphismInfo == MethodCodePolymorphismInfo.Virtual)
			{
				text += "virtual ";
			}
			else if (this.PolymorphismInfo == MethodCodePolymorphismInfo.Override)
			{
				text += "override ";
			}
			text = string.Concat(new string[]
			{
				text,
				this.ReturnParameter,
				" ",
				this.Name,
				this.MethodSignature
			});
			if (!string.IsNullOrEmpty(this.Comment))
			{
				codeGenerationFile.AddLine(this.Comment);
			}
			codeGenerationFile.AddLine(text);
			codeGenerationFile.AddLine("{");
			foreach (string line in this._lines)
			{
				codeGenerationFile.AddLine(line);
			}
			codeGenerationFile.AddLine("}");
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001569C File Offset: 0x0001389C
		public void AddLine(string line)
		{
			this._lines.Add(line);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000156AC File Offset: 0x000138AC
		public void AddLines(IEnumerable<string> lines)
		{
			foreach (string item in lines)
			{
				this._lines.Add(item);
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000156FC File Offset: 0x000138FC
		public void AddCodeBlock(CodeBlock codeBlock)
		{
			this.AddLines(codeBlock.Lines);
		}

		// Token: 0x0400020F RID: 527
		private List<string> _lines;
	}
}
