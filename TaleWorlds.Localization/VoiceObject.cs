using System;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.Localization
{
	// Token: 0x0200000C RID: 12
	public class VoiceObject
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000042E2 File Offset: 0x000024E2
		public MBReadOnlyList<string> VoicePaths
		{
			get
			{
				return this._voicePaths;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000042EA File Offset: 0x000024EA
		private VoiceObject()
		{
			this._voicePaths = new MBList<string>();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000042FD File Offset: 0x000024FD
		private void AddVoicePath(string voicePath)
		{
			this._voicePaths.Add(voicePath);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000430C File Offset: 0x0000250C
		public void AddVoicePaths(XmlNode node, string modulePath)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Voice")
				{
					string voicePath = modulePath + "/" + xmlNode.Attributes["path"].InnerText;
					this.AddVoicePath(voicePath);
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004398 File Offset: 0x00002598
		public static VoiceObject Deserialize(XmlNode node, string modulePath)
		{
			VoiceObject voiceObject = new VoiceObject();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "Voice")
				{
					string voicePath = modulePath + "/" + xmlNode.Attributes["path"].InnerText;
					voiceObject.AddVoicePath(voicePath);
				}
			}
			return voiceObject;
		}

		// Token: 0x0400002A RID: 42
		private readonly MBList<string> _voicePaths;
	}
}
