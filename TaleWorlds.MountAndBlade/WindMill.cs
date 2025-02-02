using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000370 RID: 880
	public class WindMill : ScriptComponentBehavior
	{
		// Token: 0x0600307F RID: 12415 RVA: 0x000C8BC7 File Offset: 0x000C6DC7
		protected internal override void OnInit()
		{
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000C8BD8 File Offset: 0x000C6DD8
		private void Rotate(float dt)
		{
			GameEntity gameEntity = base.GameEntity;
			float num = this.rotationSpeed * 0.001f * dt;
			MatrixFrame frame = gameEntity.GetFrame();
			frame.rotation.RotateAboutForward(num);
			gameEntity.SetFrame(ref frame);
			this.currentRotation += num;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000C8C23 File Offset: 0x000C6E23
		private static bool IsRotationPhaseInsidePhaseBoundaries(float currentPhase, float startPhase, float endPhase)
		{
			if (endPhase <= startPhase)
			{
				return currentPhase > startPhase;
			}
			return currentPhase > startPhase && currentPhase < endPhase;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000C8C38 File Offset: 0x000C6E38
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

		// Token: 0x06003083 RID: 12419 RVA: 0x000C8C84 File Offset: 0x000C6E84
		private void DoWaterMillCalculation()
		{
			float num = (float)base.GameEntity.ChildCount;
			if (num > 0f)
			{
				IEnumerable<GameEntity> children = base.GameEntity.GetChildren();
				float num2 = 6.28f / num;
				foreach (GameEntity gameEntity in children)
				{
					int integerFromStringEnd = WindMill.GetIntegerFromStringEnd(gameEntity.Name);
					float currentPhase = this.currentRotation % 6.28f;
					float num3 = (num2 * (float)integerFromStringEnd + this.waterSplashPhaseOffset) % 6.28f;
					float endPhase = (num3 + num2 * this.waterSplashIntervalMultiplier) % 6.28f;
					if (WindMill.IsRotationPhaseInsidePhaseBoundaries(currentPhase, num3, endPhase))
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

		// Token: 0x06003084 RID: 12420 RVA: 0x000C8D4C File Offset: 0x000C6F4C
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.TickParallel;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000C8D56 File Offset: 0x000C6F56
		protected internal override void OnTickParallel(float dt)
		{
			this.Rotate(dt);
			if (this.isWaterMill)
			{
				this.DoWaterMillCalculation();
			}
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000C8D6D File Offset: 0x000C6F6D
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.Rotate(dt);
			if (this.isWaterMill)
			{
				this.DoWaterMillCalculation();
			}
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000C8D8C File Offset: 0x000C6F8C
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "testMesh")
			{
				if (this.testMesh != null)
				{
					base.GameEntity.AddMultiMesh(this.testMesh, true);
					return;
				}
			}
			else if (variableName == "testTexture")
			{
				if (this.testTexture != null)
				{
					Material material = base.GameEntity.GetFirstMesh().GetMaterial().CreateCopy();
					material.SetTexture(Material.MBTextureType.DiffuseMap, this.testTexture);
					base.GameEntity.SetMaterialForAllMeshes(material);
					return;
				}
			}
			else
			{
				if (variableName == "testEntity")
				{
					this.testEntity != null;
					return;
				}
				if (variableName == "testButton")
				{
					this.rotationSpeed *= 2f;
				}
			}
		}

		// Token: 0x040014B1 RID: 5297
		public float rotationSpeed = 100f;

		// Token: 0x040014B2 RID: 5298
		public float waterSplashPhaseOffset;

		// Token: 0x040014B3 RID: 5299
		public float waterSplashIntervalMultiplier = 1f;

		// Token: 0x040014B4 RID: 5300
		public MetaMesh testMesh;

		// Token: 0x040014B5 RID: 5301
		public Texture testTexture;

		// Token: 0x040014B6 RID: 5302
		public GameEntity testEntity;

		// Token: 0x040014B7 RID: 5303
		public bool isWaterMill;

		// Token: 0x040014B8 RID: 5304
		private float currentRotation;
	}
}
