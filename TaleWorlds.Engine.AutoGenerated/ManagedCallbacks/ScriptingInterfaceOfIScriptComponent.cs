using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000025 RID: 37
	internal class ScriptingInterfaceOfIScriptComponent : IScriptComponent
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00014A44 File Offset: 0x00012C44
		public ScriptComponentBehavior GetScriptComponentBehavior(UIntPtr pointer)
		{
			return DotNetObject.GetManagedObjectWithId(ScriptingInterfaceOfIScriptComponent.call_GetScriptComponentBehaviorDelegate(pointer)) as ScriptComponentBehavior;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014A5C File Offset: 0x00012C5C
		public void SetVariableEditorWidgetStatus(UIntPtr pointer, string field, bool enabled)
		{
			byte[] array = null;
			if (field != null)
			{
				int byteCount = ScriptingInterfaceOfIScriptComponent._utf8.GetByteCount(field);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScriptComponent._utf8.GetBytes(field, 0, field.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScriptComponent.call_SetVariableEditorWidgetStatusDelegate(pointer, array, enabled);
		}

		// Token: 0x040003FC RID: 1020
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040003FD RID: 1021
		public static ScriptingInterfaceOfIScriptComponent.GetScriptComponentBehaviorDelegate call_GetScriptComponentBehaviorDelegate;

		// Token: 0x040003FE RID: 1022
		public static ScriptingInterfaceOfIScriptComponent.SetVariableEditorWidgetStatusDelegate call_SetVariableEditorWidgetStatusDelegate;

		// Token: 0x02000451 RID: 1105
		// (Invoke) Token: 0x06001647 RID: 5703
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetScriptComponentBehaviorDelegate(UIntPtr pointer);

		// Token: 0x02000452 RID: 1106
		// (Invoke) Token: 0x0600164B RID: 5707
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVariableEditorWidgetStatusDelegate(UIntPtr pointer, byte[] field, [MarshalAs(UnmanagedType.U1)] bool enabled);
	}
}
