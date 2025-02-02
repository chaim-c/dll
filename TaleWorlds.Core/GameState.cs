using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x0200006F RID: 111
	public abstract class GameState : MBObjectBase
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x000187D3 File Offset: 0x000169D3
		public GameState Predecessor
		{
			get
			{
				return this.GameStateManager.FindPredecessor(this);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000187E1 File Offset: 0x000169E1
		public bool IsActive
		{
			get
			{
				return this.GameStateManager != null && this.GameStateManager.ActiveState == this;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x000187FB File Offset: 0x000169FB
		public IReadOnlyCollection<IGameStateListener> Listeners
		{
			get
			{
				return this._listeners.AsReadOnly();
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00018808 File Offset: 0x00016A08
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00018810 File Offset: 0x00016A10
		public GameStateManager GameStateManager { get; internal set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00018819 File Offset: 0x00016A19
		public virtual bool IsMusicMenuState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001881C File Offset: 0x00016A1C
		public virtual bool IsMenuState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001881F File Offset: 0x00016A1F
		public virtual bool IsMission
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00018822 File Offset: 0x00016A22
		protected GameState()
		{
			this._listeners = new List<IGameStateListener>();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00018835 File Offset: 0x00016A35
		public bool RegisterListener(IGameStateListener listener)
		{
			if (this._listeners.Contains(listener))
			{
				return false;
			}
			this._listeners.Add(listener);
			return true;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00018854 File Offset: 0x00016A54
		public bool UnregisterListener(IGameStateListener listener)
		{
			return this._listeners.Remove(listener);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00018864 File Offset: 0x00016A64
		public T GetListenerOfType<T>()
		{
			using (List<IGameStateListener>.Enumerator enumerator = this._listeners.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGameStateListener gameStateListener;
					if ((gameStateListener = enumerator.Current) is T)
					{
						return (T)((object)gameStateListener);
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000188D0 File Offset: 0x00016AD0
		internal void HandleInitialize()
		{
			this.OnInitialize();
			foreach (IGameStateListener gameStateListener in this._listeners)
			{
				gameStateListener.OnInitialize();
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00018928 File Offset: 0x00016B28
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001892C File Offset: 0x00016B2C
		internal void HandleFinalize()
		{
			this.OnFinalize();
			foreach (IGameStateListener gameStateListener in this._listeners)
			{
				gameStateListener.OnFinalize();
			}
			this._listeners = null;
			this.GameStateManager = null;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00018990 File Offset: 0x00016B90
		protected virtual void OnFinalize()
		{
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00018994 File Offset: 0x00016B94
		internal void HandleActivate()
		{
			GameState.NumberOfListenerActivations = 0;
			if (this.IsActive)
			{
				this.OnActivate();
				if (this.IsActive && this._listeners.Count != 0 && GameState.NumberOfListenerActivations == 0)
				{
					foreach (IGameStateListener gameStateListener in this._listeners)
					{
						gameStateListener.OnActivate();
					}
					GameState.NumberOfListenerActivations++;
				}
				if (!string.IsNullOrEmpty(GameStateManager.StateActivateCommand))
				{
					bool flag;
					CommandLineFunctionality.CallFunction(GameStateManager.StateActivateCommand, "", out flag);
				}
				Debug.ReportMemoryBookmark("GameState Activated: " + base.GetType().Name);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00018A5C File Offset: 0x00016C5C
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00018A64 File Offset: 0x00016C64
		public bool Activated { get; private set; }

		// Token: 0x06000747 RID: 1863 RVA: 0x00018A6D File Offset: 0x00016C6D
		protected virtual void OnActivate()
		{
			this.Activated = true;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00018A78 File Offset: 0x00016C78
		internal void HandleDeactivate()
		{
			this.OnDeactivate();
			foreach (IGameStateListener gameStateListener in this._listeners)
			{
				gameStateListener.OnDeactivate();
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00018AD0 File Offset: 0x00016CD0
		protected virtual void OnDeactivate()
		{
			this.Activated = false;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00018AD9 File Offset: 0x00016CD9
		protected internal virtual void OnTick(float dt)
		{
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00018ADB File Offset: 0x00016CDB
		protected internal virtual void OnIdleTick(float dt)
		{
		}

		// Token: 0x040003AE RID: 942
		public int Level;

		// Token: 0x040003AF RID: 943
		private List<IGameStateListener> _listeners;

		// Token: 0x040003B0 RID: 944
		public static int NumberOfListenerActivations;
	}
}
