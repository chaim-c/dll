using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x0200010D RID: 269
	public class ClanRoleItemVM : ViewModel
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x0005DA9A File Offset: 0x0005BC9A
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x0005DAA2 File Offset: 0x0005BCA2
		public SkillEffect.PerkRole Role { get; private set; }

		// Token: 0x060019F5 RID: 6645 RVA: 0x0005DAAC File Offset: 0x0005BCAC
		public ClanRoleItemVM(MobileParty party, SkillEffect.PerkRole role, MBBindingList<ClanPartyMemberItemVM> heroMembers, Action<ClanRoleItemVM> onRoleSelectionToggled, Action onRoleAssigned)
		{
			this.Role = role;
			this._comparer = new ClanRoleItemVM.ClanRoleMemberComparer();
			this._party = party;
			this._onRoleSelectionToggled = onRoleSelectionToggled;
			this._onRoleAssigned = onRoleAssigned;
			this._heroMembers = heroMembers;
			this.Members = new MBBindingList<ClanRoleMemberItemVM>();
			this.NotAssignedHint = new HintViewModel(new TextObject("{=S1iS3OYj}Party leader is default for unassigned roles", null), null);
			this.DisabledHint = new HintViewModel();
			this.IsEnabled = true;
			this.Refresh();
			this.RefreshValues();
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0005DB30 File Offset: 0x0005BD30
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = GameTexts.FindText("role", this.Role.ToString()).ToString();
			this.NoEffectText = GameTexts.FindText("str_clan_role_no_effect", null).ToString();
			ClanRoleMemberItemVM effectiveOwner = this.EffectiveOwner;
			this.AssignedMemberEffects = (((effectiveOwner != null) ? effectiveOwner.GetEffectsList(this.Role) : null) ?? "");
			this.HasEffects = !string.IsNullOrEmpty(this.AssignedMemberEffects);
			this.Members.ApplyActionOnAllItems(delegate(ClanRoleMemberItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0005DBE7 File Offset: 0x0005BDE7
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Members.ApplyActionOnAllItems(delegate(ClanRoleMemberItemVM x)
			{
				x.OnFinalize();
			});
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0005DC1C File Offset: 0x0005BE1C
		public void Refresh()
		{
			this.Members.ApplyActionOnAllItems(delegate(ClanRoleMemberItemVM x)
			{
				x.OnFinalize();
			});
			this.Members.Clear();
			foreach (ClanPartyMemberItemVM clanPartyMemberItemVM in this._heroMembers)
			{
				if (ClanRoleMemberItemVM.IsHeroAssignableForRole(clanPartyMemberItemVM.HeroObject, this.Role, this._party))
				{
					this.Members.Add(new ClanRoleMemberItemVM(this._party, this.Role, clanPartyMemberItemVM, new Action(this.OnRoleAssigned)));
				}
			}
			this.Members.Add(new ClanRoleMemberItemVM(this._party, this.Role, null, new Action(this.OnRoleAssigned)));
			this.Members.Sort(this._comparer);
			Hero effectiveRoleOwner;
			Hero hero;
			this.GetMemberAssignedToRole(this._party, this.Role, out hero, out effectiveRoleOwner);
			this.EffectiveOwner = this.Members.FirstOrDefault(delegate(ClanRoleMemberItemVM x)
			{
				ClanPartyMemberItemVM member = x.Member;
				return ((member != null) ? member.HeroObject : null) == effectiveRoleOwner;
			});
			this.IsNotAssigned = (hero == null);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0005DD5C File Offset: 0x0005BF5C
		public void ExecuteToggleRoleSelection()
		{
			Action<ClanRoleItemVM> onRoleSelectionToggled = this._onRoleSelectionToggled;
			if (onRoleSelectionToggled == null)
			{
				return;
			}
			onRoleSelectionToggled(this);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0005DD70 File Offset: 0x0005BF70
		private void GetMemberAssignedToRole(MobileParty party, SkillEffect.PerkRole role, out Hero roleOwner, out Hero effectiveRoleOwner)
		{
			roleOwner = party.GetRoleHolder(role);
			switch (role)
			{
			case SkillEffect.PerkRole.Surgeon:
				effectiveRoleOwner = party.EffectiveSurgeon;
				return;
			case SkillEffect.PerkRole.Engineer:
				effectiveRoleOwner = party.EffectiveEngineer;
				return;
			case SkillEffect.PerkRole.Scout:
				effectiveRoleOwner = party.EffectiveScout;
				return;
			case SkillEffect.PerkRole.Quartermaster:
				effectiveRoleOwner = party.EffectiveQuartermaster;
				return;
			default:
				effectiveRoleOwner = party.LeaderHero;
				roleOwner = party.LeaderHero;
				Debug.FailedAssert("Given party role is not valid.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\ClanManagement\\ClanRoleItemVM.cs", "GetMemberAssignedToRole", 107);
				return;
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0005DDEF File Offset: 0x0005BFEF
		private void OnRoleAssigned()
		{
			MBInformationManager.HideInformations();
			Action onRoleAssigned = this._onRoleAssigned;
			if (onRoleAssigned == null)
			{
				return;
			}
			onRoleAssigned();
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0005DE06 File Offset: 0x0005C006
		public void SetEnabled(bool enabled, TextObject disabledHint)
		{
			this.IsEnabled = enabled;
			this.DisabledHint.HintText = disabledHint;
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0005DE1B File Offset: 0x0005C01B
		// (set) Token: 0x060019FE RID: 6654 RVA: 0x0005DE23 File Offset: 0x0005C023
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0005DE41 File Offset: 0x0005C041
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x0005DE49 File Offset: 0x0005C049
		[DataSourceProperty]
		public MBBindingList<ClanRoleMemberItemVM> Members
		{
			get
			{
				return this._members;
			}
			set
			{
				if (value != this._members)
				{
					this._members = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanRoleMemberItemVM>>(value, "Members");
				}
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x0005DE67 File Offset: 0x0005C067
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x0005DE6F File Offset: 0x0005C06F
		[DataSourceProperty]
		public ClanRoleMemberItemVM EffectiveOwner
		{
			get
			{
				return this._effectiveOwner;
			}
			set
			{
				if (value != this._effectiveOwner)
				{
					this._effectiveOwner = value;
					base.OnPropertyChangedWithValue<ClanRoleMemberItemVM>(value, "EffectiveOwner");
				}
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x0005DE8D File Offset: 0x0005C08D
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x0005DE95 File Offset: 0x0005C095
		[DataSourceProperty]
		public HintViewModel NotAssignedHint
		{
			get
			{
				return this._notAssignedHint;
			}
			set
			{
				if (value != this._notAssignedHint)
				{
					this._notAssignedHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "NotAssignedHint");
				}
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x0005DEB3 File Offset: 0x0005C0B3
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x0005DEBB File Offset: 0x0005C0BB
		[DataSourceProperty]
		public HintViewModel DisabledHint
		{
			get
			{
				return this._disabledHint;
			}
			set
			{
				if (value != this._disabledHint)
				{
					this._disabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisabledHint");
				}
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0005DED9 File Offset: 0x0005C0D9
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x0005DEE1 File Offset: 0x0005C0E1
		[DataSourceProperty]
		public bool IsNotAssigned
		{
			get
			{
				return this._isNotAssigned;
			}
			set
			{
				if (value != this._isNotAssigned)
				{
					this._isNotAssigned = value;
					base.OnPropertyChangedWithValue(value, "IsNotAssigned");
				}
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0005DEFF File Offset: 0x0005C0FF
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x0005DF07 File Offset: 0x0005C107
		[DataSourceProperty]
		public bool HasEffects
		{
			get
			{
				return this._hasEffects;
			}
			set
			{
				if (value != this._hasEffects)
				{
					this._hasEffects = value;
					base.OnPropertyChangedWithValue(value, "HasEffects");
				}
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0005DF25 File Offset: 0x0005C125
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x0005DF2D File Offset: 0x0005C12D
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0005DF50 File Offset: 0x0005C150
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x0005DF58 File Offset: 0x0005C158
		[DataSourceProperty]
		public string AssignedMemberEffects
		{
			get
			{
				return this._assignedMemberEffects;
			}
			set
			{
				if (value != this._assignedMemberEffects)
				{
					this._assignedMemberEffects = value;
					base.OnPropertyChangedWithValue<string>(value, "AssignedMemberEffects");
				}
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0005DF7B File Offset: 0x0005C17B
		// (set) Token: 0x06001A10 RID: 6672 RVA: 0x0005DF83 File Offset: 0x0005C183
		[DataSourceProperty]
		public string NoEffectText
		{
			get
			{
				return this._noEffectText;
			}
			set
			{
				if (value != this._noEffectText)
				{
					this._noEffectText = value;
					base.OnPropertyChangedWithValue<string>(value, "NoEffectText");
				}
			}
		}

		// Token: 0x04000C45 RID: 3141
		private Action<ClanRoleItemVM> _onRoleSelectionToggled;

		// Token: 0x04000C46 RID: 3142
		private Action _onRoleAssigned;

		// Token: 0x04000C47 RID: 3143
		private MBBindingList<ClanPartyMemberItemVM> _heroMembers;

		// Token: 0x04000C48 RID: 3144
		private MobileParty _party;

		// Token: 0x04000C49 RID: 3145
		private ClanRoleItemVM.ClanRoleMemberComparer _comparer;

		// Token: 0x04000C4A RID: 3146
		private bool _isEnabled;

		// Token: 0x04000C4B RID: 3147
		private MBBindingList<ClanRoleMemberItemVM> _members;

		// Token: 0x04000C4C RID: 3148
		private ClanRoleMemberItemVM _effectiveOwner;

		// Token: 0x04000C4D RID: 3149
		private HintViewModel _notAssignedHint;

		// Token: 0x04000C4E RID: 3150
		private HintViewModel _disabledHint;

		// Token: 0x04000C4F RID: 3151
		private bool _isNotAssigned;

		// Token: 0x04000C50 RID: 3152
		private bool _hasEffects;

		// Token: 0x04000C51 RID: 3153
		private string _name;

		// Token: 0x04000C52 RID: 3154
		private string _assignedMemberEffects;

		// Token: 0x04000C53 RID: 3155
		private string _noEffectText;

		// Token: 0x02000247 RID: 583
		private class ClanRoleMemberComparer : IComparer<ClanRoleMemberItemVM>
		{
			// Token: 0x060022C6 RID: 8902 RVA: 0x000755B0 File Offset: 0x000737B0
			public int Compare(ClanRoleMemberItemVM x, ClanRoleMemberItemVM y)
			{
				int num = y.RelevantSkillValue.CompareTo(x.RelevantSkillValue);
				if (num == 0)
				{
					return x.Member.HeroObject.Name.ToString().CompareTo(y.Member.HeroObject.Name.ToString());
				}
				return num;
			}
		}
	}
}
