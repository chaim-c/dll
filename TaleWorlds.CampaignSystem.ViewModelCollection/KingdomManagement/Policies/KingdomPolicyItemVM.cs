using System;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Policies
{
	// Token: 0x0200005F RID: 95
	public class KingdomPolicyItemVM : KingdomItemVM
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00022D0C File Offset: 0x00020F0C
		public KingdomPolicyItemVM(PolicyObject policy, Action<KingdomPolicyItemVM> onSelect, Func<PolicyObject, bool> getIsPolicyActive)
		{
			this._onSelect = onSelect;
			this._policy = policy;
			this._getIsPolicyActive = getIsPolicyActive;
			this.Name = policy.Name.ToString();
			this.Explanation = policy.Description.ToString();
			this.LikelihoodHint = new HintViewModel();
			this.PolicyEffectList = new MBBindingList<StringItemWithHintVM>();
			foreach (string text in policy.SecondaryEffects.ToString().Split(new char[]
			{
				'\n'
			}))
			{
				this.PolicyEffectList.Add(new StringItemWithHintVM(text, TextObject.Empty));
			}
			this.RefreshValues();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00022DB8 File Offset: 0x00020FB8
		public override void RefreshValues()
		{
			base.RefreshValues();
			Func<PolicyObject, bool> getIsPolicyActive = this._getIsPolicyActive;
			this.PolicyAcceptanceText = ((getIsPolicyActive != null && getIsPolicyActive(this.Policy)) ? GameTexts.FindText("str_policy_support_for_abolishing", null).ToString() : GameTexts.FindText("str_policy_support_for_enacting", null).ToString());
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00022E10 File Offset: 0x00021010
		private void DeterminePolicyLikelihood()
		{
			float likelihoodForSponsor = new KingdomElection(new KingdomPolicyDecision(Clan.PlayerClan, this._policy, false)).GetLikelihoodForSponsor(Clan.PlayerClan);
			this.PolicyLikelihood = MathF.Round(likelihoodForSponsor * 100f);
			GameTexts.SetVariable("NUMBER", this.PolicyLikelihood);
			this.PolicyLikelihoodText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00022E76 File Offset: 0x00021076
		protected override void OnSelect()
		{
			base.OnSelect();
			this._onSelect(this);
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00022E8A File Offset: 0x0002108A
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00022E92 File Offset: 0x00021092
		[DataSourceProperty]
		public string PolicyAcceptanceText
		{
			get
			{
				return this._policyAcceptanceText;
			}
			set
			{
				if (value != this._policyAcceptanceText)
				{
					this._policyAcceptanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "PolicyAcceptanceText");
				}
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00022EB5 File Offset: 0x000210B5
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00022EBD File Offset: 0x000210BD
		[DataSourceProperty]
		public MBBindingList<StringItemWithHintVM> PolicyEffectList
		{
			get
			{
				return this._policyEffectList;
			}
			set
			{
				if (value != this._policyEffectList)
				{
					this._policyEffectList = value;
					base.OnPropertyChangedWithValue<MBBindingList<StringItemWithHintVM>>(value, "PolicyEffectList");
				}
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00022EDB File Offset: 0x000210DB
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00022EE3 File Offset: 0x000210E3
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00022F06 File Offset: 0x00021106
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00022F0E File Offset: 0x0002110E
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00022F2C File Offset: 0x0002112C
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00022F34 File Offset: 0x00021134
		[DataSourceProperty]
		public PolicyObject Policy
		{
			get
			{
				return this._policy;
			}
			set
			{
				if (value != this._policy)
				{
					this._policy = value;
					base.OnPropertyChangedWithValue<PolicyObject>(value, "Policy");
				}
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00022F52 File Offset: 0x00021152
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x00022F5A File Offset: 0x0002115A
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00022F78 File Offset: 0x00021178
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x00022F80 File Offset: 0x00021180
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

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00022FA3 File Offset: 0x000211A3
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x00022FAB File Offset: 0x000211AB
		[DataSourceProperty]
		public string Explanation
		{
			get
			{
				return this._explanation;
			}
			set
			{
				if (value != this._explanation)
				{
					this._explanation = value;
					base.OnPropertyChangedWithValue<string>(value, "Explanation");
				}
			}
		}

		// Token: 0x04000397 RID: 919
		private readonly Action<KingdomPolicyItemVM> _onSelect;

		// Token: 0x04000398 RID: 920
		private readonly Func<PolicyObject, bool> _getIsPolicyActive;

		// Token: 0x04000399 RID: 921
		private string _name;

		// Token: 0x0400039A RID: 922
		private string _explanation;

		// Token: 0x0400039B RID: 923
		private string _policyAcceptanceText;

		// Token: 0x0400039C RID: 924
		private PolicyObject _policy;

		// Token: 0x0400039D RID: 925
		private int _policyLikelihood;

		// Token: 0x0400039E RID: 926
		private string _policyLikelihoodText;

		// Token: 0x0400039F RID: 927
		private HintViewModel _likelihoodHint;

		// Token: 0x040003A0 RID: 928
		private MBBindingList<StringItemWithHintVM> _policyEffectList;
	}
}
