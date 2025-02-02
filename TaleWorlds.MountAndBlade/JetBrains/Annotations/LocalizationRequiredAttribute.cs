using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000DB RID: 219
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x0000F0E6 File Offset: 0x0000D2E6
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0000F0FD File Offset: 0x0000D2FD
		[UsedImplicitly]
		public bool Required { get; set; }

		// Token: 0x060008E9 RID: 2281 RVA: 0x0000F108 File Offset: 0x0000D308
		public override bool Equals(object obj)
		{
			LocalizationRequiredAttribute localizationRequiredAttribute = obj as LocalizationRequiredAttribute;
			return localizationRequiredAttribute != null && localizationRequiredAttribute.Required == this.Required;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0000F12F File Offset: 0x0000D32F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
