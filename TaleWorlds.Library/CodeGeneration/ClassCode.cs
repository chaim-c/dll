using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.CodeGeneration
{
	// Token: 0x020000B5 RID: 181
	public class ClassCode
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00014BA5 File Offset: 0x00012DA5
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00014BAD File Offset: 0x00012DAD
		public string Name { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00014BB6 File Offset: 0x00012DB6
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00014BBE File Offset: 0x00012DBE
		public bool IsGeneric { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00014BC7 File Offset: 0x00012DC7
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00014BCF File Offset: 0x00012DCF
		public int GenericTypeCount { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00014BD8 File Offset: 0x00012DD8
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00014BE0 File Offset: 0x00012DE0
		public bool IsPartial { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00014BE9 File Offset: 0x00012DE9
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00014BF1 File Offset: 0x00012DF1
		public ClassCodeAccessModifier AccessModifier { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00014BFA File Offset: 0x00012DFA
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00014C02 File Offset: 0x00012E02
		public bool IsClass { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00014C0B File Offset: 0x00012E0B
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00014C13 File Offset: 0x00012E13
		public List<string> InheritedInterfaces { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00014C1C File Offset: 0x00012E1C
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00014C24 File Offset: 0x00012E24
		public List<ClassCode> NestedClasses { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00014C2D File Offset: 0x00012E2D
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x00014C35 File Offset: 0x00012E35
		public List<MethodCode> Methods { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00014C3E File Offset: 0x00012E3E
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x00014C46 File Offset: 0x00012E46
		public List<ConstructorCode> Constructors { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00014C4F File Offset: 0x00012E4F
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x00014C57 File Offset: 0x00012E57
		public List<VariableCode> Variables { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00014C60 File Offset: 0x00012E60
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x00014C68 File Offset: 0x00012E68
		public CommentSection CommentSection { get; set; }

		// Token: 0x06000698 RID: 1688 RVA: 0x00014C74 File Offset: 0x00012E74
		public ClassCode()
		{
			this.IsClass = true;
			this.IsGeneric = false;
			this.GenericTypeCount = 0;
			this.InheritedInterfaces = new List<string>();
			this.NestedClasses = new List<ClassCode>();
			this.Methods = new List<MethodCode>();
			this.Constructors = new List<ConstructorCode>();
			this.Variables = new List<VariableCode>();
			this.AccessModifier = ClassCodeAccessModifier.DoNotMention;
			this.Name = "UnnamedClass";
			this.CommentSection = null;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00014CEC File Offset: 0x00012EEC
		public void GenerateInto(CodeGenerationFile codeGenerationFile)
		{
			if (this.CommentSection != null)
			{
				this.CommentSection.GenerateInto(codeGenerationFile);
			}
			string text = "";
			if (this.AccessModifier == ClassCodeAccessModifier.Public)
			{
				text += "public ";
			}
			else if (this.AccessModifier == ClassCodeAccessModifier.Internal)
			{
				text += "internal ";
			}
			if (this.IsPartial)
			{
				text += "partial ";
			}
			string str = "class";
			if (!this.IsClass)
			{
				str = "struct";
			}
			text = text + str + " " + this.Name;
			if (this.InheritedInterfaces.Count > 0)
			{
				text += " : ";
				for (int i = 0; i < this.InheritedInterfaces.Count; i++)
				{
					string str2 = this.InheritedInterfaces[i];
					text = text + " " + str2;
					if (i + 1 != this.InheritedInterfaces.Count)
					{
						text += ", ";
					}
				}
			}
			if (this.IsGeneric)
			{
				text += "<";
				for (int j = 0; j < this.GenericTypeCount; j++)
				{
					if (this.GenericTypeCount == 1)
					{
						text += "T";
					}
					else
					{
						text = text + "T" + j;
					}
					if (j + 1 != this.GenericTypeCount)
					{
						text += ", ";
					}
				}
				text += ">";
			}
			codeGenerationFile.AddLine(text);
			codeGenerationFile.AddLine("{");
			foreach (ClassCode classCode in this.NestedClasses)
			{
				classCode.GenerateInto(codeGenerationFile);
			}
			foreach (VariableCode variableCode in this.Variables)
			{
				string line = variableCode.GenerateLine();
				codeGenerationFile.AddLine(line);
			}
			if (this.Variables.Count > 0)
			{
				codeGenerationFile.AddLine("");
			}
			foreach (ConstructorCode constructorCode in this.Constructors)
			{
				constructorCode.GenerateInto(codeGenerationFile);
				codeGenerationFile.AddLine("");
			}
			foreach (MethodCode methodCode in this.Methods)
			{
				methodCode.GenerateInto(codeGenerationFile);
				codeGenerationFile.AddLine("");
			}
			codeGenerationFile.AddLine("}");
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00014FB4 File Offset: 0x000131B4
		public void AddVariable(VariableCode variableCode)
		{
			this.Variables.Add(variableCode);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00014FC2 File Offset: 0x000131C2
		public void AddNestedClass(ClassCode clasCode)
		{
			this.NestedClasses.Add(clasCode);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00014FD0 File Offset: 0x000131D0
		public void AddMethod(MethodCode methodCode)
		{
			this.Methods.Add(methodCode);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00014FDE File Offset: 0x000131DE
		public void AddConsturctor(ConstructorCode constructorCode)
		{
			constructorCode.Name = this.Name;
			this.Constructors.Add(constructorCode);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00014FF8 File Offset: 0x000131F8
		public void AddInterface(string interfaceName)
		{
			this.InheritedInterfaces.Add(interfaceName);
		}
	}
}
