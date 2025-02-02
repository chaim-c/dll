using System;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Sound
{
	// Token: 0x0200005C RID: 92
	public class MusicSilencedMissionView : MissionView, IMusicHandler
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x000220E8 File Offset: 0x000202E8
		bool IMusicHandler.IsPausable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000220EB File Offset: 0x000202EB
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			MBMusicManager.Current.DeactivateCurrentMode();
			MBMusicManager.Current.OnSilencedMusicHandlerInit(this);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00022108 File Offset: 0x00020308
		public override void OnMissionScreenFinalize()
		{
			MBMusicManager.Current.OnSilencedMusicHandlerFinalize();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00022114 File Offset: 0x00020314
		void IMusicHandler.OnUpdated(float dt)
		{
		}
	}
}
