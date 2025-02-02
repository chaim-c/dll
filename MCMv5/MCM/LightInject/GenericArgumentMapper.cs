using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000112 RID: 274
	[ExcludeFromCodeCoverage]
	internal class GenericArgumentMapper : IGenericArgumentMapper
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001488C File Offset: 0x00012A8C
		public GenericMappingResult Map(Type genericServiceType, Type openGenericImplementingType)
		{
			string[] genericParameterNames = (from t in openGenericImplementingType.GetTypeInfo().GenericTypeParameters
			select t.Name).ToArray<string>();
			Dictionary<string, Type> genericArgumentMap = GenericArgumentMapper.CreateMap(genericServiceType, openGenericImplementingType, genericParameterNames);
			return new GenericMappingResult(genericParameterNames, genericArgumentMap, genericServiceType, openGenericImplementingType);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000148E8 File Offset: 0x00012AE8
		public Type TryMakeGenericType(Type genericServiceType, Type openGenericImplementingType)
		{
			GenericMappingResult mappingResult = this.Map(genericServiceType, openGenericImplementingType);
			bool flag = !mappingResult.IsValid;
			Type result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					result = openGenericImplementingType.MakeGenericType(mappingResult.GetMappedArguments());
				}
				catch (Exception)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001493C File Offset: 0x00012B3C
		private static Dictionary<string, Type> CreateMap(Type genericServiceType, Type openGenericImplementingType, string[] genericParameterNames)
		{
			Dictionary<string, Type> genericArgumentMap = new Dictionary<string, Type>(genericParameterNames.Length);
			Type[] genericArguments = GenericArgumentMapper.GetGenericArgumentsOrParameters(genericServiceType);
			bool flag = genericArguments.Length != 0;
			Dictionary<string, Type> result;
			if (flag)
			{
				genericServiceType = genericServiceType.GetTypeInfo().GetGenericTypeDefinition();
				Type baseTypeImplementingOpenGenericServiceType = GenericArgumentMapper.GetBaseTypeImplementingGenericTypeDefinition(openGenericImplementingType, genericServiceType);
				Type[] baseTypeGenericArguments = GenericArgumentMapper.GetGenericArgumentsOrParameters(baseTypeImplementingOpenGenericServiceType);
				GenericArgumentMapper.MapGenericArguments(genericArguments, baseTypeGenericArguments, genericArgumentMap);
				result = genericArgumentMap;
			}
			else
			{
				result = genericArgumentMap;
			}
			return result;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001499C File Offset: 0x00012B9C
		private static Type[] GetGenericArgumentsOrParameters(Type type)
		{
			TypeInfo typeInfo = type.GetTypeInfo();
			bool isGenericTypeDefinition = typeInfo.IsGenericTypeDefinition;
			Type[] result;
			if (isGenericTypeDefinition)
			{
				result = typeInfo.GenericTypeParameters;
			}
			else
			{
				result = typeInfo.GenericTypeArguments;
			}
			return result;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000149D0 File Offset: 0x00012BD0
		private static void MapGenericArguments(Type[] serviceTypeGenericArguments, Type[] baseTypeGenericArguments, IDictionary<string, Type> map)
		{
			for (int index = 0; index < baseTypeGenericArguments.Length; index++)
			{
				Type baseTypeGenericArgument = baseTypeGenericArguments[index];
				Type serviceTypeGenericArgument = serviceTypeGenericArguments[index];
				bool isGenericParameter = baseTypeGenericArgument.GetTypeInfo().IsGenericParameter;
				if (isGenericParameter)
				{
					map[baseTypeGenericArgument.Name] = serviceTypeGenericArgument;
				}
				else
				{
					bool isGenericType = baseTypeGenericArgument.GetTypeInfo().IsGenericType;
					if (isGenericType)
					{
						bool isGenericType2 = serviceTypeGenericArgument.GetTypeInfo().IsGenericType;
						if (isGenericType2)
						{
							GenericArgumentMapper.MapGenericArguments(serviceTypeGenericArgument.GetTypeInfo().GenericTypeArguments, baseTypeGenericArgument.GetTypeInfo().GenericTypeArguments, map);
						}
						else
						{
							GenericArgumentMapper.MapGenericArguments(serviceTypeGenericArguments, baseTypeGenericArgument.GetTypeInfo().GenericTypeArguments, map);
						}
					}
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00014A80 File Offset: 0x00012C80
		private static Type GetBaseTypeImplementingGenericTypeDefinition(Type implementingType, Type genericTypeDefinition)
		{
			Type baseTypeImplementingGenericTypeDefinition = null;
			bool isInterface = genericTypeDefinition.GetTypeInfo().IsInterface;
			if (isInterface)
			{
				baseTypeImplementingGenericTypeDefinition = implementingType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault((Type i) => i.GetTypeInfo().IsGenericType && i.GetTypeInfo().GetGenericTypeDefinition() == genericTypeDefinition);
			}
			else
			{
				Type baseType = implementingType;
				while (!GenericArgumentMapper.ImplementsOpenGenericTypeDefinition(genericTypeDefinition, baseType) && baseType != typeof(object))
				{
					baseType = baseType.GetTypeInfo().BaseType;
				}
				bool flag = baseType != typeof(object);
				if (flag)
				{
					baseTypeImplementingGenericTypeDefinition = baseType;
				}
			}
			bool flag2 = baseTypeImplementingGenericTypeDefinition == null;
			if (flag2)
			{
				throw new InvalidOperationException("The generic type definition " + genericTypeDefinition.FullName + " not implemented by implementing type " + implementingType.FullName);
			}
			return baseTypeImplementingGenericTypeDefinition;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00014B64 File Offset: 0x00012D64
		private static bool ImplementsOpenGenericTypeDefinition(Type genericTypeDefinition, Type baseType)
		{
			return baseType.GetTypeInfo().IsGenericType && baseType.GetTypeInfo().GetGenericTypeDefinition() == genericTypeDefinition;
		}
	}
}
