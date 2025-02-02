using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004F RID: 79
	public class Imgui
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x00004F57 File Offset: 0x00003157
		public static void BeginMainThreadScope()
		{
			EngineApplicationInterface.IImgui.BeginMainThreadScope();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00004F63 File Offset: 0x00003163
		public static void EndMainThreadScope()
		{
			EngineApplicationInterface.IImgui.EndMainThreadScope();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00004F6F File Offset: 0x0000316F
		public static void PushStyleColor(Imgui.ColorStyle style, ref Vec3 color)
		{
			EngineApplicationInterface.IImgui.PushStyleColor((int)style, ref color);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00004F7D File Offset: 0x0000317D
		public static void PopStyleColor()
		{
			EngineApplicationInterface.IImgui.PopStyleColor();
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00004F89 File Offset: 0x00003189
		public static void NewFrame()
		{
			EngineApplicationInterface.IImgui.NewFrame();
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00004F95 File Offset: 0x00003195
		public static void Render()
		{
			EngineApplicationInterface.IImgui.Render();
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00004FA1 File Offset: 0x000031A1
		public static void Begin(string text)
		{
			EngineApplicationInterface.IImgui.Begin(text);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00004FAE File Offset: 0x000031AE
		public static void Begin(string text, ref bool is_open)
		{
			EngineApplicationInterface.IImgui.BeginWithCloseButton(text, ref is_open);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00004FBC File Offset: 0x000031BC
		public static void End()
		{
			EngineApplicationInterface.IImgui.End();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00004FC8 File Offset: 0x000031C8
		public static void Text(string text)
		{
			EngineApplicationInterface.IImgui.Text(text);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00004FD5 File Offset: 0x000031D5
		public static bool Checkbox(string text, ref bool is_checked)
		{
			return EngineApplicationInterface.IImgui.Checkbox(text, ref is_checked);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00004FE3 File Offset: 0x000031E3
		public static bool TreeNode(string name)
		{
			return EngineApplicationInterface.IImgui.TreeNode(name);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00004FF0 File Offset: 0x000031F0
		public static void TreePop()
		{
			EngineApplicationInterface.IImgui.TreePop();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00004FFC File Offset: 0x000031FC
		public static void Separator()
		{
			EngineApplicationInterface.IImgui.Separator();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00005008 File Offset: 0x00003208
		public static bool Button(string text)
		{
			return EngineApplicationInterface.IImgui.Button(text);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00005018 File Offset: 0x00003218
		public static void PlotLines(string name, float[] values, int valuesCount, int valuesOffset, string overlayText, float minScale, float maxScale, float graphWidth, float graphHeight, int stride)
		{
			EngineApplicationInterface.IImgui.PlotLines(name, values, valuesCount, valuesOffset, overlayText, minScale, maxScale, graphWidth, graphHeight, stride);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000503F File Offset: 0x0000323F
		public static void ProgressBar(float progress)
		{
			EngineApplicationInterface.IImgui.ProgressBar(progress);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000504C File Offset: 0x0000324C
		public static void NewLine()
		{
			EngineApplicationInterface.IImgui.NewLine();
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00005058 File Offset: 0x00003258
		public static void SameLine(float posX = 0f, float spacingWidth = 0f)
		{
			EngineApplicationInterface.IImgui.SameLine(posX, spacingWidth);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00005066 File Offset: 0x00003266
		public static bool Combo(string label, ref int selectedIndex, string items)
		{
			return EngineApplicationInterface.IImgui.Combo(label, ref selectedIndex, items);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00005075 File Offset: 0x00003275
		public static bool InputInt(string label, ref int value)
		{
			return EngineApplicationInterface.IImgui.InputInt(label, ref value);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00005083 File Offset: 0x00003283
		public static bool SliderFloat(string label, ref float value, float min, float max)
		{
			return EngineApplicationInterface.IImgui.SliderFloat(label, ref value, min, max);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00005093 File Offset: 0x00003293
		public static void Columns(int count = 1, string id = "", bool border = true)
		{
			EngineApplicationInterface.IImgui.Columns(count, id, border);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000050A2 File Offset: 0x000032A2
		public static void NextColumn()
		{
			EngineApplicationInterface.IImgui.NextColumn();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000050AE File Offset: 0x000032AE
		public static bool RadioButton(string label, bool active)
		{
			return EngineApplicationInterface.IImgui.RadioButton(label, active);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000050BC File Offset: 0x000032BC
		public static bool CollapsingHeader(string label)
		{
			return EngineApplicationInterface.IImgui.CollapsingHeader(label);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000050C9 File Offset: 0x000032C9
		public static bool IsItemHovered()
		{
			return EngineApplicationInterface.IImgui.IsItemHovered();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000050D5 File Offset: 0x000032D5
		public static void SetTooltip(string label)
		{
			EngineApplicationInterface.IImgui.SetTooltip(label);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000050E2 File Offset: 0x000032E2
		public static bool SmallButton(string label)
		{
			return EngineApplicationInterface.IImgui.SmallButton(label);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000050EF File Offset: 0x000032EF
		public static bool InputFloat(string label, ref float val, float step, float stepFast, int decimalPrecision = -1)
		{
			return EngineApplicationInterface.IImgui.InputFloat(label, ref val, step, stepFast, decimalPrecision);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00005101 File Offset: 0x00003301
		public static bool InputFloat2(string label, ref float val0, ref float val1, int decimalPrecision = -1)
		{
			return EngineApplicationInterface.IImgui.InputFloat2(label, ref val0, ref val1, decimalPrecision);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00005111 File Offset: 0x00003311
		public static bool InputFloat3(string label, ref float val0, ref float val1, ref float val2, int decimalPrecision = -1)
		{
			return EngineApplicationInterface.IImgui.InputFloat3(label, ref val0, ref val1, ref val2, decimalPrecision);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00005123 File Offset: 0x00003323
		public static bool InputFloat4(string label, ref float val0, ref float val1, ref float val2, ref float val3, int decimalPrecision = -1)
		{
			return EngineApplicationInterface.IImgui.InputFloat4(label, ref val0, ref val1, ref val2, ref val3, decimalPrecision);
		}

		// Token: 0x020000B8 RID: 184
		public enum ColorStyle
		{
			// Token: 0x04000392 RID: 914
			Text,
			// Token: 0x04000393 RID: 915
			TextDisabled,
			// Token: 0x04000394 RID: 916
			WindowBg,
			// Token: 0x04000395 RID: 917
			ChildWindowBg,
			// Token: 0x04000396 RID: 918
			PopupBg,
			// Token: 0x04000397 RID: 919
			Border,
			// Token: 0x04000398 RID: 920
			BorderShadow,
			// Token: 0x04000399 RID: 921
			FrameBg,
			// Token: 0x0400039A RID: 922
			FrameBgHovered,
			// Token: 0x0400039B RID: 923
			FrameBgActive,
			// Token: 0x0400039C RID: 924
			TitleBg,
			// Token: 0x0400039D RID: 925
			TitleBgCollapsed,
			// Token: 0x0400039E RID: 926
			TitleBgActive,
			// Token: 0x0400039F RID: 927
			MenuBarBg,
			// Token: 0x040003A0 RID: 928
			ScrollbarBg,
			// Token: 0x040003A1 RID: 929
			ScrollbarGrab,
			// Token: 0x040003A2 RID: 930
			ScrollbarGrabHovered,
			// Token: 0x040003A3 RID: 931
			ScrollbarGrabActive,
			// Token: 0x040003A4 RID: 932
			ComboBg,
			// Token: 0x040003A5 RID: 933
			CheckMark,
			// Token: 0x040003A6 RID: 934
			SliderGrab,
			// Token: 0x040003A7 RID: 935
			SliderGrabActive,
			// Token: 0x040003A8 RID: 936
			Button,
			// Token: 0x040003A9 RID: 937
			ButtonHovered,
			// Token: 0x040003AA RID: 938
			ButtonActive,
			// Token: 0x040003AB RID: 939
			Header,
			// Token: 0x040003AC RID: 940
			HeaderHovered,
			// Token: 0x040003AD RID: 941
			HeaderActive,
			// Token: 0x040003AE RID: 942
			Column,
			// Token: 0x040003AF RID: 943
			ColumnHovered,
			// Token: 0x040003B0 RID: 944
			ColumnActive,
			// Token: 0x040003B1 RID: 945
			ResizeGrip,
			// Token: 0x040003B2 RID: 946
			ResizeGripHovered,
			// Token: 0x040003B3 RID: 947
			ResizeGripActive,
			// Token: 0x040003B4 RID: 948
			CloseButton,
			// Token: 0x040003B5 RID: 949
			CloseButtonHovered,
			// Token: 0x040003B6 RID: 950
			CloseButtonActive,
			// Token: 0x040003B7 RID: 951
			PlotLines,
			// Token: 0x040003B8 RID: 952
			PlotLinesHovered,
			// Token: 0x040003B9 RID: 953
			PlotHistogram,
			// Token: 0x040003BA RID: 954
			PlotHistogramHovered,
			// Token: 0x040003BB RID: 955
			TextSelectedBg,
			// Token: 0x040003BC RID: 956
			ModalWindowDarkening
		}
	}
}
