using System;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.Engine
{
	// Token: 0x02000045 RID: 69
	public class ScreenManagerEngineConnection : IScreenManagerEngineConnection
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000379F File Offset: 0x0000199F
		float IScreenManagerEngineConnection.RealScreenResolutionWidth
		{
			get
			{
				return Screen.RealScreenResolutionWidth;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000037A6 File Offset: 0x000019A6
		float IScreenManagerEngineConnection.RealScreenResolutionHeight
		{
			get
			{
				return Screen.RealScreenResolutionHeight;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000037AD File Offset: 0x000019AD
		float IScreenManagerEngineConnection.AspectRatio
		{
			get
			{
				return Screen.AspectRatio;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000037B4 File Offset: 0x000019B4
		Vec2 IScreenManagerEngineConnection.DesktopResolution
		{
			get
			{
				return Screen.DesktopResolution;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000037BB File Offset: 0x000019BB
		void IScreenManagerEngineConnection.ActivateMouseCursor(CursorType mouseId)
		{
			MouseManager.ActivateMouseCursor(mouseId);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000037C3 File Offset: 0x000019C3
		void IScreenManagerEngineConnection.SetMouseVisible(bool value)
		{
			EngineApplicationInterface.IScreen.SetMouseVisible(value);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000037D0 File Offset: 0x000019D0
		bool IScreenManagerEngineConnection.GetMouseVisible()
		{
			return EngineApplicationInterface.IScreen.GetMouseVisible();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000037DC File Offset: 0x000019DC
		bool IScreenManagerEngineConnection.GetIsEnterButtonRDown()
		{
			return EngineApplicationInterface.IScreen.IsEnterButtonCross();
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000037E8 File Offset: 0x000019E8
		void IScreenManagerEngineConnection.BeginDebugPanel(string panelTitle)
		{
			Imgui.BeginMainThreadScope();
			Imgui.Begin(panelTitle);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000037F5 File Offset: 0x000019F5
		void IScreenManagerEngineConnection.EndDebugPanel()
		{
			Imgui.End();
			Imgui.EndMainThreadScope();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00003801 File Offset: 0x00001A01
		void IScreenManagerEngineConnection.DrawDebugText(string text)
		{
			Imgui.Text(text);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00003809 File Offset: 0x00001A09
		bool IScreenManagerEngineConnection.DrawDebugTreeNode(string text)
		{
			return Imgui.TreeNode(text);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00003811 File Offset: 0x00001A11
		void IScreenManagerEngineConnection.PopDebugTreeNode()
		{
			Imgui.TreePop();
		}
	}
}
