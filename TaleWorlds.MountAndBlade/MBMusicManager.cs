using System;
using System.Threading;
using psai.net;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D1 RID: 465
	public class MBMusicManager
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0005CAE0 File Offset: 0x0005ACE0
		// (set) Token: 0x06001A69 RID: 6761 RVA: 0x0005CAE7 File Offset: 0x0005ACE7
		public static MBMusicManager Current { get; private set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0005CAEF File Offset: 0x0005ACEF
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x0005CAF7 File Offset: 0x0005ACF7
		public MusicMode CurrentMode { get; private set; }

		// Token: 0x06001A6C RID: 6764 RVA: 0x0005CB00 File Offset: 0x0005AD00
		private MBMusicManager()
		{
			if (!NativeConfig.DisableSound)
			{
				PsaiCore.Instance.LoadSoundtrackFromProjectFile(BasePath.Name + "music/soundtrack.xml");
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0005CB30 File Offset: 0x0005AD30
		public static bool IsCreationCompleted()
		{
			return MBMusicManager._creationCompleted;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0005CB37 File Offset: 0x0005AD37
		private static void ProcessCreation(object callback)
		{
			MBMusicManager.Current = new MBMusicManager();
			MusicParameters.LoadFromXml();
			MBMusicManager._creationCompleted = true;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0005CB4E File Offset: 0x0005AD4E
		public static void Create()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(MBMusicManager.ProcessCreation));
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0005CB64 File Offset: 0x0005AD64
		public static void Initialize()
		{
			if (!MBMusicManager._initialized)
			{
				MBMusicManager.Current._battleMode = new MBMusicManager.BattleMusicMode();
				MBMusicManager.Current._campaignMode = new MBMusicManager.CampaignMusicMode();
				MBMusicManager.Current.CurrentMode = MusicMode.Paused;
				MBMusicManager.Current._menuModeActivationTimer = 0.5f;
				MBMusicManager._initialized = true;
				Debug.Print("MusicManager Initialize completed.", 0, Debug.DebugColor.Green, 281474976710656UL);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0005CBCB File Offset: 0x0005ADCB
		public void OnCampaignMusicHandlerInit(IMusicHandler campaignMusicHandler)
		{
			this._campaignMusicHandler = campaignMusicHandler;
			this._activeMusicHandler = this._campaignMusicHandler;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0005CBE0 File Offset: 0x0005ADE0
		public void OnCampaignMusicHandlerFinalize()
		{
			this._campaignMusicHandler = null;
			this.CheckActiveHandler();
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0005CBEF File Offset: 0x0005ADEF
		public void OnBattleMusicHandlerInit(IMusicHandler battleMusicHandler)
		{
			this._battleMusicHandler = battleMusicHandler;
			this._activeMusicHandler = this._battleMusicHandler;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0005CC04 File Offset: 0x0005AE04
		public void OnBattleMusicHandlerFinalize()
		{
			this._battleMusicHandler = null;
			this.CheckActiveHandler();
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0005CC13 File Offset: 0x0005AE13
		public void OnSilencedMusicHandlerInit(IMusicHandler silencedMusicHandler)
		{
			this._silencedMusicHandler = silencedMusicHandler;
			this._activeMusicHandler = this._silencedMusicHandler;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0005CC28 File Offset: 0x0005AE28
		public void OnSilencedMusicHandlerFinalize()
		{
			this._silencedMusicHandler = null;
			this.CheckActiveHandler();
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0005CC37 File Offset: 0x0005AE37
		private void CheckActiveHandler()
		{
			IMusicHandler activeMusicHandler;
			if ((activeMusicHandler = this._battleMusicHandler) == null)
			{
				activeMusicHandler = (this._silencedMusicHandler ?? this._campaignMusicHandler);
			}
			this._activeMusicHandler = activeMusicHandler;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0005CC59 File Offset: 0x0005AE59
		private void ActivateMenuMode()
		{
			if (!this._systemPaused)
			{
				this.CurrentMode = MusicMode.Menu;
				PsaiCore.Instance.MenuModeEnter(5, 0.5f);
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0005CC7B File Offset: 0x0005AE7B
		private void DeactivateMenuMode()
		{
			PsaiCore.Instance.MenuModeLeave();
			this.CurrentMode = MusicMode.Paused;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0005CC8F File Offset: 0x0005AE8F
		public void ActivateBattleMode()
		{
			if (!this._systemPaused)
			{
				this.CurrentMode = MusicMode.Battle;
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0005CCA0 File Offset: 0x0005AEA0
		public void DeactivateBattleMode()
		{
			PsaiCore.Instance.StopMusic(true, 3f);
			this.CurrentMode = MusicMode.Paused;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0005CCBA File Offset: 0x0005AEBA
		public void ActivateCampaignMode()
		{
			if (!this._systemPaused)
			{
				this.CurrentMode = MusicMode.Campaign;
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0005CCCB File Offset: 0x0005AECB
		public void DeactivateCampaignMode()
		{
			PsaiCore.Instance.StopMusic(true, 3f);
			this.CurrentMode = MusicMode.Paused;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0005CCE8 File Offset: 0x0005AEE8
		public void DeactivateCurrentMode()
		{
			switch (this.CurrentMode)
			{
			case MusicMode.Menu:
				break;
			case MusicMode.Campaign:
				this.DeactivateCampaignMode();
				return;
			case MusicMode.Battle:
				this.DeactivateBattleMode();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0005CD1E File Offset: 0x0005AF1E
		private bool CheckMenuModeActivationTimer()
		{
			return this._menuModeActivationTimer <= 0f;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0005CD30 File Offset: 0x0005AF30
		public void UnpauseMusicManagerSystem()
		{
			if (this._systemPaused)
			{
				this._systemPaused = false;
				this._menuModeActivationTimer = 1f;
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0005CD4C File Offset: 0x0005AF4C
		public void PauseMusicManagerSystem()
		{
			if (!this._systemPaused)
			{
				if (this.CurrentMode == MusicMode.Menu)
				{
					this.DeactivateMenuMode();
				}
				this._systemPaused = true;
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0005CD6C File Offset: 0x0005AF6C
		public void StartTheme(MusicTheme theme, float startIntensity, bool queueEndSegment = false)
		{
			PsaiCore.Instance.TriggerMusicTheme((int)theme, startIntensity);
			if (queueEndSegment)
			{
				PsaiCore.Instance.StopMusic(false, 3f);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0005CD8F File Offset: 0x0005AF8F
		public void StartThemeWithConstantIntensity(MusicTheme theme, bool queueEndSegment = false)
		{
			PsaiCore.Instance.HoldCurrentIntensity(true);
			this.StartTheme(theme, 0f, queueEndSegment);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0005CDAA File Offset: 0x0005AFAA
		public void ForceStopThemeWithFadeOut()
		{
			PsaiCore.Instance.StopMusic(true, 3f);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0005CDBD File Offset: 0x0005AFBD
		public void ChangeCurrentThemeIntensity(float deltaIntensity)
		{
			PsaiCore.Instance.AddToCurrentIntensity(deltaIntensity);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0005CDCC File Offset: 0x0005AFCC
		public void Update(float dt)
		{
			if (Utilities.EngineFrameNo == this._latestFrameUpdatedNo)
			{
				return;
			}
			this._latestFrameUpdatedNo = Utilities.EngineFrameNo;
			if (this._menuModeActivationTimer > 0f)
			{
				this._menuModeActivationTimer -= dt;
			}
			if (!this._systemPaused)
			{
				if (GameStateManager.Current != null && GameStateManager.Current.ActiveState != null)
				{
					GameState activeState = GameStateManager.Current.ActiveState;
					MusicMode currentMode = this.CurrentMode;
					if (currentMode != MusicMode.Paused)
					{
						if (currentMode == MusicMode.Menu)
						{
							if (!activeState.IsMusicMenuState)
							{
								this.DeactivateMenuMode();
							}
						}
					}
					else if (activeState.IsMusicMenuState && this.CheckMenuModeActivationTimer())
					{
						this.ActivateMenuMode();
					}
				}
				if (this._activeMusicHandler != null)
				{
					this._activeMusicHandler.OnUpdated(dt);
				}
			}
			PsaiCore.Instance.Update();
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0005CE88 File Offset: 0x0005B088
		public MusicTheme GetSiegeTheme(CultureCode cultureCode)
		{
			return this._battleMode.GetSiegeTheme(cultureCode);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0005CE96 File Offset: 0x0005B096
		public MusicTheme GetBattleTheme(CultureCode cultureCode, int battleSize, out bool isPaganBattle)
		{
			return this._battleMode.GetBattleTheme(cultureCode, battleSize, out isPaganBattle);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0005CEA6 File Offset: 0x0005B0A6
		public MusicTheme GetBattleEndTheme(CultureCode cultureCode, bool isVictory)
		{
			return this._battleMode.GetBattleEndTheme(cultureCode, isVictory);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0005CEB5 File Offset: 0x0005B0B5
		public MusicTheme GetBattleTurnsOneSideTheme(CultureCode cultureCode, bool isPositive, bool isPaganBattle)
		{
			if (isPaganBattle)
			{
				if (!isPositive)
				{
					return MusicTheme.PaganTurnsNegative;
				}
				return MusicTheme.PaganTurnsPositive;
			}
			else
			{
				if (!isPositive)
				{
					return MusicTheme.BattleTurnsNegative;
				}
				return MusicTheme.BattleTurnsPositive;
			}
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0005CECC File Offset: 0x0005B0CC
		public MusicTheme GetCampaignMusicTheme(CultureCode cultureCode, bool isDark, bool isWarMode)
		{
			MusicTheme musicTheme = MusicTheme.None;
			if (!isDark && isWarMode)
			{
				musicTheme = this._campaignMode.GetCampaignDramaticThemeWithCulture(cultureCode);
			}
			if (musicTheme != MusicTheme.None)
			{
				return musicTheme;
			}
			return this._campaignMode.GetCampaignTheme(cultureCode, isDark);
		}

		// Token: 0x04000836 RID: 2102
		private const float DefaultFadeOutDurationInSeconds = 3f;

		// Token: 0x04000837 RID: 2103
		private const float MenuModeActivationTimerInSeconds = 0.5f;

		// Token: 0x0400083A RID: 2106
		private MBMusicManager.BattleMusicMode _battleMode;

		// Token: 0x0400083B RID: 2107
		private MBMusicManager.CampaignMusicMode _campaignMode;

		// Token: 0x0400083C RID: 2108
		private IMusicHandler _campaignMusicHandler;

		// Token: 0x0400083D RID: 2109
		private IMusicHandler _battleMusicHandler;

		// Token: 0x0400083E RID: 2110
		private IMusicHandler _silencedMusicHandler;

		// Token: 0x0400083F RID: 2111
		private IMusicHandler _activeMusicHandler;

		// Token: 0x04000840 RID: 2112
		private static bool _initialized;

		// Token: 0x04000841 RID: 2113
		private static bool _creationCompleted;

		// Token: 0x04000842 RID: 2114
		private float _menuModeActivationTimer;

		// Token: 0x04000843 RID: 2115
		private bool _systemPaused;

		// Token: 0x04000844 RID: 2116
		private int _latestFrameUpdatedNo = -1;

		// Token: 0x020004DC RID: 1244
		private class CampaignMusicMode
		{
			// Token: 0x06003789 RID: 14217 RVA: 0x000DFC3E File Offset: 0x000DDE3E
			public CampaignMusicMode()
			{
				this._factionSpecificCampaignThemeSelectionFactor = 0.35f;
				this._factionSpecificCampaignDramaticThemeSelectionFactor = 0.35f;
			}

			// Token: 0x0600378A RID: 14218 RVA: 0x000DFC5C File Offset: 0x000DDE5C
			public MusicTheme GetCampaignTheme(CultureCode cultureCode, bool isDark)
			{
				if (isDark)
				{
					return MusicTheme.CampaignDark;
				}
				MusicTheme campaignThemeWithCulture = this.GetCampaignThemeWithCulture(cultureCode);
				MusicTheme result;
				if (campaignThemeWithCulture == MusicTheme.None)
				{
					result = MusicTheme.CampaignStandard;
					this._factionSpecificCampaignThemeSelectionFactor += 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignThemeSelectionFactor);
				}
				else
				{
					result = campaignThemeWithCulture;
					this._factionSpecificCampaignThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignThemeSelectionFactor);
				}
				return result;
			}

			// Token: 0x0600378B RID: 14219 RVA: 0x000DFCBC File Offset: 0x000DDEBC
			private MusicTheme GetCampaignThemeWithCulture(CultureCode cultureCode)
			{
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificCampaignThemeSelectionFactor)
				{
					this._factionSpecificCampaignThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignThemeSelectionFactor);
					switch (cultureCode)
					{
					case CultureCode.Empire:
						if (MBRandom.NondeterministicRandomFloat >= 0.5f)
						{
							return MusicTheme.EmpireCampaignB;
						}
						return MusicTheme.EmpireCampaignA;
					case CultureCode.Sturgia:
						return MusicTheme.SturgiaCampaignA;
					case CultureCode.Aserai:
						return MusicTheme.AseraiCampaignA;
					case CultureCode.Vlandia:
						return MusicTheme.VlandiaCampaignA;
					case CultureCode.Khuzait:
						return MusicTheme.KhuzaitCampaignA;
					case CultureCode.Battania:
						return MusicTheme.BattaniaCampaignA;
					}
				}
				return MusicTheme.None;
			}

			// Token: 0x0600378C RID: 14220 RVA: 0x000DFD38 File Offset: 0x000DDF38
			public MusicTheme GetCampaignDramaticThemeWithCulture(CultureCode cultureCode)
			{
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificCampaignDramaticThemeSelectionFactor)
				{
					this._factionSpecificCampaignDramaticThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificCampaignDramaticThemeSelectionFactor);
					switch (cultureCode)
					{
					case CultureCode.Empire:
						return MusicTheme.EmpireCampaignDramatic;
					case CultureCode.Sturgia:
						return MusicTheme.SturgiaCampaignDramatic;
					case CultureCode.Aserai:
						return MusicTheme.AseraiCampaignDramatic;
					case CultureCode.Vlandia:
						return MusicTheme.VlandiaCampaignDramatic;
					case CultureCode.Khuzait:
						return MusicTheme.KhuzaitCampaignDramatic;
					case CultureCode.Battania:
						return MusicTheme.BattaniaCampaignDramatic;
					}
				}
				this._factionSpecificCampaignDramaticThemeSelectionFactor += 0.1f;
				MBMath.ClampUnit(ref this._factionSpecificCampaignDramaticThemeSelectionFactor);
				return MusicTheme.None;
			}

			// Token: 0x04001B47 RID: 6983
			private const float DefaultSelectionFactorForFactionSpecificCampaignTheme = 0.35f;

			// Token: 0x04001B48 RID: 6984
			private const float SelectionFactorDecayAmountForFactionSpecificCampaignTheme = 0.1f;

			// Token: 0x04001B49 RID: 6985
			private const float SelectionFactorGrowthAmountForFactionSpecificCampaignTheme = 0.1f;

			// Token: 0x04001B4A RID: 6986
			private float _factionSpecificCampaignThemeSelectionFactor;

			// Token: 0x04001B4B RID: 6987
			private float _factionSpecificCampaignDramaticThemeSelectionFactor;
		}

		// Token: 0x020004DD RID: 1245
		private class BattleMusicMode
		{
			// Token: 0x0600378D RID: 14221 RVA: 0x000DFDBE File Offset: 0x000DDFBE
			public BattleMusicMode()
			{
				this._factionSpecificBattleThemeSelectionFactor = 0.35f;
				this._factionSpecificSiegeThemeSelectionFactor = 0.35f;
			}

			// Token: 0x0600378E RID: 14222 RVA: 0x000DFDDC File Offset: 0x000DDFDC
			private MusicTheme GetBattleThemeWithCulture(CultureCode cultureCode, out bool isPaganBattle)
			{
				isPaganBattle = false;
				MusicTheme result = MusicTheme.None;
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificBattleThemeSelectionFactor)
				{
					this._factionSpecificBattleThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificBattleThemeSelectionFactor);
					if (cultureCode - CultureCode.Sturgia <= 1 || cultureCode - CultureCode.Khuzait <= 1)
					{
						isPaganBattle = true;
						result = ((MBRandom.NondeterministicRandomFloat < 0.5f) ? MusicTheme.BattlePaganA : MusicTheme.BattlePaganB);
					}
					else
					{
						result = ((MBRandom.NondeterministicRandomFloat < 0.5f) ? MusicTheme.CombatA : MusicTheme.CombatB);
					}
				}
				return result;
			}

			// Token: 0x0600378F RID: 14223 RVA: 0x000DFE50 File Offset: 0x000DE050
			private MusicTheme GetSiegeThemeWithCulture(CultureCode cultureCode)
			{
				MusicTheme result = MusicTheme.None;
				if (MBRandom.NondeterministicRandomFloat <= this._factionSpecificSiegeThemeSelectionFactor)
				{
					this._factionSpecificSiegeThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificSiegeThemeSelectionFactor);
					if (cultureCode - CultureCode.Sturgia <= 1 || cultureCode - CultureCode.Khuzait <= 1)
					{
						result = MusicTheme.PaganSiege;
					}
				}
				return result;
			}

			// Token: 0x06003790 RID: 14224 RVA: 0x000DFE99 File Offset: 0x000DE099
			private MusicTheme GetVictoryThemeForCulture(CultureCode cultureCode)
			{
				if (MBRandom.NondeterministicRandomFloat <= 0.65f)
				{
					switch (cultureCode)
					{
					case CultureCode.Empire:
						return MusicTheme.EmpireVictory;
					case CultureCode.Sturgia:
						return MusicTheme.SturgiaVictory;
					case CultureCode.Aserai:
						return MusicTheme.AseraiVictory;
					case CultureCode.Vlandia:
						return MusicTheme.VlandiaVictory;
					case CultureCode.Khuzait:
						return MusicTheme.KhuzaitVictory;
					case CultureCode.Battania:
						return MusicTheme.BattaniaVictory;
					}
				}
				return MusicTheme.None;
			}

			// Token: 0x06003791 RID: 14225 RVA: 0x000DFEDC File Offset: 0x000DE0DC
			public MusicTheme GetBattleTheme(CultureCode culture, int battleSize, out bool isPaganBattle)
			{
				MusicTheme battleThemeWithCulture = this.GetBattleThemeWithCulture(culture, out isPaganBattle);
				MusicTheme result;
				if (battleThemeWithCulture == MusicTheme.None)
				{
					result = (((float)battleSize < (float)MusicParameters.SmallBattleTreshold - (float)MusicParameters.SmallBattleTreshold * 0.2f * MBRandom.NondeterministicRandomFloat) ? MusicTheme.BattleSmall : MusicTheme.BattleMedium);
					this._factionSpecificBattleThemeSelectionFactor += 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificBattleThemeSelectionFactor);
				}
				else
				{
					result = battleThemeWithCulture;
					this._factionSpecificBattleThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificBattleThemeSelectionFactor);
				}
				return result;
			}

			// Token: 0x06003792 RID: 14226 RVA: 0x000DFF5C File Offset: 0x000DE15C
			public MusicTheme GetSiegeTheme(CultureCode culture)
			{
				MusicTheme siegeThemeWithCulture = this.GetSiegeThemeWithCulture(culture);
				MusicTheme result;
				if (siegeThemeWithCulture == MusicTheme.None)
				{
					result = MusicTheme.BattleSiege;
					this._factionSpecificSiegeThemeSelectionFactor += 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificSiegeThemeSelectionFactor);
				}
				else
				{
					result = siegeThemeWithCulture;
					this._factionSpecificSiegeThemeSelectionFactor -= 0.1f;
					MBMath.ClampUnit(ref this._factionSpecificSiegeThemeSelectionFactor);
				}
				return result;
			}

			// Token: 0x06003793 RID: 14227 RVA: 0x000DFFB8 File Offset: 0x000DE1B8
			public MusicTheme GetBattleEndTheme(CultureCode culture, bool isVictorious)
			{
				MusicTheme result;
				if (isVictorious)
				{
					MusicTheme victoryThemeForCulture = this.GetVictoryThemeForCulture(culture);
					if (victoryThemeForCulture == MusicTheme.None)
					{
						result = MusicTheme.BattleVictory;
					}
					else
					{
						result = victoryThemeForCulture;
					}
				}
				else
				{
					result = MusicTheme.BattleDefeat;
				}
				return result;
			}

			// Token: 0x04001B4C RID: 6988
			private const float DefaultSelectionFactorForFactionSpecificBattleTheme = 0.35f;

			// Token: 0x04001B4D RID: 6989
			private const float SelectionFactorDecayAmountForFactionSpecificBattleTheme = 0.1f;

			// Token: 0x04001B4E RID: 6990
			private const float SelectionFactorGrowthAmountForFactionSpecificBattleTheme = 0.1f;

			// Token: 0x04001B4F RID: 6991
			private const float DefaultSelectionFactorForFactionSpecificVictoryTheme = 0.65f;

			// Token: 0x04001B50 RID: 6992
			private float _factionSpecificBattleThemeSelectionFactor;

			// Token: 0x04001B51 RID: 6993
			private float _factionSpecificSiegeThemeSelectionFactor;
		}
	}
}
