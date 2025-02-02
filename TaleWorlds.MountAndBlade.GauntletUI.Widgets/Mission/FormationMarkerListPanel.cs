using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D5 RID: 213
	public class FormationMarkerListPanel : ListPanel
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001EEB9 File Offset: 0x0001D0B9
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0001EEC1 File Offset: 0x0001D0C1
		public float FarAlphaTarget { get; set; } = 0.2f;

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001EECA File Offset: 0x0001D0CA
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0001EED2 File Offset: 0x0001D0D2
		public float FarDistanceCutoff { get; set; } = 50f;

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001EEDB File Offset: 0x0001D0DB
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0001EEE3 File Offset: 0x0001D0E3
		public float CloseDistanceCutoff { get; set; } = 25f;

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0001EEEC File Offset: 0x0001D0EC
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		public float ClosestFadeoutRange { get; set; } = 3f;

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001EEFD File Offset: 0x0001D0FD
		public FormationMarkerListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001EF3C File Offset: 0x0001D13C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			float delta = MathF.Clamp(dt * 12f, 0f, 1f);
			if (this._isMarkersDirty)
			{
				Sprite sprite = null;
				if (!string.IsNullOrEmpty(this.MarkerType))
				{
					sprite = base.Context.SpriteData.GetSprite("General\\compass\\" + this.MarkerType);
				}
				if (sprite != null && this.FormationTypeMarker != null)
				{
					this.FormationTypeMarker.Sprite = sprite;
				}
				else
				{
					Debug.FailedAssert("Couldn't find formation marker type image", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Mission\\FormationMarkerListPanel.cs", "OnLateUpdate", 48);
				}
				if (this.TeamTypeMarker != null)
				{
					this.TeamTypeMarker.RegisterBrushStatesOfWidget();
					if (this.TeamType == 0)
					{
						this.TeamTypeMarker.SetState("Player");
					}
					else if (this.TeamType == 1)
					{
						this.TeamTypeMarker.SetState("Ally");
					}
					else
					{
						this.TeamTypeMarker.SetState("Enemy");
					}
				}
				this._isMarkersDirty = false;
			}
			if (this.IsMarkerEnabled)
			{
				float distanceRelatedAlphaTarget = this.GetDistanceRelatedAlphaTarget(this.Distance);
				this.SetGlobalAlphaRecursively(distanceRelatedAlphaTarget);
				base.IsVisible = ((double)distanceRelatedAlphaTarget > 0.05);
			}
			else
			{
				float alphaFactor = this.LocalLerp(base.AlphaFactor, 0f, delta);
				this.SetGlobalAlphaRecursively(alphaFactor);
				base.IsVisible = ((double)base.AlphaFactor > 0.05);
			}
			this.UpdateVisibility();
			if (base.IsVisible)
			{
				this.UpdateScreenPosition();
				this.DistanceTextWidget.Text = ((int)this.Distance).ToString();
			}
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001F0C2 File Offset: 0x0001D2C2
		private void UpdateVisibility()
		{
			base.IsVisible = (this.IsInsideScreenBoundaries || this.IsTargetingAFormation);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		private void UpdateScreenPosition()
		{
			float num = this.Position.X - base.Size.X / 2f;
			float num2 = this.Position.Y - base.Size.Y / 2f;
			if (this.WSign > 0 && num - base.Size.X / 2f > 0f && num + base.Size.X / 2f < base.Context.EventManager.PageSize.X && num2 > 0f && num2 + base.Size.Y < base.Context.EventManager.PageSize.Y)
			{
				base.IsVisible = true;
				base.ScaledPositionXOffset = num;
				base.ScaledPositionYOffset = num2;
				return;
			}
			if (this.IsTargetingAFormation)
			{
				base.IsVisible = true;
				Vec2 vec = this.Position;
				Vector2 pageSize = base.Context.EventManager.PageSize;
				Vec2 vec2 = new Vec2(base.Context.EventManager.PageSize.X / 2f, base.Context.EventManager.PageSize.Y / 2f);
				vec -= vec2;
				if (this.WSign < 0)
				{
					vec *= -1f;
				}
				float radian = Mathf.Atan2(vec.y, vec.x) - 1.5707964f;
				float num3 = Mathf.Cos(radian);
				float num4 = Mathf.Sin(radian);
				float num5 = num3 / num4;
				Vec2 vec3 = vec2 * 1f;
				vec = ((num3 > 0f) ? new Vec2(-vec3.y / num5, vec2.y) : new Vec2(vec3.y / num5, -vec2.y));
				if (vec.x > vec3.x)
				{
					vec = new Vec2(vec3.x, -vec3.x * num5);
				}
				else if (vec.x < -vec3.x)
				{
					vec = new Vec2(-vec3.x, vec3.x * num5);
				}
				vec += vec2;
				base.ScaledPositionXOffset = Mathf.Clamp(vec.x - base.Size.X / 2f, 0f, pageSize.X - base.Size.X);
				base.ScaledPositionYOffset = Mathf.Clamp(vec.y - base.Size.Y, 0f, pageSize.Y - base.Size.Y);
				return;
			}
			base.IsVisible = false;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001F38C File Offset: 0x0001D58C
		private float GetDistanceRelatedAlphaTarget(float distance)
		{
			if (distance > this.FarDistanceCutoff)
			{
				return this.FarAlphaTarget;
			}
			if (distance <= this.FarDistanceCutoff && distance >= this.CloseDistanceCutoff)
			{
				float amount = (float)Math.Pow((double)((distance - this.CloseDistanceCutoff) / (this.FarDistanceCutoff - this.CloseDistanceCutoff)), 0.3333333333333333);
				return MathF.Clamp(MathF.Lerp(1f, this.FarAlphaTarget, amount, 1E-05f), this.FarAlphaTarget, 1f);
			}
			if (distance < this.CloseDistanceCutoff && distance > this.CloseDistanceCutoff - this.ClosestFadeoutRange)
			{
				float amount2 = (distance - (this.CloseDistanceCutoff - this.ClosestFadeoutRange)) / this.ClosestFadeoutRange;
				return MathF.Lerp(0f, 1f, amount2, 1E-05f);
			}
			return 0f;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001F454 File Offset: 0x0001D654
		private float LocalLerp(float start, float end, float delta)
		{
			if (Math.Abs(start - end) > 1E-45f)
			{
				return (end - start) * delta + start;
			}
			return end;
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0001F46E File Offset: 0x0001D66E
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0001F476 File Offset: 0x0001D676
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
				}
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0001F494 File Offset: 0x0001D694
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0001F49C File Offset: 0x0001D69C
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
				}
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0001F4BA File Offset: 0x0001D6BA
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x0001F4C2 File Offset: 0x0001D6C2
		[DataSourceProperty]
		public Widget FormationTypeMarker
		{
			get
			{
				return this._formationTypeMarker;
			}
			set
			{
				if (this._formationTypeMarker != value)
				{
					this._formationTypeMarker = value;
					base.OnPropertyChanged<Widget>(value, "FormationTypeMarker");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001F4E7 File Offset: 0x0001D6E7
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0001F4EF File Offset: 0x0001D6EF
		[DataSourceProperty]
		public Widget TeamTypeMarker
		{
			get
			{
				return this._teamTypeMarker;
			}
			set
			{
				if (this._teamTypeMarker != value)
				{
					this._teamTypeMarker = value;
					base.OnPropertyChanged<Widget>(value, "TeamTypeMarker");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0001F514 File Offset: 0x0001D714
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0001F51C File Offset: 0x0001D71C
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

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001F53F File Offset: 0x0001D73F
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0001F547 File Offset: 0x0001D747
		[DataSourceProperty]
		public float Distance
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

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0001F565 File Offset: 0x0001D765
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0001F56D File Offset: 0x0001D76D
		[DataSourceProperty]
		public int TeamType
		{
			get
			{
				return this._teamType;
			}
			set
			{
				if (this._teamType != value)
				{
					this._teamType = value;
					base.OnPropertyChanged(value, "TeamType");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001F592 File Offset: 0x0001D792
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0001F59A File Offset: 0x0001D79A
		[DataSourceProperty]
		public int WSign
		{
			get
			{
				return this._wSign;
			}
			set
			{
				if (this._teamType != value)
				{
					this._wSign = value;
					base.OnPropertyChanged(value, "WSign");
				}
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
		[DataSourceProperty]
		public string MarkerType
		{
			get
			{
				return this._markerType;
			}
			set
			{
				if (this._markerType != value)
				{
					this._markerType = value;
					base.OnPropertyChanged<string>(value, "MarkerType");
					this._isMarkersDirty = true;
				}
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001F5EA File Offset: 0x0001D7EA
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0001F5F2 File Offset: 0x0001D7F2
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0001F610 File Offset: 0x0001D810
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0001F618 File Offset: 0x0001D818
		[DataSourceProperty]
		public bool IsTargetingAFormation
		{
			get
			{
				return this._isTargetingAFormation;
			}
			set
			{
				if (this._isTargetingAFormation != value)
				{
					this._isTargetingAFormation = value;
					base.OnPropertyChanged(value, "IsTargetingAFormation");
				}
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0001F636 File Offset: 0x0001D836
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0001F63E File Offset: 0x0001D83E
		[DataSourceProperty]
		public bool IsInsideScreenBoundaries
		{
			get
			{
				return this._isInsideScreenBoundaries;
			}
			set
			{
				if (this._isInsideScreenBoundaries != value)
				{
					this._isInsideScreenBoundaries = value;
					base.OnPropertyChanged(value, "IsInsideScreenBoundaries");
				}
			}
		}

		// Token: 0x04000503 RID: 1283
		private bool _isMarkersDirty = true;

		// Token: 0x04000504 RID: 1284
		private float _distance;

		// Token: 0x04000505 RID: 1285
		private TextWidget _nameTextWidget;

		// Token: 0x04000506 RID: 1286
		private TextWidget _distanceTextWidget;

		// Token: 0x04000507 RID: 1287
		private Vec2 _position;

		// Token: 0x04000508 RID: 1288
		private bool _isMarkerEnabled;

		// Token: 0x04000509 RID: 1289
		private bool _isTargetingAFormation;

		// Token: 0x0400050A RID: 1290
		private bool _isInsideScreenBoundaries;

		// Token: 0x0400050B RID: 1291
		private string _markerType;

		// Token: 0x0400050C RID: 1292
		private int _teamType;

		// Token: 0x0400050D RID: 1293
		private int _wSign;

		// Token: 0x0400050E RID: 1294
		private Widget _formationTypeMarker;

		// Token: 0x0400050F RID: 1295
		private Widget _teamTypeMarker;
	}
}
