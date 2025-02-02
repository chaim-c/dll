using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Credits
{
	// Token: 0x02000153 RID: 339
	public class CreditsItemWidget : Widget
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0003190F File Offset: 0x0002FB0F
		public CreditsItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00031918 File Offset: 0x0002FB18
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.RefreshItemWidget();
				this._initialized = true;
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00031938 File Offset: 0x0002FB38
		private void RefreshItemWidget()
		{
			if (!string.IsNullOrEmpty(this.ItemType))
			{
				if (this.CategoryWidget != null)
				{
					this.CategoryWidget.IsVisible = (this.ItemType == "Category");
				}
				if (this.SectionWidget != null)
				{
					this.SectionWidget.IsVisible = (this.ItemType == "Section");
				}
				if (this.EntryWidget != null)
				{
					this.EntryWidget.IsVisible = (this.ItemType == "Entry");
				}
				if (this.EmptyLineWidget != null)
				{
					this.EmptyLineWidget.IsVisible = (this.ItemType == "EmptyLine");
				}
				if (this.ImageWidget != null)
				{
					this.ImageWidget.IsVisible = (this.ItemType == "Image");
					if (this.ImageWidget.Sprite != null)
					{
						this.ImageWidget.SuggestedWidth = (float)this.ImageWidget.Sprite.Width;
						this.ImageWidget.SuggestedHeight = (float)this.ImageWidget.Sprite.Height;
					}
				}
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00031A49 File Offset: 0x0002FC49
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x00031A51 File Offset: 0x0002FC51
		[Editor(false)]
		public string ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (this._itemType != value)
				{
					this._itemType = value;
					base.OnPropertyChanged<string>(value, "ItemType");
				}
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00031A74 File Offset: 0x0002FC74
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x00031A7C File Offset: 0x0002FC7C
		[Editor(false)]
		public Widget CategoryWidget
		{
			get
			{
				return this._categoryWidget;
			}
			set
			{
				if (this._categoryWidget != value)
				{
					this._categoryWidget = value;
					base.OnPropertyChanged<Widget>(value, "CategoryWidget");
				}
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00031A9A File Offset: 0x0002FC9A
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x00031AA2 File Offset: 0x0002FCA2
		[Editor(false)]
		public Widget ImageWidget
		{
			get
			{
				return this._imageWidget;
			}
			set
			{
				if (this._imageWidget != value)
				{
					this._imageWidget = value;
					base.OnPropertyChanged<Widget>(value, "ImageWidget");
				}
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00031AC0 File Offset: 0x0002FCC0
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x00031AC8 File Offset: 0x0002FCC8
		[Editor(false)]
		public Widget SectionWidget
		{
			get
			{
				return this._sectionWidget;
			}
			set
			{
				if (this._sectionWidget != value)
				{
					this._sectionWidget = value;
					base.OnPropertyChanged<Widget>(value, "SectionWidget");
				}
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00031AE6 File Offset: 0x0002FCE6
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x00031AEE File Offset: 0x0002FCEE
		[Editor(false)]
		public Widget EntryWidget
		{
			get
			{
				return this._entryWidget;
			}
			set
			{
				if (this._entryWidget != value)
				{
					this._entryWidget = value;
					base.OnPropertyChanged<Widget>(value, "EntryWidget");
				}
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00031B0C File Offset: 0x0002FD0C
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x00031B14 File Offset: 0x0002FD14
		[Editor(false)]
		public Widget EmptyLineWidget
		{
			get
			{
				return this._emptyLineWidget;
			}
			set
			{
				if (this._emptyLineWidget != value)
				{
					this._emptyLineWidget = value;
					base.OnPropertyChanged<Widget>(value, "EmptyLineWidget");
				}
			}
		}

		// Token: 0x0400082B RID: 2091
		private bool _initialized;

		// Token: 0x0400082C RID: 2092
		private string _itemType;

		// Token: 0x0400082D RID: 2093
		private Widget _categoryWidget;

		// Token: 0x0400082E RID: 2094
		private Widget _sectionWidget;

		// Token: 0x0400082F RID: 2095
		private Widget _entryWidget;

		// Token: 0x04000830 RID: 2096
		private Widget _emptyLineWidget;

		// Token: 0x04000831 RID: 2097
		private Widget _imageWidget;
	}
}
