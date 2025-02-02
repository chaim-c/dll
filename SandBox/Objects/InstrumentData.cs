using System;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace SandBox.Objects
{
	// Token: 0x02000036 RID: 54
	public class InstrumentData : MBObjectBase
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000C538 File Offset: 0x0000A738
		public MBReadOnlyList<ValueTuple<HumanBone, string>> InstrumentEntities
		{
			get
			{
				return this._instrumentEntities;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000C540 File Offset: 0x0000A740
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000C548 File Offset: 0x0000A748
		public string SittingAction { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000C551 File Offset: 0x0000A751
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000C559 File Offset: 0x0000A759
		public string StandingAction { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000C562 File Offset: 0x0000A762
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000C56A File Offset: 0x0000A76A
		public string Tag { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000C573 File Offset: 0x0000A773
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000C57B File Offset: 0x0000A77B
		public bool IsDataWithoutInstrument { get; private set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C584 File Offset: 0x0000A784
		public InstrumentData()
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C58C File Offset: 0x0000A78C
		public InstrumentData(string stringId) : base(stringId)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C595 File Offset: 0x0000A795
		public void InitializeInstrumentData(string sittingAction, string standingAction, bool isDataWithoutInstrument)
		{
			this.SittingAction = sittingAction;
			this.StandingAction = standingAction;
			this._instrumentEntities = new MBList<ValueTuple<HumanBone, string>>(0);
			this.IsDataWithoutInstrument = isDataWithoutInstrument;
			this.Tag = string.Empty;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			this.SittingAction = Convert.ToString(node.Attributes["sittingAction"].Value);
			this.StandingAction = Convert.ToString(node.Attributes["standingAction"].Value);
			XmlAttribute xmlAttribute = node.Attributes["tag"];
			this.Tag = Convert.ToString((xmlAttribute != null) ? xmlAttribute.Value : null);
			this._instrumentEntities = new MBList<ValueTuple<HumanBone, string>>();
			if (node.HasChildNodes)
			{
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name == "Entities")
					{
						foreach (object obj2 in xmlNode.ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (xmlNode2.Name == "Entity")
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								if (((attributes != null) ? attributes["name"] : null) != null && xmlNode2.Attributes["bone"] != null)
								{
									string item = Convert.ToString(xmlNode2.Attributes["name"].Value);
									HumanBone item2;
									if (Enum.TryParse<HumanBone>(xmlNode2.Attributes["bone"].Value, out item2))
									{
										this._instrumentEntities.Add(new ValueTuple<HumanBone, string>(item2, item));
									}
									else
									{
										Debug.FailedAssert("Couldn't parse bone xml node for instrument.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Objects\\InstrumentData.cs", "Deserialize", 62);
									}
								}
								else
								{
									Debug.FailedAssert("Couldn't find required attributes of entity xml node in Instrument", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Objects\\InstrumentData.cs", "Deserialize", 67);
								}
							}
						}
					}
				}
			}
			this._instrumentEntities.Capacity = this._instrumentEntities.Count;
		}

		// Token: 0x040000AA RID: 170
		private MBList<ValueTuple<HumanBone, string>> _instrumentEntities;
	}
}
