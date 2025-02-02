﻿using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x02000037 RID: 55
	public sealed class SiegeEngineType : MBObjectBase
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		internal static void AutoGeneratedStaticCollectObjectsSiegeEngineType(object o, List<object> collectedObjects)
		{
			((SiegeEngineType)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000F9FF File Offset: 0x0000DBFF
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000FA07 File Offset: 0x0000DC07
		public int Difficulty { get; private set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000FA10 File Offset: 0x0000DC10
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public int BaseHitPoints { get; private set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000FA21 File Offset: 0x0000DC21
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000FA29 File Offset: 0x0000DC29
		public int ToolCost { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000FA32 File Offset: 0x0000DC32
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000FA3A File Offset: 0x0000DC3A
		public float HitChance { get; private set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000FA43 File Offset: 0x0000DC43
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000FA4B File Offset: 0x0000DC4B
		public bool IsAntiPersonnel { get; private set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000FA54 File Offset: 0x0000DC54
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public float AntiPersonnelHitChance { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000FA65 File Offset: 0x0000DC65
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000FA6D File Offset: 0x0000DC6D
		public bool IsConstructible { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000FA76 File Offset: 0x0000DC76
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000FA7E File Offset: 0x0000DC7E
		public bool IsRanged { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000FA87 File Offset: 0x0000DC87
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000FA8F File Offset: 0x0000DC8F
		public int Damage { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000FA98 File Offset: 0x0000DC98
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		public int ManDayCost { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000FAA9 File Offset: 0x0000DCA9
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000FAB1 File Offset: 0x0000DCB1
		public float CampaignRateOfFirePerDay { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000FABA File Offset: 0x0000DCBA
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000FAC2 File Offset: 0x0000DCC2
		public float MovementSpeed { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000FACB File Offset: 0x0000DCCB
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000FAD3 File Offset: 0x0000DCD3
		public float ProjectileSpeed { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000FADC File Offset: 0x0000DCDC
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
		public TextObject Name { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000FAED File Offset: 0x0000DCED
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000FAF5 File Offset: 0x0000DCF5
		public TextObject Description { get; private set; }

		// Token: 0x0600042F RID: 1071 RVA: 0x0000FAFE File Offset: 0x0000DCFE
		public override string ToString()
		{
			return this.Name.ToString();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000FB0C File Offset: 0x0000DD0C
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			this.Name = new TextObject(node.Attributes["name"].InnerText, null);
			this.Description = new TextObject(node.Attributes["description"].InnerText, null);
			XmlAttribute xmlAttribute = node.Attributes["max_hit_points"];
			if (xmlAttribute != null)
			{
				this.BaseHitPoints = Convert.ToInt32(xmlAttribute.Value);
			}
			else
			{
				this.BaseHitPoints = 1;
			}
			XmlAttribute xmlAttribute2 = node.Attributes["difficulty"];
			this.Difficulty = Convert.ToInt32((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
			this.ToolCost = Convert.ToInt32(node.Attributes["tool_cost"].Value);
			this.HitChance = (float)Convert.ToDouble(node.Attributes["hit_chance"].Value);
			this.IsAntiPersonnel = Convert.ToBoolean(node.Attributes["is_anti_personnel"].Value);
			this.AntiPersonnelHitChance = (this.IsAntiPersonnel ? ((float)Convert.ToDouble(node.Attributes["anti_personnel_hit_chance"].Value)) : 0f);
			this.IsConstructible = Convert.ToBoolean(node.Attributes["is_constructible"].Value);
			this.IsRanged = Convert.ToBoolean(node.Attributes["is_ranged"].Value);
			this.Damage = Convert.ToInt32(node.Attributes["damage"].Value);
			this.ManDayCost = Convert.ToInt32(node.Attributes["man_day_cost"].Value);
			this.CampaignRateOfFirePerDay = (float)Convert.ToDouble(node.Attributes["campaign_rate_of_fire_per_day"].Value);
			this.MovementSpeed = (float)Convert.ToDouble(node.Attributes["movement_speed"].Value);
			this.ProjectileSpeed = (float)Convert.ToDouble(node.Attributes["projectile_speed"].Value);
		}
	}
}
