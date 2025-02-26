﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Siege
{
	// Token: 0x02000281 RID: 641
	public class SiegeEventManager
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x000927D8 File Offset: 0x000909D8
		internal static void AutoGeneratedStaticCollectObjectsSiegeEventManager(object o, List<object> collectedObjects)
		{
			((SiegeEventManager)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000927E6 File Offset: 0x000909E6
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this._siegeEvents);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000927F4 File Offset: 0x000909F4
		internal static object AutoGeneratedGetMemberValue_siegeEvents(object o)
		{
			return ((SiegeEventManager)o)._siegeEvents;
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00092801 File Offset: 0x00090A01
		public MBReadOnlyList<SiegeEvent> SiegeEvents
		{
			get
			{
				return this._siegeEvents;
			}
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00092809 File Offset: 0x00090A09
		public SiegeEventManager()
		{
			this._siegeEvents = new MBList<SiegeEvent>();
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0009281C File Offset: 0x00090A1C
		public SiegeEvent StartSiegeEvent(Settlement settlement, MobileParty besiegerParty)
		{
			SiegeEvent siegeEvent = new SiegeEvent(settlement, besiegerParty);
			this._siegeEvents.Add(siegeEvent);
			settlement.Party.SetVisualAsDirty();
			return siegeEvent;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0009284C File Offset: 0x00090A4C
		public void Tick(float dt)
		{
			for (int i = 0; i < this._siegeEvents.Count; i++)
			{
				if (this._siegeEvents[i].ReadyToBeRemoved)
				{
					this._siegeEvents[i] = this._siegeEvents[this._siegeEvents.Count - 1];
					this._siegeEvents.RemoveAt(this._siegeEvents.Count - 1);
					i--;
				}
				else
				{
					this._siegeEvents[i].Tick(dt);
				}
			}
		}

		// Token: 0x04000A90 RID: 2704
		[SaveableField(1)]
		private MBList<SiegeEvent> _siegeEvents;
	}
}
