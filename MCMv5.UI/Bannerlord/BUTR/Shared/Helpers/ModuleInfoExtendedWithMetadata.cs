using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using Bannerlord.ModuleManager;

namespace Bannerlord.BUTR.Shared.Helpers
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal class ModuleInfoExtendedWithMetadata : ModuleInfoExtended, IEquatable<ModuleInfoExtendedWithMetadata>
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000A277 File Offset: 0x00008477
		[CompilerGenerated]
		protected override Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModuleInfoExtendedWithMetadata);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A283 File Offset: 0x00008483
		public bool IsExternal { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A28B File Offset: 0x0000848B
		public string Path { get; }

		// Token: 0x06000279 RID: 633 RVA: 0x0000A293 File Offset: 0x00008493
		public ModuleInfoExtendedWithMetadata(ModuleInfoExtended module, bool isExternal, string path) : base(module)
		{
			this.IsExternal = isExternal;
			this.Path = path;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A2AC File Offset: 0x000084AC
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

		// Token: 0x0600027B RID: 635 RVA: 0x0000A2F8 File Offset: 0x000084F8
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

		// Token: 0x0600027C RID: 636 RVA: 0x0000A35B File Offset: 0x0000855B
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModuleInfoExtendedWithMetadata left, ModuleInfoExtendedWithMetadata right)
		{
			return !(left == right);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A367 File Offset: 0x00008567
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModuleInfoExtendedWithMetadata left, ModuleInfoExtendedWithMetadata right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A37D File Offset: 0x0000857D
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (base.GetHashCode() * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsExternal>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A3B3 File Offset: 0x000085B3
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModuleInfoExtendedWithMetadata);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A3C1 File Offset: 0x000085C1
		[NullableContext(2)]
		[CompilerGenerated]
		public sealed override bool Equals(ModuleInfoExtended other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A3CC File Offset: 0x000085CC
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ModuleInfoExtendedWithMetadata other)
		{
			return this == other || (base.Equals(other) && EqualityComparer<bool>.Default.Equals(this.<IsExternal>k__BackingField, other.<IsExternal>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField));
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A422 File Offset: 0x00008622
		[CompilerGenerated]
		protected ModuleInfoExtendedWithMetadata(ModuleInfoExtendedWithMetadata original) : base(original)
		{
			this.IsExternal = original.<IsExternal>k__BackingField;
			this.Path = original.<Path>k__BackingField;
		}
	}
}
