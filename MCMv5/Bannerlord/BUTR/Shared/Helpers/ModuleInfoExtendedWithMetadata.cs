using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using Bannerlord.ModuleManager;

namespace Bannerlord.BUTR.Shared.Helpers
{
	// Token: 0x0200012E RID: 302
	[NullableContext(1)]
	[Nullable(0)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal class ModuleInfoExtendedWithMetadata : ModuleInfoExtended, IEquatable<ModuleInfoExtendedWithMetadata>
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00017F3B File Offset: 0x0001613B
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModuleInfoExtendedWithMetadata);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x00017F47 File Offset: 0x00016147
		public bool IsExternal { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00017F4F File Offset: 0x0001614F
		public string Path { get; }

		// Token: 0x06000787 RID: 1927 RVA: 0x00017F57 File Offset: 0x00016157
		public ModuleInfoExtendedWithMetadata(ModuleInfoExtended module, bool isExternal, string path) : base(module)
		{
			this.IsExternal = isExternal;
			this.Path = path;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00017F70 File Offset: 0x00016170
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ModuleInfoExtendedWithMetadata");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00017FBC File Offset: 0x000161BC
		[CompilerGenerated]
		protected override bool PrintMembers(StringBuilder builder)
		{
			if (base.PrintMembers(builder))
			{
				builder.Append(", ");
			}
			builder.Append("IsExternal = ");
			builder.Append(this.IsExternal.ToString());
			builder.Append(", Path = ");
			builder.Append(this.Path);
			return true;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001801F File Offset: 0x0001621F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModuleInfoExtendedWithMetadata left, ModuleInfoExtendedWithMetadata right)
		{
			return !(left == right);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001802B File Offset: 0x0001622B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModuleInfoExtendedWithMetadata left, ModuleInfoExtendedWithMetadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00018041 File Offset: 0x00016241
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsExternal>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00018077 File Offset: 0x00016277
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModuleInfoExtendedWithMetadata);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00018085 File Offset: 0x00016285
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ModuleInfoExtended other)
		{
			return this.Equals(other);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00018090 File Offset: 0x00016290
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ModuleInfoExtendedWithMetadata other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<IsExternal>k__BackingField, other.<IsExternal>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField));
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000180E6 File Offset: 0x000162E6
		[CompilerGenerated]
		protected ModuleInfoExtendedWithMetadata(ModuleInfoExtendedWithMetadata original) : base(original)
		{
			this.IsExternal = original.<IsExternal>k__BackingField;
			this.Path = original.<Path>k__BackingField;
		}
	}
}
