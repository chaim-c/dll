using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E5 RID: 229
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[BaseTypeRequired(typeof(Attribute))]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0000F1A7 File Offset: 0x0000D3A7
		public BaseTypeRequiredAttribute(Type baseType)
		{
			this.BaseTypes = new Type[]
			{
				baseType
			};
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0000F1BF File Offset: 0x0000D3BF
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x0000F1C7 File Offset: 0x0000D3C7
		public Type[] BaseTypes { get; private set; }
	}
}
