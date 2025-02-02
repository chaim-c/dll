using System;
using System.Collections.Generic;
using System.Reflection;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200009A RID: 154
	internal class MetadataImporter
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00016269 File Offset: 0x00014469
		public MetadataImporter(ModuleDefinition module)
		{
			this.module = module;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00016278 File Offset: 0x00014478
		public TypeReference ImportType(Type type, ImportGenericContext context)
		{
			return this.ImportType(type, context, ImportGenericKind.Open);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016284 File Offset: 0x00014484
		public TypeReference ImportType(Type type, ImportGenericContext context, ImportGenericKind import_kind)
		{
			if (MetadataImporter.IsTypeSpecification(type) || MetadataImporter.ImportOpenGenericType(type, import_kind))
			{
				return this.ImportTypeSpecification(type, context);
			}
			TypeReference typeReference = new TypeReference(string.Empty, type.Name, this.module, this.ImportScope(type.Assembly), type.IsValueType);
			typeReference.etype = MetadataImporter.ImportElementType(type);
			if (MetadataImporter.IsNestedType(type))
			{
				typeReference.DeclaringType = this.ImportType(type.DeclaringType, context, import_kind);
			}
			else
			{
				typeReference.Namespace = (type.Namespace ?? string.Empty);
			}
			if (type.IsGenericType)
			{
				MetadataImporter.ImportGenericParameters(typeReference, type.GetGenericArguments());
			}
			return typeReference;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00016328 File Offset: 0x00014528
		private static bool ImportOpenGenericType(Type type, ImportGenericKind import_kind)
		{
			return type.IsGenericType && type.IsGenericTypeDefinition && import_kind == ImportGenericKind.Open;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00016340 File Offset: 0x00014540
		private static bool ImportOpenGenericMethod(MethodBase method, ImportGenericKind import_kind)
		{
			return method.IsGenericMethod && method.IsGenericMethodDefinition && import_kind == ImportGenericKind.Open;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00016358 File Offset: 0x00014558
		private static bool IsNestedType(Type type)
		{
			return type.IsNested;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00016360 File Offset: 0x00014560
		private TypeReference ImportTypeSpecification(Type type, ImportGenericContext context)
		{
			if (type.IsByRef)
			{
				return new ByReferenceType(this.ImportType(type.GetElementType(), context));
			}
			if (type.IsPointer)
			{
				return new PointerType(this.ImportType(type.GetElementType(), context));
			}
			if (type.IsArray)
			{
				return new ArrayType(this.ImportType(type.GetElementType(), context), type.GetArrayRank());
			}
			if (type.IsGenericType)
			{
				return this.ImportGenericInstance(type, context);
			}
			if (type.IsGenericParameter)
			{
				return MetadataImporter.ImportGenericParameter(type, context);
			}
			throw new NotSupportedException(type.FullName);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000163F0 File Offset: 0x000145F0
		private static TypeReference ImportGenericParameter(Type type, ImportGenericContext context)
		{
			if (context.IsEmpty)
			{
				throw new InvalidOperationException();
			}
			if (type.DeclaringMethod != null)
			{
				return context.MethodParameter(type.DeclaringMethod.Name, type.GenericParameterPosition);
			}
			if (type.DeclaringType != null)
			{
				return context.TypeParameter(MetadataImporter.NormalizedFullName(type.DeclaringType), type.GenericParameterPosition);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00016453 File Offset: 0x00014653
		private static string NormalizedFullName(Type type)
		{
			if (MetadataImporter.IsNestedType(type))
			{
				return MetadataImporter.NormalizedFullName(type.DeclaringType) + "/" + type.Name;
			}
			return type.FullName;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016480 File Offset: 0x00014680
		private TypeReference ImportGenericInstance(Type type, ImportGenericContext context)
		{
			TypeReference typeReference = this.ImportType(type.GetGenericTypeDefinition(), context, ImportGenericKind.Definition);
			GenericInstanceType genericInstanceType = new GenericInstanceType(typeReference);
			Type[] genericArguments = type.GetGenericArguments();
			Collection<TypeReference> genericArguments2 = genericInstanceType.GenericArguments;
			context.Push(typeReference);
			TypeReference result;
			try
			{
				for (int i = 0; i < genericArguments.Length; i++)
				{
					genericArguments2.Add(this.ImportType(genericArguments[i], context));
				}
				result = genericInstanceType;
			}
			finally
			{
				context.Pop();
			}
			return result;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000164FC File Offset: 0x000146FC
		private static bool IsTypeSpecification(Type type)
		{
			return type.HasElementType || MetadataImporter.IsGenericInstance(type) || type.IsGenericParameter;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00016516 File Offset: 0x00014716
		private static bool IsGenericInstance(Type type)
		{
			return type.IsGenericType && !type.IsGenericTypeDefinition;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001652C File Offset: 0x0001472C
		private static ElementType ImportElementType(Type type)
		{
			ElementType result;
			if (!MetadataImporter.type_etype_mapping.TryGetValue(type, out result))
			{
				return ElementType.None;
			}
			return result;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001654C File Offset: 0x0001474C
		private AssemblyNameReference ImportScope(Assembly assembly)
		{
			AssemblyName name = assembly.GetName();
			AssemblyNameReference assemblyNameReference;
			if (this.TryGetAssemblyNameReference(name, out assemblyNameReference))
			{
				return assemblyNameReference;
			}
			assemblyNameReference = new AssemblyNameReference(name.Name, name.Version)
			{
				Culture = name.CultureInfo.Name,
				PublicKeyToken = name.GetPublicKeyToken(),
				HashAlgorithm = (AssemblyHashAlgorithm)name.HashAlgorithm
			};
			this.module.AssemblyReferences.Add(assemblyNameReference);
			return assemblyNameReference;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000165BC File Offset: 0x000147BC
		private bool TryGetAssemblyNameReference(AssemblyName name, out AssemblyNameReference assembly_reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = this.module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				if (!(name.FullName != assemblyNameReference.FullName))
				{
					assembly_reference = assemblyNameReference;
					return true;
				}
			}
			assembly_reference = null;
			return false;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001660C File Offset: 0x0001480C
		public FieldReference ImportField(FieldInfo field, ImportGenericContext context)
		{
			TypeReference typeReference = this.ImportType(field.DeclaringType, context);
			if (MetadataImporter.IsGenericInstance(field.DeclaringType))
			{
				field = MetadataImporter.ResolveFieldDefinition(field);
			}
			context.Push(typeReference);
			FieldReference result;
			try
			{
				result = new FieldReference
				{
					Name = field.Name,
					DeclaringType = typeReference,
					FieldType = this.ImportType(field.FieldType, context)
				};
			}
			finally
			{
				context.Pop();
			}
			return result;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00016690 File Offset: 0x00014890
		private static FieldInfo ResolveFieldDefinition(FieldInfo field)
		{
			return field.Module.ResolveField(field.MetadataToken);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000166A4 File Offset: 0x000148A4
		public MethodReference ImportMethod(MethodBase method, ImportGenericContext context, ImportGenericKind import_kind)
		{
			if (MetadataImporter.IsMethodSpecification(method) || MetadataImporter.ImportOpenGenericMethod(method, import_kind))
			{
				return this.ImportMethodSpecification(method, context);
			}
			TypeReference declaringType = this.ImportType(method.DeclaringType, context);
			if (MetadataImporter.IsGenericInstance(method.DeclaringType))
			{
				method = method.Module.ResolveMethod(method.MetadataToken);
			}
			MethodReference methodReference = new MethodReference
			{
				Name = method.Name,
				HasThis = MetadataImporter.HasCallingConvention(method, CallingConventions.HasThis),
				ExplicitThis = MetadataImporter.HasCallingConvention(method, CallingConventions.ExplicitThis),
				DeclaringType = this.ImportType(method.DeclaringType, context, ImportGenericKind.Definition)
			};
			if (MetadataImporter.HasCallingConvention(method, CallingConventions.VarArgs))
			{
				MethodReference methodReference2 = methodReference;
				methodReference2.CallingConvention &= MethodCallingConvention.VarArg;
			}
			if (method.IsGenericMethod)
			{
				MetadataImporter.ImportGenericParameters(methodReference, method.GetGenericArguments());
			}
			context.Push(methodReference);
			MethodReference result;
			try
			{
				MethodInfo methodInfo = method as MethodInfo;
				methodReference.ReturnType = ((methodInfo != null) ? this.ImportType(methodInfo.ReturnType, context) : this.ImportType(typeof(void), default(ImportGenericContext)));
				ParameterInfo[] parameters = method.GetParameters();
				Collection<ParameterDefinition> parameters2 = methodReference.Parameters;
				for (int i = 0; i < parameters.Length; i++)
				{
					parameters2.Add(new ParameterDefinition(this.ImportType(parameters[i].ParameterType, context)));
				}
				methodReference.DeclaringType = declaringType;
				result = methodReference;
			}
			finally
			{
				context.Pop();
			}
			return result;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00016814 File Offset: 0x00014A14
		private static void ImportGenericParameters(IGenericParameterProvider provider, Type[] arguments)
		{
			Collection<GenericParameter> genericParameters = provider.GenericParameters;
			for (int i = 0; i < arguments.Length; i++)
			{
				genericParameters.Add(new GenericParameter(arguments[i].Name, provider));
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001684A File Offset: 0x00014A4A
		private static bool IsMethodSpecification(MethodBase method)
		{
			return method.IsGenericMethod && !method.IsGenericMethodDefinition;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00016860 File Offset: 0x00014A60
		private MethodReference ImportMethodSpecification(MethodBase method, ImportGenericContext context)
		{
			MethodInfo methodInfo = method as MethodInfo;
			if (methodInfo == null)
			{
				throw new InvalidOperationException();
			}
			MethodReference methodReference = this.ImportMethod(methodInfo.GetGenericMethodDefinition(), context, ImportGenericKind.Definition);
			GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(methodReference);
			Type[] genericArguments = method.GetGenericArguments();
			Collection<TypeReference> genericArguments2 = genericInstanceMethod.GenericArguments;
			context.Push(methodReference);
			MethodReference result;
			try
			{
				for (int i = 0; i < genericArguments.Length; i++)
				{
					genericArguments2.Add(this.ImportType(genericArguments[i], context));
				}
				result = genericInstanceMethod;
			}
			finally
			{
				context.Pop();
			}
			return result;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000168F0 File Offset: 0x00014AF0
		private static bool HasCallingConvention(MethodBase method, CallingConventions conventions)
		{
			return (method.CallingConvention & conventions) != (CallingConventions)0;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00016900 File Offset: 0x00014B00
		public TypeReference ImportType(TypeReference type, ImportGenericContext context)
		{
			if (type.IsTypeSpecification())
			{
				return this.ImportTypeSpecification(type, context);
			}
			TypeReference typeReference = new TypeReference(type.Namespace, type.Name, this.module, this.ImportScope(type.Scope), type.IsValueType);
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference);
			if (type.IsNested)
			{
				typeReference.DeclaringType = this.ImportType(type.DeclaringType, context);
			}
			if (type.HasGenericParameters)
			{
				MetadataImporter.ImportGenericParameters(typeReference, type);
			}
			return typeReference;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001697C File Offset: 0x00014B7C
		private IMetadataScope ImportScope(IMetadataScope scope)
		{
			switch (scope.MetadataScopeType)
			{
			case MetadataScopeType.AssemblyNameReference:
				return this.ImportAssemblyName((AssemblyNameReference)scope);
			case MetadataScopeType.ModuleReference:
				throw new NotImplementedException();
			case MetadataScopeType.ModuleDefinition:
				if (scope == this.module)
				{
					return scope;
				}
				return this.ImportAssemblyName(((ModuleDefinition)scope).Assembly.Name);
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000169E0 File Offset: 0x00014BE0
		private AssemblyNameReference ImportAssemblyName(AssemblyNameReference name)
		{
			AssemblyNameReference assemblyNameReference;
			if (this.TryGetAssemblyNameReference(name, out assemblyNameReference))
			{
				return assemblyNameReference;
			}
			assemblyNameReference = new AssemblyNameReference(name.Name, name.Version)
			{
				Culture = name.Culture,
				HashAlgorithm = name.HashAlgorithm,
				IsRetargetable = name.IsRetargetable
			};
			byte[] array = (!name.PublicKeyToken.IsNullOrEmpty<byte>()) ? new byte[name.PublicKeyToken.Length] : Empty<byte>.Array;
			if (array.Length > 0)
			{
				Buffer.BlockCopy(name.PublicKeyToken, 0, array, 0, array.Length);
			}
			assemblyNameReference.PublicKeyToken = array;
			this.module.AssemblyReferences.Add(assemblyNameReference);
			return assemblyNameReference;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00016A84 File Offset: 0x00014C84
		private bool TryGetAssemblyNameReference(AssemblyNameReference name_reference, out AssemblyNameReference assembly_reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = this.module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				if (!(name_reference.FullName != assemblyNameReference.FullName))
				{
					assembly_reference = assemblyNameReference;
					return true;
				}
			}
			assembly_reference = null;
			return false;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00016AD4 File Offset: 0x00014CD4
		private static void ImportGenericParameters(IGenericParameterProvider imported, IGenericParameterProvider original)
		{
			Collection<GenericParameter> genericParameters = original.GenericParameters;
			Collection<GenericParameter> genericParameters2 = imported.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				genericParameters2.Add(new GenericParameter(genericParameters[i].Name, imported));
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00016B18 File Offset: 0x00014D18
		private TypeReference ImportTypeSpecification(TypeReference type, ImportGenericContext context)
		{
			ElementType etype = type.etype;
			if (etype <= ElementType.CModOpt)
			{
				switch (etype)
				{
				case ElementType.Ptr:
				{
					PointerType pointerType = (PointerType)type;
					return new PointerType(this.ImportType(pointerType.ElementType, context));
				}
				case ElementType.ByRef:
				{
					ByReferenceType byReferenceType = (ByReferenceType)type;
					return new ByReferenceType(this.ImportType(byReferenceType.ElementType, context));
				}
				case ElementType.ValueType:
				case ElementType.Class:
					break;
				case ElementType.Var:
				{
					GenericParameter genericParameter = (GenericParameter)type;
					if (genericParameter.DeclaringType == null)
					{
						throw new InvalidOperationException();
					}
					return context.TypeParameter(genericParameter.DeclaringType.FullName, genericParameter.Position);
				}
				case ElementType.Array:
				{
					ArrayType arrayType = (ArrayType)type;
					ArrayType arrayType2 = new ArrayType(this.ImportType(arrayType.ElementType, context));
					if (arrayType.IsVector)
					{
						return arrayType2;
					}
					Collection<ArrayDimension> dimensions = arrayType.Dimensions;
					Collection<ArrayDimension> dimensions2 = arrayType2.Dimensions;
					dimensions2.Clear();
					for (int i = 0; i < dimensions.Count; i++)
					{
						ArrayDimension arrayDimension = dimensions[i];
						dimensions2.Add(new ArrayDimension(arrayDimension.LowerBound, arrayDimension.UpperBound));
					}
					return arrayType2;
				}
				case ElementType.GenericInst:
				{
					GenericInstanceType genericInstanceType = (GenericInstanceType)type;
					TypeReference type2 = this.ImportType(genericInstanceType.ElementType, context);
					GenericInstanceType genericInstanceType2 = new GenericInstanceType(type2);
					Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
					Collection<TypeReference> genericArguments2 = genericInstanceType2.GenericArguments;
					for (int j = 0; j < genericArguments.Count; j++)
					{
						genericArguments2.Add(this.ImportType(genericArguments[j], context));
					}
					return genericInstanceType2;
				}
				default:
					switch (etype)
					{
					case ElementType.SzArray:
					{
						ArrayType arrayType3 = (ArrayType)type;
						return new ArrayType(this.ImportType(arrayType3.ElementType, context));
					}
					case ElementType.MVar:
					{
						GenericParameter genericParameter2 = (GenericParameter)type;
						if (genericParameter2.DeclaringMethod == null)
						{
							throw new InvalidOperationException();
						}
						return context.MethodParameter(genericParameter2.DeclaringMethod.Name, genericParameter2.Position);
					}
					case ElementType.CModReqD:
					{
						RequiredModifierType requiredModifierType = (RequiredModifierType)type;
						return new RequiredModifierType(this.ImportType(requiredModifierType.ModifierType, context), this.ImportType(requiredModifierType.ElementType, context));
					}
					case ElementType.CModOpt:
					{
						OptionalModifierType optionalModifierType = (OptionalModifierType)type;
						return new OptionalModifierType(this.ImportType(optionalModifierType.ModifierType, context), this.ImportType(optionalModifierType.ElementType, context));
					}
					}
					break;
				}
			}
			else
			{
				if (etype == ElementType.Sentinel)
				{
					SentinelType sentinelType = (SentinelType)type;
					return new SentinelType(this.ImportType(sentinelType.ElementType, context));
				}
				if (etype == ElementType.Pinned)
				{
					PinnedType pinnedType = (PinnedType)type;
					return new PinnedType(this.ImportType(pinnedType.ElementType, context));
				}
			}
			throw new NotSupportedException(type.etype.ToString());
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00016DC0 File Offset: 0x00014FC0
		public FieldReference ImportField(FieldReference field, ImportGenericContext context)
		{
			TypeReference typeReference = this.ImportType(field.DeclaringType, context);
			context.Push(typeReference);
			FieldReference result;
			try
			{
				result = new FieldReference
				{
					Name = field.Name,
					DeclaringType = typeReference,
					FieldType = this.ImportType(field.FieldType, context)
				};
			}
			finally
			{
				context.Pop();
			}
			return result;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00016E2C File Offset: 0x0001502C
		public MethodReference ImportMethod(MethodReference method, ImportGenericContext context)
		{
			if (method.IsGenericInstance)
			{
				return this.ImportMethodSpecification(method, context);
			}
			TypeReference declaringType = this.ImportType(method.DeclaringType, context);
			MethodReference methodReference = new MethodReference
			{
				Name = method.Name,
				HasThis = method.HasThis,
				ExplicitThis = method.ExplicitThis,
				DeclaringType = declaringType,
				CallingConvention = method.CallingConvention
			};
			if (method.HasGenericParameters)
			{
				MetadataImporter.ImportGenericParameters(methodReference, method);
			}
			context.Push(methodReference);
			MethodReference result;
			try
			{
				methodReference.ReturnType = this.ImportType(method.ReturnType, context);
				if (!method.HasParameters)
				{
					result = methodReference;
				}
				else
				{
					Collection<ParameterDefinition> parameters = methodReference.Parameters;
					Collection<ParameterDefinition> parameters2 = method.Parameters;
					for (int i = 0; i < parameters2.Count; i++)
					{
						parameters.Add(new ParameterDefinition(this.ImportType(parameters2[i].ParameterType, context)));
					}
					result = methodReference;
				}
			}
			finally
			{
				context.Pop();
			}
			return result;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00016F38 File Offset: 0x00015138
		private MethodSpecification ImportMethodSpecification(MethodReference method, ImportGenericContext context)
		{
			if (!method.IsGenericInstance)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod genericInstanceMethod = (GenericInstanceMethod)method;
			MethodReference method2 = this.ImportMethod(genericInstanceMethod.ElementMethod, context);
			GenericInstanceMethod genericInstanceMethod2 = new GenericInstanceMethod(method2);
			Collection<TypeReference> genericArguments = genericInstanceMethod.GenericArguments;
			Collection<TypeReference> genericArguments2 = genericInstanceMethod2.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				genericArguments2.Add(this.ImportType(genericArguments[i], context));
			}
			return genericInstanceMethod2;
		}

		// Token: 0x040003F4 RID: 1012
		private readonly ModuleDefinition module;

		// Token: 0x040003F5 RID: 1013
		private static readonly Dictionary<Type, ElementType> type_etype_mapping = new Dictionary<Type, ElementType>(18)
		{
			{
				typeof(void),
				ElementType.Void
			},
			{
				typeof(bool),
				ElementType.Boolean
			},
			{
				typeof(char),
				ElementType.Char
			},
			{
				typeof(sbyte),
				ElementType.I1
			},
			{
				typeof(byte),
				ElementType.U1
			},
			{
				typeof(short),
				ElementType.I2
			},
			{
				typeof(ushort),
				ElementType.U2
			},
			{
				typeof(int),
				ElementType.I4
			},
			{
				typeof(uint),
				ElementType.U4
			},
			{
				typeof(long),
				ElementType.I8
			},
			{
				typeof(ulong),
				ElementType.U8
			},
			{
				typeof(float),
				ElementType.R4
			},
			{
				typeof(double),
				ElementType.R8
			},
			{
				typeof(string),
				ElementType.String
			},
			{
				typeof(TypedReference),
				ElementType.TypedByRef
			},
			{
				typeof(IntPtr),
				ElementType.I
			},
			{
				typeof(UIntPtr),
				ElementType.U
			},
			{
				typeof(object),
				ElementType.Object
			}
		};
	}
}
