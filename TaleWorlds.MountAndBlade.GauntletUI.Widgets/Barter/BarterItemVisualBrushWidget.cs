using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Barter
{
	// Token: 0x02000184 RID: 388
	public class BarterItemVisualBrushWidget : BrushWidget
	{
		// Token: 0x060013E5 RID: 5093 RVA: 0x000365EE File Offset: 0x000347EE
		public BarterItemVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00036604 File Offset: 0x00034804
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			if (!this._imageDetermined)
			{
				this.RegisterStatesOfWidgetFromBrush(this.SpriteWidget);
				this.UpdateVisual();
				this._imageDetermined = true;
			}
			if (this._imageDetermined && this.Type == "fief_barterable")
			{
				this.SpriteClipWidget.ClipContents = true;
				this.SpriteWidget.WidthSizePolicy = SizePolicy.Fixed;
				this.SpriteWidget.HeightSizePolicy = SizePolicy.Fixed;
				this.SpriteWidget.ScaledSuggestedHeight = this.SpriteClipWidget.Size.X;
				this.SpriteWidget.ScaledSuggestedWidth = this.SpriteClipWidget.Size.X;
				this.SpriteWidget.PositionYOffset = 18f;
				this.SpriteWidget.VerticalAlignment = VerticalAlignment.Center;
			}
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x000366CC File Offset: 0x000348CC
		private void RegisterStatesOfWidgetFromBrush(BrushWidget widget)
		{
			if (widget != null)
			{
				foreach (BrushLayer brushLayer in widget.ReadOnlyBrush.Layers)
				{
					widget.AddState(brushLayer.Name);
				}
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0003672C File Offset: 0x0003492C
		private void UpdateVisual()
		{
			Sprite sprite = null;
			this.SpriteWidget.IsVisible = false;
			this.MaskedTextureWidget.IsVisible = false;
			this.ImageIdentifierWidget.IsVisible = false;
			string type = this.Type;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
			if (num <= 2080743372U)
			{
				if (num <= 403518212U)
				{
					if (num != 189982571U)
					{
						if (num != 284088421U)
						{
							if (num != 403518212U)
							{
								goto IL_294;
							}
							if (!(type == "fief_barterable"))
							{
								goto IL_294;
							}
							sprite = base.EventManager.Context.SpriteData.GetSprite(this.FiefImagePath + "_t");
							this.SpriteWidget.Brush = base.EventManager.Context.DefaultBrush;
							this.SpriteWidget.IsVisible = true;
							goto IL_2A0;
						}
						else
						{
							if (!(type == "lift_siege_barterable"))
							{
								goto IL_294;
							}
							goto IL_294;
						}
					}
					else if (!(type == "join_faction_barterable"))
					{
						goto IL_294;
					}
				}
				else if (num <= 1289251258U)
				{
					if (num != 806661062U)
					{
						if (num != 1289251258U)
						{
							goto IL_294;
						}
						if (!(type == "marriage_barterable"))
						{
							goto IL_294;
						}
						goto IL_286;
					}
					else
					{
						if (!(type == "war_barterable"))
						{
							goto IL_294;
						}
						goto IL_294;
					}
				}
				else if (num != 1654682144U)
				{
					if (num != 2080743372U)
					{
						goto IL_294;
					}
					if (!(type == "leave_faction_barterable"))
					{
						goto IL_294;
					}
				}
				else
				{
					if (!(type == "start_siege_barterable"))
					{
						goto IL_294;
					}
					goto IL_294;
				}
			}
			else if (num <= 2639715379U)
			{
				if (num != 2166136261U)
				{
					if (num != 2342284176U)
					{
						if (num != 2639715379U)
						{
							goto IL_294;
						}
						if (!(type == "item_barterable"))
						{
							goto IL_294;
						}
						goto IL_286;
					}
					else
					{
						if (!(type == "set_prisoner_free_barterable"))
						{
							goto IL_294;
						}
						goto IL_286;
					}
				}
				else
				{
					if (type != null && type.Length != 0)
					{
						goto IL_294;
					}
					goto IL_294;
				}
			}
			else if (num <= 3787227692U)
			{
				if (num != 3249789840U)
				{
					if (num != 3787227692U)
					{
						goto IL_294;
					}
					if (!(type == "safe_passage_barterable"))
					{
						goto IL_294;
					}
					goto IL_294;
				}
				else if (!(type == "mercenary_join_faction_barterable"))
				{
					goto IL_294;
				}
			}
			else if (num != 3835993774U)
			{
				if (num != 3957684540U)
				{
					goto IL_294;
				}
				if (!(type == "gold_barterable"))
				{
					goto IL_294;
				}
				goto IL_294;
			}
			else
			{
				if (!(type == "peace_barterable"))
				{
					goto IL_294;
				}
				goto IL_294;
			}
			this.MaskedTextureWidget.IsVisible = true;
			goto IL_2A0;
			IL_286:
			this.ImageIdentifierWidget.IsVisible = true;
			goto IL_2A0;
			IL_294:
			this.SpriteWidget.IsVisible = true;
			IL_2A0:
			if (this.SpriteWidget.ContainsState(this.Type))
			{
				this.SpriteWidget.SetState(this.Type);
			}
			if (sprite != null)
			{
				this.SetWidgetSpriteForAllStyles(this.SpriteWidget, sprite);
			}
			this.SpriteClipWidget.IsVisible = this.SpriteWidget.IsVisible;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00036A24 File Offset: 0x00034C24
		private void SetWidgetSpriteForAllStyles(BrushWidget widget, Sprite sprite)
		{
			widget.Sprite = sprite;
			foreach (Style style in widget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Sprite = sprite;
				}
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00036A98 File Offset: 0x00034C98
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00036AA0 File Offset: 0x00034CA0
		[Editor(false)]
		public BrushWidget SpriteWidget
		{
			get
			{
				return this._spriteWidget;
			}
			set
			{
				if (this._spriteWidget != value)
				{
					this._spriteWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "SpriteWidget");
				}
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00036ABE File Offset: 0x00034CBE
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00036AC6 File Offset: 0x00034CC6
		[Editor(false)]
		public Widget SpriteClipWidget
		{
			get
			{
				return this._spriteClipWidget;
			}
			set
			{
				if (this._spriteClipWidget != value)
				{
					this._spriteClipWidget = value;
					base.OnPropertyChanged<Widget>(value, "SpriteClipWidget");
				}
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00036AE4 File Offset: 0x00034CE4
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00036AEC File Offset: 0x00034CEC
		[Editor(false)]
		public ImageIdentifierWidget ImageIdentifierWidget
		{
			get
			{
				return this._imageIdentifierWidget;
			}
			set
			{
				if (this._imageIdentifierWidget != value)
				{
					this._imageIdentifierWidget = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "ImageIdentifierWidget");
				}
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00036B0A File Offset: 0x00034D0A
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00036B12 File Offset: 0x00034D12
		[Editor(false)]
		public MaskedTextureWidget MaskedTextureWidget
		{
			get
			{
				return this._maskedTextureWidget;
			}
			set
			{
				if (this._maskedTextureWidget != value)
				{
					this._maskedTextureWidget = value;
					base.OnPropertyChanged<MaskedTextureWidget>(value, "MaskedTextureWidget");
				}
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00036B30 File Offset: 0x00034D30
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00036B38 File Offset: 0x00034D38
		[Editor(false)]
		public bool HasVisualIdentifier
		{
			get
			{
				return this._hasVisualIdentifier;
			}
			set
			{
				if (this._hasVisualIdentifier != value)
				{
					this._hasVisualIdentifier = value;
					base.OnPropertyChanged(value, "HasVisualIdentifier");
				}
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00036B56 File Offset: 0x00034D56
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00036B5E File Offset: 0x00034D5E
		[Editor(false)]
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (this._type != value)
				{
					this._type = value;
					base.OnPropertyChanged<string>(value, "Type");
				}
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00036B81 File Offset: 0x00034D81
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x00036B89 File Offset: 0x00034D89
		[Editor(false)]
		public string FiefImagePath
		{
			get
			{
				return this._fiefImagePath;
			}
			set
			{
				if (this._fiefImagePath != value)
				{
					this._fiefImagePath = value;
					base.OnPropertyChanged<string>(value, "FiefImagePath");
				}
			}
		}

		// Token: 0x04000910 RID: 2320
		private bool _imageDetermined;

		// Token: 0x04000911 RID: 2321
		private string _type = "";

		// Token: 0x04000912 RID: 2322
		private string _fiefImagePath;

		// Token: 0x04000913 RID: 2323
		private bool _hasVisualIdentifier;

		// Token: 0x04000914 RID: 2324
		private BrushWidget _spriteWidget;

		// Token: 0x04000915 RID: 2325
		private MaskedTextureWidget _maskedTextureWidget;

		// Token: 0x04000916 RID: 2326
		private ImageIdentifierWidget _imageIdentifierWidget;

		// Token: 0x04000917 RID: 2327
		private Widget _spriteClipWidget;
	}
}
