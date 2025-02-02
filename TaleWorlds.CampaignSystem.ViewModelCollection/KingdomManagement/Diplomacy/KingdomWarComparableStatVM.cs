using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.KingdomManagement.Diplomacy
{
	// Token: 0x02000064 RID: 100
	public class KingdomWarComparableStatVM : ViewModel
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x00024B38 File Offset: 0x00022D38
		public KingdomWarComparableStatVM(int faction1Stat, int faction2Stat, TextObject name, string faction1Color, string faction2Color, int defaultRange, BasicTooltipViewModel faction1Hint = null, BasicTooltipViewModel faction2Hint = null)
		{
			int num = MathF.Max(MathF.Max(faction1Stat, faction2Stat), defaultRange);
			if (num == 0)
			{
				num = 1;
			}
			this.Faction1Color = faction1Color;
			this.Faction2Color = faction2Color;
			this.Faction1Value = faction1Stat;
			this.Faction2Value = faction2Stat;
			this._defaultRange = defaultRange;
			this.Faction1Percentage = MathF.Round((float)faction1Stat / (float)num * 100f);
			this.Faction2Percentage = MathF.Round((float)faction2Stat / (float)num * 100f);
			this._nameObj = name;
			this.Faction1Hint = faction1Hint;
			this.Faction2Hint = faction2Hint;
			this.RefreshValues();
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00024BCE File Offset: 0x00022DCE
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._nameObj.ToString();
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00024BE7 File Offset: 0x00022DE7
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00024BEF File Offset: 0x00022DEF
		[DataSourceProperty]
		public BasicTooltipViewModel Faction1Hint
		{
			get
			{
				return this._faction1Hint;
			}
			set
			{
				if (value != this._faction1Hint)
				{
					this._faction1Hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Faction1Hint");
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00024C0D File Offset: 0x00022E0D
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00024C15 File Offset: 0x00022E15
		[DataSourceProperty]
		public BasicTooltipViewModel Faction2Hint
		{
			get
			{
				return this._faction2Hint;
			}
			set
			{
				if (value != this._faction2Hint)
				{
					this._faction2Hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Faction2Hint");
				}
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00024C33 File Offset: 0x00022E33
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00024C3B File Offset: 0x00022E3B
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

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00024C5E File Offset: 0x00022E5E
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x00024C66 File Offset: 0x00022E66
		[DataSourceProperty]
		public string Faction1Color
		{
			get
			{
				return this._faction1Color;
			}
			set
			{
				if (value != this._faction1Color)
				{
					this._faction1Color = value;
					base.OnPropertyChangedWithValue<string>(value, "Faction1Color");
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00024C89 File Offset: 0x00022E89
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x00024C91 File Offset: 0x00022E91
		[DataSourceProperty]
		public string Faction2Color
		{
			get
			{
				return this._faction2Color;
			}
			set
			{
				if (value != this._faction2Color)
				{
					this._faction2Color = value;
					base.OnPropertyChangedWithValue<string>(value, "Faction2Color");
				}
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00024CB4 File Offset: 0x00022EB4
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00024CBC File Offset: 0x00022EBC
		[DataSourceProperty]
		public int Faction1Percentage
		{
			get
			{
				return this._faction1Percentage;
			}
			set
			{
				if (value != this._faction1Percentage)
				{
					this._faction1Percentage = value;
					base.OnPropertyChangedWithValue(value, "Faction1Percentage");
				}
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00024CDA File Offset: 0x00022EDA
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00024CE2 File Offset: 0x00022EE2
		[DataSourceProperty]
		public int Faction1Value
		{
			get
			{
				return this._faction1Value;
			}
			set
			{
				if (value != this._faction1Value)
				{
					this._faction1Value = value;
					base.OnPropertyChangedWithValue(value, "Faction1Value");
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00024D00 File Offset: 0x00022F00
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00024D08 File Offset: 0x00022F08
		[DataSourceProperty]
		public int Faction2Percentage
		{
			get
			{
				return this._faction2Percentage;
			}
			set
			{
				if (value != this._faction2Percentage)
				{
					this._faction2Percentage = value;
					base.OnPropertyChangedWithValue(value, "Faction2Percentage");
				}
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00024D26 File Offset: 0x00022F26
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00024D2E File Offset: 0x00022F2E
		[DataSourceProperty]
		public int Faction2Value
		{
			get
			{
				return this._faction2Value;
			}
			set
			{
				if (value != this._faction2Value)
				{
					this._faction2Value = value;
					base.OnPropertyChangedWithValue(value, "Faction2Value");
				}
			}
		}

		// Token: 0x040003D8 RID: 984
		private TextObject _nameObj;

		// Token: 0x040003D9 RID: 985
		private int _defaultRange;

		// Token: 0x040003DA RID: 986
		private BasicTooltipViewModel _faction1Hint;

		// Token: 0x040003DB RID: 987
		private BasicTooltipViewModel _faction2Hint;

		// Token: 0x040003DC RID: 988
		private string _name;

		// Token: 0x040003DD RID: 989
		private string _faction1Color;

		// Token: 0x040003DE RID: 990
		private string _faction2Color;

		// Token: 0x040003DF RID: 991
		private int _faction1Percentage;

		// Token: 0x040003E0 RID: 992
		private int _faction1Value;

		// Token: 0x040003E1 RID: 993
		private int _faction2Percentage;

		// Token: 0x040003E2 RID: 994
		private int _faction2Value;
	}
}
