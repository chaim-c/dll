﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TaleWorlds.InputSystem;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200005F RID: 95
	public class FloatInputTextWidget : EditableTextWidget
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0001A8BF File Offset: 0x00018ABF
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0001A8C7 File Offset: 0x00018AC7
		public bool EnableClamp { get; set; }

		// Token: 0x06000627 RID: 1575 RVA: 0x0001A8D0 File Offset: 0x00018AD0
		public FloatInputTextWidget(UIContext context) : base(context)
		{
			base.PropertyChanged += this.IntegerInputTextWidget_PropertyChanged;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001A904 File Offset: 0x00018B04
		private void IntegerInputTextWidget_PropertyChanged(PropertyOwnerObject arg1, string arg2, object arg3)
		{
			float floatText;
			if (arg2 == "RealText" && (string)arg3 != this.FloatText.ToString() && float.TryParse((string)arg3, out floatText))
			{
				this.FloatText = floatText;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001A950 File Offset: 0x00018B50
		public override void HandleInput(IReadOnlyList<int> lastKeysPressed)
		{
			int count = lastKeysPressed.Count;
			for (int i = 0; i < count; i++)
			{
				int num = lastKeysPressed[i];
				char c2 = Convert.ToChar(num);
				if (char.IsDigit(c2) || (c2 == '.' && this.GetNumberOfSeperatorsInText(base.RealText) == 0))
				{
					float num2;
					if (num != 60 && num != 62 && float.TryParse(this.GetAppendResult(num), out num2))
					{
						this.HandleInput(num);
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
				this._nextRepeatTime = tickCount + 500;
				if (Input.IsKeyDown(InputKey.LeftShift))
				{
					if (!this._editableText.IsAnySelected())
					{
						this._editableText.BeginSelection();
					}
					this._isSelection = true;
				}
			}
			if (this._cursorDirection != EditableTextWidget.CursorMovementDirection.None && (flag || flag2 || tickCount >= this._nextRepeatTime))
			{
				if (flag)
				{
					int direction = (int)this._cursorDirection;
					if (Input.IsKeyDown(InputKey.LeftControl))
					{
						direction = base.FindNextWordPosition(direction) - this._editableText.CursorPosition;
					}
					base.MoveCursor(direction, this._isSelection);
					if (tickCount >= this._nextRepeatTime)
					{
						this._nextRepeatTime = tickCount + 30;
					}
				}
				else if (flag2)
				{
					int direction2 = (this._cursorDirection == EditableTextWidget.CursorMovementDirection.Left) ? (-this._editableText.CursorPosition) : (this._editableText.VisibleText.Length - this._editableText.CursorPosition);
					base.MoveCursor(direction2, this._isSelection);
					if (tickCount >= this._nextRepeatTime)
					{
						this._nextRepeatTime = tickCount + 30;
					}
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
						base.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					}
					else if (Input.IsKeyDown(InputKey.LeftControl))
					{
						int num3 = base.FindNextWordPosition(-1) - this._editableText.CursorPosition;
						base.DeleteText(this._editableText.CursorPosition + num3, this._editableText.CursorPosition);
					}
					else
					{
						base.DeleteChar(this._keyboardAction == EditableTextWidget.KeyboardAction.Delete);
					}
					this.TrySetStringAsFloat(base.RealText);
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
					base.CopyText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					return;
				}
				if (Input.IsKeyPressed(InputKey.X))
				{
					base.CopyText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					base.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					this.TrySetStringAsFloat(base.RealText);
					return;
				}
				if (Input.IsKeyPressed(InputKey.V))
				{
					base.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
					string text = Regex.Replace(Input.GetClipboardText(), "[<>]+", " ");
					text = new string((from c in text
					where char.IsDigit(c)
					select c).ToArray<char>());
					base.AppendText(text);
					this.TrySetStringAsFloat(base.RealText);
				}
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001AE14 File Offset: 0x00019014
		private void HandleInput(int lastPressedKey)
		{
			string text = null;
			bool flag = false;
			if (base.MaxLength > -1 && base.Text.Length >= base.MaxLength)
			{
				text = base.RealText;
			}
			if (text == null)
			{
				string text2 = base.RealText;
				if (this._editableText.SelectedTextBegin != this._editableText.SelectedTextEnd)
				{
					if (this._editableText.SelectedTextEnd > base.RealText.Length)
					{
						text = Convert.ToChar(lastPressedKey).ToString();
						flag = true;
					}
					else
					{
						text2 = base.RealText.Substring(0, this._editableText.SelectedTextBegin) + base.RealText.Substring(this._editableText.SelectedTextEnd, base.RealText.Length - this._editableText.SelectedTextEnd);
						if (this._editableText.SelectedTextEnd - this._editableText.SelectedTextBegin >= base.RealText.Length)
						{
							this._editableText.SetCursorPosition(0, true);
							this._editableText.ResetSelected();
							flag = true;
						}
						else
						{
							this._editableText.SetCursorPosition(this._editableText.SelectedTextBegin, true);
						}
						int cursorPosition = this._editableText.CursorPosition;
						char c = Convert.ToChar(lastPressedKey);
						text = text2.Substring(0, cursorPosition) + c.ToString() + text2.Substring(cursorPosition, text2.Length - cursorPosition);
					}
					this._editableText.ResetSelected();
				}
				else
				{
					if (this._editableText.CursorPosition == base.RealText.Length)
					{
						flag = true;
					}
					int cursorPosition2 = this._editableText.CursorPosition;
					char c2 = Convert.ToChar(lastPressedKey);
					text = text2.Substring(0, cursorPosition2) + c2.ToString() + text2.Substring(cursorPosition2, text2.Length - cursorPosition2);
					if (!flag)
					{
						this._editableText.SetCursor(cursorPosition2 + 1, true, false);
					}
				}
			}
			this.TrySetStringAsFloat(text);
			if (flag)
			{
				this._editableText.SetCursorPosition(base.RealText.Length, true);
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001B014 File Offset: 0x00019214
		private bool TrySetStringAsFloat(string str)
		{
			float @float;
			if (float.TryParse(str, out @float))
			{
				this.SetFloat(@float);
				if (this._editableText.SelectedTextEnd - this._editableText.SelectedTextBegin >= base.RealText.Length)
				{
					this._editableText.SetCursorPosition(0, true);
					this._editableText.ResetSelected();
				}
				else if (this._editableText.SelectedTextBegin != 0 || this._editableText.SelectedTextEnd != 0)
				{
					this._editableText.SetCursorPosition(this._editableText.SelectedTextBegin, true);
				}
				if (this._editableText.CursorPosition > base.RealText.Length)
				{
					this._editableText.SetCursorPosition(base.RealText.Length, true);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001B0D8 File Offset: 0x000192D8
		private void SetFloat(float newFloat)
		{
			if (this.EnableClamp && (newFloat > this.MaxFloat || newFloat < this.MinFloat))
			{
				newFloat = ((newFloat > this.MaxFloat) ? this.MaxFloat : this.MinFloat);
				base.ResetSelected();
			}
			this.FloatText = newFloat;
			if (this.FloatText.ToString() != base.RealText)
			{
				base.RealText = this.FloatText.ToString();
				base.Text = this.FloatText.ToString();
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001B168 File Offset: 0x00019368
		private int GetNumberOfSeperatorsInText(string realText)
		{
			return realText.Count((char c) => char.IsPunctuation(c));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001B190 File Offset: 0x00019390
		private string GetAppendResult(int lastPressedKey)
		{
			if (base.MaxLength > -1 && base.Text.Length >= base.MaxLength)
			{
				return base.RealText;
			}
			string realText = base.RealText;
			if (this._editableText.SelectedTextBegin != this._editableText.SelectedTextEnd)
			{
				base.RealText.Substring(0, this._editableText.SelectedTextBegin) + base.RealText.Substring(this._editableText.SelectedTextEnd, base.RealText.Length - this._editableText.SelectedTextEnd);
			}
			int cursorPosition = this._editableText.CursorPosition;
			char c = Convert.ToChar(lastPressedKey);
			return base.RealText.Substring(0, cursorPosition) + c.ToString() + base.RealText.Substring(cursorPosition, base.RealText.Length - cursorPosition);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001B270 File Offset: 0x00019470
		public override void SetAllText(string text)
		{
			base.DeleteText(0, base.RealText.Length);
			string text2 = Regex.Replace(text, "[<>]+", " ");
			text2 = new string((from c in text2
			where char.IsDigit(c)
			select c).ToArray<char>());
			base.AppendText(text2);
			this.TrySetStringAsFloat(text2);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001B2DF File Offset: 0x000194DF
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001B2E7 File Offset: 0x000194E7
		[Editor(false)]
		public float FloatText
		{
			get
			{
				return this._floatText;
			}
			set
			{
				if (this._floatText != value)
				{
					this._floatText = value;
					base.OnPropertyChanged(value, "FloatText");
					base.RealText = value.ToString();
					base.Text = value.ToString();
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001B31F File Offset: 0x0001951F
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001B327 File Offset: 0x00019527
		[Editor(false)]
		public float MaxFloat
		{
			get
			{
				return this._maxFloat;
			}
			set
			{
				if (this._maxFloat != value)
				{
					this._maxFloat = value;
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001B339 File Offset: 0x00019539
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001B341 File Offset: 0x00019541
		[Editor(false)]
		public float MinFloat
		{
			get
			{
				return this._minFloat;
			}
			set
			{
				if (this._minFloat != value)
				{
					this._minFloat = value;
				}
			}
		}

		// Token: 0x040002E9 RID: 745
		private float _floatText;

		// Token: 0x040002EA RID: 746
		private float _maxFloat = float.MaxValue;

		// Token: 0x040002EB RID: 747
		private float _minFloat = float.MinValue;
	}
}
