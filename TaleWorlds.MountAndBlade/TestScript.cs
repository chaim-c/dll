using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000356 RID: 854
	public class TestScript : ScriptComponentBehavior
	{
		// Token: 0x06002ED2 RID: 11986 RVA: 0x000BF65C File Offset: 0x000BD85C
		private void Move(float dt)
		{
			if (MathF.Abs(this.MoveDistance) < 1E-05f)
			{
				return;
			}
			Vec3 v = new Vec3(this.MoveAxisX, this.MoveAxisY, this.MoveAxisZ, -1f);
			v.Normalize();
			Vec3 vec = v * this.MoveSpeed * this.MoveDirection;
			float num = vec.Length * this.MoveDirection;
			if (this.CurrentDistance + num <= -this.MoveDistance)
			{
				this.MoveDirection = 1f;
				num *= -1f;
				vec *= -1f;
			}
			else if (this.CurrentDistance + num >= this.MoveDistance)
			{
				this.MoveDirection = -1f;
				num *= -1f;
				vec *= -1f;
			}
			this.CurrentDistance += num;
			MatrixFrame frame = base.GameEntity.GetFrame();
			frame.origin += vec;
			base.GameEntity.SetFrame(ref frame);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000BF76C File Offset: 0x000BD96C
		private void Rotate(float dt)
		{
			MatrixFrame frame = base.GameEntity.GetFrame();
			frame.rotation.RotateAboutUp(this.rotationSpeed * 0.001f * dt);
			base.GameEntity.SetFrame(ref frame);
			this.currentRotation += this.rotationSpeed * 0.001f * dt;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000BF7C7 File Offset: 0x000BD9C7
		private bool isRotationPhaseInsidePhaseBoundries(float currentPhase, float startPhase, float endPhase)
		{
			if (endPhase <= startPhase)
			{
				return currentPhase > startPhase;
			}
			return currentPhase > startPhase && currentPhase < endPhase;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000BF7DC File Offset: 0x000BD9DC
		public static int GetIntegerFromStringEnd(string str)
		{
			string text = "";
			for (int i = str.Length - 1; i > -1; i--)
			{
				char c = str[i];
				if (c < '0' || c > '9')
				{
					break;
				}
				text = c.ToString() + text;
			}
			return Convert.ToInt32(text);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000BF828 File Offset: 0x000BDA28
		private void DoWaterMillCalculation()
		{
			float num = (float)base.GameEntity.ChildCount;
			if (num > 0f)
			{
				IEnumerable<GameEntity> children = base.GameEntity.GetChildren();
				float num2 = 6.28f / num;
				foreach (GameEntity gameEntity in children)
				{
					int integerFromStringEnd = TestScript.GetIntegerFromStringEnd(gameEntity.Name);
					float currentPhase = this.currentRotation % 6.28f;
					float num3 = (num2 * (float)integerFromStringEnd + this.waterSplashPhaseOffset) % 6.28f;
					float endPhase = (num3 + num2 * this.waterSplashIntervalMultiplier) % 6.28f;
					if (this.isRotationPhaseInsidePhaseBoundries(currentPhase, num3, endPhase))
					{
						gameEntity.ResumeParticleSystem(true);
					}
					else
					{
						gameEntity.PauseParticleSystem(true);
					}
				}
			}
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000BF8F4 File Offset: 0x000BDAF4
		protected internal override void OnInit()
		{
			base.OnInit();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000BF908 File Offset: 0x000BDB08
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000BF912 File Offset: 0x000BDB12
		protected internal override void OnTick(float dt)
		{
			this.Rotate(dt);
			this.Move(dt);
			if (this.isWaterMill)
			{
				this.DoWaterMillCalculation();
			}
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000BF930 File Offset: 0x000BDB30
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.Rotate(dt);
			this.Move(dt);
			if (this.isWaterMill)
			{
				this.DoWaterMillCalculation();
			}
			if (this.sideRotatingEntity != null)
			{
				MatrixFrame frame = this.sideRotatingEntity.GetFrame();
				frame.rotation.RotateAboutSide(this.rotationSpeed * 0.01f * dt);
				this.sideRotatingEntity.SetFrame(ref frame);
			}
			if (this.forwardRotatingEntity != null)
			{
				MatrixFrame frame2 = this.forwardRotatingEntity.GetFrame();
				frame2.rotation.RotateAboutSide(this.rotationSpeed * 0.005f * dt);
				this.forwardRotatingEntity.SetFrame(ref frame2);
			}
		}

		// Token: 0x040013BC RID: 5052
		public string testString;

		// Token: 0x040013BD RID: 5053
		public float rotationSpeed;

		// Token: 0x040013BE RID: 5054
		public float waterSplashPhaseOffset;

		// Token: 0x040013BF RID: 5055
		public float waterSplashIntervalMultiplier = 1f;

		// Token: 0x040013C0 RID: 5056
		public bool isWaterMill;

		// Token: 0x040013C1 RID: 5057
		private float currentRotation;

		// Token: 0x040013C2 RID: 5058
		public float MoveAxisX = 1f;

		// Token: 0x040013C3 RID: 5059
		public float MoveAxisY;

		// Token: 0x040013C4 RID: 5060
		public float MoveAxisZ;

		// Token: 0x040013C5 RID: 5061
		public float MoveSpeed = 0.0001f;

		// Token: 0x040013C6 RID: 5062
		public float MoveDistance = 10f;

		// Token: 0x040013C7 RID: 5063
		protected float MoveDirection = 1f;

		// Token: 0x040013C8 RID: 5064
		protected float CurrentDistance;

		// Token: 0x040013C9 RID: 5065
		public GameEntity sideRotatingEntity;

		// Token: 0x040013CA RID: 5066
		public GameEntity forwardRotatingEntity;
	}
}
