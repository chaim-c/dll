using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000135 RID: 309
	[NullableContext(1)]
	[Nullable(0)]
	internal class DependentModule : IEquatable<DependentModule>
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000199F9 File Offset: 0x00017BF9
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DependentModule);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00019A05 File Offset: 0x00017C05
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00019A0D File Offset: 0x00017C0D
		public string Id { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00019A16 File Offset: 0x00017C16
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00019A1E File Offset: 0x00017C1E
		public ApplicationVersion Version { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00019A27 File Offset: 0x00017C27
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00019A2F File Offset: 0x00017C2F
		public bool IsOptional { get; set; }

		// Token: 0x060007F3 RID: 2035 RVA: 0x00019A38 File Offset: 0x00017C38
		public DependentModule()
		{
			this.Id = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.IsOptional = false;
			base..ctor();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00019A5F File Offset: 0x00017C5F
		public DependentModule(string id, ApplicationVersion version, bool isOptional)
		{
			this.Id = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.IsOptional = false;
			base..ctor();
			this.Id = id;
			this.Version = version;
			this.IsOptional = isOptional;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00019AA0 File Offset: 0x00017CA0
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DependentModule");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00019AEC File Offset: 0x00017CEC
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Version = ");
			builder.Append(this.Version);
			builder.Append(", IsOptional = ");
			builder.Append(this.IsOptional.ToString());
			return true;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00019B58 File Offset: 0x00017D58
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DependentModule left, DependentModule right)
		{
			return !(left == right);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00019B64 File Offset: 0x00017D64
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DependentModule left, DependentModule right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00019B7C File Offset: 0x00017D7C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Version>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsOptional>k__BackingField);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00019BDE File Offset: 0x00017DDE
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DependentModule);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00019BEC File Offset: 0x00017DEC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DependentModule other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Version>k__BackingField, other.<Version>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsOptional>k__BackingField, other.<IsOptional>k__BackingField));
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00019C67 File Offset: 0x00017E67
		[CompilerGenerated]
		protected DependentModule(DependentModule original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Version = original.<Version>k__BackingField;
			this.IsOptional = original.<IsOptional>k__BackingField;
		}
	}
}
