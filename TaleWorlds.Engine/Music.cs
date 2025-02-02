using System;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006B RID: 107
	public class Music
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x00008729 File Offset: 0x00006929
		public static int GetFreeMusicChannelIndex()
		{
			return EngineApplicationInterface.IMusic.GetFreeMusicChannelIndex();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00008735 File Offset: 0x00006935
		public static void LoadClip(int index, string pathToClip)
		{
			EngineApplicationInterface.IMusic.LoadClip(index, pathToClip);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00008743 File Offset: 0x00006943
		public static void UnloadClip(int index)
		{
			EngineApplicationInterface.IMusic.UnloadClip(index);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00008750 File Offset: 0x00006950
		public static bool IsClipLoaded(int index)
		{
			return EngineApplicationInterface.IMusic.IsClipLoaded(index);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0000875D File Offset: 0x0000695D
		public static void PlayMusic(int index)
		{
			EngineApplicationInterface.IMusic.PlayMusic(index);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0000876A File Offset: 0x0000696A
		public static void PlayDelayed(int index, int deltaMilliseconds)
		{
			EngineApplicationInterface.IMusic.PlayDelayed(index, deltaMilliseconds);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00008778 File Offset: 0x00006978
		public static bool IsMusicPlaying(int index)
		{
			return EngineApplicationInterface.IMusic.IsMusicPlaying(index);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00008785 File Offset: 0x00006985
		public static void PauseMusic(int index)
		{
			EngineApplicationInterface.IMusic.PauseMusic(index);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00008792 File Offset: 0x00006992
		public static void StopMusic(int index)
		{
			EngineApplicationInterface.IMusic.StopMusic(index);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0000879F File Offset: 0x0000699F
		public static void SetVolume(int index, float volume)
		{
			EngineApplicationInterface.IMusic.SetVolume(index, volume);
		}
	}
}
