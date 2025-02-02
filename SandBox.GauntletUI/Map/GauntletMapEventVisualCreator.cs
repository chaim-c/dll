using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200002A RID: 42
	public class GauntletMapEventVisualCreator : IMapEventVisualCreator
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000C164 File Offset: 0x0000A364
		public IMapEventVisual CreateMapEventVisual(MapEvent mapEvent)
		{
			GauntletMapEventVisual newEventVisual = new GauntletMapEventVisual(mapEvent, new Action<GauntletMapEventVisual>(this.OnMapEventInitialized), new Action<GauntletMapEventVisual>(this.OnMapEventVisibilityChanged), new Action<GauntletMapEventVisual>(this.OnMapEventOver));
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers != null)
			{
				handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
				{
					h.OnNewEventStarted(newEventVisual);
				});
			}
			this._listOfEvents.Add(newEventVisual);
			return newEventVisual;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		private void OnMapEventOver(GauntletMapEventVisual overEvent)
		{
			this._listOfEvents.Remove(overEvent);
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers == null)
			{
				return;
			}
			handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
			{
				h.OnEventEnded(overEvent);
			});
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C224 File Offset: 0x0000A424
		private void OnMapEventInitialized(GauntletMapEventVisual initializedEvent)
		{
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers == null)
			{
				return;
			}
			handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
			{
				h.OnInitialized(initializedEvent);
			});
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C25C File Offset: 0x0000A45C
		private void OnMapEventVisibilityChanged(GauntletMapEventVisual visibilityChangedEvent)
		{
			List<IGauntletMapEventVisualHandler> handlers = this.Handlers;
			if (handlers == null)
			{
				return;
			}
			handlers.ForEach(delegate(IGauntletMapEventVisualHandler h)
			{
				h.OnEventVisibilityChanged(visibilityChangedEvent);
			});
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C292 File Offset: 0x0000A492
		public IEnumerable<GauntletMapEventVisual> GetCurrentEvents()
		{
			return this._listOfEvents.AsEnumerable<GauntletMapEventVisual>();
		}

		// Token: 0x040000C0 RID: 192
		public List<IGauntletMapEventVisualHandler> Handlers = new List<IGauntletMapEventVisualHandler>();

		// Token: 0x040000C1 RID: 193
		private readonly List<GauntletMapEventVisual> _listOfEvents = new List<GauntletMapEventVisual>();
	}
}
