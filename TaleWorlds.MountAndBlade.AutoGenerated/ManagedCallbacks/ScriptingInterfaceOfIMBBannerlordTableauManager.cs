using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200000F RID: 15
	internal class ScriptingInterfaceOfIMBBannerlordTableauManager : IMBBannerlordTableauManager
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000A4EA File Offset: 0x000086EA
		public int GetNumberOfPendingTableauRequests()
		{
			return ScriptingInterfaceOfIMBBannerlordTableauManager.call_GetNumberOfPendingTableauRequestsDelegate();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A4F6 File Offset: 0x000086F6
		public void InitializeCharacterTableauRenderSystem()
		{
			ScriptingInterfaceOfIMBBannerlordTableauManager.call_InitializeCharacterTableauRenderSystemDelegate();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A504 File Offset: 0x00008704
		public void RequestCharacterTableauRender(int characterCodeId, string path, UIntPtr poseEntity, UIntPtr cameraObject, int tableauType)
		{
			byte[] array = null;
			if (path != null)
			{
				int byteCount = ScriptingInterfaceOfIMBBannerlordTableauManager._utf8.GetByteCount(path);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBBannerlordTableauManager._utf8.GetBytes(path, 0, path.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBBannerlordTableauManager.call_RequestCharacterTableauRenderDelegate(characterCodeId, array, poseEntity, cameraObject, tableauType);
		}

		// Token: 0x04000164 RID: 356
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000165 RID: 357
		public static ScriptingInterfaceOfIMBBannerlordTableauManager.GetNumberOfPendingTableauRequestsDelegate call_GetNumberOfPendingTableauRequestsDelegate;

		// Token: 0x04000166 RID: 358
		public static ScriptingInterfaceOfIMBBannerlordTableauManager.InitializeCharacterTableauRenderSystemDelegate call_InitializeCharacterTableauRenderSystemDelegate;

		// Token: 0x04000167 RID: 359
		public static ScriptingInterfaceOfIMBBannerlordTableauManager.RequestCharacterTableauRenderDelegate call_RequestCharacterTableauRenderDelegate;

		// Token: 0x020001C9 RID: 457
		// (Invoke) Token: 0x060009C9 RID: 2505
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumberOfPendingTableauRequestsDelegate();

		// Token: 0x020001CA RID: 458
		// (Invoke) Token: 0x060009CD RID: 2509
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InitializeCharacterTableauRenderSystemDelegate();

		// Token: 0x020001CB RID: 459
		// (Invoke) Token: 0x060009D1 RID: 2513
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RequestCharacterTableauRenderDelegate(int characterCodeId, byte[] path, UIntPtr poseEntity, UIntPtr cameraObject, int tableauType);
	}
}
