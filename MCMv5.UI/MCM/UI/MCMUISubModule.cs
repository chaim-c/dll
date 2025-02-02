using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Bannerlord.BUTR.Shared.Helpers;
using Bannerlord.ButterLib.Common.Extensions;
using Bannerlord.ButterLib.HotKeys;
using Bannerlord.UIExtenderEx;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.ButterLib;
using BUTR.DependencyInjection.Extensions;
using BUTR.DependencyInjection.Logger;
using BUTR.MessageBoxPInvoke.Helpers;
using HarmonyLib;
using MCM.Abstractions;
using MCM.Internal.Extensions;
using MCM.UI.ButterLib;
using MCM.UI.Functionality;
using MCM.UI.Functionality.Injectors;
using MCM.UI.GUI.GauntletUI;
using MCM.UI.HotKeys;
using MCM.UI.Patches;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace MCM.UI
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MCMUISubModule : MBSubModuleBase
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000027F3 File Offset: 0x000009F3
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000027FB File Offset: 0x000009FB
		private bool DelayedServiceCreation { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002804 File Offset: 0x00000A04
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000280C File Offset: 0x00000A0C
		private bool ServiceRegistrationWasCalled { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002815 File Offset: 0x00000A15
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000281D File Offset: 0x00000A1D
		private bool OnBeforeInitialModuleScreenSetAsRootWasCalled { get; set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002826 File Offset: 0x00000A26
		public MCMUISubModule()
		{
			MCMSubModule instance = MCMSubModule.Instance;
			if (instance != null)
			{
				instance.OverrideServiceContainer(new ButterLibServiceContainer());
			}
			MCMUISubModule.ValidateLoadOrder();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000284C File Offset: 0x00000A4C
		public void OnServiceRegistration()
		{
			this.ServiceRegistrationWasCalled = true;
			IGenericServiceContainer services = this.GetServiceContainer();
			bool flag = services != null;
			if (flag)
			{
				services.AddSettingsContainer<ButterLibSettingsContainer>();
				services.AddTransient(typeof(IServiceProvider), () => DependencyInjectionExtensions.GetTempServiceProvider(this) ?? DependencyInjectionExtensions.GetServiceProvider(this));
				services.AddTransient<IBUTRLogger, LoggerWrapper>();
				services.AddTransient(typeof(IBUTRLogger<>), typeof(LoggerWrapper<>));
				services.AddTransient<IMCMOptionsScreen, ModOptionsGauntletScreen>();
				services.AddSingleton<BaseGameMenuScreenHandler, DefaultGameMenuScreenHandler>();
				services.AddSingleton<ResourceInjector, DefaultResourceInjector>();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028D0 File Offset: 0x00000AD0
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			bool flag = !this.ServiceRegistrationWasCalled;
			IServiceProvider serviceProvider;
			if (flag)
			{
				this.OnServiceRegistration();
				this.DelayedServiceCreation = true;
				serviceProvider = DependencyInjectionExtensions.GetTempServiceProvider(this);
			}
			else
			{
				serviceProvider = DependencyInjectionExtensions.GetServiceProvider(this);
			}
			MCMUISubModule.Logger = ServiceProviderServiceExtensions.GetRequiredService<ILogger<MCMUISubModule>>(serviceProvider);
			LoggerExtensions.LogTrace(MCMUISubModule.Logger, "OnSubModuleLoad: Logging started...", Array.Empty<object>());
			Harmony optionsGauntletScreenHarmony = new Harmony("bannerlord.mcm.ui.optionsgauntletscreenpatch");
			OptionsGauntletScreenPatch.Patch(optionsGauntletScreenHarmony);
			MissionGauntletOptionsUIHandlerPatch.Patch(optionsGauntletScreenHarmony);
			Harmony optionsSwitchHarmony = new Harmony("bannerlord.mcm.ui.optionsswitchpatch");
			OptionsVMPatch.Patch(optionsSwitchHarmony);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002960 File Offset: 0x00000B60
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			bool flag = !this.OnBeforeInitialModuleScreenSetAsRootWasCalled;
			if (flag)
			{
				this.OnBeforeInitialModuleScreenSetAsRootWasCalled = true;
				bool delayedServiceCreation = this.DelayedServiceCreation;
				if (delayedServiceCreation)
				{
					MCMUISubModule.Logger = ServiceProviderServiceExtensions.GetRequiredService<ILogger<MCMUISubModule>>(DependencyInjectionExtensions.GetServiceProvider(this));
				}
				MCMUISubModule.Extender.Register(typeof(MCMUISubModule).Assembly);
				MCMUISubModule.Extender.Enable();
				HotKeyManager hkm = HotKeyManager.Create("MCM.UI");
				bool flag2 = hkm != null;
				if (flag2)
				{
					MCMUISubModule.ResetValueToDefault = hkm.Add<ResetValueToDefault>();
					hkm.Build();
				}
				ResourceInjector resourceInjector = GenericServiceProvider.GetService<ResourceInjector>();
				if (resourceInjector != null)
				{
					resourceInjector.Inject();
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A0C File Offset: 0x00000C0C
		internal static void UpdateOptionScreen(MCMUISettings settings)
		{
			bool useStandardOptionScreen = settings.UseStandardOptionScreen;
			if (useStandardOptionScreen)
			{
				BaseGameMenuScreenHandler instance = BaseGameMenuScreenHandler.Instance;
				if (instance != null)
				{
					instance.RemoveScreen("MCM_OptionScreen");
				}
			}
			else
			{
				BaseGameMenuScreenHandler instance2 = BaseGameMenuScreenHandler.Instance;
				if (instance2 != null)
				{
					instance2.AddScreen("MCM_OptionScreen", 9990, () => GenericServiceProvider.GetService<IMCMOptionsScreen>() as ScreenBase, new TextObject("{=MainMenu_ModOptions}Mod Options", null));
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A88 File Offset: 0x00000C88
		private static void ValidateLoadOrder()
		{
			List<ModuleInfoExtendedWithMetadata> loadedModules = ModuleInfoHelper.GetLoadedModules().ToList<ModuleInfoExtendedWithMetadata>();
			bool flag = loadedModules.Count == 0;
			if (!flag)
			{
				StringBuilder sb = new StringBuilder();
				string report;
				bool flag2 = !ModuleInfoHelper.ValidateLoadOrder(typeof(MCMUISubModule), out report);
				if (flag2)
				{
					sb.AppendLine(report);
					sb.AppendLine();
					sb.AppendLine(new TextObject("{=eXs6FLm5DP}It's strongly recommended to terminate the game now. Do you wish to terminate it?", null).ToString());
					MessageBoxResult messageBoxResult = MessageBoxDialog.Show(sb.ToString(), new TextObject("{=dzeWx4xSfR}Warning from MCM!", null).ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxModal)262144U);
					MessageBoxResult messageBoxResult2 = messageBoxResult;
					if (messageBoxResult2 == MessageBoxResult.Yes)
					{
						Environment.Exit(1);
					}
				}
			}
		}

		// Token: 0x0400000B RID: 11
		private const string SMessageContinue = "{=eXs6FLm5DP}It's strongly recommended to terminate the game now. Do you wish to terminate it?";

		// Token: 0x0400000C RID: 12
		private const string SWarningTitle = "{=dzeWx4xSfR}Warning from MCM!";

		// Token: 0x0400000D RID: 13
		private static readonly UIExtender Extender = new UIExtender("MCM.UI");

		// Token: 0x0400000E RID: 14
		internal static ILogger<MCMUISubModule> Logger = NullLogger<MCMUISubModule>.Instance;

		// Token: 0x0400000F RID: 15
		[Nullable(2)]
		internal static ResetValueToDefault ResetValueToDefault;
	}
}
