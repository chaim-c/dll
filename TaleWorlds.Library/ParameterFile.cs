using System;
using System.IO;
using System.Xml;

namespace TaleWorlds.Library
{
	// Token: 0x02000070 RID: 112
	public class ParameterFile
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public string Path { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000CB5D File Offset: 0x0000AD5D
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000CB65 File Offset: 0x0000AD65
		public DateTime LastCheckedTime { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000CB6E File Offset: 0x0000AD6E
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000CB76 File Offset: 0x0000AD76
		public ParameterContainer ParameterContainer { get; private set; }

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000CB7F File Offset: 0x0000AD7F
		public ParameterFile(string path)
		{
			this.ParameterContainer = new ParameterContainer();
			this.Path = path;
			this.LastCheckedTime = DateTime.MinValue;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public bool CheckIfNeedsToBeRefreshed()
		{
			return File.GetLastWriteTime(this.Path) > this.LastCheckedTime;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public void Refresh()
		{
			this.ParameterContainer.ClearParameters();
			DateTime lastWriteTime = File.GetLastWriteTime(this.Path);
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(this.Path);
			}
			catch
			{
				this._failedAttemptsCount++;
				if (this._failedAttemptsCount >= 100)
				{
					Debug.FailedAssert("Could not load parameters file", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\ParameterFile.cs", "Refresh", 47);
				}
				return;
			}
			this._failedAttemptsCount = 0;
			foreach (object obj in xmlDocument.FirstChild.ChildNodes)
			{
				XmlElement xmlElement = (XmlElement)obj;
				string attribute = xmlElement.GetAttribute("name");
				string attribute2 = xmlElement.GetAttribute("value");
				this.ParameterContainer.AddParameter(attribute, attribute2, true);
			}
			this.LastCheckedTime = lastWriteTime;
		}

		// Token: 0x04000123 RID: 291
		private int _failedAttemptsCount;

		// Token: 0x04000124 RID: 292
		private const int MaxFailedAttemptsCount = 100;
	}
}
