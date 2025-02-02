using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Map
{
	// Token: 0x0200002D RID: 45
	public class MobilePartyTrackItemVM : ViewModel
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000108CD File Offset: 0x0000EACD
		// (set) Token: 0x0600036B RID: 875 RVA: 0x000108D5 File Offset: 0x0000EAD5
		public MobileParty TrackedParty { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000108DE File Offset: 0x0000EADE
		// (set) Token: 0x0600036D RID: 877 RVA: 0x000108E6 File Offset: 0x0000EAE6
		public Army TrackedArmy { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000108EF File Offset: 0x0000EAEF
		private MobileParty _concernedMobileParty
		{
			get
			{
				Army trackedArmy = this.TrackedArmy;
				return ((trackedArmy != null) ? trackedArmy.LeaderParty : null) ?? this.TrackedParty;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010910 File Offset: 0x0000EB10
		public MobilePartyTrackItemVM(MobileParty trackedParty, Camera mapCamera, Action<Vec2> fastMoveCameraToPosition)
		{
			this._mapCamera = mapCamera;
			this._fastMoveCameraToPosition = fastMoveCameraToPosition;
			this.TrackedParty = trackedParty;
			this.IsTracked = Campaign.Current.VisualTrackerManager.CheckTracked(this._concernedMobileParty);
			this.UpdateProperties();
			this.IsArmy = false;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010960 File Offset: 0x0000EB60
		public MobilePartyTrackItemVM(Army trackedArmy, Camera mapCamera, Action<Vec2> fastMoveCameraToPosition)
		{
			this._mapCamera = mapCamera;
			this._fastMoveCameraToPosition = fastMoveCameraToPosition;
			this.TrackedArmy = trackedArmy;
			this.IsTracked = Campaign.Current.VisualTrackerManager.CheckTracked(this._concernedMobileParty);
			this.UpdateProperties();
			this.IsArmy = true;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000109B0 File Offset: 0x0000EBB0
		internal void UpdateProperties()
		{
			if (this.TrackedArmy != null)
			{
				Army trackedArmy = this.TrackedArmy;
				this._nameBind = ((trackedArmy != null) ? trackedArmy.Name.ToString() : null);
			}
			else if (this.TrackedParty != null)
			{
				if (this.TrackedParty.IsCaravan && this.TrackedParty.LeaderHero != null)
				{
					Hero leaderHero = this.TrackedParty.LeaderHero;
					this._nameBind = ((leaderHero != null) ? leaderHero.Name.ToString() : null);
				}
				else
				{
					this._nameBind = this.TrackedParty.Name.ToString();
				}
			}
			else
			{
				this._nameBind = "";
			}
			this._isVisibleOnMapBind = this.GetIsVisibleOnMap();
			Hero leaderHero2 = this._concernedMobileParty.LeaderHero;
			if (((leaderHero2 != null) ? leaderHero2.Clan : null) != null)
			{
				this._factionVisualBind = new ImageIdentifierVM(BannerCode.CreateFrom(this._concernedMobileParty.LeaderHero.Clan.Banner), true);
				return;
			}
			IFaction mapFaction = this._concernedMobileParty.MapFaction;
			this._factionVisualBind = new ImageIdentifierVM(BannerCode.CreateFrom((mapFaction != null) ? mapFaction.Banner : null), true);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		private bool GetIsVisibleOnMap()
		{
			MobileParty concernedMobileParty = this._concernedMobileParty;
			return (concernedMobileParty == null || !concernedMobileParty.IsVisible) && (this.TrackedArmy != null || (this.TrackedParty != null && this.TrackedParty.IsActive && this.TrackedParty.AttachedTo == null));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00010B18 File Offset: 0x0000ED18
		internal void UpdatePosition()
		{
			if (this._concernedMobileParty != null)
			{
				float z = 0f;
				Campaign.Current.MapSceneWrapper.GetHeightAtPoint(this._concernedMobileParty.VisualPosition2DWithoutError, ref z);
				Vec3 worldSpacePosition = this._concernedMobileParty.VisualPosition2DWithoutError.ToVec3(z) + new Vec3(0f, 0f, 1f, -1f);
				this._latestX = 0f;
				this._latestY = 0f;
				this._latestW = 0f;
				MBWindowManager.WorldToScreenInsideUsableArea(this._mapCamera, worldSpacePosition, ref this._latestX, ref this._latestY, ref this._latestW);
				this._partyPositionBind = new Vec2(this._latestX, this._latestY);
				this._isBehindBind = (this._latestW < 0f);
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		public void ExecuteToggleTrack()
		{
			if (this.IsTracked)
			{
				this.Untrack();
				return;
			}
			this.Track();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00010C07 File Offset: 0x0000EE07
		private void Track()
		{
			this.IsTracked = true;
			if (!Campaign.Current.VisualTrackerManager.CheckTracked(this._concernedMobileParty))
			{
				Campaign.Current.VisualTrackerManager.RegisterObject(this._concernedMobileParty);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010C3C File Offset: 0x0000EE3C
		private void Untrack()
		{
			this.IsTracked = false;
			if (Campaign.Current.VisualTrackerManager.CheckTracked(this._concernedMobileParty))
			{
				Campaign.Current.VisualTrackerManager.RemoveTrackedObject(this._concernedMobileParty, false);
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010C74 File Offset: 0x0000EE74
		public void ExecuteGoToPosition()
		{
			if (this._concernedMobileParty != null)
			{
				Action<Vec2> fastMoveCameraToPosition = this._fastMoveCameraToPosition;
				if (fastMoveCameraToPosition == null)
				{
					return;
				}
				fastMoveCameraToPosition(this._concernedMobileParty.GetLogicalPosition().AsVec2);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00010CAC File Offset: 0x0000EEAC
		public void ExecuteShowTooltip()
		{
			if (this.TrackedArmy != null)
			{
				InformationManager.ShowTooltip(typeof(Army), new object[]
				{
					this.TrackedArmy,
					true,
					false
				});
				return;
			}
			if (this.TrackedParty != null)
			{
				InformationManager.ShowTooltip(typeof(MobileParty), new object[]
				{
					this.TrackedParty,
					true,
					false
				});
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010D2A File Offset: 0x0000EF2A
		public void ExecuteHideTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010D31 File Offset: 0x0000EF31
		public void RefreshBinding()
		{
			this.PartyPosition = this._partyPositionBind;
			this.Name = this._nameBind;
			this.IsEnabled = this._isVisibleOnMapBind;
			this.IsBehind = this._isBehindBind;
			this.FactionVisual = this._factionVisualBind;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00010D6F File Offset: 0x0000EF6F
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00010D77 File Offset: 0x0000EF77
		public Vec2 PartyPosition
		{
			get
			{
				return this._partyPosition;
			}
			set
			{
				if (value != this._partyPosition)
				{
					this._partyPosition = value;
					base.OnPropertyChangedWithValue(value, "PartyPosition");
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00010D9A File Offset: 0x0000EF9A
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public ImageIdentifierVM FactionVisual
		{
			get
			{
				return this._factionVisual;
			}
			set
			{
				if (value != this._factionVisual)
				{
					this._factionVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "FactionVisual");
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00010DC8 File Offset: 0x0000EFC8
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00010DEB File Offset: 0x0000EFEB
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00010DF3 File Offset: 0x0000EFF3
		public bool IsArmy
		{
			get
			{
				return this._isArmy;
			}
			set
			{
				if (value != this._isArmy)
				{
					this._isArmy = value;
					base.OnPropertyChangedWithValue(value, "IsArmy");
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00010E11 File Offset: 0x0000F011
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00010E19 File Offset: 0x0000F019
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
					base.OnPropertyChangedWithValue(value, "IsTracked");
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00010E37 File Offset: 0x0000F037
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00010E3F File Offset: 0x0000F03F
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
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00010E5D File Offset: 0x0000F05D
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00010E65 File Offset: 0x0000F065
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (value != this._isBehind)
				{
					this._isBehind = value;
					base.OnPropertyChangedWithValue(value, "IsBehind");
				}
			}
		}

		// Token: 0x040001CB RID: 459
		private float _latestX;

		// Token: 0x040001CC RID: 460
		private float _latestY;

		// Token: 0x040001CD RID: 461
		private float _latestW;

		// Token: 0x040001CE RID: 462
		private readonly Camera _mapCamera;

		// Token: 0x040001CF RID: 463
		private readonly Action<Vec2> _fastMoveCameraToPosition;

		// Token: 0x040001D0 RID: 464
		private Vec2 _partyPositionBind;

		// Token: 0x040001D1 RID: 465
		private ImageIdentifierVM _factionVisualBind;

		// Token: 0x040001D2 RID: 466
		private bool _isVisibleOnMapBind;

		// Token: 0x040001D3 RID: 467
		private bool _isBehindBind;

		// Token: 0x040001D4 RID: 468
		private string _nameBind;

		// Token: 0x040001D5 RID: 469
		private Vec2 _partyPosition;

		// Token: 0x040001D6 RID: 470
		private ImageIdentifierVM _factionVisual;

		// Token: 0x040001D7 RID: 471
		private string _name;

		// Token: 0x040001D8 RID: 472
		private bool _isArmy;

		// Token: 0x040001D9 RID: 473
		private bool _isTracked;

		// Token: 0x040001DA RID: 474
		private bool _isEnabled;

		// Token: 0x040001DB RID: 475
		private bool _isBehind;
	}
}
