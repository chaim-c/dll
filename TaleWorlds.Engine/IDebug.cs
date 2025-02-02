using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000034 RID: 52
	[ApplicationInterfaceBase]
	internal interface IDebug
	{
		// Token: 0x0600045C RID: 1116
		[EngineMethod("write_debug_line_on_screen", false)]
		void WriteDebugLineOnScreen(string line);

		// Token: 0x0600045D RID: 1117
		[EngineMethod("abort_game", false)]
		void AbortGame(int ExitCode);

		// Token: 0x0600045E RID: 1118
		[EngineMethod("assert_memory_usage", false)]
		void AssertMemoryUsage(int memoryMB);

		// Token: 0x0600045F RID: 1119
		[EngineMethod("write_line", false)]
		void WriteLine(int logLevel, string line, int color, ulong filter);

		// Token: 0x06000460 RID: 1120
		[EngineMethod("render_debug_direction_arrow", false)]
		void RenderDebugDirectionArrow(Vec3 position, Vec3 direction, uint color, bool depthCheck);

		// Token: 0x06000461 RID: 1121
		[EngineMethod("render_debug_line", false)]
		void RenderDebugLine(Vec3 position, Vec3 direction, uint color, bool depthCheck, float time);

		// Token: 0x06000462 RID: 1122
		[EngineMethod("render_debug_sphere", false)]
		void RenderDebugSphere(Vec3 position, float radius, uint color, bool depthCheck, float time);

		// Token: 0x06000463 RID: 1123
		[EngineMethod("render_debug_capsule", false)]
		void RenderDebugCapsule(Vec3 p0, Vec3 p1, float radius, uint color, bool depthCheck, float time);

		// Token: 0x06000464 RID: 1124
		[EngineMethod("render_debug_frame", false)]
		void RenderDebugFrame(ref MatrixFrame frame, float lineLength, float time);

		// Token: 0x06000465 RID: 1125
		[EngineMethod("render_debug_text3d", false)]
		void RenderDebugText3d(Vec3 worldPosition, string str, uint color, int screenPosOffsetX, int screenPosOffsetY, float time);

		// Token: 0x06000466 RID: 1126
		[EngineMethod("render_debug_text", false)]
		void RenderDebugText(float screenX, float screenY, string str, uint color, float time);

		// Token: 0x06000467 RID: 1127
		[EngineMethod("render_debug_rect", false)]
		void RenderDebugRect(float left, float bottom, float right, float top);

		// Token: 0x06000468 RID: 1128
		[EngineMethod("render_debug_rect_with_color", false)]
		void RenderDebugRectWithColor(float left, float bottom, float right, float top, uint color);

		// Token: 0x06000469 RID: 1129
		[EngineMethod("clear_all_debug_render_objects", false)]
		void ClearAllDebugRenderObjects();

		// Token: 0x0600046A RID: 1130
		[EngineMethod("get_debug_vector", false)]
		Vec3 GetDebugVector();

		// Token: 0x0600046B RID: 1131
		[EngineMethod("render_debug_box_object", false)]
		void RenderDebugBoxObject(Vec3 min, Vec3 max, uint color, bool depthCheck, float time);

		// Token: 0x0600046C RID: 1132
		[EngineMethod("render_debug_box_object_with_frame", false)]
		void RenderDebugBoxObjectWithFrame(Vec3 min, Vec3 max, ref MatrixFrame frame, uint color, bool depthCheck, float time);

		// Token: 0x0600046D RID: 1133
		[EngineMethod("post_warning_line", false)]
		void PostWarningLine(string line);

		// Token: 0x0600046E RID: 1134
		[EngineMethod("is_error_report_mode_active", false)]
		bool IsErrorReportModeActive();

		// Token: 0x0600046F RID: 1135
		[EngineMethod("is_error_report_mode_pause_mission", false)]
		bool IsErrorReportModePauseMission();

		// Token: 0x06000470 RID: 1136
		[EngineMethod("set_error_report_scene", false)]
		void SetErrorReportScene(UIntPtr scenePointer);

		// Token: 0x06000471 RID: 1137
		[EngineMethod("set_dump_generation_disabled", false)]
		void SetDumpGenerationDisabled(bool Disabled);

		// Token: 0x06000472 RID: 1138
		[EngineMethod("message_box", false)]
		int MessageBox(string lpText, string lpCaption, uint uType);

		// Token: 0x06000473 RID: 1139
		[EngineMethod("get_show_debug_info", false)]
		int GetShowDebugInfo();

		// Token: 0x06000474 RID: 1140
		[EngineMethod("set_show_debug_info", false)]
		void SetShowDebugInfo(int value);

		// Token: 0x06000475 RID: 1141
		[EngineMethod("error", false)]
		bool Error(string MessageString);

		// Token: 0x06000476 RID: 1142
		[EngineMethod("warning", false)]
		bool Warning(string MessageString);

		// Token: 0x06000477 RID: 1143
		[EngineMethod("content_warning", false)]
		bool ContentWarning(string MessageString);

		// Token: 0x06000478 RID: 1144
		[EngineMethod("failed_assert", false)]
		bool FailedAssert(string messageString, string callerFile, string callerMethod, int callerLine);

		// Token: 0x06000479 RID: 1145
		[EngineMethod("silent_assert", false)]
		bool SilentAssert(string messageString, string callerFile, string callerMethod, int callerLine, bool getDump);

		// Token: 0x0600047A RID: 1146
		[EngineMethod("is_test_mode", false)]
		bool IsTestMode();

		// Token: 0x0600047B RID: 1147
		[EngineMethod("echo_command_window", false)]
		void EchoCommandWindow(string content);
	}
}
