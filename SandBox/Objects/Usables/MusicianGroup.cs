using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.AI;
using SandBox.Objects.AnimationPoints;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace SandBox.Objects.Usables
{
	// Token: 0x0200003A RID: 58
	public class MusicianGroup : UsableMachine
	{
		// Token: 0x060001FC RID: 508 RVA: 0x0000D047 File Offset: 0x0000B247
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			return TextObject.Empty;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000D04E File Offset: 0x0000B24E
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			return string.Empty;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000D055 File Offset: 0x0000B255
		public override UsableMachineAIBase CreateAIBehaviorObject()
		{
			return new UsablePlaceAI(this);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D05D File Offset: 0x0000B25D
		public void SetPlayList(List<SettlementMusicData> playList)
		{
			this._playList = playList.ToList<SettlementMusicData>();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D06B File Offset: 0x0000B26B
		protected override void OnInit()
		{
			base.OnInit();
			this._playList = new List<SettlementMusicData>();
			this._musicianPoints = base.StandingPoints.OfType<PlayMusicPoint>().ToList<PlayMusicPoint>();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D094 File Offset: 0x0000B294
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000D09E File Offset: 0x0000B29E
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			this.CheckNewTrackStart();
			this.CheckTrackEnd();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		private void CheckNewTrackStart()
		{
			if (this._playList.Count > 0 && this._trackEvent == null && (this._gapTimer == null || this._gapTimer.ElapsedTime > 8f))
			{
				if (this._musicianPoints.Any((PlayMusicPoint x) => x.HasUser))
				{
					this._currentTrackIndex++;
					if (this._currentTrackIndex == this._playList.Count)
					{
						this._currentTrackIndex = 0;
					}
					this.SetupInstruments();
					this.StartTrack();
					this._gapTimer = null;
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000D160 File Offset: 0x0000B360
		private void CheckTrackEnd()
		{
			if (this._trackEvent != null)
			{
				if (this._trackEvent.IsPlaying())
				{
					if (!this._musicianPoints.Any((PlayMusicPoint x) => x.HasUser))
					{
						this._trackEvent.Stop();
					}
				}
				if (this._trackEvent != null && !this._trackEvent.IsPlaying())
				{
					this._trackEvent.Release();
					this._trackEvent = null;
					this.StopMusicians();
					this._gapTimer = new BasicMissionTimer();
				}
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		private void StopMusicians()
		{
			foreach (PlayMusicPoint playMusicPoint in this._musicianPoints)
			{
				if (playMusicPoint.HasUser)
				{
					playMusicPoint.EndLoop();
				}
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000D250 File Offset: 0x0000B450
		private void SetupInstruments()
		{
			List<PlayMusicPoint> list = this._musicianPoints.ToList<PlayMusicPoint>();
			list.Shuffle<PlayMusicPoint>();
			SettlementMusicData settlementMusicData = this._playList[this._currentTrackIndex];
			using (List<InstrumentData>.Enumerator enumerator = settlementMusicData.Instruments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					InstrumentData instrumentData = enumerator.Current;
					PlayMusicPoint playMusicPoint = list.FirstOrDefault((PlayMusicPoint x) => x.GameEntity.Parent.Tags.Contains(instrumentData.Tag) || string.IsNullOrEmpty(instrumentData.Tag));
					if (playMusicPoint != null)
					{
						Tuple<InstrumentData, float> instrument = new Tuple<InstrumentData, float>(instrumentData, (float)settlementMusicData.Tempo / 120f);
						playMusicPoint.ChangeInstrument(instrument);
						list.Remove(playMusicPoint);
					}
				}
			}
			Tuple<InstrumentData, float> instrumentEmptyData = this.GetInstrumentEmptyData(settlementMusicData.Tempo);
			foreach (PlayMusicPoint playMusicPoint2 in list)
			{
				playMusicPoint2.ChangeInstrument(instrumentEmptyData);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000D35C File Offset: 0x0000B55C
		private Tuple<InstrumentData, float> GetInstrumentEmptyData(int tempo)
		{
			Tuple<InstrumentData, float> result;
			if (tempo > 130)
			{
				result = new Tuple<InstrumentData, float>(MBObjectManager.Instance.GetObject<InstrumentData>("cheerful"), 1f);
			}
			else if (tempo > 100)
			{
				result = new Tuple<InstrumentData, float>(MBObjectManager.Instance.GetObject<InstrumentData>("active"), 1f);
			}
			else
			{
				result = new Tuple<InstrumentData, float>(MBObjectManager.Instance.GetObject<InstrumentData>("calm"), 1f);
			}
			return result;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		private void StartTrack()
		{
			int eventIdFromString = SoundEvent.GetEventIdFromString(this._playList[this._currentTrackIndex].MusicPath);
			this._trackEvent = SoundEvent.CreateEvent(eventIdFromString, Mission.Current.Scene);
			this._trackEvent.SetPosition(base.GameEntity.GetGlobalFrame().origin);
			this._trackEvent.Play();
			foreach (PlayMusicPoint playMusicPoint in this._musicianPoints)
			{
				playMusicPoint.StartLoop(this._trackEvent);
			}
		}

		// Token: 0x040000BA RID: 186
		public const int GapBetweenTracks = 8;

		// Token: 0x040000BB RID: 187
		public const bool DisableAmbientMusic = true;

		// Token: 0x040000BC RID: 188
		private const int TempoMidValue = 120;

		// Token: 0x040000BD RID: 189
		private const int TempoSpeedUpLimit = 130;

		// Token: 0x040000BE RID: 190
		private const int TempoSlowDownLimit = 100;

		// Token: 0x040000BF RID: 191
		private List<PlayMusicPoint> _musicianPoints;

		// Token: 0x040000C0 RID: 192
		private SoundEvent _trackEvent;

		// Token: 0x040000C1 RID: 193
		private BasicMissionTimer _gapTimer;

		// Token: 0x040000C2 RID: 194
		private List<SettlementMusicData> _playList;

		// Token: 0x040000C3 RID: 195
		private int _currentTrackIndex = -1;
	}
}
