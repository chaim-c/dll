using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000034 RID: 52
	public class MapSiegeProductionMachineVM : ViewModel
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000122D7 File Offset: 0x000104D7
		public SiegeEngineType Engine { get; }

		// Token: 0x060003EC RID: 1004 RVA: 0x000122DF File Offset: 0x000104DF
		public MapSiegeProductionMachineVM(SiegeEngineType engineType, int number, Action<MapSiegeProductionMachineVM> onSelection)
		{
			this._onSelection = onSelection;
			this.Engine = engineType;
			this.NumberOfMachines = number;
			this.MachineID = engineType.StringId;
			this.IsReserveOption = false;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001230F File Offset: 0x0001050F
		public MapSiegeProductionMachineVM(Action<MapSiegeProductionMachineVM> onSelection, bool isCancel)
		{
			this._onSelection = onSelection;
			this.Engine = null;
			this.NumberOfMachines = 0;
			this.MachineID = "reserve";
			this.IsReserveOption = true;
			this._isCancel = isCancel;
			this.RefreshValues();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001234B File Offset: 0x0001054B
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ActionText = (this._isCancel ? GameTexts.FindText("str_cancel", null).ToString() : GameTexts.FindText("str_siege_move_to_reserve", null).ToString());
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00012383 File Offset: 0x00010583
		public void OnSelection()
		{
			this._onSelection(this);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00012391 File Offset: 0x00010591
		public void ExecuteShowTooltip()
		{
			if (this.Engine != null)
			{
				InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
				{
					SandBoxUIHelper.GetSiegeEngineTooltip(this.Engine)
				});
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000123BE File Offset: 0x000105BE
		public void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000123C5 File Offset: 0x000105C5
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x000123CD File Offset: 0x000105CD
		[DataSourceProperty]
		public int MachineType
		{
			get
			{
				return this._machineType;
			}
			set
			{
				if (value != this._machineType)
				{
					this._machineType = value;
					base.OnPropertyChangedWithValue(value, "MachineType");
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000123EB File Offset: 0x000105EB
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000123F3 File Offset: 0x000105F3
		[DataSourceProperty]
		public string MachineID
		{
			get
			{
				return this._machineID;
			}
			set
			{
				if (value != this._machineID)
				{
					this._machineID = value;
					base.OnPropertyChangedWithValue<string>(value, "MachineID");
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00012416 File Offset: 0x00010616
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0001241E File Offset: 0x0001061E
		[DataSourceProperty]
		public int NumberOfMachines
		{
			get
			{
				return this._numberOfMachines;
			}
			set
			{
				if (value != this._numberOfMachines)
				{
					this._numberOfMachines = value;
					base.OnPropertyChangedWithValue(value, "NumberOfMachines");
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0001243C File Offset: 0x0001063C
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00012444 File Offset: 0x00010644
		[DataSourceProperty]
		public string ActionText
		{
			get
			{
				return this._actionText;
			}
			set
			{
				if (value != this._actionText)
				{
					this._actionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionText");
				}
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00012467 File Offset: 0x00010667
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0001246F File Offset: 0x0001066F
		[DataSourceProperty]
		public bool IsReserveOption
		{
			get
			{
				return this._isReserveOption;
			}
			set
			{
				if (value != this._isReserveOption)
				{
					this._isReserveOption = value;
					base.OnPropertyChangedWithValue(value, "IsReserveOption");
				}
			}
		}

		// Token: 0x04000210 RID: 528
		private Action<MapSiegeProductionMachineVM> _onSelection;

		// Token: 0x04000212 RID: 530
		private bool _isCancel;

		// Token: 0x04000213 RID: 531
		private int _machineType;

		// Token: 0x04000214 RID: 532
		private int _numberOfMachines;

		// Token: 0x04000215 RID: 533
		private string _machineID;

		// Token: 0x04000216 RID: 534
		private bool _isReserveOption;

		// Token: 0x04000217 RID: 535
		private string _actionText;
	}
}
