using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200003D RID: 61
	[ApplicationInterfaceBase]
	internal interface IMusic
	{
		// Token: 0x06000546 RID: 1350
		[EngineMethod("get_free_music_channel_index", false)]
		int GetFreeMusicChannelIndex();

		// Token: 0x06000547 RID: 1351
		[EngineMethod("load_clip", false)]
		void LoadClip(int index, string pathToClip);

		// Token: 0x06000548 RID: 1352
		[EngineMethod("unload_clip", false)]
		void UnloadClip(int index);

		// Token: 0x06000549 RID: 1353
		[EngineMethod("is_clip_loaded", false)]
		bool IsClipLoaded(int index);

		// Token: 0x0600054A RID: 1354
		[EngineMethod("play_music", false)]
		void PlayMusic(int index);

		// Token: 0x0600054B RID: 1355
		[EngineMethod("play_delayed", false)]
		void PlayDelayed(int index, int delayMilliseconds);

		// Token: 0x0600054C RID: 1356
		[EngineMethod("is_music_playing", false)]
		bool IsMusicPlaying(int index);

		// Token: 0x0600054D RID: 1357
		[EngineMethod("pause_music", false)]
		void PauseMusic(int index);

		// Token: 0x0600054E RID: 1358
		[EngineMethod("stop_music", false)]
		void StopMusic(int index);

		// Token: 0x0600054F RID: 1359
		[EngineMethod("set_volume", false)]
		void SetVolume(int index, float volume);
	}
}
