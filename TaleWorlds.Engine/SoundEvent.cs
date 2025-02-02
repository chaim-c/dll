using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000089 RID: 137
	public class SoundEvent
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x0000B6AE File Offset: 0x000098AE
		public int GetSoundId()
		{
			return this._soundId;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0000B6B6 File Offset: 0x000098B6
		private SoundEvent(int soundId)
		{
			this._soundId = soundId;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0000B6C8 File Offset: 0x000098C8
		public static SoundEvent CreateEventFromString(string eventId, Scene scene)
		{
			UIntPtr scene2 = (scene == null) ? UIntPtr.Zero : scene.Pointer;
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEventFromString(eventId, scene2));
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0000B6FD File Offset: 0x000098FD
		public void SetEventMinMaxDistance(Vec3 newRadius)
		{
			EngineApplicationInterface.ISoundEvent.SetEventMinMaxDistance(this._soundId, newRadius);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000B710 File Offset: 0x00009910
		public static int GetEventIdFromString(string name)
		{
			return EngineApplicationInterface.ISoundEvent.GetEventIdFromString(name);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0000B71D File Offset: 0x0000991D
		public static bool PlaySound2D(int soundCodeId)
		{
			return EngineApplicationInterface.ISoundEvent.PlaySound2D(soundCodeId);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0000B72A File Offset: 0x0000992A
		public static bool PlaySound2D(string soundName)
		{
			return SoundEvent.PlaySound2D(SoundEvent.GetEventIdFromString(soundName));
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0000B737 File Offset: 0x00009937
		public static int GetTotalEventCount()
		{
			return EngineApplicationInterface.ISoundEvent.GetTotalEventCount();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0000B743 File Offset: 0x00009943
		public static SoundEvent CreateEvent(int soundCodeId, Scene scene)
		{
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEvent(soundCodeId, scene.Pointer));
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0000B75B File Offset: 0x0000995B
		public bool IsNullSoundEvent()
		{
			return this == SoundEvent.NullSoundEvent;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0000B765 File Offset: 0x00009965
		public bool IsValid
		{
			get
			{
				return this._soundId != -1 && EngineApplicationInterface.ISoundEvent.IsValid(this._soundId);
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0000B782 File Offset: 0x00009982
		public bool Play()
		{
			return EngineApplicationInterface.ISoundEvent.StartEvent(this._soundId);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0000B794 File Offset: 0x00009994
		public void Pause()
		{
			EngineApplicationInterface.ISoundEvent.PauseEvent(this._soundId);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0000B7A6 File Offset: 0x000099A6
		public void Resume()
		{
			EngineApplicationInterface.ISoundEvent.ResumeEvent(this._soundId);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public void PlayExtraEvent(string eventName)
		{
			EngineApplicationInterface.ISoundEvent.PlayExtraEvent(this._soundId, eventName);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0000B7CB File Offset: 0x000099CB
		public void SetSwitch(string switchGroupName, string newSwitchStateName)
		{
			EngineApplicationInterface.ISoundEvent.SetSwitch(this._soundId, switchGroupName, newSwitchStateName);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0000B7DF File Offset: 0x000099DF
		public void TriggerCue()
		{
			EngineApplicationInterface.ISoundEvent.TriggerCue(this._soundId);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000B7F1 File Offset: 0x000099F1
		public bool PlayInPosition(Vec3 position)
		{
			return EngineApplicationInterface.ISoundEvent.StartEventInPosition(this._soundId, ref position);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0000B805 File Offset: 0x00009A05
		public void Stop()
		{
			if (!this.IsValid)
			{
				return;
			}
			EngineApplicationInterface.ISoundEvent.StopEvent(this._soundId);
			this._soundId = -1;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0000B827 File Offset: 0x00009A27
		public void SetParameter(string parameterName, float value)
		{
			EngineApplicationInterface.ISoundEvent.SetEventParameterFromString(this._soundId, parameterName, value);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000B83B File Offset: 0x00009A3B
		public void SetParameter(int parameterIndex, float value)
		{
			EngineApplicationInterface.ISoundEvent.SetEventParameterAtIndex(this._soundId, parameterIndex, value);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0000B84F File Offset: 0x00009A4F
		public Vec3 GetEventMinMaxDistance()
		{
			return EngineApplicationInterface.ISoundEvent.GetEventMinMaxDistance(this._soundId);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000B861 File Offset: 0x00009A61
		public void SetPosition(Vec3 vec)
		{
			if (!this.IsValid)
			{
				return;
			}
			EngineApplicationInterface.ISoundEvent.SetEventPosition(this._soundId, ref vec);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000B87E File Offset: 0x00009A7E
		public void SetVelocity(Vec3 vec)
		{
			if (!this.IsValid)
			{
				return;
			}
			EngineApplicationInterface.ISoundEvent.SetEventVelocity(this._soundId, ref vec);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000B89C File Offset: 0x00009A9C
		public void Release()
		{
			MBDebug.Print("Release Sound Event " + this._soundId, 0, Debug.DebugColor.Red, 17592186044416UL);
			if (this.IsValid)
			{
				if (this.IsPlaying())
				{
					this.Stop();
				}
				EngineApplicationInterface.ISoundEvent.ReleaseEvent(this._soundId);
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		public bool IsPlaying()
		{
			return EngineApplicationInterface.ISoundEvent.IsPlaying(this._soundId);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000B906 File Offset: 0x00009B06
		public bool IsPaused()
		{
			return EngineApplicationInterface.ISoundEvent.IsPaused(this._soundId);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000B918 File Offset: 0x00009B18
		public static SoundEvent CreateEventFromSoundBuffer(string eventId, byte[] soundData, Scene scene)
		{
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEventFromSoundBuffer(eventId, soundData, (scene != null) ? scene.Pointer : UIntPtr.Zero));
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000B941 File Offset: 0x00009B41
		public static SoundEvent CreateEventFromExternalFile(string programmerEventName, string soundFilePath, Scene scene)
		{
			return new SoundEvent(EngineApplicationInterface.ISoundEvent.CreateEventFromExternalFile(programmerEventName, soundFilePath, scene.Pointer));
		}

		// Token: 0x040001AD RID: 429
		private const int NullSoundId = -1;

		// Token: 0x040001AE RID: 430
		private static readonly SoundEvent NullSoundEvent = new SoundEvent(-1);

		// Token: 0x040001AF RID: 431
		private int _soundId;
	}
}
