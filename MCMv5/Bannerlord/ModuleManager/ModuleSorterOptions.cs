using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000141 RID: 321
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ModuleSorterOptions : IEquatable<ModuleSorterOptions>
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001C542 File Offset: 0x0001A742
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModuleSorterOptions);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001C54E File Offset: 0x0001A74E
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0001C556 File Offset: 0x0001A756
		public bool SkipOptionals { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0001C55F File Offset: 0x0001A75F
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0001C567 File Offset: 0x0001A767
		public bool SkipExternalDependencies { get; set; }

		// Token: 0x0600088C RID: 2188 RVA: 0x0001C570 File Offset: 0x0001A770
		public ModuleSorterOptions()
		{
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001C57A File Offset: 0x0001A77A
		public ModuleSorterOptions(bool skipOptionals, bool skipExternalDependencies)
		{
			this.SkipOptionals = skipOptionals;
			this.SkipExternalDependencies = skipExternalDependencies;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001C594 File Offset: 0x0001A794
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ModuleSorterOptions");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("SkipOptionals = ");
			builder.Append(this.SkipOptionals.ToString());
			builder.Append(", SkipExternalDependencies = ");
			builder.Append(this.SkipExternalDependencies.ToString());
			return true;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001C641 File Offset: 0x0001A841
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModuleSorterOptions left, ModuleSorterOptions right)
		{
			return !(left == right);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001C64D File Offset: 0x0001A84D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModuleSorterOptions left, ModuleSorterOptions right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001C663 File Offset: 0x0001A863
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SkipOptionals>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SkipExternalDependencies>k__BackingField);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001C6A3 File Offset: 0x0001A8A3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModuleSorterOptions);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(ModuleSorterOptions other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<SkipOptionals>k__BackingField, other.<SkipOptionals>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SkipExternalDependencies>k__BackingField, other.<SkipExternalDependencies>k__BackingField));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001C717 File Offset: 0x0001A917
		[CompilerGenerated]
		private ModuleSorterOptions(ModuleSorterOptions original)
		{
			this.SkipOptionals = original.<SkipOptionals>k__BackingField;
			this.SkipExternalDependencies = original.<SkipExternalDependencies>k__BackingField;
		}
	}
}
