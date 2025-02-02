using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000058 RID: 88
	[EngineClass("rglManaged_script_component")]
	public sealed class ManagedScriptComponent : ScriptComponent
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x000062B1 File Offset: 0x000044B1
		public ScriptComponentBehavior ScriptComponentBehavior
		{
			get
			{
				return EngineApplicationInterface.IScriptComponent.GetScriptComponentBehavior(base.Pointer);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000062C3 File Offset: 0x000044C3
		public void SetVariableEditorWidgetStatus(string field, bool enabled)
		{
			EngineApplicationInterface.IScriptComponent.SetVariableEditorWidgetStatus(base.Pointer, field, enabled);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000062D7 File Offset: 0x000044D7
		private ManagedScriptComponent()
		{
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000062DF File Offset: 0x000044DF
		internal ManagedScriptComponent(UIntPtr pointer) : base(pointer)
		{
		}
	}
}
