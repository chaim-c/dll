﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.BarterSystem.Barterables
{
	// Token: 0x02000412 RID: 1042
	public abstract class Barterable
	{
		// Token: 0x06003F56 RID: 16214 RVA: 0x00139937 File Offset: 0x00137B37
		protected Barterable(Hero originalOwner, PartyBase originalParty)
		{
			this.OriginalOwner = originalOwner;
			this.OriginalParty = originalParty;
			this.CurrentAmount = 1;
			this._linkedBarterables = new MBList<Barterable>();
			this.Side = 0;
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06003F57 RID: 16215
		public abstract string StringID { get; }

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06003F58 RID: 16216 RVA: 0x00139966 File Offset: 0x00137B66
		// (set) Token: 0x06003F59 RID: 16217 RVA: 0x0013996E File Offset: 0x00137B6E
		public Hero OriginalOwner { get; private set; }

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06003F5A RID: 16218 RVA: 0x00139977 File Offset: 0x00137B77
		// (set) Token: 0x06003F5B RID: 16219 RVA: 0x0013997F File Offset: 0x00137B7F
		public PartyBase OriginalParty { get; private set; }

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003F5C RID: 16220
		public abstract TextObject Name { get; }

		// Token: 0x06003F5D RID: 16221 RVA: 0x00139988 File Offset: 0x00137B88
		public int GetValueForFaction(IFaction faction)
		{
			return this.GetUnitValueForFaction(faction) * this.CurrentAmount;
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x00139998 File Offset: 0x00137B98
		public virtual void CheckBarterLink(Barterable linkedBarterable)
		{
		}

		// Token: 0x06003F5F RID: 16223
		public abstract int GetUnitValueForFaction(IFaction faction);

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x0013999A File Offset: 0x00137B9A
		public virtual int MaxAmount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x0013999D File Offset: 0x00137B9D
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x001399A5 File Offset: 0x00137BA5
		public int CurrentAmount
		{
			get
			{
				return this._currentAmout;
			}
			set
			{
				this._currentAmout = value;
				if (this._currentAmout > this.MaxAmount)
				{
					this._currentAmout = this.MaxAmount;
				}
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003F63 RID: 16227 RVA: 0x001399C8 File Offset: 0x00137BC8
		// (set) Token: 0x06003F64 RID: 16228 RVA: 0x001399D0 File Offset: 0x00137BD0
		public bool IsOffered { get; protected set; }

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x001399D9 File Offset: 0x00137BD9
		// (set) Token: 0x06003F66 RID: 16230 RVA: 0x001399E1 File Offset: 0x00137BE1
		public bool IsContextDependent { get; protected set; }

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x001399EA File Offset: 0x00137BEA
		// (set) Token: 0x06003F68 RID: 16232 RVA: 0x001399F2 File Offset: 0x00137BF2
		public BarterGroup Group { get; protected set; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003F69 RID: 16233 RVA: 0x001399FB File Offset: 0x00137BFB
		public MBReadOnlyList<Barterable> LinkedBarterables
		{
			get
			{
				return this._linkedBarterables;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x00139A03 File Offset: 0x00137C03
		public Barterable.BarterSide Side { get; }

		// Token: 0x06003F6B RID: 16235 RVA: 0x00139A0C File Offset: 0x00137C0C
		public void SetIsOffered(bool value)
		{
			if (this.IsOffered != value)
			{
				this.IsOffered = value;
				foreach (Barterable barterable in this._linkedBarterables)
				{
					barterable.IsOffered = value;
				}
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x00139A70 File Offset: 0x00137C70
		public void AddBarterLink(Barterable barterable)
		{
			this._linkedBarterables.Add(barterable);
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x00139A7E File Offset: 0x00137C7E
		public void Initialize(BarterGroup barterGroup, bool isContextDependent)
		{
			this.Group = barterGroup;
			this.IsContextDependent = isContextDependent;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x00139A8E File Offset: 0x00137C8E
		public virtual bool IsCompatible(Barterable barterable)
		{
			return true;
		}

		// Token: 0x06003F6F RID: 16239
		public abstract ImageIdentifier GetVisualIdentifier();

		// Token: 0x06003F70 RID: 16240 RVA: 0x00139A91 File Offset: 0x00137C91
		public virtual string GetEncyclopediaLink()
		{
			return "";
		}

		// Token: 0x06003F71 RID: 16241
		public abstract void Apply();

		// Token: 0x06003F72 RID: 16242 RVA: 0x00139A98 File Offset: 0x00137C98
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
		}

		// Token: 0x0400128A RID: 4746
		private int _currentAmout;

		// Token: 0x0400128E RID: 4750
		protected MBList<Barterable> _linkedBarterables;

		// Token: 0x02000763 RID: 1891
		public enum BarterSide
		{
			// Token: 0x04001EE8 RID: 7912
			Left,
			// Token: 0x04001EE9 RID: 7913
			Right
		}
	}
}
