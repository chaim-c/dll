using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000281 RID: 641
	public struct MissionSpawnSettings
	{
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0007AE9F File Offset: 0x0007909F
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x0007AEA7 File Offset: 0x000790A7
		public float GlobalReinforcementInterval
		{
			get
			{
				return this._globalReinforcementInterval;
			}
			set
			{
				this._globalReinforcementInterval = MathF.Max(value, 1f);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0007AEBA File Offset: 0x000790BA
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x0007AEC2 File Offset: 0x000790C2
		public float DefenderAdvantageFactor
		{
			get
			{
				return this._defenderAdvantageFactor;
			}
			set
			{
				this._defenderAdvantageFactor = MathF.Clamp(value, 0.1f, 10f);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x0007AEDA File Offset: 0x000790DA
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x0007AEE2 File Offset: 0x000790E2
		public float MaximumBattleSideRatio
		{
			get
			{
				return this._maximumBattleSizeRatio;
			}
			set
			{
				this._maximumBattleSizeRatio = MathF.Clamp(value, 0.5f, 0.99f);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0007AEFA File Offset: 0x000790FA
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x0007AF02 File Offset: 0x00079102
		public MissionSpawnSettings.InitialSpawnMethod InitialTroopsSpawnMethod { get; private set; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0007AF0B File Offset: 0x0007910B
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0007AF13 File Offset: 0x00079113
		public MissionSpawnSettings.ReinforcementTimingMethod ReinforcementTroopsTimingMethod { get; private set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0007AF1C File Offset: 0x0007911C
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x0007AF24 File Offset: 0x00079124
		public MissionSpawnSettings.ReinforcementSpawnMethod ReinforcementTroopsSpawnMethod { get; private set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0007AF2D File Offset: 0x0007912D
		// (set) Token: 0x060021B6 RID: 8630 RVA: 0x0007AF35 File Offset: 0x00079135
		public float ReinforcementBatchPercentage
		{
			get
			{
				return this._reinforcementBatchPercentage;
			}
			set
			{
				this._reinforcementBatchPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0007AF4D File Offset: 0x0007914D
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0007AF55 File Offset: 0x00079155
		public float DesiredReinforcementPercentage
		{
			get
			{
				return this._desiredReinforcementPercentage;
			}
			set
			{
				this._desiredReinforcementPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x0007AF6D File Offset: 0x0007916D
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x0007AF75 File Offset: 0x00079175
		public float ReinforcementWavePercentage
		{
			get
			{
				return this._reinforcementWavePercentage;
			}
			set
			{
				this._reinforcementWavePercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0007AF8D File Offset: 0x0007918D
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x0007AF95 File Offset: 0x00079195
		public int MaximumReinforcementWaveCount
		{
			get
			{
				return this._maximumReinforcementWaveCount;
			}
			set
			{
				this._maximumReinforcementWaveCount = MathF.Max(value, 0);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0007AFA4 File Offset: 0x000791A4
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x0007AFAC File Offset: 0x000791AC
		public float DefenderReinforcementBatchPercentage
		{
			get
			{
				return this._defenderReinforcementBatchPercentage;
			}
			set
			{
				this._defenderReinforcementBatchPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0007AFC4 File Offset: 0x000791C4
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x0007AFCC File Offset: 0x000791CC
		public float AttackerReinforcementBatchPercentage
		{
			get
			{
				return this._attackerReinforcementBatchPercentage;
			}
			set
			{
				this._attackerReinforcementBatchPercentage = MathF.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0007AFE4 File Offset: 0x000791E4
		public MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod initialTroopsSpawnMethod, MissionSpawnSettings.ReinforcementTimingMethod reinforcementTimingMethod, MissionSpawnSettings.ReinforcementSpawnMethod reinforcementTroopsSpawnMethod, float globalReinforcementInterval = 0f, float reinforcementBatchPercentage = 0f, float desiredReinforcementPercentage = 0f, float reinforcementWavePercentage = 0f, int maximumReinforcementWaveCount = 0, float defenderReinforcementBatchPercentage = 0f, float attackerReinforcementBatchPercentage = 0f, float defenderAdvantageFactor = 1f, float maximumBattleSizeRatio = 0.75f)
		{
			this = default(MissionSpawnSettings);
			this.InitialTroopsSpawnMethod = initialTroopsSpawnMethod;
			this.ReinforcementTroopsTimingMethod = reinforcementTimingMethod;
			this.ReinforcementTroopsSpawnMethod = reinforcementTroopsSpawnMethod;
			this.GlobalReinforcementInterval = globalReinforcementInterval;
			this.ReinforcementBatchPercentage = reinforcementBatchPercentage;
			this.DesiredReinforcementPercentage = desiredReinforcementPercentage;
			this.ReinforcementWavePercentage = reinforcementWavePercentage;
			this.MaximumReinforcementWaveCount = maximumReinforcementWaveCount;
			this.DefenderReinforcementBatchPercentage = defenderReinforcementBatchPercentage;
			this.AttackerReinforcementBatchPercentage = attackerReinforcementBatchPercentage;
			this.DefenderAdvantageFactor = defenderAdvantageFactor;
			this.MaximumBattleSideRatio = maximumBattleSizeRatio;
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0007B058 File Offset: 0x00079258
		public static MissionSpawnSettings CreateDefaultSpawnSettings()
		{
			return new MissionSpawnSettings(MissionSpawnSettings.InitialSpawnMethod.BattleSizeAllocating, MissionSpawnSettings.ReinforcementTimingMethod.GlobalTimer, MissionSpawnSettings.ReinforcementSpawnMethod.Balanced, 10f, 0.05f, 0.166f, 0f, 0, 0f, 0f, 1f, 0.75f);
		}

		// Token: 0x04000C82 RID: 3202
		public const float MinimumReinforcementInterval = 1f;

		// Token: 0x04000C83 RID: 3203
		public const float MinimumDefenderAdvantageFactor = 0.1f;

		// Token: 0x04000C84 RID: 3204
		public const float MaximumDefenderAdvantageFactor = 10f;

		// Token: 0x04000C85 RID: 3205
		public const float MinimumBattleSizeRatioLimit = 0.5f;

		// Token: 0x04000C86 RID: 3206
		public const float MaximumBattleSizeRatioLimit = 0.99f;

		// Token: 0x04000C87 RID: 3207
		public const float DefaultMaximumBattleSizeRatio = 0.75f;

		// Token: 0x04000C88 RID: 3208
		public const float DefaultDefenderAdvantageFactor = 1f;

		// Token: 0x04000C8C RID: 3212
		private float _globalReinforcementInterval;

		// Token: 0x04000C8D RID: 3213
		private float _defenderAdvantageFactor;

		// Token: 0x04000C8E RID: 3214
		private float _maximumBattleSizeRatio;

		// Token: 0x04000C8F RID: 3215
		private float _reinforcementBatchPercentage;

		// Token: 0x04000C90 RID: 3216
		private float _desiredReinforcementPercentage;

		// Token: 0x04000C91 RID: 3217
		private float _reinforcementWavePercentage;

		// Token: 0x04000C92 RID: 3218
		private int _maximumReinforcementWaveCount;

		// Token: 0x04000C93 RID: 3219
		private float _defenderReinforcementBatchPercentage;

		// Token: 0x04000C94 RID: 3220
		private float _attackerReinforcementBatchPercentage;

		// Token: 0x02000534 RID: 1332
		public enum ReinforcementSpawnMethod
		{
			// Token: 0x04001C87 RID: 7303
			Balanced,
			// Token: 0x04001C88 RID: 7304
			Wave,
			// Token: 0x04001C89 RID: 7305
			Fixed
		}

		// Token: 0x02000535 RID: 1333
		public enum ReinforcementTimingMethod
		{
			// Token: 0x04001C8B RID: 7307
			GlobalTimer,
			// Token: 0x04001C8C RID: 7308
			CustomTimer
		}

		// Token: 0x02000536 RID: 1334
		public enum InitialSpawnMethod
		{
			// Token: 0x04001C8E RID: 7310
			BattleSizeAllocating,
			// Token: 0x04001C8F RID: 7311
			FreeAllocation
		}
	}
}
