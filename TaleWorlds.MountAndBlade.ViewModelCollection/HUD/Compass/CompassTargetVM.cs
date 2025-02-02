using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.Compass
{
	// Token: 0x02000058 RID: 88
	public class CompassTargetVM : ViewModel
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B2B8 File Offset: 0x000194B8
		public CompassTargetVM(TargetIconType iconType, uint color, uint color2, BannerCode bannercode, bool isAttacker, bool isAlly)
		{
			this.IconType = iconType.ToString();
			this.LetterCode = this.GetLetterCode(iconType);
			this.RefreshColor(color, color2);
			this.IsFlag = (iconType >= TargetIconType.Flag_A && iconType <= TargetIconType.Flag_I);
			this.IsAttacker = isAttacker;
			this.IsEnemy = !isAlly;
			if (bannercode == null)
			{
				this.Banner = new ImageIdentifierVM(ImageIdentifierType.Null);
				return;
			}
			this.Banner = new ImageIdentifierVM(bannercode, false);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001B344 File Offset: 0x00019544
		private string GetLetterCode(TargetIconType iconType)
		{
			switch (iconType)
			{
			case TargetIconType.Flag_A:
				return "A";
			case TargetIconType.Flag_B:
				return "B";
			case TargetIconType.Flag_C:
				return "C";
			case TargetIconType.Flag_D:
				return "D";
			case TargetIconType.Flag_E:
				return "E";
			case TargetIconType.Flag_F:
				return "F";
			case TargetIconType.Flag_G:
				return "G";
			case TargetIconType.Flag_H:
				return "H";
			case TargetIconType.Flag_I:
				return "I";
			default:
				return "";
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001B3BC File Offset: 0x000195BC
		public void RefreshColor(uint color, uint color2)
		{
			if (color != 0U)
			{
				string text = color.ToString("X");
				char c = text[0];
				char c2 = text[1];
				text = text.Remove(0, 2);
				text = text.Add(c.ToString() + c2.ToString(), false);
				this.Color = "#" + text;
			}
			else
			{
				this.Color = "#FFFFFFFF";
			}
			if (color2 != 0U)
			{
				string text2 = color2.ToString("X");
				char c3 = text2[0];
				char c4 = text2[1];
				text2 = text2.Remove(0, 2);
				text2 = text2.Add(c3.ToString() + c4.ToString(), false);
				this.Color2 = "#" + text2;
				return;
			}
			this.Color2 = "#FFFFFFFF";
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001B48E File Offset: 0x0001968E
		public virtual void Refresh(float circleX, float x, float distance)
		{
			this.FullPosition = circleX;
			this.Position = x;
			this.Distance = MathF.Round(distance);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001B4AA File Offset: 0x000196AA
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001B4B4 File Offset: 0x000196B4
		[DataSourceProperty]
		public ImageIdentifierVM Banner
		{
			get
			{
				return this._banner;
			}
			set
			{
				if (value != this._banner && (value == null || this._banner == null || this._banner.Id != value.Id))
				{
					this._banner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Banner");
				}
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001B500 File Offset: 0x00019700
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001B508 File Offset: 0x00019708
		[DataSourceProperty]
		public bool IsFlag
		{
			get
			{
				return this._isFlag;
			}
			set
			{
				if (value != this._isFlag)
				{
					this._isFlag = value;
					base.OnPropertyChangedWithValue(value, "IsFlag");
				}
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001B526 File Offset: 0x00019726
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0001B52E File Offset: 0x0001972E
		[DataSourceProperty]
		public int Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (value != this._distance)
				{
					this._distance = value;
					base.OnPropertyChangedWithValue(value, "Distance");
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001B54C File Offset: 0x0001974C
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0001B554 File Offset: 0x00019754
		[DataSourceProperty]
		public string Color2
		{
			get
			{
				return this._color2;
			}
			set
			{
				if (value != this._color2)
				{
					this._color2 = value;
					base.OnPropertyChangedWithValue<string>(value, "Color2");
				}
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001B577 File Offset: 0x00019777
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001B57F File Offset: 0x0001977F
		[DataSourceProperty]
		public string Color
		{
			get
			{
				return this._color;
			}
			set
			{
				if (value != this._color)
				{
					this._color = value;
					base.OnPropertyChangedWithValue<string>(value, "Color");
				}
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001B5A2 File Offset: 0x000197A2
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001B5AA File Offset: 0x000197AA
		[DataSourceProperty]
		public string IconType
		{
			get
			{
				return this._iconType;
			}
			set
			{
				if (value != this._iconType)
				{
					this._iconType = value;
					base.OnPropertyChangedWithValue<string>(value, "IconType");
					this.IconSpriteType = value;
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001B5D4 File Offset: 0x000197D4
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0001B5DC File Offset: 0x000197DC
		[DataSourceProperty]
		public string IconSpriteType
		{
			get
			{
				return this._iconSpriteType;
			}
			set
			{
				if (value != this._iconSpriteType)
				{
					this._iconSpriteType = value;
					base.OnPropertyChangedWithValue<string>(value, "IconSpriteType");
				}
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0001B5FF File Offset: 0x000197FF
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0001B607 File Offset: 0x00019807
		[DataSourceProperty]
		public string LetterCode
		{
			get
			{
				return this._letterCode;
			}
			set
			{
				if (value != this._letterCode)
				{
					this._letterCode = value;
					base.OnPropertyChangedWithValue<string>(value, "LetterCode");
				}
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001B62A File Offset: 0x0001982A
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0001B632 File Offset: 0x00019832
		[DataSourceProperty]
		public float FullPosition
		{
			get
			{
				return this._fullPosition;
			}
			set
			{
				if (MathF.Abs(value - this._fullPosition) > 1E-45f)
				{
					this._fullPosition = value;
					base.OnPropertyChangedWithValue(value, "FullPosition");
				}
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001B65B File Offset: 0x0001985B
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0001B663 File Offset: 0x00019863
		[DataSourceProperty]
		public float Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (MathF.Abs(value - this._position) > 1E-45f)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001B68C File Offset: 0x0001988C
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0001B694 File Offset: 0x00019894
		[DataSourceProperty]
		public bool IsAttacker
		{
			get
			{
				return this._isAttacker;
			}
			set
			{
				if (value != this._isAttacker)
				{
					this._isAttacker = value;
					base.OnPropertyChangedWithValue(value, "IsAttacker");
				}
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001B6B2 File Offset: 0x000198B2
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0001B6BA File Offset: 0x000198BA
		[DataSourceProperty]
		public bool IsEnemy
		{
			get
			{
				return this._isEnemy;
			}
			set
			{
				if (value != this._isEnemy)
				{
					this._isEnemy = value;
					base.OnPropertyChangedWithValue(value, "IsEnemy");
				}
			}
		}

		// Token: 0x04000341 RID: 833
		private int _distance;

		// Token: 0x04000342 RID: 834
		private string _color;

		// Token: 0x04000343 RID: 835
		private string _color2;

		// Token: 0x04000344 RID: 836
		private ImageIdentifierVM _banner;

		// Token: 0x04000345 RID: 837
		private string _iconType;

		// Token: 0x04000346 RID: 838
		private string _iconSpriteType;

		// Token: 0x04000347 RID: 839
		private string _letterCode;

		// Token: 0x04000348 RID: 840
		private float _position;

		// Token: 0x04000349 RID: 841
		private float _fullPosition;

		// Token: 0x0400034A RID: 842
		private bool _isAttacker;

		// Token: 0x0400034B RID: 843
		private bool _isEnemy;

		// Token: 0x0400034C RID: 844
		private bool _isFlag;
	}
}
