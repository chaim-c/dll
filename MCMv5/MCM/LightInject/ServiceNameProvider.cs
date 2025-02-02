using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MCM.LightInject
{
	// Token: 0x02000114 RID: 276
	[ExcludeFromCodeCoverage]
	internal class ServiceNameProvider : IServiceNameProvider
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00014E28 File Offset: 0x00013028
		public string GetServiceName(Type serviceType, Type implementingType)
		{
			string implementingTypeName = implementingType.FullName;
			string serviceTypeName = serviceType.FullName;
			bool isGenericTypeDefinition = implementingType.GetTypeInfo().IsGenericTypeDefinition;
			if (isGenericTypeDefinition)
			{
				Regex regex = new Regex("((?:[a-z][a-z.]+))", RegexOptions.IgnoreCase);
				implementingTypeName = regex.Match(implementingTypeName).Groups[1].Value;
				serviceTypeName = regex.Match(serviceTypeName).Groups[1].Value;
			}
			bool flag = serviceTypeName.Split(new char[]
			{
				'.'
			}).Last<string>().Substring(1) == implementingTypeName.Split(new char[]
			{
				'.'
			}).Last<string>();
			if (flag)
			{
				implementingTypeName = string.Empty;
			}
			return implementingTypeName;
		}
	}
}
