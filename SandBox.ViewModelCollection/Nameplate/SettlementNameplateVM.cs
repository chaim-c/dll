using System;
using System.Collections.Generic;
using SandBox.ViewModelCollection.Nameplate.NameplateNotifications.SettlementNotificationTypes;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x0200001C RID: 28
	public class SettlementNameplateVM : NameplateVM
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000C9AF File Offset: 0x0000ABAF
		public Settlement Settlement { get; }

		// Token: 0x06000286 RID: 646 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		public SettlementNameplateVM(Settlement settlement, GameEntity entity, Camera mapCamera, Action<Vec2> fastMoveCameraToPosition)
		{
			this.Settlement = settlement;
			this._mapCamera = mapCamera;
			this._entity = entity;
			this._fastMoveCameraToPosition = fastMoveCameraToPosition;
			this.SettlementNotifications = new SettlementNameplateNotificationsVM(settlement);
			this.SettlementParties = new SettlementNameplatePartyMarkersVM(settlement);
			this.SettlementEvents = new SettlementNameplateEventsVM(settlement);
			this.Name = this.Settlement.Name.ToString();
			this.IsTracked = Campaign.Current.VisualTrackerManager.CheckTracked(settlement);
			if (this.Settlement.IsCastle)
			{
				this.SettlementType = 1;
				this._isCastle = true;
			}
			else if (this.Settlement.IsVillage)
			{
				this.SettlementType = 0;
				this._isVillage = true;
			}
			else if (this.Settlement.IsTown)
			{
				this.SettlementType = 2;
				this._isTown = true;
			}
			else
			{
				this.SettlementType = 0;
				this._isTown = true;
			}
			if (this._entity != null)
			{
				this._worldPos = this._entity.GlobalPosition;
			}
			else
			{
				this._worldPos = this.Settlement.GetLogicalPosition();
			}
			this.RefreshDynamicProperties(false);
			base.SizeType = 1;
			this._rebelliousClans = new List<Clan>();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000CAFA File Offset: 0x0000ACFA
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.Settlement.Name.ToString();
			this.RefreshDynamicProperties(true);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CB20 File Offset: 0x0000AD20
		public override void RefreshDynamicProperties(bool forceUpdate)
		{
			base.RefreshDynamicProperties(forceUpdate);
			if ((this._bindIsVisibleOnMap && this._currentFaction != this.Settlement.MapFaction) || forceUpdate)
			{
				string str = "#";
				IFaction mapFaction = this.Settlement.MapFaction;
				this._bindFactionColor = str + Color.UIntToColorString((mapFaction != null) ? mapFaction.Color : uint.MaxValue);
				Banner banner = null;
				if (this.Settlement.OwnerClan != null)
				{
					banner = this.Settlement.OwnerClan.Banner;
					IFaction mapFaction2 = this.Settlement.MapFaction;
					if (mapFaction2 != null && mapFaction2.IsKingdomFaction && ((Kingdom)this.Settlement.MapFaction).RulingClan == this.Settlement.OwnerClan)
					{
						banner = this.Settlement.OwnerClan.Kingdom.Banner;
					}
				}
				int num = (banner != null) ? banner.GetVersionNo() : 0;
				if ((this._latestBanner != banner && !this._latestBanner.IsContentsSameWith(banner)) || this._latestBannerVersionNo != num)
				{
					this._bindBanner = ((banner != null) ? new ImageIdentifierVM(BannerCode.CreateFrom(banner), true) : new ImageIdentifierVM(ImageIdentifierType.Null));
					this._latestBannerVersionNo = num;
					this._latestBanner = banner;
				}
				this._currentFaction = this.Settlement.MapFaction;
			}
			this._bindIsTracked = Campaign.Current.VisualTrackerManager.CheckTracked(this.Settlement);
			if (this.Settlement.IsHideout)
			{
				ISpottable spottable = (ISpottable)this.Settlement.SettlementComponent;
				this._bindIsInRange = (spottable != null && spottable.IsSpotted);
				return;
			}
			this._bindIsInRange = this.Settlement.IsInspected;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		public override void RefreshRelationStatus()
		{
			this._bindRelation = 0;
			if (this.Settlement.OwnerClan != null)
			{
				if (FactionManager.IsAtWarAgainstFaction(this.Settlement.MapFaction, Hero.MainHero.MapFaction))
				{
					this._bindRelation = 2;
					return;
				}
				if (FactionManager.IsAlliedWithFaction(this.Settlement.MapFaction, Hero.MainHero.MapFaction))
				{
					this._bindRelation = 1;
				}
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000CD28 File Offset: 0x0000AF28
		public override void RefreshPosition()
		{
			base.RefreshPosition();
			this._bindWPos = this._wPosAfterPositionCalculation;
			this._bindWSign = (int)this._bindWPos;
			this._bindIsInside = this._latestIsInsideWindow;
			if (this._bindIsVisibleOnMap)
			{
				this._bindPosition = new Vec2(this._latestX, this._latestY);
				return;
			}
			this._bindPosition = new Vec2(-1000f, -1000f);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public override void RefreshTutorialStatus(string newTutorialHighlightElementID)
		{
			base.RefreshTutorialStatus(newTutorialHighlightElementID);
			Settlement settlement = this.Settlement;
			bool flag;
			if (settlement == null)
			{
				flag = (null != null);
			}
			else
			{
				PartyBase party = settlement.Party;
				flag = (((party != null) ? party.Id : null) != null);
			}
			if (!flag)
			{
				Debug.FailedAssert("Settlement party id is null when refreshing tutorial status", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Nameplate\\SettlementNameplateVM.cs", "RefreshTutorialStatus", 231);
				return;
			}
			this._bindIsTargetedByTutorial = (this.Settlement.Party.Id == newTutorialHighlightElementID);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000CE04 File Offset: 0x0000B004
		public void OnSiegeEventStartedOnSettlement(SiegeEvent siegeEvent)
		{
			this.MapEventVisualType = 2;
			if (this.Settlement.MapFaction == Hero.MainHero.MapFaction && (BannerlordConfig.AutoTrackAttackedSettlements == 0 || (BannerlordConfig.AutoTrackAttackedSettlements == 1 && this.Settlement.MapFaction.Leader == Hero.MainHero)))
			{
				this.Track();
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000CE5C File Offset: 0x0000B05C
		public void OnSiegeEventEndedOnSettlement(SiegeEvent siegeEvent)
		{
			Settlement settlement = this.Settlement;
			bool flag;
			if (settlement == null)
			{
				flag = (null != null);
			}
			else
			{
				PartyBase party = settlement.Party;
				flag = (((party != null) ? party.MapEvent : null) != null);
			}
			if (flag && !this.Settlement.Party.MapEvent.IsFinished)
			{
				this.OnMapEventStartedOnSettlement(this.Settlement.Party.MapEvent);
			}
			else
			{
				this.OnMapEventEndedOnSettlement();
			}
			if (!this._isTrackedManually && BannerlordConfig.AutoTrackAttackedSettlements < 2 && this.Settlement.MapFaction == Hero.MainHero.MapFaction)
			{
				this.Untrack();
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public void OnMapEventStartedOnSettlement(MapEvent mapEvent)
		{
			this.MapEventVisualType = (int)SandBoxUIHelper.GetMapEventVisualTypeFromMapEvent(mapEvent);
			if (this.Settlement.MapFaction == Hero.MainHero.MapFaction && (this.Settlement.IsUnderRaid || this.Settlement.IsUnderSiege || this.Settlement.InRebelliousState) && (BannerlordConfig.AutoTrackAttackedSettlements == 0 || (BannerlordConfig.AutoTrackAttackedSettlements == 1 && this.Settlement.MapFaction.Leader == Hero.MainHero)))
			{
				this.Track();
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000CF70 File Offset: 0x0000B170
		public void OnMapEventEndedOnSettlement()
		{
			this.MapEventVisualType = 0;
			if (!this._isTrackedManually && BannerlordConfig.AutoTrackAttackedSettlements < 2 && !this.Settlement.IsUnderSiege && !this.Settlement.IsUnderRaid && !this.Settlement.InRebelliousState)
			{
				this.Untrack();
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		public void OnRebelliousClanFormed(Clan clan)
		{
			this.MapEventVisualType = 4;
			this._rebelliousClans.Add(clan);
			if (this.Settlement.MapFaction == Hero.MainHero.MapFaction && (BannerlordConfig.AutoTrackAttackedSettlements == 0 || (BannerlordConfig.AutoTrackAttackedSettlements == 1 && this.Settlement.MapFaction.Leader == Hero.MainHero)))
			{
				this.Track();
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000D028 File Offset: 0x0000B228
		public void OnRebelliousClanDisbanded(Clan clan)
		{
			this._rebelliousClans.Remove(clan);
			if (this._rebelliousClans.IsEmpty<Clan>())
			{
				if (this.Settlement.IsUnderSiege)
				{
					this.MapEventVisualType = 2;
					return;
				}
				this.MapEventVisualType = 0;
				if (!this._isTrackedManually && BannerlordConfig.AutoTrackAttackedSettlements < 2)
				{
					this.Untrack();
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000D084 File Offset: 0x0000B284
		public void UpdateNameplateMT(Vec3 cameraPosition)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this.CalculatePosition(cameraPosition);
				this.DetermineIsInsideWindow();
				this.DetermineIsVisibleOnMap(cameraPosition);
				this.RefreshPosition();
				this.RefreshDynamicProperties(false);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		private void CalculatePosition(in Vec3 cameraPosition)
		{
			this._worldPosWithHeight = this._worldPos;
			if (this._isVillage)
			{
				this._heightOffset = 0.5f + MathF.Clamp(cameraPosition.z / 30f, 0f, 1f) * 2.5f;
			}
			else if (this._isCastle)
			{
				this._heightOffset = 0.5f + MathF.Clamp(cameraPosition.z / 30f, 0f, 1f) * 3f;
			}
			else if (this._isTown)
			{
				this._heightOffset = 0.5f + MathF.Clamp(cameraPosition.z / 30f, 0f, 1f) * 6f;
			}
			else
			{
				this._heightOffset = 1f;
			}
			this._worldPosWithHeight += new Vec3(0f, 0f, this._heightOffset, -1f);
			if (this._worldPosWithHeight.IsValidXYZW && this._mapCamera.Position.IsValidXYZW)
			{
				this._latestX = 0f;
				this._latestY = 0f;
				this._latestW = 0f;
				MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, this._worldPosWithHeight, ref this._latestX, ref this._latestY, ref this._latestW);
			}
			this._wPosAfterPositionCalculation = ((this._latestW < 0f) ? -1f : 1.1f);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000D262 File Offset: 0x0000B462
		private void DetermineIsVisibleOnMap(in Vec3 cameraPosition)
		{
			this._bindIsVisibleOnMap = this.IsVisible(cameraPosition);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000D271 File Offset: 0x0000B471
		private void DetermineIsInsideWindow()
		{
			this._latestIsInsideWindow = this.IsInsideWindow();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000D280 File Offset: 0x0000B480
		public void RefreshBindValues()
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				base.FactionColor = this._bindFactionColor;
				this.Banner = this._bindBanner;
				this.Relation = this._bindRelation;
				this.WPos = this._bindWPos;
				this.WSign = this._bindWSign;
				this.IsInside = this._bindIsInside;
				base.Position = this._bindPosition;
				base.IsVisibleOnMap = this._bindIsVisibleOnMap;
				this.IsInRange = this._bindIsInRange;
				base.IsTargetedByTutorial = this._bindIsTargetedByTutorial;
				this.IsTracked = this._bindIsTracked;
				base.DistanceToCamera = this._bindDistanceToCamera;
			}
			if (this.SettlementNotifications.IsEventsRegistered)
			{
				this.SettlementNotifications.Tick();
			}
			if (this.SettlementEvents.IsEventsRegistered)
			{
				this.SettlementEvents.Tick();
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000D37C File Offset: 0x0000B57C
		private bool IsVisible(in Vec3 cameraPosition)
		{
			this._bindDistanceToCamera = this._worldPos.Distance(cameraPosition);
			if (this.IsTracked)
			{
				return true;
			}
			if (this.WPos < 0f || !this._latestIsInsideWindow)
			{
				return false;
			}
			if (cameraPosition.z > 400f)
			{
				return this.Settlement.IsTown;
			}
			if (cameraPosition.z > 200f)
			{
				return this.Settlement.IsFortification;
			}
			return this._bindDistanceToCamera < cameraPosition.z + 60f;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000D408 File Offset: 0x0000B608
		private bool IsInsideWindow()
		{
			return this._latestX <= Screen.RealScreenResolutionWidth && this._latestY <= Screen.RealScreenResolutionHeight && this._latestX + 200f >= 0f && this._latestY + 100f >= 0f;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000D45A File Offset: 0x0000B65A
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.SettlementNotifications.UnloadEvents();
			this.SettlementParties.UnloadEvents();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000D478 File Offset: 0x0000B678
		public void ExecuteTrack()
		{
			if (this.IsTracked)
			{
				this.Untrack();
				this._isTrackedManually = false;
				return;
			}
			this.Track();
			this._isTrackedManually = true;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000D49D File Offset: 0x0000B69D
		private void Track()
		{
			this.IsTracked = true;
			if (!Campaign.Current.VisualTrackerManager.CheckTracked(this.Settlement))
			{
				Campaign.Current.VisualTrackerManager.RegisterObject(this.Settlement);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000D4D2 File Offset: 0x0000B6D2
		private void Untrack()
		{
			this.IsTracked = false;
			if (Campaign.Current.VisualTrackerManager.CheckTracked(this.Settlement))
			{
				Campaign.Current.VisualTrackerManager.RemoveTrackedObject(this.Settlement, false);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000D508 File Offset: 0x0000B708
		public void ExecuteSetCameraPosition()
		{
			this._fastMoveCameraToPosition(this.Settlement.Position2D);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000D520 File Offset: 0x0000B720
		public void ExecuteOpenEncyclopedia()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this.Settlement.EncyclopediaLink);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000D53C File Offset: 0x0000B73C
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000D544 File Offset: 0x0000B744
		public SettlementNameplateNotificationsVM SettlementNotifications
		{
			get
			{
				return this._settlementNotifications;
			}
			set
			{
				if (value != this._settlementNotifications)
				{
					this._settlementNotifications = value;
					base.OnPropertyChangedWithValue<SettlementNameplateNotificationsVM>(value, "SettlementNotifications");
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000D562 File Offset: 0x0000B762
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000D56A File Offset: 0x0000B76A
		public SettlementNameplatePartyMarkersVM SettlementParties
		{
			get
			{
				return this._settlementParties;
			}
			set
			{
				if (value != this._settlementParties)
				{
					this._settlementParties = value;
					base.OnPropertyChangedWithValue<SettlementNameplatePartyMarkersVM>(value, "SettlementParties");
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000D588 File Offset: 0x0000B788
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000D590 File Offset: 0x0000B790
		public SettlementNameplateEventsVM SettlementEvents
		{
			get
			{
				return this._settlementEvents;
			}
			set
			{
				if (value != this._settlementEvents)
				{
					this._settlementEvents = value;
					base.OnPropertyChangedWithValue<SettlementNameplateEventsVM>(value, "SettlementEvents");
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000D5AE File Offset: 0x0000B7AE
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000D5B6 File Offset: 0x0000B7B6
		public int Relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				if (value != this._relation)
				{
					this._relation = value;
					base.OnPropertyChangedWithValue(value, "Relation");
				}
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public int MapEventVisualType
		{
			get
			{
				return this._mapEventVisualType;
			}
			set
			{
				if (value != this._mapEventVisualType)
				{
					this._mapEventVisualType = value;
					base.OnPropertyChangedWithValue(value, "MapEventVisualType");
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000D5FA File Offset: 0x0000B7FA
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000D602 File Offset: 0x0000B802
		public int WSign
		{
			get
			{
				return this._wSign;
			}
			set
			{
				if (value != this._wSign)
				{
					this._wSign = value;
					base.OnPropertyChangedWithValue(value, "WSign");
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000D620 File Offset: 0x0000B820
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000D628 File Offset: 0x0000B828
		public float WPos
		{
			get
			{
				return this._wPos;
			}
			set
			{
				if (value != this._wPos)
				{
					this._wPos = value;
					base.OnPropertyChangedWithValue(value, "WPos");
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000D646 File Offset: 0x0000B846
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000D64E File Offset: 0x0000B84E
		public ImageIdentifierVM Banner
		{
			get
			{
				return this._banner;
			}
			set
			{
				if (value != this._banner)
				{
					this._banner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Banner");
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000D66C File Offset: 0x0000B86C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000D674 File Offset: 0x0000B874
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000D697 File Offset: 0x0000B897
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000D6A9 File Offset: 0x0000B8A9
		public bool IsTracked
		{
			get
			{
				return this._isTracked || this._bindIsTargetedByTutorial;
			}
			set
			{
				if (value != this._isTracked)
				{
					this._isTracked = value;
					base.OnPropertyChangedWithValue(value, "IsTracked");
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000D6C7 File Offset: 0x0000B8C7
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000D6CF File Offset: 0x0000B8CF
		public bool IsInside
		{
			get
			{
				return this._isInside;
			}
			set
			{
				if (value != this._isInside)
				{
					this._isInside = value;
					base.OnPropertyChangedWithValue(value, "IsInside");
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000D6ED File Offset: 0x0000B8ED
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public bool IsInRange
		{
			get
			{
				return this._isInRange;
			}
			set
			{
				if (value != this._isInRange)
				{
					this._isInRange = value;
					base.OnPropertyChangedWithValue(value, "IsInRange");
					if (this.IsInRange)
					{
						this.SettlementNotifications.RegisterEvents();
						this.SettlementParties.RegisterEvents();
						SettlementNameplateEventsVM settlementEvents = this.SettlementEvents;
						if (settlementEvents == null)
						{
							return;
						}
						settlementEvents.RegisterEvents();
						return;
					}
					else
					{
						this.SettlementNotifications.UnloadEvents();
						this.SettlementParties.UnloadEvents();
						SettlementNameplateEventsVM settlementEvents2 = this.SettlementEvents;
						if (settlementEvents2 == null)
						{
							return;
						}
						settlementEvents2.UnloadEvents();
					}
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000D776 File Offset: 0x0000B976
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000D77E File Offset: 0x0000B97E
		public int SettlementType
		{
			get
			{
				return this._settlementType;
			}
			set
			{
				if (value != this._settlementType)
				{
					this._settlementType = value;
					base.OnPropertyChangedWithValue(value, "SettlementType");
				}
			}
		}

		// Token: 0x04000133 RID: 307
		private readonly Camera _mapCamera;

		// Token: 0x04000134 RID: 308
		private object _lockObject = new object();

		// Token: 0x04000136 RID: 310
		private float _latestX;

		// Token: 0x04000137 RID: 311
		private float _latestY;

		// Token: 0x04000138 RID: 312
		private float _latestW;

		// Token: 0x04000139 RID: 313
		private float _heightOffset;

		// Token: 0x0400013A RID: 314
		private bool _latestIsInsideWindow;

		// Token: 0x0400013B RID: 315
		private Banner _latestBanner;

		// Token: 0x0400013C RID: 316
		private int _latestBannerVersionNo;

		// Token: 0x0400013D RID: 317
		private bool _isTrackedManually;

		// Token: 0x0400013E RID: 318
		private readonly GameEntity _entity;

		// Token: 0x0400013F RID: 319
		private Vec3 _worldPos;

		// Token: 0x04000140 RID: 320
		private Vec3 _worldPosWithHeight;

		// Token: 0x04000141 RID: 321
		private IFaction _currentFaction;

		// Token: 0x04000142 RID: 322
		private readonly Action<Vec2> _fastMoveCameraToPosition;

		// Token: 0x04000143 RID: 323
		private readonly bool _isVillage;

		// Token: 0x04000144 RID: 324
		private readonly bool _isCastle;

		// Token: 0x04000145 RID: 325
		private readonly bool _isTown;

		// Token: 0x04000146 RID: 326
		private float _wPosAfterPositionCalculation;

		// Token: 0x04000147 RID: 327
		private string _bindFactionColor;

		// Token: 0x04000148 RID: 328
		private bool _bindIsTracked;

		// Token: 0x04000149 RID: 329
		private ImageIdentifierVM _bindBanner;

		// Token: 0x0400014A RID: 330
		private int _bindRelation;

		// Token: 0x0400014B RID: 331
		private float _bindWPos;

		// Token: 0x0400014C RID: 332
		private float _bindDistanceToCamera;

		// Token: 0x0400014D RID: 333
		private int _bindWSign;

		// Token: 0x0400014E RID: 334
		private bool _bindIsInside;

		// Token: 0x0400014F RID: 335
		private Vec2 _bindPosition;

		// Token: 0x04000150 RID: 336
		private bool _bindIsVisibleOnMap;

		// Token: 0x04000151 RID: 337
		private bool _bindIsInRange;

		// Token: 0x04000152 RID: 338
		private List<Clan> _rebelliousClans;

		// Token: 0x04000153 RID: 339
		private string _name;

		// Token: 0x04000154 RID: 340
		private int _settlementType = -1;

		// Token: 0x04000155 RID: 341
		private ImageIdentifierVM _banner;

		// Token: 0x04000156 RID: 342
		private int _relation;

		// Token: 0x04000157 RID: 343
		private int _wSign;

		// Token: 0x04000158 RID: 344
		private float _wPos;

		// Token: 0x04000159 RID: 345
		private bool _isTracked;

		// Token: 0x0400015A RID: 346
		private bool _isInside;

		// Token: 0x0400015B RID: 347
		private bool _isInRange;

		// Token: 0x0400015C RID: 348
		private int _mapEventVisualType;

		// Token: 0x0400015D RID: 349
		private SettlementNameplateNotificationsVM _settlementNotifications;

		// Token: 0x0400015E RID: 350
		private SettlementNameplatePartyMarkersVM _settlementParties;

		// Token: 0x0400015F RID: 351
		private SettlementNameplateEventsVM _settlementEvents;

		// Token: 0x02000078 RID: 120
		public enum Type
		{
			// Token: 0x04000304 RID: 772
			Village,
			// Token: 0x04000305 RID: 773
			Castle,
			// Token: 0x04000306 RID: 774
			Town
		}

		// Token: 0x02000079 RID: 121
		public enum RelationType
		{
			// Token: 0x04000308 RID: 776
			Neutral,
			// Token: 0x04000309 RID: 777
			Ally,
			// Token: 0x0400030A RID: 778
			Enemy
		}

		// Token: 0x0200007A RID: 122
		public enum IssueTypes
		{
			// Token: 0x0400030C RID: 780
			None,
			// Token: 0x0400030D RID: 781
			Possible,
			// Token: 0x0400030E RID: 782
			Active
		}

		// Token: 0x0200007B RID: 123
		public enum MainQuestTypes
		{
			// Token: 0x04000310 RID: 784
			None,
			// Token: 0x04000311 RID: 785
			Possible,
			// Token: 0x04000312 RID: 786
			Active
		}
	}
}
