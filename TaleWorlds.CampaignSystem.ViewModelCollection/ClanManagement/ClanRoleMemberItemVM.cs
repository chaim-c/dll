using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200010E RID: 270
	public class ClanRoleMemberItemVM : ViewModel
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0005DFA6 File Offset: 0x0005C1A6
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0005DFAE File Offset: 0x0005C1AE
		public SkillEffect.PerkRole Role { get; private set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0005DFB7 File Offset: 0x0005C1B7
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x0005DFBF File Offset: 0x0005C1BF
		public SkillObject RelevantSkill { get; private set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x0005DFC8 File Offset: 0x0005C1C8
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x0005DFD0 File Offset: 0x0005C1D0
		public int RelevantSkillValue { get; private set; }

		// Token: 0x06001A17 RID: 6679 RVA: 0x0005DFDC File Offset: 0x0005C1DC
		public ClanRoleMemberItemVM(MobileParty party, SkillEffect.PerkRole role, ClanPartyMemberItemVM member, Action onRoleAssigned)
		{
			this.Role = role;
			this.Member = member;
			this._party = party;
			this._onRoleAssigned = onRoleAssigned;
			this.RelevantSkill = ClanRoleMemberItemVM.GetRelevantSkillForRole(role);
			ClanPartyMemberItemVM member2 = this.Member;
			int? num;
			if (member2 == null)
			{
				num = null;
			}
			else
			{
				Hero heroObject = member2.HeroObject;
				num = ((heroObject != null) ? new int?(heroObject.GetSkillValue(this.RelevantSkill)) : null);
			}
			this.RelevantSkillValue = (num ?? -1);
			this._skillEffects = from x in SkillEffect.All
			where x.PrimaryRole != SkillEffect.PerkRole.Personal || x.SecondaryRole != SkillEffect.PerkRole.Personal
			select x;
			this._perks = from x in PerkObject.All
			where this.Member.HeroObject.GetPerkValue(x)
			select x;
			this.IsRemoveAssigneeOption = (this.Member == null);
			this.Hint = new HintViewModel(this.IsRemoveAssigneeOption ? new TextObject("{=bfWlTVjs}Remove assignee", null) : this.GetRoleHint(this.Role), null);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0005E0F2 File Offset: 0x0005C2F2
		public override void RefreshValues()
		{
			base.RefreshValues();
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0005E0FA File Offset: 0x0005C2FA
		public override void OnFinalize()
		{
			base.OnFinalize();
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0005E104 File Offset: 0x0005C304
		public void ExecuteAssignHeroToRole()
		{
			if (this.Member == null)
			{
				switch (this.Role)
				{
				case SkillEffect.PerkRole.Surgeon:
					this._party.SetPartySurgeon(null);
					break;
				case SkillEffect.PerkRole.Engineer:
					this._party.SetPartyEngineer(null);
					break;
				case SkillEffect.PerkRole.Scout:
					this._party.SetPartyScout(null);
					break;
				case SkillEffect.PerkRole.Quartermaster:
					this._party.SetPartyQuartermaster(null);
					break;
				}
			}
			else
			{
				this.OnSetMemberAsRole(this.Role);
			}
			Action onRoleAssigned = this._onRoleAssigned;
			if (onRoleAssigned == null)
			{
				return;
			}
			onRoleAssigned();
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0005E190 File Offset: 0x0005C390
		private void OnSetMemberAsRole(SkillEffect.PerkRole role)
		{
			if (role != SkillEffect.PerkRole.None)
			{
				if (this._party.GetHeroPerkRole(this.Member.HeroObject) != role)
				{
					this._party.RemoveHeroPerkRole(this.Member.HeroObject);
					if (role == SkillEffect.PerkRole.Engineer)
					{
						this._party.SetPartyEngineer(this.Member.HeroObject);
					}
					else if (role == SkillEffect.PerkRole.Quartermaster)
					{
						this._party.SetPartyQuartermaster(this.Member.HeroObject);
					}
					else if (role == SkillEffect.PerkRole.Scout)
					{
						this._party.SetPartyScout(this.Member.HeroObject);
					}
					else if (role == SkillEffect.PerkRole.Surgeon)
					{
						this._party.SetPartySurgeon(this.Member.HeroObject);
					}
					Game game = Game.Current;
					if (game != null)
					{
						game.EventManager.TriggerEvent<ClanRoleAssignedThroughClanScreenEvent>(new ClanRoleAssignedThroughClanScreenEvent(role, this.Member.HeroObject));
					}
				}
			}
			else if (role == SkillEffect.PerkRole.None)
			{
				this._party.RemoveHeroPerkRole(this.Member.HeroObject);
			}
			Action onRoleAssigned = this._onRoleAssigned;
			if (onRoleAssigned == null)
			{
				return;
			}
			onRoleAssigned();
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0005E298 File Offset: 0x0005C498
		private TextObject GetRoleHint(SkillEffect.PerkRole role)
		{
			string text = "";
			if (this.RelevantSkillValue <= 0)
			{
				GameTexts.SetVariable("LEFT", this.RelevantSkill.Name.ToString());
				GameTexts.SetVariable("RIGHT", this.RelevantSkillValue.ToString());
				GameTexts.SetVariable("STR2", GameTexts.FindText("str_LEFT_colon_RIGHT", null).ToString());
				GameTexts.SetVariable("STR1", this.Member.Name.ToString());
				text = GameTexts.FindText("str_string_newline_string", null).ToString();
			}
			else if (!ClanRoleMemberItemVM.DoesHeroHaveEnoughSkillForRole(this.Member.HeroObject, role, this._party))
			{
				GameTexts.SetVariable("SKILL_NAME", this.RelevantSkill.Name.ToString());
				GameTexts.SetVariable("MIN_SKILL_AMOUNT", 0);
				text = GameTexts.FindText("str_character_role_disabled_tooltip", null).ToString();
			}
			else
			{
				if (!role.Equals(SkillEffect.PerkRole.None))
				{
					IEnumerable<SkillEffect> enumerable = from x in this._skillEffects
					where x.PrimaryRole == role || x.SecondaryRole == role
					select x;
					IEnumerable<PerkObject> enumerable2 = from x in this._perks
					where x.PrimaryRole == role || x.SecondaryRole == role
					select x;
					GameTexts.SetVariable("LEFT", this.RelevantSkill.Name.ToString());
					GameTexts.SetVariable("RIGHT", this.RelevantSkillValue.ToString());
					GameTexts.SetVariable("STR2", GameTexts.FindText("str_LEFT_colon_RIGHT", null).ToString());
					GameTexts.SetVariable("STR1", this.Member.Name.ToString());
					text = GameTexts.FindText("str_string_newline_string", null).ToString();
					int num = 0;
					TextObject textObject = GameTexts.FindText("str_LEFT_colon_RIGHT", null).CopyTextObject();
					textObject.SetTextVariable("LEFT", new TextObject("{=Avy8Gua1}Perks", null));
					textObject.SetTextVariable("RIGHT", new TextObject("{=Gp2vmZGZ}{PERKS}", null));
					foreach (PerkObject perkObject in enumerable2)
					{
						if (num == 0)
						{
							GameTexts.SetVariable("PERKS", perkObject.Name.ToString());
						}
						else
						{
							GameTexts.SetVariable("RIGHT", perkObject.Name.ToString());
							GameTexts.SetVariable("LEFT", new TextObject("{=Gp2vmZGZ}{PERKS}", null).ToString());
							GameTexts.SetVariable("PERKS", GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString());
						}
						num++;
					}
					GameTexts.SetVariable("newline", "\n \n");
					if (num > 0)
					{
						GameTexts.SetVariable("STR1", text);
						GameTexts.SetVariable("STR2", textObject.ToString());
						text = GameTexts.FindText("str_string_newline_string", null).ToString();
					}
					GameTexts.SetVariable("LEFT", new TextObject("{=DKJIp6xG}Effects", null).ToString());
					string content = GameTexts.FindText("str_LEFT_colon", null).ToString();
					GameTexts.SetVariable("STR1", text);
					GameTexts.SetVariable("STR2", content);
					text = GameTexts.FindText("str_string_newline_string", null).ToString();
					GameTexts.SetVariable("newline", "\n");
					using (IEnumerator<SkillEffect> enumerator2 = enumerable.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							SkillEffect effect = enumerator2.Current;
							GameTexts.SetVariable("STR1", text);
							GameTexts.SetVariable("STR2", SkillHelper.GetEffectDescriptionForSkillLevel(effect, this.RelevantSkillValue));
							text = GameTexts.FindText("str_string_newline_string", null).ToString();
						}
						goto IL_390;
					}
				}
				text = null;
			}
			IL_390:
			if (!string.IsNullOrEmpty(text))
			{
				return new TextObject("{=!}" + text, null);
			}
			return TextObject.Empty;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0005E670 File Offset: 0x0005C870
		public string GetEffectsList(SkillEffect.PerkRole role)
		{
			string text = "";
			IEnumerable<SkillEffect> enumerable = from x in this._skillEffects
			where x.PrimaryRole == role || x.SecondaryRole == role
			select x;
			int num = 0;
			if (this.RelevantSkillValue > 0)
			{
				foreach (SkillEffect effect in enumerable)
				{
					if (num == 0)
					{
						text = SkillHelper.GetEffectDescriptionForSkillLevel(effect, this.RelevantSkillValue);
					}
					else
					{
						GameTexts.SetVariable("STR1", text);
						GameTexts.SetVariable("STR2", SkillHelper.GetEffectDescriptionForSkillLevel(effect, this.RelevantSkillValue));
						text = GameTexts.FindText("str_string_newline_string", null).ToString();
					}
					num++;
				}
			}
			return text;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0005E738 File Offset: 0x0005C938
		private static SkillObject GetRelevantSkillForRole(SkillEffect.PerkRole role)
		{
			if (role == SkillEffect.PerkRole.Engineer)
			{
				return DefaultSkills.Engineering;
			}
			if (role == SkillEffect.PerkRole.Quartermaster)
			{
				return DefaultSkills.Steward;
			}
			if (role == SkillEffect.PerkRole.Scout)
			{
				return DefaultSkills.Scouting;
			}
			if (role == SkillEffect.PerkRole.Surgeon)
			{
				return DefaultSkills.Medicine;
			}
			Debug.FailedAssert(string.Format("Undefined clan role relevant skill {0}", role), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\ClanManagement\\ClanRoleMemberItemVM.cs", "GetRelevantSkillForRole", 246);
			return null;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0005E794 File Offset: 0x0005C994
		public static bool IsHeroAssignableForRole(Hero hero, SkillEffect.PerkRole role, MobileParty party)
		{
			return ClanRoleMemberItemVM.DoesHeroHaveEnoughSkillForRole(hero, role, party) && hero.CanBeGovernorOrHavePartyRole();
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
		private static bool DoesHeroHaveEnoughSkillForRole(Hero hero, SkillEffect.PerkRole role, MobileParty party)
		{
			if (party.GetHeroPerkRole(hero) == role)
			{
				return true;
			}
			if (role == SkillEffect.PerkRole.Engineer)
			{
				return MobilePartyHelper.IsHeroAssignableForEngineerInParty(hero, party);
			}
			if (role == SkillEffect.PerkRole.Quartermaster)
			{
				return MobilePartyHelper.IsHeroAssignableForQuartermasterInParty(hero, party);
			}
			if (role == SkillEffect.PerkRole.Scout)
			{
				return MobilePartyHelper.IsHeroAssignableForScoutInParty(hero, party);
			}
			if (role == SkillEffect.PerkRole.Surgeon)
			{
				return MobilePartyHelper.IsHeroAssignableForSurgeonInParty(hero, party);
			}
			if (role == SkillEffect.PerkRole.None)
			{
				return true;
			}
			Debug.FailedAssert(string.Format("Undefined clan role is asked if assignable {0}", role), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\ClanManagement\\ClanRoleMemberItemVM.cs", "DoesHeroHaveEnoughSkillForRole", 284);
			return false;
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0005E81D File Offset: 0x0005CA1D
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x0005E825 File Offset: 0x0005CA25
		[DataSourceProperty]
		public ClanPartyMemberItemVM Member
		{
			get
			{
				return this._member;
			}
			set
			{
				if (value != this._member)
				{
					this._member = value;
					base.OnPropertyChangedWithValue<ClanPartyMemberItemVM>(value, "Member");
				}
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x0005E843 File Offset: 0x0005CA43
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x0005E84B File Offset: 0x0005CA4B
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0005E869 File Offset: 0x0005CA69
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x0005E871 File Offset: 0x0005CA71
		[DataSourceProperty]
		public bool IsRemoveAssigneeOption
		{
			get
			{
				return this._isRemoveAssigneeOption;
			}
			set
			{
				if (value != this._isRemoveAssigneeOption)
				{
					this._isRemoveAssigneeOption = value;
					base.OnPropertyChangedWithValue(value, "IsRemoveAssigneeOption");
				}
			}
		}

		// Token: 0x04000C57 RID: 3159
		private Action _onRoleAssigned;

		// Token: 0x04000C58 RID: 3160
		private MobileParty _party;

		// Token: 0x04000C59 RID: 3161
		private readonly IEnumerable<SkillEffect> _skillEffects;

		// Token: 0x04000C5A RID: 3162
		private readonly IEnumerable<PerkObject> _perks;

		// Token: 0x04000C5B RID: 3163
		private ClanPartyMemberItemVM _member;

		// Token: 0x04000C5C RID: 3164
		private HintViewModel _hint;

		// Token: 0x04000C5D RID: 3165
		private bool _isRemoveAssigneeOption;
	}
}
