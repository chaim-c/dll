using System;
using System.Runtime.CompilerServices;
using Bannerlord.ButterLib.HotKeys;
using TaleWorlds.InputSystem;

namespace MCM.UI.HotKeys
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public class ResetValueToDefault : HotKeyBase
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000043DA File Offset: 0x000025DA
		protected override string DisplayName { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000043E2 File Offset: 0x000025E2
		protected override string Description { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000043EA File Offset: 0x000025EA
		protected override InputKey DefaultKey { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000043F2 File Offset: 0x000025F2
		protected override string Category { get; }

		// Token: 0x060000AF RID: 175 RVA: 0x000043FA File Offset: 0x000025FA
		public ResetValueToDefault() : base("ResetValueToDefault")
		{
			this.DisplayName = "{=HOV8WIcBrb}Reset Mod Options Value to Default";
			this.Description = "{=2d99VmOZZH}Resets a value in Mod Options menu to its default value when hovered.";
			this.DefaultKey = 19;
			this.Category = HotKeyManager.Categories[2];
		}
	}
}
