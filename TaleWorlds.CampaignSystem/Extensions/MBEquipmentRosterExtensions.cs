using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Extensions
{
	// Token: 0x02000153 RID: 339
	public static class MBEquipmentRosterExtensions
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0007C66E File Offset: 0x0007A86E
		public static MBReadOnlyList<MBEquipmentRoster> All
		{
			get
			{
				return Campaign.Current.AllEquipmentRosters;
			}
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0007C67A File Offset: 0x0007A87A
		public static IEnumerable<Equipment> GetCivilianEquipments(this MBEquipmentRoster instance)
		{
			return from x in instance.AllEquipments
			where x.IsCivilian
			select x;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0007C6A6 File Offset: 0x0007A8A6
		public static IEnumerable<Equipment> GetBattleEquipments(this MBEquipmentRoster instance)
		{
			return from x in instance.AllEquipments
			where !x.IsCivilian
			select x;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0007C6D2 File Offset: 0x0007A8D2
		public static Equipment GetRandomCivilianEquipment(this MBEquipmentRoster instance)
		{
			return instance.AllEquipments.GetRandomElementWithPredicate((Equipment x) => x.IsCivilian);
		}
	}
}
