﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Settlements.Buildings
{
	// Token: 0x02000370 RID: 880
	public sealed class BuildingType : MBObjectBase
	{
		// Token: 0x060033C4 RID: 13252 RVA: 0x000D6C90 File Offset: 0x000D4E90
		internal static void AutoGeneratedStaticCollectObjectsBuildingType(object o, List<object> collectedObjects)
		{
			((BuildingType)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000D6C9E File Offset: 0x000D4E9E
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000D6CA7 File Offset: 0x000D4EA7
		public static MBReadOnlyList<BuildingType> All
		{
			get
			{
				return Campaign.Current.AllBuildingTypes;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000D6CB3 File Offset: 0x000D4EB3
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000D6CBB File Offset: 0x000D4EBB
		public TextObject Name { get; private set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000D6CC4 File Offset: 0x000D4EC4
		// (set) Token: 0x060033CA RID: 13258 RVA: 0x000D6CCC File Offset: 0x000D4ECC
		public TextObject Explanation { get; private set; }

		// Token: 0x060033CB RID: 13259 RVA: 0x000D6CD5 File Offset: 0x000D4ED5
		public BuildingType(string stringId) : base(stringId)
		{
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000D6CEC File Offset: 0x000D4EEC
		public void Initialize(TextObject name, TextObject explanation, int[] productionCosts, BuildingLocation buildingLocation, Tuple<BuildingEffectEnum, float, float, float>[] effects, int startLevel = 0)
		{
			base.Initialize();
			this.Name = name;
			this.Explanation = explanation;
			this._productionCosts = productionCosts;
			this.IsDefaultProject = (buildingLocation == BuildingLocation.Daily);
			this._effects = (from x in effects
			select new BuildingType.EffectInfo(x.Item1, x.Item2, x.Item3, x.Item4)).ToArray<BuildingType.EffectInfo>();
			this.StartLevel = startLevel;
			this.BuildingLocation = buildingLocation;
			base.AfterInitialized();
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x000D6D66 File Offset: 0x000D4F66
		public override string ToString()
		{
			return this.Name.ToString();
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000D6D73 File Offset: 0x000D4F73
		public int GetProductionCost(int level)
		{
			if (level < this.StartLevel || level >= 3)
			{
				return 0;
			}
			return this._productionCosts[level];
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000D6D8C File Offset: 0x000D4F8C
		public float GetBaseBuildingEffectAmount(BuildingEffectEnum effect, int level)
		{
			for (int i = 0; i < this._effects.Length; i++)
			{
				if (this._effects[i].BuildingEffect == effect)
				{
					return this._effects[i].GetEffectValue(level);
				}
			}
			return 0f;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000D6DD8 File Offset: 0x000D4FD8
		public TextObject GetExplanationAtLevel(int level)
		{
			if (level == 0 || level > 3)
			{
				return TextObject.Empty;
			}
			TextObject textObject = TextObject.Empty;
			if (this._effects.Length == 1)
			{
				textObject = GameTexts.FindText("str_building_effect_explanation", Enum.GetName(typeof(BuildingEffectEnum), this._effects[0].BuildingEffect));
				textObject.SetTextVariable("BONUS_AMOUNT", this._effects[0].GetEffectValue(level));
			}
			else if (this._effects.Length >= 2)
			{
				textObject = GameTexts.FindText("str_string_newline_string", null);
				TextObject textObject2 = GameTexts.FindText("str_building_effect_explanation", Enum.GetName(typeof(BuildingEffectEnum), this._effects[0].BuildingEffect));
				textObject2.SetTextVariable("BONUS_AMOUNT", this._effects[0].GetEffectValue(level));
				TextObject textObject3 = GameTexts.FindText("str_building_effect_explanation", Enum.GetName(typeof(BuildingEffectEnum), this._effects[1].BuildingEffect));
				textObject3.SetTextVariable("BONUS_AMOUNT", this._effects[1].GetEffectValue(level));
				textObject.SetTextVariable("STR1", textObject2);
				textObject.SetTextVariable("STR2", textObject3);
				textObject.SetTextVariable("newline", "\n");
				for (int i = 2; i < this._effects.Length; i++)
				{
					TextObject textObject4 = GameTexts.FindText("str_string_newline_string", null);
					textObject4.SetTextVariable("STR1", textObject);
					TextObject textObject5 = GameTexts.FindText("str_building_effect_explanation", Enum.GetName(typeof(BuildingEffectEnum), this._effects[i].BuildingEffect));
					textObject5.SetTextVariable("BONUS_AMOUNT", this._effects[i].GetEffectValue(level));
					textObject4.SetTextVariable("STR2", textObject5);
					textObject4.SetTextVariable("newline", "\n");
					textObject = textObject4;
				}
			}
			return textObject;
		}

		// Token: 0x040010B4 RID: 4276
		public const int MaxLevel = 3;

		// Token: 0x040010B5 RID: 4277
		public bool IsDefaultProject;

		// Token: 0x040010B6 RID: 4278
		private int[] _productionCosts = new int[3];

		// Token: 0x040010B7 RID: 4279
		public int StartLevel;

		// Token: 0x040010B8 RID: 4280
		public BuildingLocation BuildingLocation;

		// Token: 0x040010BB RID: 4283
		private BuildingType.EffectInfo[] _effects;

		// Token: 0x020006B5 RID: 1717
		public struct EffectInfo
		{
			// Token: 0x170013A6 RID: 5030
			// (get) Token: 0x0600569E RID: 22174 RVA: 0x0017F7B4 File Offset: 0x0017D9B4
			public BuildingEffectEnum BuildingEffect { get; }

			// Token: 0x170013A7 RID: 5031
			// (get) Token: 0x0600569F RID: 22175 RVA: 0x0017F7BC File Offset: 0x0017D9BC
			public float Level1Effect { get; }

			// Token: 0x170013A8 RID: 5032
			// (get) Token: 0x060056A0 RID: 22176 RVA: 0x0017F7C4 File Offset: 0x0017D9C4
			public float Level2Effect { get; }

			// Token: 0x170013A9 RID: 5033
			// (get) Token: 0x060056A1 RID: 22177 RVA: 0x0017F7CC File Offset: 0x0017D9CC
			public float Level3Effect { get; }

			// Token: 0x060056A2 RID: 22178 RVA: 0x0017F7D4 File Offset: 0x0017D9D4
			public float GetEffectValue(int i)
			{
				if (i == 1)
				{
					return this.Level1Effect;
				}
				if (i != 2)
				{
					return this.Level3Effect;
				}
				return this.Level2Effect;
			}

			// Token: 0x060056A3 RID: 22179 RVA: 0x0017F7F2 File Offset: 0x0017D9F2
			public EffectInfo(BuildingEffectEnum effect, float[] effectValues)
			{
				this.BuildingEffect = effect;
				this.Level1Effect = effectValues[0];
				this.Level2Effect = effectValues[1];
				this.Level3Effect = effectValues[2];
			}

			// Token: 0x060056A4 RID: 22180 RVA: 0x0017F816 File Offset: 0x0017DA16
			public EffectInfo(BuildingEffectEnum effect, float effectValue1, float effectValue2, float effectValue3)
			{
				this.BuildingEffect = effect;
				this.Level1Effect = effectValue1;
				this.Level2Effect = effectValue2;
				this.Level3Effect = effectValue3;
			}
		}
	}
}
