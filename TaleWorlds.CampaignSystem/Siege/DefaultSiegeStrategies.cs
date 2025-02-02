using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Siege
{
	// Token: 0x02000284 RID: 644
	public class DefaultSiegeStrategies
	{
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x000938DA File Offset: 0x00091ADA
		private static DefaultSiegeStrategies Instance
		{
			get
			{
				return Campaign.Current.DefaultSiegeStrategies;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000938E6 File Offset: 0x00091AE6
		public static SiegeStrategy PreserveStrength
		{
			get
			{
				return DefaultSiegeStrategies.Instance._preserveStrength;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000938F2 File Offset: 0x00091AF2
		public static SiegeStrategy PrepareAgainstAssault
		{
			get
			{
				return DefaultSiegeStrategies.Instance._prepareAgainstAssault;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000938FE File Offset: 0x00091AFE
		public static SiegeStrategy CounterBombardment
		{
			get
			{
				return DefaultSiegeStrategies.Instance._counterBombardment;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x0009390A File Offset: 0x00091B0A
		public static SiegeStrategy PrepareAssault
		{
			get
			{
				return DefaultSiegeStrategies.Instance._prepareAssault;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x00093916 File Offset: 0x00091B16
		public static SiegeStrategy BreachWalls
		{
			get
			{
				return DefaultSiegeStrategies.Instance._breachWalls;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x00093922 File Offset: 0x00091B22
		public static SiegeStrategy WearOutDefenders
		{
			get
			{
				return DefaultSiegeStrategies.Instance._wearOutDefenders;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0009392E File Offset: 0x00091B2E
		public static SiegeStrategy Custom
		{
			get
			{
				return DefaultSiegeStrategies.Instance._custom;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0009393A File Offset: 0x00091B3A
		public static IEnumerable<SiegeStrategy> AllAttackerStrategies
		{
			get
			{
				yield return DefaultSiegeStrategies.PrepareAssault;
				yield return DefaultSiegeStrategies.BreachWalls;
				yield return DefaultSiegeStrategies.WearOutDefenders;
				yield return DefaultSiegeStrategies.PreserveStrength;
				yield return DefaultSiegeStrategies.Custom;
				yield break;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x00093943 File Offset: 0x00091B43
		public static IEnumerable<SiegeStrategy> AllDefenderStrategies
		{
			get
			{
				yield return DefaultSiegeStrategies.PrepareAgainstAssault;
				yield return DefaultSiegeStrategies.CounterBombardment;
				yield return DefaultSiegeStrategies.PreserveStrength;
				yield return DefaultSiegeStrategies.Custom;
				yield break;
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0009394C File Offset: 0x00091B4C
		public DefaultSiegeStrategies()
		{
			this.RegisterAll();
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0009395C File Offset: 0x00091B5C
		private void RegisterAll()
		{
			this._preserveStrength = this.Create("siege_strategy_preserve_strength");
			this._prepareAgainstAssault = this.Create("siege_strategy_prepare_against_assault");
			this._counterBombardment = this.Create("siege_strategy_counter_bombardment");
			this._prepareAssault = this.Create("siege_strategy_prepare_assault");
			this._breachWalls = this.Create("siege_strategy_breach_walls");
			this._wearOutDefenders = this.Create("siege_strategy_wear_out_defenders");
			this._custom = this.Create("siege_strategy_custom");
			this.InitializeAll();
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000939E6 File Offset: 0x00091BE6
		private SiegeStrategy Create(string stringId)
		{
			return Game.Current.ObjectManager.RegisterPresumedObject<SiegeStrategy>(new SiegeStrategy(stringId));
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x00093A00 File Offset: 0x00091C00
		private void InitializeAll()
		{
			this._custom.Initialize(new TextObject("{=!}Custom", null), new TextObject("{=!}Custom strategy that can be managed entirely.", null));
			this._preserveStrength.Initialize(new TextObject("{=!}Preserve Strength", null), new TextObject("{=!}Priority is set to preserving our strength.", null));
			this._prepareAgainstAssault.Initialize(new TextObject("{=!}Prepare Against Assault", null), new TextObject("{=!}Priority is set to keep advantage when the enemies' assault starts.", null));
			this._counterBombardment.Initialize(new TextObject("{=!}Counter Bombardment", null), new TextObject("{=!}Priority is set to countering enemy bombardment.", null));
			this._prepareAssault.Initialize(new TextObject("{=!}Prepare Assault", null), new TextObject("{=!}Priority is set to assaulting the walls.", null));
			this._breachWalls.Initialize(new TextObject("{=!}Breach Walls", null), new TextObject("{=!}Priority is set to breaching the walls.", null));
			this._wearOutDefenders.Initialize(new TextObject("{=!}Wear out Defenders", null), new TextObject("{=!}Priority is set to destroying engines of the enemy.", null));
		}

		// Token: 0x04000A9A RID: 2714
		private SiegeStrategy _preserveStrength;

		// Token: 0x04000A9B RID: 2715
		private SiegeStrategy _prepareAgainstAssault;

		// Token: 0x04000A9C RID: 2716
		private SiegeStrategy _counterBombardment;

		// Token: 0x04000A9D RID: 2717
		private SiegeStrategy _prepareAssault;

		// Token: 0x04000A9E RID: 2718
		private SiegeStrategy _breachWalls;

		// Token: 0x04000A9F RID: 2719
		private SiegeStrategy _wearOutDefenders;

		// Token: 0x04000AA0 RID: 2720
		private SiegeStrategy _custom;
	}
}
