using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Base;

namespace MCM.Abstractions
{
	// Token: 0x02000060 RID: 96
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MemorySettingsPreset : ISettingsPreset
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00008B73 File Offset: 0x00006D73
		public string SettingsId { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008B7B File Offset: 0x00006D7B
		public string Id { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00008B83 File Offset: 0x00006D83
		public string Name { get; }

		// Token: 0x0600022E RID: 558 RVA: 0x00008B8B File Offset: 0x00006D8B
		public MemorySettingsPreset(string settingId, string id, string name, Func<BaseSettings> template)
		{
			this.SettingsId = settingId;
			this.Id = id;
			this.Name = name;
			this._template = template;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008BB2 File Offset: 0x00006DB2
		public BaseSettings LoadPreset()
		{
			return this._template();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008BBF File Offset: 0x00006DBF
		public bool SavePreset(BaseSettings settings)
		{
			return true;
		}

		// Token: 0x040000A7 RID: 167
		private readonly Func<BaseSettings> _template;
	}
}
