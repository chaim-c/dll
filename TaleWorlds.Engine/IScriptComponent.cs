using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200002A RID: 42
	[ApplicationInterfaceBase]
	internal interface IScriptComponent
	{
		// Token: 0x060003FF RID: 1023
		[EngineMethod("get_script_component_behavior", false)]
		ScriptComponentBehavior GetScriptComponentBehavior(UIntPtr pointer);

		// Token: 0x06000400 RID: 1024
		[EngineMethod("set_variable_editor_widget_status", false)]
		void SetVariableEditorWidgetStatus(UIntPtr pointer, string field, bool enabled);
	}
}
