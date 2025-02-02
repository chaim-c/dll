using System;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000C0 RID: 192
	public class VariableCode
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00015852 File Offset: 0x00013A52
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0001585A File Offset: 0x00013A5A
		public string Name { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00015863 File Offset: 0x00013A63
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0001586B File Offset: 0x00013A6B
		public string Type { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00015874 File Offset: 0x00013A74
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001587C File Offset: 0x00013A7C
		public bool IsStatic { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00015885 File Offset: 0x00013A85
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001588D File Offset: 0x00013A8D
		public VariableCodeAccessModifier AccessModifier { get; set; }

		// Token: 0x060006DD RID: 1757 RVA: 0x00015896 File Offset: 0x00013A96
		public VariableCode()
		{
			this.Type = "System.Object";
			this.Name = "Unnamed variable";
			this.IsStatic = false;
			this.AccessModifier = VariableCodeAccessModifier.Private;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000158C4 File Offset: 0x00013AC4
		public string GenerateLine()
		{
			string text = "";
			if (this.AccessModifier == VariableCodeAccessModifier.Public)
			{
				text += "public ";
			}
			else if (this.AccessModifier == VariableCodeAccessModifier.Protected)
			{
				text += "protected ";
			}
			else if (this.AccessModifier == VariableCodeAccessModifier.Private)
			{
				text += "private ";
			}
			else if (this.AccessModifier == VariableCodeAccessModifier.Internal)
			{
				text += "internal ";
			}
			if (this.IsStatic)
			{
				text += "static ";
			}
			return string.Concat(new string[]
			{
				text,
				this.Type,
				" ",
				this.Name,
				";"
			});
		}
	}
}
