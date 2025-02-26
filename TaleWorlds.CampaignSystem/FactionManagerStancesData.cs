﻿using System;
using System.Collections.Generic;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200006C RID: 108
	internal class FactionManagerStancesData
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x000454D8 File Offset: 0x000436D8
		internal static void AutoGeneratedStaticCollectObjectsFactionManagerStancesData(object o, List<object> collectedObjects)
		{
			((FactionManagerStancesData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000454E6 File Offset: 0x000436E6
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this._stances);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000454F4 File Offset: 0x000436F4
		internal static object AutoGeneratedGetMemberValue_stances(object o)
		{
			return ((FactionManagerStancesData)o)._stances;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00045501 File Offset: 0x00043701
		public Dictionary<ValueTuple<IFaction, IFaction>, StanceLink>.ValueCollection GetStanceLinks()
		{
			return this._stances.Values;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00045510 File Offset: 0x00043710
		internal StanceLink GetStance(IFaction faction1, IFaction faction2)
		{
			ValueTuple<IFaction, IFaction> key = this.GetKey(faction1, faction2);
			StanceLink result;
			this._stances.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00045538 File Offset: 0x00043738
		public void AddStance(StanceLink stance)
		{
			ValueTuple<IFaction, IFaction> key = this.GetKey(stance);
			if (this._stances.ContainsKey(key))
			{
				this._stances[key] = stance;
				return;
			}
			this._stances.Add(key, stance);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00045578 File Offset: 0x00043778
		public void RemoveStance(StanceLink stance)
		{
			ValueTuple<IFaction, IFaction> key = this.GetKey(stance);
			if (this._stances.ContainsKey(key))
			{
				this._stances.Remove(key);
			}
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000455A8 File Offset: 0x000437A8
		private ValueTuple<IFaction, IFaction> GetKey(IFaction faction1, IFaction faction2)
		{
			if (faction1.Id < faction2.Id)
			{
				return new ValueTuple<IFaction, IFaction>(faction1, faction2);
			}
			return new ValueTuple<IFaction, IFaction>(faction2, faction1);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000455CC File Offset: 0x000437CC
		private ValueTuple<IFaction, IFaction> GetKey(StanceLink stance)
		{
			return this.GetKey(stance.Faction1, stance.Faction2);
		}

		// Token: 0x04000427 RID: 1063
		[SaveableField(10)]
		private Dictionary<ValueTuple<IFaction, IFaction>, StanceLink> _stances = new Dictionary<ValueTuple<IFaction, IFaction>, StanceLink>();
	}
}
