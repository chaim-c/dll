using System;
using TaleWorlds.PlayerServices;
using TaleWorlds.PlayerServices.Avatar;

namespace TaleWorlds.PlatformService.Steam
{
	// Token: 0x02000005 RID: 5
	public class SteamPlatformAvatarService : IAvatarService
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000024DA File Offset: 0x000006DA
		public SteamPlatformAvatarService(SteamPlatformServices steamPlatformServices)
		{
			this._steamPlatformServices = steamPlatformServices;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024EC File Offset: 0x000006EC
		public AvatarData GetPlayerAvatar(PlayerId playerId)
		{
			AvatarData avatarData = new AvatarData();
			this.FetchPlayerAvatar(avatarData, playerId);
			return avatarData;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002508 File Offset: 0x00000708
		public async void FetchPlayerAvatar(AvatarData avatarData, PlayerId playerId)
		{
			AvatarData avatarData2 = await((IPlatformServices)this._steamPlatformServices).GetUserAvatar(playerId);
			if (avatarData2 != null)
			{
				if (avatarData2.Width > 0U && avatarData2.Height > 0U)
				{
					avatarData.SetImageData(avatarData2.Image, avatarData2.Width, avatarData2.Height);
				}
				else
				{
					avatarData.SetImageData(avatarData2.Image);
				}
			}
			else
			{
				avatarData.SetFailed();
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002551 File Offset: 0x00000751
		public void Initialize()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002553 File Offset: 0x00000753
		public void ClearCache()
		{
			this._steamPlatformServices.ClearAvatarCache();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002560 File Offset: 0x00000760
		public bool IsInitialized()
		{
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002563 File Offset: 0x00000763
		public void Tick(float dt)
		{
		}

		// Token: 0x0400000E RID: 14
		private SteamPlatformServices _steamPlatformServices;
	}
}
