using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000136 RID: 310
	[NullableContext(1)]
	[Nullable(0)]
	internal class DependentModuleMetadata : IEquatable<DependentModuleMetadata>
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00019C95 File Offset: 0x00017E95
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DependentModuleMetadata);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00019CA1 File Offset: 0x00017EA1
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00019CA9 File Offset: 0x00017EA9
		public string Id { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00019CB2 File Offset: 0x00017EB2
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00019CBA File Offset: 0x00017EBA
		public LoadType LoadType { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00019CC3 File Offset: 0x00017EC3
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00019CCB File Offset: 0x00017ECB
		public bool IsOptional { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00019CD4 File Offset: 0x00017ED4
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00019CDC File Offset: 0x00017EDC
		public bool IsIncompatible { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00019CE5 File Offset: 0x00017EE5
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00019CED File Offset: 0x00017EED
		public ApplicationVersion Version { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00019CF6 File Offset: 0x00017EF6
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00019CFE File Offset: 0x00017EFE
		public ApplicationVersionRange VersionRange { get; set; }

		// Token: 0x0600080B RID: 2059 RVA: 0x00019D07 File Offset: 0x00017F07
		public DependentModuleMetadata()
		{
			this.Id = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.VersionRange = ApplicationVersionRange.Empty;
			base..ctor();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00019D34 File Offset: 0x00017F34
		public DependentModuleMetadata(string id, LoadType loadType, bool isOptional, bool isIncompatible, ApplicationVersion version, ApplicationVersionRange versionRange)
		{
			this.Id = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.VersionRange = ApplicationVersionRange.Empty;
			base..ctor();
			this.Id = id;
			this.LoadType = loadType;
			this.IsOptional = isOptional;
			this.IsIncompatible = isIncompatible;
			this.Version = version;
			this.VersionRange = versionRange;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public static string GetLoadType(LoadType loadType)
		{
			if (!true)
			{
			}
			string result;
			switch (loadType)
			{
			case LoadType.None:
				result = "";
				break;
			case LoadType.LoadAfterThis:
				result = "Before       ";
				break;
			case LoadType.LoadBeforeThis:
				result = "After        ";
				break;
			default:
				result = "ERROR        ";
				break;
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00019DEA File Offset: 0x00017FEA
		public static string GetVersion([Nullable(2)] ApplicationVersion av)
		{
			return (av != null && av.IsSameWithChangeSet(ApplicationVersion.Empty)) ? "" : string.Format(" {0}", av);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00019E0E File Offset: 0x0001800E
		public static string GetVersionRange([Nullable(2)] ApplicationVersionRange avr)
		{
			return (avr == ApplicationVersionRange.Empty) ? "" : string.Format(" {0}", avr);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00019E2F File Offset: 0x0001802F
		public static string GetOptional(bool isOptional)
		{
			return isOptional ? " Optional" : "";
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00019E40 File Offset: 0x00018040
		public static string GetIncompatible(bool isOptional)
		{
			return isOptional ? "Incompatible " : "";
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00019E54 File Offset: 0x00018054
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				DependentModuleMetadata.GetLoadType(this.LoadType),
				DependentModuleMetadata.GetIncompatible(this.IsIncompatible),
				this.Id,
				DependentModuleMetadata.GetVersion(this.Version),
				DependentModuleMetadata.GetVersionRange(this.VersionRange),
				DependentModuleMetadata.GetOptional(this.IsOptional)
			});
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00019EBC File Offset: 0x000180BC
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", LoadType = ");
			builder.Append(this.LoadType.ToString());
			builder.Append(", IsOptional = ");
			builder.Append(this.IsOptional.ToString());
			builder.Append(", IsIncompatible = ");
			builder.Append(this.IsIncompatible.ToString());
			builder.Append(", Version = ");
			builder.Append(this.Version);
			builder.Append(", VersionRange = ");
			builder.Append(this.VersionRange);
			return true;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00019F8F File Offset: 0x0001818F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DependentModuleMetadata left, DependentModuleMetadata right)
		{
			return !(left == right);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00019F9B File Offset: 0x0001819B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DependentModuleMetadata left, DependentModuleMetadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00019FB4 File Offset: 0x000181B4
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<LoadType>.Default.GetHashCode(this.<LoadType>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsOptional>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsIncompatible>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Version>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersionRange>.Default.GetHashCode(this.<VersionRange>k__BackingField);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001A05B File Offset: 0x0001825B
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DependentModuleMetadata);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001A06C File Offset: 0x0001826C
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DependentModuleMetadata other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<LoadType>.Default.Equals(this.<LoadType>k__BackingField, other.<LoadType>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsOptional>k__BackingField, other.<IsOptional>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsIncompatible>k__BackingField, other.<IsIncompatible>k__BackingField) && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Version>k__BackingField, other.<Version>k__BackingField) && EqualityComparer<ApplicationVersionRange>.Default.Equals(this.<VersionRange>k__BackingField, other.<VersionRange>k__BackingField));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001A138 File Offset: 0x00018338
		[CompilerGenerated]
		protected DependentModuleMetadata(DependentModuleMetadata original)
		{
			this.Id = original.<Id>k__BackingField;
			this.LoadType = original.<LoadType>k__BackingField;
			this.IsOptional = original.<IsOptional>k__BackingField;
			this.IsIncompatible = original.<IsIncompatible>k__BackingField;
			this.Version = original.<Version>k__BackingField;
			this.VersionRange = original.<VersionRange>k__BackingField;
		}
	}
}
