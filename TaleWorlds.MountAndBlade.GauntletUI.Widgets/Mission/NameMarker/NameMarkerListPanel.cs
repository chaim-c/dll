using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.NameMarker
{
	// Token: 0x020000EB RID: 235
	public class NameMarkerListPanel : ListPanel
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002190D File Offset: 0x0001FB0D
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00021915 File Offset: 0x0001FB15
		public float FarAlphaTarget { get; set; } = 0.2f;

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002191E File Offset: 0x0001FB1E
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00021926 File Offset: 0x0001FB26
		public float FarDistanceCutoff { get; set; } = 50f;

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0002192F File Offset: 0x0001FB2F
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00021937 File Offset: 0x0001FB37
		public float CloseDistanceCutoff { get; set; } = 25f;

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00021940 File Offset: 0x0001FB40
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00021948 File Offset: 0x0001FB48
		public bool HasTypeMarker { get; private set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00021951 File Offset: 0x0001FB51
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x00021959 File Offset: 0x0001FB59
		public MarkerRect Rect { get; private set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00021962 File Offset: 0x0001FB62
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x0002196A File Offset: 0x0001FB6A
		public bool IsInScreenBoundaries { get; private set; }

		// Token: 0x06000C3C RID: 3132 RVA: 0x00021974 File Offset: 0x0001FB74
		public NameMarkerListPanel(UIContext context) : base(context)
		{
			this._parentScreenWidget = base.EventManager.Root.GetChild(0).GetChild(0);
			this.Rect = new MarkerRect();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000219E8 File Offset: 0x0001FBE8
		public void Update(float dt)
		{
			this._transitionDT = MathF.Clamp(dt * 12f, 0f, 1f);
			this._targetAlpha = (this.IsMarkerEnabled ? this.GetDistanceRelatedAlphaTarget(this.Distance) : 0f);
			this.ApplyActionForThisAndAllChildren(new Action<Widget>(this.UpdateAlpha));
			TextWidget nameTextWidget = this.NameTextWidget;
			if ((nameTextWidget != null && nameTextWidget.IsVisible) || this.TypeVisualWidget.IsVisible)
			{
				base.ScaledPositionYOffset = this.Position.y - base.Size.Y / 2f;
				base.ScaledPositionXOffset = this.Position.x - base.Size.X / 2f;
			}
			this.UpdateRectangle();
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00021AB4 File Offset: 0x0001FCB4
		private void UpdateAlpha(Widget item)
		{
			if ((item == this.NameTextWidget || item == this.DistanceTextWidget || item == this.DistanceIconWidget) && this.HasTypeMarker && !this.IsFocused)
			{
				return;
			}
			float alphaFactor = this.LocalLerp(item.AlphaFactor, this._targetAlpha, this._transitionDT);
			item.SetAlpha(alphaFactor);
			item.IsVisible = ((double)item.AlphaFactor > 0.05);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00021B28 File Offset: 0x0001FD28
		public void UpdateRectangle()
		{
			this.Rect.Reset();
			this.Rect.UpdatePoints(base.ScaledPositionXOffset, base.ScaledPositionXOffset + base.Size.X, base.ScaledPositionYOffset, base.ScaledPositionYOffset + base.Size.Y);
			this.IsInScreenBoundaries = (this.Rect.Left > -50f && this.Rect.Right < base.EventManager.PageSize.X + 50f && this.Rect.Top > -50f && this.Rect.Bottom < base.EventManager.PageSize.Y + 50f);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00021BF0 File Offset: 0x0001FDF0
		private float GetDistanceRelatedAlphaTarget(int distance)
		{
			if (this.IsFocused)
			{
				return 1f;
			}
			if ((float)distance > this.FarDistanceCutoff)
			{
				return this.FarAlphaTarget;
			}
			if ((float)distance <= this.FarDistanceCutoff && (float)distance >= this.CloseDistanceCutoff)
			{
				float amount = (float)Math.Pow((double)(((float)distance - this.CloseDistanceCutoff) / (this.FarDistanceCutoff - this.CloseDistanceCutoff)), 0.3333333333333333);
				return MathF.Clamp(MathF.Lerp(1f, this.FarAlphaTarget, amount, 1E-05f), this.FarAlphaTarget, 1f);
			}
			return 1f;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00021C84 File Offset: 0x0001FE84
		private float LocalLerp(float start, float end, float delta)
		{
			if (Math.Abs(start - end) > 1E-45f)
			{
				return (end - start) * delta + start;
			}
			return end;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00021CA0 File Offset: 0x0001FEA0
		private void OnStateChanged()
		{
			TextWidget nameTextWidget = this.NameTextWidget;
			if (nameTextWidget != null)
			{
				nameTextWidget.SetState(this.NameType);
			}
			BrushWidget typeVisualWidget = this.TypeVisualWidget;
			if (typeVisualWidget != null)
			{
				typeVisualWidget.SetState(this.IconType);
			}
			this.HasTypeMarker = (this.IconType != string.Empty);
			if (this.HasTypeMarker && this.IsFocused)
			{
				TextWidget nameTextWidget2 = this.NameTextWidget;
				if (nameTextWidget2 != null)
				{
					nameTextWidget2.SetAlpha(1f);
				}
				TextWidget distanceTextWidget = this.DistanceTextWidget;
				if (distanceTextWidget != null)
				{
					distanceTextWidget.SetAlpha(1f);
				}
				BrushWidget distanceIconWidget = this.DistanceIconWidget;
				if (distanceIconWidget != null)
				{
					distanceIconWidget.SetAlpha(1f);
				}
			}
			else if (this.HasTypeMarker && !this.IsFocused)
			{
				TextWidget nameTextWidget3 = this.NameTextWidget;
				if (nameTextWidget3 != null)
				{
					nameTextWidget3.SetAlpha(0f);
				}
				TextWidget distanceTextWidget2 = this.DistanceTextWidget;
				if (distanceTextWidget2 != null)
				{
					distanceTextWidget2.SetAlpha(0f);
				}
				BrushWidget distanceIconWidget2 = this.DistanceIconWidget;
				if (distanceIconWidget2 != null)
				{
					distanceIconWidget2.SetAlpha(0f);
				}
			}
			if (this.IsEnemy)
			{
				this.TypeVisualWidget.Brush.GlobalColor = this.EnemyColor;
			}
			else if (this.IsFriendly)
			{
				this.TypeVisualWidget.Brush.GlobalColor = this.FriendlyColor;
			}
			else if (this.HasMainQuest)
			{
				this.TypeVisualWidget.Brush.GlobalColor = this.MainQuestNotificationColor;
			}
			else if (this.HasIssue)
			{
				this.TypeVisualWidget.Brush.GlobalColor = this.IssueNotificationColor;
			}
			BrushWidget typeVisualWidget2 = this.TypeVisualWidget;
			Sprite sprite;
			if (typeVisualWidget2 == null)
			{
				sprite = null;
			}
			else
			{
				Style style = typeVisualWidget2.Brush.GetStyle(this.IconType);
				if (style == null)
				{
					sprite = null;
				}
				else
				{
					StyleLayer layer = style.GetLayer(0);
					sprite = ((layer != null) ? layer.Sprite : null);
				}
			}
			Sprite sprite2 = sprite;
			if (sprite2 != null)
			{
				base.SuggestedWidth = base.SuggestedHeight / (float)sprite2.Height * (float)sprite2.Width;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00021E6B File Offset: 0x0002006B
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00021E73 File Offset: 0x00020073
		[DataSourceProperty]
		public TextWidget NameTextWidget
		{
			get
			{
				return this._nameTextWidget;
			}
			set
			{
				if (this._nameTextWidget != value)
				{
					this._nameTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "NameTextWidget");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00021E97 File Offset: 0x00020097
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x00021E9F File Offset: 0x0002009F
		[DataSourceProperty]
		public BrushWidget TypeVisualWidget
		{
			get
			{
				return this._typeVisualWidget;
			}
			set
			{
				if (this._typeVisualWidget != value)
				{
					this._typeVisualWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "TypeVisualWidget");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x00021EC3 File Offset: 0x000200C3
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x00021ECB File Offset: 0x000200CB
		[DataSourceProperty]
		public BrushWidget DistanceIconWidget
		{
			get
			{
				return this._distanceIconWidget;
			}
			set
			{
				if (this._distanceIconWidget != value)
				{
					this._distanceIconWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "DistanceIconWidget");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x00021EEF File Offset: 0x000200EF
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x00021EF7 File Offset: 0x000200F7
		[DataSourceProperty]
		public TextWidget DistanceTextWidget
		{
			get
			{
				return this._distanceTextWidget;
			}
			set
			{
				if (this._distanceTextWidget != value)
				{
					this._distanceTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "DistanceTextWidget");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00021F1B File Offset: 0x0002011B
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x00021F23 File Offset: 0x00020123
		[DataSourceProperty]
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00021F46 File Offset: 0x00020146
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00021F4E File Offset: 0x0002014E
		[Editor(false)]
		public Color IssueNotificationColor
		{
			get
			{
				return this._issueNotificationColor;
			}
			set
			{
				if (value != this._issueNotificationColor)
				{
					this._issueNotificationColor = value;
					base.OnPropertyChanged(value, "IssueNotificationColor");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00021F77 File Offset: 0x00020177
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00021F7F File Offset: 0x0002017F
		[Editor(false)]
		public Color MainQuestNotificationColor
		{
			get
			{
				return this._mainQuestNotificationColor;
			}
			set
			{
				if (value != this._mainQuestNotificationColor)
				{
					this._mainQuestNotificationColor = value;
					base.OnPropertyChanged(value, "MainQuestNotificationColor");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00021FA8 File Offset: 0x000201A8
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x00021FB0 File Offset: 0x000201B0
		[Editor(false)]
		public Color EnemyColor
		{
			get
			{
				return this._enemyColor;
			}
			set
			{
				if (value != this._enemyColor)
				{
					this._enemyColor = value;
					base.OnPropertyChanged(value, "EnemyColor");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00021FD9 File Offset: 0x000201D9
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00021FE1 File Offset: 0x000201E1
		[Editor(false)]
		public Color FriendlyColor
		{
			get
			{
				return this._friendlyColor;
			}
			set
			{
				if (value != this._friendlyColor)
				{
					this._friendlyColor = value;
					base.OnPropertyChanged(value, "FriendlyColor");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0002200A File Offset: 0x0002020A
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00022012 File Offset: 0x00020212
		[Editor(false)]
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
					base.OnPropertyChanged<string>(value, "IconType");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002203B File Offset: 0x0002023B
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x00022043 File Offset: 0x00020243
		[Editor(false)]
		public string NameType
		{
			get
			{
				return this._nameType;
			}
			set
			{
				if (value != this._nameType)
				{
					this._nameType = value;
					base.OnPropertyChanged<string>(value, "NameType");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002206C File Offset: 0x0002026C
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00022074 File Offset: 0x00020274
		[DataSourceProperty]
		public int Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (this._distance != value)
				{
					this._distance = value;
					base.OnPropertyChanged(value, "Distance");
				}
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00022092 File Offset: 0x00020292
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x0002209A File Offset: 0x0002029A
		[DataSourceProperty]
		public bool IsMarkerEnabled
		{
			get
			{
				return this._isMarkerEnabled;
			}
			set
			{
				if (this._isMarkerEnabled != value)
				{
					this._isMarkerEnabled = value;
					base.OnPropertyChanged(value, "IsMarkerEnabled");
				}
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x000220B8 File Offset: 0x000202B8
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x000220C0 File Offset: 0x000202C0
		[DataSourceProperty]
		public bool HasIssue
		{
			get
			{
				return this._hasIssue;
			}
			set
			{
				if (this._hasIssue != value)
				{
					this._hasIssue = value;
					base.OnPropertyChanged(value, "HasIssue");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x000220E4 File Offset: 0x000202E4
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x000220EC File Offset: 0x000202EC
		[DataSourceProperty]
		public bool HasMainQuest
		{
			get
			{
				return this._hasMainQuest;
			}
			set
			{
				if (this._hasMainQuest != value)
				{
					this._hasMainQuest = value;
					base.OnPropertyChanged(value, "HasMainQuest");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00022110 File Offset: 0x00020310
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x00022118 File Offset: 0x00020318
		[DataSourceProperty]
		public bool IsEnemy
		{
			get
			{
				return this._isEnemy;
			}
			set
			{
				if (this._isEnemy != value)
				{
					this._isEnemy = value;
					base.OnPropertyChanged(value, "IsEnemy");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0002213C File Offset: 0x0002033C
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00022144 File Offset: 0x00020344
		[DataSourceProperty]
		public bool IsFriendly
		{
			get
			{
				return this._isFriendly;
			}
			set
			{
				if (this._isFriendly != value)
				{
					this._isFriendly = value;
					base.OnPropertyChanged(value, "IsFriendly");
					this.OnStateChanged();
				}
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00022168 File Offset: 0x00020368
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x00022170 File Offset: 0x00020370
		[Editor(false)]
		public new bool IsFocused
		{
			get
			{
				return this._isFocused;
			}
			set
			{
				if (value != this._isFocused)
				{
					this._isFocused = value;
					base.OnPropertyChanged(value, "IsFocused");
					if (!value && this.IsMarkerEnabled)
					{
						TextWidget nameTextWidget = this.NameTextWidget;
						if (nameTextWidget != null)
						{
							nameTextWidget.SetAlpha(0f);
						}
						TextWidget distanceTextWidget = this.DistanceTextWidget;
						if (distanceTextWidget != null)
						{
							distanceTextWidget.SetAlpha(0f);
						}
						BrushWidget distanceIconWidget = this.DistanceIconWidget;
						if (distanceIconWidget != null)
						{
							distanceIconWidget.SetAlpha(0f);
						}
					}
					else if (value && this.IsMarkerEnabled)
					{
						TextWidget nameTextWidget2 = this.NameTextWidget;
						if (nameTextWidget2 != null)
						{
							nameTextWidget2.SetAlpha(1f);
						}
						TextWidget distanceTextWidget2 = this.DistanceTextWidget;
						if (distanceTextWidget2 != null)
						{
							distanceTextWidget2.SetAlpha(1f);
						}
						BrushWidget distanceIconWidget2 = this.DistanceIconWidget;
						if (distanceIconWidget2 != null)
						{
							distanceIconWidget2.SetAlpha(1f);
						}
					}
					base.RenderLate = value;
				}
			}
		}

		// Token: 0x04000591 RID: 1425
		private Widget _parentScreenWidget;

		// Token: 0x04000595 RID: 1429
		private const float BoundaryOffset = 50f;

		// Token: 0x04000596 RID: 1430
		private float _transitionDT;

		// Token: 0x04000597 RID: 1431
		private float _targetAlpha;

		// Token: 0x04000598 RID: 1432
		private string _iconType = string.Empty;

		// Token: 0x04000599 RID: 1433
		private string _nameType = string.Empty;

		// Token: 0x0400059A RID: 1434
		private int _distance;

		// Token: 0x0400059B RID: 1435
		private TextWidget _nameTextWidget;

		// Token: 0x0400059C RID: 1436
		private BrushWidget _typeVisualWidget;

		// Token: 0x0400059D RID: 1437
		private BrushWidget _distanceIconWidget;

		// Token: 0x0400059E RID: 1438
		private TextWidget _distanceTextWidget;

		// Token: 0x0400059F RID: 1439
		private Vec2 _position;

		// Token: 0x040005A0 RID: 1440
		private Color _issueNotificationColor;

		// Token: 0x040005A1 RID: 1441
		private Color _mainQuestNotificationColor;

		// Token: 0x040005A2 RID: 1442
		private Color _enemyColor;

		// Token: 0x040005A3 RID: 1443
		private Color _friendlyColor;

		// Token: 0x040005A4 RID: 1444
		private bool _isMarkerEnabled;

		// Token: 0x040005A5 RID: 1445
		private bool _hasIssue;

		// Token: 0x040005A6 RID: 1446
		private bool _hasMainQuest;

		// Token: 0x040005A7 RID: 1447
		private bool _isEnemy;

		// Token: 0x040005A8 RID: 1448
		private bool _isFriendly;

		// Token: 0x040005A9 RID: 1449
		private bool _isFocused;
	}
}
