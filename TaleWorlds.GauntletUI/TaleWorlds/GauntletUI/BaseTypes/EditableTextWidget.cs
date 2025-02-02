using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.InputSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200005E RID: 94
	public class EditableTextWidget : BrushWidget
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000193F0 File Offset: 0x000175F0
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000193F8 File Offset: 0x000175F8
		public int MaxLength { get; set; } = -1;

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00019401 File Offset: 0x00017601
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00019409 File Offset: 0x00017609
		public bool IsObfuscationEnabled
		{
			get
			{
				return this._isObfuscationEnabled;
			}
			set
			{
				if (value != this._isObfuscationEnabled)
				{
					this._isObfuscationEnabled = value;
					this.OnObfuscationToggled(value);
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00019424 File Offset: 0x00017624
		private Vector2 LocalMousePosition
		{
			get
			{
				Vector2 mousePosition = base.EventManager.MousePosition;
				Vector2 globalPosition = base.GlobalPosition;
				float x = mousePosition.X - globalPosition.X;
				float y = mousePosition.Y - globalPosition.Y;
				return new Vector2(x, y);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00019465 File Offset: 0x00017665
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0001946D File Offset: 0x0001766D
		public string DefaultSearchText { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00019476 File Offset: 0x00017676
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00019480 File Offset: 0x00017680
		[Editor(false)]
		public string RealText
		{
			get
			{
				return this._realText;
			}
			set
			{
				if (this._realText != value)
				{
					this._editableText.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
					if (string.IsNullOrEmpty(value))
					{
						value = "";
					}
					this.Text = (this.IsObfuscationEnabled ? this.ObfuscateText(value) : value);
					this._realText = value;
					base.OnPropertyChanged<string>(value, "RealText");
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000194F0 File Offset: 0x000176F0
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x000194F8 File Offset: 0x000176F8
		[Editor(false)]
		public string KeyboardInfoText
		{
			get
			{
				return this._keyboardInfoText;
			}
			set
			{
				if (this._keyboardInfoText != value)
				{
					this._keyboardInfoText = value;
					base.OnPropertyChanged<string>(value, "KeyboardInfoText");
				}
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001951B File Offset: 0x0001771B
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00019528 File Offset: 0x00017728
		[Editor(false)]
		public string Text
		{
			get
			{
				return this._editableText.VisibleText;
			}
			set
			{
				if (this._editableText.VisibleText != value)
				{
					this._editableText.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
					this._editableText.VisibleText = value;
					if (!string.IsNullOrEmpty(this._editableText.VisibleText))
					{
						this._editableText.SetCursor(this._editableText.VisibleText.Length, true, false);
					}
					if (string.IsNullOrEmpty(value))
					{
						this._editableText.VisibleText = "";
						this._editableText.SetCursor(0, base.IsFocused, false);
					}
					this.RealText = value;
					base.OnPropertyChanged<string>(value, "Text");
					base.SetMeasureAndLayoutDirty();
				}
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000195E8 File Offset: 0x000177E8
		public EditableTextWidget(UIContext context) : base(context)
		{
			FontFactory fontFactory = context.FontFactory;
			this._editableText = new EditableText((int)base.Size.X, (int)base.Size.Y, fontFactory.DefaultFont, new Func<int, Font>(fontFactory.GetUsableFontForCharacter));
			base.LayoutImp = new TextLayout(this._editableText);
			this._realText = "";
			this._textHeight = -1;
			this._cursorVisible = false;
			this._lastFontBrush = null;
			this._cursorDirection = EditableTextWidget.CursorMovementDirection.None;
			this._keyboardAction = EditableTextWidget.KeyboardAction.None;
			this._nextRepeatTime = int.MinValue;
			this._isSelection = false;
			base.IsFocusable = true;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000196C0 File Offset: 0x000178C0
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.UpdateText();
			if (base.IsFocused && base.IsEnabled)
			{
				this._editableText.BlinkTimer += dt;
				if (this._editableText.BlinkTimer > 0.5f)
				{
					this._editableText.BlinkCursor();
					this._editableText.BlinkTimer = 0f;
				}
				if (base.ContainsState("Selected"))
				{
					this.SetState("Selected");
				}
			}
			else if (this._editableText.IsCursorVisible())
			{
				this._editableText.BlinkCursor();
			}
			this.SetEditTextParameters();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00019764 File Offset: 0x00017964
		private void SetEditTextParameters()
		{
			bool flag = false;
			this._editableText.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
			this.UpdateFontData();
			if (this._editableText.HorizontalAlignment != base.ReadOnlyBrush.TextHorizontalAlignment)
			{
				this._editableText.HorizontalAlignment = base.ReadOnlyBrush.TextHorizontalAlignment;
				flag = true;
			}
			if (this._editableText.VerticalAlignment != base.ReadOnlyBrush.TextVerticalAlignment)
			{
				this._editableText.VerticalAlignment = base.ReadOnlyBrush.TextVerticalAlignment;
				flag = true;
			}
			if (this._editableText.TextHeight != this._textHeight)
			{
				this._textHeight = this._editableText.TextHeight;
				flag = true;
			}
			if (flag)
			{
				base.SetMeasureAndLayoutDirty();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019823 File Offset: 0x00017A23
		protected void BlinkCursor()
		{
			this._cursorVisible = !this._cursorVisible;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00019834 File Offset: 0x00017A34
		protected void ResetSelected()
		{
			this._editableText.ResetSelected();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00019844 File Offset: 0x00017A44
		protected void DeleteChar(bool nextChar = false)
		{
			int num = this._editableText.CursorPosition;
			if (nextChar)
			{
				num++;
			}
			if (num == 0 || num > this.Text.Length)
			{
				return;
			}
			if (this.IsObfuscationEnabled)
			{
				this.RealText = this.RealText.Substring(0, num - 1) + this.RealText.Substring(num, this.RealText.Length - num);
				this.Text = this.ObfuscateText(this.RealText);
			}
			else
			{
				this.Text = this.Text.Substring(0, num - 1) + this.Text.Substring(num, this.Text.Length - num);
				this.RealText = this.Text;
			}
			this._editableText.SetCursor(num - 1, true, false);
			this.ResetSelected();
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001991A File Offset: 0x00017B1A
		protected int FindNextWordPosition(int direction)
		{
			return this._editableText.FindNextWordPosition(direction);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00019928 File Offset: 0x00017B28
		protected void MoveCursor(int direction, bool withSelection = false)
		{
			if (!withSelection)
			{
				this.ResetSelected();
			}
			this._editableText.SetCursor(this._editableText.CursorPosition + direction, true, withSelection);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00019950 File Offset: 0x00017B50
		protected string GetAppendCharacterResult(int charCode)
		{
			if (this.MaxLength > -1 && this.Text.Length >= this.MaxLength)
			{
				return this.RealText;
			}
			int cursorPosition = this._editableText.CursorPosition;
			char c = Convert.ToChar(charCode);
			return this.RealText.Substring(0, cursorPosition) + c.ToString() + this.RealText.Substring(cursorPosition, this.RealText.Length - cursorPosition);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000199C8 File Offset: 0x00017BC8
		protected void AppendCharacter(int charCode)
		{
			if (this.MaxLength > -1 && this.Text.Length >= this.MaxLength)
			{
				return;
			}
			int cursorPosition = this._editableText.CursorPosition;
			this.RealText = this.GetAppendCharacterResult(charCode);
			this.Text = (this.IsObfuscationEnabled ? this.ObfuscateText(this.RealText) : this.RealText);
			this._editableText.SetCursor(cursorPosition + 1, true, false);
			this.ResetSelected();
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00019A44 File Offset: 0x00017C44
		protected void AppendText(string text)
		{
			if (this.MaxLength > -1 && this.Text.Length >= this.MaxLength)
			{
				return;
			}
			if (this.MaxLength > -1 && this.Text.Length + text.Length >= this.MaxLength)
			{
				text = text.Substring(0, this.MaxLength - this.Text.Length);
			}
			int cursorPosition = this._editableText.CursorPosition;
			this.RealText = this.RealText.Substring(0, cursorPosition) + text + this.RealText.Substring(cursorPosition, this.RealText.Length - cursorPosition);
			this.Text = (this.IsObfuscationEnabled ? this.ObfuscateText(this.RealText) : this.RealText);
			this._editableText.SetCursor(cursorPosition + text.Length, true, false);
			this.ResetSelected();
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00019B28 File Offset: 0x00017D28
		protected void DeleteText(int beginIndex, int endIndex)
		{
			if (beginIndex == endIndex)
			{
				return;
			}
			this.RealText = this.RealText.Substring(0, beginIndex) + this.RealText.Substring(endIndex, this.RealText.Length - endIndex);
			this.Text = (this.IsObfuscationEnabled ? this.ObfuscateText(this.RealText) : this.RealText);
			this._editableText.SetCursor(beginIndex, true, false);
			this.ResetSelected();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00019BA4 File Offset: 0x00017DA4
		protected void CopyText(int beginIndex, int endIndex)
		{
			if (beginIndex == endIndex)
			{
				return;
			}
			int num = Math.Min(beginIndex, endIndex);
			int num2 = Math.Max(beginIndex, endIndex);
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > this.RealText.Length)
			{
				num2 = this.RealText.Length;
			}
			Input.SetClipboardText(this.RealText.Substring(num, num2 - num));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00019BFC File Offset: 0x00017DFC
		protected void PasteText()
		{
			string text = Regex.Replace(Input.GetClipboardText(), "[<>]+", " ");
			this.AppendText(text);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00019C28 File Offset: 0x00017E28
		public override void HandleInput(IReadOnlyList<int> lastKeysPressed)
		{
			if (base.IsDisabled)
			{
				return;
			}
			int count = lastKeysPressed.Count;
			for (int i = 0; i < count; i++)
			{
				int num = lastKeysPressed[i];
				if (num >= 32 && (num < 127 || num >= 160))
				{
					if (num != 60 && num != 62)
					{
						this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
						this.AppendCharacter(num);
					}
					this._cursorDirection = EditableTextWidget.CursorMovementDirection.None;
					this._isSelection = false;
				}
			}
			int tickCount = Environment.TickCount;
			bool flag = false;
			bool flag2 = false;
			if (Input.IsKeyPressed(InputKey.Left))
			{
				this._cursorDirection = EditableTextWidget.CursorMovementDirection.Left;
				flag = true;
			}
			else if (Input.IsKeyPressed(InputKey.Right))
			{
				this._cursorDirection = EditableTextWidget.CursorMovementDirection.Right;
				flag = true;
			}
			else if ((this._cursorDirection == EditableTextWidget.CursorMovementDirection.Left && !Input.IsKeyDown(InputKey.Left)) || (this._cursorDirection == EditableTextWidget.CursorMovementDirection.Right && !Input.IsKeyDown(InputKey.Right)))
			{
				this._cursorDirection = EditableTextWidget.CursorMovementDirection.None;
				if (!Input.IsKeyDown(InputKey.LeftShift))
				{
					this._isSelection = false;
				}
			}
			else if (Input.IsKeyReleased(InputKey.LeftShift))
			{
				this._isSelection = false;
			}
			else if (Input.IsKeyDown(InputKey.Home))
			{
				this._cursorDirection = EditableTextWidget.CursorMovementDirection.Left;
				flag2 = true;
			}
			else if (Input.IsKeyDown(InputKey.End))
			{
				this._cursorDirection = EditableTextWidget.CursorMovementDirection.Right;
				flag2 = true;
			}
			if (flag || flag2)
			{
				if (flag)
				{
					this._nextRepeatTime = tickCount + 500;
				}
				if (Input.IsKeyDown(InputKey.LeftShift))
				{
					if (!this._editableText.IsAnySelected())
					{
						this._editableText.BeginSelection();
					}
					this._isSelection = true;
				}
			}
			if (this._cursorDirection != EditableTextWidget.CursorMovementDirection.None)
			{
				if (flag || tickCount >= this._nextRepeatTime)
				{
					int direction = (int)this._cursorDirection;
					if (Input.IsKeyDown(InputKey.LeftControl))
					{
						direction = this.FindNextWordPosition(direction) - this._editableText.CursorPosition;
					}
					this.MoveCursor(direction, this._isSelection);
					if (tickCount >= this._nextRepeatTime)
					{
						this._nextRepeatTime = tickCount + 30;
					}
				}
				else if (flag2)
				{
					int direction2 = (this._cursorDirection == EditableTextWidget.CursorMovementDirection.Left) ? (-this._editableText.CursorPosition) : (this._editableText.VisibleText.Length - this._editableText.CursorPosition);
					this.MoveCursor(direction2, this._isSelection);
				}
			}
			bool flag3 = false;
			if (Input.IsKeyPressed(InputKey.BackSpace))
			{
				flag3 = true;
				this._keyboardAction = EditableTextWidget.KeyboardAction.BackSpace;
				this._nextRepeatTime = tickCount + 500;
			}
			else if (Input.IsKeyPressed(InputKey.Delete))
			{
				flag3 = true;
				this._keyboardAction = EditableTextWidget.KeyboardAction.Delete;
				this._nextRepeatTime = tickCount + 500;
			}
			if ((this._keyboardAction == EditableTextWidget.KeyboardAction.BackSpace && !Input.IsKeyDown(InputKey.BackSpace)) || (this._keyboardAction == EditableTextWidget.KeyboardAction.Delete && !Input.IsKeyDown(InputKey.Delete)))
			{
				this._keyboardAction = EditableTextWidget.KeyboardAction.None;
			}
			if (Input.IsKeyReleased(InputKey.Enter) || Input.IsKeyReleased(InputKey.NumpadEnter))
			{
				base.EventFired("TextEntered", Array.Empty<object>());
				return;
			}
			if (this._keyboardAction == EditableTextWidget.KeyboardAction.BackSpace || this._keyboardAction == EditableTextWidget.KeyboardAction.Delete)
			{
				if (flag3 || tickCount >= this._nextRepeatTime)
				{
					if (this._editableText.IsAnySelected())
					{
						this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					}
					else if (Input.IsKeyDown(InputKey.LeftControl))
					{
						int num2 = this.FindNextWordPosition(-1) - this._editableText.CursorPosition;
						this.DeleteText(this._editableText.CursorPosition + num2, this._editableText.CursorPosition);
					}
					else
					{
						this.DeleteChar(this._keyboardAction == EditableTextWidget.KeyboardAction.Delete);
					}
					if (tickCount >= this._nextRepeatTime)
					{
						this._nextRepeatTime = tickCount + 30;
						return;
					}
				}
			}
			else if (Input.IsKeyDown(InputKey.LeftControl))
			{
				if (Input.IsKeyPressed(InputKey.A))
				{
					this._editableText.SelectAll();
					return;
				}
				if (Input.IsKeyPressed(InputKey.C))
				{
					this.CopyText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					return;
				}
				if (Input.IsKeyPressed(InputKey.X))
				{
					this.CopyText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					return;
				}
				if (Input.IsKeyPressed(InputKey.V))
				{
					this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					this.PasteText();
				}
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001A064 File Offset: 0x00018264
		protected internal override void OnGainFocus()
		{
			base.OnGainFocus();
			this._editableText.SetCursor(this.RealText.Length, true, false);
			if (string.IsNullOrEmpty(this.RealText) && !string.IsNullOrEmpty(this.DefaultSearchText))
			{
				this._editableText.VisibleText = "";
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001A0BC File Offset: 0x000182BC
		protected internal override void OnLoseFocus()
		{
			base.OnLoseFocus();
			this._editableText.ResetSelected();
			this._isSelection = false;
			this._editableText.SetCursor(0, false, false);
			if (string.IsNullOrEmpty(this.RealText) && !string.IsNullOrEmpty(this.DefaultSearchText))
			{
				this._editableText.VisibleText = this.DefaultSearchText;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001A11C File Offset: 0x0001831C
		private void UpdateText()
		{
			if (base.IsDisabled)
			{
				this.SetState("Disabled");
			}
			else if (base.IsPressed)
			{
				this.SetState("Pressed");
			}
			else if (base.IsHovered)
			{
				this.SetState("Hovered");
			}
			else
			{
				this.SetState("Default");
			}
			if (string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.DefaultSearchText) && this._mouseState == EditableTextWidget.MouseState.None && base.EventManager.FocusedWidget != this)
			{
				this._editableText.VisibleText = this.DefaultSearchText;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001A1B4 File Offset: 0x000183B4
		private void UpdateFontData()
		{
			if (this._lastFontBrush == base.ReadOnlyBrush && this._lastScale == base._scaleToUse && this._lastLanguageCode == base.Context.FontFactory.CurrentLangageID)
			{
				return;
			}
			this._editableText.StyleFontContainer.ClearFonts();
			foreach (Style style in base.ReadOnlyBrush.Styles)
			{
				Font font;
				if (style.Font != null)
				{
					font = style.Font;
				}
				else if (base.ReadOnlyBrush.Font != null)
				{
					font = base.ReadOnlyBrush.Font;
				}
				else
				{
					font = base.Context.FontFactory.DefaultFont;
				}
				Font mappedFontForLocalization = base.Context.FontFactory.GetMappedFontForLocalization(font.Name);
				this._editableText.StyleFontContainer.Add(style.Name, mappedFontForLocalization, (float)base.ReadOnlyBrush.FontSize * base._scaleToUse);
			}
			this._lastFontBrush = base.ReadOnlyBrush;
			this._lastScale = base._scaleToUse;
			this._lastLanguageCode = base.Context.FontFactory.CurrentLangageID;
			this._editableText.CurrentLanguage = base.Context.FontFactory.GetCurrentLanguage();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001A320 File Offset: 0x00018520
		private Font GetFont(Style style = null)
		{
			if (((style != null) ? style.Font : null) != null)
			{
				return base.Context.FontFactory.GetMappedFontForLocalization(style.Font.Name);
			}
			if (base.ReadOnlyBrush.Font != null)
			{
				return base.Context.FontFactory.GetMappedFontForLocalization(base.ReadOnlyBrush.Font.Name);
			}
			return base.Context.FontFactory.DefaultFont;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001A398 File Offset: 0x00018598
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.Size.X > 0f && base.Size.Y > 0f)
			{
				Vector2 localMousePosition = this.LocalMousePosition;
				bool focus = this._mouseState == EditableTextWidget.MouseState.Down;
				this._editableText.UpdateSize((int)base.Size.X, (int)base.Size.Y);
				this.SetEditTextParameters();
				this.UpdateFontData();
				bool isFixedWidth = base.WidthSizePolicy != SizePolicy.CoverChildren || base.MaxWidth != 0f;
				bool isFixedHeight = base.HeightSizePolicy != SizePolicy.CoverChildren || base.MaxHeight != 0f;
				this._editableText.Update(base.Context.SpriteData, localMousePosition, focus, isFixedWidth, isFixedHeight, base._scaleToUse);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001A470 File Offset: 0x00018670
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			base.OnRender(twoDimensionContext, drawContext);
			if (!string.IsNullOrEmpty(this._editableText.Value))
			{
				Vector2 globalPosition = base.GlobalPosition;
				foreach (RichTextPart richTextPart in this._editableText.GetParts())
				{
					DrawObject2D drawObject2D = richTextPart.DrawObject2D;
					Style styleOrDefault = base.ReadOnlyBrush.GetStyleOrDefault(richTextPart.Style);
					Font font = this.GetFont(styleOrDefault);
					int fontSize = styleOrDefault.FontSize;
					float scaleFactor = (float)fontSize * base._scaleToUse;
					float num = (float)fontSize / (float)font.Size;
					float height = (float)font.LineHeight * num * base._scaleToUse;
					TextMaterial textMaterial = styleOrDefault.CreateTextMaterial(drawContext);
					textMaterial.ColorFactor *= base.ReadOnlyBrush.GlobalColorFactor;
					textMaterial.AlphaFactor *= base.ReadOnlyBrush.GlobalAlphaFactor;
					textMaterial.Color *= base.ReadOnlyBrush.GlobalColor;
					textMaterial.Texture = font.FontSprite.Texture;
					textMaterial.ScaleFactor = scaleFactor;
					textMaterial.Smooth = font.Smooth;
					textMaterial.SmoothingConstant = font.SmoothingConstant;
					if (textMaterial.GlowRadius > 0f || textMaterial.Blur > 0f || textMaterial.OutlineAmount > 0f)
					{
						TextMaterial textMaterial2 = styleOrDefault.CreateTextMaterial(drawContext);
						textMaterial2.CopyFrom(textMaterial);
						drawContext.Draw(globalPosition.X, globalPosition.Y, textMaterial2, drawObject2D, base.Size.X, base.Size.Y);
					}
					textMaterial.GlowRadius = 0f;
					textMaterial.Blur = 0f;
					textMaterial.OutlineAmount = 0f;
					Material material = textMaterial;
					if (richTextPart.Style == "Highlight")
					{
						SpriteData spriteData = base.Context.SpriteData;
						string name = "warm_overlay";
						Sprite sprite = spriteData.GetSprite(name);
						SimpleMaterial simpleMaterial = drawContext.CreateSimpleMaterial();
						simpleMaterial.Reset((sprite != null) ? sprite.Texture : null);
						drawContext.DrawSprite(sprite, simpleMaterial, globalPosition.X + richTextPart.PartPosition.X, globalPosition.Y + richTextPart.PartPosition.Y, 1f, richTextPart.WordWidth, height, false, false);
					}
					drawContext.Draw(globalPosition.X, globalPosition.Y, material, drawObject2D, base.Size.X, base.Size.Y);
				}
				if (this._editableText.IsCursorVisible())
				{
					Style styleOrDefault2 = base.ReadOnlyBrush.GetStyleOrDefault("Default");
					Font font2 = this.GetFont(styleOrDefault2);
					int fontSize2 = styleOrDefault2.FontSize;
					float num2 = (float)fontSize2 / (float)font2.Size;
					float height2 = (float)font2.LineHeight * num2 * base._scaleToUse;
					SpriteData spriteData2 = base.Context.SpriteData;
					string name2 = "BlankWhiteSquare_9";
					Sprite sprite2 = spriteData2.GetSprite(name2);
					SimpleMaterial simpleMaterial2 = drawContext.CreateSimpleMaterial();
					simpleMaterial2.Reset((sprite2 != null) ? sprite2.Texture : null);
					Vector2 cursorPosition = this._editableText.GetCursorPosition(font2, (float)fontSize2, base._scaleToUse);
					drawContext.DrawSprite(sprite2, simpleMaterial2, (float)((int)(globalPosition.X + cursorPosition.X)), globalPosition.Y + cursorPosition.Y, 1f, 1f, height2, false, false);
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001A800 File Offset: 0x00018A00
		protected internal override void OnMousePressed()
		{
			base.OnMousePressed();
			this._mouseDownPosition = this.LocalMousePosition;
			this._mouseState = EditableTextWidget.MouseState.Down;
			this._editableText.HighlightStart = true;
			this._editableText.HighlightEnd = false;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001A833 File Offset: 0x00018A33
		protected internal override void OnMouseReleased()
		{
			base.OnMouseReleased();
			this._mouseState = EditableTextWidget.MouseState.Up;
			this._editableText.HighlightEnd = true;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001A84E File Offset: 0x00018A4E
		private void OnObfuscationToggled(bool isEnabled)
		{
			if (isEnabled)
			{
				this.Text = this.ObfuscateText(this.RealText);
				return;
			}
			this.Text = this.RealText;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001A872 File Offset: 0x00018A72
		private string ObfuscateText(string stringToObfuscate)
		{
			return new string(this._obfuscationChar, stringToObfuscate.Length);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001A888 File Offset: 0x00018A88
		public virtual void SetAllText(string text)
		{
			this.DeleteText(0, this.RealText.Length);
			string text2 = Regex.Replace(text, "[<>]+", " ");
			this.AppendText(text2);
		}

		// Token: 0x040002D6 RID: 726
		protected EditableText _editableText;

		// Token: 0x040002D7 RID: 727
		protected readonly char _obfuscationChar = '*';

		// Token: 0x040002D8 RID: 728
		protected float _lastScale = -1f;

		// Token: 0x040002D9 RID: 729
		protected bool _isObfuscationEnabled;

		// Token: 0x040002DA RID: 730
		protected string _lastLanguageCode;

		// Token: 0x040002DB RID: 731
		protected Brush _lastFontBrush;

		// Token: 0x040002DC RID: 732
		protected EditableTextWidget.MouseState _mouseState;

		// Token: 0x040002DD RID: 733
		protected Vector2 _mouseDownPosition;

		// Token: 0x040002DE RID: 734
		protected bool _cursorVisible;

		// Token: 0x040002DF RID: 735
		protected int _textHeight;

		// Token: 0x040002E0 RID: 736
		protected EditableTextWidget.CursorMovementDirection _cursorDirection;

		// Token: 0x040002E1 RID: 737
		protected EditableTextWidget.KeyboardAction _keyboardAction;

		// Token: 0x040002E2 RID: 738
		protected int _nextRepeatTime;

		// Token: 0x040002E3 RID: 739
		protected bool _isSelection;

		// Token: 0x040002E6 RID: 742
		private string _realText = "";

		// Token: 0x040002E7 RID: 743
		private string _keyboardInfoText = "";

		// Token: 0x0200008E RID: 142
		protected enum MouseState
		{
			// Token: 0x04000474 RID: 1140
			None,
			// Token: 0x04000475 RID: 1141
			Down,
			// Token: 0x04000476 RID: 1142
			Up
		}

		// Token: 0x0200008F RID: 143
		protected enum CursorMovementDirection
		{
			// Token: 0x04000478 RID: 1144
			None,
			// Token: 0x04000479 RID: 1145
			Left = -1,
			// Token: 0x0400047A RID: 1146
			Right = 1
		}

		// Token: 0x02000090 RID: 144
		protected enum KeyboardAction
		{
			// Token: 0x0400047C RID: 1148
			None,
			// Token: 0x0400047D RID: 1149
			BackSpace,
			// Token: 0x0400047E RID: 1150
			Delete
		}
	}
}
