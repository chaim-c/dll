using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200000A RID: 10
	public class Brush
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002EED File Offset: 0x000010ED
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002EF5 File Offset: 0x000010F5
		public Brush ClonedFrom { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002EFE File Offset: 0x000010FE
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002F06 File Offset: 0x00001106
		[Editor(false)]
		public string Name { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002F0F File Offset: 0x0000110F
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002F17 File Offset: 0x00001117
		[Editor(false)]
		public float TransitionDuration { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002F20 File Offset: 0x00001120
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002F28 File Offset: 0x00001128
		public Style DefaultStyle { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002F31 File Offset: 0x00001131
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002F3E File Offset: 0x0000113E
		public Font Font
		{
			get
			{
				return this.DefaultStyle.Font;
			}
			set
			{
				this.DefaultStyle.Font = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002F4C File Offset: 0x0000114C
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002F59 File Offset: 0x00001159
		public FontStyle FontStyle
		{
			get
			{
				return this.DefaultStyle.FontStyle;
			}
			set
			{
				this.DefaultStyle.FontStyle = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002F67 File Offset: 0x00001167
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002F74 File Offset: 0x00001174
		public int FontSize
		{
			get
			{
				return this.DefaultStyle.FontSize;
			}
			set
			{
				this.DefaultStyle.FontSize = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002F82 File Offset: 0x00001182
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002F8A File Offset: 0x0000118A
		[Editor(false)]
		public TextHorizontalAlignment TextHorizontalAlignment { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002F93 File Offset: 0x00001193
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002F9B File Offset: 0x0000119B
		[Editor(false)]
		public TextVerticalAlignment TextVerticalAlignment { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002FA4 File Offset: 0x000011A4
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002FAC File Offset: 0x000011AC
		[Editor(false)]
		public float GlobalColorFactor { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002FB5 File Offset: 0x000011B5
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002FBD File Offset: 0x000011BD
		[Editor(false)]
		public float GlobalAlphaFactor { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002FC6 File Offset: 0x000011C6
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002FCE File Offset: 0x000011CE
		[Editor(false)]
		public Color GlobalColor { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002FD7 File Offset: 0x000011D7
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002FDF File Offset: 0x000011DF
		public SoundProperties SoundProperties { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002FF5 File Offset: 0x000011F5
		public Sprite Sprite
		{
			get
			{
				return this.DefaultStyleLayer.Sprite;
			}
			set
			{
				this.DefaultStyleLayer.Sprite = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003003 File Offset: 0x00001203
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003010 File Offset: 0x00001210
		[Editor(false)]
		public bool VerticalFlip
		{
			get
			{
				return this.DefaultStyleLayer.VerticalFlip;
			}
			set
			{
				this.DefaultStyleLayer.VerticalFlip = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000301E File Offset: 0x0000121E
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000302B File Offset: 0x0000122B
		[Editor(false)]
		public bool HorizontalFlip
		{
			get
			{
				return this.DefaultStyleLayer.HorizontalFlip;
			}
			set
			{
				this.DefaultStyleLayer.HorizontalFlip = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003039 File Offset: 0x00001239
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003046 File Offset: 0x00001246
		public Color Color
		{
			get
			{
				return this.DefaultStyleLayer.Color;
			}
			set
			{
				this.DefaultStyleLayer.Color = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003054 File Offset: 0x00001254
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003061 File Offset: 0x00001261
		public float ColorFactor
		{
			get
			{
				return this.DefaultStyleLayer.ColorFactor;
			}
			set
			{
				this.DefaultStyleLayer.ColorFactor = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000306F File Offset: 0x0000126F
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000307C File Offset: 0x0000127C
		public float AlphaFactor
		{
			get
			{
				return this.DefaultStyleLayer.AlphaFactor;
			}
			set
			{
				this.DefaultStyleLayer.AlphaFactor = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000308A File Offset: 0x0000128A
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003097 File Offset: 0x00001297
		public float HueFactor
		{
			get
			{
				return this.DefaultStyleLayer.HueFactor;
			}
			set
			{
				this.DefaultStyleLayer.HueFactor = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000030A5 File Offset: 0x000012A5
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000030B2 File Offset: 0x000012B2
		public float SaturationFactor
		{
			get
			{
				return this.DefaultStyleLayer.SaturationFactor;
			}
			set
			{
				this.DefaultStyleLayer.SaturationFactor = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000030C0 File Offset: 0x000012C0
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000030CD File Offset: 0x000012CD
		public float ValueFactor
		{
			get
			{
				return this.DefaultStyleLayer.ValueFactor;
			}
			set
			{
				this.DefaultStyleLayer.ValueFactor = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000030DB File Offset: 0x000012DB
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000030E8 File Offset: 0x000012E8
		public Color FontColor
		{
			get
			{
				return this.DefaultStyle.FontColor;
			}
			set
			{
				this.DefaultStyle.FontColor = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000030F6 File Offset: 0x000012F6
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003103 File Offset: 0x00001303
		public float TextColorFactor
		{
			get
			{
				return this.DefaultStyle.TextColorFactor;
			}
			set
			{
				this.DefaultStyle.TextColorFactor = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003111 File Offset: 0x00001311
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000311E File Offset: 0x0000131E
		public float TextAlphaFactor
		{
			get
			{
				return this.DefaultStyle.TextAlphaFactor;
			}
			set
			{
				this.DefaultStyle.TextAlphaFactor = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000312C File Offset: 0x0000132C
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003139 File Offset: 0x00001339
		public float TextHueFactor
		{
			get
			{
				return this.DefaultStyle.TextHueFactor;
			}
			set
			{
				this.DefaultStyle.TextHueFactor = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003147 File Offset: 0x00001347
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003154 File Offset: 0x00001354
		public float TextSaturationFactor
		{
			get
			{
				return this.DefaultStyle.TextSaturationFactor;
			}
			set
			{
				this.DefaultStyle.TextSaturationFactor = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003162 File Offset: 0x00001362
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000316F File Offset: 0x0000136F
		public float TextValueFactor
		{
			get
			{
				return this.DefaultStyle.TextValueFactor;
			}
			set
			{
				this.DefaultStyle.TextValueFactor = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000317D File Offset: 0x0000137D
		[Editor(false)]
		public Dictionary<string, BrushLayer>.ValueCollection Layers
		{
			get
			{
				return this._layers.Values;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000318A File Offset: 0x0000138A
		public StyleLayer DefaultStyleLayer
		{
			get
			{
				return this.DefaultStyle.DefaultLayer;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003197 File Offset: 0x00001397
		public BrushLayer DefaultLayer
		{
			get
			{
				return this._layers["Default"];
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000031AC File Offset: 0x000013AC
		public Brush()
		{
			this._styles = new Dictionary<string, Style>();
			this._layers = new Dictionary<string, BrushLayer>();
			this._brushAnimations = new Dictionary<string, BrushAnimation>();
			this.SoundProperties = new SoundProperties();
			this.TextHorizontalAlignment = TextHorizontalAlignment.Center;
			this.TextVerticalAlignment = TextVerticalAlignment.Center;
			BrushLayer brushLayer = new BrushLayer();
			brushLayer.Name = "Default";
			this._layers.Add(brushLayer.Name, brushLayer);
			this.DefaultStyle = new Style(new List<BrushLayer>
			{
				brushLayer
			});
			this.DefaultStyle.Name = "Default";
			this.DefaultStyle.SetAsDefaultStyle();
			this.AddStyle(this.DefaultStyle);
			this.ClonedFrom = null;
			this.TransitionDuration = 0.05f;
			this.GlobalColorFactor = 1f;
			this.GlobalAlphaFactor = 1f;
			this.GlobalColor = Color.White;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003290 File Offset: 0x00001490
		public Style GetStyle(string name)
		{
			Style result;
			this._styles.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000032AD File Offset: 0x000014AD
		[Editor(false)]
		public Dictionary<string, Style>.ValueCollection Styles
		{
			get
			{
				return this._styles.Values;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000032BC File Offset: 0x000014BC
		public Style GetStyleOrDefault(string name)
		{
			Style style;
			this._styles.TryGetValue(name, out style);
			return style ?? this.DefaultStyle;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000032E4 File Offset: 0x000014E4
		public void AddStyle(Style style)
		{
			string name = style.Name;
			this._styles.Add(name, style);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003305 File Offset: 0x00001505
		public void RemoveStyle(string styleName)
		{
			this._styles.Remove(styleName);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003314 File Offset: 0x00001514
		public void AddLayer(BrushLayer layer)
		{
			this._layers.Add(layer.Name, layer);
			foreach (Style style in this.Styles)
			{
				style.AddLayer(new StyleLayer(layer));
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000337C File Offset: 0x0000157C
		public void RemoveLayer(string layerName)
		{
			this._layers.Remove(layerName);
			foreach (Style style in this.Styles)
			{
				style.RemoveLayer(layerName);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000033DC File Offset: 0x000015DC
		public BrushLayer GetLayer(string name)
		{
			BrushLayer result;
			if (this._layers.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000033FC File Offset: 0x000015FC
		public void FillFrom(Brush brush)
		{
			this.Name = brush.Name;
			this.TransitionDuration = brush.TransitionDuration;
			this.TextVerticalAlignment = brush.TextVerticalAlignment;
			this.TextHorizontalAlignment = brush.TextHorizontalAlignment;
			this.GlobalColorFactor = brush.GlobalColorFactor;
			this.GlobalAlphaFactor = brush.GlobalAlphaFactor;
			this.GlobalColor = brush.GlobalColor;
			this._layers = new Dictionary<string, BrushLayer>();
			foreach (BrushLayer brushLayer in brush._layers.Values)
			{
				BrushLayer brushLayer2 = new BrushLayer();
				brushLayer2.FillFrom(brushLayer);
				this._layers.Add(brushLayer2.Name, brushLayer2);
			}
			this._styles = new Dictionary<string, Style>();
			Style style = brush._styles["Default"];
			Style style2 = new Style(this._layers.Values);
			style2.SetAsDefaultStyle();
			style2.FillFrom(style);
			this._styles.Add(style2.Name, style2);
			this.DefaultStyle = style2;
			foreach (Style style3 in brush._styles.Values)
			{
				if (style3.Name != "Default")
				{
					Style style4 = new Style(this._layers.Values);
					style4.DefaultStyle = this.DefaultStyle;
					style4.FillFrom(style3);
					this._styles.Add(style4.Name, style4);
				}
			}
			this._brushAnimations = new Dictionary<string, BrushAnimation>();
			foreach (BrushAnimation animation in brush._brushAnimations.Values)
			{
				BrushAnimation brushAnimation = new BrushAnimation();
				brushAnimation.FillFrom(animation);
				this._brushAnimations.Add(brushAnimation.Name, brushAnimation);
			}
			this.SoundProperties = new SoundProperties();
			this.SoundProperties.FillFrom(brush.SoundProperties);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003640 File Offset: 0x00001840
		public Brush Clone()
		{
			Brush brush = new Brush();
			brush.FillFrom(this);
			brush.Name = this.Name + "(Clone)";
			brush.ClonedFrom = this;
			return brush;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000366B File Offset: 0x0000186B
		public void AddAnimation(BrushAnimation animation)
		{
			this._brushAnimations.Add(animation.Name, animation);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003680 File Offset: 0x00001880
		public BrushAnimation GetAnimation(string name)
		{
			BrushAnimation result;
			if (name != null && this._brushAnimations.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000036A3 File Offset: 0x000018A3
		public IEnumerable<BrushAnimation> GetAnimations()
		{
			return this._brushAnimations.Values;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000036B0 File Offset: 0x000018B0
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return base.ToString();
			}
			return this.Name;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000036CC File Offset: 0x000018CC
		public bool IsCloneRelated(Brush brush)
		{
			return this.ClonedFrom == brush || brush.ClonedFrom == this || brush.ClonedFrom == this.ClonedFrom;
		}

		// Token: 0x04000023 RID: 35
		private const float DefaultTransitionDuration = 0.05f;

		// Token: 0x0400002B RID: 43
		private Dictionary<string, Style> _styles;

		// Token: 0x0400002C RID: 44
		private Dictionary<string, BrushLayer> _layers;

		// Token: 0x0400002D RID: 45
		private Dictionary<string, BrushAnimation> _brushAnimations;
	}
}
