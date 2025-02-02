using System;
using TaleWorlds.DotNet;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.Engine
{
	// Token: 0x02000044 RID: 68
	internal class EngineScreenManager
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x00003722 File Offset: 0x00001922
		[EngineCallback]
		internal static void PreTick(float dt)
		{
			ScreenManager.EarlyUpdate(EngineApplicationInterface.IScreen.GetUsableAreaPercentages());
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00003734 File Offset: 0x00001934
		[EngineCallback]
		public static void Tick(float dt)
		{
			bool mouseVisible = EngineApplicationInterface.IScreen.GetMouseVisible();
			ScreenManager.Tick(dt, mouseVisible);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00003753 File Offset: 0x00001953
		[EngineCallback]
		internal static void LateTick(float dt)
		{
			ScreenManager.LateTick(dt);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000375B File Offset: 0x0000195B
		[EngineCallback]
		internal static void OnOnscreenKeyboardDone(string inputText)
		{
			ScreenManager.OnOnscreenKeyboardDone(inputText);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00003763 File Offset: 0x00001963
		[EngineCallback]
		internal static void OnOnscreenKeyboardCanceled()
		{
			ScreenManager.OnOnscreenKeyboardCanceled();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000376A File Offset: 0x0000196A
		[EngineCallback]
		internal static void OnGameWindowFocusChange(bool focusGained)
		{
			ScreenManager.OnGameWindowFocusChange(focusGained);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00003772 File Offset: 0x00001972
		[EngineCallback]
		internal static void Update()
		{
			ScreenManager.Update(EngineScreenManager._lastPressedKeys);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000377E File Offset: 0x0000197E
		[EngineCallback]
		internal static void InitializeLastPressedKeys(NativeArray lastKeysPressed)
		{
			EngineScreenManager._lastPressedKeys = new NativeArrayEnumerator<int>(lastKeysPressed);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000378B File Offset: 0x0000198B
		internal static void Initialize()
		{
			ScreenManager.Initialize(new ScreenManagerEngineConnection());
		}

		// Token: 0x04000057 RID: 87
		private static NativeArrayEnumerator<int> _lastPressedKeys;
	}
}
