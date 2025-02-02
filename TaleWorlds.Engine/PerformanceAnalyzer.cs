using System;
using System.Collections.Generic;
using System.Xml;

namespace TaleWorlds.Engine
{
	// Token: 0x02000072 RID: 114
	public class PerformanceAnalyzer
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x00008CEC File Offset: 0x00006EEC
		public void Start(string name)
		{
			PerformanceAnalyzer.PerformanceObject item = new PerformanceAnalyzer.PerformanceObject(name);
			this.currentObject = item;
			this.objects.Add(item);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00008D13 File Offset: 0x00006F13
		public void End()
		{
			this.currentObject = null;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00008D1C File Offset: 0x00006F1C
		public void FinalizeAndWrite(string filePath)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlNode xmlNode = xmlDocument.CreateElement("objects");
				xmlDocument.AppendChild(xmlNode);
				foreach (PerformanceAnalyzer.PerformanceObject performanceObject in this.objects)
				{
					XmlNode xmlNode2 = xmlDocument.CreateElement("object");
					performanceObject.Write(xmlNode2, xmlDocument);
					xmlNode.AppendChild(xmlNode2);
				}
				xmlDocument.Save(filePath);
			}
			catch (Exception ex)
			{
				MBDebug.ShowWarning("Exception occurred while trying to write " + filePath + ": " + ex.ToString());
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00008DD4 File Offset: 0x00006FD4
		public void Tick(float dt)
		{
			if (this.currentObject != null)
			{
				this.currentObject.AddFps(Utilities.GetFps(), Utilities.GetMainFps(), Utilities.GetRendererFps());
			}
		}

		// Token: 0x04000151 RID: 337
		private List<PerformanceAnalyzer.PerformanceObject> objects = new List<PerformanceAnalyzer.PerformanceObject>();

		// Token: 0x04000152 RID: 338
		private PerformanceAnalyzer.PerformanceObject currentObject;

		// Token: 0x020000C3 RID: 195
		private class PerformanceObject
		{
			// Token: 0x170000AD RID: 173
			// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0000FA9E File Offset: 0x0000DC9E
			private float AverageMainFps
			{
				get
				{
					if (this.frameCount > 0)
					{
						return this.totalMainFps / (float)this.frameCount;
					}
					return 0f;
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0000FABD File Offset: 0x0000DCBD
			private float AverageRendererFps
			{
				get
				{
					if (this.frameCount > 0)
					{
						return this.totalRendererFps / (float)this.frameCount;
					}
					return 0f;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0000FADC File Offset: 0x0000DCDC
			private float AverageFps
			{
				get
				{
					if (this.frameCount > 0)
					{
						return this.totalFps / (float)this.frameCount;
					}
					return 0f;
				}
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x0000FAFB File Offset: 0x0000DCFB
			public void AddFps(float fps, float main, float renderer)
			{
				this.frameCount++;
				this.totalFps += fps;
				this.totalMainFps += main;
				this.totalRendererFps += renderer;
			}

			// Token: 0x06000C97 RID: 3223 RVA: 0x0000FB38 File Offset: 0x0000DD38
			public void Write(XmlNode node, XmlDocument document)
			{
				XmlAttribute xmlAttribute = document.CreateAttribute("name");
				xmlAttribute.Value = this.name;
				node.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = document.CreateAttribute("frameCount");
				xmlAttribute2.Value = this.frameCount.ToString();
				node.Attributes.Append(xmlAttribute2);
				XmlAttribute xmlAttribute3 = document.CreateAttribute("averageFps");
				xmlAttribute3.Value = this.AverageFps.ToString();
				node.Attributes.Append(xmlAttribute3);
				XmlAttribute xmlAttribute4 = document.CreateAttribute("averageMainFps");
				xmlAttribute4.Value = this.AverageMainFps.ToString();
				node.Attributes.Append(xmlAttribute4);
				XmlAttribute xmlAttribute5 = document.CreateAttribute("averageRendererFps");
				xmlAttribute5.Value = this.AverageRendererFps.ToString();
				node.Attributes.Append(xmlAttribute5);
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x0000FC21 File Offset: 0x0000DE21
			public PerformanceObject(string objectName)
			{
				this.name = objectName;
			}

			// Token: 0x04000407 RID: 1031
			private string name;

			// Token: 0x04000408 RID: 1032
			private int frameCount;

			// Token: 0x04000409 RID: 1033
			private float totalMainFps;

			// Token: 0x0400040A RID: 1034
			private float totalRendererFps;

			// Token: 0x0400040B RID: 1035
			private float totalFps;
		}
	}
}
