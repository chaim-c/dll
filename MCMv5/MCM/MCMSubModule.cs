using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Extensions;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Abstractions.Properties;
using MCM.Internal.Extensions;
using MCM.LightInject;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace MCM
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	public class MCMSubModule : MBSubModuleBase
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000223A File Offset: 0x0000043A
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002241 File Offset: 0x00000441
		[Nullable(2)]
		public static MCMSubModule Instance { [NullableContext(2)] get; [NullableContext(2)] private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002249 File Offset: 0x00000449
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002251 File Offset: 0x00000451
		private bool ServiceRegistrationWasCalled { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000225A File Offset: 0x0000045A
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002262 File Offset: 0x00000462
		private bool OnBeforeInitialModuleScreenSetAsRootWasCalled { get; set; }

		// Token: 0x06000015 RID: 21 RVA: 0x0000226B File Offset: 0x0000046B
		public MCMSubModule()
		{
			MCMSubModule.Instance = this;
			BUTR.DependencyInjection.Extensions.ServiceCollectionExtensions.ServiceContainer = new WithHistoryGenericServiceContainer(new LightInjectServiceContainer(MCMSubModule.LightInjectServiceContainer));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002290 File Offset: 0x00000490
		public void OnServiceRegistration()
		{
			this.ServiceRegistrationWasCalled = true;
			IGenericServiceContainer services = this.GetServiceContainer();
			bool flag = services != null;
			if (flag)
			{
				services.AddSettingsFormat<MemorySettingsFormat>();
				services.AddSettingsFormat<MemorySettingsFormat>();
				services.AddSettingsPropertyDiscoverer<NoneSettingsPropertyDiscoverer>();
				services.AddTransient<IBUTRLogger, DefaultBUTRLogger>();
				services.AddTransient(typeof(IBUTRLogger<>), typeof(DefaultBUTRLogger<>));
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022F0 File Offset: 0x000004F0
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			bool flag = !this.ServiceRegistrationWasCalled;
			if (flag)
			{
				this.OnServiceRegistration();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002319 File Offset: 0x00000519
		protected override void OnSubModuleUnloaded()
		{
			base.OnSubModuleUnloaded();
			MCMSubModule.Instance = null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000232C File Offset: 0x0000052C
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			bool flag = !this.OnBeforeInitialModuleScreenSetAsRootWasCalled;
			if (flag)
			{
				this.OnBeforeInitialModuleScreenSetAsRootWasCalled = true;
				GenericServiceProvider.GlobalServiceProvider = BUTR.DependencyInjection.Extensions.ServiceCollectionExtensions.ServiceContainer.Build();
				MCMSubModule.Logger = (GenericServiceProvider.GetService<IBUTRLogger<MCMSubModule>>() ?? MCMSubModule.Logger);
				BaseSettingsProvider.Instance = GenericServiceProvider.GetService<BaseSettingsProvider>();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002388 File Offset: 0x00000588
		public void OverrideServiceContainer(IGenericServiceContainer serviceContainer)
		{
			WithHistoryGenericServiceContainer oldServiceContainer = BUTR.DependencyInjection.Extensions.ServiceCollectionExtensions.ServiceContainer;
			bool flag = oldServiceContainer != null;
			if (flag)
			{
				BUTR.DependencyInjection.Extensions.ServiceCollectionExtensions.ServiceContainer = new WithHistoryGenericServiceContainer(serviceContainer);
				foreach (Action<IGenericServiceContainer> historyAction in oldServiceContainer.History)
				{
					historyAction(BUTR.DependencyInjection.Extensions.ServiceCollectionExtensions.ServiceContainer);
				}
			}
			else
			{
				BUTR.DependencyInjection.Extensions.ServiceCollectionExtensions.ServiceContainer = new WithHistoryGenericServiceContainer(serviceContainer);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002410 File Offset: 0x00000610
		public override void OnCampaignStart(Game game, object starterObject)
		{
			base.OnCampaignStart(game, starterObject);
			CampaignGameStarter campaignGameStarter = starterObject as CampaignGameStarter;
			bool flag = campaignGameStarter != null;
			if (flag)
			{
				campaignGameStarter.AddBehavior(new SettingsProviderCampaignBehavior(GenericServiceProvider.GetService<BaseSettingsProvider>()));
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002449 File Offset: 0x00000649
		public override void OnMissionBehaviorInitialize(Mission mission)
		{
			base.OnMissionBehaviorInitialize(mission);
			mission.AddMissionBehavior(new SettingsProviderMissionBehavior(GenericServiceProvider.GetService<BaseSettingsProvider>()));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002465 File Offset: 0x00000665
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			base.OnGameStart(game, gameStarterObject);
			GenericServiceProvider.GameScopeServiceProvider = GenericServiceProvider.CreateScope();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000247B File Offset: 0x0000067B
		public override void OnGameEnd(Game game)
		{
			base.OnGameEnd(game);
			IGenericServiceProviderScope gameScopeServiceProvider = GenericServiceProvider.GameScopeServiceProvider;
			if (gameScopeServiceProvider != null)
			{
				gameScopeServiceProvider.Dispose();
			}
			GenericServiceProvider.GameScopeServiceProvider = null;
		}

		// Token: 0x04000007 RID: 7
		private static IBUTRLogger<MCMSubModule> Logger = new DefaultBUTRLogger<MCMSubModule>();

		// Token: 0x04000008 RID: 8
		private static readonly ServiceContainer LightInjectServiceContainer = new ServiceContainer(delegate(ContainerOptions options)
		{
			options.EnableCurrentScope = false;
		});
	}
}
