using System;
using System.Collections.Generic;

namespace TaleWorlds.Core
{
	// Token: 0x02000066 RID: 102
	public abstract class GameManagerBase
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00018285 File Offset: 0x00016485
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0001828C File Offset: 0x0001648C
		public static GameManagerBase Current { get; private set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00018294 File Offset: 0x00016494
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0001829C File Offset: 0x0001649C
		public Game Game
		{
			get
			{
				return this._game;
			}
			internal set
			{
				if (value == null)
				{
					this._game = null;
					this._initialized = false;
					return;
				}
				this._game = value;
				this.Initialize();
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000182BD File Offset: 0x000164BD
		public void Initialize()
		{
			if (!this._initialized)
			{
				this._initialized = true;
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000182CE File Offset: 0x000164CE
		protected GameManagerBase()
		{
			GameManagerBase.Current = this;
			this._entitySystem = new EntitySystem<GameManagerComponent>();
			this._stepNo = GameManagerLoadingSteps.PreInitializeZerothStep;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000182EE File Offset: 0x000164EE
		public IEnumerable<GameManagerComponent> Components
		{
			get
			{
				return this._entitySystem.Components;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000182FB File Offset: 0x000164FB
		public GameManagerComponent AddComponent(Type componentType)
		{
			GameManagerComponent gameManagerComponent = this._entitySystem.AddComponent(componentType);
			gameManagerComponent.GameManager = this;
			return gameManagerComponent;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00018310 File Offset: 0x00016510
		public T AddComponent<T>() where T : GameManagerComponent, new()
		{
			return (T)((object)this.AddComponent(typeof(T)));
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00018327 File Offset: 0x00016527
		public GameManagerComponent GetComponent(Type componentType)
		{
			return this._entitySystem.GetComponent(componentType);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00018335 File Offset: 0x00016535
		public T GetComponent<T>() where T : GameManagerComponent
		{
			return this._entitySystem.GetComponent<T>();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00018342 File Offset: 0x00016542
		public IEnumerable<T> GetComponents<T>() where T : GameManagerComponent
		{
			return this._entitySystem.GetComponents<T>();
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00018350 File Offset: 0x00016550
		public void RemoveComponent<T>() where T : GameManagerComponent
		{
			T component = this._entitySystem.GetComponent<T>();
			this.RemoveComponent(component);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00018375 File Offset: 0x00016575
		public void RemoveComponent(GameManagerComponent component)
		{
			this._entitySystem.RemoveComponent(component);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00018384 File Offset: 0x00016584
		public void OnTick(float dt)
		{
			foreach (GameManagerComponent gameManagerComponent in this._entitySystem.Components)
			{
				gameManagerComponent.OnTick();
			}
			if (this.Game != null)
			{
				this.Game.OnTick(dt);
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000183F0 File Offset: 0x000165F0
		public void OnGameNetworkBegin()
		{
			foreach (GameManagerComponent gameManagerComponent in this._entitySystem.Components)
			{
				gameManagerComponent.OnGameNetworkBegin();
			}
			if (this.Game != null)
			{
				this.Game.OnGameNetworkBegin();
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00018458 File Offset: 0x00016658
		public void OnGameNetworkEnd()
		{
			foreach (GameManagerComponent gameManagerComponent in this._entitySystem.Components)
			{
				gameManagerComponent.OnGameNetworkEnd();
			}
			if (this.Game != null)
			{
				this.Game.OnGameNetworkEnd();
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000184C0 File Offset: 0x000166C0
		public void OnPlayerConnect(VirtualPlayer peer)
		{
			foreach (GameManagerComponent gameManagerComponent in this._entitySystem.Components)
			{
				gameManagerComponent.OnEarlyPlayerConnect(peer);
			}
			if (this.Game != null)
			{
				this.Game.OnEarlyPlayerConnect(peer);
			}
			foreach (GameManagerComponent gameManagerComponent2 in this._entitySystem.Components)
			{
				gameManagerComponent2.OnPlayerConnect(peer);
			}
			if (this.Game != null)
			{
				this.Game.OnPlayerConnect(peer);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00018584 File Offset: 0x00016784
		public void OnPlayerDisconnect(VirtualPlayer peer)
		{
			foreach (GameManagerComponent gameManagerComponent in this._entitySystem.Components)
			{
				gameManagerComponent.OnPlayerDisconnect(peer);
			}
			if (this.Game != null)
			{
				this.Game.OnPlayerDisconnect(peer);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000185F0 File Offset: 0x000167F0
		public virtual void OnGameEnd(Game game)
		{
			GameManagerBase.Current = null;
			this.Game = null;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000185FF File Offset: 0x000167FF
		protected virtual void DoLoadingForGameManager(GameManagerLoadingSteps gameManagerLoadingStep, out GameManagerLoadingSteps nextStep)
		{
			nextStep = GameManagerLoadingSteps.None;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00018604 File Offset: 0x00016804
		public bool DoLoadingForGameManager()
		{
			bool result = false;
			GameManagerLoadingSteps gameManagerLoadingSteps = GameManagerLoadingSteps.None;
			switch (this._stepNo)
			{
			case GameManagerLoadingSteps.PreInitializeZerothStep:
				this.DoLoadingForGameManager(GameManagerLoadingSteps.PreInitializeZerothStep, out gameManagerLoadingSteps);
				if (gameManagerLoadingSteps == GameManagerLoadingSteps.FirstInitializeFirstStep)
				{
					this._stepNo++;
				}
				break;
			case GameManagerLoadingSteps.FirstInitializeFirstStep:
				this.DoLoadingForGameManager(GameManagerLoadingSteps.FirstInitializeFirstStep, out gameManagerLoadingSteps);
				if (gameManagerLoadingSteps == GameManagerLoadingSteps.WaitSecondStep)
				{
					this._stepNo++;
				}
				break;
			case GameManagerLoadingSteps.WaitSecondStep:
				this.DoLoadingForGameManager(GameManagerLoadingSteps.WaitSecondStep, out gameManagerLoadingSteps);
				if (gameManagerLoadingSteps == GameManagerLoadingSteps.SecondInitializeThirdState)
				{
					this._stepNo++;
				}
				break;
			case GameManagerLoadingSteps.SecondInitializeThirdState:
				this.DoLoadingForGameManager(GameManagerLoadingSteps.SecondInitializeThirdState, out gameManagerLoadingSteps);
				if (gameManagerLoadingSteps == GameManagerLoadingSteps.PostInitializeFourthState)
				{
					this._stepNo++;
				}
				break;
			case GameManagerLoadingSteps.PostInitializeFourthState:
				this.DoLoadingForGameManager(GameManagerLoadingSteps.PostInitializeFourthState, out gameManagerLoadingSteps);
				if (gameManagerLoadingSteps == GameManagerLoadingSteps.FinishLoadingFifthStep)
				{
					this._stepNo++;
				}
				break;
			case GameManagerLoadingSteps.FinishLoadingFifthStep:
				this.DoLoadingForGameManager(GameManagerLoadingSteps.FinishLoadingFifthStep, out gameManagerLoadingSteps);
				if (gameManagerLoadingSteps == GameManagerLoadingSteps.None)
				{
					this._stepNo++;
					result = true;
				}
				break;
			case GameManagerLoadingSteps.LoadingIsOver:
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00018702 File Offset: 0x00016902
		public virtual void OnLoadFinished()
		{
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00018704 File Offset: 0x00016904
		public virtual void InitializeGameStarter(Game game, IGameStarter starterObject)
		{
		}

		// Token: 0x06000709 RID: 1801
		public abstract void OnGameStart(Game game, IGameStarter gameStarter);

		// Token: 0x0600070A RID: 1802
		public abstract void BeginGameStart(Game game);

		// Token: 0x0600070B RID: 1803
		public abstract void OnNewCampaignStart(Game game, object starterObject);

		// Token: 0x0600070C RID: 1804
		public abstract void OnAfterCampaignStart(Game game);

		// Token: 0x0600070D RID: 1805
		public abstract void RegisterSubModuleObjects(bool isSavedCampaign);

		// Token: 0x0600070E RID: 1806
		public abstract void AfterRegisterSubModuleObjects(bool isSavedCampaign);

		// Token: 0x0600070F RID: 1807
		public abstract void OnGameInitializationFinished(Game game);

		// Token: 0x06000710 RID: 1808
		public abstract void OnNewGameCreated(Game game, object initializerObject);

		// Token: 0x06000711 RID: 1809
		public abstract void OnGameLoaded(Game game, object initializerObject);

		// Token: 0x06000712 RID: 1810
		public abstract void OnAfterGameInitializationFinished(Game game, object initializerObject);

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000713 RID: 1811
		public abstract float ApplicationTime { get; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000714 RID: 1812
		public abstract bool CheatMode { get; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000715 RID: 1813
		public abstract bool IsDevelopmentMode { get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000716 RID: 1814
		public abstract bool IsEditModeOn { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000717 RID: 1815
		public abstract UnitSpawnPrioritizations UnitSpawnPrioritization { get; }

		// Token: 0x040003A7 RID: 935
		private EntitySystem<GameManagerComponent> _entitySystem;

		// Token: 0x040003A8 RID: 936
		private GameManagerLoadingSteps _stepNo;

		// Token: 0x040003AA RID: 938
		private Game _game;

		// Token: 0x040003AB RID: 939
		private bool _initialized;
	}
}
