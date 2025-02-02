using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MCM.Abstractions.GameFeatures
{
	// Token: 0x02000070 RID: 112
	[NullableContext(1)]
	[Nullable(0)]
	public class GameDirectory : IEquatable<GameDirectory>
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00009A5C File Offset: 0x00007C5C
		public GameDirectory(PlatformDirectoryType Type, string Path)
		{
			this.Type = Type;
			this.Path = Path;
			base..ctor();
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009A73 File Offset: 0x00007C73
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GameDirectory);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00009A7F File Offset: 0x00007C7F
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00009A87 File Offset: 0x00007C87
		public PlatformDirectoryType Type { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00009A90 File Offset: 0x00007C90
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00009A98 File Offset: 0x00007C98
		public string Path { get; set; }

		// Token: 0x0600027A RID: 634 RVA: 0x00009AA4 File Offset: 0x00007CA4
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GameDirectory");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00009AF0 File Offset: 0x00007CF0
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Type = ");
			builder.Append(this.Type.ToString());
			builder.Append(", Path = ");
			builder.Append(this.Path);
			return true;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009B43 File Offset: 0x00007D43
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GameDirectory left, GameDirectory right)
		{
			return !(left == right);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009B4F File Offset: 0x00007D4F
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GameDirectory left, GameDirectory right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009B65 File Offset: 0x00007D65
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<System.Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<PlatformDirectoryType>.Default.GetHashCode(this.<Type>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009BA5 File Offset: 0x00007DA5
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameDirectory);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009BB4 File Offset: 0x00007DB4
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GameDirectory other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<PlatformDirectoryType>.Default.Equals(this.<Type>k__BackingField, other.<Type>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009C17 File Offset: 0x00007E17
		[CompilerGenerated]
		protected GameDirectory(GameDirectory original)
		{
			this.Type = original.<Type>k__BackingField;
			this.Path = original.<Path>k__BackingField;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009C39 File Offset: 0x00007E39
		[CompilerGenerated]
		public void Deconstruct(out PlatformDirectoryType Type, out string Path)
		{
			Type = this.Type;
			Path = this.Path;
		}
	}
}
