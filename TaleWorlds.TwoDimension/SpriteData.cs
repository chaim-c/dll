using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200002F RID: 47
	public class SpriteData
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00007D1B File Offset: 0x00005F1B
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00007D23 File Offset: 0x00005F23
		public Dictionary<string, SpritePart> SpritePartNames { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00007D2C File Offset: 0x00005F2C
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00007D34 File Offset: 0x00005F34
		public Dictionary<string, Sprite> SpriteNames { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007D3D File Offset: 0x00005F3D
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00007D45 File Offset: 0x00005F45
		public Dictionary<string, SpriteCategory> SpriteCategories { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00007D4E File Offset: 0x00005F4E
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00007D56 File Offset: 0x00005F56
		public string Name { get; private set; }

		// Token: 0x06000203 RID: 515 RVA: 0x00007D5F File Offset: 0x00005F5F
		public SpriteData(string name)
		{
			this.Name = name;
			this.SpritePartNames = new Dictionary<string, SpritePart>();
			this.SpriteNames = new Dictionary<string, Sprite>();
			this.SpriteCategories = new Dictionary<string, SpriteCategory>();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007D90 File Offset: 0x00005F90
		public Sprite GetSprite(string name)
		{
			Sprite result;
			if (this.SpriteNames.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public bool SpriteExists(string spriteName)
		{
			return this.GetSprite(spriteName) != null;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007DBC File Offset: 0x00005FBC
		private void LoadFromDepot(ResourceDepot resourceDepot)
		{
			XmlDocument spriteData = new XmlDocument();
			foreach (string text in resourceDepot.GetFilesEndingWith(this.Name + ".xml"))
			{
				try
				{
					this.LoadSpriteDataFromFile(spriteData, text);
				}
				catch (Exception)
				{
					Debug.FailedAssert("Failed to load sprite data from file: " + text, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.TwoDimension\\SpriteData.cs", "LoadFromDepot", 58);
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007E50 File Offset: 0x00006050
		private void LoadSpriteDataFromFile(XmlDocument spriteData, string filePath)
		{
			spriteData.Load(filePath);
			XmlElement xmlElement = spriteData["SpriteData"];
			XmlNode xmlNode = xmlElement["SpriteCategories"];
			XmlNode xmlNode2 = xmlElement["SpriteParts"];
			XmlNode xmlNode3 = xmlElement["Sprites"];
			foreach (object obj in xmlNode)
			{
				XmlNode xmlNode4 = (XmlNode)obj;
				string innerText = xmlNode4["Name"].InnerText;
				int num = Convert.ToInt32(xmlNode4["SpriteSheetCount"].InnerText);
				bool alwaysLoad = false;
				Vec2i[] array = new Vec2i[num];
				foreach (object obj2 in xmlNode4.ChildNodes)
				{
					XmlNode xmlNode5 = (XmlNode)obj2;
					if (xmlNode5.Name == "SpriteSheetSize")
					{
						int num2 = Convert.ToInt32(xmlNode5.Attributes["ID"].InnerText);
						int x = Convert.ToInt32(xmlNode5.Attributes["Width"].InnerText);
						int y = Convert.ToInt32(xmlNode5.Attributes["Height"].InnerText);
						array[num2 - 1] = new Vec2i(x, y);
					}
					else if (xmlNode5.Name == "AlwaysLoad")
					{
						alwaysLoad = true;
					}
				}
				SpriteCategory spriteCategory = new SpriteCategory(innerText, this, num, alwaysLoad)
				{
					SheetSizes = array
				};
				this.SpriteCategories[spriteCategory.Name] = spriteCategory;
			}
			foreach (object obj3 in xmlNode2)
			{
				XmlNode xmlNode6 = (XmlNode)obj3;
				string innerText2 = xmlNode6["Name"].InnerText;
				int width = Convert.ToInt32(xmlNode6["Width"].InnerText);
				int height = Convert.ToInt32(xmlNode6["Height"].InnerText);
				string innerText3 = xmlNode6["CategoryName"].InnerText;
				SpriteCategory category = this.SpriteCategories[innerText3];
				SpritePart spritePart = new SpritePart(innerText2, category, width, height)
				{
					SheetID = Convert.ToInt32(xmlNode6["SheetID"].InnerText),
					SheetX = Convert.ToInt32(xmlNode6["SheetX"].InnerText),
					SheetY = Convert.ToInt32(xmlNode6["SheetY"].InnerText)
				};
				this.SpritePartNames[spritePart.Name] = spritePart;
				spritePart.UpdateInitValues();
			}
			foreach (object obj4 in xmlNode3)
			{
				XmlNode xmlNode7 = (XmlNode)obj4;
				Sprite sprite = null;
				if (xmlNode7.Name == "GenericSprite")
				{
					string innerText4 = xmlNode7["Name"].InnerText;
					string innerText5 = xmlNode7["SpritePartName"].InnerText;
					SpritePart spritePart2 = this.SpritePartNames[innerText5];
					sprite = new SpriteGeneric(innerText4, spritePart2);
				}
				else if (xmlNode7.Name == "NineRegionSprite")
				{
					string innerText6 = xmlNode7["Name"].InnerText;
					string innerText7 = xmlNode7["SpritePartName"].InnerText;
					int leftWidth = Convert.ToInt32(xmlNode7["LeftWidth"].InnerText);
					int rightWidth = Convert.ToInt32(xmlNode7["RightWidth"].InnerText);
					int topHeight = Convert.ToInt32(xmlNode7["TopHeight"].InnerText);
					int bottomHeight = Convert.ToInt32(xmlNode7["BottomHeight"].InnerText);
					sprite = new SpriteNineRegion(innerText6, this.SpritePartNames[innerText7], leftWidth, rightWidth, topHeight, bottomHeight);
				}
				this.SpriteNames[sprite.Name] = sprite;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000082DC File Offset: 0x000064DC
		public void Load(ResourceDepot resourceDepot)
		{
			this.LoadFromDepot(resourceDepot);
		}
	}
}
