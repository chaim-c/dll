using System;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.Avatar.PlayerServices;
using TaleWorlds.Library;

namespace TaleWorlds.PlayerServices.Avatar
{
	// Token: 0x0200000A RID: 10
	internal class ForcedAvatarService : IAvatarService
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002F23 File Offset: 0x00001123
		public int AvatarCount
		{
			get
			{
				return this._avatarImagesAsByteArrays.Count;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F30 File Offset: 0x00001130
		public AvatarData GetPlayerAvatar(PlayerId playerId)
		{
			if (this._avatarImagesAsByteArrays.Count == 0)
			{
				return new AvatarData();
			}
			return this.GetForcedPlayerAvatar(AvatarServices.GetForcedAvatarIndexOfPlayer(playerId));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F51 File Offset: 0x00001151
		private AvatarData GetForcedPlayerAvatar(int forcedIndex)
		{
			return new AvatarData(this._avatarImagesAsByteArrays[forcedIndex]);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F64 File Offset: 0x00001164
		public void Initialize()
		{
			if (this._isInitialized)
			{
				return;
			}
			this._avatarImagesAsByteArrays.Clear();
			if (Directory.Exists(this._resourceFolder))
			{
				foreach (string path in Directory.GetFiles(this._resourceFolder, "*.png"))
				{
					this._avatarImagesAsByteArrays.Add(File.ReadAllBytes(path));
				}
			}
			this._isInitialized = true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FCD File Offset: 0x000011CD
		public bool IsInitialized()
		{
			return this._isInitialized;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FD5 File Offset: 0x000011D5
		public void ClearCache()
		{
			if (!this._isInitialized)
			{
				return;
			}
			this._avatarImagesAsByteArrays.Clear();
			this._isInitialized = false;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FF2 File Offset: 0x000011F2
		public void Tick(float dt)
		{
		}

		// Token: 0x04000026 RID: 38
		private readonly string _resourceFolder = BasePath.Name + "Modules/Native/MultiplayerForcedAvatars/";

		// Token: 0x04000027 RID: 39
		private readonly List<byte[]> _avatarImagesAsByteArrays = new List<byte[]>();

		// Token: 0x04000028 RID: 40
		private bool _isInitialized;
	}
}
