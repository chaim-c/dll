using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x0200001B RID: 27
	public class OnlineImageTextureProvider : TextureProvider
	{
		// Token: 0x17000045 RID: 69
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00007650 File Offset: 0x00005850
		public string OnlineSourceUrl
		{
			set
			{
				this._onlineSourceUrl = value;
				this.RefreshOnlineImage();
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000765F File Offset: 0x0000585F
		public OnlineImageTextureProvider()
		{
			this._onlineImageCache = new Dictionary<string, PlatformFilePath>();
			this._onlineImageCacheFolderPath = new PlatformDirectoryPath(PlatformFileType.Application, this.DataFolder);
			this.PopulateOnlineImageCache();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007695 File Offset: 0x00005895
		public override void Tick(float dt)
		{
			base.Tick(dt);
			if (this._requiresRetry)
			{
				if (10 < this._retryCount)
				{
					this._requiresRetry = false;
					return;
				}
				this._retryCount++;
				this.RefreshOnlineImage();
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000076CC File Offset: 0x000058CC
		private async void RefreshOnlineImage()
		{
			if (this._retryCount < 10)
			{
				try
				{
					string guidOfRequestedURL = OnlineImageTextureProvider.ToGuid(this._onlineSourceUrl).ToString();
					if (!this._onlineImageCache.ContainsKey(guidOfRequestedURL))
					{
						PlatformFilePath pathOfTheDownloadedImage = new PlatformFilePath(this._onlineImageCacheFolderPath, guidOfRequestedURL + ".png");
						byte[] array = await HttpHelper.DownloadDataTaskAsync(this._onlineSourceUrl);
						if (array != null)
						{
							FileHelper.SaveFile(pathOfTheDownloadedImage, array);
							this._onlineImageCache.Add(guidOfRequestedURL, pathOfTheDownloadedImage);
						}
						pathOfTheDownloadedImage = default(PlatformFilePath);
					}
					PlatformFilePath filePath;
					if (this._onlineImageCache.TryGetValue(guidOfRequestedURL, out filePath))
					{
						TaleWorlds.Engine.Texture texture = TaleWorlds.Engine.Texture.CreateTextureFromPath(filePath);
						if (texture == null)
						{
							this._onlineImageCache.Remove(guidOfRequestedURL);
							Debug.Print(string.Format("RETRYING TO DOWNLOAD: {0} | RETRY COUNT: {1}", this._onlineSourceUrl, this._retryCount), 0, Debug.DebugColor.Red, 17592186044416UL);
							this._requiresRetry = true;
						}
						else
						{
							this.OnTextureCreated(texture);
							this._requiresRetry = false;
						}
					}
					else
					{
						Debug.Print(string.Format("RETRYING TO DOWNLOAD: {0} | RETRY COUNT: {1}", this._onlineSourceUrl, this._retryCount), 0, Debug.DebugColor.Red, 17592186044416UL);
						this._requiresRetry = true;
					}
					guidOfRequestedURL = null;
				}
				catch (Exception ex)
				{
					Debug.FailedAssert("Error while trying to get image online: " + ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\TextureProviders\\OnlineImageTextureProvider.cs", "RefreshOnlineImage", 109);
					Debug.Print(string.Format("RETRYING TO DOWNLOAD: {0} | RETRY COUNT: {1}", this._onlineSourceUrl, this._retryCount), 0, Debug.DebugColor.Red, 17592186044416UL);
					this._requiresRetry = true;
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007705 File Offset: 0x00005905
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			if (this._texture != null)
			{
				return new TaleWorlds.TwoDimension.Texture(new EngineTexture(this._texture));
			}
			return null;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007728 File Offset: 0x00005928
		private void PopulateOnlineImageCache()
		{
			foreach (PlatformFilePath value in FileHelper.GetFiles(this._onlineImageCacheFolderPath, "*.png"))
			{
				string fileNameWithoutExtension = value.GetFileNameWithoutExtension();
				this._onlineImageCache.Add(fileNameWithoutExtension, value);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007774 File Offset: 0x00005974
		private static Guid ToGuid(string src)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(src);
			byte[] b = new SHA1CryptoServiceProvider().ComputeHash(bytes);
			Array.Resize<byte>(ref b, 16);
			return new Guid(b);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000077A8 File Offset: 0x000059A8
		private void OnTextureCreated(TaleWorlds.Engine.Texture texture)
		{
			this._texture = texture;
		}

		// Token: 0x040000A4 RID: 164
		private Dictionary<string, PlatformFilePath> _onlineImageCache;

		// Token: 0x040000A5 RID: 165
		private readonly string DataFolder = "Online Images";

		// Token: 0x040000A6 RID: 166
		private readonly PlatformDirectoryPath _onlineImageCacheFolderPath;

		// Token: 0x040000A7 RID: 167
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x040000A8 RID: 168
		private bool _requiresRetry;

		// Token: 0x040000A9 RID: 169
		private int _retryCount;

		// Token: 0x040000AA RID: 170
		private const int _maxRetryCount = 10;

		// Token: 0x040000AB RID: 171
		private string _onlineSourceUrl;
	}
}
