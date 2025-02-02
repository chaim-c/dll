using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MCM.Abstractions
{
	// Token: 0x0200005A RID: 90
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SettingSnapshot : IEquatable<SettingSnapshot>
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00007F3E File Offset: 0x0000613E
		public SettingSnapshot(string Path, string JsonContent)
		{
			this.Path = Path;
			this.JsonContent = JsonContent;
			base..ctor();
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007F55 File Offset: 0x00006155
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(SettingSnapshot);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007F61 File Offset: 0x00006161
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00007F69 File Offset: 0x00006169
		public string Path { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007F72 File Offset: 0x00006172
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00007F7A File Offset: 0x0000617A
		public string JsonContent { get; set; }

		// Token: 0x060001DE RID: 478 RVA: 0x00007F84 File Offset: 0x00006184
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SettingSnapshot");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007FD0 File Offset: 0x000061D0
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Path = ");
			builder.Append(this.Path);
			builder.Append(", JsonContent = ");
			builder.Append(this.JsonContent);
			return true;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000800A File Offset: 0x0000620A
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(SettingSnapshot left, SettingSnapshot right)
		{
			return !(left == right);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008016 File Offset: 0x00006216
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(SettingSnapshot left, SettingSnapshot right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000802C File Offset: 0x0000622C
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JsonContent>k__BackingField);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000806C File Offset: 0x0000626C
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SettingSnapshot);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000807C File Offset: 0x0000627C
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(SettingSnapshot other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JsonContent>k__BackingField, other.<JsonContent>k__BackingField));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000080DF File Offset: 0x000062DF
		[CompilerGenerated]
		private SettingSnapshot(SettingSnapshot original)
		{
			this.Path = original.<Path>k__BackingField;
			this.JsonContent = original.<JsonContent>k__BackingField;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008101 File Offset: 0x00006301
		[CompilerGenerated]
		public void Deconstruct(out string Path, out string JsonContent)
		{
			Path = this.Path;
			JsonContent = this.JsonContent;
		}
	}
}
