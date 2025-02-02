using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement
{
	// Token: 0x0200013C RID: 316
	public class ArmyManagementBoostEventVM : ViewModel
	{
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0006DA93 File Offset: 0x0006BC93
		public ArmyManagementBoostEventVM.BoostCurrency CurrencyToPayForCohesion { get; }

		// Token: 0x06001EBD RID: 7869 RVA: 0x0006DA9B File Offset: 0x0006BC9B
		public ArmyManagementBoostEventVM(ArmyManagementBoostEventVM.BoostCurrency currencyToPayForCohesion, int amountToPay, int amountOfCohesionToGain, Action<ArmyManagementBoostEventVM> onExecuteEvent)
		{
			this.IsEnabled = true;
			this._onExecuteEvent = onExecuteEvent;
			this.AmountToPay = amountToPay;
			this.AmountOfCohesionToGain = amountOfCohesionToGain;
			this.CurrencyToPayForCohesion = currencyToPayForCohesion;
			this.CurrencyType = (int)currencyToPayForCohesion;
			this.RefreshValues();
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0006DAD4 File Offset: 0x0006BCD4
		public override void RefreshValues()
		{
			base.RefreshValues();
			GameTexts.SetVariable("AMOUNT", this.AmountToPay);
			this.SpendText = GameTexts.FindText("str_cohesion_boost_spend", null).ToString();
			GameTexts.SetVariable("GAIN_AMOUNT", this.AmountOfCohesionToGain);
			this.GainText = GameTexts.FindText("str_cohesion_boost_gain", null).ToString();
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0006DB33 File Offset: 0x0006BD33
		private void ExecuteEvent()
		{
			this._onExecuteEvent(this);
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0006DB41 File Offset: 0x0006BD41
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x0006DB49 File Offset: 0x0006BD49
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

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0006DB67 File Offset: 0x0006BD67
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x0006DB6F File Offset: 0x0006BD6F
		[DataSourceProperty]
		public int AmountToPay
		{
			get
			{
				return this._amountToPay;
			}
			set
			{
				if (value != this._amountToPay)
				{
					this._amountToPay = value;
					base.OnPropertyChangedWithValue(value, "AmountToPay");
				}
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x0006DB8D File Offset: 0x0006BD8D
		// (set) Token: 0x06001EC5 RID: 7877 RVA: 0x0006DB95 File Offset: 0x0006BD95
		[DataSourceProperty]
		public int CurrencyType
		{
			get
			{
				return this._currencyType;
			}
			set
			{
				if (value != this._currencyType)
				{
					this._currencyType = value;
					base.OnPropertyChangedWithValue(value, "CurrencyType");
				}
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0006DBB3 File Offset: 0x0006BDB3
		// (set) Token: 0x06001EC7 RID: 7879 RVA: 0x0006DBBB File Offset: 0x0006BDBB
		[DataSourceProperty]
		public int AmountOfCohesionToGain
		{
			get
			{
				return this._amountOfCohesionToGain;
			}
			set
			{
				if (value != this._amountOfCohesionToGain)
				{
					this._amountOfCohesionToGain = value;
					base.OnPropertyChangedWithValue(value, "AmountOfCohesionToGain");
				}
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0006DBD9 File Offset: 0x0006BDD9
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x0006DBE1 File Offset: 0x0006BDE1
		[DataSourceProperty]
		public string SpendText
		{
			get
			{
				return this._spendText;
			}
			set
			{
				if (value != this._spendText)
				{
					this._spendText = value;
					base.OnPropertyChangedWithValue<string>(value, "SpendText");
				}
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x0006DC04 File Offset: 0x0006BE04
		// (set) Token: 0x06001ECB RID: 7883 RVA: 0x0006DC0C File Offset: 0x0006BE0C
		[DataSourceProperty]
		public string GainText
		{
			get
			{
				return this._gainText;
			}
			set
			{
				if (value != this._gainText)
				{
					this._gainText = value;
					base.OnPropertyChangedWithValue<string>(value, "GainText");
				}
			}
		}

		// Token: 0x04000E76 RID: 3702
		private readonly Action<ArmyManagementBoostEventVM> _onExecuteEvent;

		// Token: 0x04000E77 RID: 3703
		private int _amountToPay;

		// Token: 0x04000E78 RID: 3704
		private int _amountOfCohesionToGain;

		// Token: 0x04000E79 RID: 3705
		private int _currencyType;

		// Token: 0x04000E7A RID: 3706
		private string _spendText;

		// Token: 0x04000E7B RID: 3707
		private string _gainText;

		// Token: 0x04000E7C RID: 3708
		private bool _isEnabled;

		// Token: 0x020002A3 RID: 675
		public enum BoostCurrency
		{
			// Token: 0x04001253 RID: 4691
			Gold,
			// Token: 0x04001254 RID: 4692
			Influence
		}
	}
}
