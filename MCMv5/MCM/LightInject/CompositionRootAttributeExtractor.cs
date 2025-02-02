using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x0200010D RID: 269
	[ExcludeFromCodeCoverage]
	internal class CompositionRootAttributeExtractor : ICompositionRootAttributeExtractor
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00014188 File Offset: 0x00012388
		public CompositionRootTypeAttribute[] GetAttributes(Assembly assembly)
		{
			return assembly.GetCustomAttributes(typeof(CompositionRootTypeAttribute)).Cast<CompositionRootTypeAttribute>().ToArray<CompositionRootTypeAttribute>();
		}
	}
}
