using System;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.BUTR.Shared.Utils
{
	// Token: 0x02000129 RID: 297
	[NullableContext(2)]
	[Nullable(0)]
	public class MBSubModuleBaseSimpleWrapper : MBSubModuleBase
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00017262 File Offset: 0x00015462
		private MBSubModuleBaseSimpleWrapper.OnSubModuleLoadDelegate OnSubModuleLoadInstance { get; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001726A File Offset: 0x0001546A
		private MBSubModuleBaseSimpleWrapper.OnSubModuleUnloadedDelegate OnSubModuleUnloadedInstance { get; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00017272 File Offset: 0x00015472
		private MBSubModuleBaseSimpleWrapper.OnBeforeInitialModuleScreenSetAsRootDelegate OnBeforeInitialModuleScreenSetAsRootInstance { get; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0001727A File Offset: 0x0001547A
		private MBSubModuleBaseSimpleWrapper.OnGameStartDelegate OnGameStartInstance { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00017282 File Offset: 0x00015482
		private MBSubModuleBaseSimpleWrapper.OnApplicationTickDelegate OnApplicationTickInstance { get; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0001728A File Offset: 0x0001548A
		private MBSubModuleBaseSimpleWrapper.InitializeGameStarterDelegate InitializeGameStarterInstance { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00017292 File Offset: 0x00015492
		private MBSubModuleBaseSimpleWrapper.AfterRegisterSubModuleObjectsDelegate AfterRegisterSubModuleObjectsInstance { get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0001729A File Offset: 0x0001549A
		private MBSubModuleBaseSimpleWrapper.AfterAsyncTickTickDelegate AfterAsyncTickTickInstance { get; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000172A2 File Offset: 0x000154A2
		[Nullable(1)]
		public MBSubModuleBase SubModule { [NullableContext(1)] get; }

		// Token: 0x0600070F RID: 1807 RVA: 0x000172AC File Offset: 0x000154AC
		[NullableContext(1)]
		public MBSubModuleBaseSimpleWrapper(MBSubModuleBase subModule)
		{
			this.SubModule = subModule;
			this.OnSubModuleLoadInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.OnSubModuleLoadDelegate, MBSubModuleBase>(subModule, "OnSubModuleLoad", null, null, true);
			this.OnSubModuleUnloadedInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.OnSubModuleUnloadedDelegate, MBSubModuleBase>(subModule, "OnSubModuleUnloaded", null, null, true);
			this.OnBeforeInitialModuleScreenSetAsRootInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.OnBeforeInitialModuleScreenSetAsRootDelegate, MBSubModuleBase>(subModule, "OnBeforeInitialModuleScreenSetAsRoot", null, null, true);
			this.OnGameStartInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.OnGameStartDelegate, MBSubModuleBase>(subModule, "OnGameStart", null, null, true);
			this.OnApplicationTickInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.OnApplicationTickDelegate, MBSubModuleBase>(subModule, "OnApplicationTick", null, null, true);
			this.InitializeGameStarterInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.InitializeGameStarterDelegate, MBSubModuleBase>(subModule, "InitializeGameStarter", null, null, true);
			this.AfterRegisterSubModuleObjectsInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.AfterRegisterSubModuleObjectsDelegate, MBSubModuleBase>(subModule, "AfterRegisterSubModuleObjects", null, null, true);
			this.AfterAsyncTickTickInstance = AccessTools2.GetDelegate<MBSubModuleBaseSimpleWrapper.AfterAsyncTickTickDelegate, MBSubModuleBase>(subModule, "AfterAsyncTickTick", null, null, true);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00017368 File Offset: 0x00015568
		protected override void OnSubModuleLoad()
		{
			MBSubModuleBaseSimpleWrapper.OnSubModuleLoadDelegate onSubModuleLoadInstance = this.OnSubModuleLoadInstance;
			if (onSubModuleLoadInstance != null)
			{
				onSubModuleLoadInstance();
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001737C File Offset: 0x0001557C
		protected override void OnSubModuleUnloaded()
		{
			MBSubModuleBaseSimpleWrapper.OnSubModuleUnloadedDelegate onSubModuleUnloadedInstance = this.OnSubModuleUnloadedInstance;
			if (onSubModuleUnloadedInstance != null)
			{
				onSubModuleUnloadedInstance();
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00017390 File Offset: 0x00015590
		protected override void OnApplicationTick(float dt)
		{
			MBSubModuleBaseSimpleWrapper.OnApplicationTickDelegate onApplicationTickInstance = this.OnApplicationTickInstance;
			if (onApplicationTickInstance != null)
			{
				onApplicationTickInstance(dt);
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000173A5 File Offset: 0x000155A5
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			MBSubModuleBaseSimpleWrapper.OnBeforeInitialModuleScreenSetAsRootDelegate onBeforeInitialModuleScreenSetAsRootInstance = this.OnBeforeInitialModuleScreenSetAsRootInstance;
			if (onBeforeInitialModuleScreenSetAsRootInstance != null)
			{
				onBeforeInitialModuleScreenSetAsRootInstance();
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000173B9 File Offset: 0x000155B9
		[NullableContext(1)]
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			MBSubModuleBaseSimpleWrapper.OnGameStartDelegate onGameStartInstance = this.OnGameStartInstance;
			if (onGameStartInstance != null)
			{
				onGameStartInstance(game, gameStarterObject);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000173CF File Offset: 0x000155CF
		[NullableContext(1)]
		protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
		{
			MBSubModuleBaseSimpleWrapper.InitializeGameStarterDelegate initializeGameStarterInstance = this.InitializeGameStarterInstance;
			if (initializeGameStarterInstance != null)
			{
				initializeGameStarterInstance(game, starterObject);
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x000173E5 File Offset: 0x000155E5
		protected override void AfterAsyncTickTick(float dt)
		{
			MBSubModuleBaseSimpleWrapper.AfterAsyncTickTickDelegate afterAsyncTickTickInstance = this.AfterAsyncTickTickInstance;
			if (afterAsyncTickTickInstance != null)
			{
				afterAsyncTickTickInstance(dt);
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000173FA File Offset: 0x000155FA
		[NullableContext(1)]
		public override bool DoLoading(Game game)
		{
			return this.SubModule.DoLoading(game);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00017408 File Offset: 0x00015608
		[NullableContext(1)]
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			this.SubModule.OnGameLoaded(game, initializerObject);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00017418 File Offset: 0x00015618
		[NullableContext(1)]
		public override void OnCampaignStart(Game game, object starterObject)
		{
			this.SubModule.OnCampaignStart(game, starterObject);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00017428 File Offset: 0x00015628
		[NullableContext(1)]
		public override void BeginGameStart(Game game)
		{
			this.SubModule.BeginGameStart(game);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00017437 File Offset: 0x00015637
		[NullableContext(1)]
		public override void OnGameEnd(Game game)
		{
			this.SubModule.OnGameEnd(game);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00017446 File Offset: 0x00015646
		[NullableContext(1)]
		public override void OnGameInitializationFinished(Game game)
		{
			this.SubModule.OnGameInitializationFinished(game);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00017455 File Offset: 0x00015655
		[NullableContext(1)]
		public override void OnBeforeMissionBehaviorInitialize(Mission mission)
		{
			this.SubModule.OnBeforeMissionBehaviorInitialize(mission);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00017464 File Offset: 0x00015664
		[NullableContext(1)]
		public override void OnMissionBehaviorInitialize(Mission mission)
		{
			this.SubModule.OnMissionBehaviorInitialize(mission);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00017473 File Offset: 0x00015673
		[NullableContext(1)]
		public override void OnMultiplayerGameStart(Game game, object starterObject)
		{
			this.SubModule.OnMultiplayerGameStart(game, starterObject);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00017483 File Offset: 0x00015683
		[NullableContext(1)]
		public override void OnNewGameCreated(Game game, object initializerObject)
		{
			this.SubModule.OnNewGameCreated(game, initializerObject);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00017493 File Offset: 0x00015693
		public override void RegisterSubModuleObjects(bool isSavedCampaign)
		{
			this.SubModule.RegisterSubModuleObjects(isSavedCampaign);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000174A2 File Offset: 0x000156A2
		[NullableContext(1)]
		public override void OnAfterGameInitializationFinished(Game game, object starterObject)
		{
			this.SubModule.OnAfterGameInitializationFinished(game, starterObject);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000174B2 File Offset: 0x000156B2
		public override void AfterRegisterSubModuleObjects(bool isSavedCampaign)
		{
			MBSubModuleBaseSimpleWrapper.AfterRegisterSubModuleObjectsDelegate afterRegisterSubModuleObjectsInstance = this.AfterRegisterSubModuleObjectsInstance;
			if (afterRegisterSubModuleObjectsInstance != null)
			{
				afterRegisterSubModuleObjectsInstance(isSavedCampaign);
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000174C7 File Offset: 0x000156C7
		public override void OnConfigChanged()
		{
			this.SubModule.OnConfigChanged();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000174D5 File Offset: 0x000156D5
		public override void OnInitialState()
		{
			this.SubModule.OnInitialState();
		}

		// Token: 0x02000247 RID: 583
		// (Invoke) Token: 0x06000D57 RID: 3415
		[NullableContext(0)]
		private delegate void OnSubModuleLoadDelegate();

		// Token: 0x02000248 RID: 584
		// (Invoke) Token: 0x06000D5B RID: 3419
		[NullableContext(0)]
		private delegate void OnSubModuleUnloadedDelegate();

		// Token: 0x02000249 RID: 585
		// (Invoke) Token: 0x06000D5F RID: 3423
		[NullableContext(0)]
		private delegate void OnBeforeInitialModuleScreenSetAsRootDelegate();

		// Token: 0x0200024A RID: 586
		// (Invoke) Token: 0x06000D63 RID: 3427
		[NullableContext(0)]
		private delegate void OnGameStartDelegate(Game game, IGameStarter gameStarterObject);

		// Token: 0x0200024B RID: 587
		// (Invoke) Token: 0x06000D67 RID: 3431
		[NullableContext(0)]
		private delegate void OnApplicationTickDelegate(float dt);

		// Token: 0x0200024C RID: 588
		// (Invoke) Token: 0x06000D6B RID: 3435
		[NullableContext(0)]
		private delegate void InitializeGameStarterDelegate(Game game, IGameStarter starterObject);

		// Token: 0x0200024D RID: 589
		// (Invoke) Token: 0x06000D6F RID: 3439
		[NullableContext(0)]
		private delegate void AfterRegisterSubModuleObjectsDelegate(bool isSavedCampaign);

		// Token: 0x0200024E RID: 590
		// (Invoke) Token: 0x06000D73 RID: 3443
		[NullableContext(0)]
		private delegate void AfterAsyncTickTickDelegate(float dt);
	}
}
