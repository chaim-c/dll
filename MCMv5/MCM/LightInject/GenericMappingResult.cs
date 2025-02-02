using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000100 RID: 256
	[ExcludeFromCodeCoverage]
	internal class GenericMappingResult
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x0001381D File Offset: 0x00011A1D
		internal GenericMappingResult(string[] genericParameterNames, IDictionary<string, Type> genericArgumentMap, Type genericServiceType, Type openGenericImplementingType)
		{
			this.genericParameterNames = genericParameterNames;
			this.genericArgumentMap = genericArgumentMap;
			this.genericServiceType = genericServiceType;
			this.openGenericImplementingType = openGenericImplementingType;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00013844 File Offset: 0x00011A44
		public bool IsValid
		{
			get
			{
				bool flag = !this.genericServiceType.GetTypeInfo().IsGenericType && this.openGenericImplementingType.GetTypeInfo().ContainsGenericParameters;
				return !flag && this.genericParameterNames.All((string n) => this.genericArgumentMap.ContainsKey(n));
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001389C File Offset: 0x00011A9C
		public Type[] GetMappedArguments()
		{
			string[] missingParameters = (from n in this.genericParameterNames
			where !this.genericArgumentMap.ContainsKey(n)
			select n).ToArray<string>();
			bool flag = missingParameters.Any<string>();
			if (flag)
			{
				string missingParametersString = missingParameters.Aggregate((string current, string next) => current + "," + next);
				string message = string.Concat(new string[]
				{
					"The generic parameter(s) ",
					missingParametersString,
					" found in type ",
					this.openGenericImplementingType.FullName,
					" cannot be mapped from ",
					this.genericServiceType.FullName
				});
				throw new InvalidOperationException(message);
			}
			return (from parameterName in this.genericParameterNames
			select this.genericArgumentMap[parameterName]).ToArray<Type>();
		}

		// Token: 0x040001C4 RID: 452
		private readonly string[] genericParameterNames;

		// Token: 0x040001C5 RID: 453
		private readonly IDictionary<string, Type> genericArgumentMap;

		// Token: 0x040001C6 RID: 454
		private readonly Type genericServiceType;

		// Token: 0x040001C7 RID: 455
		private readonly Type openGenericImplementingType;
	}
}
