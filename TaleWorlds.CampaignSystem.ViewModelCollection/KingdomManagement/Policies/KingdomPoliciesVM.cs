using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Policies
{
	// Token: 0x0200005E RID: 94
	public class KingdomPoliciesVM : KingdomCategoryVM
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x00022214 File Offset: 0x00020414
		public KingdomPoliciesVM(Action<KingdomDecision> forceDecide)
		{
			this._forceDecide = forceDecide;
			this.ActivePolicies = new MBBindingList<KingdomPolicyItemVM>();
			this.OtherPolicies = new MBBindingList<KingdomPolicyItemVM>();
			this.DoneHint = new HintViewModel();
			this._playerKingdom = (Hero.MainHero.MapFaction as Kingdom);
			this.ProposalAndDisavowalCost = Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfPolicyProposalAndDisavowal(Clan.PlayerClan);
			base.IsAcceptableItemSelected = false;
			this.RefreshValues();
			this.ExecuteSwitchMode();
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000222A0 File Offset: 0x000204A0
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PoliciesText = GameTexts.FindText("str_policies", null).ToString();
			this.ActivePoliciesText = GameTexts.FindText("str_active_policies", null).ToString();
			this.OtherPoliciesText = GameTexts.FindText("str_other_policies", null).ToString();
			this.ProposeNewPolicyText = GameTexts.FindText("str_propose_new_policy", null).ToString();
			this.DisavowPolicyText = GameTexts.FindText("str_disavow_a_policy", null).ToString();
			base.NoItemSelectedText = GameTexts.FindText("str_kingdom_no_policy_selected", null).ToString();
			base.CategoryNameText = new TextObject("{=Sls0KQVn}Elections", null).ToString();
			this.RefreshPolicyList();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00022354 File Offset: 0x00020554
		public void SelectPolicy(PolicyObject policy)
		{
			bool flag = false;
			foreach (KingdomPolicyItemVM kingdomPolicyItemVM in this.ActivePolicies)
			{
				if (kingdomPolicyItemVM.Policy == policy)
				{
					this.OnPolicySelect(kingdomPolicyItemVM);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				foreach (KingdomPolicyItemVM kingdomPolicyItemVM2 in this.OtherPolicies)
				{
					if (kingdomPolicyItemVM2.Policy == policy)
					{
						this.OnPolicySelect(kingdomPolicyItemVM2);
						flag = true;
						break;
					}
				}
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000223FC File Offset: 0x000205FC
		private void OnPolicySelect(KingdomPolicyItemVM policy)
		{
			if (this.CurrentSelectedPolicy != policy)
			{
				if (this.CurrentSelectedPolicy != null)
				{
					this.CurrentSelectedPolicy.IsSelected = false;
				}
				this.CurrentSelectedPolicy = policy;
				if (this.CurrentSelectedPolicy != null)
				{
					this.CurrentSelectedPolicy.IsSelected = true;
					this._currentSelectedPolicyObject = policy.Policy;
					this._currentItemsUnresolvedDecision = Clan.PlayerClan.Kingdom.UnresolvedDecisions.FirstOrDefault(delegate(KingdomDecision d)
					{
						KingdomPolicyDecision kingdomPolicyDecision;
						return (kingdomPolicyDecision = (d as KingdomPolicyDecision)) != null && kingdomPolicyDecision.Policy == this._currentSelectedPolicyObject && !d.ShouldBeCancelled();
					});
					if (this._currentItemsUnresolvedDecision != null)
					{
						TextObject hintText;
						this.CanProposeOrDisavowPolicy = this.GetCanProposeOrDisavowPolicyWithReason(true, out hintText);
						this.DoneHint.HintText = hintText;
						this.ProposeOrDisavowText = GameTexts.FindText("str_resolve", null).ToString();
						this.ProposeActionExplanationText = GameTexts.FindText("str_resolve_explanation", null).ToString();
						this.PolicyLikelihood = KingdomPoliciesVM.CalculateLikelihood(policy.Policy);
					}
					else
					{
						float influence = Clan.PlayerClan.Influence;
						int proposalAndDisavowalCost = this.ProposalAndDisavowalCost;
						bool isUnderMercenaryService = Clan.PlayerClan.IsUnderMercenaryService;
						TextObject hintText2;
						this.CanProposeOrDisavowPolicy = this.GetCanProposeOrDisavowPolicyWithReason(false, out hintText2);
						this.DoneHint.HintText = hintText2;
						if (this.IsPolicyActive(policy.Policy))
						{
							this.ProposeActionExplanationText = GameTexts.FindText("str_policy_propose_again_action_explanation", null).SetTextVariable("SUPPORT", KingdomPoliciesVM.CalculateLikelihood(policy.Policy)).ToString();
						}
						else
						{
							this.ProposeActionExplanationText = GameTexts.FindText("str_policy_propose_action_explanation", null).SetTextVariable("SUPPORT", KingdomPoliciesVM.CalculateLikelihood(policy.Policy)).ToString();
						}
						this.ProposeOrDisavowText = ((this._playerKingdom.Clans.Count > 1) ? GameTexts.FindText("str_policy_propose", null).ToString() : GameTexts.FindText("str_policy_enact", null).ToString());
						base.NotificationCount = Clan.PlayerClan.Kingdom.UnresolvedDecisions.Count((KingdomDecision d) => !d.ShouldBeCancelled());
						this.PolicyLikelihood = KingdomPoliciesVM.CalculateLikelihood(policy.Policy);
					}
					GameTexts.SetVariable("NUMBER", this.PolicyLikelihood);
					this.PolicyLikelihoodText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				}
				base.IsAcceptableItemSelected = (this.CurrentSelectedPolicy != null);
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00022638 File Offset: 0x00020838
		private bool GetCanProposeOrDisavowPolicyWithReason(bool hasUnresolvedDecision, out TextObject disabledReason)
		{
			TextObject textObject;
			if (!CampaignUIHelper.GetMapScreenActionIsEnabledWithReason(out textObject))
			{
				disabledReason = textObject;
				return false;
			}
			if (Clan.PlayerClan.IsUnderMercenaryService)
			{
				disabledReason = GameTexts.FindText("str_mercenaries_cannot_propose_policies", null);
				return false;
			}
			if (!hasUnresolvedDecision && Clan.PlayerClan.Influence < (float)this.ProposalAndDisavowalCost)
			{
				disabledReason = GameTexts.FindText("str_warning_you_dont_have_enough_influence", null);
				return false;
			}
			disabledReason = TextObject.Empty;
			return true;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0002269C File Offset: 0x0002089C
		public void RefreshPolicyList()
		{
			this.ActivePolicies.Clear();
			this.OtherPolicies.Clear();
			if (this._playerKingdom != null)
			{
				foreach (PolicyObject policy in this._playerKingdom.ActivePolicies)
				{
					this.ActivePolicies.Add(new KingdomPolicyItemVM(policy, new Action<KingdomPolicyItemVM>(this.OnPolicySelect), new Func<PolicyObject, bool>(this.IsPolicyActive)));
				}
				foreach (PolicyObject policy2 in from p in PolicyObject.All
				where !this.IsPolicyActive(p)
				select p)
				{
					this.OtherPolicies.Add(new KingdomPolicyItemVM(policy2, new Action<KingdomPolicyItemVM>(this.OnPolicySelect), new Func<PolicyObject, bool>(this.IsPolicyActive)));
				}
			}
			GameTexts.SetVariable("STR", this.ActivePolicies.Count);
			this.NumOfActivePoliciesText = GameTexts.FindText("str_STR_in_parentheses", null).ToString();
			GameTexts.SetVariable("STR", this.OtherPolicies.Count);
			this.NumOfOtherPoliciesText = GameTexts.FindText("str_STR_in_parentheses", null).ToString();
			this.SetDefaultSelectedPolicy();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000227FC File Offset: 0x000209FC
		private bool IsPolicyActive(PolicyObject policy)
		{
			return this._playerKingdom.ActivePolicies.Contains(policy);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00022810 File Offset: 0x00020A10
		private void SetDefaultSelectedPolicy()
		{
			KingdomPolicyItemVM policy = this.IsInProposeMode ? this.OtherPolicies.FirstOrDefault<KingdomPolicyItemVM>() : this.ActivePolicies.FirstOrDefault<KingdomPolicyItemVM>();
			this.OnPolicySelect(policy);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00022848 File Offset: 0x00020A48
		private void ExecuteSwitchMode()
		{
			this.IsInProposeMode = !this.IsInProposeMode;
			this.CurrentActiveModeText = (this.IsInProposeMode ? this.OtherPoliciesText : this.ActivePoliciesText);
			this.CurrentActionText = (this.IsInProposeMode ? this.DisavowPolicyText : this.ProposeNewPolicyText);
			this.SetDefaultSelectedPolicy();
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000228A4 File Offset: 0x00020AA4
		private void ExecuteProposeOrDisavow()
		{
			if (this._currentItemsUnresolvedDecision != null)
			{
				this._forceDecide(this._currentItemsUnresolvedDecision);
				return;
			}
			if (this.CanProposeOrDisavowPolicy)
			{
				KingdomDecision kingdomDecision = new KingdomPolicyDecision(Clan.PlayerClan, this._currentSelectedPolicyObject, this.IsPolicyActive(this._currentSelectedPolicyObject));
				Clan.PlayerClan.Kingdom.AddDecision(kingdomDecision, false);
				this._forceDecide(kingdomDecision);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0002290D File Offset: 0x00020B0D
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00022915 File Offset: 0x00020B15
		[DataSourceProperty]
		public HintViewModel DoneHint
		{
			get
			{
				return this._doneHint;
			}
			set
			{
				if (value != this._doneHint)
				{
					this._doneHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DoneHint");
				}
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00022933 File Offset: 0x00020B33
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0002293B File Offset: 0x00020B3B
		[DataSourceProperty]
		public MBBindingList<KingdomPolicyItemVM> ActivePolicies
		{
			get
			{
				return this._activePolicies;
			}
			set
			{
				if (value != this._activePolicies)
				{
					this._activePolicies = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomPolicyItemVM>>(value, "ActivePolicies");
				}
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00022959 File Offset: 0x00020B59
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x00022961 File Offset: 0x00020B61
		[DataSourceProperty]
		public MBBindingList<KingdomPolicyItemVM> OtherPolicies
		{
			get
			{
				return this._otherPolicies;
			}
			set
			{
				if (value != this._otherPolicies)
				{
					this._otherPolicies = value;
					base.OnPropertyChangedWithValue<MBBindingList<KingdomPolicyItemVM>>(value, "OtherPolicies");
				}
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0002297F File Offset: 0x00020B7F
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x00022987 File Offset: 0x00020B87
		[DataSourceProperty]
		public KingdomPolicyItemVM CurrentSelectedPolicy
		{
			get
			{
				return this._currentSelectedPolicy;
			}
			set
			{
				if (value != this._currentSelectedPolicy)
				{
					this._currentSelectedPolicy = value;
					base.OnPropertyChangedWithValue<KingdomPolicyItemVM>(value, "CurrentSelectedPolicy");
				}
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000229A5 File Offset: 0x00020BA5
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x000229AD File Offset: 0x00020BAD
		[DataSourceProperty]
		public bool CanProposeOrDisavowPolicy
		{
			get
			{
				return this._canProposeOrDisavowPolicy;
			}
			set
			{
				if (value != this._canProposeOrDisavowPolicy)
				{
					this._canProposeOrDisavowPolicy = value;
					base.OnPropertyChangedWithValue(value, "CanProposeOrDisavowPolicy");
				}
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000229CB File Offset: 0x00020BCB
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000229D3 File Offset: 0x00020BD3
		[DataSourceProperty]
		public int ProposalAndDisavowalCost
		{
			get
			{
				return this._proposalAndDisavowalCost;
			}
			set
			{
				if (value != this._proposalAndDisavowalCost)
				{
					this._proposalAndDisavowalCost = value;
					base.OnPropertyChangedWithValue(value, "ProposalAndDisavowalCost");
				}
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000229F1 File Offset: 0x00020BF1
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x000229F9 File Offset: 0x00020BF9
		[DataSourceProperty]
		public string NumOfActivePoliciesText
		{
			get
			{
				return this._numOfActivePoliciesText;
			}
			set
			{
				if (value != this._numOfActivePoliciesText)
				{
					this._numOfActivePoliciesText = value;
					base.OnPropertyChangedWithValue<string>(value, "NumOfActivePoliciesText");
				}
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00022A1C File Offset: 0x00020C1C
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00022A24 File Offset: 0x00020C24
		[DataSourceProperty]
		public string NumOfOtherPoliciesText
		{
			get
			{
				return this._numOfOtherPoliciesText;
			}
			set
			{
				if (value != this._numOfOtherPoliciesText)
				{
					this._numOfOtherPoliciesText = value;
					base.OnPropertyChangedWithValue<string>(value, "NumOfOtherPoliciesText");
				}
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00022A47 File Offset: 0x00020C47
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00022A4F File Offset: 0x00020C4F
		[DataSourceProperty]
		public bool IsInProposeMode
		{
			get
			{
				return this._isInProposeMode;
			}
			set
			{
				if (value != this._isInProposeMode)
				{
					this._isInProposeMode = value;
					base.OnPropertyChangedWithValue(value, "IsInProposeMode");
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00022A6D File Offset: 0x00020C6D
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00022A75 File Offset: 0x00020C75
		[DataSourceProperty]
		public string DisavowPolicyText
		{
			get
			{
				return this._disavowPolicyText;
			}
			set
			{
				if (value != this._disavowPolicyText)
				{
					this._disavowPolicyText = value;
					base.OnPropertyChangedWithValue<string>(value, "DisavowPolicyText");
				}
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00022A98 File Offset: 0x00020C98
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00022AA0 File Offset: 0x00020CA0
		[DataSourceProperty]
		public string CurrentActiveModeText
		{
			get
			{
				return this._currentActiveModeText;
			}
			set
			{
				if (value != this._currentActiveModeText)
				{
					this._currentActiveModeText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentActiveModeText");
				}
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00022AC3 File Offset: 0x00020CC3
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00022ACB File Offset: 0x00020CCB
		[DataSourceProperty]
		public string CurrentActionText
		{
			get
			{
				return this._currentActionText;
			}
			set
			{
				if (value != this._currentActionText)
				{
					this._currentActionText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentActionText");
				}
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00022AEE File Offset: 0x00020CEE
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00022AF6 File Offset: 0x00020CF6
		[DataSourceProperty]
		public string ProposeNewPolicyText
		{
			get
			{
				return this._proposeNewPolicyText;
			}
			set
			{
				if (value != this._proposeNewPolicyText)
				{
					this._proposeNewPolicyText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProposeNewPolicyText");
				}
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00022B19 File Offset: 0x00020D19
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00022B21 File Offset: 0x00020D21
		[DataSourceProperty]
		public string BackText
		{
			get
			{
				return this._backText;
			}
			set
			{
				if (value != this._backText)
				{
					this._backText = value;
					base.OnPropertyChangedWithValue<string>(value, "BackText");
				}
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00022B44 File Offset: 0x00020D44
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00022B4C File Offset: 0x00020D4C
		[DataSourceProperty]
		public string PoliciesText
		{
			get
			{
				return this._policiesText;
			}
			set
			{
				if (value != this._policiesText)
				{
					this._policiesText = value;
					base.OnPropertyChangedWithValue<string>(value, "PoliciesText");
				}
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00022B6F File Offset: 0x00020D6F
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00022B77 File Offset: 0x00020D77
		[DataSourceProperty]
		public string ActivePoliciesText
		{
			get
			{
				return this._activePoliciesText;
			}
			set
			{
				if (value != this._activePoliciesText)
				{
					this._activePoliciesText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActivePoliciesText");
				}
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00022B9A File Offset: 0x00020D9A
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00022BA2 File Offset: 0x00020DA2
		[DataSourceProperty]
		public string PolicyLikelihoodText
		{
			get
			{
				return this._policyLikelihoodText;
			}
			set
			{
				if (value != this._policyLikelihoodText)
				{
					this._policyLikelihoodText = value;
					base.OnPropertyChangedWithValue<string>(value, "PolicyLikelihoodText");
				}
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00022BC5 File Offset: 0x00020DC5
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00022BCD File Offset: 0x00020DCD
		[DataSourceProperty]
		public HintViewModel LikelihoodHint
		{
			get
			{
				return this._likelihoodHint;
			}
			set
			{
				if (value != this._likelihoodHint)
				{
					this._likelihoodHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "LikelihoodHint");
				}
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00022BEB File Offset: 0x00020DEB
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00022BF3 File Offset: 0x00020DF3
		[DataSourceProperty]
		public int PolicyLikelihood
		{
			get
			{
				return this._policyLikelihood;
			}
			set
			{
				if (value != this._policyLikelihood)
				{
					this._policyLikelihood = value;
					base.OnPropertyChangedWithValue(value, "PolicyLikelihood");
				}
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00022C11 File Offset: 0x00020E11
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x00022C19 File Offset: 0x00020E19
		[DataSourceProperty]
		public string OtherPoliciesText
		{
			get
			{
				return this._otherPoliciesText;
			}
			set
			{
				if (value != this._otherPoliciesText)
				{
					this._otherPoliciesText = value;
					base.OnPropertyChangedWithValue<string>(value, "OtherPoliciesText");
				}
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00022C3C File Offset: 0x00020E3C
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x00022C44 File Offset: 0x00020E44
		[DataSourceProperty]
		public string ProposeOrDisavowText
		{
			get
			{
				return this._proposeOrDisavowText;
			}
			set
			{
				if (value != this._proposeOrDisavowText)
				{
					this._proposeOrDisavowText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProposeOrDisavowText");
				}
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00022C67 File Offset: 0x00020E67
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x00022C6F File Offset: 0x00020E6F
		[DataSourceProperty]
		public string ProposeActionExplanationText
		{
			get
			{
				return this._proposeActionExplanationText;
			}
			set
			{
				if (value != this._proposeActionExplanationText)
				{
					this._proposeActionExplanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProposeActionExplanationText");
				}
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00022C92 File Offset: 0x00020E92
		private static int CalculateLikelihood(PolicyObject policy)
		{
			return MathF.Round(new KingdomElection(new KingdomPolicyDecision(Clan.PlayerClan, policy, Clan.PlayerClan.Kingdom.ActivePolicies.Contains(policy))).GetLikelihoodForSponsor(Clan.PlayerClan) * 100f);
		}

		// Token: 0x0400037D RID: 893
		private readonly Action<KingdomDecision> _forceDecide;

		// Token: 0x0400037E RID: 894
		private readonly Kingdom _playerKingdom;

		// Token: 0x0400037F RID: 895
		private PolicyObject _currentSelectedPolicyObject;

		// Token: 0x04000380 RID: 896
		private KingdomDecision _currentItemsUnresolvedDecision;

		// Token: 0x04000381 RID: 897
		private MBBindingList<KingdomPolicyItemVM> _activePolicies;

		// Token: 0x04000382 RID: 898
		private MBBindingList<KingdomPolicyItemVM> _otherPolicies;

		// Token: 0x04000383 RID: 899
		private KingdomPolicyItemVM _currentSelectedPolicy;

		// Token: 0x04000384 RID: 900
		private bool _canProposeOrDisavowPolicy;

		// Token: 0x04000385 RID: 901
		private bool _isInProposeMode = true;

		// Token: 0x04000386 RID: 902
		private string _proposeOrDisavowText;

		// Token: 0x04000387 RID: 903
		private string _proposeActionExplanationText;

		// Token: 0x04000388 RID: 904
		private string _activePoliciesText;

		// Token: 0x04000389 RID: 905
		private string _otherPoliciesText;

		// Token: 0x0400038A RID: 906
		private string _currentActiveModeText;

		// Token: 0x0400038B RID: 907
		private string _currentActionText;

		// Token: 0x0400038C RID: 908
		private string _proposeNewPolicyText;

		// Token: 0x0400038D RID: 909
		private string _disavowPolicyText;

		// Token: 0x0400038E RID: 910
		private string _policiesText;

		// Token: 0x0400038F RID: 911
		private string _backText;

		// Token: 0x04000390 RID: 912
		private int _proposalAndDisavowalCost;

		// Token: 0x04000391 RID: 913
		private string _numOfActivePoliciesText;

		// Token: 0x04000392 RID: 914
		private string _numOfOtherPoliciesText;

		// Token: 0x04000393 RID: 915
		private HintViewModel _doneHint;

		// Token: 0x04000394 RID: 916
		private string _policyLikelihoodText;

		// Token: 0x04000395 RID: 917
		private HintViewModel _likelihoodHint;

		// Token: 0x04000396 RID: 918
		private int _policyLikelihood;
	}
}
