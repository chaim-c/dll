using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information
{
	// Token: 0x0200013C RID: 316
	public class TooltipPropertyWidget : Widget
	{
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0002E540 File Offset: 0x0002C740
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x0002E548 File Offset: 0x0002C748
		public bool IsTwoColumn { get; private set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0002E551 File Offset: 0x0002C751
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x0002E559 File Offset: 0x0002C759
		public TooltipPropertyWidget.TooltipPropertyFlags PropertyModifierAsFlag { get; private set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0002E562 File Offset: 0x0002C762
		private bool _allBrushesInitialized
		{
			get
			{
				return this.SubtextBrush != null && this.ValueTextBrush != null && this.DescriptionTextBrush != null && this.ValueNameTextBrush != null && this.RundownSeperatorSprite != null && this.DefaultSeperatorSprite != null && this.TitleBackgroundSprite != null;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0002E59F File Offset: 0x0002C79F
		public bool IsMultiLine
		{
			get
			{
				return this._isMultiLine;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0002E5A7 File Offset: 0x0002C7A7
		public bool IsBattleMode
		{
			get
			{
				return this._isBattleMode;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0002E5AF File Offset: 0x0002C7AF
		public bool IsBattleModeOver
		{
			get
			{
				return this._isBattleModeOver;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0002E5B7 File Offset: 0x0002C7B7
		public bool IsCost
		{
			get
			{
				return this._isCost;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0002E5BF File Offset: 0x0002C7BF
		public bool IsRelation
		{
			get
			{
				return this._isRelation;
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0002E5C7 File Offset: 0x0002C7C7
		public TooltipPropertyWidget(UIContext context) : base(context)
		{
			this._isMultiLine = false;
			this._isBattleMode = false;
			this._isBattleModeOver = false;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0002E5F3 File Offset: 0x0002C7F3
		public void SetBattleScope(bool battleScope)
		{
			if (battleScope)
			{
				this.DefinitionLabel.HorizontalAlignment = HorizontalAlignment.Center;
				this.ValueLabel.HorizontalAlignment = HorizontalAlignment.Center;
				return;
			}
			this.DefinitionLabel.HorizontalAlignment = HorizontalAlignment.Right;
			this.ValueLabel.HorizontalAlignment = HorizontalAlignment.Left;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0002E62C File Offset: 0x0002C82C
		public void RefreshSize(bool inBattleScope, float battleScopeSize, float maxValueLabelSizeX, float maxDefinitionLabelSizeX, Brush definitionRelationBrush = null, Brush valueRelationBrush = null)
		{
			if (this._isMultiLine || this._isSubtext)
			{
				this.DefinitionLabelContainer.IsVisible = false;
				this.DefinitionLabelContainer.ScaledSuggestedWidth = 0f;
				this.ValueLabel.WidthSizePolicy = SizePolicy.Fixed;
				this.ValueLabelContainer.WidthSizePolicy = SizePolicy.Fixed;
				this.ValueLabel.ScaledSuggestedWidth = base.ParentWidget.Size.X - (base.ScaledMarginLeft + base.ScaledMarginRight);
				this.ValueLabelContainer.ScaledSuggestedWidth = base.ParentWidget.Size.X - (base.ScaledMarginLeft + base.ScaledMarginRight);
			}
			else if (inBattleScope)
			{
				this.DefinitionLabelContainer.ScaledSuggestedWidth = battleScopeSize;
				this.DefinitionLabel.Brush = definitionRelationBrush;
				this.ValueLabelContainer.ScaledSuggestedWidth = battleScopeSize;
				this.ValueLabel.Brush = valueRelationBrush;
				this.ValueLabelContainer.HorizontalAlignment = HorizontalAlignment.Left;
				this.ValueLabel.HorizontalAlignment = HorizontalAlignment.Left;
				this.ValueLabel.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Left;
			}
			else if (!this.IsTwoColumn)
			{
				if (!string.IsNullOrEmpty(this.DefinitionLabel.Text))
				{
					float scaledSuggestedWidth = (this.DefinitionLabel.Size.X > this.ValueLabel.Size.X) ? this.DefinitionLabel.Size.X : this.ValueLabel.Size.X;
					this.DefinitionLabelContainer.ScaledSuggestedWidth = scaledSuggestedWidth;
					this.ValueLabelContainer.ScaledSuggestedWidth = scaledSuggestedWidth;
				}
				else
				{
					this.DefinitionLabelContainer.ScaledSuggestedWidth = 0f;
					this.DefinitionLabelContainer.IsVisible = false;
					this.ValueLabelContainer.ScaledSuggestedWidth = this.ValueLabel.Size.X;
				}
			}
			this._maxValueLabelSizeX = maxValueLabelSizeX;
			if (this.IsTwoColumn && !this._isMultiLine && (!this._isTitle || (this._isTitle && this.IsTwoColumn)))
			{
				this.ValueLabel.WidthSizePolicy = SizePolicy.Fixed;
				this.ValueLabel.ScaledSuggestedWidth = MathF.Max(53f * base._scaleToUse, this._maxValueLabelSizeX);
				this.ValueLabelContainer.WidthSizePolicy = SizePolicy.Fixed;
				this.ValueLabelContainer.ScaledSuggestedWidth = MathF.Max(53f * base._scaleToUse, this._maxValueLabelSizeX);
			}
			if (this.IsTwoColumn && !this._isMultiLine && this._isTitle)
			{
				this.DefinitionLabel.WidthSizePolicy = SizePolicy.Fixed;
				this.DefinitionLabel.ScaledSuggestedWidth = MathF.Max(53f * base._scaleToUse, maxDefinitionLabelSizeX);
				this.DefinitionLabelContainer.WidthSizePolicy = SizePolicy.Fixed;
				this.DefinitionLabelContainer.ScaledSuggestedWidth = MathF.Max(53f * base._scaleToUse, maxDefinitionLabelSizeX);
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0002E8E4 File Offset: 0x0002CAE4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this._firstFrame)
			{
				this.RefreshText();
				this._firstFrame = false;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0002E904 File Offset: 0x0002CB04
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.ValueBackgroundSpriteWidget.HeightSizePolicy = SizePolicy.CoverChildren;
			if (this._currentSprite != null)
			{
				if (this.DefinitionLabelContainer.Size.X + this.ValueLabelContainer.Size.X > base.ParentWidget.Size.X)
				{
					this.ValueBackgroundSpriteWidget.WidthSizePolicy = SizePolicy.Fixed;
					this.ValueBackgroundSpriteWidget.ScaledSuggestedWidth = this.DefinitionLabelContainer.Size.X + this.ValueLabelContainer.Size.X;
					base.MarginLeft = 0f;
					base.MarginRight = 0f;
				}
				else
				{
					this.ValueBackgroundSpriteWidget.WidthSizePolicy = SizePolicy.Fixed;
					this.ValueBackgroundSpriteWidget.ScaledSuggestedWidth = base.ParentWidget.Size.X - (base.MarginLeft + base.MarginRight) * base._scaleToUse;
				}
				this.ValueBackgroundSpriteWidget.MinHeight = (float)this._currentSprite.Height;
				if (this._isTitle)
				{
					base.PositionXOffset = -base.MarginLeft;
					if (!this.IsTwoColumn)
					{
						this.ValueLabelContainer.MarginLeft = base.MarginLeft;
						this.ValueBackgroundSpriteWidget.ScaledSuggestedHeight = this.ValueLabel.Size.Y;
						return;
					}
					this.DefinitionLabelContainer.MarginLeft = base.MarginLeft;
					this.DefinitionLabel.HorizontalAlignment = HorizontalAlignment.Left;
					return;
				}
			}
			else
			{
				this.ValueBackgroundSpriteWidget.SuggestedWidth = 0f;
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002EA80 File Offset: 0x0002CC80
		private void RefreshText()
		{
			this.DefinitionLabel.Text = this._definitionText;
			this.ValueLabel.Text = this._valueText;
			this.DetermineTypeOfTooltipProperty();
			this.ValueLabelContainer.IsVisible = true;
			this.DefinitionLabelContainer.IsVisible = true;
			this.DefinitionLabel.IsVisible = true;
			this.ValueLabel.IsVisible = true;
			this._currentSprite = null;
			if (this._allBrushesInitialized)
			{
				if (this._isRelation)
				{
					this.DefinitionLabel.Text = "";
					this.ValueLabel.Text = "";
				}
				else if (this._isBattleMode)
				{
					this.DefinitionLabel.Text = "";
					this.ValueLabel.Text = "";
				}
				else if (this._isBattleModeOver)
				{
					this.DefinitionLabel.Text = "";
					this.ValueLabel.Text = "";
				}
				else if (this._isMultiLine)
				{
					this.DefinitionLabelContainer.IsVisible = false;
					this.ValueLabel.Text = this._valueText;
					this.ValueLabel.Brush = this.DescriptionTextBrush;
					this.ValueLabel.WidthSizePolicy = SizePolicy.Fixed;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.Fixed;
					this.ValueLabel.SuggestedWidth = 0f;
					this.ValueLabelContainer.SuggestedWidth = 0f;
				}
				else if (this._isCost)
				{
					this.DefinitionLabel.Text = "";
					this.ValueLabel.Text = this._valueText;
					base.HorizontalAlignment = HorizontalAlignment.Center;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
				}
				else if (this._isRundownSeperator)
				{
					this.ValueLabel.IsVisible = false;
					this.DefinitionLabelContainer.IsVisible = false;
					this.ValueBackgroundSpriteWidget.IsVisible = true;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					this._currentSprite = this.RundownSeperatorSprite;
					this.ValueBackgroundSpriteWidget.HorizontalAlignment = HorizontalAlignment.Right;
					this.ValueBackgroundSpriteWidget.PositionXOffset = base.Right * base._inverseScaleToUse;
					this.ValueBackgroundSpriteWidget.Sprite = this._currentSprite;
					this.ValueBackgroundSpriteWidget.HeightSizePolicy = SizePolicy.Fixed;
					this.ValueBackgroundSpriteWidget.WidthSizePolicy = SizePolicy.Fixed;
				}
				else if (this._isDefaultSeperator)
				{
					this.ValueLabel.IsVisible = false;
					this.DefinitionLabelContainer.IsVisible = false;
					this.ValueBackgroundSpriteWidget.IsVisible = true;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					this._currentSprite = this.DefaultSeperatorSprite;
					this.ValueBackgroundSpriteWidget.HorizontalAlignment = HorizontalAlignment.Right;
					this.ValueBackgroundSpriteWidget.PositionXOffset = base.Right * base._inverseScaleToUse;
					this.ValueBackgroundSpriteWidget.Sprite = this._currentSprite;
					this.ValueBackgroundSpriteWidget.AlphaFactor = 0.5f;
					this.ValueBackgroundSpriteWidget.HeightSizePolicy = SizePolicy.Fixed;
					this.ValueBackgroundSpriteWidget.WidthSizePolicy = SizePolicy.Fixed;
				}
				else if (this._isTitle)
				{
					this.DefinitionLabel.Brush = this.TitleTextBrush;
					this.ValueLabel.Brush = this.TitleTextBrush;
					this.DefinitionLabel.HeightSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabel.HeightSizePolicy = SizePolicy.CoverChildren;
					this.DefinitionLabelContainer.HeightSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabelContainer.HeightSizePolicy = SizePolicy.CoverChildren;
					if (this.IsTwoColumn)
					{
						this.DefinitionLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
						this.DefinitionLabelContainer.HorizontalAlignment = HorizontalAlignment.Left;
						this.DefinitionLabel.WidthSizePolicy = SizePolicy.CoverChildren;
						this.DefinitionLabel.HorizontalAlignment = HorizontalAlignment.Left;
						this.DefinitionLabel.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Left;
						this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
						this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
						this.ValueLabel.Text = " " + this.ValueLabel.Text;
					}
					else
					{
						this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
						this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
					}
					this._currentSprite = this.TitleBackgroundSprite;
					this.ValueBackgroundSpriteWidget.HeightSizePolicy = SizePolicy.CoverChildren;
					this.ValueBackgroundSpriteWidget.Sprite = this._currentSprite;
					this.ValueBackgroundSpriteWidget.IsVisible = true;
				}
				else if (this.IsTwoColumn)
				{
					this.DefinitionLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					this.DefinitionLabel.WidthSizePolicy = SizePolicy.CoverChildren;
					this.DefinitionLabelContainer.HorizontalAlignment = HorizontalAlignment.Right;
					this.DefinitionLabel.HorizontalAlignment = HorizontalAlignment.Right;
					this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					base.HorizontalAlignment = HorizontalAlignment.Right;
					this.ValueLabel.Text = " " + this.ValueLabel.Text;
					this.DefinitionLabel.Brush = this.ValueNameTextBrush;
					this.ValueLabel.Brush = this.ValueTextBrush;
				}
				else if (this._isSubtext)
				{
					this.DefinitionLabelContainer.IsVisible = false;
					this.ValueLabel.Brush = this.SubtextBrush;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
				}
				else if (this._isEmptySpace)
				{
					this.DefinitionLabel.IsVisible = false;
					this.ValueLabel.Text = " ";
					this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					if (this.TextHeight > 0)
					{
						this.ValueLabel.Brush.FontSize = 30;
					}
					else if (this.TextHeight < 0)
					{
						this.ValueLabel.Brush.FontSize = 10;
					}
					else
					{
						this.ValueLabel.Brush.FontSize = 15;
					}
				}
				else if (this.DefinitionLabel.Text == string.Empty && this.ValueLabel.Text != string.Empty)
				{
					this.DefinitionLabelContainer.IsVisible = false;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabel.Brush = this.DescriptionTextBrush;
					this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
					this.ValueLabelContainer.WidthSizePolicy = SizePolicy.CoverChildren;
				}
				else
				{
					this.ValueLabel.WidthSizePolicy = SizePolicy.CoverChildren;
					this.DefinitionLabel.WidthSizePolicy = SizePolicy.CoverChildren;
				}
				if (this._useCustomColor)
				{
					this.ValueLabel.Brush.FontColor = this.TextColor;
					this.ValueLabel.Brush.TextAlphaFactor = this.TextColor.Alpha;
				}
				if (this._isRundownResult)
				{
					this.ValueLabel.Brush.FontSize = (int)((float)this.ValueLabel.ReadOnlyBrush.FontSize * 1.3f);
					this.DefinitionLabel.Brush.FontSize = (int)((float)this.DefinitionLabel.ReadOnlyBrush.FontSize * 1.3f);
				}
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002F154 File Offset: 0x0002D354
		private void DetermineTypeOfTooltipProperty()
		{
			this.PropertyModifierAsFlag = (TooltipPropertyWidget.TooltipPropertyFlags)this.PropertyModifier;
			this._isMultiLine = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.MultiLine) == TooltipPropertyWidget.TooltipPropertyFlags.MultiLine);
			this._isBattleMode = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.BattleMode) == TooltipPropertyWidget.TooltipPropertyFlags.BattleMode);
			this._isBattleModeOver = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.BattleModeOver) == TooltipPropertyWidget.TooltipPropertyFlags.BattleModeOver);
			this._isCost = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.Cost) == TooltipPropertyWidget.TooltipPropertyFlags.Cost);
			this._isTitle = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.Title) == TooltipPropertyWidget.TooltipPropertyFlags.Title);
			this._isRelation = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarFirstEnemy) == TooltipPropertyWidget.TooltipPropertyFlags.WarFirstEnemy || (this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarFirstAlly) == TooltipPropertyWidget.TooltipPropertyFlags.WarFirstAlly || (this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarFirstNeutral) == TooltipPropertyWidget.TooltipPropertyFlags.WarFirstNeutral || (this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarSecondEnemy) == TooltipPropertyWidget.TooltipPropertyFlags.WarSecondEnemy || (this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarSecondAlly) == TooltipPropertyWidget.TooltipPropertyFlags.WarSecondAlly || (this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarSecondNeutral) == TooltipPropertyWidget.TooltipPropertyFlags.WarSecondNeutral);
			this._isRundownSeperator = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.RundownSeperator) == TooltipPropertyWidget.TooltipPropertyFlags.RundownSeperator);
			this._isDefaultSeperator = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.DefaultSeperator) == TooltipPropertyWidget.TooltipPropertyFlags.DefaultSeperator);
			this._isRundownResult = ((this.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.RundownResult) == TooltipPropertyWidget.TooltipPropertyFlags.RundownResult);
			this.IsTwoColumn = false;
			this._isSubtext = false;
			this._isEmptySpace = false;
			if (!this._isMultiLine && !this._isBattleMode && !this._isBattleModeOver && !this._isCost && !this._isRundownSeperator && !this._isDefaultSeperator)
			{
				this._isEmptySpace = (this.DefinitionText == string.Empty && this.ValueText == string.Empty);
				this.IsTwoColumn = (this.DefinitionText != string.Empty && this.ValueText != string.Empty && this.TextHeight == 0);
				this._isSubtext = (this.DefinitionText == string.Empty && this.ValueText != string.Empty && this.TextHeight < 0);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0002F36C File Offset: 0x0002D56C
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x0002F374 File Offset: 0x0002D574
		[Editor(false)]
		public string RundownSeperatorSpriteName
		{
			get
			{
				return this._rundownSeperatorSpriteName;
			}
			set
			{
				if (this._rundownSeperatorSpriteName != value)
				{
					this._rundownSeperatorSpriteName = value;
					base.OnPropertyChanged<string>(value, "RundownSeperatorSpriteName");
					this.RundownSeperatorSprite = base.Context.SpriteData.GetSprite(value);
				}
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0002F3AE File Offset: 0x0002D5AE
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x0002F3B6 File Offset: 0x0002D5B6
		[Editor(false)]
		public string DefaultSeperatorSpriteName
		{
			get
			{
				return this._defaultSeperatorSpriteName;
			}
			set
			{
				if (this._defaultSeperatorSpriteName != value)
				{
					this._defaultSeperatorSpriteName = value;
					base.OnPropertyChanged<string>(value, "DefaultSeperatorSpriteName");
					this.DefaultSeperatorSprite = base.Context.SpriteData.GetSprite(value);
				}
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0002F3F8 File Offset: 0x0002D5F8
		[Editor(false)]
		public string TitleBackgroundSpriteName
		{
			get
			{
				return this._titleBackgroundSpriteName;
			}
			set
			{
				if (this._titleBackgroundSpriteName != value)
				{
					this._titleBackgroundSpriteName = value;
					base.OnPropertyChanged<string>(value, "TitleBackgroundSpriteName");
					this.TitleBackgroundSprite = base.Context.SpriteData.GetSprite(value);
				}
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0002F432 File Offset: 0x0002D632
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x0002F43A File Offset: 0x0002D63A
		[Editor(false)]
		public Brush ValueNameTextBrush
		{
			get
			{
				return this._valueNameTextBrush;
			}
			set
			{
				if (this._valueNameTextBrush != value)
				{
					this._valueNameTextBrush = value;
					base.OnPropertyChanged<Brush>(value, "ValueNameTextBrush");
				}
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0002F458 File Offset: 0x0002D658
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0002F460 File Offset: 0x0002D660
		[Editor(false)]
		public Brush TitleTextBrush
		{
			get
			{
				return this._titleTextBrush;
			}
			set
			{
				if (this._titleTextBrush != value)
				{
					this._titleTextBrush = value;
					base.OnPropertyChanged<Brush>(value, "TitleTextBrush");
				}
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0002F47E File Offset: 0x0002D67E
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x0002F486 File Offset: 0x0002D686
		[Editor(false)]
		public Brush SubtextBrush
		{
			get
			{
				return this._subtextBrush;
			}
			set
			{
				if (this._subtextBrush != value)
				{
					this._subtextBrush = value;
					base.OnPropertyChanged<Brush>(value, "SubtextBrush");
				}
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		[Editor(false)]
		public Brush ValueTextBrush
		{
			get
			{
				return this._valueTextBrush;
			}
			set
			{
				if (this._valueTextBrush != value)
				{
					this._valueTextBrush = value;
					base.OnPropertyChanged<Brush>(value, "ValueTextBrush");
				}
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0002F4CA File Offset: 0x0002D6CA
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x0002F4D2 File Offset: 0x0002D6D2
		[Editor(false)]
		public Brush DescriptionTextBrush
		{
			get
			{
				return this._descriptionTextBrush;
			}
			set
			{
				if (this._descriptionTextBrush != value)
				{
					this._descriptionTextBrush = value;
					base.OnPropertyChanged<Brush>(value, "DescriptionTextBrush");
				}
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0002F4F0 File Offset: 0x0002D6F0
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x0002F4F8 File Offset: 0x0002D6F8
		[Editor(false)]
		public bool ModifyDefinitionColor
		{
			get
			{
				return this._modifyDefinitionColor;
			}
			set
			{
				if (this._modifyDefinitionColor != value)
				{
					this._modifyDefinitionColor = value;
					base.OnPropertyChanged(value, "ModifyDefinitionColor");
				}
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0002F516 File Offset: 0x0002D716
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x0002F51E File Offset: 0x0002D71E
		[Editor(false)]
		public RichTextWidget DefinitionLabel
		{
			get
			{
				return this._definitionLabel;
			}
			set
			{
				if (this._definitionLabel != value)
				{
					this._definitionLabel = value;
					base.OnPropertyChanged<RichTextWidget>(value, "DefinitionLabel");
				}
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0002F53C File Offset: 0x0002D73C
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x0002F544 File Offset: 0x0002D744
		[Editor(false)]
		public RichTextWidget ValueLabel
		{
			get
			{
				return this._valueLabel;
			}
			set
			{
				if (this._valueLabel != value)
				{
					this._valueLabel = value;
					base.OnPropertyChanged<RichTextWidget>(value, "ValueLabel");
				}
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0002F562 File Offset: 0x0002D762
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x0002F56A File Offset: 0x0002D76A
		[Editor(false)]
		public ListPanel ValueBackgroundSpriteWidget
		{
			get
			{
				return this._valueBackgroundSpriteWidget;
			}
			set
			{
				if (this._valueBackgroundSpriteWidget != value)
				{
					this._valueBackgroundSpriteWidget = value;
					base.OnPropertyChanged<ListPanel>(value, "ValueBackgroundSpriteWidget");
				}
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0002F588 File Offset: 0x0002D788
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x0002F590 File Offset: 0x0002D790
		[Editor(false)]
		public Widget DefinitionLabelContainer
		{
			get
			{
				return this._definitionLabelContainer;
			}
			set
			{
				if (this._definitionLabelContainer != value)
				{
					this._definitionLabelContainer = value;
					base.OnPropertyChanged<Widget>(value, "DefinitionLabelContainer");
				}
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0002F5AE File Offset: 0x0002D7AE
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x0002F5B6 File Offset: 0x0002D7B6
		[Editor(false)]
		public Widget ValueLabelContainer
		{
			get
			{
				return this._valueLabelContainer;
			}
			set
			{
				if (this._valueLabelContainer != value)
				{
					this._valueLabelContainer = value;
					base.OnPropertyChanged<Widget>(value, "ValueLabelContainer");
				}
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0002F5D4 File Offset: 0x0002D7D4
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x0002F5DC File Offset: 0x0002D7DC
		[Editor(false)]
		public Color TextColor
		{
			get
			{
				return this._textColor;
			}
			set
			{
				if (this._textColor != value)
				{
					this._textColor = value;
					base.OnPropertyChanged(value, "TextColor");
					this._useCustomColor = true;
				}
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0002F606 File Offset: 0x0002D806
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0002F60E File Offset: 0x0002D80E
		[Editor(false)]
		public int TextHeight
		{
			get
			{
				return this._textHeight;
			}
			set
			{
				if (this._textHeight != value)
				{
					this._textHeight = value;
					base.OnPropertyChanged(value, "TextHeight");
				}
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0002F62C File Offset: 0x0002D82C
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0002F634 File Offset: 0x0002D834
		[Editor(false)]
		public string DefinitionText
		{
			get
			{
				return this._definitionText;
			}
			set
			{
				if (this._definitionText != value)
				{
					this._definitionText = value;
					base.OnPropertyChanged<string>(value, "DefinitionText");
					this._firstFrame = true;
				}
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0002F65E File Offset: 0x0002D85E
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0002F666 File Offset: 0x0002D866
		[Editor(false)]
		public string ValueText
		{
			get
			{
				return this._valueText;
			}
			set
			{
				if (this._valueText != value)
				{
					this._valueText = value;
					base.OnPropertyChanged<string>(value, "ValueText");
					this._firstFrame = true;
				}
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0002F690 File Offset: 0x0002D890
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0002F698 File Offset: 0x0002D898
		[Editor(false)]
		public int PropertyModifier
		{
			get
			{
				return this._propertyModifier;
			}
			set
			{
				if (this._propertyModifier != value)
				{
					this._propertyModifier = value;
					base.OnPropertyChanged(value, "PropertyModifier");
				}
			}
		}

		// Token: 0x0400079F RID: 1951
		private const int HeaderSize = 30;

		// Token: 0x040007A0 RID: 1952
		private const int DefaultSize = 15;

		// Token: 0x040007A1 RID: 1953
		private const int SubTextSize = 10;

		// Token: 0x040007A3 RID: 1955
		private bool _isMultiLine;

		// Token: 0x040007A4 RID: 1956
		private bool _isBattleMode;

		// Token: 0x040007A5 RID: 1957
		private bool _isBattleModeOver;

		// Token: 0x040007A6 RID: 1958
		private bool _isCost;

		// Token: 0x040007A7 RID: 1959
		private bool _isRundownSeperator;

		// Token: 0x040007A8 RID: 1960
		private bool _isDefaultSeperator;

		// Token: 0x040007A9 RID: 1961
		private bool _isRundownResult;

		// Token: 0x040007AA RID: 1962
		private bool _isTitle;

		// Token: 0x040007AB RID: 1963
		private bool _isSubtext;

		// Token: 0x040007AC RID: 1964
		private bool _isEmptySpace;

		// Token: 0x040007AD RID: 1965
		private bool _isRelation;

		// Token: 0x040007AF RID: 1967
		private bool _useCustomColor;

		// Token: 0x040007B0 RID: 1968
		private Sprite RundownSeperatorSprite;

		// Token: 0x040007B1 RID: 1969
		private Sprite DefaultSeperatorSprite;

		// Token: 0x040007B2 RID: 1970
		private Sprite TitleBackgroundSprite;

		// Token: 0x040007B3 RID: 1971
		private Sprite _currentSprite;

		// Token: 0x040007B4 RID: 1972
		private float _maxValueLabelSizeX;

		// Token: 0x040007B5 RID: 1973
		private bool _firstFrame = true;

		// Token: 0x040007B6 RID: 1974
		private bool _modifyDefinitionColor = true;

		// Token: 0x040007B7 RID: 1975
		private Color _textColor;

		// Token: 0x040007B8 RID: 1976
		private RichTextWidget _definitionLabel;

		// Token: 0x040007B9 RID: 1977
		private RichTextWidget _valueLabel;

		// Token: 0x040007BA RID: 1978
		private Widget _definitionLabelContainer;

		// Token: 0x040007BB RID: 1979
		private Widget _valueLabelContainer;

		// Token: 0x040007BC RID: 1980
		private ListPanel _valueBackgroundSpriteWidget;

		// Token: 0x040007BD RID: 1981
		private int _textHeight;

		// Token: 0x040007BE RID: 1982
		private Brush _titleTextBrush;

		// Token: 0x040007BF RID: 1983
		private Brush _subtextBrush;

		// Token: 0x040007C0 RID: 1984
		private Brush _valueTextBrush;

		// Token: 0x040007C1 RID: 1985
		private Brush _descriptionTextBrush;

		// Token: 0x040007C2 RID: 1986
		private Brush _valueNameTextBrush;

		// Token: 0x040007C3 RID: 1987
		private string _rundownSeperatorSpriteName;

		// Token: 0x040007C4 RID: 1988
		private string _defaultSeperatorSpriteName;

		// Token: 0x040007C5 RID: 1989
		private string _titleBackgroundSpriteName;

		// Token: 0x040007C6 RID: 1990
		private string _definitionText;

		// Token: 0x040007C7 RID: 1991
		private string _valueText;

		// Token: 0x040007C8 RID: 1992
		private int _propertyModifier;

		// Token: 0x020001B8 RID: 440
		[Flags]
		public enum TooltipPropertyFlags
		{
			// Token: 0x040009D6 RID: 2518
			None = 0,
			// Token: 0x040009D7 RID: 2519
			MultiLine = 1,
			// Token: 0x040009D8 RID: 2520
			BattleMode = 2,
			// Token: 0x040009D9 RID: 2521
			BattleModeOver = 4,
			// Token: 0x040009DA RID: 2522
			WarFirstEnemy = 8,
			// Token: 0x040009DB RID: 2523
			WarFirstAlly = 16,
			// Token: 0x040009DC RID: 2524
			WarFirstNeutral = 32,
			// Token: 0x040009DD RID: 2525
			WarSecondEnemy = 64,
			// Token: 0x040009DE RID: 2526
			WarSecondAlly = 128,
			// Token: 0x040009DF RID: 2527
			WarSecondNeutral = 256,
			// Token: 0x040009E0 RID: 2528
			RundownSeperator = 512,
			// Token: 0x040009E1 RID: 2529
			DefaultSeperator = 1024,
			// Token: 0x040009E2 RID: 2530
			Cost = 2048,
			// Token: 0x040009E3 RID: 2531
			Title = 4096,
			// Token: 0x040009E4 RID: 2532
			RundownResult = 8192
		}
	}
}
