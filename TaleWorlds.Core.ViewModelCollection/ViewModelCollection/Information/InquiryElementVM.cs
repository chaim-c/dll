using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x02000018 RID: 24
	public class InquiryElementVM : ViewModel
	{
		// Token: 0x0600011B RID: 283 RVA: 0x0000428C File Offset: 0x0000248C
		public InquiryElementVM(InquiryElement elementData, TextObject hint, Action<InquiryElementVM, bool> onSelectedStateChanged = null)
		{
			this.Text = elementData.Title;
			this.ImageIdentifier = ((elementData.ImageIdentifier != null) ? new ImageIdentifierVM(elementData.ImageIdentifier) : new ImageIdentifierVM(ImageIdentifierType.Null));
			this.InquiryElement = elementData;
			this.IsEnabled = elementData.IsEnabled;
			this.HasVisuals = (elementData.ImageIdentifier != null);
			this.Hint = new HintViewModel(hint, null);
			this._onSelectedStateChanged = onSelectedStateChanged;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004302 File Offset: 0x00002502
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000430A File Offset: 0x0000250A
		[DataSourceProperty]
		public bool IsFilteredOut
		{
			get
			{
				return this._isFilteredOut;
			}
			set
			{
				if (this._isFilteredOut != value)
				{
					this._isFilteredOut = value;
					base.OnPropertyChangedWithValue(value, "IsFilteredOut");
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004328 File Offset: 0x00002528
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004330 File Offset: 0x00002530
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (this._isSelected != value)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
					Action<InquiryElementVM, bool> onSelectedStateChanged = this._onSelectedStateChanged;
					if (onSelectedStateChanged == null)
					{
						return;
					}
					onSelectedStateChanged(this, value);
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004360 File Offset: 0x00002560
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004368 File Offset: 0x00002568
		[DataSourceProperty]
		public bool HasVisuals
		{
			get
			{
				return this._hasVisuals;
			}
			set
			{
				if (this._hasVisuals != value)
				{
					this._hasVisuals = value;
					base.OnPropertyChangedWithValue(value, "HasVisuals");
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004386 File Offset: 0x00002586
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000438E File Offset: 0x0000258E
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (this._isEnabled != value)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000043AC File Offset: 0x000025AC
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000043B4 File Offset: 0x000025B4
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this._text != value)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000043D7 File Offset: 0x000025D7
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000043DF File Offset: 0x000025DF
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (this._imageIdentifier != value)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000043FD File Offset: 0x000025FD
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00004405 File Offset: 0x00002605
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (this._hint != value)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x04000071 RID: 113
		public readonly InquiryElement InquiryElement;

		// Token: 0x04000072 RID: 114
		private readonly Action<InquiryElementVM, bool> _onSelectedStateChanged;

		// Token: 0x04000073 RID: 115
		private bool _isFilteredOut;

		// Token: 0x04000074 RID: 116
		private bool _isSelected;

		// Token: 0x04000075 RID: 117
		private bool _isEnabled;

		// Token: 0x04000076 RID: 118
		private string _text;

		// Token: 0x04000077 RID: 119
		private bool _hasVisuals;

		// Token: 0x04000078 RID: 120
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x04000079 RID: 121
		private HintViewModel _hint;
	}
}
