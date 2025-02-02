using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.Library.CodeGeneration;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000067 RID: 103
	internal class SaveCodeGenerationContextAssembly
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		public Assembly Assembly { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000C7C1 File Offset: 0x0000A9C1
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000C7C9 File Offset: 0x0000A9C9
		public string Location { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000C7DA File Offset: 0x0000A9DA
		public string FileName { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000C7E3 File Offset: 0x0000A9E3
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000C7EB File Offset: 0x0000A9EB
		public string DefaultNamespace { get; private set; }

		// Token: 0x0600030E RID: 782 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public SaveCodeGenerationContextAssembly(DefinitionContext definitionContext, Assembly assembly, string defaultNamespace, string location, string fileName)
		{
			this.Assembly = assembly;
			this.Location = location;
			this.FileName = fileName;
			this.DefaultNamespace = defaultNamespace;
			this._definitionContext = definitionContext;
			this._definitions = new List<TypeDefinition>();
			this._structDefinitions = new List<TypeDefinition>();
			this._containerDefinitions = new List<ContainerDefinition>();
			this._codeGenerationContext = new CodeGenerationContext();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C858 File Offset: 0x0000AA58
		public void AddClassDefinition(TypeDefinition classDefinition)
		{
			this._definitions.Add(classDefinition);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000C866 File Offset: 0x0000AA66
		public void AddStructDefinition(TypeDefinition classDefinition)
		{
			this._structDefinitions.Add(classDefinition);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C874 File Offset: 0x0000AA74
		public bool CheckIfGotAnyNonPrimitiveMembers(TypeDefinition typeDefinition)
		{
			MemberDefinition[] array = typeDefinition.MemberDefinitions.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				Type memberType = array[i].GetMemberType();
				if (memberType.IsClass && memberType != typeof(string))
				{
					return true;
				}
				if (!(this._definitionContext.GetTypeDefinition(memberType) is BasicTypeDefinition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000C8D6 File Offset: 0x0000AAD6
		private string[] GetNestedClasses(string fullClassName)
		{
			return fullClassName.Split(new char[]
			{
				'.'
			}, StringSplitOptions.None);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		private bool CheckIfBaseTypeDefind(Type type)
		{
			Type baseType = type.BaseType;
			TypeDefinitionBase typeDefinitionBase = null;
			while (baseType != null && baseType != typeof(object))
			{
				Type type2 = baseType;
				if (baseType.IsGenericType && !baseType.IsGenericTypeDefinition)
				{
					type2 = baseType.GetGenericTypeDefinition();
				}
				typeDefinitionBase = this._definitionContext.GetTypeDefinition(type2);
				if (typeDefinitionBase != null)
				{
					break;
				}
				baseType = baseType.BaseType;
			}
			return typeDefinitionBase != null && !(typeDefinitionBase is BasicTypeDefinition);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C964 File Offset: 0x0000AB64
		private bool CheckIfTypeDefined(Type type)
		{
			Type type2 = type;
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				type2 = type.GetGenericTypeDefinition();
			}
			TypeDefinitionBase typeDefinition = this._definitionContext.GetTypeDefinition(type2);
			return typeDefinition != null && !(typeDefinition is BasicTypeDefinition);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C9AB File Offset: 0x0000ABAB
		private static bool IsBuitlinTypeByDotNet(Type type)
		{
			return type.FullName.StartsWith("System.");
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C9C4 File Offset: 0x0000ABC4
		private bool CheckIfPrimitiveOrPrimiteHolderStruct(Type type)
		{
			bool flag = false;
			TypeDefinitionBase typeDefinition = this._definitionContext.GetTypeDefinition(type);
			if (typeDefinition is BasicTypeDefinition)
			{
				flag = true;
			}
			else if (typeDefinition is EnumDefinition)
			{
				flag = true;
			}
			if (!flag && typeDefinition is TypeDefinition)
			{
				TypeDefinition typeDefinition2 = (TypeDefinition)typeDefinition;
				if (!typeDefinition2.IsClassDefinition && !this.CheckIfGotAnyNonPrimitiveMembers(typeDefinition2))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		private int GetClassGenericInformation(string className)
		{
			if (className.Length > 2 && className[className.Length - 2] == '`')
			{
				return Convert.ToInt32(className[className.Length - 1].ToString());
			}
			return -1;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000CA64 File Offset: 0x0000AC64
		private void GenerateForClassOrStruct(TypeDefinition typeDefinition)
		{
			Type type = typeDefinition.Type;
			bool isClass = type.IsClass;
			bool flag = !isClass;
			bool flag2 = this.CheckIfBaseTypeDefind(type);
			string @namespace = type.Namespace;
			string text = type.FullName.Replace('+', '.');
			string text2 = text.Substring(@namespace.Length + 1, text.Length - @namespace.Length - 1);
			text2 = text2.Replace('+', '.');
			string[] nestedClasses = this.GetNestedClasses(text2);
			NamespaceCode namespaceCode = this._codeGenerationContext.FindOrCreateNamespace(@namespace);
			string str = nestedClasses[nestedClasses.Length - 1];
			ClassCode classCode = null;
			for (int i = 0; i < nestedClasses.Length; i++)
			{
				string text3 = nestedClasses[i];
				ClassCode classCode2 = new ClassCode();
				classCode2.IsPartial = true;
				if (i + 1 == nestedClasses.Length)
				{
					classCode2.IsClass = isClass;
				}
				classCode2.AccessModifier = ClassCodeAccessModifier.DoNotMention;
				int classGenericInformation = this.GetClassGenericInformation(text3);
				if (classGenericInformation >= 0)
				{
					classCode2.IsGeneric = true;
					classCode2.GenericTypeCount = classGenericInformation;
					text3 = text3.Substring(0, text3.Length - 2);
				}
				classCode2.Name = text3;
				if (classCode != null)
				{
					classCode.AddNestedClass(classCode2);
				}
				else
				{
					namespaceCode.AddClass(classCode2);
				}
				classCode = classCode2;
			}
			TypeSaveId typeSaveId = (TypeSaveId)typeDefinition.SaveId;
			int delegateCount = this._delegateCount;
			this._delegateCount++;
			this._managerMethod.AddLine(string.Concat(new object[]
			{
				"var typeDefinition",
				delegateCount,
				" =  (global::TaleWorlds.SaveSystem.Definition.TypeDefinition)definitionContext.TryGetTypeDefinition(new global::TaleWorlds.SaveSystem.Definition.TypeSaveId(",
				typeSaveId.Id,
				"));"
			}));
			if (!type.IsGenericTypeDefinition && !type.IsAbstract)
			{
				MethodCode methodCode = new MethodCode();
				methodCode.IsStatic = true;
				methodCode.AccessModifier = (flag ? MethodCodeAccessModifier.Public : MethodCodeAccessModifier.Internal);
				methodCode.Name = "AutoGeneratedStaticCollectObjects" + str;
				methodCode.MethodSignature = "(object o, global::System.Collections.Generic.List<object> collectedObjects)";
				methodCode.AddLine("var target = (global::" + text + ")o;");
				methodCode.AddLine("target.AutoGeneratedInstanceCollectObjects(collectedObjects);");
				classCode.AddMethod(methodCode);
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"TaleWorlds.SaveSystem.Definition.CollectObjectsDelegate d",
					delegateCount,
					" = global::",
					@namespace,
					".",
					text2,
					".",
					methodCode.Name,
					";"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"typeDefinition",
					delegateCount,
					".InitializeForAutoGeneration(d",
					delegateCount,
					");"
				}));
			}
			this._managerMethod.AddLine("");
			MethodCode methodCode2 = new MethodCode();
			if (flag2)
			{
				methodCode2.PolymorphismInfo = MethodCodePolymorphismInfo.Override;
				methodCode2.AccessModifier = MethodCodeAccessModifier.Protected;
			}
			else if (!type.IsSealed)
			{
				methodCode2.PolymorphismInfo = MethodCodePolymorphismInfo.Virtual;
				methodCode2.AccessModifier = MethodCodeAccessModifier.Protected;
			}
			else
			{
				methodCode2.PolymorphismInfo = MethodCodePolymorphismInfo.None;
				methodCode2.AccessModifier = MethodCodeAccessModifier.Private;
			}
			methodCode2.Name = "AutoGeneratedInstanceCollectObjects";
			methodCode2.MethodSignature = "(global::System.Collections.Generic.List<object> collectedObjects)";
			if (flag2)
			{
				methodCode2.AddLine("base.AutoGeneratedInstanceCollectObjects(collectedObjects);");
				methodCode2.AddLine("");
			}
			foreach (MemberDefinition memberDefinition in typeDefinition.MemberDefinitions)
			{
				if (memberDefinition is FieldDefinition)
				{
					FieldInfo fieldInfo = (memberDefinition as FieldDefinition).FieldInfo;
					Type fieldType = fieldInfo.FieldType;
					string name = fieldInfo.Name;
					if (fieldType.IsClass || fieldType.IsInterface)
					{
						if (fieldType != typeof(string))
						{
							bool flag3 = false;
							Type declaringType = fieldInfo.DeclaringType;
							if (declaringType != type)
							{
								flag3 = this.CheckIfTypeDefined(declaringType);
							}
							string text4 = "";
							if (flag3)
							{
								text4 += "//";
							}
							text4 = text4 + "collectedObjects.Add(this." + name + ");";
							methodCode2.AddLine(text4);
						}
					}
					else if (!fieldType.IsClass && this._definitionContext.GetStructDefinition(fieldType) != null)
					{
						string text5 = "";
						bool flag4 = false;
						Type declaringType2 = fieldInfo.DeclaringType;
						if (declaringType2 != type)
						{
							flag4 = this.CheckIfTypeDefined(declaringType2);
						}
						if (flag4)
						{
							text5 += "//";
						}
						string fullTypeName = SaveCodeGenerationContextAssembly.GetFullTypeName(fieldType);
						string usableTypeName = SaveCodeGenerationContextAssembly.GetUsableTypeName(fieldType);
						text5 = string.Concat(new string[]
						{
							text5,
							"global::",
							fullTypeName,
							".AutoGeneratedStaticCollectObjects",
							usableTypeName,
							"(this.",
							name,
							", collectedObjects);"
						});
						methodCode2.AddLine(text5);
					}
				}
			}
			methodCode2.AddLine("");
			foreach (MemberDefinition memberDefinition2 in typeDefinition.MemberDefinitions)
			{
				if (memberDefinition2 is PropertyDefinition)
				{
					PropertyDefinition propertyDefinition = memberDefinition2 as PropertyDefinition;
					PropertyInfo propertyInfo = propertyDefinition.PropertyInfo;
					Type propertyType = propertyDefinition.PropertyInfo.PropertyType;
					string name2 = propertyInfo.Name;
					if (propertyType.IsClass || propertyType.IsInterface)
					{
						if (propertyType != typeof(string))
						{
							bool flag5 = false;
							Type declaringType3 = propertyInfo.DeclaringType;
							if (declaringType3 != type)
							{
								flag5 = this.CheckIfTypeDefined(declaringType3);
							}
							string text6 = "";
							if (flag5)
							{
								text6 += "//";
							}
							text6 = text6 + "collectedObjects.Add(this." + name2 + ");";
							methodCode2.AddLine(text6);
						}
					}
					else if (!propertyType.IsClass && this._definitionContext.GetStructDefinition(propertyType) != null)
					{
						bool flag6 = false;
						Type declaringType4 = propertyInfo.DeclaringType;
						if (declaringType4 != type)
						{
							flag6 = this.CheckIfTypeDefined(declaringType4);
						}
						string text7 = "";
						if (flag6)
						{
							text7 += "//";
						}
						string fullTypeName2 = SaveCodeGenerationContextAssembly.GetFullTypeName(propertyType);
						string usableTypeName2 = SaveCodeGenerationContextAssembly.GetUsableTypeName(propertyType);
						text7 = string.Concat(new string[]
						{
							text7,
							"global::",
							fullTypeName2,
							".AutoGeneratedStaticCollectObjects",
							usableTypeName2,
							"(this.",
							name2,
							", collectedObjects);"
						});
						methodCode2.AddLine(text7);
					}
				}
			}
			classCode.AddMethod(methodCode2);
			foreach (MemberDefinition memberDefinition3 in typeDefinition.MemberDefinitions)
			{
				if (!type.IsGenericTypeDefinition)
				{
					MethodCode methodCode3 = new MethodCode();
					string str2 = "";
					Type type2 = null;
					if (memberDefinition3 is PropertyDefinition)
					{
						PropertyDefinition propertyDefinition2 = memberDefinition3 as PropertyDefinition;
						str2 = propertyDefinition2.PropertyInfo.Name;
						type2 = propertyDefinition2.PropertyInfo.DeclaringType;
					}
					else if (memberDefinition3 is FieldDefinition)
					{
						FieldDefinition fieldDefinition = memberDefinition3 as FieldDefinition;
						str2 = fieldDefinition.FieldInfo.Name;
						type2 = fieldDefinition.FieldInfo.DeclaringType;
					}
					bool flag7 = false;
					if (type2 != type)
					{
						flag7 = this.CheckIfTypeDefined(type2);
					}
					if (!flag7)
					{
						methodCode3.Name = "AutoGeneratedGetMemberValue" + str2;
						methodCode3.MethodSignature = "(object o)";
						methodCode3.IsStatic = true;
						methodCode3.AccessModifier = MethodCodeAccessModifier.Internal;
						methodCode3.PolymorphismInfo = MethodCodePolymorphismInfo.None;
						methodCode3.ReturnParameter = "object";
						methodCode3.AddLine("var target = (global::" + text + ")o;");
						methodCode3.AddLine("return (object)target." + str2 + ";");
						classCode.AddMethod(methodCode3);
						string text8 = "GetPropertyDefinitionWithId";
						if (memberDefinition3 is FieldDefinition)
						{
							text8 = "GetFieldDefinitionWithId";
						}
						this._managerMethod.AddLine("{");
						this._managerMethod.AddLine(string.Concat(new object[]
						{
							"var memberDefinition = typeDefinition",
							delegateCount,
							".",
							text8,
							"(new global::TaleWorlds.SaveSystem.Definition.MemberTypeId(",
							memberDefinition3.Id.TypeLevel,
							",",
							memberDefinition3.Id.LocalSaveId,
							"));"
						}));
						string str3 = string.Concat(new string[]
						{
							"global::",
							@namespace,
							".",
							text2,
							".",
							methodCode3.Name
						});
						this._managerMethod.AddLine("memberDefinition.InitializeForAutoGeneration(" + str3 + ");");
						this._managerMethod.AddLine("}");
						this._managerMethod.AddLine("");
					}
				}
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D39C File Offset: 0x0000B59C
		private static string GetFullTypeName(Type type)
		{
			if (type.IsArray)
			{
				return SaveCodeGenerationContextAssembly.GetFullTypeName(type.GetElementType()) + "[]";
			}
			if (type.IsGenericType)
			{
				string[] array = new string[type.GenericTypeArguments.Length];
				string text = "<";
				for (int i = 0; i < type.GenericTypeArguments.Length; i++)
				{
					string fullTypeName = SaveCodeGenerationContextAssembly.GetFullTypeName(type.GenericTypeArguments[i]);
					text += fullTypeName;
					array[i] = fullTypeName;
					if (i + 1 < type.GenericTypeArguments.Length)
					{
						text += ",";
					}
				}
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				text += ">";
				string text2 = genericTypeDefinition.FullName.Replace('+', '.');
				text2 = text2.Substring(0, text2.Length - 2);
				return text2 + text;
			}
			return type.FullName.Replace('+', '.');
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D479 File Offset: 0x0000B679
		private static string GetUsableTypeName(Type type)
		{
			return type.Name.Replace('+', '.');
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D48C File Offset: 0x0000B68C
		private void AddCustomCollectionByBuiltinTypes(CodeBlock codeBlock, Type elementType, string variableName)
		{
			codeBlock.AddLine("//custom code here - begins ");
			if (elementType.GetGenericTypeDefinition() == typeof(Tuple<, >))
			{
				Type elementType2 = elementType.GenericTypeArguments[0];
				Type elementType3 = elementType.GenericTypeArguments[1];
				codeBlock.AddLine("//Tuple here");
				codeBlock.AddLine("");
				codeBlock.AddLine("if (" + variableName + " != null)");
				codeBlock.AddLine("{");
				codeBlock.AddLine("collectedObjects.Add(" + variableName + ");");
				codeBlock.AddLine("");
				codeBlock.AddLine(string.Concat(new string[]
				{
					"var ",
					variableName,
					"_item1 = ",
					variableName,
					".Item1;"
				}));
				codeBlock.AddLine(string.Concat(new string[]
				{
					"var ",
					variableName,
					"_item2 = ",
					variableName,
					".Item2;"
				}));
				this.AddCodeForType(codeBlock, elementType2, variableName + "_item1");
				this.AddCodeForType(codeBlock, elementType3, variableName + "_item2");
				codeBlock.AddLine("}");
				return;
			}
			if (elementType.GetGenericTypeDefinition() == typeof(KeyValuePair<, >))
			{
				Type elementType4 = elementType.GenericTypeArguments[0];
				Type elementType5 = elementType.GenericTypeArguments[1];
				codeBlock.AddLine("//KeyValuePair here");
				codeBlock.AddLine("");
				codeBlock.AddLine(string.Concat(new string[]
				{
					"var ",
					variableName,
					"_key = ",
					variableName,
					".Key;"
				}));
				codeBlock.AddLine(string.Concat(new string[]
				{
					"var ",
					variableName,
					"_value = ",
					variableName,
					".Value;"
				}));
				this.AddCodeForType(codeBlock, elementType4, variableName + "_key");
				this.AddCodeForType(codeBlock, elementType5, variableName + "_value");
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D688 File Offset: 0x0000B888
		private void AddCodeForType(CodeBlock codeBlock, Type elementType, string elementVariableName)
		{
			bool flag = this.CheckIfPrimitiveOrPrimiteHolderStruct(elementType);
			TypeDefinition structDefinition = this._definitionContext.GetStructDefinition(elementType);
			bool flag2 = SaveCodeGenerationContextAssembly.IsBuitlinTypeByDotNet(elementType);
			if (elementType != typeof(object) && !flag)
			{
				if (flag2)
				{
					codeBlock.AddLine("//Builtin type in dot net: " + elementType);
					this.AddCustomCollectionByBuiltinTypes(codeBlock, elementType, elementVariableName);
					return;
				}
				if (structDefinition != null)
				{
					string fullTypeName = SaveCodeGenerationContextAssembly.GetFullTypeName(elementType);
					string usableTypeName = SaveCodeGenerationContextAssembly.GetUsableTypeName(elementType);
					codeBlock.AddLine(string.Concat(new string[]
					{
						"global::",
						fullTypeName,
						".AutoGeneratedStaticCollectObjects",
						usableTypeName,
						"(",
						elementVariableName,
						", collectedObjects);"
					}));
					return;
				}
				codeBlock.AddLine("collectedObjects.Add(" + elementVariableName + ");");
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D754 File Offset: 0x0000B954
		private void GenerateForList(ContainerDefinition containerDefinition)
		{
			Type type = containerDefinition.Type;
			Type type2 = type.GetGenericArguments()[0];
			bool flag = this.CheckIfPrimitiveOrPrimiteHolderStruct(type2);
			if (type2 != typeof(object))
			{
				MethodCode methodCode = new MethodCode();
				methodCode.IsStatic = true;
				methodCode.AccessModifier = MethodCodeAccessModifier.Private;
				methodCode.Comment = "//" + type.FullName;
				methodCode.Name = "AutoGeneratedStaticCollectObjectsForList" + this._containerNumber;
				methodCode.MethodSignature = "(object o, global::System.Collections.Generic.List<object> collectedObjects)";
				CodeBlock codeBlock = new CodeBlock();
				this.AddCodeForType(codeBlock, type2, "element");
				if (flag)
				{
					methodCode.AddLine("//Got no child, type: " + type.FullName);
				}
				else
				{
					string fullTypeName = SaveCodeGenerationContextAssembly.GetFullTypeName(type2);
					methodCode.AddLine("var target = (global::System.Collections.IList)o;");
					methodCode.AddLine("");
					methodCode.AddLine("for (int i = 0; i < target.Count; i++)");
					methodCode.AddLine("{");
					methodCode.AddLine("var element = (" + fullTypeName + ")target[i];");
					methodCode.AddCodeBlock(codeBlock);
					methodCode.AddLine("}");
				}
				SaveId saveId = containerDefinition.SaveId;
				StringWriter stringWriter = new StringWriter();
				saveId.WriteTo(stringWriter);
				string text = flag ? "true" : "false";
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var saveId",
					this._delegateCount,
					" = global::TaleWorlds.SaveSystem.Definition.SaveId.ReadSaveIdFrom(new global::TaleWorlds.Library.StringReader(\"",
					stringWriter.Data,
					"\"));"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var typeDefinition",
					this._delegateCount,
					" =  (global::TaleWorlds.SaveSystem.Definition.ContainerDefinition)definitionContext.TryGetTypeDefinition(saveId",
					this._delegateCount,
					");"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"TaleWorlds.SaveSystem.Definition.CollectObjectsDelegate d",
					this._delegateCount,
					" = ",
					methodCode.Name,
					";"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"typeDefinition",
					this._delegateCount,
					".InitializeForAutoGeneration(d",
					this._delegateCount,
					", ",
					text,
					");"
				}));
				this._managerMethod.AddLine("");
				this._delegateCount++;
				this._managerClass.AddMethod(methodCode);
				this._containerNumber++;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		private void GenerateForArray(ContainerDefinition containerDefinition)
		{
			Type type = containerDefinition.Type;
			Type elementType = type.GetElementType();
			bool flag = this.CheckIfPrimitiveOrPrimiteHolderStruct(elementType);
			CodeBlock codeBlock = new CodeBlock();
			this.AddCodeForType(codeBlock, elementType, "element");
			if (elementType != typeof(object))
			{
				MethodCode methodCode = new MethodCode();
				methodCode.IsStatic = true;
				methodCode.AccessModifier = MethodCodeAccessModifier.Private;
				methodCode.Comment = "//" + type.FullName;
				methodCode.Name = "AutoGeneratedStaticCollectObjectsForArray" + this._containerNumber;
				methodCode.MethodSignature = "(object o, global::System.Collections.Generic.List<object> collectedObjects)";
				if (flag)
				{
					methodCode.AddLine("//Got no child, type: " + type.FullName);
				}
				else
				{
					string fullTypeName = SaveCodeGenerationContextAssembly.GetFullTypeName(type);
					methodCode.AddLine("//Builtin type in dot net: " + type.FullName);
					methodCode.AddLine("var target = (global::" + fullTypeName + ")o;");
					methodCode.AddLine("");
					methodCode.AddLine("for (int i = 0; i < target.Length; i++)");
					methodCode.AddLine("{");
					methodCode.AddLine("var element = target[i];");
					methodCode.AddLine("");
					methodCode.AddCodeBlock(codeBlock);
					methodCode.AddLine("}");
				}
				SaveId saveId = containerDefinition.SaveId;
				StringWriter stringWriter = new StringWriter();
				saveId.WriteTo(stringWriter);
				string text = flag ? "true" : "false";
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var saveId",
					this._delegateCount,
					" = global::TaleWorlds.SaveSystem.Definition.SaveId.ReadSaveIdFrom(new global::TaleWorlds.Library.StringReader(\"",
					stringWriter.Data,
					"\"));"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var typeDefinition",
					this._delegateCount,
					" =  (global::TaleWorlds.SaveSystem.Definition.ContainerDefinition)definitionContext.TryGetTypeDefinition(saveId",
					this._delegateCount,
					");"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"TaleWorlds.SaveSystem.Definition.CollectObjectsDelegate d",
					this._delegateCount,
					" = ",
					methodCode.Name,
					";"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"typeDefinition",
					this._delegateCount,
					".InitializeForAutoGeneration(d",
					this._delegateCount,
					", ",
					text,
					");"
				}));
				this._managerMethod.AddLine("");
				this._delegateCount++;
				this._managerClass.AddMethod(methodCode);
				this._containerNumber++;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000DCCC File Offset: 0x0000BECC
		private void GenerateForQueue(ContainerDefinition containerDefinition)
		{
			Type type = containerDefinition.Type;
			Type type2 = type.GetGenericArguments()[0];
			if (this.CheckIfPrimitiveOrPrimiteHolderStruct(type2))
			{
				MethodCode methodCode = new MethodCode();
				methodCode.IsStatic = true;
				methodCode.AccessModifier = MethodCodeAccessModifier.Private;
				methodCode.Comment = "//" + type.FullName;
				methodCode.Name = "AutoGeneratedStaticCollectObjectsForQueue" + this._containerNumber;
				methodCode.MethodSignature = "(object o, global::System.Collections.Generic.List<object> collectedObjects)";
				methodCode.AddLine("//Got no child, type: " + type.FullName);
				SaveId saveId = containerDefinition.SaveId;
				StringWriter stringWriter = new StringWriter();
				saveId.WriteTo(stringWriter);
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var saveId",
					this._delegateCount,
					" = global::TaleWorlds.SaveSystem.Definition.SaveId.ReadSaveIdFrom(new global::TaleWorlds.Library.StringReader(\"",
					stringWriter.Data,
					"\"));"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var typeDefinition",
					this._delegateCount,
					" =  (global::TaleWorlds.SaveSystem.Definition.ContainerDefinition)definitionContext.TryGetTypeDefinition(saveId",
					this._delegateCount,
					");"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"TaleWorlds.SaveSystem.Definition.CollectObjectsDelegate d",
					this._delegateCount,
					" = ",
					methodCode.Name,
					";"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"typeDefinition",
					this._delegateCount,
					".InitializeForAutoGeneration(d",
					this._delegateCount,
					", true);"
				}));
				this._managerMethod.AddLine("");
				this._delegateCount++;
				this._managerClass.AddMethod(methodCode);
				this._containerNumber++;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		private void GenerateForDictionary(ContainerDefinition containerDefinition)
		{
			Type type = containerDefinition.Type;
			Type type2 = type.GetGenericArguments()[0];
			Type type3 = type.GetGenericArguments()[1];
			bool flag = this.CheckIfPrimitiveOrPrimiteHolderStruct(type2);
			bool flag2 = this.CheckIfPrimitiveOrPrimiteHolderStruct(type3);
			TypeDefinition structDefinition = this._definitionContext.GetStructDefinition(type2);
			TypeDefinition structDefinition2 = this._definitionContext.GetStructDefinition(type3);
			bool flag3 = flag && flag2;
			if ((flag || structDefinition != null) && (flag2 || structDefinition2 != null) && type2 != typeof(object) && type3 != typeof(object))
			{
				MethodCode methodCode = new MethodCode();
				methodCode.IsStatic = true;
				methodCode.AccessModifier = MethodCodeAccessModifier.Private;
				methodCode.Comment = "//" + type.FullName;
				methodCode.Name = "AutoGeneratedStaticCollectObjectsForDictionary" + this._containerNumber;
				methodCode.MethodSignature = "(object o, global::System.Collections.Generic.List<object> collectedObjects)";
				if (flag3)
				{
					methodCode.AddLine("//Got no child, type: " + type.FullName);
				}
				else
				{
					methodCode.AddLine("var target = (global::System.Collections.IDictionary)o;");
					methodCode.AddLine("");
					if (structDefinition != null)
					{
						string fullTypeName = SaveCodeGenerationContextAssembly.GetFullTypeName(type2);
						methodCode.AddLine("foreach (var key in target.Keys)");
						methodCode.AddLine("{");
						string usableTypeName = SaveCodeGenerationContextAssembly.GetUsableTypeName(type2);
						methodCode.AddLine(string.Concat(new string[]
						{
							"global::",
							fullTypeName,
							".AutoGeneratedStaticCollectObjects",
							usableTypeName,
							"(key, collectedObjects);"
						}));
						methodCode.AddLine("}");
					}
					methodCode.AddLine("");
					if (structDefinition2 != null)
					{
						string fullTypeName2 = SaveCodeGenerationContextAssembly.GetFullTypeName(type3);
						methodCode.AddLine("foreach (var value in target.Values)");
						methodCode.AddLine("{");
						string usableTypeName2 = SaveCodeGenerationContextAssembly.GetUsableTypeName(type3);
						methodCode.AddLine(string.Concat(new string[]
						{
							"global::",
							fullTypeName2,
							".AutoGeneratedStaticCollectObjects",
							usableTypeName2,
							"(value, collectedObjects);"
						}));
						methodCode.AddLine("}");
					}
				}
				SaveId saveId = containerDefinition.SaveId;
				StringWriter stringWriter = new StringWriter();
				saveId.WriteTo(stringWriter);
				string text = flag3 ? "true" : "false";
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var saveId",
					this._delegateCount,
					" = global::TaleWorlds.SaveSystem.Definition.SaveId.ReadSaveIdFrom(new global::TaleWorlds.Library.StringReader(\"",
					stringWriter.Data,
					"\"));"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"var typeDefinition",
					this._delegateCount,
					" =  (global::TaleWorlds.SaveSystem.Definition.ContainerDefinition)definitionContext.TryGetTypeDefinition(saveId",
					this._delegateCount,
					");"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"TaleWorlds.SaveSystem.Definition.CollectObjectsDelegate d",
					this._delegateCount,
					" = ",
					methodCode.Name,
					";"
				}));
				this._managerMethod.AddLine(string.Concat(new object[]
				{
					"typeDefinition",
					this._delegateCount,
					".InitializeForAutoGeneration(d",
					this._delegateCount,
					", ",
					text,
					");"
				}));
				this._managerMethod.AddLine("");
				this._delegateCount++;
				this._managerClass.AddMethod(methodCode);
				this._containerNumber++;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000E258 File Offset: 0x0000C458
		public void Generate()
		{
			NamespaceCode namespaceCode = this._codeGenerationContext.FindOrCreateNamespace(this.DefaultNamespace);
			ClassCode classCode = new ClassCode();
			classCode.AccessModifier = ClassCodeAccessModifier.Internal;
			classCode.Name = "AutoGeneratedSaveManager";
			classCode.AddInterface("global::TaleWorlds.SaveSystem.Definition.IAutoGeneratedSaveManager");
			MethodCode methodCode = new MethodCode();
			methodCode.IsStatic = false;
			methodCode.AccessModifier = MethodCodeAccessModifier.Public;
			methodCode.Name = "Initialize";
			methodCode.MethodSignature = "(global::TaleWorlds.SaveSystem.Definition.DefinitionContext definitionContext)";
			classCode.AddMethod(methodCode);
			this._managerMethod = methodCode;
			this._managerClass = classCode;
			namespaceCode.AddClass(classCode);
			foreach (TypeDefinition typeDefinition in this._definitions)
			{
				this.GenerateForClassOrStruct(typeDefinition);
			}
			foreach (TypeDefinition typeDefinition2 in this._structDefinitions)
			{
				this.GenerateForClassOrStruct(typeDefinition2);
			}
			foreach (ContainerDefinition containerDefinition in this._containerDefinitions)
			{
				ContainerType containerType;
				if (containerDefinition.Type.IsContainer(out containerType))
				{
					switch (containerType)
					{
					case ContainerType.List:
					case ContainerType.CustomList:
					case ContainerType.CustomReadOnlyList:
						this.GenerateForList(containerDefinition);
						break;
					case ContainerType.Dictionary:
						this.GenerateForDictionary(containerDefinition);
						break;
					case ContainerType.Array:
						this.GenerateForArray(containerDefinition);
						break;
					case ContainerType.Queue:
						this.GenerateForQueue(containerDefinition);
						break;
					}
				}
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000E400 File Offset: 0x0000C600
		public string GenerateText()
		{
			CodeGenerationFile codeGenerationFile = new CodeGenerationFile(null);
			this._codeGenerationContext.GenerateInto(codeGenerationFile);
			return codeGenerationFile.GenerateText();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000E426 File Offset: 0x0000C626
		internal void AddContainerDefinition(ContainerDefinition containerDefinition)
		{
			this._containerDefinitions.Add(containerDefinition);
		}

		// Token: 0x040000F6 RID: 246
		private List<TypeDefinition> _definitions;

		// Token: 0x040000F7 RID: 247
		private List<TypeDefinition> _structDefinitions;

		// Token: 0x040000F8 RID: 248
		private List<ContainerDefinition> _containerDefinitions;

		// Token: 0x040000F9 RID: 249
		private CodeGenerationContext _codeGenerationContext;

		// Token: 0x040000FA RID: 250
		private DefinitionContext _definitionContext;

		// Token: 0x040000FB RID: 251
		private ClassCode _managerClass;

		// Token: 0x040000FC RID: 252
		private MethodCode _managerMethod;

		// Token: 0x040000FD RID: 253
		private int _delegateCount;

		// Token: 0x040000FE RID: 254
		private int _containerNumber;
	}
}
