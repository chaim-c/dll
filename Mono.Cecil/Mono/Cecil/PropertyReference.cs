using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000D8 RID: 216
	public abstract class PropertyReference : MemberReference
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001B3D2 File Offset: 0x000195D2
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0001B3DA File Offset: 0x000195DA
		public TypeReference PropertyType
		{
			get
			{
				return this.property_type;
			}
			set
			{
				this.property_type = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000844 RID: 2116
		public abstract Collection<ParameterDefinition> Parameters { get; }

		// Token: 0x06000845 RID: 2117 RVA: 0x0001B3E3 File Offset: 0x000195E3
		internal PropertyReference(string name, TypeReference propertyType) : base(name)
		{
			if (propertyType == null)
			{
				throw new ArgumentNullException("propertyType");
			}
			this.property_type = propertyType;
		}

		// Token: 0x06000846 RID: 2118
		public abstract PropertyDefinition Resolve();

		// Token: 0x04000542 RID: 1346
		private TypeReference property_type;
	}
}
