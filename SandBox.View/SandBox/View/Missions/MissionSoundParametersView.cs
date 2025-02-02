using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions
{
	// Token: 0x0200001E RID: 30
	[DefaultView]
	public class MissionSoundParametersView : MissionView
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000A327 File Offset: 0x00008527
		public override void EarlyStart()
		{
			base.EarlyStart();
			this.InitializeGlobalParameters();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000A335 File Offset: 0x00008535
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			SoundManager.SetGlobalParameter("MissionCulture", 0f);
			SoundManager.SetGlobalParameter("MissionProsperity", 0f);
			SoundManager.SetGlobalParameter("MissionCombatMode", 0f);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000A36A File Offset: 0x0000856A
		public override void OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
			this.InitializeCombatModeParameter();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A372 File Offset: 0x00008572
		private void InitializeGlobalParameters()
		{
			this.InitializeCultureParameter();
			this.InitializeProsperityParameter();
			this.InitializeCombatModeParameter();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000A388 File Offset: 0x00008588
		private void InitializeCultureParameter()
		{
			MissionSoundParametersView.SoundParameterMissionCulture soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.None;
			if (Campaign.Current != null)
			{
				Settlement currentSettlement = Settlement.CurrentSettlement;
				if (currentSettlement != null)
				{
					if (currentSettlement.IsHideout)
					{
						soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Bandit;
					}
					else
					{
						string stringId = currentSettlement.Culture.StringId;
						if (!(stringId == "empire"))
						{
							if (!(stringId == "sturgia"))
							{
								if (!(stringId == "aserai"))
								{
									if (!(stringId == "vlandia"))
									{
										if (!(stringId == "battania"))
										{
											if (stringId == "khuzait")
											{
												soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Khuzait;
											}
										}
										else
										{
											soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Battania;
										}
									}
									else
									{
										soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Vlandia;
									}
								}
								else
								{
									soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Aserai;
								}
							}
							else
							{
								soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Sturgia;
							}
						}
						else
						{
							soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Empire;
						}
					}
				}
			}
			SoundManager.SetGlobalParameter("MissionCulture", (float)soundParameterMissionCulture);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000A434 File Offset: 0x00008634
		private void InitializeProsperityParameter()
		{
			MissionSoundParametersView.SoundParameterMissionProsperityLevel soundParameterMissionProsperityLevel = MissionSoundParametersView.SoundParameterMissionProsperityLevel.None;
			if (Campaign.Current != null && Settlement.CurrentSettlement != null)
			{
				switch (Settlement.CurrentSettlement.SettlementComponent.GetProsperityLevel())
				{
				case SettlementComponent.ProsperityLevel.Low:
					soundParameterMissionProsperityLevel = MissionSoundParametersView.SoundParameterMissionProsperityLevel.None;
					break;
				case SettlementComponent.ProsperityLevel.Mid:
					soundParameterMissionProsperityLevel = MissionSoundParametersView.SoundParameterMissionProsperityLevel.Mid;
					break;
				case SettlementComponent.ProsperityLevel.High:
					soundParameterMissionProsperityLevel = MissionSoundParametersView.SoundParameterMissionProsperityLevel.High;
					break;
				}
			}
			SoundManager.SetGlobalParameter("MissionProsperity", (float)soundParameterMissionProsperityLevel);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000A48C File Offset: 0x0000868C
		private void InitializeCombatModeParameter()
		{
			bool flag = base.Mission.Mode == MissionMode.Battle || base.Mission.Mode == MissionMode.Duel || base.Mission.Mode == MissionMode.Tournament;
			SoundManager.SetGlobalParameter("MissionCombatMode", (float)(flag ? 1 : 0));
		}

		// Token: 0x04000076 RID: 118
		private const string CultureParameterId = "MissionCulture";

		// Token: 0x04000077 RID: 119
		private const string ProsperityParameterId = "MissionProsperity";

		// Token: 0x04000078 RID: 120
		private const string CombatParameterId = "MissionCombatMode";

		// Token: 0x0200006A RID: 106
		public enum SoundParameterMissionCulture : short
		{
			// Token: 0x04000282 RID: 642
			None,
			// Token: 0x04000283 RID: 643
			Aserai,
			// Token: 0x04000284 RID: 644
			Battania,
			// Token: 0x04000285 RID: 645
			Empire,
			// Token: 0x04000286 RID: 646
			Khuzait,
			// Token: 0x04000287 RID: 647
			Sturgia,
			// Token: 0x04000288 RID: 648
			Vlandia,
			// Token: 0x04000289 RID: 649
			Bandit
		}

		// Token: 0x0200006B RID: 107
		private enum SoundParameterMissionProsperityLevel : short
		{
			// Token: 0x0400028B RID: 651
			None,
			// Token: 0x0400028C RID: 652
			Low = 0,
			// Token: 0x0400028D RID: 653
			Mid,
			// Token: 0x0400028E RID: 654
			High
		}
	}
}
