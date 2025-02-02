using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000072 RID: 114
	public class Widget : PropertyOwnerObject
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00020192 File Offset: 0x0001E392
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0002019A File Offset: 0x0001E39A
		public float ColorFactor { get; set; } = 1f;

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000201A3 File Offset: 0x0001E3A3
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x000201AB File Offset: 0x0001E3AB
		public float AlphaFactor { get; set; } = 1f;

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000201B4 File Offset: 0x0001E3B4
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x000201BC File Offset: 0x0001E3BC
		public float ValueFactor { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x000201C5 File Offset: 0x0001E3C5
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x000201CD File Offset: 0x0001E3CD
		public float SaturationFactor { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x000201D6 File Offset: 0x0001E3D6
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x000201DE File Offset: 0x0001E3DE
		public float ExtendLeft { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x000201E7 File Offset: 0x0001E3E7
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x000201EF File Offset: 0x0001E3EF
		public float ExtendRight { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000201F8 File Offset: 0x0001E3F8
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x00020200 File Offset: 0x0001E400
		public float ExtendTop { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00020209 File Offset: 0x0001E409
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x00020211 File Offset: 0x0001E411
		public float ExtendBottom { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0002021A File Offset: 0x0001E41A
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00020222 File Offset: 0x0001E422
		public bool VerticalFlip { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0002022B File Offset: 0x0001E42B
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00020233 File Offset: 0x0001E433
		public bool HorizontalFlip { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0002023C File Offset: 0x0001E43C
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00020244 File Offset: 0x0001E444
		public bool FrictionEnabled { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0002024D File Offset: 0x0001E44D
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00020255 File Offset: 0x0001E455
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				if (this._color != value)
				{
					this._color = value;
				}
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0002026C File Offset: 0x0001E46C
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00020274 File Offset: 0x0001E474
		[Editor(false)]
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if (this._id != value)
				{
					this._id = value;
					base.OnPropertyChanged<string>(value, "Id");
				}
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00020297 File Offset: 0x0001E497
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x0002029F File Offset: 0x0001E49F
		public Vector2 LocalPosition { get; private set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000202A8 File Offset: 0x0001E4A8
		public Vector2 GlobalPosition
		{
			get
			{
				if (this.ParentWidget != null)
				{
					return this.LocalPosition + this.ParentWidget.GlobalPosition;
				}
				return this.LocalPosition;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x000202CF File Offset: 0x0001E4CF
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x000202D8 File Offset: 0x0001E4D8
		[Editor(false)]
		public bool DoNotUseCustomScaleAndChildren
		{
			get
			{
				return this._doNotUseCustomScaleAndChildren;
			}
			set
			{
				if (this._doNotUseCustomScaleAndChildren != value)
				{
					this._doNotUseCustomScaleAndChildren = value;
					base.OnPropertyChanged(value, "DoNotUseCustomScaleAndChildren");
					this.DoNotUseCustomScale = value;
					this._children.ForEach(delegate(Widget _)
					{
						_.DoNotUseCustomScaleAndChildren = value;
					});
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00020340 File Offset: 0x0001E540
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x00020348 File Offset: 0x0001E548
		public bool DoNotUseCustomScale { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00020351 File Offset: 0x0001E551
		protected float _scaleToUse
		{
			get
			{
				if (!this.DoNotUseCustomScale)
				{
					return this.Context.CustomScale;
				}
				return this.Context.Scale;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00020372 File Offset: 0x0001E572
		protected float _inverseScaleToUse
		{
			get
			{
				if (!this.DoNotUseCustomScale)
				{
					return this.Context.CustomInverseScale;
				}
				return this.Context.InverseScale;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00020393 File Offset: 0x0001E593
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x0002039B File Offset: 0x0001E59B
		public Vector2 Size { get; private set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000203A4 File Offset: 0x0001E5A4
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x000203AC File Offset: 0x0001E5AC
		[Editor(false)]
		public float SuggestedWidth
		{
			get
			{
				return this._suggestedWidth;
			}
			set
			{
				if (this._suggestedWidth != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._suggestedWidth = value;
					base.OnPropertyChanged(value, "SuggestedWidth");
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x000203D0 File Offset: 0x0001E5D0
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x000203D8 File Offset: 0x0001E5D8
		[Editor(false)]
		public float SuggestedHeight
		{
			get
			{
				return this._suggestedHeight;
			}
			set
			{
				if (this._suggestedHeight != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._suggestedHeight = value;
					base.OnPropertyChanged(value, "SuggestedHeight");
				}
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x000203FC File Offset: 0x0001E5FC
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0002040B File Offset: 0x0001E60B
		public float ScaledSuggestedWidth
		{
			get
			{
				return this._scaleToUse * this.SuggestedWidth;
			}
			set
			{
				this.SuggestedWidth = value * this._inverseScaleToUse;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0002041B File Offset: 0x0001E61B
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0002042A File Offset: 0x0001E62A
		public float ScaledSuggestedHeight
		{
			get
			{
				return this._scaleToUse * this.SuggestedHeight;
			}
			set
			{
				this.SuggestedHeight = value * this._inverseScaleToUse;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0002043A File Offset: 0x0001E63A
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x00020444 File Offset: 0x0001E644
		[Editor(false)]
		public bool TweenPosition
		{
			get
			{
				return this._tweenPosition;
			}
			set
			{
				if (this._tweenPosition != value)
				{
					bool tweenPosition = this._tweenPosition;
					this._tweenPosition = value;
					if (this.ConnectedToRoot && (!tweenPosition || !this._tweenPosition))
					{
						this.EventManager.OnWidgetTweenPositionChanged(this);
					}
				}
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00020487 File Offset: 0x0001E687
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0002048F File Offset: 0x0001E68F
		[Editor(false)]
		public string HoveredCursorState
		{
			get
			{
				return this._hoveredCursorState;
			}
			set
			{
				if (this._hoveredCursorState != value)
				{
					string hoveredCursorState = this._hoveredCursorState;
					this._hoveredCursorState = value;
				}
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x000204AD File Offset: 0x0001E6AD
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x000204B5 File Offset: 0x0001E6B5
		[Editor(false)]
		public bool AlternateClickEventHasSpecialEvent
		{
			get
			{
				return this._alternateClickEventHasSpecialEvent;
			}
			set
			{
				if (this._alternateClickEventHasSpecialEvent != value)
				{
					bool alternateClickEventHasSpecialEvent = this._alternateClickEventHasSpecialEvent;
					this._alternateClickEventHasSpecialEvent = value;
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x000204CE File Offset: 0x0001E6CE
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x000204D6 File Offset: 0x0001E6D6
		public Vector2 PosOffset
		{
			get
			{
				return this._positionOffset;
			}
			set
			{
				if (this._positionOffset != value)
				{
					this.SetLayoutDirty();
					this._positionOffset = value;
					base.OnPropertyChanged(value, "PosOffset");
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000204FF File Offset: 0x0001E6FF
		public Vector2 ScaledPositionOffset
		{
			get
			{
				return this._positionOffset * this._scaleToUse;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00020512 File Offset: 0x0001E712
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0002051F File Offset: 0x0001E71F
		[Editor(false)]
		public float PositionXOffset
		{
			get
			{
				return this._positionOffset.X;
			}
			set
			{
				if (this._positionOffset.X != value)
				{
					this.SetLayoutDirty();
					this._positionOffset.X = value;
					base.OnPropertyChanged(value, "PositionXOffset");
				}
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0002054D File Offset: 0x0001E74D
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0002055A File Offset: 0x0001E75A
		[Editor(false)]
		public float PositionYOffset
		{
			get
			{
				return this._positionOffset.Y;
			}
			set
			{
				if (this._positionOffset.Y != value)
				{
					this.SetLayoutDirty();
					this._positionOffset.Y = value;
					base.OnPropertyChanged(value, "PositionYOffset");
				}
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00020588 File Offset: 0x0001E788
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0002059C File Offset: 0x0001E79C
		public float ScaledPositionXOffset
		{
			get
			{
				return this._positionOffset.X * this._scaleToUse;
			}
			set
			{
				float num = value * this._inverseScaleToUse;
				if (num != this._positionOffset.X)
				{
					this.SetLayoutDirty();
					this._positionOffset.X = num;
				}
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x000205D2 File Offset: 0x0001E7D2
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x000205E8 File Offset: 0x0001E7E8
		public float ScaledPositionYOffset
		{
			get
			{
				return this._positionOffset.Y * this._scaleToUse;
			}
			set
			{
				float num = value * this._inverseScaleToUse;
				if (num != this._positionOffset.Y)
				{
					this.SetLayoutDirty();
					this._positionOffset.Y = num;
				}
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002061E File Offset: 0x0001E81E
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00020628 File Offset: 0x0001E828
		public Widget ParentWidget
		{
			get
			{
				return this._parent;
			}
			set
			{
				if (this.ParentWidget != value)
				{
					if (this._parent != null)
					{
						this._parent.OnChildRemoved(this);
						if (this.ConnectedToRoot)
						{
							this.EventManager.OnWidgetDisconnectedFromRoot(this);
						}
						this._parent._children.Remove(this);
						this._parent.OnAfterChildRemoved(this);
					}
					this._parent = value;
					if (this._parent != null)
					{
						this._parent._children.Add(this);
						if (this.ConnectedToRoot)
						{
							this.EventManager.OnWidgetConnectedToRoot(this);
						}
						this._parent.OnChildAdded(this);
					}
					this.SetMeasureAndLayoutDirty();
				}
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x000206CD File Offset: 0x0001E8CD
		public EventManager EventManager
		{
			get
			{
				return this.Context.EventManager;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000206DA File Offset: 0x0001E8DA
		public IGamepadNavigationContext GamepadNavigationContext
		{
			get
			{
				return this.Context.GamepadNavigation;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000206E7 File Offset: 0x0001E8E7
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x000206EF File Offset: 0x0001E8EF
		public UIContext Context { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000206F8 File Offset: 0x0001E8F8
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00020700 File Offset: 0x0001E900
		public Vector2 MeasuredSize { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00020709 File Offset: 0x0001E909
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00020711 File Offset: 0x0001E911
		[Editor(false)]
		public float MarginTop
		{
			get
			{
				return this._marginTop;
			}
			set
			{
				if (this._marginTop != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._marginTop = value;
					base.OnPropertyChanged(value, "MarginTop");
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00020735 File Offset: 0x0001E935
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0002073D File Offset: 0x0001E93D
		[Editor(false)]
		public float MarginLeft
		{
			get
			{
				return this._marginLeft;
			}
			set
			{
				if (this._marginLeft != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._marginLeft = value;
					base.OnPropertyChanged(value, "MarginLeft");
				}
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00020761 File Offset: 0x0001E961
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00020769 File Offset: 0x0001E969
		[Editor(false)]
		public float MarginBottom
		{
			get
			{
				return this._marginBottom;
			}
			set
			{
				if (this._marginBottom != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._marginBottom = value;
					base.OnPropertyChanged(value, "MarginBottom");
				}
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0002078D File Offset: 0x0001E98D
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00020795 File Offset: 0x0001E995
		[Editor(false)]
		public float MarginRight
		{
			get
			{
				return this._marginRight;
			}
			set
			{
				if (this._marginRight != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._marginRight = value;
					base.OnPropertyChanged(value, "MarginRight");
				}
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x000207B9 File Offset: 0x0001E9B9
		public float ScaledMarginTop
		{
			get
			{
				return this._scaleToUse * this.MarginTop;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x000207C8 File Offset: 0x0001E9C8
		public float ScaledMarginLeft
		{
			get
			{
				return this._scaleToUse * this.MarginLeft;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x000207D7 File Offset: 0x0001E9D7
		public float ScaledMarginBottom
		{
			get
			{
				return this._scaleToUse * this.MarginBottom;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x000207E6 File Offset: 0x0001E9E6
		public float ScaledMarginRight
		{
			get
			{
				return this._scaleToUse * this.MarginRight;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x000207F5 File Offset: 0x0001E9F5
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x000207FD File Offset: 0x0001E9FD
		[Editor(false)]
		public VerticalAlignment VerticalAlignment
		{
			get
			{
				return this._verticalAlignment;
			}
			set
			{
				if (this._verticalAlignment != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._verticalAlignment = value;
					base.OnPropertyChanged<string>(Enum.GetName(typeof(VerticalAlignment), value), "VerticalAlignment");
				}
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00020835 File Offset: 0x0001EA35
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0002083D File Offset: 0x0001EA3D
		[Editor(false)]
		public HorizontalAlignment HorizontalAlignment
		{
			get
			{
				return this._horizontalAlignment;
			}
			set
			{
				if (this._horizontalAlignment != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._horizontalAlignment = value;
					base.OnPropertyChanged<string>(Enum.GetName(typeof(HorizontalAlignment), value), "HorizontalAlignment");
				}
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00020875 File Offset: 0x0001EA75
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00020882 File Offset: 0x0001EA82
		public float Left
		{
			get
			{
				return this._topLeft.X;
			}
			private set
			{
				if (value != this._topLeft.X)
				{
					this.EventManager.SetPositionsDirty();
					this._topLeft.X = value;
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000208A9 File Offset: 0x0001EAA9
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x000208B6 File Offset: 0x0001EAB6
		public float Top
		{
			get
			{
				return this._topLeft.Y;
			}
			private set
			{
				if (value != this._topLeft.Y)
				{
					this.EventManager.SetPositionsDirty();
					this._topLeft.Y = value;
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000208DD File Offset: 0x0001EADD
		public float Right
		{
			get
			{
				return this._topLeft.X + this.Size.X;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000208F6 File Offset: 0x0001EAF6
		public float Bottom
		{
			get
			{
				return this._topLeft.Y + this.Size.Y;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0002090F File Offset: 0x0001EB0F
		public int ChildCount
		{
			get
			{
				return this._children.Count;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0002091C File Offset: 0x0001EB1C
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00020924 File Offset: 0x0001EB24
		[Editor(false)]
		public bool ForcePixelPerfectRenderPlacement
		{
			get
			{
				return this._forcePixelPerfectRenderPlacement;
			}
			set
			{
				if (this._forcePixelPerfectRenderPlacement != value)
				{
					this._forcePixelPerfectRenderPlacement = value;
					base.OnPropertyChanged(value, "ForcePixelPerfectRenderPlacement");
				}
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00020942 File Offset: 0x0001EB42
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0002094A File Offset: 0x0001EB4A
		public bool UseGlobalTimeForAnimation { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00020953 File Offset: 0x0001EB53
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0002095B File Offset: 0x0001EB5B
		[Editor(false)]
		public SizePolicy WidthSizePolicy
		{
			get
			{
				return this._widthSizePolicy;
			}
			set
			{
				if (value != this._widthSizePolicy)
				{
					this.SetMeasureAndLayoutDirty();
					this._widthSizePolicy = value;
				}
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00020973 File Offset: 0x0001EB73
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0002097B File Offset: 0x0001EB7B
		[Editor(false)]
		public SizePolicy HeightSizePolicy
		{
			get
			{
				return this._heightSizePolicy;
			}
			set
			{
				if (value != this._heightSizePolicy)
				{
					this.SetMeasureAndLayoutDirty();
					this._heightSizePolicy = value;
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00020993 File Offset: 0x0001EB93
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0002099B File Offset: 0x0001EB9B
		[Editor(false)]
		public bool AcceptDrag { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000209A4 File Offset: 0x0001EBA4
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x000209AC File Offset: 0x0001EBAC
		[Editor(false)]
		public bool AcceptDrop { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x000209B5 File Offset: 0x0001EBB5
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x000209BD File Offset: 0x0001EBBD
		[Editor(false)]
		public bool HideOnDrag { get; set; } = true;

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000209C6 File Offset: 0x0001EBC6
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x000209CE File Offset: 0x0001EBCE
		[Editor(false)]
		public Widget DragWidget
		{
			get
			{
				return this._dragWidget;
			}
			set
			{
				if (this._dragWidget != value)
				{
					if (value != null)
					{
						this._dragWidget = value;
						this._dragWidget.IsVisible = false;
						return;
					}
					this._dragWidget = null;
				}
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000209F7 File Offset: 0x0001EBF7
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00020A09 File Offset: 0x0001EC09
		[Editor(false)]
		public bool ClipContents
		{
			get
			{
				return this.ClipVerticalContent && this.ClipHorizontalContent;
			}
			set
			{
				this.ClipHorizontalContent = value;
				this.ClipVerticalContent = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00020A19 File Offset: 0x0001EC19
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00020A21 File Offset: 0x0001EC21
		[Editor(false)]
		public bool ClipHorizontalContent { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00020A2A File Offset: 0x0001EC2A
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00020A32 File Offset: 0x0001EC32
		[Editor(false)]
		public bool ClipVerticalContent { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00020A3B File Offset: 0x0001EC3B
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00020A43 File Offset: 0x0001EC43
		[Editor(false)]
		public bool CircularClipEnabled { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00020A4C File Offset: 0x0001EC4C
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00020A54 File Offset: 0x0001EC54
		[Editor(false)]
		public float CircularClipRadius { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00020A5D File Offset: 0x0001EC5D
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x00020A65 File Offset: 0x0001EC65
		[Editor(false)]
		public bool IsCircularClipRadiusHalfOfWidth { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00020A6E File Offset: 0x0001EC6E
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x00020A76 File Offset: 0x0001EC76
		[Editor(false)]
		public bool IsCircularClipRadiusHalfOfHeight { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00020A7F File Offset: 0x0001EC7F
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00020A87 File Offset: 0x0001EC87
		[Editor(false)]
		public float CircularClipSmoothingRadius { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00020A90 File Offset: 0x0001EC90
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00020A98 File Offset: 0x0001EC98
		[Editor(false)]
		public float CircularClipXOffset { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00020AA1 File Offset: 0x0001ECA1
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00020AA9 File Offset: 0x0001ECA9
		[Editor(false)]
		public float CircularClipYOffset { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00020AB2 File Offset: 0x0001ECB2
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x00020ABA File Offset: 0x0001ECBA
		[Editor(false)]
		public bool RenderLate { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00020AC3 File Offset: 0x0001ECC3
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x00020ACB File Offset: 0x0001ECCB
		[Editor(false)]
		public bool DoNotRenderIfNotFullyInsideScissor { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00020AD4 File Offset: 0x0001ECD4
		public bool FixedWidth
		{
			get
			{
				return this.WidthSizePolicy == SizePolicy.Fixed;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00020ADF File Offset: 0x0001ECDF
		public bool FixedHeight
		{
			get
			{
				return this.HeightSizePolicy == SizePolicy.Fixed;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00020AEA File Offset: 0x0001ECEA
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00020AF2 File Offset: 0x0001ECF2
		public bool IsHovered
		{
			get
			{
				return this._isHovered;
			}
			private set
			{
				if (this._isHovered != value)
				{
					this._isHovered = value;
					this.RefreshState();
					base.OnPropertyChanged(value, "IsHovered");
				}
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00020B16 File Offset: 0x0001ED16
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00020B1E File Offset: 0x0001ED1E
		[Editor(false)]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (this._isDisabled != value)
				{
					this._isDisabled = value;
					base.OnPropertyChanged(value, "IsDisabled");
					this.RefreshState();
				}
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00020B42 File Offset: 0x0001ED42
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x00020B4A File Offset: 0x0001ED4A
		[Editor(false)]
		public bool IsFocusable
		{
			get
			{
				return this._isFocusable;
			}
			set
			{
				if (this._isFocusable != value)
				{
					this._isFocusable = value;
					if (this.ConnectedToRoot)
					{
						base.OnPropertyChanged(value, "IsFocusable");
						this.RefreshState();
					}
				}
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00020B76 File Offset: 0x0001ED76
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x00020B7E File Offset: 0x0001ED7E
		public bool IsFocused
		{
			get
			{
				return this._isFocused;
			}
			private set
			{
				if (this._isFocused != value)
				{
					this._isFocused = value;
					this.RefreshState();
				}
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x00020B96 File Offset: 0x0001ED96
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x00020BA1 File Offset: 0x0001EDA1
		[Editor(false)]
		public bool IsEnabled
		{
			get
			{
				return !this.IsDisabled;
			}
			set
			{
				if (value == this.IsDisabled)
				{
					this.IsDisabled = !value;
					base.OnPropertyChanged(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00020BC2 File Offset: 0x0001EDC2
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x00020BCA File Offset: 0x0001EDCA
		[Editor(false)]
		public bool RestartAnimationFirstFrame
		{
			get
			{
				return this._restartAnimationFirstFrame;
			}
			set
			{
				if (this._restartAnimationFirstFrame != value)
				{
					this._restartAnimationFirstFrame = value;
				}
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00020BDC File Offset: 0x0001EDDC
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x00020BE4 File Offset: 0x0001EDE4
		[Editor(false)]
		public bool DoNotPassEventsToChildren
		{
			get
			{
				return this._doNotPassEventsToChildren;
			}
			set
			{
				if (this._doNotPassEventsToChildren != value)
				{
					this._doNotPassEventsToChildren = value;
					base.OnPropertyChanged(value, "DoNotPassEventsToChildren");
				}
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00020C02 File Offset: 0x0001EE02
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x00020C0A File Offset: 0x0001EE0A
		[Editor(false)]
		public bool DoNotAcceptEvents
		{
			get
			{
				return this._doNotAcceptEvents;
			}
			set
			{
				if (this._doNotAcceptEvents != value)
				{
					this._doNotAcceptEvents = value;
					base.OnPropertyChanged(value, "DoNotAcceptEvents");
				}
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00020C28 File Offset: 0x0001EE28
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00020C33 File Offset: 0x0001EE33
		[Editor(false)]
		public bool CanAcceptEvents
		{
			get
			{
				return !this.DoNotAcceptEvents;
			}
			set
			{
				this.DoNotAcceptEvents = !value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00020C3F File Offset: 0x0001EE3F
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00020C47 File Offset: 0x0001EE47
		public bool IsPressed
		{
			get
			{
				return this._isPressed;
			}
			internal set
			{
				if (this._isPressed != value)
				{
					this._isPressed = value;
					this.RefreshState();
				}
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00020C5F File Offset: 0x0001EE5F
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00020C68 File Offset: 0x0001EE68
		[Editor(false)]
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
			set
			{
				if (this._isHidden != value)
				{
					this.SetMeasureAndLayoutDirty();
					this._isHidden = value;
					this.RefreshState();
					base.OnPropertyChanged(value, "IsHidden");
					base.OnPropertyChanged(!value, "IsVisible");
					if (this.OnVisibilityChanged != null)
					{
						this.OnVisibilityChanged(this);
					}
				}
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00020CC0 File Offset: 0x0001EEC0
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00020CCB File Offset: 0x0001EECB
		[Editor(false)]
		public bool IsVisible
		{
			get
			{
				return !this._isHidden;
			}
			set
			{
				if (value == this._isHidden)
				{
					this.IsHidden = !value;
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		[Editor(false)]
		public Sprite Sprite
		{
			get
			{
				return this._sprite;
			}
			set
			{
				if (value != this._sprite)
				{
					this._sprite = value;
				}
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00020CFA File Offset: 0x0001EEFA
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00020D04 File Offset: 0x0001EF04
		[Editor(false)]
		public VisualDefinition VisualDefinition
		{
			get
			{
				return this._visualDefinition;
			}
			set
			{
				if (this._visualDefinition != value)
				{
					VisualDefinition visualDefinition = this._visualDefinition;
					this._visualDefinition = value;
					this._stateTimer = 0f;
					if (this.ConnectedToRoot && (visualDefinition == null || this._visualDefinition == null))
					{
						this.EventManager.OnWidgetVisualDefinitionChanged(this);
					}
				}
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00020D52 File Offset: 0x0001EF52
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x00020D5A File Offset: 0x0001EF5A
		public string CurrentState { get; protected set; } = "";

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00020D63 File Offset: 0x0001EF63
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x00020D6B File Offset: 0x0001EF6B
		[Editor(false)]
		public bool UpdateChildrenStates
		{
			get
			{
				return this._updateChildrenStates;
			}
			set
			{
				if (this._updateChildrenStates != value)
				{
					this._updateChildrenStates = value;
					base.OnPropertyChanged(value, "UpdateChildrenStates");
					if (value && this.ChildCount > 0)
					{
						this.SetState(this.CurrentState);
					}
				}
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x00020DA1 File Offset: 0x0001EFA1
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x00020DA9 File Offset: 0x0001EFA9
		public object Tag { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00020DB2 File Offset: 0x0001EFB2
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00020DBA File Offset: 0x0001EFBA
		public ILayout LayoutImp { get; protected set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00020DC3 File Offset: 0x0001EFC3
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00020DCB File Offset: 0x0001EFCB
		[Editor(false)]
		public bool DropEventHandledManually { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00020DDC File Offset: 0x0001EFDC
		internal WidgetInfo WidgetInfo { get; private set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00020DE5 File Offset: 0x0001EFE5
		public IEnumerable<Widget> AllChildrenAndThis
		{
			get
			{
				yield return this;
				foreach (Widget widget in this._children)
				{
					foreach (Widget widget2 in widget.AllChildrenAndThis)
					{
						yield return widget2;
					}
					IEnumerator<Widget> enumerator2 = null;
				}
				List<Widget>.Enumerator enumerator = default(List<Widget>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00020DF8 File Offset: 0x0001EFF8
		public void ApplyActionOnAllChildren(Action<Widget> action)
		{
			foreach (Widget widget in this._children)
			{
				action(widget);
				widget.ApplyActionOnAllChildren(action);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00020E54 File Offset: 0x0001F054
		public IEnumerable<Widget> AllChildren
		{
			get
			{
				foreach (Widget widget in this._children)
				{
					yield return widget;
					foreach (Widget widget2 in widget.AllChildren)
					{
						yield return widget2;
					}
					IEnumerator<Widget> enumerator2 = null;
					widget = null;
				}
				List<Widget>.Enumerator enumerator = default(List<Widget>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00020E64 File Offset: 0x0001F064
		public List<Widget> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00020E6C File Offset: 0x0001F06C
		public IEnumerable<Widget> Parents
		{
			get
			{
				for (Widget parent = this.ParentWidget; parent != null; parent = parent.ParentWidget)
				{
					yield return parent;
				}
				yield break;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00020E7C File Offset: 0x0001F07C
		internal bool ConnectedToRoot
		{
			get
			{
				return this.Id == "Root" || (this.ParentWidget != null && this.ParentWidget.ConnectedToRoot);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00020EA7 File Offset: 0x0001F0A7
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00020EAF File Offset: 0x0001F0AF
		internal int OnUpdateListIndex { get; set; } = -1;

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		internal int OnLateUpdateListIndex { get; set; } = -1;

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00020EC9 File Offset: 0x0001F0C9
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00020ED1 File Offset: 0x0001F0D1
		internal int OnUpdateBrushesIndex { get; set; } = -1;

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00020EDA File Offset: 0x0001F0DA
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00020EE2 File Offset: 0x0001F0E2
		internal int OnParallelUpdateListIndex { get; set; } = -1;

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00020EEB File Offset: 0x0001F0EB
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00020EF3 File Offset: 0x0001F0F3
		internal int OnVisualDefinitionListIndex { get; set; } = -1;

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00020EFC File Offset: 0x0001F0FC
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00020F04 File Offset: 0x0001F104
		internal int OnTweenPositionListIndex { get; set; } = -1;

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00020F0D File Offset: 0x0001F10D
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00020F15 File Offset: 0x0001F115
		[Editor(false)]
		public float MaxWidth
		{
			get
			{
				return this._maxWidth;
			}
			set
			{
				if (this._maxWidth != value)
				{
					this._maxWidth = value;
					this._gotMaxWidth = true;
					base.OnPropertyChanged(value, "MaxWidth");
				}
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00020F3A File Offset: 0x0001F13A
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00020F42 File Offset: 0x0001F142
		[Editor(false)]
		public float MaxHeight
		{
			get
			{
				return this._maxHeight;
			}
			set
			{
				if (this._maxHeight != value)
				{
					this._maxHeight = value;
					this._gotMaxHeight = true;
					base.OnPropertyChanged(value, "MaxHeight");
				}
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00020F67 File Offset: 0x0001F167
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00020F6F File Offset: 0x0001F16F
		[Editor(false)]
		public float MinWidth
		{
			get
			{
				return this._minWidth;
			}
			set
			{
				if (this._minWidth != value)
				{
					this._minWidth = value;
					this._gotMinWidth = true;
					base.OnPropertyChanged(value, "MinWidth");
				}
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00020F94 File Offset: 0x0001F194
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00020F9C File Offset: 0x0001F19C
		[Editor(false)]
		public float MinHeight
		{
			get
			{
				return this._minHeight;
			}
			set
			{
				if (this._minHeight != value)
				{
					this._minHeight = value;
					this._gotMinHeight = true;
					base.OnPropertyChanged(value, "MinHeight");
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00020FC1 File Offset: 0x0001F1C1
		public float ScaledMaxWidth
		{
			get
			{
				return this._scaleToUse * this._maxWidth;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00020FD0 File Offset: 0x0001F1D0
		public float ScaledMaxHeight
		{
			get
			{
				return this._scaleToUse * this._maxHeight;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00020FDF File Offset: 0x0001F1DF
		public float ScaledMinWidth
		{
			get
			{
				return this._scaleToUse * this._minWidth;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00020FEE File Offset: 0x0001F1EE
		public float ScaledMinHeight
		{
			get
			{
				return this._scaleToUse * this._minHeight;
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00021000 File Offset: 0x0001F200
		public Widget(UIContext context)
		{
			this.DropEventHandledManually = true;
			this.LayoutImp = new DefaultLayout();
			this._children = new List<Widget>();
			this.Context = context;
			this._states = new List<string>();
			this.WidgetInfo = WidgetInfo.GetWidgetInfo(base.GetType());
			this.Sprite = null;
			this._stateTimer = 0f;
			this._currentVisualStateAnimationState = VisualStateAnimationState.None;
			this._isFocusable = false;
			this._seed = 0;
			this._components = new List<WidgetComponent>();
			this.AddState("Default");
			this.SetState("Default");
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00021108 File Offset: 0x0001F308
		public T GetComponent<T>() where T : WidgetComponent
		{
			for (int i = 0; i < this._components.Count; i++)
			{
				WidgetComponent widgetComponent = this._components[i];
				if (widgetComponent is T)
				{
					return (T)((object)widgetComponent);
				}
			}
			return default(T);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00021150 File Offset: 0x0001F350
		public void AddComponent(WidgetComponent component)
		{
			this._components.Add(component);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002115E File Offset: 0x0001F35E
		protected void SetMeasureAndLayoutDirty()
		{
			this.SetMeasureDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002116C File Offset: 0x0001F36C
		protected void SetMeasureDirty()
		{
			this.EventManager.SetMeasureDirty();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00021179 File Offset: 0x0001F379
		protected void SetLayoutDirty()
		{
			this.EventManager.SetLayoutDirty();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00021186 File Offset: 0x0001F386
		public void AddState(string stateName)
		{
			if (!this._states.Contains(stateName))
			{
				this._states.Add(stateName);
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000211A2 File Offset: 0x0001F3A2
		public bool ContainsState(string stateName)
		{
			return this._states.Contains(stateName);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000211B0 File Offset: 0x0001F3B0
		public virtual void SetState(string stateName)
		{
			if (this.CurrentState != stateName)
			{
				this.CurrentState = stateName;
				this._stateTimer = 0f;
				if (this._currentVisualStateAnimationState != VisualStateAnimationState.None)
				{
					this._startVisualState = new VisualState("@StartState");
					this._startVisualState.FillFromWidget(this);
				}
				this._currentVisualStateAnimationState = VisualStateAnimationState.PlayingBasicTranisition;
			}
			if (this.UpdateChildrenStates)
			{
				for (int i = 0; i < this.ChildCount; i++)
				{
					Widget child = this.GetChild(i);
					if (!(child is ImageWidget) || !((ImageWidget)child).OverrideDefaultStateSwitchingEnabled)
					{
						child.SetState(this.CurrentState);
					}
				}
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002124C File Offset: 0x0001F44C
		public Widget FindChild(BindingPath path)
		{
			string firstNode = path.FirstNode;
			BindingPath subPath = path.SubPath;
			if (firstNode == "..")
			{
				return this.ParentWidget.FindChild(subPath);
			}
			if (firstNode == ".")
			{
				return this;
			}
			foreach (Widget widget in this._children)
			{
				if (!string.IsNullOrEmpty(widget.Id) && widget.Id == firstNode)
				{
					if (subPath == null)
					{
						return widget;
					}
					return widget.FindChild(subPath);
				}
			}
			return null;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00021308 File Offset: 0x0001F508
		public Widget FindChild(string singlePathNode)
		{
			if (singlePathNode == "..")
			{
				return this.ParentWidget;
			}
			if (singlePathNode == ".")
			{
				return this;
			}
			foreach (Widget widget in this._children)
			{
				if (!string.IsNullOrEmpty(widget.Id) && widget.Id == singlePathNode)
				{
					return widget;
				}
			}
			return null;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002139C File Offset: 0x0001F59C
		public Widget FindChild(WidgetSearchDelegate widgetSearchDelegate)
		{
			for (int i = 0; i < this._children.Count; i++)
			{
				Widget widget = this._children[i];
				if (widgetSearchDelegate(widget))
				{
					return widget;
				}
			}
			return null;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000213D8 File Offset: 0x0001F5D8
		public Widget FindChild(string id, bool includeAllChildren = false)
		{
			IEnumerable<Widget> enumerable;
			if (!includeAllChildren)
			{
				IEnumerable<Widget> children = this._children;
				enumerable = children;
			}
			else
			{
				enumerable = this.AllChildren;
			}
			foreach (Widget widget in enumerable)
			{
				if (!string.IsNullOrEmpty(widget.Id) && widget.Id == id)
				{
					return widget;
				}
			}
			return null;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00021450 File Offset: 0x0001F650
		public void RemoveAllChildren()
		{
			while (this._children.Count > 0)
			{
				this._children[0].ParentWidget = null;
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00021474 File Offset: 0x0001F674
		private static float GetEaseOutBack(float t)
		{
			float num = 0.5f;
			float num2 = num + 1f;
			return 1f + num2 * MathF.Pow(t - 1f, 3f) + num * MathF.Pow(t - 1f, 2f);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000214BC File Offset: 0x0001F6BC
		internal void UpdateVisualDefinitions(float dt)
		{
			if (this.VisualDefinition != null && this._currentVisualStateAnimationState == VisualStateAnimationState.PlayingBasicTranisition)
			{
				if (this._startVisualState == null)
				{
					this._startVisualState = new VisualState("@StartState");
					this._startVisualState.FillFromWidget(this);
				}
				VisualState visualState = this.VisualDefinition.GetVisualState(this.CurrentState);
				if (visualState != null)
				{
					float num = visualState.GotTransitionDuration ? visualState.TransitionDuration : this.VisualDefinition.TransitionDuration;
					float delayOnBegin = this.VisualDefinition.DelayOnBegin;
					if (this._stateTimer < num)
					{
						if (this._stateTimer >= delayOnBegin)
						{
							float num2 = (this._stateTimer - delayOnBegin) / (num - delayOnBegin);
							if (this.VisualDefinition.EaseIn)
							{
								num2 = Widget.GetEaseOutBack(num2);
							}
							this.PositionXOffset = (visualState.GotPositionXOffset ? Mathf.Lerp(this._startVisualState.PositionXOffset, visualState.PositionXOffset, num2) : this.PositionXOffset);
							this.PositionYOffset = (visualState.GotPositionYOffset ? Mathf.Lerp(this._startVisualState.PositionYOffset, visualState.PositionYOffset, num2) : this.PositionYOffset);
							this.SuggestedWidth = (visualState.GotSuggestedWidth ? Mathf.Lerp(this._startVisualState.SuggestedWidth, visualState.SuggestedWidth, num2) : this.SuggestedWidth);
							this.SuggestedHeight = (visualState.GotSuggestedHeight ? Mathf.Lerp(this._startVisualState.SuggestedHeight, visualState.SuggestedHeight, num2) : this.SuggestedHeight);
							this.MarginTop = (visualState.GotMarginTop ? Mathf.Lerp(this._startVisualState.MarginTop, visualState.MarginTop, num2) : this.MarginTop);
							this.MarginBottom = (visualState.GotMarginBottom ? Mathf.Lerp(this._startVisualState.MarginBottom, visualState.MarginBottom, num2) : this.MarginBottom);
							this.MarginLeft = (visualState.GotMarginLeft ? Mathf.Lerp(this._startVisualState.MarginLeft, visualState.MarginLeft, num2) : this.MarginLeft);
							this.MarginRight = (visualState.GotMarginRight ? Mathf.Lerp(this._startVisualState.MarginRight, visualState.MarginRight, num2) : this.MarginRight);
						}
					}
					else
					{
						this.PositionXOffset = (visualState.GotPositionXOffset ? visualState.PositionXOffset : this.PositionXOffset);
						this.PositionYOffset = (visualState.GotPositionYOffset ? visualState.PositionYOffset : this.PositionYOffset);
						this.SuggestedWidth = (visualState.GotSuggestedWidth ? visualState.SuggestedWidth : this.SuggestedWidth);
						this.SuggestedHeight = (visualState.GotSuggestedHeight ? visualState.SuggestedHeight : this.SuggestedHeight);
						this.MarginTop = (visualState.GotMarginTop ? visualState.MarginTop : this.MarginTop);
						this.MarginBottom = (visualState.GotMarginBottom ? visualState.MarginBottom : this.MarginBottom);
						this.MarginLeft = (visualState.GotMarginLeft ? visualState.MarginLeft : this.MarginLeft);
						this.MarginRight = (visualState.GotMarginRight ? visualState.MarginRight : this.MarginRight);
						this._startVisualState = visualState;
						this._currentVisualStateAnimationState = VisualStateAnimationState.None;
					}
				}
				else
				{
					this._currentVisualStateAnimationState = VisualStateAnimationState.None;
				}
			}
			this._stateTimer += dt;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000217EF File Offset: 0x0001F9EF
		internal void Update(float dt)
		{
			this.OnUpdate(dt);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000217F8 File Offset: 0x0001F9F8
		internal void LateUpdate(float dt)
		{
			this.OnLateUpdate(dt);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00021801 File Offset: 0x0001FA01
		internal void ParallelUpdate(float dt)
		{
			if (!this._isInParallelOperation)
			{
				this._isInParallelOperation = true;
				this.OnParallelUpdate(dt);
				this._isInParallelOperation = false;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00021820 File Offset: 0x0001FA20
		protected virtual void OnUpdate(float dt)
		{
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00021822 File Offset: 0x0001FA22
		protected virtual void OnParallelUpdate(float dt)
		{
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00021824 File Offset: 0x0001FA24
		protected virtual void OnLateUpdate(float dt)
		{
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00021826 File Offset: 0x0001FA26
		protected virtual void RefreshState()
		{
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00021828 File Offset: 0x0001FA28
		public virtual void UpdateAnimationPropertiesSubTask(float alphaFactor)
		{
			this.AlphaFactor = alphaFactor;
			foreach (Widget widget in this.Children)
			{
				widget.UpdateAnimationPropertiesSubTask(alphaFactor);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00021880 File Offset: 0x0001FA80
		public void Measure(Vector2 measureSpec)
		{
			if (this.IsHidden)
			{
				this.MeasuredSize = Vector2.Zero;
				return;
			}
			this.OnMeasure(measureSpec);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000218A0 File Offset: 0x0001FAA0
		private Vector2 ProcessSizeWithBoundaries(Vector2 input)
		{
			Vector2 result = input;
			if (this._gotMinWidth && input.X < this.ScaledMinWidth)
			{
				result.X = this.ScaledMinWidth;
			}
			if (this._gotMinHeight && input.Y < this.ScaledMinHeight)
			{
				result.Y = this.ScaledMinHeight;
			}
			if (this._gotMaxWidth && input.X > this.ScaledMaxWidth)
			{
				result.X = this.ScaledMaxWidth;
			}
			if (this._gotMaxHeight && input.Y > this.ScaledMaxHeight)
			{
				result.Y = this.ScaledMaxHeight;
			}
			return result;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002193C File Offset: 0x0001FB3C
		private void OnMeasure(Vector2 measureSpec)
		{
			if (this.WidthSizePolicy == SizePolicy.Fixed)
			{
				measureSpec.X = this.ScaledSuggestedWidth;
			}
			else if (this.WidthSizePolicy == SizePolicy.StretchToParent)
			{
				measureSpec.X -= this.ScaledMarginLeft + this.ScaledMarginRight;
			}
			else
			{
				SizePolicy widthSizePolicy = this.WidthSizePolicy;
			}
			if (this.HeightSizePolicy == SizePolicy.Fixed)
			{
				measureSpec.Y = this.ScaledSuggestedHeight;
			}
			else if (this.HeightSizePolicy == SizePolicy.StretchToParent)
			{
				measureSpec.Y -= this.ScaledMarginTop + this.ScaledMarginBottom;
			}
			else
			{
				SizePolicy heightSizePolicy = this.HeightSizePolicy;
			}
			measureSpec = this.ProcessSizeWithBoundaries(measureSpec);
			Vector2 vector = this.MeasureChildren(measureSpec);
			Vector2 vector2 = new Vector2(0f, 0f);
			if (this.WidthSizePolicy == SizePolicy.Fixed)
			{
				vector2.X = this.ScaledSuggestedWidth;
			}
			else if (this.WidthSizePolicy == SizePolicy.CoverChildren)
			{
				vector2.X = vector.X;
			}
			else if (this.WidthSizePolicy == SizePolicy.StretchToParent)
			{
				vector2.X = measureSpec.X;
			}
			if (this.HeightSizePolicy == SizePolicy.Fixed)
			{
				vector2.Y = this.ScaledSuggestedHeight;
			}
			else if (this.HeightSizePolicy == SizePolicy.CoverChildren)
			{
				vector2.Y = vector.Y;
			}
			else if (this.HeightSizePolicy == SizePolicy.StretchToParent)
			{
				vector2.Y = measureSpec.Y;
			}
			vector2 = this.ProcessSizeWithBoundaries(vector2);
			this.MeasuredSize = vector2;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00021A8C File Offset: 0x0001FC8C
		public bool CheckIsMyChildRecursive(Widget child)
		{
			for (Widget widget = (child != null) ? child.ParentWidget : null; widget != null; widget = widget.ParentWidget)
			{
				if (widget == this)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00021AB9 File Offset: 0x0001FCB9
		private Vector2 MeasureChildren(Vector2 measureSpec)
		{
			return this.LayoutImp.MeasureChildren(this, measureSpec, this.Context.SpriteData, this._scaleToUse);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00021AD9 File Offset: 0x0001FCD9
		public void AddChild(Widget widget)
		{
			widget.ParentWidget = this;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00021AE2 File Offset: 0x0001FCE2
		public void AddChildAtIndex(Widget widget, int index)
		{
			widget.ParentWidget = this;
			widget.SetSiblingIndex(index, false);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00021AF4 File Offset: 0x0001FCF4
		public void SwapChildren(Widget widget1, Widget widget2)
		{
			int index = this._children.IndexOf(widget1);
			int index2 = this._children.IndexOf(widget2);
			Widget value = this._children[index];
			this._children[index] = this._children[index2];
			this._children[index2] = value;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00021B50 File Offset: 0x0001FD50
		protected virtual void OnChildAdded(Widget child)
		{
			this.EventFired("ItemAdd", new object[]
			{
				child
			});
			if (this.DoNotUseCustomScaleAndChildren)
			{
				child.DoNotUseCustomScaleAndChildren = true;
			}
			if (this.UpdateChildrenStates && (!(child is ImageWidget) || !((ImageWidget)child).OverrideDefaultStateSwitchingEnabled))
			{
				child.SetState(this.CurrentState);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00021BAA File Offset: 0x0001FDAA
		public void RemoveChild(Widget widget)
		{
			widget.ParentWidget = null;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		public virtual void OnBeforeRemovedChild(Widget widget)
		{
			if (this.IsHovered)
			{
				this.EventFired("HoverEnd", Array.Empty<object>());
			}
			this.Children.ForEach(delegate(Widget c)
			{
				c.OnBeforeRemovedChild(widget);
			});
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00021BFD File Offset: 0x0001FDFD
		public bool HasChild(Widget widget)
		{
			return this._children.Contains(widget);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00021C0B File Offset: 0x0001FE0B
		protected virtual void OnChildRemoved(Widget child)
		{
			this.EventFired("ItemRemove", new object[]
			{
				child
			});
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00021C22 File Offset: 0x0001FE22
		protected virtual void OnAfterChildRemoved(Widget child)
		{
			this.EventFired("AfterItemRemove", new object[]
			{
				child
			});
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00021C39 File Offset: 0x0001FE39
		public virtual void UpdateBrushes(float dt)
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00021C3B File Offset: 0x0001FE3B
		public int GetChildIndex(Widget child)
		{
			return this._children.IndexOf(child);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00021C4C File Offset: 0x0001FE4C
		public int GetVisibleChildIndex(Widget child)
		{
			int result = -1;
			List<Widget> list = (from c in this._children
			where c.IsVisible
			select c).ToList<Widget>();
			if (list.Count > 0)
			{
				result = list.IndexOf(child);
			}
			return result;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00021CA0 File Offset: 0x0001FEA0
		public int GetFilterChildIndex(Widget child, Func<Widget, bool> childrenFilter)
		{
			int result = -1;
			List<Widget> list = (from c in this._children
			where childrenFilter(c)
			select c).ToList<Widget>();
			if (list.Count > 0)
			{
				result = list.IndexOf(child);
			}
			return result;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00021CEB File Offset: 0x0001FEEB
		public Widget GetChild(int i)
		{
			if (i < this._children.Count)
			{
				return this._children[i];
			}
			return null;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00021D0C File Offset: 0x0001FF0C
		public void Layout(float left, float bottom, float right, float top)
		{
			if (this.IsVisible)
			{
				this.SetLayout(left, bottom, right, top);
				Vector2 scaledPositionOffset = this.ScaledPositionOffset;
				this.Left += scaledPositionOffset.X;
				this.Top += scaledPositionOffset.Y;
				this.OnLayout(this.Left, this.Bottom, this.Right, this.Top);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00021D78 File Offset: 0x0001FF78
		private void SetLayout(float left, float bottom, float right, float top)
		{
			left += this.ScaledMarginLeft;
			right -= this.ScaledMarginRight;
			top += this.ScaledMarginTop;
			bottom -= this.ScaledMarginBottom;
			float num = right - left;
			float num2 = bottom - top;
			float left2;
			if (this.HorizontalAlignment == HorizontalAlignment.Left)
			{
				left2 = left;
			}
			else if (this.HorizontalAlignment == HorizontalAlignment.Center)
			{
				left2 = left + num / 2f - this.MeasuredSize.X / 2f;
			}
			else
			{
				left2 = right - this.MeasuredSize.X;
			}
			float top2;
			if (this.VerticalAlignment == VerticalAlignment.Top)
			{
				top2 = top;
			}
			else if (this.VerticalAlignment == VerticalAlignment.Center)
			{
				top2 = top + num2 / 2f - this.MeasuredSize.Y / 2f;
			}
			else
			{
				top2 = bottom - this.MeasuredSize.Y;
			}
			this.Left = left2;
			this.Top = top2;
			this.Size = this.MeasuredSize;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00021E55 File Offset: 0x00020055
		private void OnLayout(float left, float bottom, float right, float top)
		{
			this.LayoutImp.OnLayout(this, left, bottom, right, top);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00021E68 File Offset: 0x00020068
		internal void DoTweenPosition(float dt)
		{
			if (this.IsVisible && dt > 0f)
			{
				float num = this.Left - this.LocalPosition.X;
				float num2 = this.Top - this.LocalPosition.Y;
				if (Mathf.Abs(num) + Mathf.Abs(num2) < 0.003f)
				{
					this.LocalPosition = new Vector2(this.Left, this.Top);
					return;
				}
				num = Mathf.Clamp(num, -100f, 100f);
				num2 = Mathf.Clamp(num2, -100f, 100f);
				float num3 = Mathf.Min(dt * 18f, 1f);
				this.LocalPosition = new Vector2(this.LocalPosition.X + num3 * num, this.LocalPosition.Y + num3 * num2);
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00021F3C File Offset: 0x0002013C
		internal void ParallelUpdateChildPositions(Vector2 globalPosition)
		{
			Widget.<>c__DisplayClass461_0 CS$<>8__locals1 = new Widget.<>c__DisplayClass461_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.globalPosition = globalPosition;
			TWParallel.For(0, this._children.Count, new TWParallel.ParallelForAuxPredicate(CS$<>8__locals1.<ParallelUpdateChildPositions>g__UpdateChildPositionMT|0), 16);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00021F7C File Offset: 0x0002017C
		internal void UpdatePosition(Vector2 parentPosition)
		{
			if (this.IsVisible)
			{
				if (!this.TweenPosition)
				{
					this.LocalPosition = new Vector2(this.Left, this.Top);
				}
				Vector2 vector = this.LocalPosition + parentPosition;
				if (this._children.Count >= 64)
				{
					this.ParallelUpdateChildPositions(vector);
				}
				else
				{
					for (int i = 0; i < this._children.Count; i++)
					{
						this._children[i].UpdatePosition(vector);
					}
				}
				this._cachedGlobalPosition = vector;
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00022004 File Offset: 0x00020204
		public virtual void HandleInput(IReadOnlyList<int> lastKeysPressed)
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00022008 File Offset: 0x00020208
		public bool IsPointInsideMeasuredArea(Vector2 p)
		{
			Vector2 globalPosition = this.GlobalPosition;
			return globalPosition.X <= p.X && globalPosition.Y <= p.Y && globalPosition.X + this.Size.X >= p.X && globalPosition.Y + this.Size.Y >= p.Y;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00022074 File Offset: 0x00020274
		public bool IsPointInsideGamepadCursorArea(Vector2 p)
		{
			Vector2 globalPosition = this.GlobalPosition;
			globalPosition.X -= this.ExtendCursorAreaLeft;
			globalPosition.Y -= this.ExtendCursorAreaTop;
			Vector2 size = this.Size;
			size.X += this.ExtendCursorAreaLeft + this.ExtendCursorAreaRight;
			size.Y += this.ExtendCursorAreaTop + this.ExtendCursorAreaBottom;
			return p.X >= globalPosition.X && p.Y > globalPosition.Y && p.X < globalPosition.X + size.X && p.Y < globalPosition.Y + size.Y;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00022129 File Offset: 0x00020329
		public void Hide()
		{
			this.IsHidden = true;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00022132 File Offset: 0x00020332
		public void Show()
		{
			this.IsHidden = false;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002213B File Offset: 0x0002033B
		public Vector2 GetLocalPoint(Vector2 globalPoint)
		{
			return globalPoint - this.GlobalPosition;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002214C File Offset: 0x0002034C
		public void SetSiblingIndex(int index, bool force = false)
		{
			int siblingIndex = this.GetSiblingIndex();
			if (siblingIndex != index || force)
			{
				this.ParentWidget._children.RemoveAt(siblingIndex);
				this.ParentWidget._children.Insert(index, this);
				this.SetMeasureAndLayoutDirty();
				this.EventFired("SiblingIndexChanged", Array.Empty<object>());
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000221A4 File Offset: 0x000203A4
		public int GetSiblingIndex()
		{
			Widget parentWidget = this.ParentWidget;
			if (parentWidget == null)
			{
				return -1;
			}
			return parentWidget.GetChildIndex(this);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000221B8 File Offset: 0x000203B8
		public int GetVisibleSiblingIndex()
		{
			return this.ParentWidget.GetVisibleChildIndex(this);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000221C6 File Offset: 0x000203C6
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x000221CE File Offset: 0x000203CE
		public bool DisableRender { get; set; }

		// Token: 0x0600087B RID: 2171 RVA: 0x000221D8 File Offset: 0x000203D8
		public void Render(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (!this.IsHidden && !this.DisableRender)
			{
				Vector2 cachedGlobalPosition = this._cachedGlobalPosition;
				if (this.ClipHorizontalContent || this.ClipVerticalContent)
				{
					int width = this.ClipHorizontalContent ? ((int)this.Size.X) : -1;
					int height = this.ClipVerticalContent ? ((int)this.Size.Y) : -1;
					drawContext.PushScissor((int)cachedGlobalPosition.X, (int)cachedGlobalPosition.Y, width, height);
				}
				if (this.CircularClipEnabled)
				{
					if (this.IsCircularClipRadiusHalfOfHeight)
					{
						this.CircularClipRadius = this.Size.Y / 2f * this._inverseScaleToUse;
					}
					else if (this.IsCircularClipRadiusHalfOfWidth)
					{
						this.CircularClipRadius = this.Size.X / 2f * this._inverseScaleToUse;
					}
					Vector2 position = new Vector2(cachedGlobalPosition.X + this.Size.X * 0.5f + this.CircularClipXOffset * this._scaleToUse, cachedGlobalPosition.Y + this.Size.Y * 0.5f + this.CircularClipYOffset * this._scaleToUse);
					drawContext.SetCircualMask(position, this.CircularClipRadius * this._scaleToUse, this.CircularClipSmoothingRadius * this._scaleToUse);
				}
				bool flag = false;
				if (drawContext.ScissorTestEnabled)
				{
					ScissorTestInfo currentScissor = drawContext.CurrentScissor;
					Rectangle rectangle = new Rectangle(cachedGlobalPosition.X, cachedGlobalPosition.Y, this.Size.X, this.Size.Y);
					Rectangle other = new Rectangle((float)currentScissor.X, (float)currentScissor.Y, (float)currentScissor.Width, (float)currentScissor.Height);
					if (rectangle.IsCollide(other) || this._calculateSizeFirstFrame)
					{
						flag = (!this.DoNotRenderIfNotFullyInsideScissor || rectangle.IsSubRectOf(other));
					}
				}
				else
				{
					Rectangle rectangle2 = new Rectangle(this._cachedGlobalPosition.X, this._cachedGlobalPosition.Y, this.MeasuredSize.X, this.MeasuredSize.Y);
					Rectangle other2 = new Rectangle(this.EventManager.LeftUsableAreaStart, this.EventManager.TopUsableAreaStart, this.EventManager.PageSize.X, this.EventManager.PageSize.Y);
					if (rectangle2.IsCollide(other2) || this._calculateSizeFirstFrame)
					{
						flag = true;
					}
				}
				if (flag)
				{
					this.OnRender(twoDimensionContext, drawContext);
					for (int i = 0; i < this._children.Count; i++)
					{
						Widget widget = this._children[i];
						if (!widget.RenderLate)
						{
							widget.Render(twoDimensionContext, drawContext);
						}
					}
					for (int j = 0; j < this._children.Count; j++)
					{
						Widget widget2 = this._children[j];
						if (widget2.RenderLate)
						{
							widget2.Render(twoDimensionContext, drawContext);
						}
					}
				}
				if (this.CircularClipEnabled)
				{
					drawContext.ClearCircualMask();
				}
				if (this.ClipHorizontalContent || this.ClipVerticalContent)
				{
					drawContext.PopScissor();
				}
			}
			this._calculateSizeFirstFrame = false;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000224F0 File Offset: 0x000206F0
		protected virtual void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			Vector2 globalPosition = this.GlobalPosition;
			if (this.ForcePixelPerfectRenderPlacement)
			{
				globalPosition.X = (float)MathF.Round(globalPosition.X);
				globalPosition.Y = (float)MathF.Round(globalPosition.Y);
			}
			if (this._sprite != null)
			{
				Texture texture = this._sprite.Texture;
				if (texture != null)
				{
					float num = globalPosition.X;
					float num2 = globalPosition.Y;
					SimpleMaterial simpleMaterial = drawContext.CreateSimpleMaterial();
					simpleMaterial.OverlayEnabled = false;
					simpleMaterial.CircularMaskingEnabled = false;
					simpleMaterial.Texture = texture;
					simpleMaterial.Color = this.Color;
					simpleMaterial.ColorFactor = this.ColorFactor;
					simpleMaterial.AlphaFactor = this.AlphaFactor * this.Context.ContextAlpha;
					simpleMaterial.HueFactor = 0f;
					simpleMaterial.SaturationFactor = this.SaturationFactor;
					simpleMaterial.ValueFactor = this.ValueFactor;
					float num3 = this.ExtendLeft;
					if (this.HorizontalFlip)
					{
						num3 = this.ExtendRight;
					}
					float num4 = this.Size.X;
					num4 += (this.ExtendRight + this.ExtendLeft) * this._scaleToUse;
					num -= num3 * this._scaleToUse;
					float num5 = this.Size.Y;
					float num6 = this.ExtendTop;
					if (this.HorizontalFlip)
					{
						num6 = this.ExtendBottom;
					}
					num5 += (this.ExtendTop + this.ExtendBottom) * this._scaleToUse;
					num2 -= num6 * this._scaleToUse;
					drawContext.DrawSprite(this._sprite, simpleMaterial, num, num2, this._scaleToUse, num4, num5, this.HorizontalFlip, this.VerticalFlip);
				}
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00022690 File Offset: 0x00020890
		protected void EventFired(string eventName, params object[] args)
		{
			if (this._eventTargets != null)
			{
				for (int i = 0; i < this._eventTargets.Count; i++)
				{
					this._eventTargets[i](this, eventName, args);
				}
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600087E RID: 2174 RVA: 0x000226CF File Offset: 0x000208CF
		// (remove) Token: 0x0600087F RID: 2175 RVA: 0x000226F0 File Offset: 0x000208F0
		public event Action<Widget, string, object[]> EventFire
		{
			add
			{
				if (this._eventTargets == null)
				{
					this._eventTargets = new List<Action<Widget, string, object[]>>();
				}
				this._eventTargets.Add(value);
			}
			remove
			{
				if (this._eventTargets != null)
				{
					this._eventTargets.Remove(value);
				}
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000880 RID: 2176 RVA: 0x00022708 File Offset: 0x00020908
		// (remove) Token: 0x06000881 RID: 2177 RVA: 0x00022740 File Offset: 0x00020940
		public event Action<Widget> OnVisibilityChanged;

		// Token: 0x06000882 RID: 2178 RVA: 0x00022778 File Offset: 0x00020978
		public bool IsRecursivelyVisible()
		{
			for (Widget widget = this; widget != null; widget = widget.ParentWidget)
			{
				if (!widget.IsVisible)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002279E File Offset: 0x0002099E
		internal void HandleOnDisconnectedFromRoot()
		{
			this.OnDisconnectedFromRoot();
			if (this.IsHovered)
			{
				this.EventFired("HoverEnd", Array.Empty<object>());
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000227BE File Offset: 0x000209BE
		internal void HandleOnConnectedToRoot()
		{
			if (!this._seedSet)
			{
				this._seed = this.GetSiblingIndex();
				this._seedSet = true;
			}
			this.OnConnectedToRoot();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000227E1 File Offset: 0x000209E1
		protected virtual void OnDisconnectedFromRoot()
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000227E3 File Offset: 0x000209E3
		protected virtual void OnConnectedToRoot()
		{
			this.EventFired("ConnectedToRoot", Array.Empty<object>());
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000227F5 File Offset: 0x000209F5
		public override string ToString()
		{
			return this.GetFullIDPath();
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x000227FD File Offset: 0x000209FD
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x00022805 File Offset: 0x00020A05
		public float ExtendCursorAreaTop { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002280E File Offset: 0x00020A0E
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x00022816 File Offset: 0x00020A16
		public float ExtendCursorAreaRight { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002281F File Offset: 0x00020A1F
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x00022827 File Offset: 0x00020A27
		public float ExtendCursorAreaBottom { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00022830 File Offset: 0x00020A30
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x00022838 File Offset: 0x00020A38
		public float ExtendCursorAreaLeft { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00022841 File Offset: 0x00020A41
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00022849 File Offset: 0x00020A49
		public float CursorAreaXOffset { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00022852 File Offset: 0x00020A52
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0002285A File Offset: 0x00020A5A
		public float CursorAreaYOffset { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00022863 File Offset: 0x00020A63
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0002286E File Offset: 0x00020A6E
		public bool AcceptNavigation
		{
			get
			{
				return !this.DoNotAcceptNavigation;
			}
			set
			{
				if (value == this.DoNotAcceptNavigation)
				{
					this.DoNotAcceptNavigation = !value;
				}
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00022883 File Offset: 0x00020A83
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0002288B File Offset: 0x00020A8B
		public bool DoNotAcceptNavigation
		{
			get
			{
				return this._doNotAcceptNavigation;
			}
			set
			{
				if (value != this._doNotAcceptNavigation)
				{
					this._doNotAcceptNavigation = value;
					this.GamepadNavigationContext.OnWidgetNavigationStatusChanged(this);
				}
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000228A9 File Offset: 0x00020AA9
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x000228B1 File Offset: 0x00020AB1
		public bool IsUsingNavigation
		{
			get
			{
				return this._isUsingNavigation;
			}
			set
			{
				if (value != this._isUsingNavigation)
				{
					this._isUsingNavigation = value;
					base.OnPropertyChanged(value, "IsUsingNavigation");
				}
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x000228CF File Offset: 0x00020ACF
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x000228D7 File Offset: 0x00020AD7
		public bool UseSiblingIndexForNavigation
		{
			get
			{
				return this._useSiblingIndexForNavigation;
			}
			set
			{
				if (value != this._useSiblingIndexForNavigation)
				{
					this._useSiblingIndexForNavigation = value;
					if (value)
					{
						this.GamepadNavigationIndex = this.GetSiblingIndex();
					}
				}
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x000228F8 File Offset: 0x00020AF8
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x00022900 File Offset: 0x00020B00
		public int GamepadNavigationIndex
		{
			get
			{
				return this._gamepadNavigationIndex;
			}
			set
			{
				if (value != this._gamepadNavigationIndex)
				{
					this._gamepadNavigationIndex = value;
					this.GamepadNavigationContext.OnWidgetNavigationIndexUpdated(this);
					this.OnGamepadNavigationIndexUpdated(value);
					base.OnPropertyChanged(value, "GamepadNavigationIndex");
				}
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00022931 File Offset: 0x00020B31
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00022939 File Offset: 0x00020B39
		public GamepadNavigationTypes UsedNavigationMovements
		{
			get
			{
				return this._usedNavigationMovements;
			}
			set
			{
				if (value != this._usedNavigationMovements)
				{
					this._usedNavigationMovements = value;
					this.Context.GamepadNavigation.OnWidgetUsedNavigationMovementsUpdated(this);
				}
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002295C File Offset: 0x00020B5C
		protected virtual void OnGamepadNavigationIndexUpdated(int newIndex)
		{
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002295E File Offset: 0x00020B5E
		public void OnGamepadNavigationFocusGain()
		{
			Action<Widget> onGamepadNavigationFocusGained = this.OnGamepadNavigationFocusGained;
			if (onGamepadNavigationFocusGained == null)
			{
				return;
			}
			onGamepadNavigationFocusGained(this);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00022974 File Offset: 0x00020B74
		internal bool PreviewEvent(GauntletEvent gauntletEvent)
		{
			bool result = false;
			switch (gauntletEvent)
			{
			case GauntletEvent.MouseMove:
				result = this.OnPreviewMouseMove();
				break;
			case GauntletEvent.MousePressed:
				result = this.OnPreviewMousePressed();
				break;
			case GauntletEvent.MouseReleased:
				result = this.OnPreviewMouseReleased();
				break;
			case GauntletEvent.MouseAlternatePressed:
				result = this.OnPreviewMouseAlternatePressed();
				break;
			case GauntletEvent.MouseAlternateReleased:
				result = this.OnPreviewMouseAlternateReleased();
				break;
			case GauntletEvent.DragHover:
				result = this.OnPreviewDragHover();
				break;
			case GauntletEvent.DragBegin:
				result = this.OnPreviewDragBegin();
				break;
			case GauntletEvent.DragEnd:
				result = this.OnPreviewDragEnd();
				break;
			case GauntletEvent.Drop:
				result = this.OnPreviewDrop();
				break;
			case GauntletEvent.MouseScroll:
				result = this.OnPreviewMouseScroll();
				break;
			case GauntletEvent.RightStickMovement:
				result = this.OnPreviewRightStickMovement();
				break;
			}
			return result;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00022A19 File Offset: 0x00020C19
		protected virtual bool OnPreviewMousePressed()
		{
			return true;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00022A1C File Offset: 0x00020C1C
		protected virtual bool OnPreviewMouseReleased()
		{
			return true;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00022A1F File Offset: 0x00020C1F
		protected virtual bool OnPreviewMouseAlternatePressed()
		{
			return true;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00022A22 File Offset: 0x00020C22
		protected virtual bool OnPreviewMouseAlternateReleased()
		{
			return true;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00022A25 File Offset: 0x00020C25
		protected virtual bool OnPreviewDragBegin()
		{
			return this.AcceptDrag;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00022A2D File Offset: 0x00020C2D
		protected virtual bool OnPreviewDragEnd()
		{
			return this.AcceptDrag;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00022A35 File Offset: 0x00020C35
		protected virtual bool OnPreviewDrop()
		{
			return this.AcceptDrop;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00022A3D File Offset: 0x00020C3D
		protected virtual bool OnPreviewMouseScroll()
		{
			return false;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00022A40 File Offset: 0x00020C40
		protected virtual bool OnPreviewRightStickMovement()
		{
			return false;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00022A43 File Offset: 0x00020C43
		protected virtual bool OnPreviewMouseMove()
		{
			return true;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00022A46 File Offset: 0x00020C46
		protected virtual bool OnPreviewDragHover()
		{
			return false;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00022A49 File Offset: 0x00020C49
		protected internal virtual void OnMousePressed()
		{
			this.IsPressed = true;
			this.EventFired("MouseDown", Array.Empty<object>());
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00022A62 File Offset: 0x00020C62
		protected internal virtual void OnMouseReleased()
		{
			this.IsPressed = false;
			this.EventFired("MouseUp", Array.Empty<object>());
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00022A7B File Offset: 0x00020C7B
		protected internal virtual void OnMouseAlternatePressed()
		{
			this.EventFired("MouseAlternateDown", Array.Empty<object>());
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00022A8D File Offset: 0x00020C8D
		protected internal virtual void OnMouseAlternateReleased()
		{
			this.EventFired("MouseAlternateUp", Array.Empty<object>());
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00022A9F File Offset: 0x00020C9F
		protected internal virtual void OnMouseMove()
		{
			this.EventFired("MouseMove", Array.Empty<object>());
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00022AB1 File Offset: 0x00020CB1
		protected internal virtual void OnHoverBegin()
		{
			this.IsHovered = true;
			this.EventFired("HoverBegin", Array.Empty<object>());
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00022ACA File Offset: 0x00020CCA
		protected internal virtual void OnHoverEnd()
		{
			this.EventFired("HoverEnd", Array.Empty<object>());
			this.IsHovered = false;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00022AE3 File Offset: 0x00020CE3
		protected internal virtual void OnDragBegin()
		{
			this.EventManager.BeginDragging(this);
			this.EventFired("DragBegin", Array.Empty<object>());
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00022B01 File Offset: 0x00020D01
		protected internal virtual void OnDragEnd()
		{
			this.EventFired("DragEnd", Array.Empty<object>());
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00022B14 File Offset: 0x00020D14
		protected internal virtual bool OnDrop()
		{
			if (this.AcceptDrop)
			{
				bool flag = true;
				if (this.AcceptDropHandler != null)
				{
					flag = this.AcceptDropHandler(this, this.EventManager.DraggedWidget);
				}
				if (flag)
				{
					Widget widget = this.EventManager.ReleaseDraggedWidget();
					int num = -1;
					if (!this.DropEventHandledManually)
					{
						widget.ParentWidget = this;
					}
					this.EventFired("Drop", new object[]
					{
						widget,
						num
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00022B8C File Offset: 0x00020D8C
		protected internal virtual void OnMouseScroll()
		{
			this.EventFired("MouseScroll", Array.Empty<object>());
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00022B9E File Offset: 0x00020D9E
		protected internal virtual void OnRightStickMovement()
		{
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00022BA0 File Offset: 0x00020DA0
		protected internal virtual void OnDragHoverBegin()
		{
			this.EventFired("DragHoverBegin", Array.Empty<object>());
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00022BB2 File Offset: 0x00020DB2
		protected internal virtual void OnDragHoverEnd()
		{
			this.EventFired("DragHoverEnd", Array.Empty<object>());
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00022BC4 File Offset: 0x00020DC4
		protected internal virtual void OnGainFocus()
		{
			this.IsFocused = true;
			this.EventFired("FocusGained", Array.Empty<object>());
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00022BDD File Offset: 0x00020DDD
		protected internal virtual void OnLoseFocus()
		{
			this.IsFocused = false;
			this.EventFired("FocusLost", Array.Empty<object>());
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00022BF6 File Offset: 0x00020DF6
		protected internal virtual void OnMouseOverBegin()
		{
			this.EventFired("MouseOverBegin", Array.Empty<object>());
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00022C08 File Offset: 0x00020E08
		protected internal virtual void OnMouseOverEnd()
		{
			this.EventFired("MouseOverEnd", Array.Empty<object>());
		}

		// Token: 0x04000374 RID: 884
		private Color _color = Color.White;

		// Token: 0x04000380 RID: 896
		private string _id;

		// Token: 0x04000382 RID: 898
		protected Vector2 _cachedGlobalPosition;

		// Token: 0x04000383 RID: 899
		private Widget _parent;

		// Token: 0x04000384 RID: 900
		private List<Widget> _children;

		// Token: 0x04000385 RID: 901
		private bool _doNotUseCustomScaleAndChildren;

		// Token: 0x04000387 RID: 903
		protected bool _calculateSizeFirstFrame = true;

		// Token: 0x04000389 RID: 905
		private float _suggestedWidth;

		// Token: 0x0400038A RID: 906
		private float _suggestedHeight;

		// Token: 0x0400038B RID: 907
		private bool _tweenPosition;

		// Token: 0x0400038C RID: 908
		private string _hoveredCursorState;

		// Token: 0x0400038D RID: 909
		private bool _alternateClickEventHasSpecialEvent;

		// Token: 0x0400038E RID: 910
		private Vector2 _positionOffset;

		// Token: 0x04000391 RID: 913
		private float _marginTop;

		// Token: 0x04000392 RID: 914
		private float _marginLeft;

		// Token: 0x04000393 RID: 915
		private float _marginBottom;

		// Token: 0x04000394 RID: 916
		private float _marginRight;

		// Token: 0x04000395 RID: 917
		private VerticalAlignment _verticalAlignment;

		// Token: 0x04000396 RID: 918
		private HorizontalAlignment _horizontalAlignment;

		// Token: 0x04000397 RID: 919
		private Vector2 _topLeft;

		// Token: 0x04000398 RID: 920
		private bool _forcePixelPerfectRenderPlacement;

		// Token: 0x0400039A RID: 922
		private SizePolicy _widthSizePolicy;

		// Token: 0x0400039B RID: 923
		private SizePolicy _heightSizePolicy;

		// Token: 0x0400039F RID: 927
		private Widget _dragWidget;

		// Token: 0x040003AB RID: 939
		private bool _isHovered;

		// Token: 0x040003AC RID: 940
		private bool _isDisabled;

		// Token: 0x040003AD RID: 941
		private bool _isFocusable;

		// Token: 0x040003AE RID: 942
		private bool _isFocused;

		// Token: 0x040003AF RID: 943
		private bool _restartAnimationFirstFrame;

		// Token: 0x040003B0 RID: 944
		private bool _doNotPassEventsToChildren;

		// Token: 0x040003B1 RID: 945
		private bool _doNotAcceptEvents;

		// Token: 0x040003B2 RID: 946
		public Func<Widget, Widget, bool> AcceptDropHandler;

		// Token: 0x040003B3 RID: 947
		private bool _isPressed;

		// Token: 0x040003B4 RID: 948
		private bool _isHidden;

		// Token: 0x040003B5 RID: 949
		private Sprite _sprite;

		// Token: 0x040003B6 RID: 950
		private VisualDefinition _visualDefinition;

		// Token: 0x040003B7 RID: 951
		private List<string> _states;

		// Token: 0x040003B8 RID: 952
		protected float _stateTimer;

		// Token: 0x040003BA RID: 954
		protected VisualState _startVisualState;

		// Token: 0x040003BB RID: 955
		protected VisualStateAnimationState _currentVisualStateAnimationState;

		// Token: 0x040003BC RID: 956
		private bool _updateChildrenStates;

		// Token: 0x040003C7 RID: 967
		protected int _seed;

		// Token: 0x040003C8 RID: 968
		private bool _seedSet;

		// Token: 0x040003C9 RID: 969
		private float _maxWidth;

		// Token: 0x040003CA RID: 970
		private float _maxHeight;

		// Token: 0x040003CB RID: 971
		private float _minWidth;

		// Token: 0x040003CC RID: 972
		private float _minHeight;

		// Token: 0x040003CD RID: 973
		private bool _gotMaxWidth;

		// Token: 0x040003CE RID: 974
		private bool _gotMaxHeight;

		// Token: 0x040003CF RID: 975
		private bool _gotMinWidth;

		// Token: 0x040003D0 RID: 976
		private bool _gotMinHeight;

		// Token: 0x040003D1 RID: 977
		private List<WidgetComponent> _components;

		// Token: 0x040003D2 RID: 978
		private bool _isInParallelOperation;

		// Token: 0x040003D4 RID: 980
		private List<Action<Widget, string, object[]>> _eventTargets;

		// Token: 0x040003DC RID: 988
		private bool _doNotAcceptNavigation;

		// Token: 0x040003DD RID: 989
		private bool _isUsingNavigation;

		// Token: 0x040003DE RID: 990
		private bool _useSiblingIndexForNavigation;

		// Token: 0x040003DF RID: 991
		protected internal int _gamepadNavigationIndex = -1;

		// Token: 0x040003E0 RID: 992
		private GamepadNavigationTypes _usedNavigationMovements;

		// Token: 0x040003E1 RID: 993
		public Action<Widget> OnGamepadNavigationFocusGained;
	}
}
