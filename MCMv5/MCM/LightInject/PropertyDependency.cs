using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000103 RID: 259
	[ExcludeFromCodeCoverage]
	internal class PropertyDependency : Dependency
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00013A8D File Offset: 0x00011C8D
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00013A95 File Offset: 0x00011C95
		public PropertyInfo Property { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00013AA0 File Offset: 0x00011CA0
		public override string Name
		{
			get
			{
				return this.Property.Name;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00013AC0 File Offset: 0x00011CC0
		public override string ToString()
		{
			return string.Format("[Target Type: {0}], [Property: {1}({2})]", this.Property.DeclaringType, this.Property.Name, this.Property.PropertyType) + ", " + base.ToString();
		}
	}
}
