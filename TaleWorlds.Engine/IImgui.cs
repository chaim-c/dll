using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200003E RID: 62
	[ApplicationInterfaceBase]
	internal interface IImgui
	{
		// Token: 0x06000550 RID: 1360
		[EngineMethod("begin_main_thread_scope", false)]
		void BeginMainThreadScope();

		// Token: 0x06000551 RID: 1361
		[EngineMethod("end_main_thread_scope", false)]
		void EndMainThreadScope();

		// Token: 0x06000552 RID: 1362
		[EngineMethod("push_style_color", false)]
		void PushStyleColor(int style, ref Vec3 color);

		// Token: 0x06000553 RID: 1363
		[EngineMethod("pop_style_color", false)]
		void PopStyleColor();

		// Token: 0x06000554 RID: 1364
		[EngineMethod("new_frame", false)]
		void NewFrame();

		// Token: 0x06000555 RID: 1365
		[EngineMethod("render", false)]
		void Render();

		// Token: 0x06000556 RID: 1366
		[EngineMethod("begin", false)]
		void Begin(string text);

		// Token: 0x06000557 RID: 1367
		[EngineMethod("begin_with_close_button", false)]
		void BeginWithCloseButton(string text, ref bool is_open);

		// Token: 0x06000558 RID: 1368
		[EngineMethod("end", false)]
		void End();

		// Token: 0x06000559 RID: 1369
		[EngineMethod("text", false)]
		void Text(string text);

		// Token: 0x0600055A RID: 1370
		[EngineMethod("checkbox", false)]
		bool Checkbox(string text, ref bool is_checked);

		// Token: 0x0600055B RID: 1371
		[EngineMethod("tree_node", false)]
		bool TreeNode(string name);

		// Token: 0x0600055C RID: 1372
		[EngineMethod("tree_pop", false)]
		void TreePop();

		// Token: 0x0600055D RID: 1373
		[EngineMethod("separator", false)]
		void Separator();

		// Token: 0x0600055E RID: 1374
		[EngineMethod("button", false)]
		bool Button(string text);

		// Token: 0x0600055F RID: 1375
		[EngineMethod("plot_lines", false)]
		void PlotLines(string name, float[] values, int valuesCount, int valuesOffset, string overlayText, float minScale, float maxScale, float graphWidth, float graphHeight, int stride);

		// Token: 0x06000560 RID: 1376
		[EngineMethod("progress_bar", false)]
		void ProgressBar(float value);

		// Token: 0x06000561 RID: 1377
		[EngineMethod("new_line", false)]
		void NewLine();

		// Token: 0x06000562 RID: 1378
		[EngineMethod("same_line", false)]
		void SameLine(float posX, float spacingWidth);

		// Token: 0x06000563 RID: 1379
		[EngineMethod("combo", false)]
		bool Combo(string label, ref int selectedIndex, string items);

		// Token: 0x06000564 RID: 1380
		[EngineMethod("input_int", false)]
		bool InputInt(string label, ref int value);

		// Token: 0x06000565 RID: 1381
		[EngineMethod("slider_float", false)]
		bool SliderFloat(string label, ref float value, float min, float max);

		// Token: 0x06000566 RID: 1382
		[EngineMethod("columns", false)]
		void Columns(int count = 1, string id = "", bool border = true);

		// Token: 0x06000567 RID: 1383
		[EngineMethod("next_column", false)]
		void NextColumn();

		// Token: 0x06000568 RID: 1384
		[EngineMethod("radio_button", false)]
		bool RadioButton(string label, bool active);

		// Token: 0x06000569 RID: 1385
		[EngineMethod("collapsing_header", false)]
		bool CollapsingHeader(string label);

		// Token: 0x0600056A RID: 1386
		[EngineMethod("is_item_hovered", false)]
		bool IsItemHovered();

		// Token: 0x0600056B RID: 1387
		[EngineMethod("set_tool_tip", false)]
		void SetTooltip(string label);

		// Token: 0x0600056C RID: 1388
		[EngineMethod("small_button", false)]
		bool SmallButton(string label);

		// Token: 0x0600056D RID: 1389
		[EngineMethod("input_float", false)]
		bool InputFloat(string label, ref float val, float step, float stepFast, int decimalPrecision = -1);

		// Token: 0x0600056E RID: 1390
		[EngineMethod("input_float2", false)]
		bool InputFloat2(string label, ref float val0, ref float val1, int decimalPrecision = -1);

		// Token: 0x0600056F RID: 1391
		[EngineMethod("input_float3", false)]
		bool InputFloat3(string label, ref float val0, ref float val1, ref float val2, int decimalPrecision = -1);

		// Token: 0x06000570 RID: 1392
		[EngineMethod("input_float4", false)]
		bool InputFloat4(string label, ref float val0, ref float val1, ref float val2, ref float val3, int decimalPrecision = -1);
	}
}
