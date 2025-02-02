using System;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party.PartyTroopManagerPopUp
{
	// Token: 0x0200002F RID: 47
	public class PartyRecruitTroopVM : PartyTroopManagerVM
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x000193A4 File Offset: 0x000175A4
		public PartyRecruitTroopVM(PartyVM partyVM) : base(partyVM)
		{
			this.RefreshValues();
			base.IsUpgradePopUp = false;
			this._openButtonEnabledHint = new TextObject("{=tnbCJyax}Some of your prisoners are recruitable.", null);
			this._openButtonNoTroopsHint = new TextObject("{=1xf8rHLH}You don't have any recruitable prisoners.", null);
			this._openButtonIrrelevantScreenHint = new TextObject("{=zduu7dpz}Prisoners are not recruitable in this screen.", null);
			this._openButtonUpgradesDisabledHint = new TextObject("{=HfsUngkh}Recruitment is currently disabled.", null);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001940C File Offset: 0x0001760C
		public override void RefreshValues()
		{
			base.RefreshValues();
			base.TitleText = new TextObject("{=b8CqpGHx}Recruit Prisoners", null).ToString();
			this.EffectText = new TextObject("{=opVqBNLh}Effect", null).ToString();
			this.RecruitText = new TextObject("{=recruitVerb}Recruit", null).ToString();
			this.RecruitAllText = new TextObject("{=YJaNtktT}Recruit All", null).ToString();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00019478 File Offset: 0x00017678
		public void OnTroopRecruited(PartyCharacterVM recruitedCharacter)
		{
			if (!base.IsOpen)
			{
				return;
			}
			this._hasMadeChanges = true;
			PartyTroopManagerItemVM item = base.Troops.FirstOrDefault((PartyTroopManagerItemVM x) => x.PartyCharacter == recruitedCharacter);
			recruitedCharacter.UpdateRecruitable();
			if (!recruitedCharacter.IsTroopRecruitable)
			{
				base.Troops.Remove(item);
			}
			base.UpdateLabels();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000194E5 File Offset: 0x000176E5
		public override void OpenPopUp()
		{
			base.OpenPopUp();
			this.PopulateTroops();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000194F3 File Offset: 0x000176F3
		public override void ExecuteDone()
		{
			base.ExecuteDone();
			this._partyVM.OnRecruitPopUpClosed(false);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00019507 File Offset: 0x00017707
		public override void ExecuteCancel()
		{
			base.ShowCancelInquiry(new Action(this.ConfirmCancel));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001951C File Offset: 0x0001771C
		protected override void ConfirmCancel()
		{
			base.ConfirmCancel();
			this._partyVM.OnRecruitPopUpClosed(true);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00019530 File Offset: 0x00017730
		private void PopulateTroops()
		{
			base.Troops = new MBBindingList<PartyTroopManagerItemVM>();
			foreach (PartyCharacterVM partyCharacterVM in this._partyVM.MainPartyPrisoners)
			{
				if (partyCharacterVM.IsTroopRecruitable)
				{
					base.Troops.Add(new PartyTroopManagerItemVM(partyCharacterVM, new Action<PartyTroopManagerItemVM>(base.SetFocusedCharacter)));
				}
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000195AC File Offset: 0x000177AC
		public override void ExecuteItemPrimaryAction()
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000195AE File Offset: 0x000177AE
		public override void ExecuteItemSecondaryAction()
		{
			PartyTroopManagerItemVM focusedTroop = base.FocusedTroop;
			if (focusedTroop == null)
			{
				return;
			}
			focusedTroop.PartyCharacter.ExecuteRecruitTroop();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000195C8 File Offset: 0x000177C8
		public void ExecuteRecruitAll()
		{
			for (int i = base.Troops.Count - 1; i >= 0; i--)
			{
				PartyCharacterVM partyCharacter = base.Troops[i].PartyCharacter;
				if (partyCharacter != null)
				{
					partyCharacter.RecruitAll();
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00019609 File Offset: 0x00017809
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00019611 File Offset: 0x00017811
		[DataSourceProperty]
		public string EffectText
		{
			get
			{
				return this._effectText;
			}
			set
			{
				if (value != this._effectText)
				{
					this._effectText = value;
					base.OnPropertyChangedWithValue<string>(value, "EffectText");
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00019634 File Offset: 0x00017834
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0001963C File Offset: 0x0001783C
		[DataSourceProperty]
		public string RecruitText
		{
			get
			{
				return this._recruitText;
			}
			set
			{
				if (value != this._recruitText)
				{
					this._recruitText = value;
					base.OnPropertyChangedWithValue<string>(value, "RecruitText");
				}
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001965F File Offset: 0x0001785F
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00019667 File Offset: 0x00017867
		[DataSourceProperty]
		public string RecruitAllText
		{
			get
			{
				return this._recruitAllText;
			}
			set
			{
				if (value != this._recruitAllText)
				{
					this._recruitAllText = value;
					base.OnPropertyChangedWithValue<string>(value, "RecruitAllText");
				}
			}
		}

		// Token: 0x040001FB RID: 507
		private string _effectText;

		// Token: 0x040001FC RID: 508
		private string _recruitText;

		// Token: 0x040001FD RID: 509
		private string _recruitAllText;
	}
}
