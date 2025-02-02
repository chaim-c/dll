using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B7 RID: 439
	[ScriptingInterfaceBase]
	internal interface IMBDebugExtensions
	{
		// Token: 0x060017D4 RID: 6100
		[EngineMethod("render_debug_circle_on_terrain", false)]
		void RenderDebugCircleOnTerrain(UIntPtr scenePointer, ref MatrixFrame frame, float radius, uint color, bool depthCheck, bool isDotted);

		// Token: 0x060017D5 RID: 6101
		[EngineMethod("render_debug_arc_on_terrain", false)]
		void RenderDebugArcOnTerrain(UIntPtr scenePointer, ref MatrixFrame frame, float radius, float beginAngle, float endAngle, uint color, bool depthCheck, bool isDotted);

		// Token: 0x060017D6 RID: 6102
		[EngineMethod("render_debug_line_on_terrain", false)]
		void RenderDebugLineOnTerrain(UIntPtr scenePointer, Vec3 position, Vec3 direction, uint color, bool depthCheck, float time, bool isDotted, float pointDensity);

		// Token: 0x060017D7 RID: 6103
		[EngineMethod("override_native_parameter", false)]
		void OverrideNativeParameter(string paramName, float value);

		// Token: 0x060017D8 RID: 6104
		[EngineMethod("reload_native_parameters", false)]
		void ReloadNativeParameters();
	}
}
