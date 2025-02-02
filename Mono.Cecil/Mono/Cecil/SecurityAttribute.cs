using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200009E RID: 158
	public sealed class SecurityAttribute : ICustomAttribute
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00017674 File Offset: 0x00015874
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0001767C File Offset: 0x0001587C
		public TypeReference AttributeType
		{
			get
			{
				return this.attribute_type;
			}
			set
			{
				this.attribute_type = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00017685 File Offset: 0x00015885
		public bool HasFields
		{
			get
			{
				return !this.fields.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00017698 File Offset: 0x00015898
		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				Collection<CustomAttributeNamedArgument> result;
				if ((result = this.fields) == null)
				{
					result = (this.fields = new Collection<CustomAttributeNamedArgument>());
				}
				return result;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000176BD File Offset: 0x000158BD
		public bool HasProperties
		{
			get
			{
				return !this.properties.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000176D0 File Offset: 0x000158D0
		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				Collection<CustomAttributeNamedArgument> result;
				if ((result = this.properties) == null)
				{
					result = (this.properties = new Collection<CustomAttributeNamedArgument>());
				}
				return result;
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000176F5 File Offset: 0x000158F5
		public SecurityAttribute(TypeReference attributeType)
		{
			this.attribute_type = attributeType;
		}

		// Token: 0x0400040E RID: 1038
		private TypeReference attribute_type;

		// Token: 0x0400040F RID: 1039
		internal Collection<CustomAttributeNamedArgument> fields;

		// Token: 0x04000410 RID: 1040
		internal Collection<CustomAttributeNamedArgument> properties;
	}
}
