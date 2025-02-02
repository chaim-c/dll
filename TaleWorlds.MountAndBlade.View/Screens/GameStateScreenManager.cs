using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x02000031 RID: 49
	public class GameStateScreenManager : IGameStateManagerListener
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000FA18 File Offset: 0x0000DC18
		private GameStateManager GameStateManager
		{
			get
			{
				return GameStateManager.Current;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public GameStateScreenManager()
		{
			this._screenTypes = new Dictionary<Type, Type>();
			Assembly[] viewAssemblies = GameStateScreenManager.GetViewAssemblies();
			Assembly assembly = typeof(GameStateScreen).Assembly;
			this.CheckAssemblyScreens(assembly);
			foreach (Assembly assembly2 in viewAssemblies)
			{
				this.CheckAssemblyScreens(assembly2);
			}
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000FA94 File Offset: 0x0000DC94
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.ForceVSyncInMenus)
			{
				if (!BannerlordConfig.ForceVSyncInMenus)
				{
					Utilities.SetForceVsync(false);
					return;
				}
				if (this.GameStateManager.ActiveState.IsMenuState)
				{
					Utilities.SetForceVsync(true);
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
		private void CheckAssemblyScreens(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				object[] customAttributesSafe = type.GetCustomAttributesSafe(typeof(GameStateScreen), false);
				if (customAttributesSafe != null && customAttributesSafe.Length != 0)
				{
					foreach (GameStateScreen gameStateScreen in customAttributesSafe)
					{
						if (this._screenTypes.ContainsKey(gameStateScreen.GameStateType))
						{
							this._screenTypes[gameStateScreen.GameStateType] = type;
						}
						else
						{
							this._screenTypes.Add(gameStateScreen.GameStateType, type);
						}
					}
				}
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public static Assembly[] GetViewAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(GameStateScreen).Assembly;
			foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
				for (int j = 0; j < referencedAssemblies.Length; j++)
				{
					if (referencedAssemblies[j].ToString() == assembly.GetName().ToString())
					{
						list.Add(assembly2);
						break;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000FC14 File Offset: 0x0000DE14
		public ScreenBase CreateScreen(GameState state)
		{
			Type type = null;
			if (this._screenTypes.TryGetValue(state.GetType(), out type))
			{
				return Activator.CreateInstance(type, new object[]
				{
					state
				}) as ScreenBase;
			}
			return null;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000FC50 File Offset: 0x0000DE50
		public void BuildScreens()
		{
			int num = 0;
			foreach (GameState gameState in this.GameStateManager.GameStates)
			{
				ScreenBase screenBase = this.CreateScreen(gameState);
				gameState.RegisterListener(screenBase as IGameStateListener);
				if (screenBase != null)
				{
					if (num == 0)
					{
						ScreenManager.CleanAndPushScreen(screenBase);
					}
					else
					{
						ScreenManager.PushScreen(screenBase);
					}
				}
				num++;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000FCCC File Offset: 0x0000DECC
		void IGameStateManagerListener.OnCreateState(GameState gameState)
		{
			ScreenBase screenBase = this.CreateScreen(gameState);
			gameState.RegisterListener(screenBase as IGameStateListener);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		void IGameStateManagerListener.OnPushState(GameState gameState, bool isTopGameState)
		{
			if (!gameState.IsMenuState)
			{
				Utilities.ClearOldResourcesAndObjects();
			}
			if (gameState.IsMenuState && BannerlordConfig.ForceVSyncInMenus)
			{
				Utilities.SetForceVsync(true);
			}
			else if (!gameState.IsMenuState)
			{
				Utilities.SetForceVsync(false);
			}
			ScreenBase listenerOfType;
			if ((listenerOfType = gameState.GetListenerOfType<ScreenBase>()) != null)
			{
				if (isTopGameState)
				{
					ScreenManager.CleanAndPushScreen(listenerOfType);
					return;
				}
				ScreenManager.PushScreen(listenerOfType);
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		void IGameStateManagerListener.OnPopState(GameState gameState)
		{
			if (!gameState.IsMenuState)
			{
				Utilities.ClearOldResourcesAndObjects();
			}
			if (gameState.IsMenuState && BannerlordConfig.ForceVSyncInMenus)
			{
				Utilities.SetForceVsync(false);
			}
			if (this.GameStateManager.ActiveState != null && this.GameStateManager.ActiveState.IsMenuState && BannerlordConfig.ForceVSyncInMenus)
			{
				Utilities.SetForceVsync(true);
			}
			ScreenManager.PopScreen();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		void IGameStateManagerListener.OnCleanStates()
		{
			ScreenManager.CleanScreens();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000FDB3 File Offset: 0x0000DFB3
		void IGameStateManagerListener.OnSavedGameLoadFinished()
		{
			this.BuildScreens();
		}

		// Token: 0x0400013B RID: 315
		private Dictionary<Type, Type> _screenTypes;
	}
}
