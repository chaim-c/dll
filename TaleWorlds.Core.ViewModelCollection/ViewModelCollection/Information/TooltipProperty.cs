using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x0200001B RID: 27
	public class TooltipProperty : ViewModel, ISerializableObject
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004CEE File Offset: 0x00002EEE
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00004CF6 File Offset: 0x00002EF6
		public bool OnlyShowWhenExtended { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004CFF File Offset: 0x00002EFF
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004D07 File Offset: 0x00002F07
		public bool OnlyShowWhenNotExtended { get; set; }

		// Token: 0x06000167 RID: 359 RVA: 0x00004D10 File Offset: 0x00002F10
		public TooltipProperty()
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004D38 File Offset: 0x00002F38
		public void RefreshValue()
		{
			if (this.valueFunc != null)
			{
				string text = this.valueFunc();
				if (text != null)
				{
					this.ValueLabel = text;
				}
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004D63 File Offset: 0x00002F63
		public void RefreshDefinition()
		{
			if (this.definitionFunc != null)
			{
				this.DefinitionLabel = this.definitionFunc();
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004D80 File Offset: 0x00002F80
		public TooltipProperty(string definition, string value, int textHeight, bool onlyShowWhenExtended = false, TooltipProperty.TooltipPropertyFlags modifier = TooltipProperty.TooltipPropertyFlags.None)
		{
			this.TextHeight = textHeight;
			this.DefinitionLabel = definition;
			this.ValueLabel = value;
			this.OnlyShowWhenExtended = onlyShowWhenExtended;
			this.PropertyModifier = (int)modifier;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004DD8 File Offset: 0x00002FD8
		public TooltipProperty(string definition, Func<string> _valueFunc, int textHeight, bool onlyShowWhenExtended = false, TooltipProperty.TooltipPropertyFlags modifier = TooltipProperty.TooltipPropertyFlags.None)
		{
			this.valueFunc = _valueFunc;
			this.TextHeight = textHeight;
			this.DefinitionLabel = definition;
			this.OnlyShowWhenExtended = onlyShowWhenExtended;
			this.PropertyModifier = (int)modifier;
			this.RefreshValue();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004E38 File Offset: 0x00003038
		public TooltipProperty(Func<string> _definitionFunc, Func<string> _valueFunc, int textHeight, bool onlyShowWhenExtended = false, TooltipProperty.TooltipPropertyFlags modifier = TooltipProperty.TooltipPropertyFlags.None)
		{
			this.valueFunc = _valueFunc;
			this.TextHeight = textHeight;
			this.definitionFunc = _definitionFunc;
			this.OnlyShowWhenExtended = onlyShowWhenExtended;
			this.PropertyModifier = (int)modifier;
			this.RefreshDefinition();
			this.RefreshValue();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004E9C File Offset: 0x0000309C
		public TooltipProperty(Func<string> _definitionFunc, Func<string> _valueFunc, object[] valueArgs, int textHeight, bool onlyShowWhenExtended = false, TooltipProperty.TooltipPropertyFlags modifier = TooltipProperty.TooltipPropertyFlags.None)
		{
			this.valueFunc = _valueFunc;
			this.TextHeight = textHeight;
			this.definitionFunc = _definitionFunc;
			this.OnlyShowWhenExtended = onlyShowWhenExtended;
			this.PropertyModifier = (int)modifier;
			this.RefreshDefinition();
			this.RefreshValue();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004F00 File Offset: 0x00003100
		public TooltipProperty(string definition, string value, int textHeight, Color color, bool onlyShowWhenExtended = false, TooltipProperty.TooltipPropertyFlags modifier = TooltipProperty.TooltipPropertyFlags.None)
		{
			this.TextHeight = textHeight;
			this.TextColor = color;
			this.DefinitionLabel = definition;
			this.ValueLabel = value;
			this.OnlyShowWhenExtended = onlyShowWhenExtended;
			this.PropertyModifier = (int)modifier;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004F60 File Offset: 0x00003160
		public TooltipProperty(string definition, Func<string> _valueFunc, int textHeight, Color color, bool onlyShowWhenExtended = false, TooltipProperty.TooltipPropertyFlags modifier = TooltipProperty.TooltipPropertyFlags.None)
		{
			this.valueFunc = _valueFunc;
			this.TextHeight = textHeight;
			this.TextColor = color;
			this.DefinitionLabel = definition;
			this.OnlyShowWhenExtended = onlyShowWhenExtended;
			this.PropertyModifier = (int)modifier;
			this.RefreshValue();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004FC8 File Offset: 0x000031C8
		public TooltipProperty(Func<string> _definitionFunc, Func<string> _valueFunc, int textHeight, Color color, bool onlyShowWhenExtended = false, TooltipProperty.TooltipPropertyFlags modifier = TooltipProperty.TooltipPropertyFlags.None)
		{
			this.valueFunc = _valueFunc;
			this.definitionFunc = _definitionFunc;
			this.TextHeight = textHeight;
			this.TextColor = color;
			this.OnlyShowWhenExtended = onlyShowWhenExtended;
			this.PropertyModifier = (int)modifier;
			this.RefreshDefinition();
			this.RefreshValue();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005034 File Offset: 0x00003234
		public TooltipProperty(TooltipProperty property)
		{
			this.TextHeight = property.TextHeight;
			this.TextColor = property.TextColor;
			this.DefinitionLabel = property.DefinitionLabel;
			this.ValueLabel = property.ValueLabel;
			this.OnlyShowWhenExtended = property.OnlyShowWhenExtended;
			this.PropertyModifier = property.PropertyModifier;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000050AE File Offset: 0x000032AE
		public void DeserializeFrom(IReader reader)
		{
			this.TextHeight = reader.ReadInt();
			this.TextColor = reader.ReadColor();
			this.DefinitionLabel = reader.ReadString();
			this.ValueLabel = reader.ReadString();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000050E0 File Offset: 0x000032E0
		public void SerializeTo(IWriter writer)
		{
			writer.WriteInt(this.TextHeight);
			writer.WriteColor(this.TextColor);
			writer.WriteString(this.DefinitionLabel);
			writer.WriteString(this.ValueLabel);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005112 File Offset: 0x00003312
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000511A File Offset: 0x0000331A
		public int TextHeight
		{
			get
			{
				return this._textHeight;
			}
			set
			{
				if (value != this._textHeight)
				{
					this._textHeight = value;
					base.OnPropertyChangedWithValue(value, "TextHeight");
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005138 File Offset: 0x00003338
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005140 File Offset: 0x00003340
		public Color TextColor
		{
			get
			{
				return this._textColor;
			}
			set
			{
				if (value != this._textColor)
				{
					this._textColor = value;
					base.OnPropertyChangedWithValue(value, "TextColor");
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005163 File Offset: 0x00003363
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000516B File Offset: 0x0000336B
		public string DefinitionLabel
		{
			get
			{
				return this._definitionLabel;
			}
			set
			{
				if (value != this._definitionLabel)
				{
					this._definitionLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "DefinitionLabel");
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000518E File Offset: 0x0000338E
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005196 File Offset: 0x00003396
		public string ValueLabel
		{
			get
			{
				return this._valueLabel;
			}
			set
			{
				if (value != this._valueLabel)
				{
					this._valueLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "ValueLabel");
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000051B9 File Offset: 0x000033B9
		// (set) Token: 0x0600017D RID: 381 RVA: 0x000051C1 File Offset: 0x000033C1
		public int PropertyModifier
		{
			get
			{
				return this._propertyModifier;
			}
			set
			{
				if (value != this._propertyModifier)
				{
					this._propertyModifier = value;
					base.OnPropertyChangedWithValue(value, "PropertyModifier");
				}
			}
		}

		// Token: 0x0400008A RID: 138
		private Func<string> valueFunc;

		// Token: 0x0400008B RID: 139
		private Func<string> definitionFunc;

		// Token: 0x0400008C RID: 140
		private string _definitionLabel;

		// Token: 0x0400008D RID: 141
		private string _valueLabel;

		// Token: 0x0400008E RID: 142
		private Color _textColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x0400008F RID: 143
		private int _textHeight;

		// Token: 0x04000090 RID: 144
		private int _propertyModifier;

		// Token: 0x0200002E RID: 46
		[Flags]
		public enum TooltipPropertyFlags
		{
			// Token: 0x040000D6 RID: 214
			None = 0,
			// Token: 0x040000D7 RID: 215
			MultiLine = 1,
			// Token: 0x040000D8 RID: 216
			BattleMode = 2,
			// Token: 0x040000D9 RID: 217
			BattleModeOver = 4,
			// Token: 0x040000DA RID: 218
			WarFirstEnemy = 8,
			// Token: 0x040000DB RID: 219
			WarFirstAlly = 16,
			// Token: 0x040000DC RID: 220
			WarFirstNeutral = 32,
			// Token: 0x040000DD RID: 221
			WarSecondEnemy = 64,
			// Token: 0x040000DE RID: 222
			WarSecondAlly = 128,
			// Token: 0x040000DF RID: 223
			WarSecondNeutral = 256,
			// Token: 0x040000E0 RID: 224
			RundownSeperator = 512,
			// Token: 0x040000E1 RID: 225
			DefaultSeperator = 1024,
			// Token: 0x040000E2 RID: 226
			Cost = 2048,
			// Token: 0x040000E3 RID: 227
			Title = 4096,
			// Token: 0x040000E4 RID: 228
			RundownResult = 8192
		}
	}
}
