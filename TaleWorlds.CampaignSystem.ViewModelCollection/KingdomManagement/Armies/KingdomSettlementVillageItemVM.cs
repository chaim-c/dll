using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Armies
{
	// Token: 0x0200007C RID: 124
	public class KingdomSettlementVillageItemVM : ViewModel
	{
		// Token: 0x06000B26 RID: 2854 RVA: 0x0002C155 File Offset: 0x0002A355
		public KingdomSettlementVillageItemVM(Village village)
		{
			this._village = village;
			this.VisualPath = village.BackgroundMeshName + "_t";
			this.RefreshValues();
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002C180 File Offset: 0x0002A380
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._village.Name.ToString();
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002C19E File Offset: 0x0002A39E
		private void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(Settlement), new object[]
			{
				this._village.Settlement,
				true
			});
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002C1CC File Offset: 0x0002A3CC
		private void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002C1D3 File Offset: 0x0002A3D3
		public void ExecuteLink()
		{
			if (this._village != null && this._village.Settlement != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._village.Settlement.EncyclopediaLink);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002C209 File Offset: 0x0002A409
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0002C211 File Offset: 0x0002A411
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0002C22F File Offset: 0x0002A42F
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0002C237 File Offset: 0x0002A437
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

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002C25A File Offset: 0x0002A45A
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0002C262 File Offset: 0x0002A462
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
					base.OnPropertyChangedWithValue<string>(value, "VisualPath");
				}
			}
		}

		// Token: 0x0400050B RID: 1291
		private Village _village;

		// Token: 0x0400050C RID: 1292
		private ImageIdentifierVM _visual;

		// Token: 0x0400050D RID: 1293
		private string _name;

		// Token: 0x0400050E RID: 1294
		private string _visualPath;
	}
}
