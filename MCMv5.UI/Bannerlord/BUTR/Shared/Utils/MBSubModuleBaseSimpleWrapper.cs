using System;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.BUTR.Shared.Utils
{
	// Token: 0x0200003C RID: 60
	[NullableContext(2)]
	[Nullable(0)]
	public class MBSubModuleBaseSimpleWrapper : MBSubModuleBase
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000959E File Offset: 0x0000779E
		private MBSubModuleBaseSimpleWrapper.OnSubModuleLoadDelegate OnSubModuleLoadInstance { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000095A6 File Offset: 0x000077A6
		private MBSubModuleBaseSimpleWrapper.OnSubModuleUnloadedDelegate OnSubModuleUnloadedInstance { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000095AE File Offset: 0x000077AE
		private MBSubModuleBaseSimpleWrapper.OnBeforeInitialModuleScreenSetAsRootDelegate OnBeforeInitialModuleScreenSetAsRootInstance { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000095B6 File Offset: 0x000077B6
		private MBSubModuleBaseSimpleWrapper.OnGameStartDelegate OnGameStartInstance { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000095BE File Offset: 0x000077BE
		private MBSubModuleBaseSimpleWrapper.OnApplicationTickDelegate OnApplicationTickInstance { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000095C6 File Offset: 0x000077C6
		private MBSubModuleBaseSimpleWrapper.InitializeGameStarterDelegate InitializeGameStarterInstance { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000095CE File Offset: 0x000077CE
		private MBSubModuleBaseSimpleWrapper.AfterRegisterSubModuleObjectsDelegate AfterRegisterSubModuleObjectsInstance { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000095D6 File Offset: 0x000077D6
		private MBSubModuleBaseSimpleWrapper.AfterAsyncTickTickDelegate AfterAsyncTickTickInstance { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000095DE File Offset: 0x000077DE
		[Nullable(1)]
		public MBSubModuleBase SubModule { [NullableContext(1)] get; }

		// Token: 0x06000201 RID: 513 RVA: 0x000095E8 File Offset: 0x000077E8
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

		// Token: 0x06000202 RID: 514 RVA: 0x000096A4 File Offset: 0x000078A4
		protected override void OnSubModuleLoad()
		{
			MBSubModuleBaseSimpleWrapper.OnSubModuleLoadDelegate onSubModuleLoadInstance = this.OnSubModuleLoadInstance;
			if (onSubModuleLoadInstance != null)
			{
				onSubModuleLoadInstance();
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000096B8 File Offset: 0x000078B8
		protected override void OnSubModuleUnloaded()
		{
			MBSubModuleBaseSimpleWrapper.OnSubModuleUnloadedDelegate onSubModuleUnloadedInstance = this.OnSubModuleUnloadedInstance;
			if (onSubModuleUnloadedInstance != null)
			{
				onSubModuleUnloadedInstance();
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000096CC File Offset: 0x000078CC
		protected override void OnApplicationTick(float dt)
		{
			MBSubModuleBaseSimpleWrapper.OnApplicationTickDelegate onApplicationTickInstance = this.OnApplicationTickInstance;
			if (onApplicationTickInstance != null)
			{
				onApplicationTickInstance(dt);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000096E1 File Offset: 0x000078E1
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			MBSubModuleBaseSimpleWrapper.OnBeforeInitialModuleScreenSetAsRootDelegate onBeforeInitialModuleScreenSetAsRootInstance = this.OnBeforeInitialModuleScreenSetAsRootInstance;
			if (onBeforeInitialModuleScreenSetAsRootInstance != null)
			{
				onBeforeInitialModuleScreenSetAsRootInstance();
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000096F5 File Offset: 0x000078F5
		[NullableContext(1)]
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			MBSubModuleBaseSimpleWrapper.OnGameStartDelegate onGameStartInstance = this.OnGameStartInstance;
			if (onGameStartInstance != null)
			{
				onGameStartInstance(game, gameStarterObject);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000970B File Offset: 0x0000790B
		[NullableContext(1)]
		protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
		{
			MBSubModuleBaseSimpleWrapper.InitializeGameStarterDelegate initializeGameStarterInstance = this.InitializeGameStarterInstance;
			if (initializeGameStarterInstance != null)
			{
				initializeGameStarterInstance(game, starterObject);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009721 File Offset: 0x00007921
		protected override void AfterAsyncTickTick(float dt)
		{
			MBSubModuleBaseSimpleWrapper.AfterAsyncTickTickDelegate afterAsyncTickTickInstance = this.AfterAsyncTickTickInstance;
			if (afterAsyncTickTickInstance != null)
			{
				afterAsyncTickTickInstance(dt);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009736 File Offset: 0x00007936
		[NullableContext(1)]
		public override bool DoLoading(Game game)
		{
			return this.SubModule.DoLoading(game);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009744 File Offset: 0x00007944
		[NullableContext(1)]
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			this.SubModule.OnGameLoaded(game, initializerObject);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009754 File Offset: 0x00007954
		[NullableContext(1)]
		public override void OnCampaignStart(Game game, object starterObject)
		{
			this.SubModule.OnCampaignStart(game, starterObject);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009764 File Offset: 0x00007964
		[NullableContext(1)]
		public override void BeginGameStart(Game game)
		{
			this.SubModule.BeginGameStart(game);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009773 File Offset: 0x00007973
		[NullableContext(1)]
		public override void OnGameEnd(Game game)
		{
			this.SubModule.OnGameEnd(game);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009782 File Offset: 0x00007982
		[NullableContext(1)]
		public override void OnGameInitializationFinished(Game game)
		{
			this.SubModule.OnGameInitializationFinished(game);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009791 File Offset: 0x00007991
		[NullableContext(1)]
		public override void OnBeforeMissionBehaviorInitialize(Mission mission)
		{
			this.SubModule.OnBeforeMissionBehaviorInitialize(mission);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000097A0 File Offset: 0x000079A0
		[NullableContext(1)]
		public override void OnMissionBehaviorInitialize(Mission mission)
		{
			this.SubModule.OnMissionBehaviorInitialize(mission);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000097AF File Offset: 0x000079AF
		[NullableContext(1)]
		public override void OnMultiplayerGameStart(Game game, object starterObject)
		{
			this.SubModule.OnMultiplayerGameStart(game, starterObject);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000097BF File Offset: 0x000079BF
		[NullableContext(1)]
		public override void OnNewGameCreated(Game game, object initializerObject)
		{
			this.SubModule.OnNewGameCreated(game, initializerObject);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000097CF File Offset: 0x000079CF
		public override void RegisterSubModuleObjects(bool isSavedCampaign)
		{
			this.SubModule.RegisterSubModuleObjects(isSavedCampaign);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000097DE File Offset: 0x000079DE
		[NullableContext(1)]
		public override void OnAfterGameInitializationFinished(Game game, object starterObject)
		{
			this.SubModule.OnAfterGameInitializationFinished(game, starterObject);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000097EE File Offset: 0x000079EE
		public override void AfterRegisterSubModuleObjects(bool isSavedCampaign)
		{
			MBSubModuleBaseSimpleWrapper.AfterRegisterSubModuleObjectsDelegate afterRegisterSubModuleObjectsInstance = this.AfterRegisterSubModuleObjectsInstance;
			if (afterRegisterSubModuleObjectsInstance != null)
			{
				afterRegisterSubModuleObjectsInstance(isSavedCampaign);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009803 File Offset: 0x00007A03
		public override void OnConfigChanged()
		{
			this.SubModule.OnConfigChanged();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009811 File Offset: 0x00007A11
		public override void OnInitialState()
		{
			this.SubModule.OnInitialState();
		}

		// Token: 0x020000AF RID: 175
		// (Invoke) Token: 0x06000576 RID: 1398
		[NullableContext(0)]
		private delegate void OnSubModuleLoadDelegate();

		// Token: 0x020000B0 RID: 176
		// (Invoke) Token: 0x0600057A RID: 1402
		[NullableContext(0)]
		private delegate void OnSubModuleUnloadedDelegate();

		// Token: 0x020000B1 RID: 177
		// (Invoke) Token: 0x0600057E RID: 1406
		[NullableContext(0)]
		private delegate void OnBeforeInitialModuleScreenSetAsRootDelegate();

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x06000582 RID: 1410
		[NullableContext(0)]
		private delegate void OnGameStartDelegate(Game game, IGameStarter gameStarterObject);

		// Token: 0x020000B3 RID: 179
		// (Invoke) Token: 0x06000586 RID: 1414
		[NullableContext(0)]
		private delegate void OnApplicationTickDelegate(float dt);

		// Token: 0x020000B4 RID: 180
		// (Invoke) Token: 0x0600058A RID: 1418
		[NullableContext(0)]
		private delegate void InitializeGameStarterDelegate(Game game, IGameStarter starterObject);

		// Token: 0x020000B5 RID: 181
		// (Invoke) Token: 0x0600058E RID: 1422
		[NullableContext(0)]
		private delegate void AfterRegisterSubModuleObjectsDelegate(bool isSavedCampaign);

		// Token: 0x020000B6 RID: 182
		// (Invoke) Token: 0x06000592 RID: 1426
		[NullableContext(0)]
		private delegate void AfterAsyncTickTickDelegate(float dt);
	}
}
