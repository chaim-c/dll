using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000333 RID: 819
	public class EducationState : GameState
	{
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000C1EEE File Offset: 0x000C00EE
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x000C1EF1 File Offset: 0x000C00F1
		// (set) Token: 0x06002E93 RID: 11923 RVA: 0x000C1EF9 File Offset: 0x000C00F9
		public Hero Child { get; private set; }

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x000C1F02 File Offset: 0x000C0102
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x000C1F0A File Offset: 0x000C010A
		public IEducationStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000C1F13 File Offset: 0x000C0113
		public EducationState()
		{
			Debug.FailedAssert("Do not use EducationState with default constructor!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\GameState\\EducationState.cs", ".ctor", 22);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000C1F31 File Offset: 0x000C0131
		public EducationState(Hero child)
		{
			this.Child = child;
		}

		// Token: 0x04000DED RID: 3565
		private IEducationStateHandler _handler;
	}
}
