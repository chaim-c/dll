using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.View.Map
{
	// Token: 0x02000053 RID: 83
	public class MapWeatherVisual
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001CC86 File Offset: 0x0001AE86
		public Vec2 Position
		{
			get
			{
				return this._weatherNode.Position;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0001CC94 File Offset: 0x0001AE94
		public Vec2 PrefabSpawnOffset
		{
			get
			{
				Vec2 terrainSize = Campaign.Current.MapSceneWrapper.GetTerrainSize();
				float num = terrainSize.X / (float)Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension;
				float num2 = terrainSize.Y / (float)Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension;
				return new Vec2(num * 0.5f, num2 * 0.5f);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001CD00 File Offset: 0x0001AF00
		public int MaskPixelIndex
		{
			get
			{
				if (this._maskPixelIndex == -1)
				{
					Vec2 terrainSize = Campaign.Current.MapSceneWrapper.GetTerrainSize();
					float num = terrainSize.X / (float)Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension;
					float num2 = terrainSize.Y / (float)Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension;
					int num3 = (int)(this.Position.x / num);
					int num4 = (int)(this.Position.y / num2);
					this._maskPixelIndex = num4 * Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension + num3;
				}
				return this._maskPixelIndex;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public override string ToString()
		{
			return this.Position.ToString();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001CDCD File Offset: 0x0001AFCD
		public MapWeatherVisual(WeatherNode weatherNode)
		{
			this._weatherNode = weatherNode;
			this._previousWeatherEvent = MapWeatherModel.WeatherEvent.Clear;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public void Tick()
		{
			if (this._weatherNode.IsVisuallyDirty)
			{
				bool flag = this._previousWeatherEvent == MapWeatherModel.WeatherEvent.HeavyRain;
				bool flag2 = this._previousWeatherEvent == MapWeatherModel.WeatherEvent.Blizzard;
				MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(this.Position);
				bool flag3 = weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain;
				bool flag4 = weatherEventInPosition == MapWeatherModel.WeatherEvent.LightRain;
				bool flag5 = weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard;
				byte b = flag4 ? 125 : (flag3 ? 200 : 0);
				byte value = (byte)Math.Max((int)b, flag5 ? 200 : 0);
				MapWeatherVisualManager.Current.SetRainData(this.MaskPixelIndex, b);
				MapWeatherVisualManager.Current.SetCloudData(this.MaskPixelIndex, value);
				if (this.Prefab == null)
				{
					if (flag3)
					{
						this.AttachNewRainPrefabToVisual();
					}
					else if (flag5)
					{
						this.AttachNewBlizzardPrefabToVisual();
					}
					else if (MBRandom.RandomFloat < 0.1f)
					{
						MapWeatherVisualManager.Current.SetCloudData(this.MaskPixelIndex, 200);
					}
				}
				else
				{
					if (flag && !flag3 && flag5)
					{
						MapWeatherVisualManager.Current.ReleaseRainPrefab(this.Prefab);
						this.AttachNewBlizzardPrefabToVisual();
					}
					else if (flag2 && !flag5 && flag3)
					{
						MapWeatherVisualManager.Current.ReleaseBlizzardPrefab(this.Prefab);
						this.AttachNewRainPrefabToVisual();
					}
					if (!flag3 && !flag5)
					{
						if (flag)
						{
							MapWeatherVisualManager.Current.ReleaseRainPrefab(this.Prefab);
						}
						else if (flag2)
						{
							MapWeatherVisualManager.Current.ReleaseBlizzardPrefab(this.Prefab);
						}
						this.Prefab = null;
					}
				}
				this._previousWeatherEvent = weatherEventInPosition;
				this._weatherNode.OnVisualUpdated();
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001CF80 File Offset: 0x0001B180
		private void AttachNewRainPrefabToVisual()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = new Vec3(this.Position + this.PrefabSpawnOffset, 26f, -1f);
			GameEntity rainPrefabFromPool = MapWeatherVisualManager.Current.GetRainPrefabFromPool();
			rainPrefabFromPool.SetVisibilityExcludeParents(true);
			rainPrefabFromPool.SetGlobalFrame(identity);
			this.Prefab = rainPrefabFromPool;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		private void AttachNewBlizzardPrefabToVisual()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = new Vec3(this.Position + this.PrefabSpawnOffset, 26f, -1f);
			GameEntity blizzardPrefabFromPool = MapWeatherVisualManager.Current.GetBlizzardPrefabFromPool();
			blizzardPrefabFromPool.SetVisibilityExcludeParents(true);
			blizzardPrefabFromPool.SetGlobalFrame(identity);
			this.Prefab = blizzardPrefabFromPool;
		}

		// Token: 0x040001DE RID: 478
		private readonly WeatherNode _weatherNode;

		// Token: 0x040001DF RID: 479
		public GameEntity Prefab;

		// Token: 0x040001E0 RID: 480
		private MapWeatherModel.WeatherEvent _previousWeatherEvent;

		// Token: 0x040001E1 RID: 481
		private int _maskPixelIndex = -1;
	}
}
