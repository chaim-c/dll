using System;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000061 RID: 97
	internal sealed class ImmediateModuleReader : ModuleReader
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public ImmediateModuleReader(Image image) : base(image, ReadingMode.Immediate)
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000CBB6 File Offset: 0x0000ADB6
		protected override void ReadModule()
		{
			this.module.Read<ModuleDefinition, ModuleDefinition>(this.module, delegate(ModuleDefinition module, MetadataReader reader)
			{
				base.ReadModuleManifest(reader);
				ImmediateModuleReader.ReadModule(module);
				return module;
			});
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		public static void ReadModule(ModuleDefinition module)
		{
			if (module.HasAssemblyReferences)
			{
				ImmediateModuleReader.Read(module.AssemblyReferences);
			}
			if (module.HasResources)
			{
				ImmediateModuleReader.Read(module.Resources);
			}
			if (module.HasModuleReferences)
			{
				ImmediateModuleReader.Read(module.ModuleReferences);
			}
			if (module.HasTypes)
			{
				ImmediateModuleReader.ReadTypes(module.Types);
			}
			if (module.HasExportedTypes)
			{
				ImmediateModuleReader.Read(module.ExportedTypes);
			}
			if (module.HasCustomAttributes)
			{
				ImmediateModuleReader.Read(module.CustomAttributes);
			}
			AssemblyDefinition assembly = module.Assembly;
			if (assembly == null)
			{
				return;
			}
			if (assembly.HasCustomAttributes)
			{
				ImmediateModuleReader.ReadCustomAttributes(assembly);
			}
			if (assembly.HasSecurityDeclarations)
			{
				ImmediateModuleReader.Read(assembly.SecurityDeclarations);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000CC84 File Offset: 0x0000AE84
		private static void ReadTypes(Collection<TypeDefinition> types)
		{
			for (int i = 0; i < types.Count; i++)
			{
				ImmediateModuleReader.ReadType(types[i]);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		private static void ReadType(TypeDefinition type)
		{
			ImmediateModuleReader.ReadGenericParameters(type);
			if (type.HasInterfaces)
			{
				ImmediateModuleReader.Read(type.Interfaces);
			}
			if (type.HasNestedTypes)
			{
				ImmediateModuleReader.ReadTypes(type.NestedTypes);
			}
			if (type.HasLayoutInfo)
			{
				ImmediateModuleReader.Read(type.ClassSize);
			}
			if (type.HasFields)
			{
				ImmediateModuleReader.ReadFields(type);
			}
			if (type.HasMethods)
			{
				ImmediateModuleReader.ReadMethods(type);
			}
			if (type.HasProperties)
			{
				ImmediateModuleReader.ReadProperties(type);
			}
			if (type.HasEvents)
			{
				ImmediateModuleReader.ReadEvents(type);
			}
			ImmediateModuleReader.ReadSecurityDeclarations(type);
			ImmediateModuleReader.ReadCustomAttributes(type);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000CD48 File Offset: 0x0000AF48
		private static void ReadGenericParameters(IGenericParameterProvider provider)
		{
			if (!provider.HasGenericParameters)
			{
				return;
			}
			Collection<GenericParameter> genericParameters = provider.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				GenericParameter genericParameter = genericParameters[i];
				if (genericParameter.HasConstraints)
				{
					ImmediateModuleReader.Read(genericParameter.Constraints);
				}
				ImmediateModuleReader.ReadCustomAttributes(genericParameter);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000CD98 File Offset: 0x0000AF98
		private static void ReadSecurityDeclarations(ISecurityDeclarationProvider provider)
		{
			if (!provider.HasSecurityDeclarations)
			{
				return;
			}
			Collection<SecurityDeclaration> securityDeclarations = provider.SecurityDeclarations;
			for (int i = 0; i < securityDeclarations.Count; i++)
			{
				SecurityDeclaration securityDeclaration = securityDeclarations[i];
				ImmediateModuleReader.Read(securityDeclaration.SecurityAttributes);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		private static void ReadCustomAttributes(ICustomAttributeProvider provider)
		{
			if (!provider.HasCustomAttributes)
			{
				return;
			}
			Collection<CustomAttribute> customAttributes = provider.CustomAttributes;
			for (int i = 0; i < customAttributes.Count; i++)
			{
				CustomAttribute customAttribute = customAttributes[i];
				ImmediateModuleReader.Read(customAttribute.ConstructorArguments);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000CE20 File Offset: 0x0000B020
		private static void ReadFields(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (fieldDefinition.HasConstant)
				{
					ImmediateModuleReader.Read(fieldDefinition.Constant);
				}
				if (fieldDefinition.HasLayoutInfo)
				{
					ImmediateModuleReader.Read(fieldDefinition.Offset);
				}
				if (fieldDefinition.RVA > 0)
				{
					ImmediateModuleReader.Read(fieldDefinition.InitialValue);
				}
				if (fieldDefinition.HasMarshalInfo)
				{
					ImmediateModuleReader.Read(fieldDefinition.MarshalInfo);
				}
				ImmediateModuleReader.ReadCustomAttributes(fieldDefinition);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		private static void ReadMethods(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				ImmediateModuleReader.ReadGenericParameters(methodDefinition);
				if (methodDefinition.HasParameters)
				{
					ImmediateModuleReader.ReadParameters(methodDefinition);
				}
				if (methodDefinition.HasOverrides)
				{
					ImmediateModuleReader.Read(methodDefinition.Overrides);
				}
				if (methodDefinition.IsPInvokeImpl)
				{
					ImmediateModuleReader.Read(methodDefinition.PInvokeInfo);
				}
				ImmediateModuleReader.ReadSecurityDeclarations(methodDefinition);
				ImmediateModuleReader.ReadCustomAttributes(methodDefinition);
				MethodReturnType methodReturnType = methodDefinition.MethodReturnType;
				if (methodReturnType.HasConstant)
				{
					ImmediateModuleReader.Read(methodReturnType.Constant);
				}
				if (methodReturnType.HasMarshalInfo)
				{
					ImmediateModuleReader.Read(methodReturnType.MarshalInfo);
				}
				ImmediateModuleReader.ReadCustomAttributes(methodReturnType);
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000CF54 File Offset: 0x0000B154
		private static void ReadParameters(MethodDefinition method)
		{
			Collection<ParameterDefinition> parameters = method.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterDefinition parameterDefinition = parameters[i];
				if (parameterDefinition.HasConstant)
				{
					ImmediateModuleReader.Read(parameterDefinition.Constant);
				}
				if (parameterDefinition.HasMarshalInfo)
				{
					ImmediateModuleReader.Read(parameterDefinition.MarshalInfo);
				}
				ImmediateModuleReader.ReadCustomAttributes(parameterDefinition);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		private static void ReadProperties(TypeDefinition type)
		{
			Collection<PropertyDefinition> properties = type.Properties;
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDefinition propertyDefinition = properties[i];
				ImmediateModuleReader.Read(propertyDefinition.GetMethod);
				if (propertyDefinition.HasConstant)
				{
					ImmediateModuleReader.Read(propertyDefinition.Constant);
				}
				ImmediateModuleReader.ReadCustomAttributes(propertyDefinition);
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000D004 File Offset: 0x0000B204
		private static void ReadEvents(TypeDefinition type)
		{
			Collection<EventDefinition> events = type.Events;
			for (int i = 0; i < events.Count; i++)
			{
				EventDefinition eventDefinition = events[i];
				ImmediateModuleReader.Read(eventDefinition.AddMethod);
				ImmediateModuleReader.ReadCustomAttributes(eventDefinition);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000D042 File Offset: 0x0000B242
		private static void Read(object collection)
		{
		}
	}
}
