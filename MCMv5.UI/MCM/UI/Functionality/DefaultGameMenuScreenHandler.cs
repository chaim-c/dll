using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Bannerlord.BUTR.Shared.Extensions;
using BUTR.DependencyInjection.Logger;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using MCM.UI.Utils;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu;
using TaleWorlds.ScreenSystem;

namespace MCM.UI.Functionality
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DefaultGameMenuScreenHandler : BaseGameMenuScreenHandler
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000079BD File Offset: 0x00005BBD
		[Nullable(new byte[]
		{
			1,
			1,
			0,
			1,
			2,
			1
		})]
		private static Dictionary<string, ValueTuple<int, Func<ScreenBase>, TextObject>> ScreensCache { [return: Nullable(new byte[]
		{
			1,
			1,
			0,
			1,
			2,
			1
		})] get; } = new Dictionary<string, ValueTuple<int, Func<ScreenBase>, TextObject>>();

		// Token: 0x06000174 RID: 372 RVA: 0x000079C4 File Offset: 0x00005BC4
		public DefaultGameMenuScreenHandler(IBUTRLogger<DefaultGameMenuScreenHandler> logger)
		{
			this._logger = logger;
			Harmony harmony = new Harmony("bannerlord.mcm.mainmenuscreeninjection_v4");
			harmony.Patch(AccessTools2.Method(typeof(InitialMenuVM), "RefreshMenuOptions", null, null, true), null, new HarmonyMethod(AccessTools2.Method(typeof(DefaultGameMenuScreenHandler), "RefreshMenuOptionsPostfix", null, null, true), 300, null, null, null), null, null);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007A38 File Offset: 0x00005C38
		[MethodImpl(MethodImplOptions.NoInlining)]
		private unsafe static void RefreshMenuOptionsPostfix(InitialMenuVM __instance, [Nullable(new byte[]
		{
			2,
			1
		})] ref MBBindingList<InitialMenuOptionVM> ____menuOptions)
		{
			bool flag = ____menuOptions == null || DefaultGameMenuScreenHandler.InitialStateOption == null;
			if (!flag)
			{
				DefaultGameMenuScreenHandler._instance.SetTarget(__instance);
				foreach (KeyValuePair<string, ValueTuple<int, Func<ScreenBase>, TextObject>> tuple in DefaultGameMenuScreenHandler.ScreensCache)
				{
					string text2;
					ValueTuple<int, Func<ScreenBase>, TextObject> valueTuple;
					tuple.Deconstruct(out text2, out valueTuple);
					string key = text2;
					ValueTuple<int, Func<ScreenBase>, TextObject> value = valueTuple;
					valueTuple = value;
					int index = valueTuple.Item1;
					Func<ScreenBase> screenFactory = valueTuple.Item2;
					TextObject text = valueTuple.Item3;
					InitialStateOption initialState = InitialStateOptionUtils.Create(key, text, 9000, delegate
					{
						ScreenBase screen = screenFactory();
						bool flag2 = screen != null;
						if (flag2)
						{
							ScreenManager.PushScreen(screen);
						}
					}, () => new ValueTuple<bool, TextObject>(false, null));
					int insertIndex = ____menuOptions.FindIndex((InitialMenuOptionVM i) => DefaultGameMenuScreenHandler.InitialStateOption(i)->OrderIndex > index);
					____menuOptions.Insert(insertIndex, new InitialMenuOptionVM(initialState));
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007B54 File Offset: 0x00005D54
		public unsafe override void AddScreen(string internalName, int index, [Nullable(new byte[]
		{
			1,
			2
		})] Func<ScreenBase> screenFactory, [Nullable(2)] TextObject text)
		{
			bool flag = text == null;
			if (!flag)
			{
				InitialMenuVM instance;
				bool flag2 = DefaultGameMenuScreenHandler._instance.TryGetTarget(out instance) && DefaultGameMenuScreenHandler.InitialStateOption != null;
				if (flag2)
				{
					InitialStateOption initialState = InitialStateOptionUtils.Create(internalName, text, index, delegate
					{
						ScreenBase screen = screenFactory();
						bool flag3 = screen != null;
						if (flag3)
						{
							ScreenManager.PushScreen(screen);
						}
					}, () => new ValueTuple<bool, TextObject>(false, null));
					MBBindingList<InitialMenuOptionVM> menuOptions = instance.MenuOptions;
					int insertIndex = menuOptions.FindIndex((InitialMenuOptionVM i) => DefaultGameMenuScreenHandler.InitialStateOption(i)->OrderIndex > index);
					if (menuOptions != null)
					{
						menuOptions.Insert(insertIndex, new InitialMenuOptionVM(initialState));
					}
				}
				DefaultGameMenuScreenHandler.ScreensCache[internalName] = new ValueTuple<int, Func<ScreenBase>, TextObject>(index, screenFactory, text);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007C34 File Offset: 0x00005E34
		public unsafe override void RemoveScreen(string internalName)
		{
			InitialMenuVM instance;
			bool flag = DefaultGameMenuScreenHandler._instance.TryGetTarget(out instance) && DefaultGameMenuScreenHandler.InitialStateOption != null;
			if (flag)
			{
				MBBindingList<InitialMenuOptionVM> menuOptions = instance.MenuOptions;
				InitialMenuOptionVM found = (menuOptions != null) ? menuOptions.FirstOrDefault((InitialMenuOptionVM i) => DefaultGameMenuScreenHandler.InitialStateOption(i)->Id == internalName) : null;
				bool flag2 = found != null;
				if (flag2)
				{
					if (menuOptions != null)
					{
						menuOptions.Remove(found);
					}
				}
			}
			bool flag3 = DefaultGameMenuScreenHandler.ScreensCache.ContainsKey(internalName);
			if (flag3)
			{
				DefaultGameMenuScreenHandler.ScreensCache.Remove(internalName);
			}
		}

		// Token: 0x04000066 RID: 102
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private static readonly AccessTools.FieldRef<InitialMenuOptionVM, InitialStateOption> InitialStateOption = AccessTools2.FieldRefAccess<InitialMenuOptionVM, InitialStateOption>("InitialStateOption", true);

		// Token: 0x04000067 RID: 103
		private static readonly WeakReference<InitialMenuVM> _instance = new WeakReference<InitialMenuVM>(null);

		// Token: 0x04000069 RID: 105
		private readonly IBUTRLogger _logger;
	}
}
