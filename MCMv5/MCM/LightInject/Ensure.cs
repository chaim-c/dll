using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000E3 RID: 227
	[ExcludeFromCodeCoverage]
	internal static class Ensure
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public static void IsNotNull<T>(T value, string paramName)
		{
			bool flag = value == null;
			if (flag)
			{
				throw new ArgumentNullException(paramName);
			}
		}
	}
}
