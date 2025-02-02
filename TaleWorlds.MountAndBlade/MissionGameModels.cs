using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000251 RID: 593
	public sealed class MissionGameModels : GameModelsManager
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x00070EEB File Offset: 0x0006F0EB
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x00070EF2 File Offset: 0x0006F0F2
		public static MissionGameModels Current { get; private set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x00070EFA File Offset: 0x0006F0FA
		// (set) Token: 0x06001FB7 RID: 8119 RVA: 0x00070F02 File Offset: 0x0006F102
		public AgentStatCalculateModel AgentStatCalculateModel { get; private set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x00070F0B File Offset: 0x0006F10B
		// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x00070F13 File Offset: 0x0006F113
		public ApplyWeatherEffectsModel ApplyWeatherEffectsModel { get; private set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x00070F1C File Offset: 0x0006F11C
		// (set) Token: 0x06001FBB RID: 8123 RVA: 0x00070F24 File Offset: 0x0006F124
		public StrikeMagnitudeCalculationModel StrikeMagnitudeModel { get; private set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x00070F2D File Offset: 0x0006F12D
		// (set) Token: 0x06001FBD RID: 8125 RVA: 0x00070F35 File Offset: 0x0006F135
		public AgentApplyDamageModel AgentApplyDamageModel { get; private set; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x00070F3E File Offset: 0x0006F13E
		// (set) Token: 0x06001FBF RID: 8127 RVA: 0x00070F46 File Offset: 0x0006F146
		public AgentDecideKilledOrUnconsciousModel AgentDecideKilledOrUnconsciousModel { get; private set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x00070F4F File Offset: 0x0006F14F
		// (set) Token: 0x06001FC1 RID: 8129 RVA: 0x00070F57 File Offset: 0x0006F157
		public MissionDifficultyModel MissionDifficultyModel { get; private set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x00070F60 File Offset: 0x0006F160
		// (set) Token: 0x06001FC3 RID: 8131 RVA: 0x00070F68 File Offset: 0x0006F168
		public BattleMoraleModel BattleMoraleModel { get; private set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00070F71 File Offset: 0x0006F171
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x00070F79 File Offset: 0x0006F179
		public BattleInitializationModel BattleInitializationModel { get; private set; }

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x00070F82 File Offset: 0x0006F182
		// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x00070F8A File Offset: 0x0006F18A
		public BattleSpawnModel BattleSpawnModel { get; private set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x00070F93 File Offset: 0x0006F193
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x00070F9B File Offset: 0x0006F19B
		public BattleBannerBearersModel BattleBannerBearersModel { get; private set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x00070FA4 File Offset: 0x0006F1A4
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x00070FAC File Offset: 0x0006F1AC
		public FormationArrangementModel FormationArrangementsModel { get; private set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x00070FB5 File Offset: 0x0006F1B5
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x00070FBD File Offset: 0x0006F1BD
		public AutoBlockModel AutoBlockModel { get; private set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x00070FC6 File Offset: 0x0006F1C6
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x00070FCE File Offset: 0x0006F1CE
		public DamageParticleModel DamageParticleModel { get; private set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x00070FD7 File Offset: 0x0006F1D7
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x00070FDF File Offset: 0x0006F1DF
		public ItemPickupModel ItemPickupModel { get; private set; }

		// Token: 0x06001FD2 RID: 8146 RVA: 0x00070FE8 File Offset: 0x0006F1E8
		private void GetSpecificGameBehaviors()
		{
			this.AgentStatCalculateModel = base.GetGameModel<AgentStatCalculateModel>();
			this.ApplyWeatherEffectsModel = base.GetGameModel<ApplyWeatherEffectsModel>();
			this.StrikeMagnitudeModel = base.GetGameModel<StrikeMagnitudeCalculationModel>();
			this.AgentApplyDamageModel = base.GetGameModel<AgentApplyDamageModel>();
			this.AgentDecideKilledOrUnconsciousModel = base.GetGameModel<AgentDecideKilledOrUnconsciousModel>();
			this.MissionDifficultyModel = base.GetGameModel<MissionDifficultyModel>();
			this.BattleMoraleModel = base.GetGameModel<BattleMoraleModel>();
			this.BattleInitializationModel = base.GetGameModel<BattleInitializationModel>();
			this.BattleSpawnModel = base.GetGameModel<BattleSpawnModel>();
			this.BattleBannerBearersModel = base.GetGameModel<BattleBannerBearersModel>();
			this.FormationArrangementsModel = base.GetGameModel<FormationArrangementModel>();
			this.AutoBlockModel = base.GetGameModel<AutoBlockModel>();
			this.DamageParticleModel = base.GetGameModel<DamageParticleModel>();
			this.ItemPickupModel = base.GetGameModel<ItemPickupModel>();
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0007109D File Offset: 0x0006F29D
		private void MakeGameComponentBindings()
		{
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0007109F File Offset: 0x0006F29F
		public MissionGameModels(IEnumerable<GameModel> inputComponents) : base(inputComponents)
		{
			MissionGameModels.Current = this;
			this.GetSpecificGameBehaviors();
			this.MakeGameComponentBindings();
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000710BA File Offset: 0x0006F2BA
		public static void Clear()
		{
			MissionGameModels.Current = null;
		}
	}
}
