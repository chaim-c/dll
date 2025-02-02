using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.ScreenSystem
{
	// Token: 0x02000009 RID: 9
	public static class ScreenManager
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003022 File Offset: 0x00001222
		public static IScreenManagerEngineConnection EngineInterface
		{
			get
			{
				return ScreenManager._engineInterface;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003029 File Offset: 0x00001229
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003030 File Offset: 0x00001230
		public static float Scale { get; private set; } = 1f;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003038 File Offset: 0x00001238
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000303F File Offset: 0x0000123F
		public static Vec2 UsableArea
		{
			get
			{
				return ScreenManager._usableArea;
			}
			private set
			{
				if (value != ScreenManager._usableArea)
				{
					ScreenManager._usableArea = value;
					ScreenManager.OnUsableAreaChanged(ScreenManager._usableArea);
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000305E File Offset: 0x0000125E
		public static bool IsEnterButtonRDown
		{
			get
			{
				return ScreenManager._engineInterface.GetIsEnterButtonRDown();
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000098 RID: 152 RVA: 0x0000306C File Offset: 0x0000126C
		// (remove) Token: 0x06000099 RID: 153 RVA: 0x000030A0 File Offset: 0x000012A0
		public static event ScreenManager.OnPushScreenEvent OnPushScreen;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600009A RID: 154 RVA: 0x000030D4 File Offset: 0x000012D4
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x00003108 File Offset: 0x00001308
		public static event ScreenManager.OnPopScreenEvent OnPopScreen;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600009C RID: 156 RVA: 0x0000313C File Offset: 0x0000133C
		// (remove) Token: 0x0600009D RID: 157 RVA: 0x00003170 File Offset: 0x00001370
		public static event ScreenManager.OnControllerDisconnectedEvent OnControllerDisconnected;

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000031A4 File Offset: 0x000013A4
		public static List<ScreenLayer> SortedLayers
		{
			get
			{
				if (!ScreenManager._isSortedActiveLayersDirty)
				{
					int count = ScreenManager._sortedLayers.Count;
					ScreenBase topScreen = ScreenManager.TopScreen;
					int? num = (topScreen != null) ? new int?(topScreen.Layers.Count) : null;
					ObservableCollection<GlobalLayer> globalLayers = ScreenManager._globalLayers;
					int? num2 = num + ((globalLayers != null) ? new int?(globalLayers.Count) : null);
					if (count == num2.GetValueOrDefault() & num2 != null)
					{
						goto IL_145;
					}
				}
				ScreenManager._isMouseInputActiveLastFrame = false;
				ScreenManager._sortedLayers.Clear();
				if (ScreenManager.TopScreen != null)
				{
					for (int i = 0; i < ScreenManager.TopScreen.Layers.Count; i++)
					{
						ScreenLayer screenLayer = ScreenManager.TopScreen.Layers[i];
						if (screenLayer != null)
						{
							ScreenManager._sortedLayers.Add(screenLayer);
						}
					}
				}
				foreach (GlobalLayer globalLayer in ScreenManager._globalLayers)
				{
					ScreenManager._sortedLayers.Add(globalLayer.Layer);
				}
				ScreenManager._sortedLayers.Sort();
				ScreenManager._isSortedActiveLayersDirty = false;
				IL_145:
				return ScreenManager._sortedLayers;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000330C File Offset: 0x0000150C
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003313 File Offset: 0x00001513
		public static ScreenBase TopScreen { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000331B File Offset: 0x0000151B
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003322 File Offset: 0x00001522
		public static ScreenLayer FocusedLayer { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000332A File Offset: 0x0000152A
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003331 File Offset: 0x00001531
		public static ScreenLayer FirstHitLayer { get; private set; }

		// Token: 0x060000A5 RID: 165 RVA: 0x0000333C File Offset: 0x0000153C
		static ScreenManager()
		{
			ScreenManager._globalLayers = new ObservableCollection<GlobalLayer>();
			ScreenManager._screenList = new ObservableCollection<ScreenBase>();
			ScreenManager._screenList.CollectionChanged += ScreenManager.OnScreenListChanged;
			ScreenManager._globalLayers.CollectionChanged += ScreenManager.OnGlobalListChanged;
			ScreenManager.FocusedLayer = null;
			ScreenManager.FirstHitLayer = null;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000033DD File Offset: 0x000015DD
		public static void Initialize(IScreenManagerEngineConnection engineInterface)
		{
			ScreenManager._engineInterface = engineInterface;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000033E8 File Offset: 0x000015E8
		internal static void RefreshGlobalOrder()
		{
			if (!ScreenManager._isRefreshActive)
			{
				ScreenManager._isRefreshActive = true;
				int num = -2000;
				int num2 = 10000;
				for (int i = 0; i < ScreenManager.SortedLayers.Count; i++)
				{
					if (ScreenManager.SortedLayers[i] != null)
					{
						if (!ScreenManager.SortedLayers[i].Finalized)
						{
							ScreenLayer screenLayer = ScreenManager.SortedLayers[i];
							if (screenLayer != null && screenLayer.IsActive)
							{
								ScreenLayer screenLayer2 = ScreenManager.SortedLayers[i];
								if (screenLayer2 != null)
								{
									screenLayer2.RefreshGlobalOrder(ref num);
								}
							}
							else
							{
								ScreenLayer screenLayer3 = ScreenManager.SortedLayers[i];
								if (screenLayer3 != null)
								{
									screenLayer3.RefreshGlobalOrder(ref num2);
								}
							}
						}
						ScreenManager._globalOrderDirty = false;
					}
				}
				ScreenManager._isRefreshActive = false;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000349F File Offset: 0x0000169F
		public static void RemoveGlobalLayer(GlobalLayer layer)
		{
			Debug.Print("RemoveGlobalLayer", 0, Debug.DebugColor.White, 17592186044416UL);
			ScreenManager._globalLayers.Remove(layer);
			layer.Layer.HandleDeactivate();
			ScreenManager._globalOrderDirty = true;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000034D4 File Offset: 0x000016D4
		public static void AddGlobalLayer(GlobalLayer layer, bool isFocusable)
		{
			Debug.Print("AddGlobalLayer", 0, Debug.DebugColor.White, 17592186044416UL);
			int index = ScreenManager._globalLayers.Count;
			for (int i = 0; i < ScreenManager._globalLayers.Count; i++)
			{
				if (ScreenManager._globalLayers[i].Layer.InputRestrictions.Order >= layer.Layer.InputRestrictions.Order)
				{
					index = i;
					break;
				}
			}
			ScreenManager._globalLayers.Insert(index, layer);
			layer.Layer.HandleActivate();
			ScreenManager._globalOrderDirty = true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003564 File Offset: 0x00001764
		public static void OnConstrainStateChanged(bool isConstrained)
		{
			Debug.Print("OnConstrainStateChanged: " + isConstrained.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
			ScreenManager.OnGameWindowFocusChange(!isConstrained);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003594 File Offset: 0x00001794
		public static bool ScreenTypeExistsAtList(ScreenBase screen)
		{
			Type type = screen.GetType();
			using (IEnumerator<ScreenBase> enumerator = ScreenManager._screenList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000035F4 File Offset: 0x000017F4
		public static void UpdateLayout()
		{
			foreach (GlobalLayer globalLayer in ScreenManager._globalLayers)
			{
				globalLayer.UpdateLayout();
			}
			foreach (ScreenBase screenBase in ScreenManager._screenList)
			{
				screenBase.UpdateLayout();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003678 File Offset: 0x00001878
		public static void SetSuspendLayer(ScreenLayer layer, bool isSuspended)
		{
			if (isSuspended)
			{
				layer.HandleDeactivate();
			}
			else
			{
				layer.HandleActivate();
			}
			layer.LastActiveState = !isSuspended;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003698 File Offset: 0x00001898
		public static void OnFinalize()
		{
			ScreenManager.DeactivateAndFinalizeAllScreens();
			ScreenManager._screenList.CollectionChanged -= ScreenManager.OnScreenListChanged;
			ScreenManager._globalLayers.CollectionChanged -= ScreenManager.OnGlobalListChanged;
			ScreenManager._screenList = null;
			ScreenManager._globalLayers = null;
			ScreenManager.FocusedLayer = null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000036E8 File Offset: 0x000018E8
		private static void DeactivateAndFinalizeAllScreens()
		{
			Debug.Print("DeactivateAndFinalizeAllScreens", 0, Debug.DebugColor.White, 17592186044416UL);
			for (int i = ScreenManager._screenList.Count - 1; i >= 0; i--)
			{
				ScreenManager._screenList[i].HandlePause();
			}
			for (int j = ScreenManager._screenList.Count - 1; j >= 0; j--)
			{
				ScreenManager._screenList[j].HandleDeactivate();
			}
			for (int k = ScreenManager._screenList.Count - 1; k >= 0; k--)
			{
				ScreenManager._screenList[k].HandleFinalize();
			}
			ScreenManager._screenList.Clear();
			Common.MemoryCleanupGC(false);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003790 File Offset: 0x00001990
		internal static void UpdateLateTickLayers(List<ScreenLayer> layers)
		{
			ScreenManager._lateTickLayers = layers;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003798 File Offset: 0x00001998
		public static void Tick(float dt, bool activeMouseVisible)
		{
			for (int i = 0; i < ScreenManager._globalLayers.Count; i++)
			{
				GlobalLayer globalLayer = ScreenManager._globalLayers[i];
				if (globalLayer != null)
				{
					globalLayer.EarlyTick(dt);
				}
			}
			ScreenManager.Update();
			ScreenManager._lateTickLayers = null;
			if (ScreenManager.TopScreen != null)
			{
				ScreenManager.TopScreen.FrameTick(dt);
				ScreenBase screenBase = ScreenManager.FindPredecessor(ScreenManager.TopScreen);
				if (screenBase != null)
				{
					screenBase.IdleTick(dt);
				}
			}
			for (int j = 0; j < ScreenManager._globalLayers.Count; j++)
			{
				GlobalLayer globalLayer2 = ScreenManager._globalLayers[j];
				if (globalLayer2 != null)
				{
					globalLayer2.Tick(dt);
				}
			}
			ScreenManager.LateUpdate(dt, activeMouseVisible);
			ScreenManager.ShowScreenDebugInformation();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000383C File Offset: 0x00001A3C
		public static void LateTick(float dt)
		{
			if (ScreenManager._lateTickLayers != null)
			{
				for (int i = 0; i < ScreenManager._lateTickLayers.Count; i++)
				{
					if (!ScreenManager._lateTickLayers[i].Finalized)
					{
						ScreenManager._lateTickLayers[i].LateTick(dt);
					}
				}
				ScreenManager._lateTickLayers.Clear();
			}
			for (int j = 0; j < ScreenManager._globalLayers.Count; j++)
			{
				ScreenManager._globalLayers[j].LateTick(dt);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000038B8 File Offset: 0x00001AB8
		public static void OnPlatformScreenKeyboardRequested(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum)
		{
			Action<string, string, int, int> platformTextRequested = ScreenManager.PlatformTextRequested;
			if (platformTextRequested == null)
			{
				return;
			}
			platformTextRequested(initialText, descriptionText, maxLength, keyboardTypeEnum);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000038CD File Offset: 0x00001ACD
		public static void OnOnscreenKeyboardDone(string inputText)
		{
			ScreenLayer focusedLayer = ScreenManager.FocusedLayer;
			if (focusedLayer == null)
			{
				return;
			}
			focusedLayer.OnOnScreenKeyboardDone(inputText);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000038DF File Offset: 0x00001ADF
		public static void OnOnscreenKeyboardCanceled()
		{
			ScreenLayer focusedLayer = ScreenManager.FocusedLayer;
			if (focusedLayer == null)
			{
				return;
			}
			focusedLayer.OnOnScreenKeyboardCanceled();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000038F0 File Offset: 0x00001AF0
		public static void OnGameWindowFocusChange(bool focusGained)
		{
			Debug.Print("OnGameWindowFocusChange: " + focusGained.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
			string str = "TopScreen: ";
			ScreenBase topScreen = ScreenManager.TopScreen;
			string str2;
			if (topScreen == null)
			{
				str2 = null;
			}
			else
			{
				Type type = topScreen.GetType();
				str2 = ((type != null) ? type.Name : null);
			}
			Debug.Print(str + str2, 0, Debug.DebugColor.White, 17592186044416UL);
			bool flag = false;
			if (!Debugger.IsAttached && !flag)
			{
				ScreenBase topScreen2 = ScreenManager.TopScreen;
				if (topScreen2 != null)
				{
					topScreen2.OnFocusChangeOnGameWindow(focusGained);
				}
			}
			if (focusGained)
			{
				Action focusGained2 = ScreenManager.FocusGained;
				if (focusGained2 == null)
				{
					return;
				}
				focusGained2();
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000B7 RID: 183 RVA: 0x00003988 File Offset: 0x00001B88
		// (remove) Token: 0x060000B8 RID: 184 RVA: 0x000039BC File Offset: 0x00001BBC
		public static event Action FocusGained;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000B9 RID: 185 RVA: 0x000039F0 File Offset: 0x00001BF0
		// (remove) Token: 0x060000BA RID: 186 RVA: 0x00003A24 File Offset: 0x00001C24
		public static event Action<string, string, int, int> PlatformTextRequested;

		// Token: 0x060000BB RID: 187 RVA: 0x00003A58 File Offset: 0x00001C58
		public static void ReplaceTopScreen(ScreenBase screen)
		{
			Debug.Print("ReplaceToTopScreen", 0, Debug.DebugColor.White, 17592186044416UL);
			if (ScreenManager._screenList.Count > 0)
			{
				ScreenManager.TopScreen.HandlePause();
				ScreenManager.TopScreen.HandleDeactivate();
				ScreenManager.TopScreen.HandleFinalize();
				ScreenManager.OnPopScreenEvent onPopScreen = ScreenManager.OnPopScreen;
				if (onPopScreen != null)
				{
					onPopScreen(ScreenManager.TopScreen);
				}
				ScreenManager._screenList.Remove(ScreenManager.TopScreen);
			}
			ScreenManager._screenList.Add(screen);
			screen.HandleInitialize();
			screen.HandleActivate();
			screen.HandleResume();
			ScreenManager._globalOrderDirty = true;
			ScreenManager.OnPushScreenEvent onPushScreen = ScreenManager.OnPushScreen;
			if (onPushScreen == null)
			{
				return;
			}
			onPushScreen(screen);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003B00 File Offset: 0x00001D00
		public static List<ScreenLayer> GetPersistentInputRestrictions()
		{
			List<ScreenLayer> list = new List<ScreenLayer>();
			foreach (GlobalLayer globalLayer in ScreenManager._globalLayers)
			{
				list.Add(globalLayer.Layer);
			}
			return list;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003B58 File Offset: 0x00001D58
		public static void SetAndActivateRootScreen(ScreenBase screen)
		{
			Debug.Print("SetAndActivateRootScreen", 0, Debug.DebugColor.White, 17592186044416UL);
			if (ScreenManager.TopScreen != null)
			{
				throw new Exception("TopScreen is not null.");
			}
			ScreenManager._screenList.Add(screen);
			screen.HandleInitialize();
			screen.HandleActivate();
			screen.HandleResume();
			ScreenManager._globalOrderDirty = true;
			ScreenManager.OnPushScreenEvent onPushScreen = ScreenManager.OnPushScreen;
			if (onPushScreen == null)
			{
				return;
			}
			onPushScreen(screen);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public static void CleanAndPushScreen(ScreenBase screen)
		{
			Debug.Print("CleanAndPushScreen", 0, Debug.DebugColor.White, 17592186044416UL);
			ScreenManager.DeactivateAndFinalizeAllScreens();
			ScreenManager._screenList.Add(screen);
			screen.HandleInitialize();
			screen.HandleActivate();
			screen.HandleResume();
			ScreenManager._globalOrderDirty = true;
			ScreenManager.OnPushScreenEvent onPushScreen = ScreenManager.OnPushScreen;
			if (onPushScreen == null)
			{
				return;
			}
			onPushScreen(screen);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003C1C File Offset: 0x00001E1C
		[CommandLineFunctionality.CommandLineArgumentFunction("cb_clear_siege_machine_selection", "ui")]
		public static string ClearSiegeMachineSelection(List<string> args)
		{
			ScreenBase screenBase = ScreenManager._screenList.FirstOrDefault((ScreenBase x) => x.GetType().GetMethod("ClearSiegeMachineSelections") != null);
			if (screenBase != null)
			{
				screenBase.GetType().GetMethod("ClearSiegeMachineSelections").Invoke(screenBase, null);
			}
			return "Siege machine selections have been cleared.";
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003C74 File Offset: 0x00001E74
		[CommandLineFunctionality.CommandLineArgumentFunction("cb_copy_battle_layout_to_clipboard", "ui")]
		public static string CopyCustomBattle(List<string> args)
		{
			ScreenBase screenBase = ScreenManager._screenList.FirstOrDefault((ScreenBase x) => x.GetType().GetMethod("CopyBattleLayoutToClipboard") != null);
			if (screenBase != null)
			{
				screenBase.GetType().GetMethod("CopyBattleLayoutToClipboard").Invoke(screenBase, null);
				return "Custom battle layout has been copied to clipboard as text.";
			}
			return "Something went wrong";
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003CD4 File Offset: 0x00001ED4
		[CommandLineFunctionality.CommandLineArgumentFunction("cb_apply_battle_layout_from_string", "ui")]
		public static string ApplyCustomBattleLayout(List<string> args)
		{
			ScreenBase screenBase = ScreenManager._screenList.FirstOrDefault((ScreenBase x) => x.GetType().GetMethod("ApplyCustomBattleLayout") != null);
			if (screenBase == null || args.Count <= 0)
			{
				return "Something went wrong.";
			}
			string text = args.Aggregate((string i, string j) => i + " " + j);
			if (text.Count<char>() > 5)
			{
				screenBase.GetType().GetMethod("ApplyCustomBattleLayout").Invoke(screenBase, new object[]
				{
					text
				});
				return "Applied new layout from text.";
			}
			return "Argument is not right.";
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003D78 File Offset: 0x00001F78
		public static void PushScreen(ScreenBase screen)
		{
			Debug.Print("PushScreen", 0, Debug.DebugColor.White, 17592186044416UL);
			if (ScreenManager._screenList.Count > 0)
			{
				ScreenManager.TopScreen.HandlePause();
				if (ScreenManager.TopScreen.IsActive)
				{
					ScreenManager.TopScreen.HandleDeactivate();
				}
			}
			ScreenManager._screenList.Add(screen);
			screen.HandleInitialize();
			screen.HandleActivate();
			screen.HandleResume();
			ScreenManager._globalOrderDirty = true;
			ScreenManager.OnPushScreenEvent onPushScreen = ScreenManager.OnPushScreen;
			if (onPushScreen == null)
			{
				return;
			}
			onPushScreen(screen);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003DFC File Offset: 0x00001FFC
		public static void PopScreen()
		{
			Debug.Print("PopScreen", 0, Debug.DebugColor.White, 17592186044416UL);
			if (ScreenManager._screenList.Count > 0)
			{
				ScreenManager.TopScreen.HandlePause();
				ScreenManager.TopScreen.HandleDeactivate();
				ScreenManager.TopScreen.HandleFinalize();
				Debug.Print("PopScreen - " + ScreenManager.TopScreen.GetType().ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
				ScreenManager.OnPopScreenEvent onPopScreen = ScreenManager.OnPopScreen;
				if (onPopScreen != null)
				{
					onPopScreen(ScreenManager.TopScreen);
				}
				ScreenManager._screenList.Remove(ScreenManager.TopScreen);
			}
			if (ScreenManager._screenList.Count > 0)
			{
				ScreenBase topScreen = ScreenManager.TopScreen;
				ScreenManager.TopScreen.HandleActivate();
				if (topScreen == ScreenManager.TopScreen)
				{
					ScreenManager.TopScreen.HandleResume();
				}
			}
			ScreenManager._globalOrderDirty = true;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003ECC File Offset: 0x000020CC
		public static void CleanScreens()
		{
			Debug.Print("CleanScreens", 0, Debug.DebugColor.White, 17592186044416UL);
			while (ScreenManager._screenList.Count > 0)
			{
				ScreenManager.TopScreen.HandlePause();
				ScreenManager.TopScreen.HandleDeactivate();
				ScreenManager.TopScreen.HandleFinalize();
				ScreenManager.OnPopScreenEvent onPopScreen = ScreenManager.OnPopScreen;
				if (onPopScreen != null)
				{
					onPopScreen(ScreenManager.TopScreen);
				}
				ScreenManager._screenList.Remove(ScreenManager.TopScreen);
			}
			ScreenManager._globalOrderDirty = true;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003F48 File Offset: 0x00002148
		private static ScreenBase FindPredecessor(ScreenBase screen)
		{
			ScreenBase result = null;
			int num = ScreenManager._screenList.IndexOf(screen);
			if (num > 0)
			{
				result = ScreenManager._screenList[num - 1];
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003F78 File Offset: 0x00002178
		public static void Update(IReadOnlyList<int> lastKeysPressed)
		{
			ScreenManager._lastPressedKeys = lastKeysPressed;
			ScreenBase topScreen = ScreenManager.TopScreen;
			if (topScreen != null && topScreen.IsActive)
			{
				ScreenManager.TopScreen.Update(ScreenManager._lastPressedKeys);
			}
			for (int i = 0; i < ScreenManager._globalLayers.Count; i++)
			{
				GlobalLayer globalLayer = ScreenManager._globalLayers[i];
				if (globalLayer.Layer.IsActive)
				{
					globalLayer.Update(ScreenManager._lastPressedKeys);
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003FE8 File Offset: 0x000021E8
		private static bool? GetMouseInput()
		{
			bool flag = false;
			if (Input.IsKeyDown(InputKey.LeftMouseButton) || Input.IsKeyDown(InputKey.RightMouseButton) || Input.IsKeyDown(InputKey.MiddleMouseButton) || Input.IsKeyDown(InputKey.X1MouseButton) || Input.IsKeyDown(InputKey.X2MouseButton) || Input.IsKeyDown(ScreenManager.IsEnterButtonRDown ? InputKey.ControllerRDown : InputKey.ControllerRRight))
			{
				flag = true;
			}
			if (!ScreenManager._isMouseInputActiveLastFrame && flag)
			{
				flag = true;
			}
			else
			{
				if (!ScreenManager._isMouseInputActiveLastFrame || flag)
				{
					return null;
				}
				flag = false;
			}
			ScreenManager._isMouseInputActiveLastFrame = flag;
			return new bool?(flag);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004084 File Offset: 0x00002284
		public static void EarlyUpdate(Vec2 usableArea)
		{
			ScreenManager.UsableArea = usableArea;
			ScreenManager.RefreshGlobalOrder();
			InputType inputType = InputType.None;
			for (int i = 0; i < ScreenManager.SortedLayers.Count; i++)
			{
				ScreenLayer screenLayer = ScreenManager.SortedLayers[i];
				if (screenLayer != null && screenLayer.IsActive)
				{
					ScreenManager.SortedLayers[i].MouseEnabled = true;
				}
			}
			bool? mouseInput = ScreenManager.GetMouseInput();
			for (int j = ScreenManager.SortedLayers.Count - 1; j >= 0; j--)
			{
				ScreenLayer screenLayer2 = ScreenManager.SortedLayers[j];
				if (screenLayer2 != null && screenLayer2.IsActive && !screenLayer2.Finalized)
				{
					bool? isMousePressed = null;
					bool? flag = mouseInput;
					bool flag2 = false;
					if (flag.GetValueOrDefault() == flag2 & flag != null)
					{
						isMousePressed = new bool?(false);
					}
					InputType inputType2 = InputType.None;
					InputUsageMask inputUsageMask = screenLayer2.InputUsageMask;
					screenLayer2.ScreenOrderInLastFrame = j;
					screenLayer2.IsHitThisFrame = false;
					if (screenLayer2.HitTest())
					{
						if (ScreenManager.FirstHitLayer == null)
						{
							ScreenManager.FirstHitLayer = screenLayer2;
							ScreenManager._engineInterface.ActivateMouseCursor(screenLayer2.ActiveCursor);
						}
						if (!inputType.HasAnyFlag(InputType.MouseButton) && inputUsageMask.HasAnyFlag(InputUsageMask.MouseButtons))
						{
							isMousePressed = mouseInput;
							inputType2 |= InputType.MouseButton;
							inputType |= InputType.MouseButton;
							screenLayer2.IsHitThisFrame = true;
						}
						if (!inputType.HasAnyFlag(InputType.MouseWheel) && inputUsageMask.HasAnyFlag(InputUsageMask.MouseWheels))
						{
							inputType2 |= InputType.MouseWheel;
							inputType |= InputType.MouseWheel;
							screenLayer2.IsHitThisFrame = true;
						}
					}
					if (!inputType.HasAnyFlag(InputType.Key) && ScreenManager.FocusTest(screenLayer2))
					{
						inputType2 |= InputType.Key;
						inputType |= InputType.Key;
					}
					screenLayer2.EarlyProcessEvents(inputType2, isMousePressed);
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004210 File Offset: 0x00002410
		private static void Update()
		{
			int num = 0;
			for (int i = 0; i < ScreenManager.SortedLayers.Count; i++)
			{
				if (ScreenManager.SortedLayers[i].IsActive)
				{
					num++;
				}
			}
			if (ScreenManager._sortedActiveLayersCopyForUpdate.Length < num)
			{
				ScreenManager._sortedActiveLayersCopyForUpdate = new ScreenLayer[num];
			}
			int num2 = 0;
			for (int j = 0; j < ScreenManager.SortedLayers.Count; j++)
			{
				ScreenLayer screenLayer = ScreenManager.SortedLayers[j];
				if (screenLayer.IsActive)
				{
					ScreenManager._sortedActiveLayersCopyForUpdate[num2] = screenLayer;
					num2++;
				}
			}
			for (int k = num2 - 1; k >= 0; k--)
			{
				ScreenLayer screenLayer2 = ScreenManager._sortedActiveLayersCopyForUpdate[k];
				if (!screenLayer2.Finalized)
				{
					screenLayer2.ProcessEvents();
				}
			}
			for (int l = 0; l < ScreenManager._sortedActiveLayersCopyForUpdate.Length; l++)
			{
				ScreenManager._sortedActiveLayersCopyForUpdate[l] = null;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000042E4 File Offset: 0x000024E4
		private static void LateUpdate(float dt, bool activeMouseVisible)
		{
			for (int i = 0; i < ScreenManager.SortedLayers.Count; i++)
			{
				ScreenLayer screenLayer = ScreenManager.SortedLayers[i];
				if (screenLayer != null && screenLayer.IsActive)
				{
					screenLayer.LateProcessEvents();
				}
			}
			for (int j = 0; j < ScreenManager.SortedLayers.Count; j++)
			{
				ScreenLayer screenLayer2 = ScreenManager.SortedLayers[j];
				if (screenLayer2 != null && screenLayer2.IsActive)
				{
					screenLayer2.OnLateUpdate(dt);
					if (screenLayer2 != ScreenManager.FocusedLayer || ScreenManager._focusedLayerChangedThisFrame)
					{
						screenLayer2.Input.ResetLastDownKeys();
					}
				}
			}
			if (!ScreenManager._focusedLayerChangedThisFrame)
			{
				ScreenLayer focusedLayer = ScreenManager.FocusedLayer;
				if (focusedLayer != null)
				{
					InputContext input = focusedLayer.Input;
					if (input != null)
					{
						input.UpdateLastDownKeys();
					}
				}
			}
			ScreenManager._focusedLayerChangedThisFrame = false;
			ScreenManager.FirstHitLayer = null;
			ScreenManager.UpdateMouseVisibility(activeMouseVisible);
			if (ScreenManager._globalOrderDirty)
			{
				ScreenManager.RefreshGlobalOrder();
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000043B0 File Offset: 0x000025B0
		internal static void UpdateMouseVisibility(bool activeMouseVisible)
		{
			for (int i = 0; i < ScreenManager.SortedLayers.Count; i++)
			{
				ScreenLayer screenLayer = ScreenManager.SortedLayers[i];
				if (screenLayer.IsActive && screenLayer.InputRestrictions.MouseVisibility)
				{
					if (!ScreenManager._activeMouseVisible)
					{
						ScreenManager.SetMouseVisible(true);
					}
					return;
				}
			}
			if (ScreenManager._activeMouseVisible)
			{
				ScreenManager.SetMouseVisible(false);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000440E File Offset: 0x0000260E
		public static bool IsControllerActive()
		{
			return Input.IsControllerConnected && Input.IsGamepadActive && !Input.IsMouseActive && ScreenManager._engineInterface.GetMouseVisible();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004431 File Offset: 0x00002631
		public static bool IsMouseCursorHidden()
		{
			return !Input.IsMouseActive && ScreenManager._engineInterface.GetMouseVisible();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004446 File Offset: 0x00002646
		public static bool IsMouseCursorActive()
		{
			return Input.IsMouseActive && ScreenManager._engineInterface.GetMouseVisible();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000445C File Offset: 0x0000265C
		public static bool IsLayerBlockedAtPosition(ScreenLayer layer, Vector2 position)
		{
			for (int i = ScreenManager.SortedLayers.Count - 1; i >= 0; i--)
			{
				ScreenLayer screenLayer = ScreenManager.SortedLayers[i];
				if (layer == screenLayer)
				{
					return false;
				}
				if (screenLayer != null && screenLayer.IsActive && !screenLayer.Finalized && screenLayer.HitTest(position))
				{
					if (screenLayer.InputUsageMask.HasAnyFlag(InputUsageMask.MouseButtons))
					{
						return layer != ScreenManager.SortedLayers[i];
					}
					if (screenLayer.InputUsageMask.HasAnyFlag(InputUsageMask.MouseWheels))
					{
						return layer != ScreenManager.SortedLayers[i];
					}
				}
			}
			return false;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000044EF File Offset: 0x000026EF
		private static void SetMouseVisible(bool value)
		{
			ScreenManager._activeMouseVisible = value;
			ScreenManager._engineInterface.SetMouseVisible(value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004502 File Offset: 0x00002702
		public static bool GetMouseVisibility()
		{
			return ScreenManager._activeMouseVisible;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000450C File Offset: 0x0000270C
		public static void TrySetFocus(ScreenLayer layer)
		{
			if (ScreenManager.FocusedLayer != null && ScreenManager.FocusedLayer.InputRestrictions.Order > layer.InputRestrictions.Order && layer.IsActive)
			{
				return;
			}
			if (!layer.IsFocusLayer && !layer.FocusTest())
			{
				return;
			}
			if (ScreenManager.FocusedLayer != layer)
			{
				ScreenManager._focusedLayerChangedThisFrame = true;
				if (ScreenManager.FocusedLayer != null)
				{
					ScreenManager.FocusedLayer.OnLoseFocus();
				}
			}
			ScreenManager.FocusedLayer = layer;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000457C File Offset: 0x0000277C
		public static void TryLoseFocus(ScreenLayer layer)
		{
			if (ScreenManager.FocusedLayer != layer)
			{
				return;
			}
			ScreenLayer focusedLayer = ScreenManager.FocusedLayer;
			if (focusedLayer != null)
			{
				focusedLayer.OnLoseFocus();
			}
			for (int i = ScreenManager.SortedLayers.Count - 1; i >= 0; i--)
			{
				ScreenLayer screenLayer = ScreenManager.SortedLayers[i];
				if (screenLayer.IsActive && screenLayer.IsFocusLayer && layer != screenLayer)
				{
					ScreenManager.FocusedLayer = screenLayer;
					ScreenManager._focusedLayerChangedThisFrame = true;
					return;
				}
			}
			ScreenManager.FocusedLayer = null;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000045EC File Offset: 0x000027EC
		private static bool FocusTest(ScreenLayer layer)
		{
			if (Input.IsGamepadActive && layer.InputRestrictions.CanOverrideFocusOnHit)
			{
				return layer.IsHitThisFrame;
			}
			return ScreenManager.FocusedLayer == layer;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004614 File Offset: 0x00002814
		public static void OnScaleChange(float newScale)
		{
			ScreenManager.Scale = newScale;
			foreach (GlobalLayer globalLayer in ScreenManager._globalLayers)
			{
				globalLayer.UpdateLayout();
			}
			foreach (ScreenBase screenBase in ScreenManager._screenList)
			{
				screenBase.UpdateLayout();
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000469C File Offset: 0x0000289C
		public static void OnControllerDisconnect()
		{
			ScreenManager.OnControllerDisconnectedEvent onControllerDisconnected = ScreenManager.OnControllerDisconnected;
			if (onControllerDisconnected == null)
			{
				return;
			}
			onControllerDisconnected();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000046B0 File Offset: 0x000028B0
		private static void OnScreenListChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Debug.Print("OnScreenListChanged", 0, Debug.DebugColor.White, 17592186044416UL);
			ScreenManager._isSortedActiveLayersDirty = true;
			ObservableCollection<ScreenBase> screenList = ScreenManager._screenList;
			if (screenList != null && screenList.Count > 0)
			{
				if (ScreenManager.TopScreen != null)
				{
					ScreenManager.TopScreen.OnAddLayer -= ScreenManager.OnLayerAddedToTopLayer;
					ScreenManager.TopScreen.OnRemoveLayer -= ScreenManager.OnLayerRemovedFromTopLayer;
				}
				ScreenManager.TopScreen = ScreenManager._screenList[ScreenManager._screenList.Count - 1];
				if (ScreenManager.TopScreen != null)
				{
					ScreenManager.TopScreen.OnAddLayer += ScreenManager.OnLayerAddedToTopLayer;
					ScreenManager.TopScreen.OnRemoveLayer += ScreenManager.OnLayerRemovedFromTopLayer;
				}
			}
			else
			{
				if (ScreenManager.TopScreen != null)
				{
					ScreenManager.TopScreen.OnAddLayer -= ScreenManager.OnLayerAddedToTopLayer;
					ScreenManager.TopScreen.OnRemoveLayer -= ScreenManager.OnLayerRemovedFromTopLayer;
				}
				ScreenManager.TopScreen = null;
			}
			ScreenManager._isSortedActiveLayersDirty = true;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000047B4 File Offset: 0x000029B4
		private static void OnLayerAddedToTopLayer(ScreenLayer layer)
		{
			ScreenManager._isSortedActiveLayersDirty = true;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000047BC File Offset: 0x000029BC
		private static void OnLayerRemovedFromTopLayer(ScreenLayer layer)
		{
			ScreenManager._isSortedActiveLayersDirty = true;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000047C4 File Offset: 0x000029C4
		private static void OnGlobalListChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			ScreenManager._isSortedActiveLayersDirty = true;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000047CC File Offset: 0x000029CC
		[CommandLineFunctionality.CommandLineArgumentFunction("set_screen_debug_information_enabled", "ui")]
		public static string SetScreenDebugInformationEnabled(List<string> args)
		{
			string result = "Usage: ui.set_screen_debug_information_enabled [True/False]";
			if (args.Count != 1)
			{
				return result;
			}
			bool screenDebugInformationEnabled;
			if (bool.TryParse(args[0], out screenDebugInformationEnabled))
			{
				ScreenManager.SetScreenDebugInformationEnabled(screenDebugInformationEnabled);
				return "Success.";
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004809 File Offset: 0x00002A09
		public static void SetScreenDebugInformationEnabled(bool isEnabled)
		{
			ScreenManager._isScreenDebugInformationEnabled = isEnabled;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004814 File Offset: 0x00002A14
		private static void ShowScreenDebugInformation()
		{
			if (ScreenManager._isScreenDebugInformationEnabled)
			{
				ScreenManager._engineInterface.BeginDebugPanel("Screen Debug Information");
				for (int i = 0; i < ScreenManager.SortedLayers.Count; i++)
				{
					ScreenLayer screenLayer = ScreenManager.SortedLayers[i];
					if (ScreenManager._engineInterface.DrawDebugTreeNode(string.Format("{0}###{1}.{2}.{3}", new object[]
					{
						screenLayer.GetType().Name,
						screenLayer.Name,
						i,
						screenLayer.Name.GetDeterministicHashCode()
					})))
					{
						screenLayer.DrawDebugInfo();
						ScreenManager._engineInterface.PopDebugTreeNode();
					}
				}
				ScreenManager._engineInterface.EndDebugPanel();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000048C4 File Offset: 0x00002AC4
		private static void OnUsableAreaChanged(Vec2 newUsableArea)
		{
			ScreenManager.UpdateLayout();
		}

		// Token: 0x0400002E RID: 46
		private static IScreenManagerEngineConnection _engineInterface;

		// Token: 0x04000030 RID: 48
		private static Vec2 _usableArea = new Vec2(1f, 1f);

		// Token: 0x04000034 RID: 52
		private static List<ScreenLayer> _lateTickLayers;

		// Token: 0x04000035 RID: 53
		private static ObservableCollection<ScreenBase> _screenList;

		// Token: 0x04000036 RID: 54
		private static ObservableCollection<GlobalLayer> _globalLayers;

		// Token: 0x04000037 RID: 55
		private static List<ScreenLayer> _sortedLayers = new List<ScreenLayer>(16);

		// Token: 0x04000038 RID: 56
		private static ScreenLayer[] _sortedActiveLayersCopyForUpdate = new ScreenLayer[16];

		// Token: 0x04000039 RID: 57
		private static bool _isSortedActiveLayersDirty = true;

		// Token: 0x0400003A RID: 58
		private static bool _focusedLayerChangedThisFrame;

		// Token: 0x0400003B RID: 59
		private static bool _isMouseInputActiveLastFrame;

		// Token: 0x0400003C RID: 60
		private static bool _isScreenDebugInformationEnabled;

		// Token: 0x0400003E RID: 62
		private static bool _activeMouseVisible = true;

		// Token: 0x0400003F RID: 63
		private static IReadOnlyList<int> _lastPressedKeys;

		// Token: 0x04000040 RID: 64
		private static bool _globalOrderDirty;

		// Token: 0x04000043 RID: 67
		private static bool _isRefreshActive = false;

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x060000E8 RID: 232
		public delegate void OnPushScreenEvent(ScreenBase pushedScreen);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x060000EC RID: 236
		public delegate void OnPopScreenEvent(ScreenBase poppedScreen);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x060000F0 RID: 240
		public delegate void OnControllerDisconnectedEvent();
	}
}
