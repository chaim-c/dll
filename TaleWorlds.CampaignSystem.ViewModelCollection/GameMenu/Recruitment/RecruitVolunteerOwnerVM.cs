using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment
{
	// Token: 0x020000A0 RID: 160
	public class RecruitVolunteerOwnerVM : HeroVM
	{
		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003E10F File Offset: 0x0003C30F
		public RecruitVolunteerOwnerVM(Hero hero, int relation) : base(hero, hero != null && hero.IsNotable)
		{
			this._hero = hero;
			this.RelationToPlayer = relation;
			this.RefreshValues();
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003E138 File Offset: 0x0003C338
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._hero != null)
			{
				if (this._hero.IsPreacher)
				{
					this.TitleText = GameTexts.FindText("str_preacher", null).ToString();
					return;
				}
				if (this._hero.IsGangLeader)
				{
					this.TitleText = GameTexts.FindText("str_gang_leader", null).ToString();
					return;
				}
				if (this._hero.IsMerchant)
				{
					this.TitleText = GameTexts.FindText("str_merchant", null).ToString();
					return;
				}
				if (this._hero.IsRuralNotable)
				{
					this.TitleText = GameTexts.FindText("str_rural_notable", null).ToString();
				}
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003E1E5 File Offset: 0x0003C3E5
		public void ExecuteOpenEncyclopedia()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._hero.EncyclopediaLink);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003E201 File Offset: 0x0003C401
		public void ExecuteFocus()
		{
			Action<RecruitVolunteerOwnerVM> onFocused = RecruitVolunteerOwnerVM.OnFocused;
			if (onFocused == null)
			{
				return;
			}
			onFocused(this);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0003E213 File Offset: 0x0003C413
		public void ExecuteUnfocus()
		{
			Action<RecruitVolunteerOwnerVM> onFocused = RecruitVolunteerOwnerVM.OnFocused;
			if (onFocused == null)
			{
				return;
			}
			onFocused(null);
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0003E225 File Offset: 0x0003C425
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x0003E22D File Offset: 0x0003C42D
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x0003E250 File Offset: 0x0003C450
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x0003E258 File Offset: 0x0003C458
		[DataSourceProperty]
		public int RelationToPlayer
		{
			get
			{
				return this._relationToPlayer;
			}
			set
			{
				if (value != this._relationToPlayer)
				{
					this._relationToPlayer = value;
					base.OnPropertyChangedWithValue(value, "RelationToPlayer");
				}
			}
		}

		// Token: 0x04000759 RID: 1881
		public static Action<RecruitVolunteerOwnerVM> OnFocused;

		// Token: 0x0400075A RID: 1882
		private Hero _hero;

		// Token: 0x0400075B RID: 1883
		private string _titleText;

		// Token: 0x0400075C RID: 1884
		private int _relationToPlayer;
	}
}
