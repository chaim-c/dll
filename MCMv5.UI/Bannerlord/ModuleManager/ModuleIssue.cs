using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x0200004D RID: 77
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ModuleIssue : IEquatable<ModuleIssue>
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModuleIssue);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000D9F6 File Offset: 0x0000BBF6
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000D9FE File Offset: 0x0000BBFE
		public ModuleInfoExtended Target { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000DA07 File Offset: 0x0000BC07
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000DA0F File Offset: 0x0000BC0F
		public string SourceId { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000DA18 File Offset: 0x0000BC18
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000DA20 File Offset: 0x0000BC20
		public ModuleIssueType Type { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000DA29 File Offset: 0x0000BC29
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000DA31 File Offset: 0x0000BC31
		public string Reason { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000DA3A File Offset: 0x0000BC3A
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000DA42 File Offset: 0x0000BC42
		public ApplicationVersionRange SourceVersion { get; set; }

		// Token: 0x06000343 RID: 835 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		public ModuleIssue()
		{
			this.Target = new ModuleInfoExtended();
			this.SourceId = string.Empty;
			this.Type = ModuleIssueType.NONE;
			this.Reason = string.Empty;
			this.SourceVersion = ApplicationVersionRange.Empty;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000DA99 File Offset: 0x0000BC99
		public ModuleIssue(ModuleInfoExtended target, string sourceId, ModuleIssueType type)
		{
			this.Target = target;
			this.SourceId = sourceId;
			this.Type = type;
			this.Reason = string.Empty;
			this.SourceVersion = ApplicationVersionRange.Empty;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000DAD3 File Offset: 0x0000BCD3
		public ModuleIssue(ModuleInfoExtended target, string sourceId, ModuleIssueType type, string reason, ApplicationVersionRange sourceVersion) : this(target, sourceId, type)
		{
			this.Reason = reason;
			this.SourceVersion = sourceVersion;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ModuleIssue");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000DB40 File Offset: 0x0000BD40
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Target = ");
			builder.Append(this.Target);
			builder.Append(", SourceId = ");
			builder.Append(this.SourceId);
			builder.Append(", Type = ");
			builder.Append(this.Type.ToString());
			builder.Append(", Reason = ");
			builder.Append(this.Reason);
			builder.Append(", SourceVersion = ");
			builder.Append(this.SourceVersion);
			return true;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000DBDE File Offset: 0x0000BDDE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModuleIssue left, ModuleIssue right)
		{
			return !(left == right);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000DBEA File Offset: 0x0000BDEA
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModuleIssue left, ModuleIssue right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000DC00 File Offset: 0x0000BE00
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((EqualityComparer<System.Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ModuleInfoExtended>.Default.GetHashCode(this.<Target>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceId>k__BackingField)) * -1521134295 + EqualityComparer<ModuleIssueType>.Default.GetHashCode(this.<Type>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Reason>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersionRange>.Default.GetHashCode(this.<SourceVersion>k__BackingField);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000DC90 File Offset: 0x0000BE90
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModuleIssue);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(ModuleIssue other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ModuleInfoExtended>.Default.Equals(this.<Target>k__BackingField, other.<Target>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SourceId>k__BackingField, other.<SourceId>k__BackingField) && EqualityComparer<ModuleIssueType>.Default.Equals(this.<Type>k__BackingField, other.<Type>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Reason>k__BackingField, other.<Reason>k__BackingField) && EqualityComparer<ApplicationVersionRange>.Default.Equals(this.<SourceVersion>k__BackingField, other.<SourceVersion>k__BackingField));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000DD54 File Offset: 0x0000BF54
		[CompilerGenerated]
		private ModuleIssue(ModuleIssue original)
		{
			this.Target = original.<Target>k__BackingField;
			this.SourceId = original.<SourceId>k__BackingField;
			this.Type = original.<Type>k__BackingField;
			this.Reason = original.<Reason>k__BackingField;
			this.SourceVersion = original.<SourceVersion>k__BackingField;
		}
	}
}
