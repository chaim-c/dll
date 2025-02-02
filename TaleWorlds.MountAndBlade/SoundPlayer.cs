using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000350 RID: 848
	public class SoundPlayer : ScriptComponentBehavior
	{
		// Token: 0x06002E67 RID: 11879 RVA: 0x000BE01C File Offset: 0x000BC21C
		private void ValidateSoundEvent()
		{
			if ((this.SoundEvent == null || !this.SoundEvent.IsValid) && this.SoundName.Length > 0)
			{
				if (this.SoundCode == -1)
				{
					this.SoundCode = SoundManager.GetEventGlobalIndex(this.SoundName);
				}
				this.SoundEvent = SoundEvent.CreateEvent(this.SoundCode, base.GameEntity.Scene);
			}
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000BE082 File Offset: 0x000BC282
		public void UpdatePlaying()
		{
			this.Playing = (this.SoundEvent != null && this.SoundEvent.IsValid && this.SoundEvent.IsPlaying());
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000BE0B0 File Offset: 0x000BC2B0
		public void PlaySound()
		{
			if (this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid)
			{
				this.SoundEvent.SetPosition(base.GameEntity.GlobalPosition);
				this.SoundEvent.Play();
				this.Playing = true;
			}
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000BE104 File Offset: 0x000BC304
		public void ResumeSound()
		{
			if (this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid && this.SoundEvent.IsPaused())
			{
				this.SoundEvent.Resume();
				this.Playing = true;
			}
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000BE143 File Offset: 0x000BC343
		public void PauseSound()
		{
			if (!this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid)
			{
				this.SoundEvent.Pause();
				this.Playing = false;
			}
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000BE175 File Offset: 0x000BC375
		public void StopSound()
		{
			if (!this.Playing)
			{
				return;
			}
			if (this.SoundEvent != null && this.SoundEvent.IsValid)
			{
				this.SoundEvent.Stop();
				this.Playing = false;
			}
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000BE1A7 File Offset: 0x000BC3A7
		protected internal override void OnInit()
		{
			base.OnInit();
			MBDebug.Print("SoundPlayer : OnInit called.", 0, Debug.DebugColor.Yellow, 17592186044416UL);
			this.ValidateSoundEvent();
			if (this.AutoStart)
			{
				this.PlaySound();
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000BE1E5 File Offset: 0x000BC3E5
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000BE1EF File Offset: 0x000BC3EF
		protected internal override void OnTick(float dt)
		{
			this.UpdatePlaying();
			if (!this.Playing && this.AutoLoop)
			{
				this.ValidateSoundEvent();
				this.PlaySound();
			}
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000BE213 File Offset: 0x000BC413
		protected internal override bool MovesEntity()
		{
			return false;
		}

		// Token: 0x0400138E RID: 5006
		private bool Playing;

		// Token: 0x0400138F RID: 5007
		private int SoundCode = -1;

		// Token: 0x04001390 RID: 5008
		private SoundEvent SoundEvent;

		// Token: 0x04001391 RID: 5009
		public bool AutoLoop;

		// Token: 0x04001392 RID: 5010
		public bool AutoStart;

		// Token: 0x04001393 RID: 5011
		public string SoundName;
	}
}
