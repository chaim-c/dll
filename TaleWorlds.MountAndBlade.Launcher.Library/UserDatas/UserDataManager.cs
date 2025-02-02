using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TaleWorlds.MountAndBlade.Launcher.Library.UserDatas
{
	// Token: 0x0200001E RID: 30
	public class UserDataManager
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000599A File Offset: 0x00003B9A
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000059A2 File Offset: 0x00003BA2
		public UserData UserData { get; private set; }

		// Token: 0x0600012D RID: 301 RVA: 0x000059AC File Offset: 0x00003BAC
		public UserDataManager()
		{
			this.UserData = new UserData();
			string text = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			text += "\\Mount and Blade II Bannerlord\\Configs\\";
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch (Exception value)
				{
					Console.WriteLine(value);
				}
			}
			this._filePath = text + "LauncherData.xml";
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005A18 File Offset: 0x00003C18
		public bool HasUserData()
		{
			return File.Exists(this._filePath);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005A28 File Offset: 0x00003C28
		public void LoadUserData()
		{
			if (!File.Exists(this._filePath))
			{
				return;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserData));
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(this._filePath))
				{
					this.UserData = (UserData)xmlSerializer.Deserialize(xmlReader);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public void SaveUserData()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserData));
			try
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(this._filePath, new XmlWriterSettings
				{
					Indent = true
				}))
				{
					xmlSerializer.Serialize(xmlWriter, this.UserData);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x04000093 RID: 147
		private const string DataFolder = "\\Mount and Blade II Bannerlord\\Configs\\";

		// Token: 0x04000094 RID: 148
		private const string FileName = "LauncherData.xml";

		// Token: 0x04000095 RID: 149
		private readonly string _filePath;
	}
}
