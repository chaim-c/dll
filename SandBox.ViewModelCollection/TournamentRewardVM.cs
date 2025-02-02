using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection
{
	// Token: 0x02000008 RID: 8
	public class TournamentRewardVM : ViewModel
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000050B3 File Offset: 0x000032B3
		public TournamentRewardVM(string text)
		{
			this.Text = text;
			this.GotImageIdentifier = false;
			this.ImageIdentifier = new ImageIdentifierVM(ImageIdentifierType.Null);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000050D5 File Offset: 0x000032D5
		public TournamentRewardVM(string text, ImageIdentifierVM imageIdentifierVM)
		{
			this.Text = text;
			this.GotImageIdentifier = true;
			this.ImageIdentifier = imageIdentifierVM;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000050F2 File Offset: 0x000032F2
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000050FA File Offset: 0x000032FA
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000511D File Offset: 0x0000331D
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00005125 File Offset: 0x00003325
		[DataSourceProperty]
		public bool GotImageIdentifier
		{
			get
			{
				return this._gotImageIdentifier;
			}
			set
			{
				if (value != this._gotImageIdentifier)
				{
					this._gotImageIdentifier = value;
					base.OnPropertyChangedWithValue(value, "GotImageIdentifier");
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00005143 File Offset: 0x00003343
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000514B File Offset: 0x0000334B
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x04000019 RID: 25
		private string _text;

		// Token: 0x0400001A RID: 26
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x0400001B RID: 27
		private bool _gotImageIdentifier;
	}
}
