﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Settlements
{
	// Token: 0x02000360 RID: 864
	public sealed class VillageType : MBObjectBase
	{
		// Token: 0x06003277 RID: 12919 RVA: 0x000D2DAF File Offset: 0x000D0FAF
		internal static void AutoGeneratedStaticCollectObjectsVillageType(object o, List<object> collectedObjects)
		{
			((VillageType)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000D2DBD File Offset: 0x000D0FBD
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000D2DC6 File Offset: 0x000D0FC6
		public static MBReadOnlyList<VillageType> All
		{
			get
			{
				return Campaign.Current.AllVillageTypes;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000D2DD2 File Offset: 0x000D0FD2
		public MBReadOnlyList<ValueTuple<ItemObject, float>> Productions
		{
			get
			{
				return this._productions;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000D2DDC File Offset: 0x000D0FDC
		public ItemObject PrimaryProduction
		{
			get
			{
				ValueTuple<ItemObject, float> valueTuple = this._productions[0];
				float num = 0f;
				foreach (ValueTuple<ItemObject, float> valueTuple2 in this._productions)
				{
					if (valueTuple2.Item2 * (float)valueTuple2.Item1.Value > num)
					{
						valueTuple = valueTuple2;
						num = valueTuple2.Item2 * (float)valueTuple2.Item1.Value;
					}
				}
				return valueTuple.Item1;
			}
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000D2E70 File Offset: 0x000D1070
		public VillageType(string stringId) : base(stringId)
		{
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000D2E79 File Offset: 0x000D1079
		public VillageType Initialize(TextObject shortName, string meshName, string meshNameUnderConstruction, string meshNameBurned, ValueTuple<ItemObject, float>[] productions)
		{
			this.ShortName = shortName;
			this.MeshName = meshName;
			this.MeshNameUnderConstruction = meshNameUnderConstruction;
			this.MeshNameBurned = meshNameBurned;
			this._productions = productions.ToMBList<ValueTuple<ItemObject, float>>();
			base.AfterInitialized();
			return this;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000D2EAC File Offset: 0x000D10AC
		public override string ToString()
		{
			return this.ShortName.ToString();
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000D2EB9 File Offset: 0x000D10B9
		public void AddProductions(IEnumerable<ValueTuple<ItemObject, float>> productions)
		{
			this._productions = productions.Concat(this._productions).ToMBList<ValueTuple<ItemObject, float>>();
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000D2ED4 File Offset: 0x000D10D4
		public float GetProductionPerDay(ItemObject item)
		{
			foreach (ValueTuple<ItemObject, float> valueTuple in this._productions)
			{
				if (valueTuple.Item1 == item)
				{
					return valueTuple.Item2;
				}
			}
			return 0f;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000D2F3C File Offset: 0x000D113C
		public float GetProductionPerDay(ItemCategory itemCategory)
		{
			float num = 0f;
			foreach (ValueTuple<ItemObject, float> valueTuple in this._productions)
			{
				if (valueTuple.Item1 != null && valueTuple.Item1.ItemCategory == itemCategory)
				{
					num += valueTuple.Item2;
				}
			}
			return num;
		}

		// Token: 0x0400104B RID: 4171
		private MBList<ValueTuple<ItemObject, float>> _productions;

		// Token: 0x0400104C RID: 4172
		public TextObject ShortName;

		// Token: 0x0400104D RID: 4173
		public string MeshName;

		// Token: 0x0400104E RID: 4174
		public string MeshNameUnderConstruction;

		// Token: 0x0400104F RID: 4175
		public string MeshNameBurned;
	}
}
