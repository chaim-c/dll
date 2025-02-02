using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000B9 RID: 185
	public class MetadataResolver : IMetadataResolver
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x000188DD File Offset: 0x00016ADD
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return this.assembly_resolver;
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000188E5 File Offset: 0x00016AE5
		public MetadataResolver(IAssemblyResolver assemblyResolver)
		{
			if (assemblyResolver == null)
			{
				throw new ArgumentNullException("assemblyResolver");
			}
			this.assembly_resolver = assemblyResolver;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00018904 File Offset: 0x00016B04
		public virtual TypeDefinition Resolve(TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			type = type.GetElementType();
			IMetadataScope scope = type.Scope;
			if (scope == null)
			{
				return null;
			}
			switch (scope.MetadataScopeType)
			{
			case MetadataScopeType.AssemblyNameReference:
			{
				AssemblyDefinition assemblyDefinition = this.assembly_resolver.Resolve((AssemblyNameReference)scope);
				if (assemblyDefinition == null)
				{
					return null;
				}
				return MetadataResolver.GetType(assemblyDefinition.MainModule, type);
			}
			case MetadataScopeType.ModuleReference:
			{
				Collection<ModuleDefinition> modules = type.Module.Assembly.Modules;
				ModuleReference moduleReference = (ModuleReference)scope;
				for (int i = 0; i < modules.Count; i++)
				{
					ModuleDefinition moduleDefinition = modules[i];
					if (moduleDefinition.Name == moduleReference.Name)
					{
						return MetadataResolver.GetType(moduleDefinition, type);
					}
				}
				break;
			}
			case MetadataScopeType.ModuleDefinition:
				return MetadataResolver.GetType((ModuleDefinition)scope, type);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000189E0 File Offset: 0x00016BE0
		private static TypeDefinition GetType(ModuleDefinition module, TypeReference reference)
		{
			TypeDefinition typeDefinition = MetadataResolver.GetTypeDefinition(module, reference);
			if (typeDefinition != null)
			{
				return typeDefinition;
			}
			if (!module.HasExportedTypes)
			{
				return null;
			}
			Collection<ExportedType> exportedTypes = module.ExportedTypes;
			for (int i = 0; i < exportedTypes.Count; i++)
			{
				ExportedType exportedType = exportedTypes[i];
				if (!(exportedType.Name != reference.Name) && !(exportedType.Namespace != reference.Namespace))
				{
					return exportedType.Resolve();
				}
			}
			return null;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00018A54 File Offset: 0x00016C54
		private static TypeDefinition GetTypeDefinition(ModuleDefinition module, TypeReference type)
		{
			if (!type.IsNested)
			{
				return module.GetType(type.Namespace, type.Name);
			}
			TypeDefinition typeDefinition = type.DeclaringType.Resolve();
			if (typeDefinition == null)
			{
				return null;
			}
			return typeDefinition.GetNestedType(type.TypeFullName());
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00018A9C File Offset: 0x00016C9C
		public virtual FieldDefinition Resolve(FieldReference field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			TypeDefinition typeDefinition = this.Resolve(field.DeclaringType);
			if (typeDefinition == null)
			{
				return null;
			}
			if (!typeDefinition.HasFields)
			{
				return null;
			}
			return this.GetField(typeDefinition, field);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00018ADC File Offset: 0x00016CDC
		private FieldDefinition GetField(TypeDefinition type, FieldReference reference)
		{
			while (type != null)
			{
				FieldDefinition field = MetadataResolver.GetField(type.Fields, reference);
				if (field != null)
				{
					return field;
				}
				if (type.BaseType == null)
				{
					return null;
				}
				type = this.Resolve(type.BaseType);
			}
			return null;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00018B1C File Offset: 0x00016D1C
		private static FieldDefinition GetField(Collection<FieldDefinition> fields, FieldReference reference)
		{
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (!(fieldDefinition.Name != reference.Name) && MetadataResolver.AreSame(fieldDefinition.FieldType, reference.FieldType))
				{
					return fieldDefinition;
				}
			}
			return null;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00018B6C File Offset: 0x00016D6C
		public virtual MethodDefinition Resolve(MethodReference method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			TypeDefinition typeDefinition = this.Resolve(method.DeclaringType);
			if (typeDefinition == null)
			{
				return null;
			}
			method = method.GetElementMethod();
			if (!typeDefinition.HasMethods)
			{
				return null;
			}
			return this.GetMethod(typeDefinition, method);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00018BB4 File Offset: 0x00016DB4
		private MethodDefinition GetMethod(TypeDefinition type, MethodReference reference)
		{
			while (type != null)
			{
				MethodDefinition method = MetadataResolver.GetMethod(type.Methods, reference);
				if (method != null)
				{
					return method;
				}
				if (type.BaseType == null)
				{
					return null;
				}
				type = this.Resolve(type.BaseType);
			}
			return null;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public static MethodDefinition GetMethod(Collection<MethodDefinition> methods, MethodReference reference)
		{
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (!(methodDefinition.Name != reference.Name) && methodDefinition.HasGenericParameters == reference.HasGenericParameters && (!methodDefinition.HasGenericParameters || methodDefinition.GenericParameters.Count == reference.GenericParameters.Count) && MetadataResolver.AreSame(methodDefinition.ReturnType, reference.ReturnType) && methodDefinition.HasParameters == reference.HasParameters)
				{
					if (!methodDefinition.HasParameters && !reference.HasParameters)
					{
						return methodDefinition;
					}
					if (MetadataResolver.AreSame(methodDefinition.Parameters, reference.Parameters))
					{
						return methodDefinition;
					}
				}
			}
			return null;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00018CAC File Offset: 0x00016EAC
		private static bool AreSame(Collection<ParameterDefinition> a, Collection<ParameterDefinition> b)
		{
			int count = a.Count;
			if (count != b.Count)
			{
				return false;
			}
			if (count == 0)
			{
				return true;
			}
			for (int i = 0; i < count; i++)
			{
				if (!MetadataResolver.AreSame(a[i].ParameterType, b[i].ParameterType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00018D00 File Offset: 0x00016F00
		private static bool AreSame(TypeSpecification a, TypeSpecification b)
		{
			if (!MetadataResolver.AreSame(a.ElementType, b.ElementType))
			{
				return false;
			}
			if (a.IsGenericInstance)
			{
				return MetadataResolver.AreSame((GenericInstanceType)a, (GenericInstanceType)b);
			}
			if (a.IsRequiredModifier || a.IsOptionalModifier)
			{
				return MetadataResolver.AreSame((IModifierType)a, (IModifierType)b);
			}
			return !a.IsArray || MetadataResolver.AreSame((ArrayType)a, (ArrayType)b);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00018D79 File Offset: 0x00016F79
		private static bool AreSame(ArrayType a, ArrayType b)
		{
			return a.Rank == b.Rank;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00018D8C File Offset: 0x00016F8C
		private static bool AreSame(IModifierType a, IModifierType b)
		{
			return MetadataResolver.AreSame(a.ModifierType, b.ModifierType);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00018DA0 File Offset: 0x00016FA0
		private static bool AreSame(GenericInstanceType a, GenericInstanceType b)
		{
			if (a.GenericArguments.Count != b.GenericArguments.Count)
			{
				return false;
			}
			for (int i = 0; i < a.GenericArguments.Count; i++)
			{
				if (!MetadataResolver.AreSame(a.GenericArguments[i], b.GenericArguments[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00018DFF File Offset: 0x00016FFF
		private static bool AreSame(GenericParameter a, GenericParameter b)
		{
			return a.Position == b.Position;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00018E10 File Offset: 0x00017010
		private static bool AreSame(TypeReference a, TypeReference b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.etype != b.etype)
			{
				return false;
			}
			if (a.IsGenericParameter)
			{
				return MetadataResolver.AreSame((GenericParameter)a, (GenericParameter)b);
			}
			if (a.IsTypeSpecification())
			{
				return MetadataResolver.AreSame((TypeSpecification)a, (TypeSpecification)b);
			}
			return !(a.Name != b.Name) && !(a.Namespace != b.Namespace) && MetadataResolver.AreSame(a.DeclaringType, b.DeclaringType);
		}

		// Token: 0x04000455 RID: 1109
		private readonly IAssemblyResolver assembly_resolver;
	}
}
