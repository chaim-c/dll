using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000317 RID: 791
	public class Bird : MissionObject
	{
		// Token: 0x06002AD7 RID: 10967 RVA: 0x000A58B6 File Offset: 0x000A3AB6
		private Bird.State GetState()
		{
			if (this.CanFly && !this._canLand)
			{
				return Bird.State.Airborne;
			}
			return Bird.State.Perched;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000A58CC File Offset: 0x000A3ACC
		protected internal override void OnInit()
		{
			base.OnInit();
			base.GameEntity.SetAnimationSoundActivation(true);
			base.GameEntity.Skeleton.SetAnimationAtChannel("anim_bird_idle", 0, 1f, -1f, 0f);
			base.GameEntity.Skeleton.SetAnimationParameterAtChannel(0, MBRandom.RandomFloat * 0.5f);
			this._kmPerHour = 4f;
			this._state = this.GetState();
			if (this._timer == null)
			{
				this._timer = new BasicMissionTimer();
			}
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000A5964 File Offset: 0x000A3B64
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			base.GameEntity.Skeleton.SetAnimationAtChannel("anim_bird_idle", 0, 1f, -1f, 0f);
			base.GameEntity.Skeleton.SetAnimationParameterAtChannel(0, 0f);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000A59B2 File Offset: 0x000A3BB2
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000A59BC File Offset: 0x000A3BBC
		protected internal override void OnTick(float dt)
		{
			switch (this._state)
			{
			case Bird.State.TakingOff:
				this.ApplyDisplacement(dt);
				if (base.GameEntity.Skeleton.GetAnimationParameterAtChannel(0) > 0.99f)
				{
					base.GameEntity.Skeleton.SetAnimationAtChannel("anim_bird_cycle", 0, 1f, -1f, 0f);
					this._timer.Reset();
					this.SetDisplacement();
					this._state = Bird.State.Airborne;
					return;
				}
				break;
			case Bird.State.Airborne:
				if (this._timer.ElapsedTime <= 5f)
				{
					this.ApplyDisplacement(dt);
					return;
				}
				if (this._canLand)
				{
					base.GameEntity.Skeleton.SetAnimationAtChannel("anim_bird_landing", 0, 1f, -1f, 0f);
					this._timer.Reset();
					this._state = Bird.State.Landing;
					this.SetDisplacement();
					return;
				}
				base.GameEntity.SetVisibilityExcludeParents(false);
				return;
			case Bird.State.Landing:
				this.ApplyDisplacement(dt);
				if (base.GameEntity.Skeleton.GetAnimationParameterAtChannel(0) > 0.99f)
				{
					base.GameEntity.Skeleton.SetAnimationAtChannel("anim_bird_idle", 0, 1f, -1f, 0f);
					this._timer.Reset();
					this._state = Bird.State.Perched;
					return;
				}
				break;
			case Bird.State.Perched:
				if (this.CanFly && this._timer.ElapsedTime > 5f + MBRandom.RandomFloat * 13f && base.GameEntity.Skeleton.GetAnimationParameterAtChannel(0) > 0.99f)
				{
					base.GameEntity.Skeleton.SetAnimationAtChannel("anim_bird_flying", 0, 1f, -1f, 0f);
					this._timer.Reset();
					this._state = Bird.State.TakingOff;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000A5B84 File Offset: 0x000A3D84
		private void ApplyDisplacement(float dt)
		{
			float f = this._kmPerHour * dt;
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			MatrixFrame matrixFrame = globalFrame;
			Vec3 f2 = globalFrame.rotation.f;
			Vec3 u = globalFrame.rotation.u;
			f2.Normalize();
			u.Normalize();
			if (this._state == Bird.State.TakingOff)
			{
				matrixFrame.origin = matrixFrame.origin - f2 * 0.30769232f + u * 0.1f;
			}
			else if (this._state == Bird.State.Airborne)
			{
				globalFrame.origin -= f2 * f;
				matrixFrame.origin -= f2 * f;
			}
			else if (this._state == Bird.State.Landing)
			{
				matrixFrame.origin = matrixFrame.origin - f2 * 0.30769232f - u * 0.1f;
			}
			base.GameEntity.SetGlobalFrame(globalFrame);
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000A5C90 File Offset: 0x000A3E90
		private void SetDisplacement()
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			Vec3 f = globalFrame.rotation.f;
			Vec3 u = globalFrame.rotation.u;
			f.Normalize();
			u.Normalize();
			if (this._state == Bird.State.TakingOff)
			{
				globalFrame.origin -= f * 20f - u * 6.5f;
			}
			else if (this._state == Bird.State.Landing)
			{
				globalFrame.origin -= f * 20f + u * 6.5f;
			}
			base.GameEntity.SetGlobalFrame(globalFrame);
		}

		// Token: 0x04001076 RID: 4214
		private const float Speed = 14400f;

		// Token: 0x04001077 RID: 4215
		private const string IdleAnimation = "anim_bird_idle";

		// Token: 0x04001078 RID: 4216
		private const string LandingAnimation = "anim_bird_landing";

		// Token: 0x04001079 RID: 4217
		private const string TakingOffAnimation = "anim_bird_flying";

		// Token: 0x0400107A RID: 4218
		private const string FlyCycleAnimation = "anim_bird_cycle";

		// Token: 0x0400107B RID: 4219
		public bool CanFly;

		// Token: 0x0400107C RID: 4220
		private float _kmPerHour;

		// Token: 0x0400107D RID: 4221
		private Bird.State _state;

		// Token: 0x0400107E RID: 4222
		private BasicMissionTimer _timer;

		// Token: 0x0400107F RID: 4223
		private bool _canLand = true;

		// Token: 0x020005D3 RID: 1491
		private enum State
		{
			// Token: 0x04001E9E RID: 7838
			TakingOff,
			// Token: 0x04001E9F RID: 7839
			Airborne,
			// Token: 0x04001EA0 RID: 7840
			Landing,
			// Token: 0x04001EA1 RID: 7841
			Perched
		}
	}
}
