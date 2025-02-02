using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Launcher.Library.CustomWidgets
{
	// Token: 0x02000026 RID: 38
	public class LauncherRandomImageWidget : Widget
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00006A16 File Offset: 0x00004C16
		public LauncherRandomImageWidget(UIContext context) : base(context)
		{
			this._random = new Random();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006A2C File Offset: 0x00004C2C
		private void ShuffleList<T>(List<T> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				T value = list[i];
				int index = this._random.Next(i, list.Count);
				list[i] = list[index];
				list[index] = value;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006A7C File Offset: 0x00004C7C
		private void CreateIndicesList()
		{
			this._imageIndices = new List<int>();
			for (int i = 0; i < this.ImageCount; i++)
			{
				this._imageIndices.Add(i);
			}
			this.ShuffleList<int>(this._imageIndices);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006AC0 File Offset: 0x00004CC0
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this.CreateIndicesList();
			int num = this._imageIndices[this._currentIndex];
			base.Sprite = base.Context.SpriteData.GetSprite("ConceptArts\\ConceptArt_" + num);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006B14 File Offset: 0x00004D14
		private void TriggerChanged()
		{
			this._currentIndex = (this._currentIndex + 1) % this._imageIndices.Count;
			int num = this._imageIndices[this._currentIndex];
			base.Sprite = base.Context.SpriteData.GetSprite("ConceptArts\\ConceptArt_" + num);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006B73 File Offset: 0x00004D73
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00006B7B File Offset: 0x00004D7B
		[DataSourceProperty]
		public int ImageCount
		{
			get
			{
				return this._imageCount;
			}
			set
			{
				if (value != this._imageCount)
				{
					this._imageCount = value;
					base.OnPropertyChanged(value, "ImageCount");
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006B99 File Offset: 0x00004D99
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00006BA1 File Offset: 0x00004DA1
		[DataSourceProperty]
		public bool ChangeTrigger
		{
			get
			{
				return this._changeTrigger;
			}
			set
			{
				if (value != this._changeTrigger)
				{
					this._changeTrigger = value;
					base.OnPropertyChanged(value, "ChangeTrigger");
					this.TriggerChanged();
				}
			}
		}

		// Token: 0x040000B7 RID: 183
		private readonly Random _random;

		// Token: 0x040000B8 RID: 184
		private List<int> _imageIndices;

		// Token: 0x040000B9 RID: 185
		private int _currentIndex;

		// Token: 0x040000BA RID: 186
		private int _imageCount;

		// Token: 0x040000BB RID: 187
		private bool _changeTrigger;
	}
}
