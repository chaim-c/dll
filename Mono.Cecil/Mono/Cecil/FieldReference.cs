using System;

namespace Mono.Cecil
{
	// Token: 0x020000C8 RID: 200
	public class FieldReference : MemberReference
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00019C53 File Offset: 0x00017E53
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00019C5B File Offset: 0x00017E5B
		public TypeReference FieldType
		{
			get
			{
				return this.field_type;
			}
			set
			{
				this.field_type = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00019C64 File Offset: 0x00017E64
		public override string FullName
		{
			get
			{
				return this.field_type.FullName + " " + base.MemberFullName();
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00019C81 File Offset: 0x00017E81
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.field_type.ContainsGenericParameter || base.ContainsGenericParameter;
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00019C98 File Offset: 0x00017E98
		internal FieldReference()
		{
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00019CB0 File Offset: 0x00017EB0
		public FieldReference(string name, TypeReference fieldType) : base(name)
		{
			if (fieldType == null)
			{
				throw new ArgumentNullException("fieldType");
			}
			this.field_type = fieldType;
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00019CDE File Offset: 0x00017EDE
		public FieldReference(string name, TypeReference fieldType, TypeReference declaringType) : this(name, fieldType)
		{
			if (declaringType == null)
			{
				throw new ArgumentNullException("declaringType");
			}
			this.DeclaringType = declaringType;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00019D00 File Offset: 0x00017F00
		public virtual FieldDefinition Resolve()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}

		// Token: 0x040004BD RID: 1213
		private TypeReference field_type;
	}
}
