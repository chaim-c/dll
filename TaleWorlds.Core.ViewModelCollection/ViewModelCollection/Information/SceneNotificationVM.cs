using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x0200001A RID: 26
	public class SceneNotificationVM : ViewModel
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000047B8 File Offset: 0x000029B8
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000047C0 File Offset: 0x000029C0
		public SceneNotificationData ActiveData { get; private set; }

		// Token: 0x06000141 RID: 321 RVA: 0x000047C9 File Offset: 0x000029C9
		public SceneNotificationVM(Action onPositiveTrigger, Action closeNotification, Func<string> getContinueInputText)
		{
			this._onPositiveTrigger = onPositiveTrigger;
			this._closeNotification = closeNotification;
			this._getContinueInputText = getContinueInputText;
			this.IsShown = false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000047ED File Offset: 0x000029ED
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ClickToContinueText = this._getContinueInputText();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004806 File Offset: 0x00002A06
		public void CreateNotification(SceneNotificationData data)
		{
			this.SetData(data);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004810 File Offset: 0x00002A10
		public void ForceClose()
		{
			this.IsShown = false;
			base.OnPropertyChanged("TitleText");
			base.OnPropertyChanged("AffirmativeDescription");
			base.OnPropertyChanged("CancelDescription");
			base.OnPropertyChanged("SceneID");
			base.OnPropertyChanged("IsAffirmativeOptionShown");
			base.OnPropertyChanged("IsNegativeOptionShown");
			base.OnPropertyChanged("AffirmativeText");
			base.OnPropertyChanged("NegativeText");
			base.OnPropertyChanged("AffirmativeAction");
			base.OnPropertyChanged("NegativeAction");
			base.OnPropertyChanged("AffirmativeTitleText");
			base.OnPropertyChanged("NegativeTitleText");
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000048A8 File Offset: 0x00002AA8
		private void SetData(SceneNotificationData data)
		{
			this.ActiveData = data;
			base.OnPropertyChanged("TitleText");
			base.OnPropertyChanged("AffirmativeDescription");
			base.OnPropertyChanged("CancelDescription");
			base.OnPropertyChanged("SceneID");
			base.OnPropertyChanged("IsButtonOkShown");
			base.OnPropertyChanged("IsButtonCancelShown");
			base.OnPropertyChanged("ButtonOkLabel");
			base.OnPropertyChanged("ButtonCancelLabel");
			base.OnPropertyChanged("AffirmativeAction");
			base.OnPropertyChanged("NegativeAction");
			base.OnPropertyChanged("AffirmativeTitleText");
			base.OnPropertyChanged("NegativeTitleText");
			this.SetAffirmativeHintProperties(this.ActiveData.AffirmativeHintText, this.ActiveData.AffirmativeHintTextExtended);
			this.AffirmativeHint = new BasicTooltipViewModel(() => this._affirmativeHintTooltipProperties);
			this.RefreshValues();
			this.IsShown = true;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004980 File Offset: 0x00002B80
		private void SetAffirmativeHintProperties(TextObject defaultHint, TextObject extendedHint)
		{
			this._affirmativeHintTooltipProperties = new List<TooltipProperty>();
			if (!string.IsNullOrEmpty((defaultHint != null) ? defaultHint.ToString() : null))
			{
				if (!string.IsNullOrEmpty((extendedHint != null) ? extendedHint.ToString() : null))
				{
					this._affirmativeHintTooltipProperties.Add(new TooltipProperty("", defaultHint.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None)
					{
						OnlyShowWhenNotExtended = true
					});
					this._affirmativeHintTooltipProperties.Add(new TooltipProperty("", extendedHint.ToString(), 0, true, TooltipProperty.TooltipPropertyFlags.None));
					return;
				}
				this._affirmativeHintTooltipProperties.Add(new TooltipProperty("", defaultHint.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004A20 File Offset: 0x00002C20
		public void ExecuteAffirmativeProcess()
		{
			Action onPositiveTrigger = this._onPositiveTrigger;
			if (onPositiveTrigger != null)
			{
				onPositiveTrigger();
			}
			SceneNotificationData activeData = this.ActiveData;
			if (activeData == null)
			{
				return;
			}
			activeData.OnAffirmativeAction();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004A43 File Offset: 0x00002C43
		public void ExecuteClose()
		{
			SceneNotificationData activeData = this.ActiveData;
			if (activeData != null)
			{
				activeData.OnCloseAction();
			}
			this._closeNotification();
			this.ClearData();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004A67 File Offset: 0x00002C67
		public void ExecuteNegativeProcess()
		{
			this._closeNotification();
			SceneNotificationData activeData = this.ActiveData;
			if (activeData != null)
			{
				activeData.OnNegativeAction();
			}
			this.ClearData();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004A8B File Offset: 0x00002C8B
		public void ClearData()
		{
			this.ActiveData = null;
			this.Scene = null;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004A9B File Offset: 0x00002C9B
		public override void OnFinalize()
		{
			base.OnFinalize();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004AA3 File Offset: 0x00002CA3
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00004AAB File Offset: 0x00002CAB
		[DataSourceProperty]
		public bool IsShown
		{
			get
			{
				return this._isShown;
			}
			set
			{
				if (this._isShown != value)
				{
					this._isShown = value;
					base.OnPropertyChangedWithValue(value, "IsShown");
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004AC9 File Offset: 0x00002CC9
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00004AD1 File Offset: 0x00002CD1
		[DataSourceProperty]
		public bool IsReady
		{
			get
			{
				return this._isReady;
			}
			set
			{
				if (this._isReady != value)
				{
					this._isReady = value;
					base.OnPropertyChangedWithValue(value, "IsReady");
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004AEF File Offset: 0x00002CEF
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00004AF7 File Offset: 0x00002CF7
		[DataSourceProperty]
		public string ClickToContinueText
		{
			get
			{
				return this._clickToContinueText;
			}
			set
			{
				if (this._clickToContinueText != value)
				{
					this._clickToContinueText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClickToContinueText");
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004B1A File Offset: 0x00002D1A
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				return ((activeData != null) ? activeData.TitleText.ToString() : null) ?? string.Empty;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004B3C File Offset: 0x00002D3C
		[DataSourceProperty]
		public string AffirmativeDescription
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				string text;
				if (activeData == null)
				{
					text = null;
				}
				else
				{
					TextObject affirmativeDescriptionText = activeData.AffirmativeDescriptionText;
					text = ((affirmativeDescriptionText != null) ? affirmativeDescriptionText.ToString() : null);
				}
				return text ?? string.Empty;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004B65 File Offset: 0x00002D65
		[DataSourceProperty]
		public string CancelDescription
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				string text;
				if (activeData == null)
				{
					text = null;
				}
				else
				{
					TextObject negativeDescriptionText = activeData.NegativeDescriptionText;
					text = ((negativeDescriptionText != null) ? negativeDescriptionText.ToString() : null);
				}
				return text ?? string.Empty;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004B8E File Offset: 0x00002D8E
		[DataSourceProperty]
		public string SceneID
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				return ((activeData != null) ? activeData.SceneID : null) ?? string.Empty;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00004BAB File Offset: 0x00002DAB
		[DataSourceProperty]
		public string ButtonOkLabel
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				string text;
				if (activeData == null)
				{
					text = null;
				}
				else
				{
					TextObject affirmativeText = activeData.AffirmativeText;
					text = ((affirmativeText != null) ? affirmativeText.ToString() : null);
				}
				return text ?? string.Empty;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004BD4 File Offset: 0x00002DD4
		[DataSourceProperty]
		public string ButtonCancelLabel
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				string text;
				if (activeData == null)
				{
					text = null;
				}
				else
				{
					TextObject negativeText = activeData.NegativeText;
					text = ((negativeText != null) ? negativeText.ToString() : null);
				}
				return text ?? string.Empty;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004BFD File Offset: 0x00002DFD
		[DataSourceProperty]
		public bool IsButtonOkShown
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				return activeData != null && activeData.IsAffirmativeOptionShown;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004C10 File Offset: 0x00002E10
		[DataSourceProperty]
		public bool IsButtonCancelShown
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				return activeData != null && activeData.IsNegativeOptionShown;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004C23 File Offset: 0x00002E23
		[DataSourceProperty]
		public string AffirmativeTitleText
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				string text;
				if (activeData == null)
				{
					text = null;
				}
				else
				{
					TextObject affirmativeTitleText = activeData.AffirmativeTitleText;
					text = ((affirmativeTitleText != null) ? affirmativeTitleText.ToString() : null);
				}
				return text ?? string.Empty;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004C4C File Offset: 0x00002E4C
		[DataSourceProperty]
		public string NegativeTitleText
		{
			get
			{
				SceneNotificationData activeData = this.ActiveData;
				string text;
				if (activeData == null)
				{
					text = null;
				}
				else
				{
					TextObject negativeTitleText = activeData.NegativeTitleText;
					text = ((negativeTitleText != null) ? negativeTitleText.ToString() : null);
				}
				return text ?? string.Empty;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004C75 File Offset: 0x00002E75
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004C7D File Offset: 0x00002E7D
		[DataSourceProperty]
		public object Scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				if (this._scene != value)
				{
					this._scene = value;
					base.OnPropertyChangedWithValue<object>(value, "Scene");
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004C9B File Offset: 0x00002E9B
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00004CA3 File Offset: 0x00002EA3
		[DataSourceProperty]
		public float EndProgress
		{
			get
			{
				return this._endProgress;
			}
			set
			{
				if (this._endProgress != value)
				{
					this._endProgress = value;
					base.OnPropertyChangedWithValue(value, "EndProgress");
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004CC1 File Offset: 0x00002EC1
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00004CC9 File Offset: 0x00002EC9
		[DataSourceProperty]
		public BasicTooltipViewModel AffirmativeHint
		{
			get
			{
				return this._affirmativeHint;
			}
			set
			{
				if (value != this._affirmativeHint)
				{
					this._affirmativeHint = value;
					base.OnPropertyChanged("AffirmativeHint");
				}
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly Action _closeNotification;

		// Token: 0x0400007F RID: 127
		private readonly Action _onPositiveTrigger;

		// Token: 0x04000080 RID: 128
		private readonly Func<string> _getContinueInputText;

		// Token: 0x04000081 RID: 129
		private List<TooltipProperty> _affirmativeHintTooltipProperties;

		// Token: 0x04000082 RID: 130
		private bool _isShown;

		// Token: 0x04000083 RID: 131
		private bool _isReady;

		// Token: 0x04000084 RID: 132
		private object _scene;

		// Token: 0x04000085 RID: 133
		private float _endProgress;

		// Token: 0x04000086 RID: 134
		private string _clickToContinueText;

		// Token: 0x04000087 RID: 135
		private BasicTooltipViewModel _affirmativeHint;
	}
}
