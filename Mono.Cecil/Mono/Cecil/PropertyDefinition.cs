using System;
using System.Text;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000D9 RID: 217
	public sealed class PropertyDefinition : PropertyReference, IMemberDefinition, ICustomAttributeProvider, IConstantProvider, IMetadataTokenProvider
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001B401 File Offset: 0x00019601
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x0001B409 File Offset: 0x00019609
		public PropertyAttributes Attributes
		{
			get
			{
				return (PropertyAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001B414 File Offset: 0x00019614
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001B463 File Offset: 0x00019663
		public bool HasThis
		{
			get
			{
				if (this.has_this != null)
				{
					return this.has_this.Value;
				}
				if (this.GetMethod != null)
				{
					return this.get_method.HasThis;
				}
				return this.SetMethod != null && this.set_method.HasThis;
			}
			set
			{
				this.has_this = new bool?(value);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001B471 File Offset: 0x00019671
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001B496 File Offset: 0x00019696
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001B4B4 File Offset: 0x000196B4
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0001B4D1 File Offset: 0x000196D1
		public MethodDefinition GetMethod
		{
			get
			{
				if (this.get_method != null)
				{
					return this.get_method;
				}
				this.InitializeMethods();
				return this.get_method;
			}
			set
			{
				this.get_method = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001B4DA File Offset: 0x000196DA
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0001B4F7 File Offset: 0x000196F7
		public MethodDefinition SetMethod
		{
			get
			{
				if (this.set_method != null)
				{
					return this.set_method;
				}
				this.InitializeMethods();
				return this.set_method;
			}
			set
			{
				this.set_method = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001B500 File Offset: 0x00019700
		public bool HasOtherMethods
		{
			get
			{
				if (this.other_methods != null)
				{
					return this.other_methods.Count > 0;
				}
				this.InitializeMethods();
				return !this.other_methods.IsNullOrEmpty<MethodDefinition>();
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001B530 File Offset: 0x00019730
		public Collection<MethodDefinition> OtherMethods
		{
			get
			{
				if (this.other_methods != null)
				{
					return this.other_methods;
				}
				this.InitializeMethods();
				if (this.other_methods != null)
				{
					return this.other_methods;
				}
				return this.other_methods = new Collection<MethodDefinition>();
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001B570 File Offset: 0x00019770
		public bool HasParameters
		{
			get
			{
				this.InitializeMethods();
				if (this.get_method != null)
				{
					return this.get_method.HasParameters;
				}
				return this.set_method != null && this.set_method.HasParameters && this.set_method.Parameters.Count > 1;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001B5C3 File Offset: 0x000197C3
		public override Collection<ParameterDefinition> Parameters
		{
			get
			{
				this.InitializeMethods();
				if (this.get_method != null)
				{
					return PropertyDefinition.MirrorParameters(this.get_method, 0);
				}
				if (this.set_method != null)
				{
					return PropertyDefinition.MirrorParameters(this.set_method, 1);
				}
				return new Collection<ParameterDefinition>();
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001B5FC File Offset: 0x000197FC
		private static Collection<ParameterDefinition> MirrorParameters(MethodDefinition method, int bound)
		{
			Collection<ParameterDefinition> collection = new Collection<ParameterDefinition>();
			if (!method.HasParameters)
			{
				return collection;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			int num = parameters.Count - bound;
			for (int i = 0; i < num; i++)
			{
				collection.Add(parameters[i]);
			}
			return collection;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001B643 File Offset: 0x00019843
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0001B667 File Offset: 0x00019867
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.Module);
				return this.constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					this.constant = Mixin.NoValue;
				}
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001B677 File Offset: 0x00019877
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0001B689 File Offset: 0x00019889
		public object Constant
		{
			get
			{
				if (!this.HasConstant)
				{
					return null;
				}
				return this.constant;
			}
			set
			{
				this.constant = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001B692 File Offset: 0x00019892
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0001B6A4 File Offset: 0x000198A4
		public bool IsSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(512);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(512, value);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0001B6BD File Offset: 0x000198BD
		// (set) Token: 0x0600085D RID: 2141 RVA: 0x0001B6CF File Offset: 0x000198CF
		public bool IsRuntimeSpecialName
		{
			get
			{
				return this.attributes.GetAttributes(1024);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1024, value);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0001B6E8 File Offset: 0x000198E8
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x0001B6FA File Offset: 0x000198FA
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(4096);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001B713 File Offset: 0x00019913
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001B720 File Offset: 0x00019920
		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001B729 File Offset: 0x00019929
		public override bool IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001B72C File Offset: 0x0001992C
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.PropertyType.ToString());
				stringBuilder.Append(' ');
				stringBuilder.Append(base.MemberFullName());
				stringBuilder.Append('(');
				if (this.HasParameters)
				{
					Collection<ParameterDefinition> parameters = this.Parameters;
					for (int i = 0; i < parameters.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(parameters[i].ParameterType.FullName);
					}
				}
				stringBuilder.Append(')');
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001B7C4 File Offset: 0x000199C4
		public PropertyDefinition(string name, PropertyAttributes attributes, TypeReference propertyType) : base(name, propertyType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Property);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001B7FC File Offset: 0x000199FC
		private void InitializeMethods()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				return;
			}
			lock (module.SyncRoot)
			{
				if (this.get_method == null && this.set_method == null)
				{
					if (module.HasImage())
					{
						module.Read<PropertyDefinition, PropertyDefinition>(this, (PropertyDefinition property, MetadataReader reader) => reader.ReadMethods(property));
					}
				}
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001B87C File Offset: 0x00019A7C
		public override PropertyDefinition Resolve()
		{
			return this;
		}

		// Token: 0x04000543 RID: 1347
		private bool? has_this;

		// Token: 0x04000544 RID: 1348
		private ushort attributes;

		// Token: 0x04000545 RID: 1349
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000546 RID: 1350
		internal MethodDefinition get_method;

		// Token: 0x04000547 RID: 1351
		internal MethodDefinition set_method;

		// Token: 0x04000548 RID: 1352
		internal Collection<MethodDefinition> other_methods;

		// Token: 0x04000549 RID: 1353
		private object constant = Mixin.NotResolved;
	}
}
