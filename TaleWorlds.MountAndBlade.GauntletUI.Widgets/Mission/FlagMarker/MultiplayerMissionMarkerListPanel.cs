using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.FlagMarker
{
	// Token: 0x020000F2 RID: 242
	public class MultiplayerMissionMarkerListPanel : ListPanel
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x000234DC File Offset: 0x000216DC
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x000234E4 File Offset: 0x000216E4
		public float FarAlphaTarget { get; set; } = 0.2f;

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000234ED File Offset: 0x000216ED
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x000234F5 File Offset: 0x000216F5
		public float FarDistanceCutoff { get; set; } = 50f;

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x000234FE File Offset: 0x000216FE
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x00023506 File Offset: 0x00021706
		public float CloseDistanceCutoff { get; set; } = 25f;

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002350F File Offset: 0x0002170F
		public MultiplayerMissionMarkerListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002353C File Offset: 0x0002173C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			float delta = MathF.Clamp(dt * 12f, 0f, 1f);
			if (!this._initialized)
			{
				this.SetInitialAlphaValuesOnCreation();
				this._initialized = true;
			}
			if (this.IsMarkerEnabled)
			{
				using (IEnumerator<Widget> enumerator = base.AllChildrenAndThis.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Widget widget = enumerator.Current;
						bool flag;
						if (widget != this && widget != this._activeWidget)
						{
							Widget activeWidget = this._activeWidget;
							flag = (activeWidget != null && activeWidget.CheckIsMyChildRecursive(widget));
						}
						else
						{
							flag = true;
						}
						if (flag)
						{
							float distanceRelatedAlphaTarget = this.GetDistanceRelatedAlphaTarget(this.Distance);
							if (widget == this.SpawnFlagIconWidget)
							{
								widget.SetAlpha(this.IsSpawnFlag ? this.LocalLerp(widget.AlphaFactor, distanceRelatedAlphaTarget, delta) : 0f);
							}
							else
							{
								widget.SetAlpha(this.LocalLerp(widget.AlphaFactor, distanceRelatedAlphaTarget, delta));
							}
							if (widget != this.RemovalTimeVisiblityWidget)
							{
								widget.IsVisible = ((double)widget.AlphaFactor > 0.05);
							}
						}
						else if (widget != this.RemovalTimeVisiblityWidget)
						{
							widget.IsVisible = false;
						}
					}
					goto IL_1F6;
				}
			}
			foreach (Widget widget2 in base.AllChildrenAndThis)
			{
				bool flag2;
				if (widget2 != this && widget2 != this._activeWidget)
				{
					Widget activeWidget2 = this._activeWidget;
					flag2 = (activeWidget2 != null && activeWidget2.CheckIsMyChildRecursive(widget2));
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					if (widget2 == this.SpawnFlagIconWidget)
					{
						widget2.SetAlpha(this.IsSpawnFlag ? this.LocalLerp(widget2.AlphaFactor, 0f, delta) : 0f);
					}
					else
					{
						widget2.SetAlpha(this.LocalLerp(widget2.AlphaFactor, 0f, delta));
					}
					if (widget2 != this.RemovalTimeVisiblityWidget)
					{
						widget2.IsVisible = ((double)widget2.AlphaFactor > 0.05);
					}
				}
				else if (widget2 != this.RemovalTimeVisiblityWidget)
				{
					widget2.IsVisible = false;
				}
			}
			IL_1F6:
			Widget activeWidget3 = this._activeWidget;
			if (activeWidget3 != null && activeWidget3.IsVisible)
			{
				if (this._activeMarkerType == MultiplayerMissionMarkerListPanel.MissionMarkerType.Flag)
				{
					float x = base.Context.EventManager.PageSize.X;
					float y = base.Context.EventManager.PageSize.Y;
					base.ScaledPositionXOffset = MathF.Clamp(this.Position.x - base.Size.X / 2f, 10f, x - base.Size.X - 10f);
					base.ScaledPositionYOffset = MathF.Clamp(this.Position.y - base.Size.Y / 2f, 10f, y - base.Size.Y - 10f);
					return;
				}
				base.ScaledPositionYOffset = this.Position.y - base.Size.Y / 2f;
				base.ScaledPositionXOffset = this.Position.x - base.Size.X / 2f;
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00023874 File Offset: 0x00021A74
		private float GetDistanceRelatedAlphaTarget(int distance)
		{
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

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000238FC File Offset: 0x00021AFC
		private void SetInitialAlphaValuesOnCreation()
		{
			if (this.IsMarkerEnabled)
			{
				using (IEnumerator<Widget> enumerator = base.AllChildrenAndThis.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Widget widget = enumerator.Current;
						bool flag;
						if (widget != this && widget != this._activeWidget)
						{
							Widget activeWidget = this._activeWidget;
							flag = (activeWidget != null && activeWidget.CheckIsMyChildRecursive(widget));
						}
						else
						{
							flag = true;
						}
						if (flag)
						{
							if (widget == this.SpawnFlagIconWidget)
							{
								widget.SetAlpha((float)(this.IsSpawnFlag ? 1 : 0));
							}
							else
							{
								widget.SetAlpha(1f);
							}
							if (widget != this.RemovalTimeVisiblityWidget)
							{
								widget.IsVisible = ((double)widget.AlphaFactor > 0.05);
							}
						}
						else if (widget != this.RemovalTimeVisiblityWidget)
						{
							widget.IsVisible = false;
						}
					}
					return;
				}
			}
			foreach (Widget widget2 in base.AllChildrenAndThis)
			{
				bool flag2;
				if (widget2 != this && widget2 != this._activeWidget)
				{
					Widget activeWidget2 = this._activeWidget;
					flag2 = (activeWidget2 != null && activeWidget2.CheckIsMyChildRecursive(widget2));
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					widget2.SetAlpha(0f);
					if (widget2 != this.RemovalTimeVisiblityWidget)
					{
						widget2.IsVisible = false;
					}
				}
				else if (widget2 != this.RemovalTimeVisiblityWidget)
				{
					widget2.IsVisible = false;
				}
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00023A60 File Offset: 0x00021C60
		private float LocalLerp(float start, float end, float delta)
		{
			if (Math.Abs(start - end) > 1E-45f)
			{
				return (end - start) * delta + start;
			}
			return end;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00023A7C File Offset: 0x00021C7C
		private void MarkerTypeUpdated()
		{
			this._activeMarkerType = (MultiplayerMissionMarkerListPanel.MissionMarkerType)this.MarkerType;
			switch (this._activeMarkerType)
			{
			case MultiplayerMissionMarkerListPanel.MissionMarkerType.Flag:
				this._activeWidget = this.FlagWidget;
				return;
			case MultiplayerMissionMarkerListPanel.MissionMarkerType.Peer:
				this._activeWidget = this.PeerWidget;
				return;
			case MultiplayerMissionMarkerListPanel.MissionMarkerType.SiegeEngine:
				this._activeWidget = this.SiegeEngineWidget;
				return;
			default:
				return;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00023AD5 File Offset: 0x00021CD5
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x00023ADD File Offset: 0x00021CDD
		public Widget FlagWidget
		{
			get
			{
				return this._flagWidget;
			}
			set
			{
				if (this._flagWidget != value)
				{
					this._flagWidget = value;
					base.OnPropertyChanged<Widget>(value, "FlagWidget");
					this.MarkerTypeUpdated();
				}
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00023B01 File Offset: 0x00021D01
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00023B09 File Offset: 0x00021D09
		public Widget RemovalTimeVisiblityWidget
		{
			get
			{
				return this._removalTimeVisiblityWidget;
			}
			set
			{
				if (this._removalTimeVisiblityWidget != value)
				{
					this._removalTimeVisiblityWidget = value;
					base.OnPropertyChanged<Widget>(value, "RemovalTimeVisiblityWidget");
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00023B27 File Offset: 0x00021D27
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x00023B2F File Offset: 0x00021D2F
		public Widget SpawnFlagIconWidget
		{
			get
			{
				return this._spawnFlagIconWidget;
			}
			set
			{
				if (this._spawnFlagIconWidget != value)
				{
					this._spawnFlagIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "SpawnFlagIconWidget");
				}
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00023B4D File Offset: 0x00021D4D
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x00023B55 File Offset: 0x00021D55
		public Widget PeerWidget
		{
			get
			{
				return this._peerWidget;
			}
			set
			{
				if (this._peerWidget != value)
				{
					this._peerWidget = value;
					base.OnPropertyChanged<Widget>(value, "PeerWidget");
					this.MarkerTypeUpdated();
				}
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00023B79 File Offset: 0x00021D79
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00023B81 File Offset: 0x00021D81
		public Widget SiegeEngineWidget
		{
			get
			{
				return this._siegeEngineWidget;
			}
			set
			{
				if (value != this._siegeEngineWidget)
				{
					this._siegeEngineWidget = value;
					base.OnPropertyChanged<Widget>(value, "SiegeEngineWidget");
					this.MarkerTypeUpdated();
				}
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00023BA5 File Offset: 0x00021DA5
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00023BAD File Offset: 0x00021DAD
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00023BD0 File Offset: 0x00021DD0
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00023BD8 File Offset: 0x00021DD8
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00023BF6 File Offset: 0x00021DF6
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00023BFE File Offset: 0x00021DFE
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00023C1C File Offset: 0x00021E1C
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x00023C24 File Offset: 0x00021E24
		public bool IsSpawnFlag
		{
			get
			{
				return this._isSpawnFlag;
			}
			set
			{
				if (this._isSpawnFlag != value)
				{
					this._isSpawnFlag = value;
					base.OnPropertyChanged(value, "IsSpawnFlag");
				}
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00023C42 File Offset: 0x00021E42
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00023C4A File Offset: 0x00021E4A
		public int MarkerType
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
					base.OnPropertyChanged(value, "MarkerType");
					this.MarkerTypeUpdated();
				}
			}
		}

		// Token: 0x040005DE RID: 1502
		private const int FlagMarkerEdgeMargin = 10;

		// Token: 0x040005E2 RID: 1506
		private MultiplayerMissionMarkerListPanel.MissionMarkerType _activeMarkerType;

		// Token: 0x040005E3 RID: 1507
		private Widget _activeWidget;

		// Token: 0x040005E4 RID: 1508
		private bool _initialized;

		// Token: 0x040005E5 RID: 1509
		private int _distance;

		// Token: 0x040005E6 RID: 1510
		private Widget _flagWidget;

		// Token: 0x040005E7 RID: 1511
		private Widget _peerWidget;

		// Token: 0x040005E8 RID: 1512
		private Widget _siegeEngineWidget;

		// Token: 0x040005E9 RID: 1513
		private Widget _spawnFlagIconWidget;

		// Token: 0x040005EA RID: 1514
		private Vec2 _position;

		// Token: 0x040005EB RID: 1515
		private bool _isMarkerEnabled;

		// Token: 0x040005EC RID: 1516
		private bool _isSpawnFlag;

		// Token: 0x040005ED RID: 1517
		private int _markerType;

		// Token: 0x040005EE RID: 1518
		private Widget _removalTimeVisiblityWidget;

		// Token: 0x020001AC RID: 428
		public enum MissionMarkerType
		{
			// Token: 0x040009AD RID: 2477
			Flag,
			// Token: 0x040009AE RID: 2478
			Peer,
			// Token: 0x040009AF RID: 2479
			SiegeEngine
		}
	}
}
