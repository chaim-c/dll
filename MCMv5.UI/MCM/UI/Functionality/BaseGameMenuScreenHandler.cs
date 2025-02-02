using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace MCM.UI.Functionality
{
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BaseGameMenuScreenHandler
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000079AD File Offset: 0x00005BAD
		[Nullable(2)]
		public static BaseGameMenuScreenHandler Instance
		{
			[NullableContext(2)]
			get
			{
				return GenericServiceProvider.GetService<BaseGameMenuScreenHandler>();
			}
		}

		// Token: 0x06000170 RID: 368
		public abstract void AddScreen(string internalName, int index, [Nullable(new byte[]
		{
			1,
			2
		})] Func<ScreenBase> screenFactory, [Nullable(2)] TextObject text);

		// Token: 0x06000171 RID: 369
		public abstract void RemoveScreen(string internalName);
	}
}
