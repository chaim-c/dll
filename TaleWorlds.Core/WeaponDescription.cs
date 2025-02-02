using System;
using System.Collections;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x02000046 RID: 70
	public class WeaponDescription : MBObjectBase
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00013FBE File Offset: 0x000121BE
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00013FC6 File Offset: 0x000121C6
		public WeaponClass WeaponClass { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00013FCF File Offset: 0x000121CF
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00013FD7 File Offset: 0x000121D7
		public WeaponFlags WeaponFlags { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00013FE0 File Offset: 0x000121E0
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00013FE8 File Offset: 0x000121E8
		public string ItemUsageFeatures { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00013FF1 File Offset: 0x000121F1
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00013FF9 File Offset: 0x000121F9
		public bool RotatedInHand { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00014002 File Offset: 0x00012202
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0001400A File Offset: 0x0001220A
		public bool IsHiddenFromUI { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00014013 File Offset: 0x00012213
		public MBReadOnlyList<CraftingPiece> AvailablePieces
		{
			get
			{
				return this._availablePieces;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001401C File Offset: 0x0001221C
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			this.WeaponClass = ((node.Attributes["weapon_class"] != null) ? ((WeaponClass)Enum.Parse(typeof(WeaponClass), node.Attributes["weapon_class"].Value)) : WeaponClass.Undefined);
			this.ItemUsageFeatures = ((node.Attributes["item_usage_features"] != null) ? node.Attributes["item_usage_features"].Value : "");
			this.RotatedInHand = XmlHelper.ReadBool(node, "rotated_in_hand");
			this.UseCenterOfMassAsHandBase = XmlHelper.ReadBool(node, "use_center_of_mass_as_hand_base");
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "WeaponFlags")
				{
					using (IEnumerator enumerator2 = xmlNode.ChildNodes.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlNode xmlNode2 = (XmlNode)obj2;
							this.WeaponFlags |= (WeaponFlags)Enum.Parse(typeof(WeaponFlags), xmlNode2.Attributes["value"].Value);
						}
						continue;
					}
				}
				if (xmlNode.Name == "AvailablePieces")
				{
					this._availablePieces = new MBList<CraftingPiece>();
					foreach (object obj3 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						if (xmlNode3.NodeType == XmlNodeType.Element)
						{
							string value = xmlNode3.Attributes["id"].Value;
							CraftingPiece @object = MBObjectManager.Instance.GetObject<CraftingPiece>(value);
							if (@object != null)
							{
								this._availablePieces.Add(@object);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000295 RID: 661
		public bool UseCenterOfMassAsHandBase;

		// Token: 0x04000297 RID: 663
		private MBList<CraftingPiece> _availablePieces;
	}
}
