using System;
using System.Xml;

namespace TaleWorlds.Library
{
	// Token: 0x02000030 RID: 48
	public static class FileHelperExtensions
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public static void Load(this XmlDocument document, PlatformFilePath path)
		{
			string fileContentString = FileHelper.GetFileContentString(path);
			if (!string.IsNullOrEmpty(fileContentString))
			{
				document.LoadXml(fileContentString);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public static void Save(this XmlDocument document, PlatformFilePath path)
		{
			string outerXml = document.OuterXml;
			FileHelper.SaveFileString(path, outerXml);
		}
	}
}
