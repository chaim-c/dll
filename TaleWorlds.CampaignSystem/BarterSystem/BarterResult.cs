﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.BarterSystem
{
	// Token: 0x02000411 RID: 1041
	public class BarterResult
	{
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x00139898 File Offset: 0x00137A98
		public List<Barterable> OfferedBarters
		{
			get
			{
				return this._offeredBarterables;
			}
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x001398A0 File Offset: 0x00137AA0
		public BarterResult(Hero offererHero, Hero otherHero, List<Barterable> offeredBarters, bool isAccepted)
		{
			this.OffererHero = offererHero;
			this.OtherHero = otherHero;
			this.IsAccepted = isAccepted;
			this._offeredBarterables = new List<Barterable>(offeredBarters);
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x001398CA File Offset: 0x00137ACA
		internal static void AutoGeneratedStaticCollectObjectsBarterResult(object o, List<object> collectedObjects)
		{
			((BarterResult)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x001398D8 File Offset: 0x00137AD8
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this.OffererHero);
			collectedObjects.Add(this.OtherHero);
			collectedObjects.Add(this._offeredBarterables);
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x001398FE File Offset: 0x00137AFE
		internal static object AutoGeneratedGetMemberValueOffererHero(object o)
		{
			return ((BarterResult)o).OffererHero;
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x0013990B File Offset: 0x00137B0B
		internal static object AutoGeneratedGetMemberValueOtherHero(object o)
		{
			return ((BarterResult)o).OtherHero;
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x00139918 File Offset: 0x00137B18
		internal static object AutoGeneratedGetMemberValueIsAccepted(object o)
		{
			return ((BarterResult)o).IsAccepted;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0013992A File Offset: 0x00137B2A
		internal static object AutoGeneratedGetMemberValue_offeredBarterables(object o)
		{
			return ((BarterResult)o)._offeredBarterables;
		}

		// Token: 0x04001284 RID: 4740
		[SaveableField(0)]
		public readonly Hero OffererHero;

		// Token: 0x04001285 RID: 4741
		[SaveableField(1)]
		public readonly Hero OtherHero;

		// Token: 0x04001286 RID: 4742
		[SaveableField(2)]
		private readonly List<Barterable> _offeredBarterables;

		// Token: 0x04001287 RID: 4743
		[SaveableField(3)]
		public readonly bool IsAccepted;
	}
}
