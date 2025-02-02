using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200032C RID: 812
	public class BarberState : GameState
	{
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000C1D6C File Offset: 0x000BFF6C
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000C1D6F File Offset: 0x000BFF6F
		// (set) Token: 0x06002E6B RID: 11883 RVA: 0x000C1D77 File Offset: 0x000BFF77
		public IFaceGeneratorCustomFilter Filter { get; private set; }

		// Token: 0x06002E6C RID: 11884 RVA: 0x000C1D80 File Offset: 0x000BFF80
		public BarberState()
		{
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000C1D88 File Offset: 0x000BFF88
		public BarberState(BasicCharacterObject character, IFaceGeneratorCustomFilter filter)
		{
			this.Character = character;
			this.Filter = filter;
		}

		// Token: 0x04000DE0 RID: 3552
		public BasicCharacterObject Character;
	}
}
