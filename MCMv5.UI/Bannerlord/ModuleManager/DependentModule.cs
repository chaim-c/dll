using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	[Nullable(0)]
	internal class DependentModule : IEquatable<DependentModule>
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000BD35 File Offset: 0x00009F35
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DependentModule);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000BD41 File Offset: 0x00009F41
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000BD49 File Offset: 0x00009F49
		public string Id { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000BD52 File Offset: 0x00009F52
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000BD5A File Offset: 0x00009F5A
		public ApplicationVersion Version { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000BD63 File Offset: 0x00009F63
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000BD6B File Offset: 0x00009F6B
		public bool IsOptional { get; set; }

		// Token: 0x060002E5 RID: 741 RVA: 0x0000BD74 File Offset: 0x00009F74
		public DependentModule()
		{
			this.Id = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.IsOptional = false;
			base..ctor();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000BD9B File Offset: 0x00009F9B
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

		// Token: 0x060002E7 RID: 743 RVA: 0x0000BDDC File Offset: 0x00009FDC
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

		// Token: 0x060002E8 RID: 744 RVA: 0x0000BE28 File Offset: 0x0000A028
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

		// Token: 0x060002E9 RID: 745 RVA: 0x0000BE94 File Offset: 0x0000A094
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DependentModule left, DependentModule right)
		{
			return !(left == right);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DependentModule left, DependentModule right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Version>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsOptional>k__BackingField);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000BF1A File Offset: 0x0000A11A
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DependentModule);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000BF28 File Offset: 0x0000A128
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DependentModule other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Version>k__BackingField, other.<Version>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsOptional>k__BackingField, other.<IsOptional>k__BackingField));
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BFA3 File Offset: 0x0000A1A3
		[CompilerGenerated]
		protected DependentModule(DependentModule original)
		{
			this.Id = original.<Id>k__BackingField;
			this.Version = original.<Version>k__BackingField;
			this.IsOptional = original.<IsOptional>k__BackingField;
		}
	}
}
