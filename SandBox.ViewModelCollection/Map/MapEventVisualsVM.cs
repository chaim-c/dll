using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Map
{
	// Token: 0x0200002A RID: 42
	public class MapEventVisualsVM : ViewModel
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0000FE73 File Offset: 0x0000E073
		public MapEventVisualsVM(Camera mapCamera, Func<Vec2, Vec3> getRealPositionOfEvent)
		{
			this._mapCamera = mapCamera;
			this._getRealPositionOfEvent = getRealPositionOfEvent;
			this.MapEvents = new MBBindingList<MapEventVisualItemVM>();
			this.UpdateMapEventsAuxPredicate = new TWParallel.ParallelForAuxPredicate(this.UpdateMapEventsAux);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		private void UpdateMapEventsAux(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				this.MapEvents[i].ParallelUpdatePosition();
				this.MapEvents[i].DetermineIsVisibleOnMap();
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public void Update(float dt)
		{
			TWParallel.For(0, this.MapEvents.Count, this.UpdateMapEventsAuxPredicate, 16);
			for (int i = 0; i < this.MapEvents.Count; i++)
			{
				this.MapEvents[i].UpdateBindingProperties();
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000FF3D File Offset: 0x0000E13D
		public void OnMapEventVisibilityChanged(MapEvent mapEvent)
		{
			if (this._eventToVisualMap.ContainsKey(mapEvent))
			{
				this._eventToVisualMap[mapEvent].UpdateProperties();
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000FF60 File Offset: 0x0000E160
		public void OnMapEventStarted(MapEvent mapEvent)
		{
			if (!this._eventToVisualMap.ContainsKey(mapEvent))
			{
				if (!this.IsMapEventSettlementRelated(mapEvent))
				{
					MapEventVisualItemVM mapEventVisualItemVM = new MapEventVisualItemVM(this._mapCamera, mapEvent, this._getRealPositionOfEvent);
					this._eventToVisualMap.Add(mapEvent, mapEventVisualItemVM);
					this.MapEvents.Add(mapEventVisualItemVM);
					mapEventVisualItemVM.UpdateProperties();
				}
				return;
			}
			if (!this.IsMapEventSettlementRelated(mapEvent))
			{
				this._eventToVisualMap[mapEvent].UpdateProperties();
				return;
			}
			MapEventVisualItemVM item = this._eventToVisualMap[mapEvent];
			this.MapEvents.Remove(item);
			this._eventToVisualMap.Remove(mapEvent);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		public void OnMapEventEnded(MapEvent mapEvent)
		{
			if (this._eventToVisualMap.ContainsKey(mapEvent))
			{
				MapEventVisualItemVM item = this._eventToVisualMap[mapEvent];
				this.MapEvents.Remove(item);
				this._eventToVisualMap.Remove(mapEvent);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001003E File Offset: 0x0000E23E
		private bool IsMapEventSettlementRelated(MapEvent mapEvent)
		{
			return mapEvent.MapEventSettlement != null;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00010049 File Offset: 0x0000E249
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00010051 File Offset: 0x0000E251
		public MBBindingList<MapEventVisualItemVM> MapEvents
		{
			get
			{
				return this._mapEvents;
			}
			set
			{
				if (this._mapEvents != value)
				{
					this._mapEvents = value;
					base.OnPropertyChangedWithValue<MBBindingList<MapEventVisualItemVM>>(value, "MapEvents");
				}
			}
		}

		// Token: 0x040001B1 RID: 433
		private readonly Camera _mapCamera;

		// Token: 0x040001B2 RID: 434
		private readonly Dictionary<MapEvent, MapEventVisualItemVM> _eventToVisualMap = new Dictionary<MapEvent, MapEventVisualItemVM>();

		// Token: 0x040001B3 RID: 435
		private readonly Func<Vec2, Vec3> _getRealPositionOfEvent;

		// Token: 0x040001B4 RID: 436
		private readonly TWParallel.ParallelForAuxPredicate UpdateMapEventsAuxPredicate;

		// Token: 0x040001B5 RID: 437
		private MBBindingList<MapEventVisualItemVM> _mapEvents;
	}
}
