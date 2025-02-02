using System;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.Library;

namespace TaleWorlds.PlayerServices.Avatar
{
	// Token: 0x0200000E RID: 14
	public class TestAvatarService : IAvatarService
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000031B5 File Offset: 0x000013B5
		public TestAvatarService()
		{
			this._avatarImageCache = new Dictionary<ulong, AvatarData>();
			this._avatarImagesAsByteArrays = new List<byte[]>();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031E8 File Offset: 0x000013E8
		public void ClearCache()
		{
			if (!this._isInitialized)
			{
				return;
			}
			this._avatarImageCache.Clear();
			this._avatarImagesAsByteArrays.Clear();
			this._isInitialized = false;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003210 File Offset: 0x00001410
		public AvatarData GetPlayerAvatar(PlayerId playerId)
		{
			if (this._avatarImagesAsByteArrays.Count == 0)
			{
				return new AvatarData();
			}
			int index = (int)((uint)playerId.Id2 % (uint)this._avatarImagesAsByteArrays.Count);
			return new AvatarData(this._avatarImagesAsByteArrays[index]);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003258 File Offset: 0x00001458
		public void Initialize()
		{
			if (this._isInitialized)
			{
				return;
			}
			if (Directory.Exists(this._resourceFolder))
			{
				foreach (string path in Directory.GetFiles(this._resourceFolder, "*.jpg"))
				{
					this._avatarImagesAsByteArrays.Add(File.ReadAllBytes(path));
				}
			}
			this._isInitialized = true;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032B6 File Offset: 0x000014B6
		public bool IsInitialized()
		{
			return this._isInitialized;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032BE File Offset: 0x000014BE
		public void Tick(float dt)
		{
		}

		// Token: 0x04000030 RID: 48
		private readonly Dictionary<ulong, AvatarData> _avatarImageCache;

		// Token: 0x04000031 RID: 49
		private readonly string _resourceFolder = BasePath.Name + "Modules/Native/MultiplayerTestAvatars/";

		// Token: 0x04000032 RID: 50
		private readonly List<byte[]> _avatarImagesAsByteArrays;

		// Token: 0x04000033 RID: 51
		private bool _isInitialized;
	}
}
