﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x02000097 RID: 151
	public class MBEquipmentRoster : MBObjectBase
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001B76E File Offset: 0x0001996E
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0001B776 File Offset: 0x00019976
		public EquipmentFlags EquipmentFlags { get; private set; }

		// Token: 0x06000810 RID: 2064 RVA: 0x0001B77F File Offset: 0x0001997F
		public bool HasEquipmentFlags(EquipmentFlags flags)
		{
			return (this.EquipmentFlags & flags) == flags;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001B78C File Offset: 0x0001998C
		public bool IsEquipmentTemplate()
		{
			return this.EquipmentFlags > EquipmentFlags.None;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001B797 File Offset: 0x00019997
		public MBReadOnlyList<Equipment> AllEquipments
		{
			get
			{
				if (this._equipments.IsEmpty<Equipment>())
				{
					return new MBList<Equipment>(1)
					{
						MBEquipmentRoster.EmptyEquipment
					};
				}
				return this._equipments;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001B7BE File Offset: 0x000199BE
		public Equipment DefaultEquipment
		{
			get
			{
				if (this._equipments.IsEmpty<Equipment>())
				{
					return MBEquipmentRoster.EmptyEquipment;
				}
				return this._equipments.FirstOrDefault<Equipment>();
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001B7DE File Offset: 0x000199DE
		public MBEquipmentRoster()
		{
			this._equipments = new MBList<Equipment>();
			this.EquipmentFlags = EquipmentFlags.None;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001B7F8 File Offset: 0x000199F8
		public void Init(MBObjectManager objectManager, XmlNode node)
		{
			if (node.Name == "EquipmentRoster")
			{
				this.InitEquipment(objectManager, node);
				return;
			}
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\MBEquipmentRoster.cs", "Init", 96);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001B82C File Offset: 0x00019A2C
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			if (node.Attributes["culture"] != null)
			{
				this.EquipmentCulture = MBObjectManager.Instance.ReadObjectReferenceFromXml<BasicCultureObject>("culture", node);
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "EquipmentSet")
				{
					this.InitEquipment(objectManager, xmlNode);
				}
				if (xmlNode.Name == "Flags")
				{
					foreach (object obj2 in xmlNode.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj2;
						EquipmentFlags equipmentFlags = (EquipmentFlags)Enum.Parse(typeof(EquipmentFlags), xmlAttribute.Name);
						if (bool.Parse(xmlAttribute.InnerText))
						{
							this.EquipmentFlags |= equipmentFlags;
						}
					}
				}
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001B960 File Offset: 0x00019B60
		private void InitEquipment(MBObjectManager objectManager, XmlNode node)
		{
			base.Initialize();
			Equipment equipment = new Equipment(node.Attributes["civilian"] != null && bool.Parse(node.Attributes["civilian"].Value));
			equipment.Deserialize(objectManager, node);
			this._equipments.Add(equipment);
			base.AfterInitialized();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		public void AddEquipmentRoster(MBEquipmentRoster equipmentRoster, bool isCivilian)
		{
			foreach (Equipment equipment in equipmentRoster._equipments.ToList<Equipment>())
			{
				if (equipment.IsCivilian == isCivilian)
				{
					this._equipments.Add(equipment);
				}
			}
			this.EquipmentFlags = equipmentRoster.EquipmentFlags;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001BA38 File Offset: 0x00019C38
		public void AddOverridenEquipments(MBObjectManager objectManager, List<XmlNode> overridenEquipmentSlots)
		{
			List<Equipment> list = this._equipments.ToList<Equipment>();
			this._equipments.Clear();
			foreach (Equipment equipment in list)
			{
				this._equipments.Add(equipment.Clone(false));
			}
			foreach (XmlNode node in overridenEquipmentSlots)
			{
				foreach (Equipment equipment2 in this._equipments)
				{
					equipment2.DeserializeNode(objectManager, node);
				}
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001BB20 File Offset: 0x00019D20
		public void OrderEquipments()
		{
			this._equipments = new MBList<Equipment>(from eq in this._equipments
			orderby !eq.IsCivilian descending
			select eq);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001BB57 File Offset: 0x00019D57
		public void InitializeDefaultEquipment(string equipmentName)
		{
			if (this._equipments[0] == null)
			{
				this._equipments[0] = new Equipment(false);
			}
			this._equipments[0].FillFrom(Game.Current.GetDefaultEquipmentWithName(equipmentName), true);
		}

		// Token: 0x040004A9 RID: 1193
		public static readonly Equipment EmptyEquipment = new Equipment(true);

		// Token: 0x040004AA RID: 1194
		private MBList<Equipment> _equipments;

		// Token: 0x040004AB RID: 1195
		public BasicCultureObject EquipmentCulture;
	}
}
