using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003AD RID: 941
	public class MapWeatherCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x0010F382 File Offset: 0x0010D582
		public WeatherNode[] AllWeatherNodes
		{
			get
			{
				return this._weatherNodes;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x0010F38A File Offset: 0x0010D58A
		private int DimensionSquared
		{
			get
			{
				return Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension * Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension;
			}
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x0010F3B5 File Offset: 0x0010D5B5
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunchedEvent));
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x0010F3D0 File Offset: 0x0010D5D0
		private void OnSessionLaunchedEvent(CampaignGameStarter obj)
		{
			this.InitializeTheBehavior();
			for (int i = 0; i < this.DimensionSquared; i++)
			{
				this.UpdateWeatherNodeWithIndex(i);
			}
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x0010F3FB File Offset: 0x0010D5FB
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<int>("_lastUpdatedNodeIndex", ref this._lastUpdatedNodeIndex);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x0010F410 File Offset: 0x0010D610
		private void CreateAndShuffleDataIndicesDeterministic()
		{
			this._weatherNodeDataShuffledIndices = new int[this.DimensionSquared];
			for (int i = 0; i < this.DimensionSquared; i++)
			{
				this._weatherNodeDataShuffledIndices[i] = i;
			}
			MBFastRandom mbfastRandom = new MBFastRandom((uint)Campaign.Current.UniqueGameId.GetDeterministicHashCode());
			for (int j = 0; j < 20; j++)
			{
				for (int k = 0; k < this.DimensionSquared; k++)
				{
					int num = mbfastRandom.Next(this.DimensionSquared);
					int num2 = this._weatherNodeDataShuffledIndices[k];
					this._weatherNodeDataShuffledIndices[k] = this._weatherNodeDataShuffledIndices[num];
					this._weatherNodeDataShuffledIndices[num] = num2;
				}
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x0010F4B0 File Offset: 0x0010D6B0
		private void InitializeTheBehavior()
		{
			this.CreateAndShuffleDataIndicesDeterministic();
			this._weatherNodes = new WeatherNode[this.DimensionSquared];
			Vec2 terrainSize = Campaign.Current.MapSceneWrapper.GetTerrainSize();
			int defaultWeatherNodeDimension = Campaign.Current.Models.MapWeatherModel.DefaultWeatherNodeDimension;
			int num = defaultWeatherNodeDimension;
			int num2 = defaultWeatherNodeDimension;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					float a = (float)i / (float)defaultWeatherNodeDimension * terrainSize.X;
					float b = (float)j / (float)defaultWeatherNodeDimension * terrainSize.Y;
					this._weatherNodes[i * defaultWeatherNodeDimension + j] = new WeatherNode(new Vec2(a, b));
				}
			}
			this.AddEventHandler();
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x0010F560 File Offset: 0x0010D760
		private void AddEventHandler()
		{
			long numTicks = Campaign.Current.Models.MapWeatherModel.WeatherUpdateFrequency.NumTicks - CampaignTime.Now.NumTicks % Campaign.Current.Models.MapWeatherModel.WeatherUpdateFrequency.NumTicks;
			this._weatherTickEvent = CampaignPeriodicEventManager.CreatePeriodicEvent(Campaign.Current.Models.MapWeatherModel.WeatherUpdateFrequency, new CampaignTime(numTicks));
			this._weatherTickEvent.AddHandler(new MBCampaignEvent.CampaignEventDelegate(this.WeatherUpdateTick));
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x0010F5F1 File Offset: 0x0010D7F1
		private void WeatherUpdateTick(MBCampaignEvent campaignEvent, params object[] delegateParams)
		{
			this.UpdateWeatherNodeWithIndex(this._weatherNodeDataShuffledIndices[this._lastUpdatedNodeIndex]);
			this._lastUpdatedNodeIndex++;
			if (this._lastUpdatedNodeIndex == this._weatherNodes.Length)
			{
				this._lastUpdatedNodeIndex = 0;
			}
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x0010F62C File Offset: 0x0010D82C
		private void UpdateWeatherNodeWithIndex(int index)
		{
			WeatherNode weatherNode = this._weatherNodes[index];
			MapWeatherModel.WeatherEvent currentWeatherEvent = weatherNode.CurrentWeatherEvent;
			MapWeatherModel.WeatherEvent weatherEvent = Campaign.Current.Models.MapWeatherModel.UpdateWeatherForPosition(weatherNode.Position, CampaignTime.Now);
			if (currentWeatherEvent != weatherEvent)
			{
				weatherNode.SetVisualDirty();
				return;
			}
			if (currentWeatherEvent == MapWeatherModel.WeatherEvent.Clear && MBRandom.NondeterministicRandomFloat < 0.1f)
			{
				weatherNode.SetVisualDirty();
			}
		}

		// Token: 0x0400119F RID: 4511
		private WeatherNode[] _weatherNodes;

		// Token: 0x040011A0 RID: 4512
		private MBCampaignEvent _weatherTickEvent;

		// Token: 0x040011A1 RID: 4513
		private int[] _weatherNodeDataShuffledIndices;

		// Token: 0x040011A2 RID: 4514
		private int _lastUpdatedNodeIndex;
	}
}
