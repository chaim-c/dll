using System;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Decisions
{
	// Token: 0x0200006A RID: 106
	public class DecisionSupporterVM : ViewModel
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x00025FF0 File Offset: 0x000241F0
		public DecisionSupporterVM(TextObject name, string imagePath, Clan clan, Supporter.SupportWeights weight)
		{
			this._nameObj = name;
			this._clan = clan;
			this._weight = weight;
			this.SupportWeightImagePath = DecisionSupporterVM.GetSupporterWeightImagePath(weight);
			this.RefreshValues();
			this._hero = Hero.FindFirst((Hero H) => H.Name == name);
			if (this._hero != null)
			{
				this.Visual = new ImageIdentifierVM(CampaignUIHelper.GetCharacterCode(this._hero.CharacterObject, false));
				return;
			}
			this.Visual = new ImageIdentifierVM(ImageIdentifierType.Null);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00026086 File Offset: 0x00024286
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._nameObj.ToString();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002609F File Offset: 0x0002429F
		private void ExecuteBeginHint()
		{
			if (this._hero != null)
			{
				InformationManager.ShowTooltip(typeof(Hero), new object[]
				{
					this._hero,
					false
				});
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000260D0 File Offset: 0x000242D0
		private void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000260D7 File Offset: 0x000242D7
		internal static string GetSupporterWeightImagePath(Supporter.SupportWeights weight)
		{
			switch (weight)
			{
			case Supporter.SupportWeights.SlightlyFavor:
				return "SPKingdom\\voter_strength1";
			case Supporter.SupportWeights.StronglyFavor:
				return "SPKingdom\\voter_strength2";
			case Supporter.SupportWeights.FullyPush:
				return "SPKingdom\\voter_strength3";
			}
			return string.Empty;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0002610C File Offset: 0x0002430C
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00026114 File Offset: 0x00024314
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00026132 File Offset: 0x00024332
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0002613A File Offset: 0x0002433A
		[DataSourceProperty]
		public int SupportStrength
		{
			get
			{
				return this._supportStrength;
			}
			set
			{
				if (value != this._supportStrength)
				{
					this._supportStrength = value;
					base.OnPropertyChangedWithValue(value, "SupportStrength");
				}
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00026158 File Offset: 0x00024358
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x00026160 File Offset: 0x00024360
		[DataSourceProperty]
		public string SupportWeightImagePath
		{
			get
			{
				return this._supportWeightImagePath;
			}
			set
			{
				if (value != this._supportWeightImagePath)
				{
					this._supportWeightImagePath = value;
					base.OnPropertyChangedWithValue<string>(value, "SupportWeightImagePath");
				}
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00026183 File Offset: 0x00024383
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0002618B File Offset: 0x0002438B
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
					base.OnPropertyChanged("string");
				}
			}
		}

		// Token: 0x0400041C RID: 1052
		private Supporter.SupportWeights _weight;

		// Token: 0x0400041D RID: 1053
		private Clan _clan;

		// Token: 0x0400041E RID: 1054
		private TextObject _nameObj;

		// Token: 0x0400041F RID: 1055
		private Hero _hero;

		// Token: 0x04000420 RID: 1056
		private ImageIdentifierVM _visual;

		// Token: 0x04000421 RID: 1057
		private string _name;

		// Token: 0x04000422 RID: 1058
		private int _supportStrength;

		// Token: 0x04000423 RID: 1059
		private string _supportWeightImagePath;
	}
}
