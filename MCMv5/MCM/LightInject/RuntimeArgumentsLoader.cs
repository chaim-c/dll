using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000E2 RID: 226
	[ExcludeFromCodeCoverage]
	internal static class RuntimeArgumentsLoader
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public static object[] Load(object[] constants)
		{
			object[] arguments = constants[constants.Length - 1] as object[];
			bool flag = arguments == null;
			object[] result;
			if (flag)
			{
				result = new object[0];
			}
			else
			{
				result = arguments;
			}
			return result;
		}
	}
}
