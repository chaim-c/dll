using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000031 RID: 49
	internal class ScriptingInterfaceOfIVideoPlayerView : IVideoPlayerView
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x00018264 File Offset: 0x00016464
		public VideoPlayerView CreateVideoPlayerView()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIVideoPlayerView.call_CreateVideoPlayerViewDelegate();
			VideoPlayerView result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new VideoPlayerView(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000182AD File Offset: 0x000164AD
		public void Finalize(UIntPtr pointer)
		{
			ScriptingInterfaceOfIVideoPlayerView.call_FinalizeDelegate(pointer);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000182BA File Offset: 0x000164BA
		public bool IsVideoFinished(UIntPtr pointer)
		{
			return ScriptingInterfaceOfIVideoPlayerView.call_IsVideoFinishedDelegate(pointer);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000182C8 File Offset: 0x000164C8
		public void PlayVideo(UIntPtr pointer, string videoFileName, string soundFileName, float framerate)
		{
			byte[] array = null;
			if (videoFileName != null)
			{
				int byteCount = ScriptingInterfaceOfIVideoPlayerView._utf8.GetByteCount(videoFileName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIVideoPlayerView._utf8.GetBytes(videoFileName, 0, videoFileName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (soundFileName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIVideoPlayerView._utf8.GetByteCount(soundFileName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIVideoPlayerView._utf8.GetBytes(soundFileName, 0, soundFileName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIVideoPlayerView.call_PlayVideoDelegate(pointer, array, array2, framerate);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018368 File Offset: 0x00016568
		public void StopVideo(UIntPtr pointer)
		{
			ScriptingInterfaceOfIVideoPlayerView.call_StopVideoDelegate(pointer);
		}

		// Token: 0x0400053A RID: 1338
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400053B RID: 1339
		public static ScriptingInterfaceOfIVideoPlayerView.CreateVideoPlayerViewDelegate call_CreateVideoPlayerViewDelegate;

		// Token: 0x0400053C RID: 1340
		public static ScriptingInterfaceOfIVideoPlayerView.FinalizeDelegate call_FinalizeDelegate;

		// Token: 0x0400053D RID: 1341
		public static ScriptingInterfaceOfIVideoPlayerView.IsVideoFinishedDelegate call_IsVideoFinishedDelegate;

		// Token: 0x0400053E RID: 1342
		public static ScriptingInterfaceOfIVideoPlayerView.PlayVideoDelegate call_PlayVideoDelegate;

		// Token: 0x0400053F RID: 1343
		public static ScriptingInterfaceOfIVideoPlayerView.StopVideoDelegate call_StopVideoDelegate;

		// Token: 0x02000583 RID: 1411
		// (Invoke) Token: 0x06001B0F RID: 6927
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateVideoPlayerViewDelegate();

		// Token: 0x02000584 RID: 1412
		// (Invoke) Token: 0x06001B13 RID: 6931
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FinalizeDelegate(UIntPtr pointer);

		// Token: 0x02000585 RID: 1413
		// (Invoke) Token: 0x06001B17 RID: 6935
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsVideoFinishedDelegate(UIntPtr pointer);

		// Token: 0x02000586 RID: 1414
		// (Invoke) Token: 0x06001B1B RID: 6939
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PlayVideoDelegate(UIntPtr pointer, byte[] videoFileName, byte[] soundFileName, float framerate);

		// Token: 0x02000587 RID: 1415
		// (Invoke) Token: 0x06001B1F RID: 6943
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StopVideoDelegate(UIntPtr pointer);
	}
}
