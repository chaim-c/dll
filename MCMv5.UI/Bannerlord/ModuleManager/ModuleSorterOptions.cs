using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bannerlord.ModuleManager
{
	// Token: 0x02000054 RID: 84
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ModuleSorterOptions : IEquatable<ModuleSorterOptions>
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000E87E File Offset: 0x0000CA7E
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModuleSorterOptions);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000E88A File Offset: 0x0000CA8A
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000E892 File Offset: 0x0000CA92
		public bool SkipOptionals { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000E89B File Offset: 0x0000CA9B
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000E8A3 File Offset: 0x0000CAA3
		public bool SkipExternalDependencies { get; set; }

		// Token: 0x0600037E RID: 894 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		public ModuleSorterOptions()
		{
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000E8B6 File Offset: 0x0000CAB6
		public ModuleSorterOptions(bool skipOptionals, bool skipExternalDependencies)
		{
			this.SkipOptionals = skipOptionals;
			this.SkipExternalDependencies = skipExternalDependencies;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
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

		// Token: 0x06000381 RID: 897 RVA: 0x0000E91C File Offset: 0x0000CB1C
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

		// Token: 0x06000382 RID: 898 RVA: 0x0000E97D File Offset: 0x0000CB7D
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModuleSorterOptions left, ModuleSorterOptions right)
		{
			return !(left == right);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000E989 File Offset: 0x0000CB89
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModuleSorterOptions left, ModuleSorterOptions right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000E99F File Offset: 0x0000CB9F
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SkipOptionals>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SkipExternalDependencies>k__BackingField);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E9DF File Offset: 0x0000CBDF
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModuleSorterOptions);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(ModuleSorterOptions other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<SkipOptionals>k__BackingField, other.<SkipOptionals>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SkipExternalDependencies>k__BackingField, other.<SkipExternalDependencies>k__BackingField));
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000EA53 File Offset: 0x0000CC53
		[CompilerGenerated]
		private ModuleSorterOptions(ModuleSorterOptions original)
		{
			this.SkipOptionals = original.<SkipOptionals>k__BackingField;
			this.SkipExternalDependencies = original.<SkipExternalDependencies>k__BackingField;
		}
	}
}
