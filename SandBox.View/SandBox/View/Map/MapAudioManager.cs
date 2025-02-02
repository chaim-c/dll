using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;

namespace SandBox.View.Map
{
	// Token: 0x02000035 RID: 53
	internal class MapAudioManager : CampaignEntityVisualComponent
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00012206 File Offset: 0x00010406
		public MapAudioManager()
		{
			this._mapScene = (Campaign.Current.MapSceneWrapper as MapScene);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00012224 File Offset: 0x00010424
		public override void OnVisualTick(MapScreen screen, float realDt, float dt)
		{
			if (CampaignTime.Now.GetSeasonOfYear != this._lastCachedSeason)
			{
				SoundManager.SetGlobalParameter("Season", (float)CampaignTime.Now.GetSeasonOfYear);
				this._lastCachedSeason = CampaignTime.Now.GetSeasonOfYear;
			}
			if (Math.Abs(this._lastCameraZ - this._mapScene.Scene.LastFinalRenderCameraPosition.Z) > 0.1f)
			{
				SoundManager.SetGlobalParameter("CampaignCameraHeight", this._mapScene.Scene.LastFinalRenderCameraPosition.Z);
				this._lastCameraZ = this._mapScene.Scene.LastFinalRenderCameraPosition.Z;
			}
			if ((int)CampaignTime.Now.CurrentHourInDay == this._lastHourUpdate)
			{
				SoundManager.SetGlobalParameter("Daytime", CampaignTime.Now.CurrentHourInDay);
				this._lastHourUpdate = (int)CampaignTime.Now.CurrentHourInDay;
			}
		}

		// Token: 0x040000F6 RID: 246
		private const string SeasonParameterId = "Season";

		// Token: 0x040000F7 RID: 247
		private const string CameraHeightParameterId = "CampaignCameraHeight";

		// Token: 0x040000F8 RID: 248
		private const string TimeOfDayParameterId = "Daytime";

		// Token: 0x040000F9 RID: 249
		private const string WeatherEventIntensityParameterId = "Rainfall";

		// Token: 0x040000FA RID: 250
		private CampaignTime.Seasons _lastCachedSeason;

		// Token: 0x040000FB RID: 251
		private float _lastCameraZ;

		// Token: 0x040000FC RID: 252
		private int _lastHourUpdate;

		// Token: 0x040000FD RID: 253
		private MapScene _mapScene;
	}
}
