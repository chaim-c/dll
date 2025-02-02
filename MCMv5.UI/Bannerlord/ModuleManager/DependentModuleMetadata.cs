using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000049 RID: 73
	[NullableContext(1)]
	[Nullable(0)]
	internal class DependentModuleMetadata : IEquatable<DependentModuleMetadata>
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000BFD1 File Offset: 0x0000A1D1
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(DependentModuleMetadata);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000BFDD File Offset: 0x0000A1DD
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000BFE5 File Offset: 0x0000A1E5
		public string Id { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000BFEE File Offset: 0x0000A1EE
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000BFF6 File Offset: 0x0000A1F6
		public LoadType LoadType { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000BFFF File Offset: 0x0000A1FF
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000C007 File Offset: 0x0000A207
		public bool IsOptional { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000C010 File Offset: 0x0000A210
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000C018 File Offset: 0x0000A218
		public bool IsIncompatible { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000C021 File Offset: 0x0000A221
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000C029 File Offset: 0x0000A229
		public ApplicationVersion Version { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C032 File Offset: 0x0000A232
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000C03A File Offset: 0x0000A23A
		public ApplicationVersionRange VersionRange { get; set; }

		// Token: 0x060002FD RID: 765 RVA: 0x0000C043 File Offset: 0x0000A243
		public DependentModuleMetadata()
		{
			this.Id = string.Empty;
			this.Version = ApplicationVersion.Empty;
			this.VersionRange = ApplicationVersionRange.Empty;
			base..ctor();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C070 File Offset: 0x0000A270
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

		// Token: 0x060002FF RID: 767 RVA: 0x0000C0DC File Offset: 0x0000A2DC
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

		// Token: 0x06000300 RID: 768 RVA: 0x0000C126 File Offset: 0x0000A326
		public static string GetVersion([Nullable(2)] ApplicationVersion av)
		{
			return (av != null && av.IsSameWithChangeSet(ApplicationVersion.Empty)) ? "" : string.Format(" {0}", av);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000C14A File Offset: 0x0000A34A
		public static string GetVersionRange([Nullable(2)] ApplicationVersionRange avr)
		{
			return (avr == ApplicationVersionRange.Empty) ? "" : string.Format(" {0}", avr);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C16B File Offset: 0x0000A36B
		public static string GetOptional(bool isOptional)
		{
			return isOptional ? " Optional" : "";
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C17C File Offset: 0x0000A37C
		public static string GetIncompatible(bool isOptional)
		{
			return isOptional ? "Incompatible " : "";
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C190 File Offset: 0x0000A390
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

		// Token: 0x06000305 RID: 773 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
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

		// Token: 0x06000306 RID: 774 RVA: 0x0000C2CB File Offset: 0x0000A4CB
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(DependentModuleMetadata left, DependentModuleMetadata right)
		{
			return !(left == right);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C2D7 File Offset: 0x0000A4D7
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(DependentModuleMetadata left, DependentModuleMetadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<LoadType>.Default.GetHashCode(this.<LoadType>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsOptional>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsIncompatible>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersion>.Default.GetHashCode(this.<Version>k__BackingField)) * -1521134295 + EqualityComparer<ApplicationVersionRange>.Default.GetHashCode(this.<VersionRange>k__BackingField);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C397 File Offset: 0x0000A597
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DependentModuleMetadata);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(DependentModuleMetadata other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<LoadType>.Default.Equals(this.<LoadType>k__BackingField, other.<LoadType>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsOptional>k__BackingField, other.<IsOptional>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsIncompatible>k__BackingField, other.<IsIncompatible>k__BackingField) && EqualityComparer<ApplicationVersion>.Default.Equals(this.<Version>k__BackingField, other.<Version>k__BackingField) && EqualityComparer<ApplicationVersionRange>.Default.Equals(this.<VersionRange>k__BackingField, other.<VersionRange>k__BackingField));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C474 File Offset: 0x0000A674
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
