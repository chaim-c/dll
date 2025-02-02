using System;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Map
{
	// Token: 0x0200002B RID: 43
	public class MapEventVisualItemVM : ViewModel
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0001006F File Offset: 0x0000E26F
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00010077 File Offset: 0x0000E277
		public MapEvent MapEvent { get; private set; }

		// Token: 0x0600034A RID: 842 RVA: 0x00010080 File Offset: 0x0000E280
		public MapEventVisualItemVM(Camera mapCamera, MapEvent mapEvent, Func<Vec2, Vec3> getRealPositionOfEvent)
		{
			this._mapCamera = mapCamera;
			this._getRealPositionOfEvent = getRealPositionOfEvent;
			this.MapEvent = mapEvent;
			this._mapEventPositionCache = mapEvent.Position;
			this._mapEventRealPosition = this._getRealPositionOfEvent(this._mapEventPositionCache);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000100C0 File Offset: 0x0000E2C0
		public void UpdateProperties()
		{
			this.EventType = (int)SandBoxUIHelper.GetMapEventVisualTypeFromMapEvent(this.MapEvent);
			this._isAVisibleEvent = this.MapEvent.IsVisible;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000100E4 File Offset: 0x0000E2E4
		public void ParallelUpdatePosition()
		{
			this._latestX = 0f;
			this._latestY = 0f;
			this._latestW = 0f;
			if (this._mapEventPositionCache != this.MapEvent.Position)
			{
				this._mapEventPositionCache = this.MapEvent.Position;
				this._mapEventRealPosition = this._getRealPositionOfEvent(this._mapEventPositionCache);
			}
			MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, this._mapEventRealPosition + new Vec3(0f, 0f, 1.5f, -1f), ref this._latestX, ref this._latestY, ref this._latestW);
			this._bindPosition = new Vec2(this._latestX, this._latestY);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000101AB File Offset: 0x0000E3AB
		public void DetermineIsVisibleOnMap()
		{
			this._bindIsVisibleOnMap = (this._latestW > 0f && this._mapCamera.Position.z < 200f && this._isAVisibleEvent);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000101E0 File Offset: 0x0000E3E0
		public void UpdateBindingProperties()
		{
			this.Position = this._bindPosition;
			this.IsVisibleOnMap = this._bindIsVisibleOnMap;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000101FA File Offset: 0x0000E3FA
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00010202 File Offset: 0x0000E402
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
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00010225 File Offset: 0x0000E425
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0001022D File Offset: 0x0000E42D
		public int EventType
		{
			get
			{
				return this._eventType;
			}
			set
			{
				if (this._eventType != value)
				{
					this._eventType = value;
					base.OnPropertyChangedWithValue(value, "EventType");
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0001024B File Offset: 0x0000E44B
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00010253 File Offset: 0x0000E453
		public bool IsVisibleOnMap
		{
			get
			{
				return this._isVisibleOnMap;
			}
			set
			{
				if (this._isVisibleOnMap != value)
				{
					this._isVisibleOnMap = value;
					base.OnPropertyChangedWithValue(value, "IsVisibleOnMap");
				}
			}
		}

		// Token: 0x040001B7 RID: 439
		private Camera _mapCamera;

		// Token: 0x040001B8 RID: 440
		private bool _isAVisibleEvent;

		// Token: 0x040001B9 RID: 441
		private Func<Vec2, Vec3> _getRealPositionOfEvent;

		// Token: 0x040001BA RID: 442
		private Vec2 _mapEventPositionCache;

		// Token: 0x040001BB RID: 443
		private Vec3 _mapEventRealPosition;

		// Token: 0x040001BC RID: 444
		private const float CameraDistanceCutoff = 200f;

		// Token: 0x040001BD RID: 445
		private Vec2 _bindPosition;

		// Token: 0x040001BE RID: 446
		private bool _bindIsVisibleOnMap;

		// Token: 0x040001BF RID: 447
		private float _latestX;

		// Token: 0x040001C0 RID: 448
		private float _latestY;

		// Token: 0x040001C1 RID: 449
		private float _latestW;

		// Token: 0x040001C2 RID: 450
		private Vec2 _position;

		// Token: 0x040001C3 RID: 451
		private int _eventType;

		// Token: 0x040001C4 RID: 452
		private bool _isVisibleOnMap;
	}
}
