using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x0200013A RID: 314
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ModuleIssue : IEquatable<ModuleIssue>
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001B6AE File Offset: 0x000198AE
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModuleIssue);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001B6BA File Offset: 0x000198BA
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x0001B6C2 File Offset: 0x000198C2
		public ModuleInfoExtended Target { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001B6CB File Offset: 0x000198CB
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001B6D3 File Offset: 0x000198D3
		public string SourceId { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x0001B6DC File Offset: 0x000198DC
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x0001B6E4 File Offset: 0x000198E4
		public ModuleIssueType Type { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001B6ED File Offset: 0x000198ED
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0001B6F5 File Offset: 0x000198F5
		public string Reason { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001B6FE File Offset: 0x000198FE
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x0001B706 File Offset: 0x00019906
		public ApplicationVersionRange SourceVersion { get; set; }

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B710 File Offset: 0x00019910
		public ModuleIssue()
		{
			this.Target = new ModuleInfoExtended();
			this.SourceId = string.Empty;
			this.Type = ModuleIssueType.NONE;
			this.Reason = string.Empty;
			this.SourceVersion = ApplicationVersionRange.Empty;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001B75D File Offset: 0x0001995D
		public ModuleIssue(ModuleInfoExtended target, string sourceId, ModuleIssueType type)
		{
			this.Target = target;
			this.SourceId = sourceId;
			this.Type = type;
			this.Reason = string.Empty;
			this.SourceVersion = ApplicationVersionRange.Empty;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001B797 File Offset: 0x00019997
		public ModuleIssue(ModuleInfoExtended target, string sourceId, ModuleIssueType type, string reason, ApplicationVersionRange sourceVersion) : this(target, sourceId, type)
		{
			this.Reason = reason;
			this.SourceVersion = sourceVersion;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001B7B8 File Offset: 0x000199B8
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

		// Token: 0x06000855 RID: 2133 RVA: 0x0001B804 File Offset: 0x00019A04
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

		// Token: 0x06000856 RID: 2134 RVA: 0x0001B8A2 File Offset: 0x00019AA2
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModuleIssue left, ModuleIssue right)
		{
			return !(left == right);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001B8AE File Offset: 0x00019AAE
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModuleIssue left, ModuleIssue right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((((EqualityComparer<System.Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ModuleInfoExtended>.Default.GetHashCode(this.<Target>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceId>k__BackingField)) * -1521134295 + EqualityComparer<ModuleIssueType>.Default.GetHashCode(this.<Type>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Reason>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersionRange>.Default.GetHashCode(this.<SourceVersion>k__BackingField);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001B954 File Offset: 0x00019B54
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModuleIssue);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001B964 File Offset: 0x00019B64
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(ModuleIssue other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ModuleInfoExtended>.Default.Equals(this.<Target>k__BackingField, other.<Target>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SourceId>k__BackingField, other.<SourceId>k__BackingField) && EqualityComparer<ModuleIssueType>.Default.Equals(this.<Type>k__BackingField, other.<Type>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Reason>k__BackingField, other.<Reason>k__BackingField) && EqualityComparer<ApplicationVersionRange>.Default.Equals(this.<SourceVersion>k__BackingField, other.<SourceVersion>k__BackingField));
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001BA18 File Offset: 0x00019C18
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
