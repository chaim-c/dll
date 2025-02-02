using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries
{
	// Token: 0x02000038 RID: 56
	public abstract class PopUpBaseVM : ViewModel
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00015D90 File Offset: 0x00013F90
		public PopUpBaseVM(Action closeQuery)
		{
			this._closeQuery = closeQuery;
		}

		// Token: 0x060004EC RID: 1260
		public abstract void ExecuteAffirmativeAction();

		// Token: 0x060004ED RID: 1261
		public abstract void ExecuteNegativeAction();

		// Token: 0x060004EE RID: 1262 RVA: 0x00015D9F File Offset: 0x00013F9F
		public virtual void OnTick(float dt)
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00015DA1 File Offset: 0x00013FA1
		public virtual void OnClearData()
		{
			this.TitleText = null;
			this.PopUpLabel = null;
			this.ButtonOkLabel = null;
			this.ButtonCancelLabel = null;
			this.IsButtonOkShown = false;
			this.IsButtonCancelShown = false;
			this.IsButtonOkEnabled = false;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015DD4 File Offset: 0x00013FD4
		public void ForceRefreshKeyVisuals()
		{
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.RefreshValues();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey == null)
			{
				return;
			}
			doneInputKey.RefreshValues();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00015DF7 File Offset: 0x00013FF7
		public void CloseQuery()
		{
			Action closeQuery = this._closeQuery;
			if (closeQuery == null)
			{
				return;
			}
			closeQuery();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00015E09 File Offset: 0x00014009
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey == null)
			{
				return;
			}
			cancelInputKey.OnFinalize();
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00015E32 File Offset: 0x00014032
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00015E3A File Offset: 0x0001403A
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00015E5D File Offset: 0x0001405D
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00015E65 File Offset: 0x00014065
		[DataSourceProperty]
		public string PopUpLabel
		{
			get
			{
				return this._popUpLabel;
			}
			set
			{
				if (value != this._popUpLabel)
				{
					this._popUpLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "PopUpLabel");
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00015E88 File Offset: 0x00014088
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00015E90 File Offset: 0x00014090
		[DataSourceProperty]
		public string ButtonOkLabel
		{
			get
			{
				return this._buttonOkLabel;
			}
			set
			{
				if (value != this._buttonOkLabel)
				{
					this._buttonOkLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "ButtonOkLabel");
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00015EB3 File Offset: 0x000140B3
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00015EBB File Offset: 0x000140BB
		[DataSourceProperty]
		public string ButtonCancelLabel
		{
			get
			{
				return this._buttonCancelLabel;
			}
			set
			{
				if (value != this._buttonCancelLabel)
				{
					this._buttonCancelLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "ButtonCancelLabel");
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00015EDE File Offset: 0x000140DE
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x00015EE6 File Offset: 0x000140E6
		[DataSourceProperty]
		public bool IsButtonOkShown
		{
			get
			{
				return this._isButtonOkShown;
			}
			set
			{
				if (value != this._isButtonOkShown)
				{
					this._isButtonOkShown = value;
					base.OnPropertyChangedWithValue(value, "IsButtonOkShown");
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00015F04 File Offset: 0x00014104
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00015F0C File Offset: 0x0001410C
		[DataSourceProperty]
		public bool IsButtonCancelShown
		{
			get
			{
				return this._isButtonCancelShown;
			}
			set
			{
				if (value != this._isButtonCancelShown)
				{
					this._isButtonCancelShown = value;
					base.OnPropertyChangedWithValue(value, "IsButtonCancelShown");
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00015F2A File Offset: 0x0001412A
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00015F32 File Offset: 0x00014132
		[DataSourceProperty]
		public bool IsButtonOkEnabled
		{
			get
			{
				return this._isButtonOkEnabled;
			}
			set
			{
				if (value != this._isButtonOkEnabled)
				{
					this._isButtonOkEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsButtonOkEnabled");
				}
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00015F50 File Offset: 0x00014150
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x00015F58 File Offset: 0x00014158
		[DataSourceProperty]
		public bool IsButtonCancelEnabled
		{
			get
			{
				return this._isButtonCancelEnabled;
			}
			set
			{
				if (value != this._isButtonCancelEnabled)
				{
					this._isButtonCancelEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsButtonCancelEnabled");
				}
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00015F76 File Offset: 0x00014176
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00015F7E File Offset: 0x0001417E
		[DataSourceProperty]
		public HintViewModel ButtonOkHint
		{
			get
			{
				return this._buttonOkHint;
			}
			set
			{
				if (value != this._buttonOkHint)
				{
					this._buttonOkHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ButtonOkHint");
				}
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00015F9C File Offset: 0x0001419C
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00015FA4 File Offset: 0x000141A4
		[DataSourceProperty]
		public HintViewModel ButtonCancelHint
		{
			get
			{
				return this._buttonCancelHint;
			}
			set
			{
				if (value != this._buttonCancelHint)
				{
					this._buttonCancelHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ButtonCancelHint");
				}
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00015FC2 File Offset: 0x000141C2
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015FD1 File Offset: 0x000141D1
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00015FE0 File Offset: 0x000141E0
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00015FE8 File Offset: 0x000141E8
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00016006 File Offset: 0x00014206
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0001600E File Offset: 0x0001420E
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x04000265 RID: 613
		protected Action _affirmativeAction;

		// Token: 0x04000266 RID: 614
		protected Action _negativeAction;

		// Token: 0x04000267 RID: 615
		private Action _closeQuery;

		// Token: 0x04000268 RID: 616
		private string _titleText;

		// Token: 0x04000269 RID: 617
		private string _popUpLabel;

		// Token: 0x0400026A RID: 618
		private string _buttonOkLabel;

		// Token: 0x0400026B RID: 619
		private string _buttonCancelLabel;

		// Token: 0x0400026C RID: 620
		private bool _isButtonOkShown;

		// Token: 0x0400026D RID: 621
		private bool _isButtonCancelShown;

		// Token: 0x0400026E RID: 622
		private bool _isButtonOkEnabled;

		// Token: 0x0400026F RID: 623
		private bool _isButtonCancelEnabled;

		// Token: 0x04000270 RID: 624
		private HintViewModel _buttonOkHint;

		// Token: 0x04000271 RID: 625
		private HintViewModel _buttonCancelHint;

		// Token: 0x04000272 RID: 626
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000273 RID: 627
		private InputKeyItemVM _doneInputKey;
	}
}
