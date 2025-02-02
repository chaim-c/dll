using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MCM.Abstractions.GameFeatures
{
	// Token: 0x02000071 RID: 113
	[NullableContext(1)]
	[Nullable(0)]
	public class GameFile : IEquatable<GameFile>
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00009C4B File Offset: 0x00007E4B
		public GameFile(GameDirectory Owner, string Name)
		{
			this.Owner = Owner;
			this.Name = Name;
			base..ctor();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009C62 File Offset: 0x00007E62
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(GameFile);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009C6E File Offset: 0x00007E6E
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00009C76 File Offset: 0x00007E76
		public GameDirectory Owner { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009C7F File Offset: 0x00007E7F
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00009C87 File Offset: 0x00007E87
		public string Name { get; set; }

		// Token: 0x0600028A RID: 650 RVA: 0x00009C90 File Offset: 0x00007E90
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GameFile");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009CDC File Offset: 0x00007EDC
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Owner = ");
			builder.Append(this.Owner);
			builder.Append(", Name = ");
			builder.Append(this.Name);
			return true;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009D16 File Offset: 0x00007F16
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(GameFile left, GameFile right)
		{
			return !(left == right);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009D22 File Offset: 0x00007F22
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(GameFile left, GameFile right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009D38 File Offset: 0x00007F38
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<GameDirectory>.Default.GetHashCode(this.<Owner>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009D78 File Offset: 0x00007F78
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameFile);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009D88 File Offset: 0x00007F88
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(GameFile other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<GameDirectory>.Default.Equals(this.<Owner>k__BackingField, other.<Owner>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField));
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009DEB File Offset: 0x00007FEB
		[CompilerGenerated]
		protected GameFile(GameFile original)
		{
			this.Owner = original.<Owner>k__BackingField;
			this.Name = original.<Name>k__BackingField;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009E0D File Offset: 0x0000800D
		[CompilerGenerated]
		public void Deconstruct(out GameDirectory Owner, out string Name)
		{
			Owner = this.Owner;
			Name = this.Name;
		}
	}
}
