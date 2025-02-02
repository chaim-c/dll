using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.View.Map
{
	// Token: 0x02000054 RID: 84
	public class MapWeatherVisualManager : CampaignEntityVisualComponent
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001D037 File Offset: 0x0001B237
		public static MapWeatherVisualManager Current
		{
			get
			{
				return Campaign.Current.GetEntityComponent<MapWeatherVisualManager>();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001D043 File Offset: 0x0001B243
		private int DimensionSquared
		{
			get
			{
				return Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension * Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001D070 File Offset: 0x0001B270
		public MapWeatherVisualManager()
		{
			this._unusedRainPrefabEntityPool = new List<GameEntity>();
			this._unusedBlizzardPrefabEntityPool = new List<GameEntity>();
			for (int i = 0; i < this.DimensionSquared * 2; i++)
			{
				this._rainData[i] = 0;
				this._rainDataTemporal[i] = 0;
			}
			this._allWeatherNodeVisuals = new MapWeatherVisual[this.DimensionSquared];
			this._mapScene = ((MapScene)Campaign.Current.MapSceneWrapper).Scene;
			WeatherNode[] allWeatherNodes = Campaign.Current.GetCampaignBehavior<MapWeatherCampaignBehavior>().AllWeatherNodes;
			for (int j = 0; j < allWeatherNodes.Length; j++)
			{
				this._allWeatherNodeVisuals[j] = new MapWeatherVisual(allWeatherNodes[j]);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001D184 File Offset: 0x0001B384
		public override void OnVisualTick(MapScreen screen, float realDt, float dt)
		{
			for (int i = 0; i < this._allWeatherNodeVisuals.Length; i++)
			{
				this._allWeatherNodeVisuals[i].Tick();
			}
			TWParallel.For(0, this.DimensionSquared, delegate(int startInclusive, int endExclusive)
			{
				for (int j = startInclusive; j < endExclusive; j++)
				{
					int num = j * 2;
					this._rainDataTemporal[num] = (byte)MBMath.Lerp((float)this._rainDataTemporal[num], (float)this._rainData[num], 1f - (float)Math.Exp((double)(-1.8f * (realDt + dt))), 1E-05f);
					this._rainDataTemporal[num + 1] = (byte)MBMath.Lerp((float)this._rainDataTemporal[num + 1], (float)this._rainData[num + 1], 1f - (float)Math.Exp((double)(-1.8f * (realDt + dt))), 1E-05f);
				}
			}, 16);
			this._mapScene.SetLandscapeRainMaskData(this._rainDataTemporal);
			this.WeatherAudioTick();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001D1FD File Offset: 0x0001B3FD
		public void SetRainData(int dataIndex, byte value)
		{
			this._rainData[dataIndex * 2] = value;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001D20A File Offset: 0x0001B40A
		public void SetCloudData(int dataIndex, byte value)
		{
			this._rainData[dataIndex * 2 + 1] = value;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001D21C File Offset: 0x0001B41C
		private void WeatherAudioTick()
		{
			SoundManager.SetGlobalParameter("Rainfall", 0.5f);
			float num = 1f;
			float num2 = 0f;
			IMapScene mapSceneWrapper = Campaign.Current.MapSceneWrapper;
			MatrixFrame lastFinalRenderCameraFrame = this._mapScene.LastFinalRenderCameraFrame;
			mapSceneWrapper.GetHeightAtPoint(lastFinalRenderCameraFrame.origin.AsVec2, ref num2);
			lastFinalRenderCameraFrame = this._mapScene.LastFinalRenderCameraFrame;
			float num3 = lastFinalRenderCameraFrame.origin.Z - num2;
			if (26f > num3)
			{
				num = num3 / 26f;
			}
			lastFinalRenderCameraFrame = this._mapScene.LastFinalRenderCameraFrame;
			Vec3 origin = lastFinalRenderCameraFrame.Elevate(-25f * num).origin;
			MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(origin.AsVec2);
			if (weatherEventInPosition != MapWeatherModel.WeatherEvent.Clear)
			{
				if (weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain)
				{
					if (this._mapScene.LastFinalRenderCameraPosition.z < 65f)
					{
						this._cameraRainEffect.SetVisibilityExcludeParents(true);
						lastFinalRenderCameraFrame = this._mapScene.LastFinalRenderCameraFrame;
						MatrixFrame matrixFrame = lastFinalRenderCameraFrame.Elevate(-5f);
						this._cameraRainEffect.SetFrame(ref matrixFrame);
					}
					else
					{
						this._cameraRainEffect.SetVisibilityExcludeParents(false);
					}
					this.DestroyBlizzardSound();
					this.StartRainSoundIfNeeded();
					MBMapScene.ApplyRainColorGrade = true;
					return;
				}
				if (weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard)
				{
					this.DestroyRainSound();
					this.StartBlizzardSoundIfNeeded();
					this._cameraRainEffect.SetVisibilityExcludeParents(false);
					MBMapScene.ApplyRainColorGrade = false;
					return;
				}
			}
			else
			{
				this.DestroyBlizzardSound();
				this.DestroyRainSound();
				this._cameraRainEffect.SetVisibilityExcludeParents(false);
				MBMapScene.ApplyRainColorGrade = false;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001D393 File Offset: 0x0001B593
		private void DestroyRainSound()
		{
			if (this._currentRainSound != null)
			{
				this._currentRainSound.Stop();
				this._currentRainSound = null;
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001D3AF File Offset: 0x0001B5AF
		private void DestroyBlizzardSound()
		{
			if (this._currentBlizzardSound != null)
			{
				this._currentBlizzardSound.Stop();
				this._currentBlizzardSound = null;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001D3CB File Offset: 0x0001B5CB
		private void StartRainSoundIfNeeded()
		{
			if (this._currentRainSound == null)
			{
				this._currentRainSound = SoundManager.CreateEvent("event:/map/ambient/bed/rain", this._mapScene);
				this._currentRainSound.Play();
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001D3F7 File Offset: 0x0001B5F7
		private void StartBlizzardSoundIfNeeded()
		{
			if (this._currentBlizzardSound == null)
			{
				this._currentBlizzardSound = SoundManager.CreateEvent("event:/map/ambient/bed/snow", this._mapScene);
				this._currentBlizzardSound.Play();
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001D423 File Offset: 0x0001B623
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.InitializeObjectPoolWithDefaultCount();
			this._cameraRainEffect = GameEntity.Instantiate(this._mapScene, "map_camera_rain_prefab", MatrixFrame.Identity);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001D44C File Offset: 0x0001B64C
		public GameEntity GetRainPrefabFromPool()
		{
			if (this._unusedRainPrefabEntityPool.IsEmpty<GameEntity>())
			{
				this._unusedRainPrefabEntityPool.AddRange(this.CreateNewWeatherPrefabPoolElements("campaign_rain_prefab", 5));
			}
			GameEntity gameEntity = this._unusedRainPrefabEntityPool[0];
			this._unusedRainPrefabEntityPool.Remove(gameEntity);
			return gameEntity;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001D498 File Offset: 0x0001B698
		public GameEntity GetBlizzardPrefabFromPool()
		{
			if (this._unusedBlizzardPrefabEntityPool.IsEmpty<GameEntity>())
			{
				this._unusedBlizzardPrefabEntityPool.AddRange(this.CreateNewWeatherPrefabPoolElements("campaign_snow_prefab", 5));
			}
			GameEntity gameEntity = this._unusedBlizzardPrefabEntityPool[0];
			this._unusedBlizzardPrefabEntityPool.Remove(gameEntity);
			return gameEntity;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
		public void ReleaseRainPrefab(GameEntity prefab)
		{
			this._unusedRainPrefabEntityPool.Add(prefab);
			prefab.SetVisibilityExcludeParents(false);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001D4F9 File Offset: 0x0001B6F9
		public void ReleaseBlizzardPrefab(GameEntity prefab)
		{
			this._unusedBlizzardPrefabEntityPool.Add(prefab);
			prefab.SetVisibilityExcludeParents(false);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001D50E File Offset: 0x0001B70E
		private void InitializeObjectPoolWithDefaultCount()
		{
			this._unusedRainPrefabEntityPool.AddRange(this.CreateNewWeatherPrefabPoolElements("campaign_rain_prefab", 5));
			this._unusedBlizzardPrefabEntityPool.AddRange(this.CreateNewWeatherPrefabPoolElements("campaign_snow_prefab", 5));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001D540 File Offset: 0x0001B740
		private List<GameEntity> CreateNewWeatherPrefabPoolElements(string prefabName, int delta)
		{
			List<GameEntity> list = new List<GameEntity>();
			for (int i = 0; i < delta; i++)
			{
				GameEntity gameEntity = GameEntity.Instantiate(this._mapScene, prefabName, MatrixFrame.Identity);
				gameEntity.SetVisibilityExcludeParents(false);
				list.Add(gameEntity);
			}
			return list;
		}

		// Token: 0x040001E2 RID: 482
		public const int DefaultCloudHeight = 26;

		// Token: 0x040001E3 RID: 483
		private MapWeatherVisual[] _allWeatherNodeVisuals;

		// Token: 0x040001E4 RID: 484
		private const string RainPrefabName = "campaign_rain_prefab";

		// Token: 0x040001E5 RID: 485
		private const string BlizzardPrefabName = "campaign_snow_prefab";

		// Token: 0x040001E6 RID: 486
		private const string RainSoundPath = "event:/map/ambient/bed/rain";

		// Token: 0x040001E7 RID: 487
		private const string SnowSoundPath = "event:/map/ambient/bed/snow";

		// Token: 0x040001E8 RID: 488
		private const string WeatherEventParameterName = "Rainfall";

		// Token: 0x040001E9 RID: 489
		private const string CameraRainPrefabName = "map_camera_rain_prefab";

		// Token: 0x040001EA RID: 490
		private const int DefaultRainObjectPoolCount = 5;

		// Token: 0x040001EB RID: 491
		private const int DefaultBlizzardObjectPoolCount = 5;

		// Token: 0x040001EC RID: 492
		private const int WeatherCheckOriginZDelta = 25;

		// Token: 0x040001ED RID: 493
		private readonly List<GameEntity> _unusedRainPrefabEntityPool;

		// Token: 0x040001EE RID: 494
		private readonly List<GameEntity> _unusedBlizzardPrefabEntityPool;

		// Token: 0x040001EF RID: 495
		private readonly Scene _mapScene;

		// Token: 0x040001F0 RID: 496
		private readonly byte[] _rainData = new byte[Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension * Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension * 2];

		// Token: 0x040001F1 RID: 497
		private readonly byte[] _rainDataTemporal = new byte[Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension * Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension * 2];

		// Token: 0x040001F2 RID: 498
		private SoundEvent _currentRainSound;

		// Token: 0x040001F3 RID: 499
		private SoundEvent _currentBlizzardSound;

		// Token: 0x040001F4 RID: 500
		private GameEntity _cameraRainEffect;
	}
}
