using System;

namespace Mono.Cecil
{
	// Token: 0x0200005B RID: 91
	public abstract class Resource
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C450 File Offset: 0x0000A650
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000C458 File Offset: 0x0000A658
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000C461 File Offset: 0x0000A661
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000C469 File Offset: 0x0000A669
		public ManifestResourceAttributes Attributes
		{
			get
			{
				return (ManifestResourceAttributes)this.attributes;
			}
			set
			{
				this.attributes = (uint)value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002F5 RID: 757
		public abstract ResourceType ResourceType { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000C472 File Offset: 0x0000A672
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000C481 File Offset: 0x0000A681
		public bool IsPublic
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 1U, value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000C497 File Offset: 0x0000A697
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		public bool IsPrivate
		{
			get
			{
				return this.attributes.GetMaskedAttributes(7U, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(7U, 2U, value);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		internal Resource(string name, ManifestResourceAttributes attributes)
		{
			this.name = name;
			this.attributes = (uint)attributes;
		}

		// Token: 0x04000395 RID: 917
		private string name;

		// Token: 0x04000396 RID: 918
		private uint attributes;
	}
}
