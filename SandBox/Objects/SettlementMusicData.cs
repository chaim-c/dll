using System;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace SandBox.Objects
{
	// Token: 0x02000038 RID: 56
	public class SettlementMusicData : MBObjectBase
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000CBDA File Offset: 0x0000ADDA
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000CBE2 File Offset: 0x0000ADE2
		public string MusicPath { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000CBF3 File Offset: 0x0000ADF3
		public CultureObject Culture { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public MBReadOnlyList<InstrumentData> Instruments
		{
			get
			{
				return this._instruments;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000CC04 File Offset: 0x0000AE04
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		public string LocationId { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000CC15 File Offset: 0x0000AE15
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000CC1D File Offset: 0x0000AE1D
		public int Tempo { get; private set; }

		// Token: 0x060001F2 RID: 498 RVA: 0x0000CC28 File Offset: 0x0000AE28
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			this.MusicPath = Convert.ToString(node.Attributes["event_id"].Value);
			this.Culture = Game.Current.ObjectManager.ReadObjectReferenceFromXml<CultureObject>("culture", node);
			this.LocationId = Convert.ToString(node.Attributes["location"].Value);
			this.Tempo = Convert.ToInt32(node.Attributes["tempo"].Value);
			this._instruments = new MBList<InstrumentData>();
			if (node.HasChildNodes)
			{
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name == "Instruments")
					{
						foreach (object obj2 in xmlNode.ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.Name == "Instrument")
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								if (((attributes != null) ? attributes["id"] : null) != null)
								{
									string objectName = Convert.ToString(xmlNode2.Attributes["id"].Value);
									InstrumentData @object = MBObjectManager.Instance.GetObject<InstrumentData>(objectName);
									if (@object != null)
									{
										this._instruments.Add(@object);
									}
								}
								else
								{
									Debug.FailedAssert("Couldn't find required attributes of instrument xml node in Track", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Objects\\SettlementMusicData.cs", "Deserialize", 57);
								}
							}
						}
					}
				}
			}
			this._instruments.Capacity = this._instruments.Count;
		}

		// Token: 0x040000B6 RID: 182
		private MBList<InstrumentData> _instruments;
	}
}
