using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace TaleWorlds.Library.NewsManager
{
	// Token: 0x020000A4 RID: 164
	public class NewsManager
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00013C02 File Offset: 0x00011E02
		public MBReadOnlyList<NewsItem> NewsItems
		{
			get
			{
				return this._newsItems;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00013C0A File Offset: 0x00011E0A
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00013C12 File Offset: 0x00011E12
		public bool IsInPreviewMode { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00013C1B File Offset: 0x00011E1B
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00013C23 File Offset: 0x00011E23
		public string LocalizationID { get; private set; }

		// Token: 0x06000605 RID: 1541 RVA: 0x00013C2C File Offset: 0x00011E2C
		public NewsManager()
		{
			this._newsItems = new MBList<NewsItem>();
			this.UpdateConfigSettings();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00013C4C File Offset: 0x00011E4C
		public async Task<MBReadOnlyList<NewsItem>> GetNewsItems(bool forceRefresh)
		{
			await this.UpdateNewsItems(forceRefresh);
			return this.NewsItems;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00013C99 File Offset: 0x00011E99
		public void SetNewsSourceURL(string url)
		{
			this._newsSourceURL = url;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00013CA4 File Offset: 0x00011EA4
		public async Task UpdateNewsItems(bool forceRefresh)
		{
			if (ApplicationPlatform.CurrentPlatform != Platform.Durango && ApplicationPlatform.CurrentPlatform != Platform.GDKDesktop)
			{
				if (this._isNewsItemCacheDirty || forceRefresh)
				{
					try
					{
						if (Uri.IsWellFormedUriString(this._newsSourceURL, UriKind.Absolute))
						{
							this._newsItems = await NewsManager.DeserializeObjectAsync<MBList<NewsItem>>(await HttpHelper.DownloadStringTaskAsync(this._newsSourceURL));
						}
						else
						{
							Debug.FailedAssert("News file doesn't exist", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\NewsSystem\\NewsManager.cs", "UpdateNewsItems", 73);
						}
					}
					catch (Exception)
					{
					}
					this._isNewsItemCacheDirty = false;
				}
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00013CF4 File Offset: 0x00011EF4
		public static Task<T> DeserializeObjectAsync<T>(string json)
		{
			Task<T> result;
			try
			{
				using (new StringReader(json))
				{
					result = Task.FromResult<T>(JsonConvert.DeserializeObject<T>(json));
				}
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				result = Task.FromResult<T>(default(T));
			}
			return result;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00013D64 File Offset: 0x00011F64
		private void UpdateConfigSettings()
		{
			this._configPath = this.GetConfigXMLPath();
			this.IsInPreviewMode = false;
			this.LocalizationID = "en";
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(this._configPath);
				this.IsInPreviewMode = this.GetIsInPreviewMode(xmlDocument);
				this.LocalizationID = this.GetLocalizationCode(xmlDocument);
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00013DE8 File Offset: 0x00011FE8
		private bool GetIsInPreviewMode(XmlDocument configDocument)
		{
			return configDocument != null && configDocument.HasChildNodes && bool.Parse(configDocument.ChildNodes[0].SelectSingleNode("UsePreviewLink").Attributes["Value"].InnerText);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00013E26 File Offset: 0x00012026
		private string GetLocalizationCode(XmlDocument configDocument)
		{
			if (configDocument != null && configDocument.HasChildNodes)
			{
				return configDocument.ChildNodes[0].SelectSingleNode("LocalizationID").Attributes["Value"].InnerText;
			}
			return "en";
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00013E64 File Offset: 0x00012064
		public void UpdateLocalizationID(string localizationID)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(this._configPath);
			if (xmlDocument.HasChildNodes)
			{
				xmlDocument.ChildNodes[0].SelectSingleNode("LocalizationID").Attributes["Value"].Value = localizationID;
			}
			xmlDocument.Save(this._configPath);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00013EC4 File Offset: 0x000120C4
		private PlatformFilePath GetConfigXMLPath()
		{
			PlatformDirectoryPath folderPath = new PlatformDirectoryPath(PlatformFileType.User, "Configs");
			PlatformFilePath platformFilePath = new PlatformFilePath(folderPath, "NewsFeedConfig.xml");
			bool flag = FileHelper.FileExists(platformFilePath);
			bool flag2 = true;
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					xmlDocument.Load(platformFilePath);
					flag2 = (xmlDocument.HasChildNodes && xmlDocument.FirstChild.HasChildNodes);
				}
				catch (Exception ex)
				{
					Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
					flag2 = false;
				}
			}
			if (!flag || !flag2)
			{
				try
				{
					XmlDocument xmlDocument2 = new XmlDocument();
					XmlNode xmlNode = xmlDocument2.CreateElement("Root");
					xmlDocument2.AppendChild(xmlNode);
					((XmlElement)xmlNode.AppendChild(xmlDocument2.CreateElement("LocalizationID"))).SetAttribute("Value", "en");
					((XmlElement)xmlNode.AppendChild(xmlDocument2.CreateElement("UsePreviewLink"))).SetAttribute("Value", "False");
					xmlDocument2.Save(platformFilePath);
				}
				catch (Exception ex2)
				{
					Debug.Print(ex2.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				}
			}
			return platformFilePath;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00013FF4 File Offset: 0x000121F4
		public void OnFinalize()
		{
			MBList<NewsItem> newsItems = this._newsItems;
			if (newsItems != null)
			{
				newsItems.Clear();
			}
			this._newsItems = null;
			this.LocalizationID = null;
		}

		// Token: 0x040001B8 RID: 440
		private string _newsSourceURL;

		// Token: 0x040001B9 RID: 441
		private MBList<NewsItem> _newsItems;

		// Token: 0x040001BA RID: 442
		private bool _isNewsItemCacheDirty = true;

		// Token: 0x040001BD RID: 445
		private PlatformFilePath _configPath;

		// Token: 0x040001BE RID: 446
		private const string DataFolder = "Configs";

		// Token: 0x040001BF RID: 447
		private const string FileName = "NewsFeedConfig.xml";
	}
}
