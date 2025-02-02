using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ObjectSystem;
using TaleWorlds.PlatformService;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C6 RID: 454
	public abstract class MBGameManager : GameManagerBase
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0005BDFD File Offset: 0x00059FFD
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0005BE05 File Offset: 0x0005A005
		public bool IsEnding { get; private set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0005BE0E File Offset: 0x0005A00E
		public new static MBGameManager Current
		{
			get
			{
				return (MBGameManager)GameManagerBase.Current;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0005BE1A File Offset: 0x0005A01A
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x0005BE22 File Offset: 0x0005A022
		public bool IsLoaded { get; protected set; }

		// Token: 0x06001A04 RID: 6660 RVA: 0x0005BE2B File Offset: 0x0005A02B
		protected MBGameManager()
		{
			this.IsEnding = false;
			NativeConfig.OnConfigChanged();
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0005BE4A File Offset: 0x0005A04A
		protected static void StartNewGame()
		{
			MBAPI.IMBGame.StartNew();
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0005BE56 File Offset: 0x0005A056
		protected static void LoadModuleData(bool isLoadGame)
		{
			MBAPI.IMBGame.LoadModuleData(isLoadGame);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0005BE64 File Offset: 0x0005A064
		public static void StartNewGame(MBGameManager gameLoader)
		{
			GameLoadingState gameLoadingState = GameStateManager.Current.CreateState<GameLoadingState>();
			gameLoadingState.SetLoadingParameters(gameLoader);
			GameStateManager.Current.CleanAndPushState(gameLoadingState, 0);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0005BE90 File Offset: 0x0005A090
		public override void BeginGameStart(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.BeginGameStart(game);
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0005BEE8 File Offset: 0x0005A0E8
		public override void OnNewCampaignStart(Game game, object starterObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnCampaignStart(game, starterObject);
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0005BF40 File Offset: 0x0005A140
		public override void RegisterSubModuleObjects(bool isSavedCampaign)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.RegisterSubModuleObjects(isSavedCampaign);
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0005BF98 File Offset: 0x0005A198
		public override void AfterRegisterSubModuleObjects(bool isSavedCampaign)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.AfterRegisterSubModuleObjects(isSavedCampaign);
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0005BFF0 File Offset: 0x0005A1F0
		public override void InitializeGameStarter(Game game, IGameStarter starterObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.InitializeGameStarter(game, starterObject);
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0005C048 File Offset: 0x0005A248
		public override void OnGameInitializationFinished(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnGameInitializationFinished(game);
			}
			foreach (SkeletonScale skeletonScale in Game.Current.ObjectManager.GetObjectTypeList<SkeletonScale>())
			{
				sbyte[] array = new sbyte[skeletonScale.BoneNames.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Skeleton.GetBoneIndexFromName(skeletonScale.SkeletonModel, skeletonScale.BoneNames[i]);
				}
				skeletonScale.SetBoneIndices(array);
			}
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0005C128 File Offset: 0x0005A328
		public override void OnAfterGameInitializationFinished(Game game, object initializerObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnAfterGameInitializationFinished(game, initializerObject);
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0005C180 File Offset: 0x0005A380
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnGameLoaded(game, initializerObject);
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0005C1D8 File Offset: 0x0005A3D8
		public override void OnNewGameCreated(Game game, object initializerObject)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnNewGameCreated(game, initializerObject);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0005C230 File Offset: 0x0005A430
		public override void OnGameStart(Game game, IGameStarter gameStarter)
		{
			Game.Current.MonsterMissionDataCreator = new MonsterMissionDataCreator();
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnGameStart(game, gameStarter);
			}
			Game.Current.AddGameModelsManager<MissionGameModels>(gameStarter.Models);
			Monster.GetBoneIndexWithId = new Func<string, string, sbyte>(MBActionSet.GetBoneIndexWithId);
			Monster.GetBoneHasParentBone = new Func<string, sbyte, bool>(MBActionSet.GetBoneHasParentBone);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0005C2C8 File Offset: 0x0005A4C8
		public override void OnGameEnd(Game game)
		{
			foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
			{
				mbsubModuleBase.OnGameEnd(game);
			}
			MissionGameModels.Clear();
			base.OnGameEnd(game);
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0005C32C File Offset: 0x0005A52C
		public static async void EndGame()
		{
			for (;;)
			{
				MBGameManager mbgameManager = MBGameManager.Current;
				if (mbgameManager == null || mbgameManager.IsLoaded)
				{
					break;
				}
				await Task.Delay(100);
			}
			MBGameManager mbgameManager2 = MBGameManager.Current;
			if (mbgameManager2 == null || mbgameManager2.CheckAndSetEnding())
			{
				if (Game.Current.GameStateManager != null)
				{
					while (Mission.Current != null && !(Game.Current.GameStateManager.ActiveState is MissionState))
					{
						Game.Current.GameStateManager.PopState(0);
					}
					if (Game.Current.GameStateManager.ActiveState is MissionState)
					{
						((MissionState)Game.Current.GameStateManager.ActiveState).CurrentMission.EndMission();
						while (Mission.Current != null)
						{
							await Task.Delay(1);
						}
					}
					else
					{
						Game.Current.GameStateManager.CleanStates(0);
					}
				}
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0005C35D File Offset: 0x0005A55D
		public override void OnLoadFinished()
		{
			this.IsLoaded = true;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0005C368 File Offset: 0x0005A568
		public bool CheckAndSetEnding()
		{
			object lockObject = this._lockObject;
			bool result;
			lock (lockObject)
			{
				if (this.IsEnding)
				{
					result = false;
				}
				else
				{
					this.IsEnding = true;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0005C3B8 File Offset: 0x0005A5B8
		public virtual void OnSessionInvitationAccepted(SessionInvitationType targetGameType)
		{
			if (targetGameType != SessionInvitationType.None)
			{
				MBGameManager.EndGame();
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0005C3C2 File Offset: 0x0005A5C2
		public virtual void OnPlatformRequestedMultiplayer()
		{
			MBGameManager.EndGame();
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0005C3C9 File Offset: 0x0005A5C9
		protected List<MbObjectXmlInformation> GetXmlInformationFromModule()
		{
			return XmlResource.XmlInformationList;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0005C3D0 File Offset: 0x0005A5D0
		public override float ApplicationTime
		{
			get
			{
				return MBCommon.GetApplicationTime();
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0005C3D7 File Offset: 0x0005A5D7
		public override bool CheatMode
		{
			get
			{
				return NativeConfig.CheatMode;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0005C3DE File Offset: 0x0005A5DE
		public override bool IsDevelopmentMode
		{
			get
			{
				return NativeConfig.IsDevelopmentMode;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x0005C3E5 File Offset: 0x0005A5E5
		public override bool IsEditModeOn
		{
			get
			{
				return MBEditor.IsEditModeOn;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0005C3EC File Offset: 0x0005A5EC
		public override UnitSpawnPrioritizations UnitSpawnPrioritization
		{
			get
			{
				return (UnitSpawnPrioritizations)BannerlordConfig.UnitSpawnPrioritization;
			}
		}

		// Token: 0x040007EE RID: 2030
		private readonly object _lockObject = new object();
	}
}
