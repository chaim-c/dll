using System;
using System.Runtime.CompilerServices;
using System.Text;
using MCM.Abstractions;
using TaleWorlds.Localization;

namespace MCM.UI
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PresetKey : IEquatable<PresetKey>
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002B62 File Offset: 0x00000D62
		[CompilerGenerated]
		private Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(PresetKey);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002B6E File Offset: 0x00000D6E
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002B76 File Offset: 0x00000D76
		public string Id { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002B7F File Offset: 0x00000D7F
		public string Name
		{
			get
			{
				return new TextObject(this._name, null).ToString();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B92 File Offset: 0x00000D92
		public PresetKey(ISettingsPreset preset)
		{
			this.Id = preset.Id;
			this._name = preset.Name;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BB5 File Offset: 0x00000DB5
		public PresetKey(string id, string name)
		{
			this.Id = id;
			this._name = name;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BCE File Offset: 0x00000DCE
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BE3 File Offset: 0x00000DE3
		[NullableContext(2)]
		public bool Equals(PresetKey other)
		{
			return this.Id.Equals((other != null) ? other.Id : null);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BFC File Offset: 0x00000DFC
		[CompilerGenerated]
		private bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Id = ");
			builder.Append(this.Id);
			builder.Append(", Name = ");
			builder.Append(this.Name);
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C36 File Offset: 0x00000E36
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(PresetKey left, PresetKey right)
		{
			return !(left == right);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C42 File Offset: 0x00000E42
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(PresetKey left, PresetKey right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C58 File Offset: 0x00000E58
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PresetKey);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C6E File Offset: 0x00000E6E
		[CompilerGenerated]
		private PresetKey(PresetKey original)
		{
			this.Id = original.<Id>k__BackingField;
			this._name = original._name;
		}

		// Token: 0x04000014 RID: 20
		private readonly string _name;
	}
}
