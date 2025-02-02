using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000FF RID: 255
	[ExcludeFromCodeCoverage]
	internal class ServiceRegistration : Registration
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x000136F9 File Offset: 0x000118F9
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00013701 File Offset: 0x00011901
		public string ServiceName { get; set; } = string.Empty;

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001370A File Offset: 0x0001190A
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00013712 File Offset: 0x00011912
		public ILifetime Lifetime { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001371B File Offset: 0x0001191B
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00013723 File Offset: 0x00011923
		public object Value { get; set; }

		// Token: 0x06000624 RID: 1572 RVA: 0x0001372C File Offset: 0x0001192C
		public override int GetHashCode()
		{
			return base.ServiceType.GetHashCode() ^ this.ServiceName.GetHashCode();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00013758 File Offset: 0x00011958
		public override bool Equals(object obj)
		{
			ServiceRegistration other = obj as ServiceRegistration;
			bool flag = other == null;
			bool result2;
			if (flag)
			{
				result2 = false;
			}
			else
			{
				bool result = this.ServiceName == other.ServiceName && base.ServiceType == other.ServiceType;
				result2 = result;
			}
			return result2;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000137AC File Offset: 0x000119AC
		public override string ToString()
		{
			ILifetime lifetime = this.Lifetime;
			string lifeTime = ((lifetime != null) ? lifetime.ToString() : null) ?? "Transient";
			return string.Format("ServiceType: '{0}', ServiceName: '{1}', ImplementingType: '{2}', Lifetime: '{3}'", new object[]
			{
				base.ServiceType,
				this.ServiceName,
				this.ImplementingType,
				lifeTime
			});
		}
	}
}
