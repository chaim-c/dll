using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Clans
{
	// Token: 0x02000074 RID: 116
	public class KingdomClanFiefItemVM : ViewModel
	{
		// Token: 0x06000A32 RID: 2610 RVA: 0x00029718 File Offset: 0x00027918
		public KingdomClanFiefItemVM(Settlement settlement)
		{
			this.Settlement = settlement;
			SettlementComponent settlementComponent = settlement.SettlementComponent;
			this.VisualPath = ((settlementComponent == null) ? "placeholder" : (settlementComponent.BackgroundMeshName + "_t"));
			this.RefreshValues();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002975F File Offset: 0x0002795F
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.FiefName = this.Settlement.Name.ToString();
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002977D File Offset: 0x0002797D
		private void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[]
			{
				this.Settlement,
				true
			});
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000297A6 File Offset: 0x000279A6
		private void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000297AD File Offset: 0x000279AD
		public void ExecuteLink()
		{
			if (this.Settlement != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Settlement.EncyclopediaLink);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000297D1 File Offset: 0x000279D1
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x000297D9 File Offset: 0x000279D9
		[DataSourceProperty]
		public string VisualPath
		{
			get
			{
				return this._visualPath;
			}
			set
			{
				if (value != this._visualPath)
				{
					this._visualPath = value;
					base.OnPropertyChanged("FileName");
				}
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000297FB File Offset: 0x000279FB
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00029803 File Offset: 0x00027A03
		[DataSourceProperty]
		public string FiefName
		{
			get
			{
				return this._fiefName;
			}
			set
			{
				if (value != this._fiefName)
				{
					this._fiefName = value;
					base.OnPropertyChangedWithValue<string>(value, "FiefName");
				}
			}
		}

		// Token: 0x04000497 RID: 1175
		private readonly Settlement Settlement;

		// Token: 0x04000498 RID: 1176
		private string _visualPath;

		// Token: 0x04000499 RID: 1177
		private string _fiefName;
	}
}
