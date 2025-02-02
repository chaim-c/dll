using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x0200001D RID: 29
	internal class ScriptingInterfaceOfIMusic : IMusic
	{
		// Token: 0x06000306 RID: 774 RVA: 0x000120AC File Offset: 0x000102AC
		public int GetFreeMusicChannelIndex()
		{
			return ScriptingInterfaceOfIMusic.call_GetFreeMusicChannelIndexDelegate();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000120B8 File Offset: 0x000102B8
		public bool IsClipLoaded(int index)
		{
			return ScriptingInterfaceOfIMusic.call_IsClipLoadedDelegate(index);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000120C5 File Offset: 0x000102C5
		public bool IsMusicPlaying(int index)
		{
			return ScriptingInterfaceOfIMusic.call_IsMusicPlayingDelegate(index);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000120D4 File Offset: 0x000102D4
		public void LoadClip(int index, string pathToClip)
		{
			byte[] array = null;
			if (pathToClip != null)
			{
				int byteCount = ScriptingInterfaceOfIMusic._utf8.GetByteCount(pathToClip);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMusic._utf8.GetBytes(pathToClip, 0, pathToClip.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMusic.call_LoadClipDelegate(index, array);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001212F File Offset: 0x0001032F
		public void PauseMusic(int index)
		{
			ScriptingInterfaceOfIMusic.call_PauseMusicDelegate(index);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001213C File Offset: 0x0001033C
		public void PlayDelayed(int index, int delayMilliseconds)
		{
			ScriptingInterfaceOfIMusic.call_PlayDelayedDelegate(index, delayMilliseconds);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001214A File Offset: 0x0001034A
		public void PlayMusic(int index)
		{
			ScriptingInterfaceOfIMusic.call_PlayMusicDelegate(index);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00012157 File Offset: 0x00010357
		public void SetVolume(int index, float volume)
		{
			ScriptingInterfaceOfIMusic.call_SetVolumeDelegate(index, volume);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00012165 File Offset: 0x00010365
		public void StopMusic(int index)
		{
			ScriptingInterfaceOfIMusic.call_StopMusicDelegate(index);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00012172 File Offset: 0x00010372
		public void UnloadClip(int index)
		{
			ScriptingInterfaceOfIMusic.call_UnloadClipDelegate(index);
		}

		// Token: 0x0400029E RID: 670
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400029F RID: 671
		public static ScriptingInterfaceOfIMusic.GetFreeMusicChannelIndexDelegate call_GetFreeMusicChannelIndexDelegate;

		// Token: 0x040002A0 RID: 672
		public static ScriptingInterfaceOfIMusic.IsClipLoadedDelegate call_IsClipLoadedDelegate;

		// Token: 0x040002A1 RID: 673
		public static ScriptingInterfaceOfIMusic.IsMusicPlayingDelegate call_IsMusicPlayingDelegate;

		// Token: 0x040002A2 RID: 674
		public static ScriptingInterfaceOfIMusic.LoadClipDelegate call_LoadClipDelegate;

		// Token: 0x040002A3 RID: 675
		public static ScriptingInterfaceOfIMusic.PauseMusicDelegate call_PauseMusicDelegate;

		// Token: 0x040002A4 RID: 676
		public static ScriptingInterfaceOfIMusic.PlayDelayedDelegate call_PlayDelayedDelegate;

		// Token: 0x040002A5 RID: 677
		public static ScriptingInterfaceOfIMusic.PlayMusicDelegate call_PlayMusicDelegate;

		// Token: 0x040002A6 RID: 678
		public static ScriptingInterfaceOfIMusic.SetVolumeDelegate call_SetVolumeDelegate;

		// Token: 0x040002A7 RID: 679
		public static ScriptingInterfaceOfIMusic.StopMusicDelegate call_StopMusicDelegate;

		// Token: 0x040002A8 RID: 680
		public static ScriptingInterfaceOfIMusic.UnloadClipDelegate call_UnloadClipDelegate;

		// Token: 0x020002FB RID: 763
		// (Invoke) Token: 0x060010EF RID: 4335
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFreeMusicChannelIndexDelegate();

		// Token: 0x020002FC RID: 764
		// (Invoke) Token: 0x060010F3 RID: 4339
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsClipLoadedDelegate(int index);

		// Token: 0x020002FD RID: 765
		// (Invoke) Token: 0x060010F7 RID: 4343
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsMusicPlayingDelegate(int index);

		// Token: 0x020002FE RID: 766
		// (Invoke) Token: 0x060010FB RID: 4347
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LoadClipDelegate(int index, byte[] pathToClip);

		// Token: 0x020002FF RID: 767
		// (Invoke) Token: 0x060010FF RID: 4351
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PauseMusicDelegate(int index);

		// Token: 0x02000300 RID: 768
		// (Invoke) Token: 0x06001103 RID: 4355
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PlayDelayedDelegate(int index, int delayMilliseconds);

		// Token: 0x02000301 RID: 769
		// (Invoke) Token: 0x06001107 RID: 4359
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PlayMusicDelegate(int index);

		// Token: 0x02000302 RID: 770
		// (Invoke) Token: 0x0600110B RID: 4363
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVolumeDelegate(int index, float volume);

		// Token: 0x02000303 RID: 771
		// (Invoke) Token: 0x0600110F RID: 4367
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StopMusicDelegate(int index);

		// Token: 0x02000304 RID: 772
		// (Invoke) Token: 0x06001113 RID: 4371
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UnloadClipDelegate(int index);
	}
}
