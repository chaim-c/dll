using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.GamepadNavigation
{
	// Token: 0x02000049 RID: 73
	internal class GamepadNavigationScopeCollection
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00013665 File Offset: 0x00011865
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0001366D File Offset: 0x0001186D
		public IGamepadNavigationContext Source { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00013676 File Offset: 0x00011876
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001367E File Offset: 0x0001187E
		public ReadOnlyCollection<GamepadNavigationScope> AllScopes { get; private set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00013687 File Offset: 0x00011887
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001368F File Offset: 0x0001188F
		public ReadOnlyCollection<GamepadNavigationScope> UninitializedScopes { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00013698 File Offset: 0x00011898
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x000136A0 File Offset: 0x000118A0
		public ReadOnlyCollection<GamepadNavigationScope> VisibleScopes { get; private set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000136A9 File Offset: 0x000118A9
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000136B1 File Offset: 0x000118B1
		public ReadOnlyCollection<GamepadNavigationScope> InvisibleScopes { get; private set; }

		// Token: 0x060004CB RID: 1227 RVA: 0x000136BC File Offset: 0x000118BC
		public GamepadNavigationScopeCollection(IGamepadNavigationContext source, Action<GamepadNavigationScope> onScopeNavigatableWidgetsChanged, Action<GamepadNavigationScope, bool> onScopeVisibilityChanged)
		{
			this._onScopeNavigatableWidgetsChanged = onScopeNavigatableWidgetsChanged;
			this._onScopeVisibilityChanged = onScopeVisibilityChanged;
			this.Source = source;
			this._allScopes = new List<GamepadNavigationScope>();
			this.AllScopes = new ReadOnlyCollection<GamepadNavigationScope>(this._allScopes);
			this._uninitializedScopes = new List<GamepadNavigationScope>();
			this.UninitializedScopes = new ReadOnlyCollection<GamepadNavigationScope>(this._uninitializedScopes);
			this._visibleScopes = new List<GamepadNavigationScope>();
			this.VisibleScopes = new ReadOnlyCollection<GamepadNavigationScope>(this._visibleScopes);
			this._invisibleScopes = new List<GamepadNavigationScope>();
			this.InvisibleScopes = new ReadOnlyCollection<GamepadNavigationScope>(this._invisibleScopes);
			this._dirtyScopes = new List<GamepadNavigationScope>();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001375F File Offset: 0x0001195F
		internal void OnFinalize()
		{
			this.ClearAllScopes();
			this._onScopeVisibilityChanged = null;
			this._onScopeNavigatableWidgetsChanged = null;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013778 File Offset: 0x00011978
		internal void HandleScopeVisibilities()
		{
			List<GamepadNavigationScope> dirtyScopes = this._dirtyScopes;
			lock (dirtyScopes)
			{
				for (int i = 0; i < this._dirtyScopes.Count; i++)
				{
					if (this._dirtyScopes[i] != null)
					{
						for (int j = i + 1; j < this._dirtyScopes.Count; j++)
						{
							if (this._dirtyScopes[i] == this._dirtyScopes[j])
							{
								this._dirtyScopes[j] = null;
							}
						}
					}
				}
				foreach (GamepadNavigationScope gamepadNavigationScope in this._dirtyScopes)
				{
					if (gamepadNavigationScope != null)
					{
						bool flag2 = gamepadNavigationScope.IsVisible();
						this._visibleScopes.Remove(gamepadNavigationScope);
						this._invisibleScopes.Remove(gamepadNavigationScope);
						if (flag2)
						{
							this._visibleScopes.Add(gamepadNavigationScope);
						}
						else
						{
							this._invisibleScopes.Add(gamepadNavigationScope);
						}
						this._onScopeVisibilityChanged(gamepadNavigationScope, flag2);
					}
				}
				this._dirtyScopes.Clear();
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000138D0 File Offset: 0x00011AD0
		private void OnScopeVisibilityChanged(GamepadNavigationScope scope, bool isVisible)
		{
			List<GamepadNavigationScope> dirtyScopes = this._dirtyScopes;
			lock (dirtyScopes)
			{
				this._dirtyScopes.Add(scope);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013918 File Offset: 0x00011B18
		private void OnScopeNavigatableWidgetsChanged(GamepadNavigationScope scope)
		{
			this._onScopeNavigatableWidgetsChanged(scope);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013926 File Offset: 0x00011B26
		internal int GetTotalNumberOfScopes()
		{
			return this._visibleScopes.Count + this._invisibleScopes.Count + this._uninitializedScopes.Count;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001394B File Offset: 0x00011B4B
		internal void AddScope(GamepadNavigationScope scope)
		{
			this._uninitializedScopes.Add(scope);
			this._allScopes.Add(scope);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013968 File Offset: 0x00011B68
		internal void RemoveScope(GamepadNavigationScope scope)
		{
			this._allScopes.Remove(scope);
			this._uninitializedScopes.Remove(scope);
			this._visibleScopes.Remove(scope);
			this._invisibleScopes.Remove(scope);
			scope.OnVisibilityChanged = (Action<GamepadNavigationScope, bool>)Delegate.Remove(scope.OnVisibilityChanged, new Action<GamepadNavigationScope, bool>(this.OnScopeVisibilityChanged));
			scope.OnNavigatableWidgetsChanged = (Action<GamepadNavigationScope>)Delegate.Remove(scope.OnNavigatableWidgetsChanged, new Action<GamepadNavigationScope>(this.OnScopeNavigatableWidgetsChanged));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000139ED File Offset: 0x00011BED
		internal bool HasScopeInAnyList(GamepadNavigationScope scope)
		{
			return this._visibleScopes.Contains(scope) || this._invisibleScopes.Contains(scope) || this._uninitializedScopes.Contains(scope);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013A1C File Offset: 0x00011C1C
		internal void OnNavigationScopeInitialized(GamepadNavigationScope scope)
		{
			this._uninitializedScopes.Remove(scope);
			if (scope.IsVisible())
			{
				this._visibleScopes.Add(scope);
			}
			else
			{
				this._invisibleScopes.Add(scope);
			}
			scope.OnVisibilityChanged = (Action<GamepadNavigationScope, bool>)Delegate.Combine(scope.OnVisibilityChanged, new Action<GamepadNavigationScope, bool>(this.OnScopeVisibilityChanged));
			scope.OnNavigatableWidgetsChanged = (Action<GamepadNavigationScope>)Delegate.Combine(scope.OnNavigatableWidgetsChanged, new Action<GamepadNavigationScope>(this.OnScopeNavigatableWidgetsChanged));
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00013A9C File Offset: 0x00011C9C
		internal void OnWidgetDisconnectedFromRoot(Widget widget)
		{
			for (int i = 0; i < this._visibleScopes.Count; i++)
			{
				if (this._visibleScopes[i].FindIndexOfWidget(widget) != -1)
				{
					this._visibleScopes[i].RemoveWidget(widget);
					return;
				}
			}
			for (int j = 0; j < this._invisibleScopes.Count; j++)
			{
				if (this._invisibleScopes[j].FindIndexOfWidget(widget) != -1)
				{
					this._invisibleScopes[j].RemoveWidget(widget);
					return;
				}
			}
			for (int k = 0; k < this._uninitializedScopes.Count; k++)
			{
				if (this._uninitializedScopes[k].FindIndexOfWidget(widget) != -1)
				{
					this._uninitializedScopes[k].RemoveWidget(widget);
					return;
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00013B64 File Offset: 0x00011D64
		private void ClearAllScopes()
		{
			for (int i = 0; i < this._allScopes.Count; i++)
			{
				this._allScopes[i].ClearNavigatableWidgets();
				GamepadNavigationScope gamepadNavigationScope = this._allScopes[i];
				gamepadNavigationScope.OnNavigatableWidgetsChanged = (Action<GamepadNavigationScope>)Delegate.Remove(gamepadNavigationScope.OnNavigatableWidgetsChanged, new Action<GamepadNavigationScope>(this.OnScopeNavigatableWidgetsChanged));
				GamepadNavigationScope gamepadNavigationScope2 = this._allScopes[i];
				gamepadNavigationScope2.OnVisibilityChanged = (Action<GamepadNavigationScope, bool>)Delegate.Remove(gamepadNavigationScope2.OnVisibilityChanged, new Action<GamepadNavigationScope, bool>(this.OnScopeVisibilityChanged));
			}
			this._allScopes.Clear();
			this._uninitializedScopes.Clear();
			this._invisibleScopes.Clear();
			this._visibleScopes.Clear();
			this._allScopes = null;
			this._uninitializedScopes = null;
			this._invisibleScopes = null;
			this._visibleScopes = null;
		}

		// Token: 0x04000242 RID: 578
		private Action<GamepadNavigationScope> _onScopeNavigatableWidgetsChanged;

		// Token: 0x04000243 RID: 579
		private Action<GamepadNavigationScope, bool> _onScopeVisibilityChanged;

		// Token: 0x04000244 RID: 580
		private List<GamepadNavigationScope> _allScopes;

		// Token: 0x04000245 RID: 581
		private List<GamepadNavigationScope> _uninitializedScopes;

		// Token: 0x04000246 RID: 582
		private List<GamepadNavigationScope> _visibleScopes;

		// Token: 0x04000247 RID: 583
		private List<GamepadNavigationScope> _invisibleScopes;

		// Token: 0x04000248 RID: 584
		private List<GamepadNavigationScope> _dirtyScopes;
	}
}
