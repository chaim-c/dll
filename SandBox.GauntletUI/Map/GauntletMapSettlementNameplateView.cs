using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.View.Map;
using SandBox.ViewModelCollection.Nameplate;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000034 RID: 52
	[OverrideView(typeof(MapSettlementNameplateView))]
	public class GauntletMapSettlementNameplateView : MapView, IGauntletMapEventVisualHandler
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new SettlementNameplatesVM(base.MapScreen._mapCameraView.Camera, new Action<Vec2>(base.MapScreen.FastMoveCameraToPosition));
			GauntletMapBasicView mapView = base.MapScreen.GetMapView<GauntletMapBasicView>();
			base.Layer = mapView.GauntletNameplateLayer;
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._movie = this._layerAsGauntletLayer.LoadMovie("SettlementNameplate", this._dataSource);
			List<Tuple<Settlement, GameEntity>> list = new List<Tuple<Settlement, GameEntity>>();
			foreach (Settlement settlement in Settlement.All)
			{
				GameEntity strategicEntity = PartyVisualManager.Current.GetVisualOfParty(settlement.Party).StrategicEntity;
				Tuple<Settlement, GameEntity> item = new Tuple<Settlement, GameEntity>(settlement, strategicEntity);
				list.Add(item);
			}
			CampaignEvents.OnHideoutSpottedEvent.AddNonSerializedListener(this, new Action<PartyBase, PartyBase>(this.OnHideoutSpotted));
			this._dataSource.Initialize(list);
			GauntletMapEventVisualCreator gauntletMapEventVisualCreator;
			if ((gauntletMapEventVisualCreator = (Campaign.Current.VisualCreator.MapEventVisualCreator as GauntletMapEventVisualCreator)) != null)
			{
				gauntletMapEventVisualCreator.Handlers.Add(this);
				foreach (GauntletMapEventVisual gauntletMapEventVisual in gauntletMapEventVisualCreator.GetCurrentEvents())
				{
					SettlementNameplateVM nameplateOfMapEvent = this.GetNameplateOfMapEvent(gauntletMapEventVisual);
					if (nameplateOfMapEvent != null)
					{
						nameplateOfMapEvent.OnMapEventStartedOnSettlement(gauntletMapEventVisual.MapEvent);
					}
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D86C File Offset: 0x0000BA6C
		protected override void OnResume()
		{
			base.OnResume();
			foreach (SettlementNameplateVM settlementNameplateVM in this._dataSource.Nameplates)
			{
				settlementNameplateVM.RefreshDynamicProperties(true);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		protected override void OnMapScreenUpdate(float dt)
		{
			base.OnMapScreenUpdate(dt);
			this._dataSource.Update();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		protected override void OnFinalize()
		{
			GauntletMapEventVisualCreator gauntletMapEventVisualCreator;
			if ((gauntletMapEventVisualCreator = (Campaign.Current.VisualCreator.MapEventVisualCreator as GauntletMapEventVisualCreator)) != null)
			{
				gauntletMapEventVisualCreator.Handlers.Remove(this);
			}
			CampaignEvents.OnHideoutSpottedEvent.ClearListeners(this);
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			this._dataSource.OnFinalize();
			this._layerAsGauntletLayer = null;
			base.Layer = null;
			this._movie = null;
			this._dataSource = null;
			base.OnFinalize();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D953 File Offset: 0x0000BB53
		private void OnHideoutSpotted(PartyBase party, PartyBase hideoutParty)
		{
			MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/ui/notification/hideout_found"), hideoutParty.Settlement.GetPosition());
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D970 File Offset: 0x0000BB70
		private SettlementNameplateVM GetNameplateOfMapEvent(GauntletMapEventVisual mapEvent)
		{
			bool flag;
			if (mapEvent.MapEvent.EventType == MapEvent.BattleTypes.Raid)
			{
				Settlement mapEventSettlement = mapEvent.MapEvent.MapEventSettlement;
				if (mapEventSettlement == null || !mapEventSettlement.IsUnderRaid)
				{
					GauntletMapEventVisual mapEvent2 = mapEvent;
					flag = (mapEvent2 != null && mapEvent2.MapEvent.IsFinished);
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			bool flag3;
			if (mapEvent.MapEvent.EventType == MapEvent.BattleTypes.Siege)
			{
				Settlement mapEventSettlement2 = mapEvent.MapEvent.MapEventSettlement;
				if (mapEventSettlement2 == null || !mapEventSettlement2.IsUnderSiege)
				{
					GauntletMapEventVisual mapEvent3 = mapEvent;
					flag3 = (mapEvent3 != null && mapEvent3.MapEvent.IsFinished);
				}
				else
				{
					flag3 = true;
				}
			}
			else
			{
				flag3 = false;
			}
			bool flag4 = flag3;
			bool flag5;
			if (mapEvent.MapEvent.EventType == MapEvent.BattleTypes.SallyOut)
			{
				Settlement mapEventSettlement3 = mapEvent.MapEvent.MapEventSettlement;
				if (mapEventSettlement3 == null || !mapEventSettlement3.IsUnderSiege)
				{
					GauntletMapEventVisual mapEvent4 = mapEvent;
					flag5 = (mapEvent4 != null && mapEvent4.MapEvent.IsFinished);
				}
				else
				{
					flag5 = true;
				}
			}
			else
			{
				flag5 = false;
			}
			bool flag6 = flag5;
			if (mapEvent.MapEvent.MapEventSettlement != null && (flag4 || flag2 || flag6))
			{
				return this._dataSource.Nameplates.FirstOrDefault((SettlementNameplateVM n) => n.Settlement == mapEvent.MapEvent.MapEventSettlement);
			}
			return null;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000DAAE File Offset: 0x0000BCAE
		void IGauntletMapEventVisualHandler.OnNewEventStarted(GauntletMapEventVisual newEvent)
		{
			SettlementNameplateVM nameplateOfMapEvent = this.GetNameplateOfMapEvent(newEvent);
			if (nameplateOfMapEvent == null)
			{
				return;
			}
			nameplateOfMapEvent.OnMapEventStartedOnSettlement(newEvent.MapEvent);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
		void IGauntletMapEventVisualHandler.OnInitialized(GauntletMapEventVisual newEvent)
		{
			SettlementNameplateVM nameplateOfMapEvent = this.GetNameplateOfMapEvent(newEvent);
			if (nameplateOfMapEvent == null)
			{
				return;
			}
			nameplateOfMapEvent.OnMapEventStartedOnSettlement(newEvent.MapEvent);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		void IGauntletMapEventVisualHandler.OnEventEnded(GauntletMapEventVisual newEvent)
		{
			SettlementNameplateVM nameplateOfMapEvent = this.GetNameplateOfMapEvent(newEvent);
			if (nameplateOfMapEvent == null)
			{
				return;
			}
			nameplateOfMapEvent.OnMapEventEndedOnSettlement();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000DAF3 File Offset: 0x0000BCF3
		void IGauntletMapEventVisualHandler.OnEventVisibilityChanged(GauntletMapEventVisual visibilityChangedEvent)
		{
		}

		// Token: 0x040000EB RID: 235
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000EC RID: 236
		private IGauntletMovie _movie;

		// Token: 0x040000ED RID: 237
		private SettlementNameplatesVM _dataSource;
	}
}
