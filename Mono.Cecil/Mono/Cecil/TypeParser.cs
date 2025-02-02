using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000096 RID: 150
	internal class TypeParser
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x00015641 File Offset: 0x00013841
		private TypeParser(string fullname)
		{
			this.fullname = fullname;
			this.length = fullname.Length;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001565C File Offset: 0x0001385C
		private TypeParser.Type ParseType(bool fq_name)
		{
			TypeParser.Type type = new TypeParser.Type();
			type.type_fullname = this.ParsePart();
			type.nested_names = this.ParseNestedNames();
			if (TypeParser.TryGetArity(type))
			{
				type.generic_arguments = this.ParseGenericArguments(type.arity);
			}
			type.specs = this.ParseSpecs();
			if (fq_name)
			{
				type.assembly = this.ParseAssemblyName();
			}
			return type;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000156C0 File Offset: 0x000138C0
		private static bool TryGetArity(TypeParser.Type type)
		{
			int num = 0;
			TypeParser.TryAddArity(type.type_fullname, ref num);
			string[] nested_names = type.nested_names;
			if (!nested_names.IsNullOrEmpty<string>())
			{
				for (int i = 0; i < nested_names.Length; i++)
				{
					TypeParser.TryAddArity(nested_names[i], ref num);
				}
			}
			type.arity = num;
			return num > 0;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00015710 File Offset: 0x00013910
		private static bool TryGetArity(string name, out int arity)
		{
			arity = 0;
			int num = name.LastIndexOf('`');
			return num != -1 && TypeParser.ParseInt32(name.Substring(num + 1), out arity);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001573E File Offset: 0x0001393E
		private static bool ParseInt32(string value, out int result)
		{
			return int.TryParse(value, out result);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015748 File Offset: 0x00013948
		private static void TryAddArity(string name, ref int arity)
		{
			int num;
			if (!TypeParser.TryGetArity(name, out num))
			{
				return;
			}
			arity += num;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00015768 File Offset: 0x00013968
		private string ParsePart()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.position < this.length && !TypeParser.IsDelimiter(this.fullname[this.position]))
			{
				if (this.fullname[this.position] == '\\')
				{
					this.position++;
				}
				stringBuilder.Append(this.fullname[this.position++]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000157EF File Offset: 0x000139EF
		private static bool IsDelimiter(char chr)
		{
			return "+,[]*&".IndexOf(chr) != -1;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00015802 File Offset: 0x00013A02
		private void TryParseWhiteSpace()
		{
			while (this.position < this.length && char.IsWhiteSpace(this.fullname[this.position]))
			{
				this.position++;
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001583C File Offset: 0x00013A3C
		private string[] ParseNestedNames()
		{
			string[] result = null;
			while (this.TryParse('+'))
			{
				TypeParser.Add<string>(ref result, this.ParsePart());
			}
			return result;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00015865 File Offset: 0x00013A65
		private bool TryParse(char chr)
		{
			if (this.position < this.length && this.fullname[this.position] == chr)
			{
				this.position++;
				return true;
			}
			return false;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001589C File Offset: 0x00013A9C
		private static void Add<T>(ref T[] array, T item)
		{
			if (array == null)
			{
				array = new T[]
				{
					item
				};
				return;
			}
			array = array.Resize(array.Length + 1);
			array[array.Length - 1] = item;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000158E0 File Offset: 0x00013AE0
		private int[] ParseSpecs()
		{
			int[] result = null;
			while (this.position < this.length)
			{
				char c = this.fullname[this.position];
				if (c != '&')
				{
					if (c != '*')
					{
						if (c != '[')
						{
							return result;
						}
						this.position++;
						char c2 = this.fullname[this.position];
						if (c2 != '*')
						{
							if (c2 == ']')
							{
								this.position++;
								TypeParser.Add<int>(ref result, -3);
							}
							else
							{
								int num = 1;
								while (this.TryParse(','))
								{
									num++;
								}
								TypeParser.Add<int>(ref result, num);
								this.TryParse(']');
							}
						}
						else
						{
							this.position++;
							TypeParser.Add<int>(ref result, 1);
						}
					}
					else
					{
						this.position++;
						TypeParser.Add<int>(ref result, -1);
					}
				}
				else
				{
					this.position++;
					TypeParser.Add<int>(ref result, -2);
				}
			}
			return result;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000159E8 File Offset: 0x00013BE8
		private TypeParser.Type[] ParseGenericArguments(int arity)
		{
			TypeParser.Type[] result = null;
			if (this.position == this.length || this.fullname[this.position] != '[')
			{
				return result;
			}
			this.TryParse('[');
			for (int i = 0; i < arity; i++)
			{
				bool flag = this.TryParse('[');
				TypeParser.Add<TypeParser.Type>(ref result, this.ParseType(flag));
				if (flag)
				{
					this.TryParse(']');
				}
				this.TryParse(',');
				this.TryParseWhiteSpace();
			}
			this.TryParse(']');
			return result;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00015A70 File Offset: 0x00013C70
		private string ParseAssemblyName()
		{
			if (!this.TryParse(','))
			{
				return string.Empty;
			}
			this.TryParseWhiteSpace();
			int num = this.position;
			while (this.position < this.length)
			{
				char c = this.fullname[this.position];
				if (c == '[' || c == ']')
				{
					break;
				}
				this.position++;
			}
			return this.fullname.Substring(num, this.position - num);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015AE8 File Offset: 0x00013CE8
		public static TypeReference ParseType(ModuleDefinition module, string fullname)
		{
			if (string.IsNullOrEmpty(fullname))
			{
				return null;
			}
			TypeParser typeParser = new TypeParser(fullname);
			return TypeParser.GetTypeReference(module, typeParser.ParseType(true));
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00015B14 File Offset: 0x00013D14
		private static TypeReference GetTypeReference(ModuleDefinition module, TypeParser.Type type_info)
		{
			TypeReference type;
			if (!TypeParser.TryGetDefinition(module, type_info, out type))
			{
				type = TypeParser.CreateReference(type_info, module, TypeParser.GetMetadataScope(module, type_info));
			}
			return TypeParser.CreateSpecs(type, type_info);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00015B44 File Offset: 0x00013D44
		private static TypeReference CreateSpecs(TypeReference type, TypeParser.Type type_info)
		{
			type = TypeParser.TryCreateGenericInstanceType(type, type_info);
			int[] specs = type_info.specs;
			if (specs.IsNullOrEmpty<int>())
			{
				return type;
			}
			for (int i = 0; i < specs.Length; i++)
			{
				switch (specs[i])
				{
				case -3:
					type = new ArrayType(type);
					break;
				case -2:
					type = new ByReferenceType(type);
					break;
				case -1:
					type = new PointerType(type);
					break;
				default:
				{
					ArrayType arrayType = new ArrayType(type);
					arrayType.Dimensions.Clear();
					for (int j = 0; j < specs[i]; j++)
					{
						arrayType.Dimensions.Add(default(ArrayDimension));
					}
					type = arrayType;
					break;
				}
				}
			}
			return type;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015BF0 File Offset: 0x00013DF0
		private static TypeReference TryCreateGenericInstanceType(TypeReference type, TypeParser.Type type_info)
		{
			TypeParser.Type[] generic_arguments = type_info.generic_arguments;
			if (generic_arguments.IsNullOrEmpty<TypeParser.Type>())
			{
				return type;
			}
			GenericInstanceType genericInstanceType = new GenericInstanceType(type);
			Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
			for (int i = 0; i < generic_arguments.Length; i++)
			{
				genericArguments.Add(TypeParser.GetTypeReference(type.Module, generic_arguments[i]));
			}
			return genericInstanceType;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015C40 File Offset: 0x00013E40
		public static void SplitFullName(string fullname, out string @namespace, out string name)
		{
			int num = fullname.LastIndexOf('.');
			if (num == -1)
			{
				@namespace = string.Empty;
				name = fullname;
				return;
			}
			@namespace = fullname.Substring(0, num);
			name = fullname.Substring(num + 1);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00015C7C File Offset: 0x00013E7C
		private static TypeReference CreateReference(TypeParser.Type type_info, ModuleDefinition module, IMetadataScope scope)
		{
			string @namespace;
			string name;
			TypeParser.SplitFullName(type_info.type_fullname, out @namespace, out name);
			TypeReference typeReference = new TypeReference(@namespace, name, module, scope);
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference);
			TypeParser.AdjustGenericParameters(typeReference);
			string[] nested_names = type_info.nested_names;
			if (nested_names.IsNullOrEmpty<string>())
			{
				return typeReference;
			}
			for (int i = 0; i < nested_names.Length; i++)
			{
				typeReference = new TypeReference(string.Empty, nested_names[i], module, null)
				{
					DeclaringType = typeReference
				};
				TypeParser.AdjustGenericParameters(typeReference);
			}
			return typeReference;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015CF8 File Offset: 0x00013EF8
		private static void AdjustGenericParameters(TypeReference type)
		{
			int num;
			if (!TypeParser.TryGetArity(type.Name, out num))
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				type.GenericParameters.Add(new GenericParameter(type));
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015D32 File Offset: 0x00013F32
		private static IMetadataScope GetMetadataScope(ModuleDefinition module, TypeParser.Type type_info)
		{
			if (string.IsNullOrEmpty(type_info.assembly))
			{
				return module.TypeSystem.Corlib;
			}
			return TypeParser.MatchReference(module, AssemblyNameReference.Parse(type_info.assembly));
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00015D60 File Offset: 0x00013F60
		private static AssemblyNameReference MatchReference(ModuleDefinition module, AssemblyNameReference pattern)
		{
			Collection<AssemblyNameReference> assemblyReferences = module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				if (assemblyNameReference.FullName == pattern.FullName)
				{
					return assemblyNameReference;
				}
			}
			return pattern;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00015DA4 File Offset: 0x00013FA4
		private static bool TryGetDefinition(ModuleDefinition module, TypeParser.Type type_info, out TypeReference type)
		{
			type = null;
			if (!TypeParser.TryCurrentModule(module, type_info))
			{
				return false;
			}
			TypeDefinition typeDefinition = module.GetType(type_info.type_fullname);
			if (typeDefinition == null)
			{
				return false;
			}
			string[] nested_names = type_info.nested_names;
			if (!nested_names.IsNullOrEmpty<string>())
			{
				for (int i = 0; i < nested_names.Length; i++)
				{
					typeDefinition = typeDefinition.GetNestedType(nested_names[i]);
				}
			}
			type = typeDefinition;
			return true;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00015DFC File Offset: 0x00013FFC
		private static bool TryCurrentModule(ModuleDefinition module, TypeParser.Type type_info)
		{
			return string.IsNullOrEmpty(type_info.assembly) || (module.assembly != null && module.assembly.Name.FullName == type_info.assembly);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00015E38 File Offset: 0x00014038
		public static string ToParseable(TypeReference type)
		{
			if (type == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			TypeParser.AppendType(type, stringBuilder, true, true);
			return stringBuilder.ToString();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00015E60 File Offset: 0x00014060
		private static void AppendNamePart(string part, StringBuilder name)
		{
			foreach (char c in part)
			{
				if (TypeParser.IsDelimiter(c))
				{
					name.Append('\\');
				}
				name.Append(c);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00015EA4 File Offset: 0x000140A4
		private static void AppendType(TypeReference type, StringBuilder name, bool fq_name, bool top_level)
		{
			TypeReference declaringType = type.DeclaringType;
			if (declaringType != null)
			{
				TypeParser.AppendType(declaringType, name, false, top_level);
				name.Append('+');
			}
			string @namespace = type.Namespace;
			if (!string.IsNullOrEmpty(@namespace))
			{
				TypeParser.AppendNamePart(@namespace, name);
				name.Append('.');
			}
			TypeParser.AppendNamePart(type.GetElementType().Name, name);
			if (!fq_name)
			{
				return;
			}
			if (type.IsTypeSpecification())
			{
				TypeParser.AppendTypeSpecification((TypeSpecification)type, name);
			}
			if (TypeParser.RequiresFullyQualifiedName(type, top_level))
			{
				name.Append(", ");
				name.Append(TypeParser.GetScopeFullName(type));
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00015F38 File Offset: 0x00014138
		private static string GetScopeFullName(TypeReference type)
		{
			IMetadataScope scope = type.Scope;
			switch (scope.MetadataScopeType)
			{
			case MetadataScopeType.AssemblyNameReference:
				return ((AssemblyNameReference)scope).FullName;
			case MetadataScopeType.ModuleDefinition:
				return ((ModuleDefinition)scope).Assembly.Name.FullName;
			}
			throw new ArgumentException();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00015F90 File Offset: 0x00014190
		private static void AppendTypeSpecification(TypeSpecification type, StringBuilder name)
		{
			if (type.ElementType.IsTypeSpecification())
			{
				TypeParser.AppendTypeSpecification((TypeSpecification)type.ElementType, name);
			}
			ElementType etype = type.etype;
			switch (etype)
			{
			case ElementType.Ptr:
				name.Append('*');
				return;
			case ElementType.ByRef:
				name.Append('&');
				return;
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.Var:
				return;
			case ElementType.Array:
				break;
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)type;
				Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
				name.Append('[');
				for (int i = 0; i < genericArguments.Count; i++)
				{
					if (i > 0)
					{
						name.Append(',');
					}
					TypeReference typeReference = genericArguments[i];
					bool flag = typeReference.Scope != typeReference.Module;
					if (flag)
					{
						name.Append('[');
					}
					TypeParser.AppendType(typeReference, name, true, false);
					if (flag)
					{
						name.Append(']');
					}
				}
				name.Append(']');
				return;
			}
			default:
				if (etype != ElementType.SzArray)
				{
					return;
				}
				break;
			}
			ArrayType arrayType = (ArrayType)type;
			if (arrayType.IsVector)
			{
				name.Append("[]");
				return;
			}
			name.Append('[');
			for (int j = 1; j < arrayType.Rank; j++)
			{
				name.Append(',');
			}
			name.Append(']');
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000160D3 File Offset: 0x000142D3
		private static bool RequiresFullyQualifiedName(TypeReference type, bool top_level)
		{
			return type.Scope != type.Module && (!(type.Scope.Name == "mscorlib") || !top_level);
		}

		// Token: 0x040003E4 RID: 996
		private readonly string fullname;

		// Token: 0x040003E5 RID: 997
		private readonly int length;

		// Token: 0x040003E6 RID: 998
		private int position;

		// Token: 0x02000097 RID: 151
		private class Type
		{
			// Token: 0x040003E7 RID: 999
			public const int Ptr = -1;

			// Token: 0x040003E8 RID: 1000
			public const int ByRef = -2;

			// Token: 0x040003E9 RID: 1001
			public const int SzArray = -3;

			// Token: 0x040003EA RID: 1002
			public string type_fullname;

			// Token: 0x040003EB RID: 1003
			public string[] nested_names;

			// Token: 0x040003EC RID: 1004
			public int arity;

			// Token: 0x040003ED RID: 1005
			public int[] specs;

			// Token: 0x040003EE RID: 1006
			public TypeParser.Type[] generic_arguments;

			// Token: 0x040003EF RID: 1007
			public string assembly;
		}
	}
}
