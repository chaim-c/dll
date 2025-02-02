using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x0200010E RID: 270
	[ExcludeFromCodeCoverage]
	internal class CompositionRootTypeExtractor : ITypeExtractor
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x000141BD File Offset: 0x000123BD
		public CompositionRootTypeExtractor(ICompositionRootAttributeExtractor compositionRootAttributeExtractor)
		{
			this.compositionRootAttributeExtractor = compositionRootAttributeExtractor;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000141D0 File Offset: 0x000123D0
		public Type[] Execute(Assembly assembly)
		{
			CompositionRootTypeAttribute[] compositionRootAttributes = this.compositionRootAttributeExtractor.GetAttributes(assembly);
			bool flag = compositionRootAttributes.Length != 0;
			Type[] result;
			if (flag)
			{
				result = (from a in compositionRootAttributes
				select a.CompositionRootType).ToArray<Type>();
			}
			else
			{
				result = (from t in assembly.DefinedTypes
				where !t.IsAbstract && typeof(ICompositionRoot).GetTypeInfo().IsAssignableFrom(t)
				select t).Cast<Type>().ToArray<Type>();
			}
			return result;
		}

		// Token: 0x040001E2 RID: 482
		private readonly ICompositionRootAttributeExtractor compositionRootAttributeExtractor;
	}
}
