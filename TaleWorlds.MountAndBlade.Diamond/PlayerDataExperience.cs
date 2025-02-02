using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014C RID: 332
	public struct PlayerDataExperience
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0000D7FF File Offset: 0x0000B9FF
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0000D807 File Offset: 0x0000BA07
		public int Experience { get; private set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0000D810 File Offset: 0x0000BA10
		public int Level
		{
			get
			{
				return PlayerDataExperience.CalculateLevelFromExperience(this.Experience);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0000D81D File Offset: 0x0000BA1D
		public int ExperienceToNextLevel
		{
			get
			{
				return PlayerDataExperience.CalculateExperienceFromLevel(this.Level + 1) - this.Experience;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0000D833 File Offset: 0x0000BA33
		public int ExperienceInCurrentLevel
		{
			get
			{
				return this.Experience - PlayerDataExperience.CalculateExperienceFromLevel(this.Level);
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0000D847 File Offset: 0x0000BA47
		static PlayerDataExperience()
		{
			PlayerDataExperience.InitializeXPRequirements();
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0000D855 File Offset: 0x0000BA55
		public PlayerDataExperience(int experience)
		{
			this.Experience = experience;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0000D860 File Offset: 0x0000BA60
		public static int CalculateLevelFromExperience(int experience)
		{
			int num = 1;
			int i = 0;
			while (i <= experience)
			{
				i += PlayerDataExperience.ExperienceRequiredForLevel(num + 1);
				if (i <= experience)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000D88B File Offset: 0x0000BA8B
		public static int CalculateExperienceFromLevel(int level)
		{
			if (level == 1)
			{
				return 0;
			}
			if (level < PlayerDataExperience._maxLevelForXPRequirementCalculation)
			{
				return PlayerDataExperience._levelToXP[level];
			}
			return PlayerDataExperience.ExperienceRequiredForLevel(level) + PlayerDataExperience.CalculateExperienceFromLevel(level - 1);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000D8B2 File Offset: 0x0000BAB2
		public static int ExperienceRequiredForLevel(int level)
		{
			return Convert.ToInt32(Math.Floor(100.0 * Math.Pow((double)(level - 1), 1.03)));
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000D8DC File Offset: 0x0000BADC
		private static void InitializeXPRequirements()
		{
			PlayerDataExperience._levelToXP = new int[PlayerDataExperience._maxLevelForXPRequirementCalculation];
			int num = 0;
			for (int i = 2; i < PlayerDataExperience._maxLevelForXPRequirementCalculation; i++)
			{
				num += PlayerDataExperience.ExperienceRequiredForLevel(i);
				PlayerDataExperience._levelToXP[i] = num;
			}
		}

		// Token: 0x040003C1 RID: 961
		private static int[] _levelToXP;

		// Token: 0x040003C2 RID: 962
		private static readonly int _maxLevelForXPRequirementCalculation = 30;
	}
}
