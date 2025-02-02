using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x0200002D RID: 45
	internal class ScriptingInterfaceOfIThumbnailCreatorView : IThumbnailCreatorView
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00016554 File Offset: 0x00014754
		public void CancelRequest(UIntPtr pointer, string render_id)
		{
			byte[] array = null;
			if (render_id != null)
			{
				int byteCount = ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetByteCount(render_id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetBytes(render_id, 0, render_id.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIThumbnailCreatorView.call_CancelRequestDelegate(pointer, array);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000165AF File Offset: 0x000147AF
		public void ClearRequests(UIntPtr pointer)
		{
			ScriptingInterfaceOfIThumbnailCreatorView.call_ClearRequestsDelegate(pointer);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000165BC File Offset: 0x000147BC
		public ThumbnailCreatorView CreateThumbnailCreatorView()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIThumbnailCreatorView.call_CreateThumbnailCreatorViewDelegate();
			ThumbnailCreatorView result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new ThumbnailCreatorView(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00016605 File Offset: 0x00014805
		public int GetNumberOfPendingRequests(UIntPtr pointer)
		{
			return ScriptingInterfaceOfIThumbnailCreatorView.call_GetNumberOfPendingRequestsDelegate(pointer);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00016612 File Offset: 0x00014812
		public bool IsMemoryCleared(UIntPtr pointer)
		{
			return ScriptingInterfaceOfIThumbnailCreatorView.call_IsMemoryClearedDelegate(pointer);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00016620 File Offset: 0x00014820
		public void RegisterEntity(UIntPtr pointer, UIntPtr scene_ptr, UIntPtr cam_ptr, UIntPtr texture_ptr, UIntPtr entity_ptr, string render_id, int allocationGroupIndex)
		{
			byte[] array = null;
			if (render_id != null)
			{
				int byteCount = ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetByteCount(render_id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetBytes(render_id, 0, render_id.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIThumbnailCreatorView.call_RegisterEntityDelegate(pointer, scene_ptr, cam_ptr, texture_ptr, entity_ptr, array, allocationGroupIndex);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00016688 File Offset: 0x00014888
		public void RegisterEntityWithoutTexture(UIntPtr pointer, UIntPtr scene_ptr, UIntPtr cam_ptr, UIntPtr entity_ptr, int width, int height, string render_id, string debug_name, int allocationGroupIndex)
		{
			byte[] array = null;
			if (render_id != null)
			{
				int byteCount = ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetByteCount(render_id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetBytes(render_id, 0, render_id.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (debug_name != null)
			{
				int byteCount2 = ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetByteCount(debug_name);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIThumbnailCreatorView._utf8.GetBytes(debug_name, 0, debug_name.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIThumbnailCreatorView.call_RegisterEntityWithoutTextureDelegate(pointer, scene_ptr, cam_ptr, entity_ptr, width, height, array, array2, allocationGroupIndex);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00016738 File Offset: 0x00014938
		public void RegisterScene(UIntPtr pointer, UIntPtr scene_ptr, bool use_postfx)
		{
			ScriptingInterfaceOfIThumbnailCreatorView.call_RegisterSceneDelegate(pointer, scene_ptr, use_postfx);
		}

		// Token: 0x0400049D RID: 1181
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400049E RID: 1182
		public static ScriptingInterfaceOfIThumbnailCreatorView.CancelRequestDelegate call_CancelRequestDelegate;

		// Token: 0x0400049F RID: 1183
		public static ScriptingInterfaceOfIThumbnailCreatorView.ClearRequestsDelegate call_ClearRequestsDelegate;

		// Token: 0x040004A0 RID: 1184
		public static ScriptingInterfaceOfIThumbnailCreatorView.CreateThumbnailCreatorViewDelegate call_CreateThumbnailCreatorViewDelegate;

		// Token: 0x040004A1 RID: 1185
		public static ScriptingInterfaceOfIThumbnailCreatorView.GetNumberOfPendingRequestsDelegate call_GetNumberOfPendingRequestsDelegate;

		// Token: 0x040004A2 RID: 1186
		public static ScriptingInterfaceOfIThumbnailCreatorView.IsMemoryClearedDelegate call_IsMemoryClearedDelegate;

		// Token: 0x040004A3 RID: 1187
		public static ScriptingInterfaceOfIThumbnailCreatorView.RegisterEntityDelegate call_RegisterEntityDelegate;

		// Token: 0x040004A4 RID: 1188
		public static ScriptingInterfaceOfIThumbnailCreatorView.RegisterEntityWithoutTextureDelegate call_RegisterEntityWithoutTextureDelegate;

		// Token: 0x040004A5 RID: 1189
		public static ScriptingInterfaceOfIThumbnailCreatorView.RegisterSceneDelegate call_RegisterSceneDelegate;

		// Token: 0x020004EA RID: 1258
		// (Invoke) Token: 0x060018AB RID: 6315
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CancelRequestDelegate(UIntPtr pointer, byte[] render_id);

		// Token: 0x020004EB RID: 1259
		// (Invoke) Token: 0x060018AF RID: 6319
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearRequestsDelegate(UIntPtr pointer);

		// Token: 0x020004EC RID: 1260
		// (Invoke) Token: 0x060018B3 RID: 6323
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateThumbnailCreatorViewDelegate();

		// Token: 0x020004ED RID: 1261
		// (Invoke) Token: 0x060018B7 RID: 6327
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumberOfPendingRequestsDelegate(UIntPtr pointer);

		// Token: 0x020004EE RID: 1262
		// (Invoke) Token: 0x060018BB RID: 6331
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsMemoryClearedDelegate(UIntPtr pointer);

		// Token: 0x020004EF RID: 1263
		// (Invoke) Token: 0x060018BF RID: 6335
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RegisterEntityDelegate(UIntPtr pointer, UIntPtr scene_ptr, UIntPtr cam_ptr, UIntPtr texture_ptr, UIntPtr entity_ptr, byte[] render_id, int allocationGroupIndex);

		// Token: 0x020004F0 RID: 1264
		// (Invoke) Token: 0x060018C3 RID: 6339
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RegisterEntityWithoutTextureDelegate(UIntPtr pointer, UIntPtr scene_ptr, UIntPtr cam_ptr, UIntPtr entity_ptr, int width, int height, byte[] render_id, byte[] debug_name, int allocationGroupIndex);

		// Token: 0x020004F1 RID: 1265
		// (Invoke) Token: 0x060018C7 RID: 6343
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RegisterSceneDelegate(UIntPtr pointer, UIntPtr scene_ptr, [MarshalAs(UnmanagedType.U1)] bool use_postfx);
	}
}
