using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.GamepadNavigation
{
	// Token: 0x02000048 RID: 72
	public class GamepadNavigationScope
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00011FBC File Offset: 0x000101BC
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00011FC4 File Offset: 0x000101C4
		public string ScopeID { get; set; } = "DefaultScopeID";

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00011FCD File Offset: 0x000101CD
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00011FD5 File Offset: 0x000101D5
		public bool IsActiveScope { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00011FDE File Offset: 0x000101DE
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00011FE6 File Offset: 0x000101E6
		public bool DoNotAutomaticallyFindChildren { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00011FEF File Offset: 0x000101EF
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00011FF7 File Offset: 0x000101F7
		public GamepadNavigationTypes ScopeMovements { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00012000 File Offset: 0x00010200
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00012008 File Offset: 0x00010208
		public GamepadNavigationTypes AlternateScopeMovements { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00012011 File Offset: 0x00010211
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00012019 File Offset: 0x00010219
		public int AlternateMovementStepSize { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00012022 File Offset: 0x00010222
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001202A File Offset: 0x0001022A
		public bool HasCircularMovement { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00012033 File Offset: 0x00010233
		public ReadOnlyCollection<Widget> NavigatableWidgets { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001203B File Offset: 0x0001023B
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00012044 File Offset: 0x00010244
		public Widget ParentWidget
		{
			get
			{
				return this._parentWidget;
			}
			set
			{
				if (value != this._parentWidget)
				{
					if (this._parentWidget != null)
					{
						this._invisibleParents.Clear();
						for (Widget parentWidget = this._parentWidget; parentWidget != null; parentWidget = parentWidget.ParentWidget)
						{
							parentWidget.OnVisibilityChanged -= this.OnParentVisibilityChanged;
						}
					}
					this._parentWidget = value;
					for (Widget parentWidget2 = this._parentWidget; parentWidget2 != null; parentWidget2 = parentWidget2.ParentWidget)
					{
						if (!parentWidget2.IsVisible)
						{
							this._invisibleParents.Add(parentWidget2);
						}
						parentWidget2.OnVisibilityChanged += this.OnParentVisibilityChanged;
					}
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000120D2 File Offset: 0x000102D2
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000120DA File Offset: 0x000102DA
		public int LatestNavigationElementIndex { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000120E3 File Offset: 0x000102E3
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000120EB File Offset: 0x000102EB
		public bool DoNotAutoGainNavigationOnInit { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000120F4 File Offset: 0x000102F4
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x000120FC File Offset: 0x000102FC
		public bool ForceGainNavigationBasedOnDirection { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00012105 File Offset: 0x00010305
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001210D File Offset: 0x0001030D
		public bool ForceGainNavigationOnClosestChild { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00012116 File Offset: 0x00010316
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001211E File Offset: 0x0001031E
		public bool NavigateFromScopeEdges { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00012127 File Offset: 0x00010327
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0001212F File Offset: 0x0001032F
		public bool UseDiscoveryAreaAsScopeEdges { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00012138 File Offset: 0x00010338
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00012140 File Offset: 0x00010340
		public bool DoNotAutoNavigateAfterSort { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00012149 File Offset: 0x00010349
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00012151 File Offset: 0x00010351
		public bool FollowMobileTargets { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0001215A File Offset: 0x0001035A
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00012162 File Offset: 0x00010362
		public bool DoNotAutoCollectChildScopes { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001216B File Offset: 0x0001036B
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00012173 File Offset: 0x00010373
		public bool IsDefaultNavigationScope { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001217C File Offset: 0x0001037C
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00012184 File Offset: 0x00010384
		public float ExtendDiscoveryAreaRight { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001218D File Offset: 0x0001038D
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00012195 File Offset: 0x00010395
		public float ExtendDiscoveryAreaTop { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001219E File Offset: 0x0001039E
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x000121A6 File Offset: 0x000103A6
		public float ExtendDiscoveryAreaBottom { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000121AF File Offset: 0x000103AF
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000121B7 File Offset: 0x000103B7
		public float ExtendDiscoveryAreaLeft { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000121C0 File Offset: 0x000103C0
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x000121C8 File Offset: 0x000103C8
		public float ExtendChildrenCursorAreaLeft
		{
			get
			{
				return this._extendChildrenCursorAreaLeft;
			}
			set
			{
				if (value != this._extendChildrenCursorAreaLeft)
				{
					this._extendChildrenCursorAreaLeft = value;
					for (int i = 0; i < this._navigatableWidgets.Count; i++)
					{
						this._navigatableWidgets[i].ExtendCursorAreaLeft = value;
					}
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001220D File Offset: 0x0001040D
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00012218 File Offset: 0x00010418
		public float ExtendChildrenCursorAreaRight
		{
			get
			{
				return this._extendChildrenCursorAreaRight;
			}
			set
			{
				if (value != this._extendChildrenCursorAreaRight)
				{
					this._extendChildrenCursorAreaRight = value;
					for (int i = 0; i < this._navigatableWidgets.Count; i++)
					{
						this._navigatableWidgets[i].ExtendCursorAreaRight = value;
					}
				}
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001225D File Offset: 0x0001045D
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00012268 File Offset: 0x00010468
		public float ExtendChildrenCursorAreaTop
		{
			get
			{
				return this._extendChildrenCursorAreaTop;
			}
			set
			{
				if (value != this._extendChildrenCursorAreaTop)
				{
					this._extendChildrenCursorAreaTop = value;
					for (int i = 0; i < this._navigatableWidgets.Count; i++)
					{
						this._navigatableWidgets[i].ExtendCursorAreaTop = value;
					}
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000122AD File Offset: 0x000104AD
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x000122B8 File Offset: 0x000104B8
		public float ExtendChildrenCursorAreaBottom
		{
			get
			{
				return this._extendChildrenCursorAreaBottom;
			}
			set
			{
				if (value != this._extendChildrenCursorAreaBottom)
				{
					this._extendChildrenCursorAreaBottom = value;
					for (int i = 0; i < this._navigatableWidgets.Count; i++)
					{
						this._navigatableWidgets[i].ExtendCursorAreaBottom = value;
					}
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000122FD File Offset: 0x000104FD
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00012305 File Offset: 0x00010505
		public float DiscoveryAreaOffsetX { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001230E File Offset: 0x0001050E
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00012316 File Offset: 0x00010516
		public float DiscoveryAreaOffsetY { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001231F File Offset: 0x0001051F
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00012327 File Offset: 0x00010527
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					this.IsDisabled = !value;
					Action<GamepadNavigationScope> onNavigatableWidgetsChanged = this.OnNavigatableWidgetsChanged;
					if (onNavigatableWidgetsChanged == null)
					{
						return;
					}
					onNavigatableWidgetsChanged(this);
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00012354 File Offset: 0x00010554
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001235C File Offset: 0x0001055C
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					this.IsEnabled = !value;
				}
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00012378 File Offset: 0x00010578
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00012386 File Offset: 0x00010586
		public string UpNavigationScopeID
		{
			get
			{
				return this.ManualScopeIDs[GamepadNavigationTypes.Up];
			}
			set
			{
				this.ManualScopeIDs[GamepadNavigationTypes.Up] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00012395 File Offset: 0x00010595
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x000123A3 File Offset: 0x000105A3
		public string RightNavigationScopeID
		{
			get
			{
				return this.ManualScopeIDs[GamepadNavigationTypes.Right];
			}
			set
			{
				this.ManualScopeIDs[GamepadNavigationTypes.Right] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x000123B2 File Offset: 0x000105B2
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x000123C0 File Offset: 0x000105C0
		public string DownNavigationScopeID
		{
			get
			{
				return this.ManualScopeIDs[GamepadNavigationTypes.Down];
			}
			set
			{
				this.ManualScopeIDs[GamepadNavigationTypes.Down] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000123CF File Offset: 0x000105CF
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x000123DD File Offset: 0x000105DD
		public string LeftNavigationScopeID
		{
			get
			{
				return this.ManualScopeIDs[GamepadNavigationTypes.Left];
			}
			set
			{
				this.ManualScopeIDs[GamepadNavigationTypes.Left] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000123EC File Offset: 0x000105EC
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000123FA File Offset: 0x000105FA
		public GamepadNavigationScope UpNavigationScope
		{
			get
			{
				return this.ManualScopes[GamepadNavigationTypes.Up];
			}
			set
			{
				this.ManualScopes[GamepadNavigationTypes.Up] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00012409 File Offset: 0x00010609
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00012417 File Offset: 0x00010617
		public GamepadNavigationScope RightNavigationScope
		{
			get
			{
				return this.ManualScopes[GamepadNavigationTypes.Right];
			}
			set
			{
				this.ManualScopes[GamepadNavigationTypes.Right] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00012426 File Offset: 0x00010626
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00012434 File Offset: 0x00010634
		public GamepadNavigationScope DownNavigationScope
		{
			get
			{
				return this.ManualScopes[GamepadNavigationTypes.Down];
			}
			set
			{
				this.ManualScopes[GamepadNavigationTypes.Down] = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00012443 File Offset: 0x00010643
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00012451 File Offset: 0x00010651
		public GamepadNavigationScope LeftNavigationScope
		{
			get
			{
				return this.ManualScopes[GamepadNavigationTypes.Left];
			}
			set
			{
				this.ManualScopes[GamepadNavigationTypes.Left] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00012460 File Offset: 0x00010660
		internal Widget LastNavigatedWidget
		{
			get
			{
				if (this.LatestNavigationElementIndex >= 0 && this.LatestNavigationElementIndex < this._navigatableWidgets.Count)
				{
					return this._navigatableWidgets[this.LatestNavigationElementIndex];
				}
				return null;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00012491 File Offset: 0x00010691
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00012499 File Offset: 0x00010699
		internal bool IsInitialized { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000124A2 File Offset: 0x000106A2
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x000124AA File Offset: 0x000106AA
		internal GamepadNavigationScope PreviousScope { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000124B3 File Offset: 0x000106B3
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x000124BB File Offset: 0x000106BB
		internal Dictionary<GamepadNavigationTypes, string> ManualScopeIDs { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000124C4 File Offset: 0x000106C4
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x000124CC File Offset: 0x000106CC
		internal Dictionary<GamepadNavigationTypes, GamepadNavigationScope> ManualScopes { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000124D5 File Offset: 0x000106D5
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000124DD File Offset: 0x000106DD
		internal bool IsAdditionalMovementsDirty { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000124E6 File Offset: 0x000106E6
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x000124EE File Offset: 0x000106EE
		internal Dictionary<GamepadNavigationTypes, GamepadNavigationScope> InterScopeMovements { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000124F7 File Offset: 0x000106F7
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x000124FF File Offset: 0x000106FF
		internal GamepadNavigationScope ParentScope { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00012508 File Offset: 0x00010708
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00012510 File Offset: 0x00010710
		internal ReadOnlyCollection<GamepadNavigationScope> ChildScopes { get; private set; }

		// Token: 0x0600049A RID: 1178 RVA: 0x0001251C File Offset: 0x0001071C
		public GamepadNavigationScope()
		{
			this._widgetIndices = new Dictionary<Widget, int>();
			this._navigatableWidgets = new List<Widget>();
			this.NavigatableWidgets = new ReadOnlyCollection<Widget>(this._navigatableWidgets);
			this._invisibleParents = new List<Widget>();
			this.InterScopeMovements = new Dictionary<GamepadNavigationTypes, GamepadNavigationScope>
			{
				{
					GamepadNavigationTypes.Up,
					null
				},
				{
					GamepadNavigationTypes.Right,
					null
				},
				{
					GamepadNavigationTypes.Down,
					null
				},
				{
					GamepadNavigationTypes.Left,
					null
				}
			};
			this.ManualScopeIDs = new Dictionary<GamepadNavigationTypes, string>
			{
				{
					GamepadNavigationTypes.Up,
					null
				},
				{
					GamepadNavigationTypes.Right,
					null
				},
				{
					GamepadNavigationTypes.Down,
					null
				},
				{
					GamepadNavigationTypes.Left,
					null
				}
			};
			this.ManualScopes = new Dictionary<GamepadNavigationTypes, GamepadNavigationScope>
			{
				{
					GamepadNavigationTypes.Up,
					null
				},
				{
					GamepadNavigationTypes.Right,
					null
				},
				{
					GamepadNavigationTypes.Down,
					null
				},
				{
					GamepadNavigationTypes.Left,
					null
				}
			};
			this._navigatableWidgetComparer = new GamepadNavigationScope.WidgetNavigationIndexComparer();
			this.LatestNavigationElementIndex = -1;
			this._childScopes = new List<GamepadNavigationScope>();
			this.ChildScopes = new ReadOnlyCollection<GamepadNavigationScope>(this._childScopes);
			this.IsInitialized = false;
			this.IsEnabled = true;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001262C File Offset: 0x0001082C
		public void AddWidgetAtIndex(Widget widget, int index)
		{
			if (index < this._navigatableWidgets.Count)
			{
				this._navigatableWidgets.Insert(index, widget);
				this._widgetIndices.Add(widget, index);
			}
			else
			{
				this._navigatableWidgets.Add(widget);
				this._widgetIndices.Add(widget, this._navigatableWidgets.Count - 1);
			}
			Action<GamepadNavigationScope> onNavigatableWidgetsChanged = this.OnNavigatableWidgetsChanged;
			if (onNavigatableWidgetsChanged != null)
			{
				onNavigatableWidgetsChanged(this);
			}
			this.SetCursorAreaExtensionsForChild(widget);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000126A1 File Offset: 0x000108A1
		public void AddWidget(Widget widget)
		{
			this._navigatableWidgets.Add(widget);
			Action<GamepadNavigationScope> onNavigatableWidgetsChanged = this.OnNavigatableWidgetsChanged;
			if (onNavigatableWidgetsChanged != null)
			{
				onNavigatableWidgetsChanged(this);
			}
			this.SetCursorAreaExtensionsForChild(widget);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000126C8 File Offset: 0x000108C8
		public void RemoveWidget(Widget widget)
		{
			this._navigatableWidgets.Remove(widget);
			Action<GamepadNavigationScope> onNavigatableWidgetsChanged = this.OnNavigatableWidgetsChanged;
			if (onNavigatableWidgetsChanged == null)
			{
				return;
			}
			onNavigatableWidgetsChanged(this);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000126E8 File Offset: 0x000108E8
		public void SetParentScope(GamepadNavigationScope scope)
		{
			if (this.ParentScope != null)
			{
				this.ParentScope._childScopes.Remove(this);
			}
			GamepadNavigationScope parentScope = this.ParentScope;
			this.ParentScope = scope;
			Action<GamepadNavigationScope, GamepadNavigationScope> onParentScopeChanged = this.OnParentScopeChanged;
			if (onParentScopeChanged != null)
			{
				onParentScopeChanged(parentScope, this.ParentScope);
			}
			if (this.ParentScope != null)
			{
				this.ParentScope._childScopes.Add(this);
				this.ClearMyWidgetsFromParentScope();
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00012754 File Offset: 0x00010954
		internal void SetIsActiveScope(bool isActive)
		{
			this.IsActiveScope = isActive;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001275D File Offset: 0x0001095D
		internal bool IsVisible()
		{
			return this._invisibleParents.Count == 0;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001276D File Offset: 0x0001096D
		internal bool IsAvailable()
		{
			return this.IsEnabled && this._navigatableWidgets.Count > 0 && this.IsVisible();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001278D File Offset: 0x0001098D
		internal void Initialize()
		{
			if (!this.DoNotAutomaticallyFindChildren)
			{
				this.FindNavigatableChildren();
			}
			this.IsInitialized = true;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000127A4 File Offset: 0x000109A4
		internal void RefreshNavigatableChildren()
		{
			if (this.IsInitialized)
			{
				this.FindNavigatableChildren();
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000127B4 File Offset: 0x000109B4
		internal bool HasMovement(GamepadNavigationTypes movement)
		{
			return (this.ScopeMovements & movement) != GamepadNavigationTypes.None || (this.AlternateScopeMovements & movement) > GamepadNavigationTypes.None;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000127CD File Offset: 0x000109CD
		private void FindNavigatableChildren()
		{
			this._navigatableWidgets.Clear();
			if (this.IsParentWidgetAvailableForNavigation())
			{
				this.CollectNavigatableChildrenOfWidget(this.ParentWidget);
			}
			Action<GamepadNavigationScope> onNavigatableWidgetsChanged = this.OnNavigatableWidgetsChanged;
			if (onNavigatableWidgetsChanged == null)
			{
				return;
			}
			onNavigatableWidgetsChanged(this);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00012800 File Offset: 0x00010A00
		private bool IsParentWidgetAvailableForNavigation()
		{
			for (Widget parentWidget = this.ParentWidget; parentWidget != null; parentWidget = parentWidget.ParentWidget)
			{
				if (parentWidget.DoNotAcceptNavigation)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001282C File Offset: 0x00010A2C
		private void CollectNavigatableChildrenOfWidget(Widget widget)
		{
			if (widget.DoNotAcceptNavigation)
			{
				return;
			}
			for (int i = 0; i < this._childScopes.Count; i++)
			{
				if (this._childScopes[i].ParentWidget == widget)
				{
					return;
				}
			}
			if (widget.GamepadNavigationIndex != -1)
			{
				this._navigatableWidgets.Add(widget);
			}
			List<GamepadNavigationScope> list;
			if (!this.DoNotAutoCollectChildScopes && this.ParentWidget != widget && GauntletGamepadNavigationManager.Instance.NavigationScopeParents.TryGetValue(widget, out list))
			{
				for (int j = 0; j < list.Count; j++)
				{
					list[j].SetParentScope(this);
				}
			}
			for (int k = 0; k < widget.Children.Count; k++)
			{
				this.CollectNavigatableChildrenOfWidget(widget.Children[k]);
			}
			this.ClearMyWidgetsFromParentScope();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000128F4 File Offset: 0x00010AF4
		internal GamepadNavigationTypes GetMovementsToReachMyPosition(Vector2 fromPosition)
		{
			Rectangle rectangle = this.GetRectangle();
			GamepadNavigationTypes gamepadNavigationTypes = GamepadNavigationTypes.None;
			if (fromPosition.X > rectangle.X + rectangle.Width)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Left;
			}
			else if (fromPosition.X < rectangle.X)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Right;
			}
			if (fromPosition.Y > rectangle.Y + rectangle.Height)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Up;
			}
			else if (fromPosition.Y < rectangle.Y)
			{
				gamepadNavigationTypes |= GamepadNavigationTypes.Down;
			}
			return gamepadNavigationTypes;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00012967 File Offset: 0x00010B67
		internal bool GetShouldFindScopeByPosition(GamepadNavigationTypes movement)
		{
			return this.ManualScopeIDs[movement] == null && this.ManualScopes[movement] == null;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00012988 File Offset: 0x00010B88
		internal GamepadNavigationTypes GetMovementsInsideScope()
		{
			GamepadNavigationTypes gamepadNavigationTypes = this.ScopeMovements;
			GamepadNavigationTypes gamepadNavigationTypes2 = this.AlternateScopeMovements;
			if (!this.HasCircularMovement || this._navigatableWidgets.Count == 1)
			{
				bool flag = false;
				bool flag2 = false;
				if (this.LatestNavigationElementIndex >= 0 && this.LatestNavigationElementIndex < this._navigatableWidgets.Count)
				{
					for (int i = this.LatestNavigationElementIndex + 1; i < this._navigatableWidgets.Count; i++)
					{
						if (this.IsWidgetVisible(this._navigatableWidgets[i]))
						{
							flag2 = true;
							break;
						}
					}
					int num = this.LatestNavigationElementIndex - 1;
					if (this.HasCircularMovement && num < 0)
					{
						num = this._navigatableWidgets.Count - 1;
					}
					for (int j = num; j >= 0; j--)
					{
						if (this.IsWidgetVisible(this._navigatableWidgets[j]))
						{
							flag = true;
							break;
						}
					}
				}
				if (this.LatestNavigationElementIndex == 0 || !flag)
				{
					gamepadNavigationTypes &= ~GamepadNavigationTypes.Left;
					gamepadNavigationTypes &= ~GamepadNavigationTypes.Up;
				}
				if (this.LatestNavigationElementIndex == this.NavigatableWidgets.Count - 1 || !flag2)
				{
					gamepadNavigationTypes &= ~GamepadNavigationTypes.Right;
					gamepadNavigationTypes &= ~GamepadNavigationTypes.Down;
				}
				if (gamepadNavigationTypes2 != GamepadNavigationTypes.None && this.AlternateMovementStepSize > 0)
				{
					if (this.LatestNavigationElementIndex % this.AlternateMovementStepSize == 0)
					{
						gamepadNavigationTypes &= ~GamepadNavigationTypes.Left;
						gamepadNavigationTypes &= ~GamepadNavigationTypes.Up;
					}
					if (this.LatestNavigationElementIndex % this.AlternateMovementStepSize == this.AlternateMovementStepSize - 1)
					{
						gamepadNavigationTypes &= ~GamepadNavigationTypes.Right;
						gamepadNavigationTypes &= ~GamepadNavigationTypes.Down;
					}
					if (this.LatestNavigationElementIndex - this.AlternateMovementStepSize < 0)
					{
						gamepadNavigationTypes2 &= ~GamepadNavigationTypes.Up;
						gamepadNavigationTypes2 &= ~GamepadNavigationTypes.Left;
					}
					int num2 = this._navigatableWidgets.Count % this.AlternateMovementStepSize;
					if (this._navigatableWidgets.Count > 0 && num2 == 0)
					{
						num2 = this.AlternateMovementStepSize;
					}
					if (this.LatestNavigationElementIndex + num2 > this._navigatableWidgets.Count - 1)
					{
						gamepadNavigationTypes2 &= ~GamepadNavigationTypes.Right;
						gamepadNavigationTypes2 &= ~GamepadNavigationTypes.Down;
					}
				}
			}
			return gamepadNavigationTypes | gamepadNavigationTypes2;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00012B5C File Offset: 0x00010D5C
		internal int FindIndexOfWidget(Widget widget)
		{
			int result;
			if (widget != null && this._navigatableWidgets.Count != 0 && this._widgetIndices.TryGetValue(widget, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00012B8C File Offset: 0x00010D8C
		internal void SortWidgets()
		{
			this._navigatableWidgets.Sort(this._navigatableWidgetComparer);
			this._widgetIndices.Clear();
			for (int i = 0; i < this._navigatableWidgets.Count; i++)
			{
				this._widgetIndices[this._navigatableWidgets[i]] = i;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00012BE3 File Offset: 0x00010DE3
		public void ClearNavigatableWidgets()
		{
			this._navigatableWidgets.Clear();
			this._widgetIndices.Clear();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00012BFC File Offset: 0x00010DFC
		internal Rectangle GetDiscoveryRectangle()
		{
			float customScale = this.ParentWidget.EventManager.Context.CustomScale;
			return new Rectangle(this.DiscoveryAreaOffsetX + this.ParentWidget.GlobalPosition.X - this.ExtendDiscoveryAreaLeft * customScale, this.DiscoveryAreaOffsetY + this.ParentWidget.GlobalPosition.Y - this.ExtendDiscoveryAreaTop * customScale, this.ParentWidget.Size.X + (this.ExtendDiscoveryAreaLeft + this.ExtendDiscoveryAreaRight) * customScale, this.ParentWidget.Size.Y + (this.ExtendDiscoveryAreaTop + this.ExtendDiscoveryAreaBottom) * customScale);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00012CA4 File Offset: 0x00010EA4
		internal Rectangle GetRectangle()
		{
			if (this.ParentWidget == null)
			{
				return new Rectangle(0f, 0f, 1f, 1f);
			}
			return new Rectangle(this.ParentWidget.GlobalPosition.X, this.ParentWidget.GlobalPosition.Y, this.ParentWidget.Size.X, this.ParentWidget.Size.Y);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00012D18 File Offset: 0x00010F18
		internal bool IsWidgetVisible(Widget widget)
		{
			for (Widget widget2 = widget; widget2 != null; widget2 = widget2.ParentWidget)
			{
				if (!widget2.IsVisible)
				{
					return false;
				}
				if (widget2 == this.ParentWidget)
				{
					return this.IsVisible();
				}
			}
			return true;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00012D50 File Offset: 0x00010F50
		internal Widget GetFirstAvailableWidget()
		{
			int num = -1;
			for (int i = 0; i < this._navigatableWidgets.Count; i++)
			{
				if (this.IsWidgetVisible(this._navigatableWidgets[i]))
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				return this._navigatableWidgets[num];
			}
			return null;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012DA0 File Offset: 0x00010FA0
		internal Widget GetLastAvailableWidget()
		{
			int num = -1;
			for (int i = this._navigatableWidgets.Count - 1; i >= 0; i--)
			{
				if (this.IsWidgetVisible(this._navigatableWidgets[i]))
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				return this._navigatableWidgets[num];
			}
			return null;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00012DF1 File Offset: 0x00010FF1
		private int GetApproximatelyClosestWidgetIndexToPosition(Vector2 position, out float distance, GamepadNavigationTypes movement = GamepadNavigationTypes.None, bool angleCheck = false)
		{
			if (this._navigatableWidgets.Count <= 0)
			{
				distance = -1f;
				return -1;
			}
			if (this.AlternateMovementStepSize > 0)
			{
				return this.GetClosesWidgetIndexForWithAlternateMovement(position, out distance, movement, angleCheck);
			}
			return this.GetClosesWidgetIndexForRegular(position, out distance, movement, angleCheck);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00012E2C File Offset: 0x0001102C
		internal Widget GetApproximatelyClosestWidgetToPosition(Vector2 position, GamepadNavigationTypes movement = GamepadNavigationTypes.None, bool angleCheck = false)
		{
			float num;
			return this.GetApproximatelyClosestWidgetToPosition(position, out num, movement, angleCheck);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00012E44 File Offset: 0x00011044
		internal Widget GetApproximatelyClosestWidgetToPosition(Vector2 position, out float distance, GamepadNavigationTypes movement = GamepadNavigationTypes.None, bool angleCheck = false)
		{
			int approximatelyClosestWidgetIndexToPosition = this.GetApproximatelyClosestWidgetIndexToPosition(position, out distance, movement, angleCheck);
			if (approximatelyClosestWidgetIndexToPosition != -1)
			{
				return this._navigatableWidgets[approximatelyClosestWidgetIndexToPosition];
			}
			return null;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00012E70 File Offset: 0x00011070
		private void OnParentVisibilityChanged(Widget parent)
		{
			bool flag = this.IsVisible();
			if (!parent.IsVisible)
			{
				this._invisibleParents.Add(parent);
			}
			else
			{
				this._invisibleParents.Remove(parent);
			}
			bool flag2 = this.IsVisible();
			if (flag != flag2)
			{
				Action<GamepadNavigationScope, bool> onVisibilityChanged = this.OnVisibilityChanged;
				if (onVisibilityChanged == null)
				{
					return;
				}
				onVisibilityChanged(this, flag2);
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00012EC4 File Offset: 0x000110C4
		private void ClearMyWidgetsFromParentScope()
		{
			if (this.ParentScope != null)
			{
				for (int i = 0; i < this._navigatableWidgets.Count; i++)
				{
					this.ParentScope.RemoveWidget(this._navigatableWidgets[i]);
				}
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012F08 File Offset: 0x00011108
		private Vector2 GetRelativePositionRatio(Vector2 position)
		{
			float toValue = 0f;
			float fromValue = 0f;
			float toValue2 = 0f;
			float fromValue2 = 0f;
			for (int i = 0; i < this._navigatableWidgets.Count; i++)
			{
				if (this.IsWidgetVisible(this._navigatableWidgets[i]))
				{
					fromValue = this._navigatableWidgets[i].GlobalPosition.Y;
					fromValue2 = this._navigatableWidgets[i].GlobalPosition.X;
					break;
				}
			}
			for (int j = this._navigatableWidgets.Count - 1; j >= 0; j--)
			{
				if (this.IsWidgetVisible(this._navigatableWidgets[j]))
				{
					toValue = this._navigatableWidgets[j].GlobalPosition.Y + this._navigatableWidgets[j].Size.Y;
					toValue2 = this._navigatableWidgets[j].GlobalPosition.X + this._navigatableWidgets[j].Size.X;
					break;
				}
			}
			float x = Mathf.Clamp(GamepadNavigationScope.InverseLerp(fromValue2, toValue2, position.X), 0f, 1f);
			float y = Mathf.Clamp(GamepadNavigationScope.InverseLerp(fromValue, toValue, position.Y), 0f, 1f);
			return new Vector2(x, y);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00013068 File Offset: 0x00011268
		private bool IsPositionAvailableForMovement(Vector2 fromPos, Vector2 toPos, GamepadNavigationTypes movement)
		{
			if (movement == GamepadNavigationTypes.Right)
			{
				return fromPos.X <= toPos.X;
			}
			if (movement == GamepadNavigationTypes.Left)
			{
				return fromPos.X >= toPos.X;
			}
			if (movement == GamepadNavigationTypes.Up)
			{
				return fromPos.Y >= toPos.Y;
			}
			return movement != GamepadNavigationTypes.Down || fromPos.Y <= toPos.Y;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000130D0 File Offset: 0x000112D0
		private int GetClosesWidgetIndexForWithAlternateMovement(Vector2 fromPos, out float distance, GamepadNavigationTypes movement = GamepadNavigationTypes.None, bool angleCheck = false)
		{
			distance = -1f;
			List<int> list = new List<int>();
			Vector2 relativePositionRatio = this.GetRelativePositionRatio(fromPos);
			float num = float.MaxValue;
			int result = -1;
			Rectangle rectangle = this.GetRectangle();
			if (!rectangle.IsPointInside(fromPos))
			{
				List<int> list2 = new List<int>();
				if (fromPos.X < rectangle.X)
				{
					for (int i = 0; i < this._navigatableWidgets.Count; i += this.AlternateMovementStepSize)
					{
						list2.Add(i);
					}
				}
				else if (fromPos.X > rectangle.X2)
				{
					for (int j = MathF.Min(this.AlternateMovementStepSize - 1, this._navigatableWidgets.Count - 1); j < this._navigatableWidgets.Count; j += this.AlternateMovementStepSize)
					{
						list2.Add(j);
					}
				}
				if (list2.Count > 0)
				{
					int[] targetIndicesFromListByRatio = GamepadNavigationScope.GetTargetIndicesFromListByRatio(relativePositionRatio.Y, list2);
					for (int k = 0; k < targetIndicesFromListByRatio.Length; k++)
					{
						list.Add(targetIndicesFromListByRatio[k]);
					}
				}
				if (fromPos.Y < rectangle.Y)
				{
					int endIndex = Mathf.Clamp(this.AlternateMovementStepSize - 1, 0, this._navigatableWidgets.Count - 1);
					int[] targetIndicesByRatio = GamepadNavigationScope.GetTargetIndicesByRatio(relativePositionRatio.X, 0, endIndex, 5);
					for (int l = 0; l < targetIndicesByRatio.Length; l++)
					{
						list.Add(targetIndicesByRatio[l]);
					}
				}
				else if (fromPos.Y > rectangle.Y2)
				{
					int num2 = this._navigatableWidgets.Count % this.AlternateMovementStepSize;
					if (this._navigatableWidgets.Count > 0 && num2 == 0)
					{
						num2 = this.AlternateMovementStepSize;
					}
					int startIndex = Mathf.Clamp(this._navigatableWidgets.Count - num2, 0, this._navigatableWidgets.Count - 1);
					int[] targetIndicesByRatio2 = GamepadNavigationScope.GetTargetIndicesByRatio(relativePositionRatio.X, startIndex, this._navigatableWidgets.Count - 1, 5);
					for (int m = 0; m < targetIndicesByRatio2.Length; m++)
					{
						list.Add(targetIndicesByRatio2[m]);
					}
				}
				for (int n = 0; n < list.Count; n++)
				{
					int num3 = list[n];
					Vector2 toPos;
					float distanceToClosestWidgetEdge = GamepadNavigationHelper.GetDistanceToClosestWidgetEdge(this._navigatableWidgets[num3], fromPos, movement, out toPos);
					if (distanceToClosestWidgetEdge < num && (!angleCheck || this.IsPositionAvailableForMovement(fromPos, toPos, movement)))
					{
						num = distanceToClosestWidgetEdge;
						distance = num;
						result = num3;
					}
				}
			}
			else
			{
				result = this.GetClosesWidgetIndexForRegular(fromPos, out distance, GamepadNavigationTypes.None, false);
			}
			return result;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00013338 File Offset: 0x00011538
		private int GetClosestWidgetIndexForRegularInefficient(Vector2 fromPos, out float distance, GamepadNavigationTypes movement = GamepadNavigationTypes.None, bool angleCheck = false)
		{
			distance = -1f;
			int result = -1;
			float num = float.MaxValue;
			for (int i = 0; i < this._navigatableWidgets.Count; i++)
			{
				Vector2 toPos;
				float distanceToClosestWidgetEdge = GamepadNavigationHelper.GetDistanceToClosestWidgetEdge(this._navigatableWidgets[i], fromPos, movement, out toPos);
				if (distanceToClosestWidgetEdge < num && this.IsWidgetVisible(this._navigatableWidgets[i]) && (!angleCheck || this.IsPositionAvailableForMovement(fromPos, toPos, movement)))
				{
					num = distanceToClosestWidgetEdge;
					distance = num;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000133B0 File Offset: 0x000115B0
		private int GetClosesWidgetIndexForRegular(Vector2 fromPos, out float distance, GamepadNavigationTypes movement = GamepadNavigationTypes.None, bool angleCheck = false)
		{
			distance = -1f;
			List<int> list = new List<int>();
			Vector2 relativePositionRatio = this.GetRelativePositionRatio(fromPos);
			int[] targetIndicesByRatio = GamepadNavigationScope.GetTargetIndicesByRatio(relativePositionRatio.X, 0, this._navigatableWidgets.Count - 1, 5);
			int[] targetIndicesByRatio2 = GamepadNavigationScope.GetTargetIndicesByRatio(relativePositionRatio.Y, 0, this._navigatableWidgets.Count - 1, 5);
			for (int i = 0; i < targetIndicesByRatio.Length; i++)
			{
				if (!list.Contains(targetIndicesByRatio[i]))
				{
					list.Add(targetIndicesByRatio[i]);
				}
			}
			for (int j = 0; j < targetIndicesByRatio2.Length; j++)
			{
				if (!list.Contains(targetIndicesByRatio2[j]))
				{
					list.Add(targetIndicesByRatio2[j]);
				}
			}
			float num = float.MaxValue;
			int result = -1;
			int num2 = 0;
			for (int k = 0; k < list.Count; k++)
			{
				int num3 = list[k];
				if (num3 != -1 && this.IsWidgetVisible(this._navigatableWidgets[num3]))
				{
					num2++;
					Vector2 toPos;
					float distanceToClosestWidgetEdge = GamepadNavigationHelper.GetDistanceToClosestWidgetEdge(this._navigatableWidgets[num3], fromPos, movement, out toPos);
					if (distanceToClosestWidgetEdge < num && (!angleCheck || this.IsPositionAvailableForMovement(fromPos, toPos, movement)))
					{
						num = distanceToClosestWidgetEdge;
						distance = num;
						result = num3;
					}
				}
			}
			if (num2 == 0)
			{
				return this.GetClosestWidgetIndexForRegularInefficient(fromPos, out distance, GamepadNavigationTypes.None, false);
			}
			return result;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000134E9 File Offset: 0x000116E9
		private static float InverseLerp(float fromValue, float toValue, float value)
		{
			if (fromValue == toValue)
			{
				return 0f;
			}
			return (value - fromValue) / (toValue - fromValue);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000134FC File Offset: 0x000116FC
		private static int[] GetTargetIndicesFromListByRatio(float ratio, List<int> lookupIndices)
		{
			int num = MathF.Round((float)lookupIndices.Count * ratio);
			return new int[]
			{
				lookupIndices[Mathf.Clamp(num - 2, 0, lookupIndices.Count - 1)],
				lookupIndices[Mathf.Clamp(num - 1, 0, lookupIndices.Count - 1)],
				lookupIndices[Mathf.Clamp(num, 0, lookupIndices.Count - 1)],
				lookupIndices[Mathf.Clamp(num + 1, 0, lookupIndices.Count - 1)],
				lookupIndices[Mathf.Clamp(num + 2, 0, lookupIndices.Count - 1)]
			};
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000135A0 File Offset: 0x000117A0
		private static int[] GetTargetIndicesByRatio(float ratio, int startIndex, int endIndex, int arraySize = 5)
		{
			int num = MathF.Round((float)startIndex + (float)(endIndex - startIndex) * ratio);
			int[] array = new int[arraySize];
			int num2 = MathF.Floor((float)arraySize / 2f);
			for (int i = 0; i < arraySize; i++)
			{
				int num3 = -num2 + i;
				array[i] = Mathf.Clamp(num - num3, 0, endIndex);
			}
			return array;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000135F4 File Offset: 0x000117F4
		private void SetCursorAreaExtensionsForChild(Widget child)
		{
			if (this.ExtendChildrenCursorAreaLeft != 0f)
			{
				child.ExtendCursorAreaLeft = this.ExtendChildrenCursorAreaLeft;
			}
			if (this.ExtendChildrenCursorAreaRight != 0f)
			{
				child.ExtendCursorAreaRight = this.ExtendChildrenCursorAreaRight;
			}
			if (this.ExtendChildrenCursorAreaTop != 0f)
			{
				child.ExtendCursorAreaTop = this.ExtendChildrenCursorAreaTop;
			}
			if (this.ExtendChildrenCursorAreaBottom != 0f)
			{
				child.ExtendCursorAreaBottom = this.ExtendChildrenCursorAreaBottom;
			}
		}

		// Token: 0x0400021A RID: 538
		private List<Widget> _navigatableWidgets;

		// Token: 0x0400021C RID: 540
		private Dictionary<Widget, int> _widgetIndices;

		// Token: 0x0400021D RID: 541
		private Widget _parentWidget;

		// Token: 0x0400022C RID: 556
		private float _extendChildrenCursorAreaLeft;

		// Token: 0x0400022D RID: 557
		private float _extendChildrenCursorAreaRight;

		// Token: 0x0400022E RID: 558
		private float _extendChildrenCursorAreaTop;

		// Token: 0x0400022F RID: 559
		private float _extendChildrenCursorAreaBottom;

		// Token: 0x04000232 RID: 562
		private bool _isEnabled;

		// Token: 0x04000233 RID: 563
		private bool _isDisabled;

		// Token: 0x0400023A RID: 570
		private GamepadNavigationScope.WidgetNavigationIndexComparer _navigatableWidgetComparer;

		// Token: 0x0400023B RID: 571
		private List<Widget> _invisibleParents;

		// Token: 0x0400023D RID: 573
		private List<GamepadNavigationScope> _childScopes;

		// Token: 0x0400023F RID: 575
		internal Action<GamepadNavigationScope> OnNavigatableWidgetsChanged;

		// Token: 0x04000240 RID: 576
		internal Action<GamepadNavigationScope, bool> OnVisibilityChanged;

		// Token: 0x04000241 RID: 577
		internal Action<GamepadNavigationScope, GamepadNavigationScope> OnParentScopeChanged;

		// Token: 0x02000086 RID: 134
		private class WidgetNavigationIndexComparer : IComparer<Widget>
		{
			// Token: 0x060008FF RID: 2303 RVA: 0x00023C84 File Offset: 0x00021E84
			public int Compare(Widget x, Widget y)
			{
				return x.GamepadNavigationIndex.CompareTo(y.GamepadNavigationIndex);
			}
		}
	}
}
