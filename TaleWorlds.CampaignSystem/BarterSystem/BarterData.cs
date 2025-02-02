﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x02000408 RID: 1032
	public class BarterData
	{
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x00138EE5 File Offset: 0x001370E5
		public IFaction OffererMapFaction
		{
			get
			{
				Hero offererHero = this.OffererHero;
				return ((offererHero != null) ? offererHero.MapFaction : null) ?? this.OffererParty.MapFaction;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x00138F08 File Offset: 0x00137108
		public IFaction OtherMapFaction
		{
			get
			{
				Hero otherHero = this.OtherHero;
				return ((otherHero != null) ? otherHero.MapFaction : null) ?? this.OtherParty.MapFaction;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x00138F2B File Offset: 0x0013712B
		public bool IsAiBarter { get; }

		// Token: 0x06003F1A RID: 16154 RVA: 0x00138F34 File Offset: 0x00137134
		public BarterData(Hero offerer, Hero other, PartyBase offererParty, PartyBase otherParty, BarterManager.BarterContextInitializer contextInitializer = null, int persuasionCostReduction = 0, bool isAiBarter = false)
		{
			this.OffererParty = offererParty;
			this.OtherParty = otherParty;
			this.OffererHero = offerer;
			this.OtherHero = other;
			this.ContextInitializer = contextInitializer;
			this.PersuasionCostReduction = persuasionCostReduction;
			this._barterables = new List<Barterable>(16);
			this._barterGroups = Campaign.Current.Models.DiplomacyModel.GetBarterGroups().ToList<BarterGroup>();
			this.IsAiBarter = isAiBarter;
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x00138FA8 File Offset: 0x001371A8
		public void AddBarterable<T>(Barterable barterable, bool isContextDependent = false)
		{
			foreach (BarterGroup barterGroup in this._barterGroups)
			{
				if (barterGroup is T)
				{
					barterable.Initialize(barterGroup, isContextDependent);
					this._barterables.Add(barterable);
					break;
				}
			}
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x00139014 File Offset: 0x00137214
		public void AddBarterGroup(BarterGroup barterGroup)
		{
			this._barterGroups.Add(barterGroup);
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x00139022 File Offset: 0x00137222
		public List<BarterGroup> GetBarterGroups()
		{
			return this._barterGroups;
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x0013902A File Offset: 0x0013722A
		public List<Barterable> GetBarterables()
		{
			return this._barterables;
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x00139034 File Offset: 0x00137234
		public BarterGroup GetBarterGroup<T>()
		{
			IEnumerable<T> source = this._barterGroups.OfType<T>();
			if (source.IsEmpty<T>())
			{
				return null;
			}
			return source.First<T>() as BarterGroup;
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x00139067 File Offset: 0x00137267
		public List<Barterable> GetOfferedBarterables()
		{
			return (from barterable in this.GetBarterables()
			where barterable.IsOffered
			select barterable).ToList<Barterable>();
		}

		// Token: 0x04001276 RID: 4726
		public readonly Hero OffererHero;

		// Token: 0x04001277 RID: 4727
		public readonly Hero OtherHero;

		// Token: 0x04001278 RID: 4728
		public readonly PartyBase OffererParty;

		// Token: 0x04001279 RID: 4729
		public readonly PartyBase OtherParty;

		// Token: 0x0400127A RID: 4730
		private List<Barterable> _barterables;

		// Token: 0x0400127B RID: 4731
		private List<BarterGroup> _barterGroups;

		// Token: 0x0400127C RID: 4732
		public readonly BarterManager.BarterContextInitializer ContextInitializer;

		// Token: 0x0400127D RID: 4733
		public readonly int PersuasionCostReduction;
	}
}
