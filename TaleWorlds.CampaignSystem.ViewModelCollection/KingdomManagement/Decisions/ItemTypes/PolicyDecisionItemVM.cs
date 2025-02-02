using System;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions.ItemTypes
{
	// Token: 0x02000072 RID: 114
	public class PolicyDecisionItemVM : DecisionItemBaseVM
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public KingdomPolicyDecision PolicyDecision
		{
			get
			{
				KingdomPolicyDecision result;
				if ((result = this._policyDecision) == null)
				{
					result = (this._policyDecision = (this._decision as KingdomPolicyDecision));
				}
				return result;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00028B0B File Offset: 0x00026D0B
		public PolicyObject Policy
		{
			get
			{
				return this.PolicyDecision.Policy;
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00028B18 File Offset: 0x00026D18
		public PolicyDecisionItemVM(KingdomPolicyDecision decision, Action onDecisionOver) : base(decision, onDecisionOver)
		{
			base.DecisionType = 3;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00028B2C File Offset: 0x00026D2C
		protected override void InitValues()
		{
			base.InitValues();
			base.DecisionType = 3;
			this.NameText = this.Policy.Name.ToString();
			this.PolicyDescriptionText = this.Policy.Description.ToString();
			this.PolicyEffectList = new MBBindingList<StringItemWithHintVM>();
			foreach (string text in this.Policy.SecondaryEffects.ToString().Split(new char[]
			{
				'\n'
			}))
			{
				this.PolicyEffectList.Add(new StringItemWithHintVM(text, TextObject.Empty));
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00028BC6 File Offset: 0x00026DC6
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00028BCE File Offset: 0x00026DCE
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00028BF1 File Offset: 0x00026DF1
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00028BF9 File Offset: 0x00026DF9
		[DataSourceProperty]
		public string PolicyDescriptionText
		{
			get
			{
				return this._policyDescriptionText;
			}
			set
			{
				if (value != this._policyDescriptionText)
				{
					this._policyDescriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "PolicyDescriptionText");
				}
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00028C1C File Offset: 0x00026E1C
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00028C24 File Offset: 0x00026E24
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

		// Token: 0x04000471 RID: 1137
		private KingdomPolicyDecision _policyDecision;

		// Token: 0x04000472 RID: 1138
		private MBBindingList<StringItemWithHintVM> _policyEffectList;

		// Token: 0x04000473 RID: 1139
		private string _nameText;

		// Token: 0x04000474 RID: 1140
		private string _policyDescriptionText;
	}
}
