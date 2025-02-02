using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameMenus
{
	// Token: 0x020000DF RID: 223
	public class GameMenuCallbackManager
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00058A62 File Offset: 0x00056C62
		public static GameMenuCallbackManager Instance
		{
			get
			{
				return Campaign.Current.GameMenuCallbackManager;
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00058A6E File Offset: 0x00056C6E
		public GameMenuCallbackManager()
		{
			this.FillInitializationHandlers();
			this.FillEventHandlers();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00058A84 File Offset: 0x00056C84
		private void FillInitializationHandlers()
		{
			this._gameMenuInitializationHandlers = new Dictionary<string, GameMenuInitializationHandlerDelegate>();
			Assembly assembly = typeof(GameMenuInitializationHandler).Assembly;
			this.FillInitializationHandlerWith(assembly);
			foreach (Assembly assembly2 in GameMenuCallbackManager.GeAssemblies())
			{
				this.FillInitializationHandlerWith(assembly2);
			}
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00058AD4 File Offset: 0x00056CD4
		private static Assembly[] GeAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(GameMenu).Assembly;
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

		// Token: 0x060013D6 RID: 5078 RVA: 0x00058B5D File Offset: 0x00056D5D
		public void OnGameLoad()
		{
			this.FillInitializationHandlers();
			this.FillEventHandlers();
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00058B6C File Offset: 0x00056D6C
		private void FillInitializationHandlerWith(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object[] customAttributesSafe = method.GetCustomAttributesSafe(typeof(GameMenuInitializationHandler), false);
					if (customAttributesSafe != null && customAttributesSafe.Length != 0)
					{
						foreach (GameMenuInitializationHandler gameMenuInitializationHandler in customAttributesSafe)
						{
							GameMenuInitializationHandlerDelegate value = Delegate.CreateDelegate(typeof(GameMenuInitializationHandlerDelegate), method) as GameMenuInitializationHandlerDelegate;
							if (!this._gameMenuInitializationHandlers.ContainsKey(gameMenuInitializationHandler.MenuId))
							{
								this._gameMenuInitializationHandlers.Add(gameMenuInitializationHandler.MenuId, value);
							}
							else
							{
								this._gameMenuInitializationHandlers[gameMenuInitializationHandler.MenuId] = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00058C74 File Offset: 0x00056E74
		private void FillEventHandlers()
		{
			this._eventHandlers = new Dictionary<string, Dictionary<string, GameMenuEventHandlerDelegate>>();
			Assembly assembly = typeof(GameMenuEventHandler).Assembly;
			this.FillEventHandlersWith(assembly);
			foreach (Assembly assembly2 in GameMenuCallbackManager.GeAssemblies())
			{
				this.FillEventHandlersWith(assembly2);
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00058CC4 File Offset: 0x00056EC4
		private void FillEventHandlersWith(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object[] customAttributesSafe = method.GetCustomAttributesSafe(typeof(GameMenuEventHandler), false);
					if (customAttributesSafe != null && customAttributesSafe.Length != 0)
					{
						foreach (GameMenuEventHandler gameMenuEventHandler in customAttributesSafe)
						{
							GameMenuEventHandlerDelegate value = Delegate.CreateDelegate(typeof(GameMenuEventHandlerDelegate), method) as GameMenuEventHandlerDelegate;
							Dictionary<string, GameMenuEventHandlerDelegate> dictionary;
							if (!this._eventHandlers.TryGetValue(gameMenuEventHandler.MenuId, out dictionary))
							{
								dictionary = new Dictionary<string, GameMenuEventHandlerDelegate>();
								this._eventHandlers.Add(gameMenuEventHandler.MenuId, dictionary);
							}
							if (!dictionary.ContainsKey(gameMenuEventHandler.MenuOptionId))
							{
								dictionary.Add(gameMenuEventHandler.MenuOptionId, value);
							}
							else
							{
								dictionary[gameMenuEventHandler.MenuOptionId] = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00058DFC File Offset: 0x00056FFC
		public void InitializeState(string menuId, MenuContext state)
		{
			GameMenuInitializationHandlerDelegate gameMenuInitializationHandlerDelegate = null;
			if (this._gameMenuInitializationHandlers.TryGetValue(menuId, out gameMenuInitializationHandlerDelegate))
			{
				MenuCallbackArgs args = new MenuCallbackArgs(state, null);
				gameMenuInitializationHandlerDelegate(args);
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00058E2C File Offset: 0x0005702C
		public void OnConsequence(string menuId, GameMenuOption gameMenuOption, MenuContext state)
		{
			Dictionary<string, GameMenuEventHandlerDelegate> dictionary = null;
			if (this._eventHandlers.TryGetValue(menuId, out dictionary))
			{
				GameMenuEventHandlerDelegate gameMenuEventHandlerDelegate = null;
				if (dictionary.TryGetValue(gameMenuOption.IdString, out gameMenuEventHandlerDelegate))
				{
					MenuCallbackArgs args = new MenuCallbackArgs(state, gameMenuOption.Text);
					gameMenuEventHandlerDelegate(args);
				}
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00058E71 File Offset: 0x00057071
		public TextObject GetMenuOptionTooltip(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetMenuOptionTooltip(menuItemNumber);
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionText");
			}
			return TextObject.Empty;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00058EA0 File Offset: 0x000570A0
		public TextObject GetVirtualMenuOptionTooltip(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null)
			{
				int num = (menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1;
				if (virtualMenuItemIndex < num)
				{
					return this.GetMenuOptionTooltip(menuContext, 0);
				}
				return this.GetMenuOptionTooltip(menuContext, virtualMenuItemIndex + 1 - num);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
				}
				return TextObject.Empty;
			}
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00058F10 File Offset: 0x00057110
		public TextObject GetVirtualMenuOptionText(MenuContext menuContext, int virtualMenuItemIndex)
		{
			if (menuContext.GameMenu != null)
			{
				int num = (menuContext.GameMenu.MenuRepeatObjects.Count > 0) ? menuContext.GameMenu.MenuRepeatObjects.Count : 1;
				if (virtualMenuItemIndex < num)
				{
					return this.GetMenuOptionText(menuContext, 0);
				}
				return this.GetMenuOptionText(menuContext, virtualMenuItemIndex + 1 - num);
			}
			else
			{
				if (menuContext.GameMenu == null)
				{
					throw new MBMisuseException("Current game menu empty, can not run GetVirtualMenuOptionText");
				}
				return TextObject.Empty;
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00058F7E File Offset: 0x0005717E
		public TextObject GetMenuOptionText(MenuContext menuContext, int menuItemNumber)
		{
			if (menuContext.GameMenu != null)
			{
				return menuContext.GameMenu.GetMenuOptionText(menuItemNumber);
			}
			if (menuContext.GameMenu == null)
			{
				throw new MBMisuseException("Current game menu empty, can not run GetMenuOptionText");
			}
			return TextObject.Empty;
		}

		// Token: 0x040006D6 RID: 1750
		private Dictionary<string, GameMenuInitializationHandlerDelegate> _gameMenuInitializationHandlers;

		// Token: 0x040006D7 RID: 1751
		private Dictionary<string, Dictionary<string, GameMenuEventHandlerDelegate>> _eventHandlers;
	}
}
