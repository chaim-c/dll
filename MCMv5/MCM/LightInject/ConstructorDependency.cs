using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x02000104 RID: 260
	[ExcludeFromCodeCoverage]
	internal class ConstructorDependency : Dependency
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00013B16 File Offset: 0x00011D16
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x00013B1E File Offset: 0x00011D1E
		public ParameterInfo Parameter { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00013B27 File Offset: 0x00011D27
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x00013B2F File Offset: 0x00011D2F
		public bool IsDecoratorTarget { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00013B38 File Offset: 0x00011D38
		public override string Name
		{
			get
			{
				return this.Parameter.Name;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00013B58 File Offset: 0x00011D58
		public override string ToString()
		{
			return string.Format("[Target Type: {0}], [Parameter: {1}({2})]", this.Parameter.Member.DeclaringType, this.Parameter.Name, this.Parameter.ParameterType) + ", " + base.ToString();
		}
	}
}
