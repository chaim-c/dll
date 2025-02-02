using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.Core
{
	// Token: 0x02000070 RID: 112
	public class GameStateManager
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00018ADD File Offset: 0x00016CDD
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public static GameStateManager Current
		{
			get
			{
				return GameStateManager._current;
			}
			set
			{
				GameStateManager current = GameStateManager._current;
				if (current != null)
				{
					current.CleanStates(0);
				}
				GameStateManager._current = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00018AFD File Offset: 0x00016CFD
		public IReadOnlyCollection<IGameStateManagerListener> Listeners
		{
			get
			{
				return this._listeners.AsReadOnly();
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00018B0A File Offset: 0x00016D0A
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x00018B12 File Offset: 0x00016D12
		public GameStateManager.GameStateManagerType CurrentType { get; private set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x00018B1B File Offset: 0x00016D1B
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x00018B23 File Offset: 0x00016D23
		public IGameStateManagerOwner Owner { get; private set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00018B2C File Offset: 0x00016D2C
		public IEnumerable<GameState> GameStates
		{
			get
			{
				return this._gameStates.AsReadOnly();
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00018B39 File Offset: 0x00016D39
		public bool ActiveStateDisabledByUser
		{
			get
			{
				return this._activeStateDisableRequests.Count > 0;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00018B49 File Offset: 0x00016D49
		public GameState ActiveState
		{
			get
			{
				if (this._gameStates.Count <= 0)
				{
					return null;
				}
				return this._gameStates[this._gameStates.Count - 1];
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00018B74 File Offset: 0x00016D74
		public GameStateManager(IGameStateManagerOwner owner, GameStateManager.GameStateManagerType gameStateManagerType)
		{
			this.Owner = owner;
			this.CurrentType = gameStateManagerType;
			this._gameStateJobs = new Queue<GameStateManager.GameStateJob>();
			this._gameStates = new List<GameState>();
			this._listeners = new List<IGameStateManagerListener>();
			this._activeStateDisableRequests = new List<WeakReference>();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00018BC4 File Offset: 0x00016DC4
		internal GameState FindPredecessor(GameState gameState)
		{
			GameState result = null;
			int num = this._gameStates.IndexOf(gameState);
			if (num > 0)
			{
				result = this._gameStates[num - 1];
			}
			return result;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public bool RegisterListener(IGameStateManagerListener listener)
		{
			if (this._listeners.Contains(listener))
			{
				return false;
			}
			this._listeners.Add(listener);
			return true;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00018C13 File Offset: 0x00016E13
		public bool UnregisterListener(IGameStateManagerListener listener)
		{
			return this._listeners.Remove(listener);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00018C24 File Offset: 0x00016E24
		public T GetListenerOfType<T>()
		{
			using (List<IGameStateManagerListener>.Enumerator enumerator = this._listeners.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGameStateManagerListener gameStateManagerListener;
					if ((gameStateManagerListener = enumerator.Current) is T)
					{
						return (T)((object)gameStateManagerListener);
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00018C90 File Offset: 0x00016E90
		public void RegisterActiveStateDisableRequest(object requestingInstance)
		{
			if (!this._activeStateDisableRequests.Contains(requestingInstance))
			{
				this._activeStateDisableRequests.Add(new WeakReference(requestingInstance));
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00018CB4 File Offset: 0x00016EB4
		public void UnregisterActiveStateDisableRequest(object requestingInstance)
		{
			for (int i = 0; i < this._activeStateDisableRequests.Count; i++)
			{
				WeakReference weakReference = this._activeStateDisableRequests[i];
				if (((weakReference != null) ? weakReference.Target : null) == requestingInstance)
				{
					this._activeStateDisableRequests.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00018D00 File Offset: 0x00016F00
		public void OnSavedGameLoadFinished()
		{
			foreach (IGameStateManagerListener gameStateManagerListener in this._listeners)
			{
				gameStateManagerListener.OnSavedGameLoadFinished();
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00018D50 File Offset: 0x00016F50
		public T LastOrDefault<T>() where T : GameState
		{
			return this._gameStates.LastOrDefault((GameState g) => g is T) as T;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00018D88 File Offset: 0x00016F88
		public T CreateState<T>() where T : GameState, new()
		{
			T t = Activator.CreateInstance<T>();
			this.HandleCreateState(t);
			return t;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00018DA8 File Offset: 0x00016FA8
		public T CreateState<T>(params object[] parameters) where T : GameState, new()
		{
			GameState gameState = (GameState)Activator.CreateInstance(typeof(T), parameters);
			this.HandleCreateState(gameState);
			return (T)((object)gameState);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00018DD8 File Offset: 0x00016FD8
		private void HandleCreateState(GameState state)
		{
			state.GameStateManager = this;
			foreach (IGameStateManagerListener gameStateManagerListener in this._listeners)
			{
				gameStateManagerListener.OnCreateState(state);
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00018E30 File Offset: 0x00017030
		public void OnTick(float dt)
		{
			this.CleanRequests();
			if (this.ActiveState != null)
			{
				if (this.ActiveStateDisabledByUser)
				{
					this.ActiveState.OnIdleTick(dt);
					return;
				}
				this.ActiveState.OnTick(dt);
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00018E64 File Offset: 0x00017064
		private void CleanRequests()
		{
			for (int i = this._activeStateDisableRequests.Count - 1; i >= 0; i--)
			{
				WeakReference weakReference = this._activeStateDisableRequests[i];
				if (weakReference == null || !weakReference.IsAlive)
				{
					this._activeStateDisableRequests.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00018EB4 File Offset: 0x000170B4
		public void PushState(GameState gameState, int level = 0)
		{
			GameStateManager.GameStateJob item = new GameStateManager.GameStateJob(GameStateManager.GameStateJob.JobType.Push, gameState, level);
			this._gameStateJobs.Enqueue(item);
			this.DoGameStateJobs();
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00018EE0 File Offset: 0x000170E0
		public void PopState(int level = 0)
		{
			GameStateManager.GameStateJob item = new GameStateManager.GameStateJob(GameStateManager.GameStateJob.JobType.Pop, null, level);
			this._gameStateJobs.Enqueue(item);
			this.DoGameStateJobs();
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00018F0C File Offset: 0x0001710C
		public void CleanAndPushState(GameState gameState, int level = 0)
		{
			GameStateManager.GameStateJob item = new GameStateManager.GameStateJob(GameStateManager.GameStateJob.JobType.CleanAndPushState, gameState, level);
			this._gameStateJobs.Enqueue(item);
			this.DoGameStateJobs();
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00018F38 File Offset: 0x00017138
		public void CleanStates(int level = 0)
		{
			GameStateManager.GameStateJob item = new GameStateManager.GameStateJob(GameStateManager.GameStateJob.JobType.CleanStates, null, level);
			this._gameStateJobs.Enqueue(item);
			this.DoGameStateJobs();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00018F64 File Offset: 0x00017164
		private void OnPushState(GameState gameState)
		{
			GameState activeState = this.ActiveState;
			bool isTopGameState = this._gameStates.Count == 0;
			int num = this._gameStates.FindLastIndex((GameState state) => state.Level <= gameState.Level);
			if (num == -1)
			{
				this._gameStates.Add(gameState);
			}
			else
			{
				this._gameStates.Insert(num + 1, gameState);
			}
			GameState activeState2 = this.ActiveState;
			if (activeState2 != activeState)
			{
				if (activeState != null && activeState.Activated)
				{
					activeState.HandleDeactivate();
				}
				foreach (IGameStateManagerListener gameStateManagerListener in this._listeners)
				{
					gameStateManagerListener.OnPushState(activeState2, isTopGameState);
				}
				activeState2.HandleInitialize();
				activeState2.HandleActivate();
				this.Owner.OnStateChanged(activeState);
			}
			Common.MemoryCleanupGC(false);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001905C File Offset: 0x0001725C
		private void OnPopState(int level)
		{
			GameState activeState = this.ActiveState;
			int index = this._gameStates.FindLastIndex((GameState state) => state.Level == level);
			GameState gameState = this._gameStates[index];
			gameState.HandleDeactivate();
			gameState.HandleFinalize();
			this._gameStates.RemoveAt(index);
			GameState activeState2 = this.ActiveState;
			foreach (IGameStateManagerListener gameStateManagerListener in this._listeners)
			{
				gameStateManagerListener.OnPopState(gameState);
			}
			if (activeState2 != activeState)
			{
				if (activeState2 != null)
				{
					activeState2.HandleActivate();
				}
				else if (this._gameStateJobs.Count == 0 || (this._gameStateJobs.Peek().Job != GameStateManager.GameStateJob.JobType.Push && this._gameStateJobs.Peek().Job != GameStateManager.GameStateJob.JobType.CleanAndPushState))
				{
					this.Owner.OnStateStackEmpty();
				}
				this.Owner.OnStateChanged(gameState);
			}
			Common.MemoryCleanupGC(false);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001916C File Offset: 0x0001736C
		private void OnCleanAndPushState(GameState gameState)
		{
			int num = -1;
			for (int i = 0; i < this._gameStates.Count; i++)
			{
				if (this._gameStates[i].Level >= gameState.Level)
				{
					num = i - 1;
					break;
				}
			}
			GameState activeState = this.ActiveState;
			for (int j = this._gameStates.Count - 1; j > num; j--)
			{
				GameState gameState2 = this._gameStates[j];
				if (gameState2.Activated)
				{
					gameState2.HandleDeactivate();
				}
				gameState2.HandleFinalize();
				this._gameStates.RemoveAt(j);
			}
			this.OnPushState(gameState);
			this.Owner.OnStateChanged(activeState);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00019214 File Offset: 0x00017414
		private void OnCleanStates(int popLevel)
		{
			int num = -1;
			for (int i = 0; i < this._gameStates.Count; i++)
			{
				if (this._gameStates[i].Level >= popLevel)
				{
					num = i - 1;
					break;
				}
			}
			GameState activeState = this.ActiveState;
			for (int j = this._gameStates.Count - 1; j > num; j--)
			{
				GameState gameState = this._gameStates[j];
				if (gameState.Activated)
				{
					gameState.HandleDeactivate();
				}
				gameState.HandleFinalize();
				this._gameStates.RemoveAt(j);
			}
			foreach (IGameStateManagerListener gameStateManagerListener in this._listeners)
			{
				gameStateManagerListener.OnCleanStates();
			}
			GameState activeState2 = this.ActiveState;
			if (activeState != activeState2)
			{
				if (activeState2 != null)
				{
					activeState2.HandleActivate();
				}
				else if (this._gameStateJobs.Count == 0 || (this._gameStateJobs.Peek().Job != GameStateManager.GameStateJob.JobType.Push && this._gameStateJobs.Peek().Job != GameStateManager.GameStateJob.JobType.CleanAndPushState))
				{
					this.Owner.OnStateStackEmpty();
				}
				this.Owner.OnStateChanged(activeState);
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00019350 File Offset: 0x00017550
		private void DoGameStateJobs()
		{
			while (this._gameStateJobs.Count > 0)
			{
				GameStateManager.GameStateJob gameStateJob = this._gameStateJobs.Dequeue();
				switch (gameStateJob.Job)
				{
				case GameStateManager.GameStateJob.JobType.Push:
					this.OnPushState(gameStateJob.GameState);
					break;
				case GameStateManager.GameStateJob.JobType.Pop:
					this.OnPopState(gameStateJob.PopLevel);
					break;
				case GameStateManager.GameStateJob.JobType.CleanAndPushState:
					this.OnCleanAndPushState(gameStateJob.GameState);
					break;
				case GameStateManager.GameStateJob.JobType.CleanStates:
					this.OnCleanStates(gameStateJob.PopLevel);
					break;
				}
			}
		}

		// Token: 0x040003B3 RID: 947
		private static GameStateManager _current;

		// Token: 0x040003B4 RID: 948
		public static string StateActivateCommand;

		// Token: 0x040003B7 RID: 951
		private readonly List<GameState> _gameStates;

		// Token: 0x040003B8 RID: 952
		private readonly List<IGameStateManagerListener> _listeners;

		// Token: 0x040003B9 RID: 953
		private readonly List<WeakReference> _activeStateDisableRequests;

		// Token: 0x040003BA RID: 954
		private readonly Queue<GameStateManager.GameStateJob> _gameStateJobs;

		// Token: 0x020000F7 RID: 247
		public enum GameStateManagerType
		{
			// Token: 0x040006CA RID: 1738
			Game,
			// Token: 0x040006CB RID: 1739
			Global
		}

		// Token: 0x020000F8 RID: 248
		private struct GameStateJob
		{
			// Token: 0x06000A3E RID: 2622 RVA: 0x00021681 File Offset: 0x0001F881
			public GameStateJob(GameStateManager.GameStateJob.JobType job, GameState gameState, int popLevel)
			{
				this.Job = job;
				this.GameState = gameState;
				this.PopLevel = popLevel;
			}

			// Token: 0x040006CC RID: 1740
			public readonly GameStateManager.GameStateJob.JobType Job;

			// Token: 0x040006CD RID: 1741
			public readonly GameState GameState;

			// Token: 0x040006CE RID: 1742
			public readonly int PopLevel;

			// Token: 0x0200011A RID: 282
			public enum JobType
			{
				// Token: 0x0400074B RID: 1867
				None,
				// Token: 0x0400074C RID: 1868
				Push,
				// Token: 0x0400074D RID: 1869
				Pop,
				// Token: 0x0400074E RID: 1870
				CleanAndPushState,
				// Token: 0x0400074F RID: 1871
				CleanStates
			}
		}
	}
}
