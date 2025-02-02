using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MCM.Abstractions
{
	// Token: 0x0200005D RID: 93
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UnavailableSetting : IEquatable<UnavailableSetting>
	{
		// Token: 0x06000214 RID: 532 RVA: 0x00008906 File Offset: 0x00006B06
		public UnavailableSetting(string Id, string DisplayName, UnavailableSettingType Type)
		{
			this.Id = Id;
			this.DisplayName = DisplayName;
			this.Type = Type;
			base..ctor();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008924 File Offset: 0x00006B24
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(UnavailableSetting);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008930 File Offset: 0x00006B30
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00008938 File Offset: 0x00006B38
		public string Id { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008941 File Offset: 0x00006B41
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00008949 File Offset: 0x00006B49
		public string DisplayName { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00008952 File Offset: 0x00006B52
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000895A File Offset: 0x00006B5A
		public UnavailableSettingType Type { get; set; }

		// Token: 0x0600021C RID: 540 RVA: 0x00008964 File Offset: 0x00006B64
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UnavailableSetting");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000089B0 File Offset: 0x00006BB0
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", DisplayName = ");
			builder.Append(this.DisplayName);
			builder.Append(", Type = ");
			builder.Append(this.Type.ToString());
			return true;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008A1C File Offset: 0x00006C1C
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(UnavailableSetting left, UnavailableSetting right)
		{
			return !(left == right);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008A28 File Offset: 0x00006C28
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(UnavailableSetting left, UnavailableSetting right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008A40 File Offset: 0x00006C40
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<System.Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<UnavailableSettingType>.Default.GetHashCode(this.<Type>k__BackingField);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008AA2 File Offset: 0x00006CA2
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as UnavailableSetting);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008AB0 File Offset: 0x00006CB0
		[NullableContext(2)]
		[CompilerGenerated]
		public bool Equals(UnavailableSetting other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<UnavailableSettingType>.Default.Equals(this.<Type>k__BackingField, other.<Type>k__BackingField));
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008B2B File Offset: 0x00006D2B
		[CompilerGenerated]
		private UnavailableSetting(UnavailableSetting original)
		{
			this.Id = original.<Id>k__BackingField;
			this.DisplayName = original.<DisplayName>k__BackingField;
			this.Type = original.<Type>k__BackingField;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008B59 File Offset: 0x00006D59
		[CompilerGenerated]
		public void Deconstruct(out string Id, out string DisplayName, out UnavailableSettingType Type)
		{
			Id = this.Id;
			DisplayName = this.DisplayName;
			Type = this.Type;
		}
	}
}
