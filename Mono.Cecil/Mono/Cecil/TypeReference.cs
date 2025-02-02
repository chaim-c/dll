using System;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000053 RID: 83
	public class TypeReference : MemberReference, IGenericParameterProvider, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000BD18 File Offset: 0x00009F18
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000BD20 File Offset: 0x00009F20
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
				this.fullname = null;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000BD30 File Offset: 0x00009F30
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000BD38 File Offset: 0x00009F38
		public virtual string Namespace
		{
			get
			{
				return this.@namespace;
			}
			set
			{
				this.@namespace = value;
				this.fullname = null;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000BD48 File Offset: 0x00009F48
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000BD50 File Offset: 0x00009F50
		public virtual bool IsValueType
		{
			get
			{
				return this.value_type;
			}
			set
			{
				this.value_type = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public override ModuleDefinition Module
		{
			get
			{
				if (this.module != null)
				{
					return this.module;
				}
				TypeReference declaringType = this.DeclaringType;
				if (declaringType != null)
				{
					return declaringType.Module;
				}
				return null;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000BD8A File Offset: 0x00009F8A
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000BD8D File Offset: 0x00009F8D
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000BD90 File Offset: 0x00009F90
		GenericParameterType IGenericParameterProvider.GenericParameterType
		{
			get
			{
				return GenericParameterType.Type;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000BD93 File Offset: 0x00009F93
		public virtual bool HasGenericParameters
		{
			get
			{
				return !this.generic_parameters.IsNullOrEmpty<GenericParameter>();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (this.generic_parameters != null)
				{
					return this.generic_parameters;
				}
				return this.generic_parameters = new GenericParameterCollection(this);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		public virtual IMetadataScope Scope
		{
			get
			{
				TypeReference declaringType = this.DeclaringType;
				if (declaringType != null)
				{
					return declaringType.Scope;
				}
				return this.scope;
			}
			set
			{
				TypeReference declaringType = this.DeclaringType;
				if (declaringType != null)
				{
					declaringType.Scope = value;
					return;
				}
				this.scope = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000BE1A File Offset: 0x0000A01A
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000BE28 File Offset: 0x0000A028
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000BE30 File Offset: 0x0000A030
		public override TypeReference DeclaringType
		{
			get
			{
				return base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
				this.fullname = null;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000BE40 File Offset: 0x0000A040
		public override string FullName
		{
			get
			{
				if (this.fullname != null)
				{
					return this.fullname;
				}
				this.fullname = this.TypeFullName();
				if (this.IsNested)
				{
					this.fullname = this.DeclaringType.FullName + "/" + this.fullname;
				}
				return this.fullname;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000BE97 File Offset: 0x0000A097
		public virtual bool IsByReference
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000BE9A File Offset: 0x0000A09A
		public virtual bool IsPointer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000BE9D File Offset: 0x0000A09D
		public virtual bool IsSentinel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public virtual bool IsArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000BEA3 File Offset: 0x0000A0A3
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000BEA6 File Offset: 0x0000A0A6
		public virtual bool IsGenericInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000BEA9 File Offset: 0x0000A0A9
		public virtual bool IsRequiredModifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		public virtual bool IsOptionalModifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000BEAF File Offset: 0x0000A0AF
		public virtual bool IsPinned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		public virtual bool IsFunctionPointer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000BEB5 File Offset: 0x0000A0B5
		public virtual bool IsPrimitive
		{
			get
			{
				return this.etype.IsPrimitive();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public virtual MetadataType MetadataType
		{
			get
			{
				ElementType elementType = this.etype;
				if (elementType != ElementType.None)
				{
					return (MetadataType)this.etype;
				}
				if (!this.IsValueType)
				{
					return MetadataType.Class;
				}
				return MetadataType.ValueType;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		protected TypeReference(string @namespace, string name) : base(name)
		{
			this.@namespace = (@namespace ?? string.Empty);
			this.token = new MetadataToken(TokenType.TypeRef, 0);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000BF1A File Offset: 0x0000A11A
		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope) : this(@namespace, name)
		{
			this.module = module;
			this.scope = scope;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000BF33 File Offset: 0x0000A133
		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope, bool valueType) : this(@namespace, name, module, scope)
		{
			this.value_type = valueType;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000BF48 File Offset: 0x0000A148
		public virtual TypeReference GetElementType()
		{
			return this;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public virtual TypeDefinition Resolve()
		{
			ModuleDefinition moduleDefinition = this.Module;
			if (moduleDefinition == null)
			{
				throw new NotSupportedException();
			}
			return moduleDefinition.Resolve(this);
		}

		// Token: 0x0400037B RID: 891
		private string @namespace;

		// Token: 0x0400037C RID: 892
		private bool value_type;

		// Token: 0x0400037D RID: 893
		internal IMetadataScope scope;

		// Token: 0x0400037E RID: 894
		internal ModuleDefinition module;

		// Token: 0x0400037F RID: 895
		internal ElementType etype;

		// Token: 0x04000380 RID: 896
		private string fullname;

		// Token: 0x04000381 RID: 897
		protected Collection<GenericParameter> generic_parameters;
	}
}
