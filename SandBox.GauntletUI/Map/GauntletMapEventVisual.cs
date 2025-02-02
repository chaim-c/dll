using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200002C RID: 44
	public class GauntletMapEventVisual : IMapEventVisual
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000C2BD File Offset: 0x0000A4BD
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000C2C5 File Offset: 0x0000A4C5
		public MapEvent MapEvent { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000C2CE File Offset: 0x0000A4CE
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000C2D6 File Offset: 0x0000A4D6
		public Vec2 WorldPosition { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000C2DF File Offset: 0x0000A4DF
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x0000C2E7 File Offset: 0x0000A4E7
		public bool IsVisible { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		private Scene MapScene
		{
			get
			{
				if (this._mapScene == null)
				{
					Campaign campaign = Campaign.Current;
					if (((campaign != null) ? campaign.MapSceneWrapper : null) != null)
					{
						this._mapScene = ((MapScene)Campaign.Current.MapSceneWrapper).Scene;
					}
				}
				return this._mapScene;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000C33E File Offset: 0x0000A53E
		public GauntletMapEventVisual(MapEvent mapEvent, Action<GauntletMapEventVisual> onInitialized, Action<GauntletMapEventVisual> onVisibilityChanged, Action<GauntletMapEventVisual> onDeactivate)
		{
			this._onDeactivate = onDeactivate;
			this._onInitialized = onInitialized;
			this._onVisibilityChanged = onVisibilityChanged;
			this.MapEvent = mapEvent;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000C364 File Offset: 0x0000A564
		public void Initialize(Vec2 position, int battleSizeValue, bool hasSound, bool isVisible)
		{
			this.WorldPosition = position;
			this.IsVisible = isVisible;
			Action<GauntletMapEventVisual> onInitialized = this._onInitialized;
			if (onInitialized != null)
			{
				onInitialized(this);
			}
			if (hasSound)
			{
				if (this.MapEvent.IsFieldBattle)
				{
					if (GauntletMapEventVisual._battleSoundEventIndex == -1)
					{
						GauntletMapEventVisual._battleSoundEventIndex = SoundEvent.GetEventIdFromString("event:/map/ambient/node/battle");
					}
					this._battleSound = SoundEvent.CreateEvent(GauntletMapEventVisual._battleSoundEventIndex, this.MapScene);
					this._battleSound.SetParameter("battle_size", (float)battleSizeValue);
					float num = 0f;
					this.MapScene.GetHeightAtPoint(position, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref num);
					this._battleSound.PlayInPosition(new Vec3(position.x, position.y, num + 2f, -1f));
					if (!isVisible)
					{
						this._battleSound.Pause();
						return;
					}
				}
				else
				{
					if (this.MapEvent.IsSiegeAssault || this.MapEvent.IsSiegeOutside || this.MapEvent.IsSiegeAmbush)
					{
						float z = 0f;
						Vec2 point = (this.MapEvent.MapEventSettlement != null) ? this.MapEvent.MapEventSettlement.GatePosition : this.MapEvent.Position;
						Campaign.Current.MapSceneWrapper.GetHeightAtPoint(point, ref z);
						Vec3 position2 = new Vec3(point.X, point.Y, z, -1f);
						SoundEvent siegeSoundEvent = this._siegeSoundEvent;
						if (siegeSoundEvent != null)
						{
							siegeSoundEvent.Stop();
						}
						this._siegeSoundEvent = SoundEvent.CreateEventFromString("event:/map/ambient/node/battle_siege", this.MapScene);
						this._siegeSoundEvent.SetParameter("battle_size", 4f);
						this._siegeSoundEvent.SetPosition(position2);
						this._siegeSoundEvent.Play();
						return;
					}
					if (this.MapEvent.IsRaid)
					{
						if (this.MapEvent.MapEventSettlement.IsRaided && this._raidedSoundEvent == null)
						{
							this._raidedSoundEvent = SoundEvent.CreateEventFromString("event:/map/ambient/node/burning_village", this.MapScene);
							this._raidedSoundEvent.SetParameter("battle_size", 4f);
							this._raidedSoundEvent.SetPosition(this.MapEvent.MapEventSettlement.GetPosition());
							this._raidedSoundEvent.Play();
							return;
						}
						if (!this.MapEvent.MapEventSettlement.IsRaided && this._raidedSoundEvent != null)
						{
							this._raidedSoundEvent.Stop();
							this._raidedSoundEvent = null;
						}
					}
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		public void OnMapEventEnd()
		{
			Action<GauntletMapEventVisual> onDeactivate = this._onDeactivate;
			if (onDeactivate != null)
			{
				onDeactivate(this);
			}
			if (this._battleSound != null)
			{
				this._battleSound.Stop();
				this._battleSound = null;
			}
			if (this._siegeSoundEvent != null)
			{
				this._siegeSoundEvent.Stop();
				this._siegeSoundEvent = null;
			}
			if (this._raidedSoundEvent != null)
			{
				this._raidedSoundEvent.Stop();
				this._raidedSoundEvent = null;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C634 File Offset: 0x0000A834
		public void SetVisibility(bool isVisible)
		{
			this.IsVisible = isVisible;
			Action<GauntletMapEventVisual> onVisibilityChanged = this._onVisibilityChanged;
			if (onVisibilityChanged != null)
			{
				onVisibilityChanged(this);
			}
			SoundEvent battleSound = this._battleSound;
			if (battleSound != null && battleSound.IsValid)
			{
				if (isVisible && this._battleSound.IsPaused())
				{
					this._battleSound.Resume();
				}
				else if (!isVisible && !this._battleSound.IsPaused())
				{
					this._battleSound.Pause();
				}
			}
			SoundEvent siegeSoundEvent = this._siegeSoundEvent;
			if (siegeSoundEvent != null && siegeSoundEvent.IsValid)
			{
				if (isVisible && this._siegeSoundEvent.IsPaused())
				{
					this._siegeSoundEvent.Resume();
				}
				else if (!isVisible && !this._siegeSoundEvent.IsPaused())
				{
					this._siegeSoundEvent.Pause();
				}
			}
			SoundEvent raidedSoundEvent = this._raidedSoundEvent;
			if (raidedSoundEvent != null && raidedSoundEvent.IsValid)
			{
				if (isVisible && this._raidedSoundEvent.IsPaused())
				{
					this._raidedSoundEvent.Resume();
					return;
				}
				if (!isVisible && !this._raidedSoundEvent.IsPaused())
				{
					this._raidedSoundEvent.Pause();
				}
			}
		}

		// Token: 0x040000C2 RID: 194
		private static int _battleSoundEventIndex = -1;

		// Token: 0x040000C3 RID: 195
		private const string BattleSoundPath = "event:/map/ambient/node/battle";

		// Token: 0x040000C4 RID: 196
		private const string RaidSoundPath = "event:/map/ambient/node/battle_raid";

		// Token: 0x040000C5 RID: 197
		private const string SiegeSoundPath = "event:/map/ambient/node/battle_siege";

		// Token: 0x040000C6 RID: 198
		private SoundEvent _siegeSoundEvent;

		// Token: 0x040000C7 RID: 199
		private SoundEvent _raidedSoundEvent;

		// Token: 0x040000C8 RID: 200
		private SoundEvent _battleSound;

		// Token: 0x040000CC RID: 204
		private readonly Action<GauntletMapEventVisual> _onDeactivate;

		// Token: 0x040000CD RID: 205
		private readonly Action<GauntletMapEventVisual> _onInitialized;

		// Token: 0x040000CE RID: 206
		private readonly Action<GauntletMapEventVisual> _onVisibilityChanged;

		// Token: 0x040000CF RID: 207
		private Scene _mapScene;
	}
}
