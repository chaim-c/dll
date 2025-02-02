﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Settlements
{
	// Token: 0x02000355 RID: 853
	public class Alley : SettlementArea
	{
		// Token: 0x060030B9 RID: 12473 RVA: 0x000CE2A1 File Offset: 0x000CC4A1
		internal static void AutoGeneratedStaticCollectObjectsAlley(object o, List<object> collectedObjects)
		{
			((Alley)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000CE2AF File Offset: 0x000CC4AF
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this._owner);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000CE2C4 File Offset: 0x000CC4C4
		internal static object AutoGeneratedGetMemberValue_owner(object o)
		{
			return ((Alley)o)._owner;
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000CE2D1 File Offset: 0x000CC4D1
		public override Settlement Settlement
		{
			get
			{
				return this._settlement;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000CE2D9 File Offset: 0x000CC4D9
		public override TextObject Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000CE2E1 File Offset: 0x000CC4E1
		public override Hero Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000CE2E9 File Offset: 0x000CC4E9
		public override string Tag
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000CE2F4 File Offset: 0x000CC4F4
		public void SetOwner(Hero newOwner)
		{
			if (this._owner != null)
			{
				this._owner.OwnedAlleys.Remove(this);
			}
			Hero owner = this._owner;
			this._owner = newOwner;
			if (this._owner != null)
			{
				this._owner.OwnedAlleys.Add(this);
				this.State = ((this._owner == Hero.MainHero) ? Alley.AreaState.OccupiedByPlayer : Alley.AreaState.OccupiedByGangLeader);
			}
			else
			{
				this.State = Alley.AreaState.Empty;
			}
			CampaignEventDispatcher.Instance.OnAlleyOwnerChanged(this, newOwner, owner);
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000CE36F File Offset: 0x000CC56F
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x000CE377 File Offset: 0x000CC577
		public Alley.AreaState State { get; private set; }

		// Token: 0x060030C3 RID: 12483 RVA: 0x000CE380 File Offset: 0x000CC580
		public Alley(Settlement settlement, string tag, TextObject name)
		{
			this.Initialize(settlement, tag, name);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000CE391 File Offset: 0x000CC591
		public void Initialize(Settlement settlement, string tag, TextObject name)
		{
			this._name = name;
			this._settlement = settlement;
			this._tag = tag;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000CE3A8 File Offset: 0x000CC5A8
		internal void AfterLoad()
		{
			if (this._owner != null)
			{
				this.State = ((this._owner == Hero.MainHero) ? Alley.AreaState.OccupiedByPlayer : Alley.AreaState.OccupiedByGangLeader);
				this._owner.OwnedAlleys.Add(this);
				if (MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.2.0", 45697) && !this._owner.IsAlive)
				{
					this.SetOwner(null);
					this.State = Alley.AreaState.Empty;
					return;
				}
			}
			else
			{
				this.State = Alley.AreaState.Empty;
			}
		}

		// Token: 0x04000FE3 RID: 4067
		private Settlement _settlement;

		// Token: 0x04000FE4 RID: 4068
		[CachedData]
		private TextObject _name;

		// Token: 0x04000FE5 RID: 4069
		[SaveableField(10)]
		private Hero _owner;

		// Token: 0x04000FE6 RID: 4070
		private string _tag;

		// Token: 0x0200069A RID: 1690
		public enum AreaState
		{
			// Token: 0x04001B81 RID: 7041
			Empty,
			// Token: 0x04001B82 RID: 7042
			OccupiedByGangLeader,
			// Token: 0x04001B83 RID: 7043
			OccupiedByPlayer
		}
	}
}
