using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.NameMarker
{
	// Token: 0x020000EA RID: 234
	public class DuelTargetMarkerListPanel : ListPanel
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x000213A6 File Offset: 0x0001F5A6
		public DuelTargetMarkerListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000213B0 File Offset: 0x0001F5B0
		protected override void OnLateUpdate(float dt)
		{
			if (!this.IsAvailable)
			{
				base.IsVisible = false;
				return;
			}
			float x = base.Context.EventManager.PageSize.X;
			float y = base.Context.EventManager.PageSize.Y;
			Vec2 vec = this.Position;
			if (this.WSign > 0 && vec.x - base.Size.X / 2f > 0f && vec.x + base.Size.X / 2f < base.Context.EventManager.PageSize.X && vec.y > 0f && vec.y + base.Size.Y < base.Context.EventManager.PageSize.Y)
			{
				base.ScaledPositionXOffset = vec.x - base.Size.X / 2f;
				base.ScaledPositionYOffset = vec.y - base.Size.Y - 20f;
				this._actionText.ScaledPositionXOffset = base.ScaledPositionXOffset;
				this._actionText.ScaledPositionYOffset = base.ScaledPositionYOffset + base.Size.Y;
				base.IsVisible = true;
				return;
			}
			if (this.IsTracked)
			{
				Vec2 vec2 = new Vec2(base.Context.EventManager.PageSize.X / 2f, base.Context.EventManager.PageSize.Y / 2f);
				vec -= vec2;
				if (this.WSign < 0)
				{
					vec *= -1f;
				}
				float radian = Mathf.Atan2(vec.y, vec.x) - 1.5707964f;
				float num = Mathf.Cos(radian);
				float num2 = Mathf.Sin(radian);
				vec = vec2 + new Vec2(num2 * 150f, num * 150f);
				float num3 = num / num2;
				Vec2 vec3 = vec2 * 1f;
				vec = ((num > 0f) ? new Vec2(-vec3.y / num3, vec2.y) : new Vec2(vec3.y / num3, -vec2.y));
				if (vec.x > vec3.x)
				{
					vec = new Vec2(vec3.x, -vec3.x * num3);
				}
				else if (vec.x < -vec3.x)
				{
					vec = new Vec2(-vec3.x, vec3.x * num3);
				}
				vec += vec2;
				base.ScaledPositionXOffset = Mathf.Clamp(vec.x - base.Size.X / 2f, 0f, x - base.Size.X);
				base.ScaledPositionYOffset = Mathf.Clamp(vec.y - base.Size.Y, 0f, y - base.Size.Y);
				base.IsVisible = true;
				return;
			}
			base.IsVisible = false;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000216CC File Offset: 0x0001F8CC
		private void UpdateChildrenFocusStates()
		{
			string state = this.HasTargetSentDuelRequest ? "Tracked" : ((this.HasPlayerSentDuelRequest || this.IsAgentFocused) ? "Focused" : "Default");
			this.Background.SetState(state);
			this.Border.SetState(state);
			BrushWidget troopClassBorder = this.TroopClassBorder;
			if (troopClassBorder == null)
			{
				return;
			}
			troopClassBorder.SetState(state);
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0002172E File Offset: 0x0001F92E
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x00021736 File Offset: 0x0001F936
		[Editor(false)]
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (value != this._position)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00021759 File Offset: 0x0001F959
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00021761 File Offset: 0x0001F961
		[Editor(false)]
		public bool IsAgentInScreenBoundaries
		{
			get
			{
				return this._isAgentInScreenBoundaries;
			}
			set
			{
				if (value != this._isAgentInScreenBoundaries)
				{
					this._isAgentInScreenBoundaries = value;
					base.OnPropertyChanged(value, "IsAgentInScreenBoundaries");
				}
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002177F File Offset: 0x0001F97F
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00021787 File Offset: 0x0001F987
		[Editor(false)]
		public bool IsAvailable
		{
			get
			{
				return this._isAvailable;
			}
			set
			{
				if (value != this._isAvailable)
				{
					this._isAvailable = value;
					base.OnPropertyChanged(value, "IsAvailable");
				}
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000217A5 File Offset: 0x0001F9A5
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x000217AD File Offset: 0x0001F9AD
		[Editor(false)]
		public bool IsTracked
		{
			get
			{
				return this._isTracked;
			}
			set
			{
				if (value != this._isTracked)
				{
					this._isTracked = value;
					base.OnPropertyChanged(value, "IsTracked");
				}
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000217CB File Offset: 0x0001F9CB
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x000217D3 File Offset: 0x0001F9D3
		[Editor(false)]
		public bool IsAgentFocused
		{
			get
			{
				return this._isAgentFocused;
			}
			set
			{
				if (value != this._isAgentFocused)
				{
					this._isAgentFocused = value;
					base.OnPropertyChanged(value, "IsAgentFocused");
					this.UpdateChildrenFocusStates();
				}
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x000217F7 File Offset: 0x0001F9F7
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x000217FF File Offset: 0x0001F9FF
		[Editor(false)]
		public bool HasTargetSentDuelRequest
		{
			get
			{
				return this._hasTargetSentDuelRequest;
			}
			set
			{
				if (value != this._hasTargetSentDuelRequest)
				{
					this._hasTargetSentDuelRequest = value;
					base.OnPropertyChanged(value, "HasTargetSentDuelRequest");
					this.UpdateChildrenFocusStates();
				}
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00021823 File Offset: 0x0001FA23
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x0002182B File Offset: 0x0001FA2B
		[Editor(false)]
		public bool HasPlayerSentDuelRequest
		{
			get
			{
				return this._hasPlayerSentDuelRequest;
			}
			set
			{
				if (value != this._hasPlayerSentDuelRequest)
				{
					this._hasPlayerSentDuelRequest = value;
					base.OnPropertyChanged(value, "HasPlayerSentDuelRequest");
					this.UpdateChildrenFocusStates();
				}
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002184F File Offset: 0x0001FA4F
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00021857 File Offset: 0x0001FA57
		[Editor(false)]
		public int WSign
		{
			get
			{
				return this._wSign;
			}
			set
			{
				if (this._wSign != value)
				{
					this._wSign = value;
					base.OnPropertyChanged(value, "WSign");
				}
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00021875 File Offset: 0x0001FA75
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0002187D File Offset: 0x0001FA7D
		[Editor(false)]
		public RichTextWidget ActionText
		{
			get
			{
				return this._actionText;
			}
			set
			{
				if (value != this._actionText)
				{
					this._actionText = value;
					base.OnPropertyChanged<RichTextWidget>(value, "ActionText");
				}
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0002189B File Offset: 0x0001FA9B
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000218A3 File Offset: 0x0001FAA3
		[Editor(false)]
		public BrushWidget Background
		{
			get
			{
				return this._background;
			}
			set
			{
				if (value != this._background)
				{
					this._background = value;
					base.OnPropertyChanged<BrushWidget>(value, "Background");
				}
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000218C1 File Offset: 0x0001FAC1
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x000218C9 File Offset: 0x0001FAC9
		[Editor(false)]
		public BrushWidget Border
		{
			get
			{
				return this._border;
			}
			set
			{
				if (value != this._border)
				{
					this._border = value;
					base.OnPropertyChanged<BrushWidget>(value, "Border");
				}
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x000218E7 File Offset: 0x0001FAE7
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x000218EF File Offset: 0x0001FAEF
		[Editor(false)]
		public BrushWidget TroopClassBorder
		{
			get
			{
				return this._troopClassBorder;
			}
			set
			{
				if (value != this._troopClassBorder)
				{
					this._troopClassBorder = value;
					base.OnPropertyChanged<BrushWidget>(value, "TroopClassBorder");
				}
			}
		}

		// Token: 0x0400057F RID: 1407
		private const string DefaultState = "Default";

		// Token: 0x04000580 RID: 1408
		private const string FocusedState = "Focused";

		// Token: 0x04000581 RID: 1409
		private const string TrackedState = "Tracked";

		// Token: 0x04000582 RID: 1410
		private Vec2 _position;

		// Token: 0x04000583 RID: 1411
		private bool _isAgentInScreenBoundaries;

		// Token: 0x04000584 RID: 1412
		private bool _isAvailable;

		// Token: 0x04000585 RID: 1413
		private bool _isTracked;

		// Token: 0x04000586 RID: 1414
		private bool _isAgentFocused;

		// Token: 0x04000587 RID: 1415
		private bool _hasTargetSentDuelRequest;

		// Token: 0x04000588 RID: 1416
		private bool _hasPlayerSentDuelRequest;

		// Token: 0x04000589 RID: 1417
		private int _wSign;

		// Token: 0x0400058A RID: 1418
		private RichTextWidget _actionText;

		// Token: 0x0400058B RID: 1419
		private BrushWidget _background;

		// Token: 0x0400058C RID: 1420
		private BrushWidget _border;

		// Token: 0x0400058D RID: 1421
		private BrushWidget _troopClassBorder;
	}
}
