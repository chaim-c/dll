﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;
using TaleWorlds.TwoDimension.Standalone;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000003 RID: 3
	public class LauncherOnlineImageTextureProvider : TextureProvider
	{
		// Token: 0x17000001 RID: 1
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021D3 File Offset: 0x000003D3
		public string OnlineSourceUrl
		{
			set
			{
				this._onlineSourceUrl = value;
				if (!string.IsNullOrEmpty(this._onlineSourceUrl))
				{
					this.RefreshOnlineImage();
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021F0 File Offset: 0x000003F0
		public LauncherOnlineImageTextureProvider()
		{
			this._onlineImageCache = new Dictionary<string, string>();
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			this._onlineImageCacheFolderPath = Path.Combine(folderPath, "Mount and Blade II Bannerlord/Online Images/");
			if (!Directory.Exists(this._onlineImageCacheFolderPath))
			{
				try
				{
					Directory.CreateDirectory(this._onlineImageCacheFolderPath);
				}
				catch (Exception ex)
				{
					Debug.Print("Could not create directory for launcher images at \"" + this._onlineImageCacheFolderPath + "\": " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				}
			}
			this.PopulateOnlineImageCache();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002288 File Offset: 0x00000488
		public override void Tick(float dt)
		{
			base.Tick(dt);
			if (this._requiresRetry)
			{
				if (this._retryCount > 10)
				{
					Debug.FailedAssert("Couldn't download " + this._onlineSourceUrl, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Launcher.Library\\LauncherOnlineImageTextureProvider.cs", "Tick", 68);
					this._requiresRetry = false;
					return;
				}
				this._retryCount++;
				this.RefreshOnlineImage();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022EC File Offset: 0x000004EC
		private async void RefreshOnlineImage()
		{
			if (this._retryCount < 10)
			{
				try
				{
					this._texture = null;
					string guidOfRequestedURL = LauncherOnlineImageTextureProvider.ToGuid(this._onlineSourceUrl).ToString();
					if (!this._onlineImageCache.ContainsKey(guidOfRequestedURL))
					{
						using (WebClient client = new WebClient())
						{
							string pathOfTheDownloadedImage = this._onlineImageCacheFolderPath + guidOfRequestedURL + ".png";
							Task downloadTask = client.DownloadFileTaskAsync(new Uri(this._onlineSourceUrl), pathOfTheDownloadedImage);
							await downloadTask;
							if (downloadTask.Status == TaskStatus.RanToCompletion)
							{
								this._onlineImageCache.Add(guidOfRequestedURL, pathOfTheDownloadedImage);
							}
							pathOfTheDownloadedImage = null;
							downloadTask = null;
						}
						WebClient client = null;
					}
					string fullFilePath;
					if (this._onlineImageCache.TryGetValue(guidOfRequestedURL, out fullFilePath))
					{
						OpenGLTexture openGLTexture = OpenGLTexture.FromFile(fullFilePath);
						if (openGLTexture == null)
						{
							this._onlineImageCache.Remove(guidOfRequestedURL);
							Debug.Print(string.Format("RETRYING TO DOWNLOAD: {0} | RETRY COUNT: {1}", this._onlineSourceUrl, this._retryCount), 0, Debug.DebugColor.Red, 17592186044416UL);
							this._requiresRetry = true;
						}
						else
						{
							openGLTexture.ClampToEdge = true;
							Texture texture = new Texture(openGLTexture);
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
					Debug.FailedAssert("Error while trying to get image online: " + ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Launcher.Library\\LauncherOnlineImageTextureProvider.cs", "RefreshOnlineImage", 130);
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002325 File Offset: 0x00000525
		public override Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			return this._texture;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002330 File Offset: 0x00000530
		private void PopulateOnlineImageCache()
		{
			if (Directory.Exists(this._onlineImageCacheFolderPath))
			{
				string[] array = new string[0];
				try
				{
					array = Directory.GetFiles(this._onlineImageCacheFolderPath, "*.png");
				}
				catch (Exception ex)
				{
					Debug.Print("Could not load launcher images at \"" + this._onlineImageCacheFolderPath + "\": " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				}
				foreach (string text in array)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					this._onlineImageCache.Add(fileNameWithoutExtension, text);
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023D0 File Offset: 0x000005D0
		private static Guid ToGuid(string src)
		{
			if (!string.IsNullOrEmpty(src))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(src);
				byte[] b = new SHA1CryptoServiceProvider().ComputeHash(bytes);
				Array.Resize<byte>(ref b, 16);
				return new Guid(b);
			}
			return Guid.Empty;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002412 File Offset: 0x00000612
		private void OnTextureCreated(Texture texture)
		{
			this._texture = texture;
		}

		// Token: 0x04000002 RID: 2
		private Dictionary<string, string> _onlineImageCache;

		// Token: 0x04000003 RID: 3
		private const string DataFolder = "Mount and Blade II Bannerlord/Online Images/";

		// Token: 0x04000004 RID: 4
		private readonly string _onlineImageCacheFolderPath;

		// Token: 0x04000005 RID: 5
		private Texture _texture;

		// Token: 0x04000006 RID: 6
		private bool _requiresRetry;

		// Token: 0x04000007 RID: 7
		private int _retryCount;

		// Token: 0x04000008 RID: 8
		private const int _maxRetryCount = 10;

		// Token: 0x04000009 RID: 9
		private string _onlineSourceUrl;
	}
}
