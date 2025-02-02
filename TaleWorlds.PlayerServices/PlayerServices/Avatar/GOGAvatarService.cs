using System;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.Library;

namespace TaleWorlds.PlayerServices.Avatar
{
	// Token: 0x0200000B RID: 11
	public class GOGAvatarService : IAvatarService
	{
		// Token: 0x06000065 RID: 101 RVA: 0x0000301C File Offset: 0x0000121C
		public void Initialize()
		{
			if (this._isInitalized)
			{
				return;
			}
			foreach (string path in Directory.GetFiles(this._resourceFolder, "*.png"))
			{
				this._avatarImagesAsByteArrays.Add(File.ReadAllBytes(path));
			}
			this._isInitalized = true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000306D File Offset: 0x0000126D
		public void ClearCache()
		{
			if (!this._isInitalized)
			{
				return;
			}
			this._avatarImageCache.Clear();
			this._avatarImagesAsByteArrays.Clear();
			this._isInitalized = false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003098 File Offset: 0x00001298
		public AvatarData GetPlayerAvatar(PlayerId playerId)
		{
			int index = (int)((uint)playerId.Id2 % (uint)this._avatarImagesAsByteArrays.Count);
			return new AvatarData(this._avatarImagesAsByteArrays[index]);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030CB File Offset: 0x000012CB
		public bool IsInitialized()
		{
			return this._isInitalized;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000030D3 File Offset: 0x000012D3
		public void Tick(float dt)
		{
		}

		// Token: 0x04000029 RID: 41
		private readonly Dictionary<ulong, AvatarData> _avatarImageCache = new Dictionary<ulong, AvatarData>();

		// Token: 0x0400002A RID: 42
		private readonly string _resourceFolder = BasePath.Name + "Modules/Native/MultiplayerForcedAvatars/";

		// Token: 0x0400002B RID: 43
		private readonly List<byte[]> _avatarImagesAsByteArrays = new List<byte[]>();

		// Token: 0x0400002C RID: 44
		private bool _isInitalized;
	}
}
