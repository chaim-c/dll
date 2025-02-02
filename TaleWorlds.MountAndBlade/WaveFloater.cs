using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200036F RID: 879
	public class WaveFloater : ScriptComponentBehavior
	{
		// Token: 0x0600306E RID: 12398 RVA: 0x000C7F9A File Offset: 0x000C619A
		private float ConvertToRadians(float angle)
		{
			return 0.017453292f * angle;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000C7FA3 File Offset: 0x000C61A3
		private void SetMatrix()
		{
			this.resetMF = base.GameEntity.GetGlobalFrame();
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000C7FB6 File Offset: 0x000C61B6
		private void ResetMatrix()
		{
			base.GameEntity.SetGlobalFrame(this.resetMF);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000C7FCC File Offset: 0x000C61CC
		private void CalculateAxis()
		{
			this.axis = new Vec3(Convert.ToSingle(this.oscillateAtX), Convert.ToSingle(this.oscillateAtY), Convert.ToSingle(this.oscillateAtZ), -1f);
			base.GameEntity.GetGlobalFrame().TransformToParent(this.axis);
			this.axis.Normalize();
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000C8030 File Offset: 0x000C6230
		private float CalculateSpeed(float fq, float maxVal, bool angular)
		{
			if (!angular)
			{
				return maxVal * 2f * fq * 1f;
			}
			return maxVal * 3.1415927f / 90f * fq * 1f;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000C805C File Offset: 0x000C625C
		private void CalculateOscilations()
		{
			this.ResetMatrix();
			this.oscillationStart = base.GameEntity.GetGlobalFrame();
			this.oscillationEnd = base.GameEntity.GetGlobalFrame();
			this.oscillationStart.rotation.RotateAboutAnArbitraryVector(this.axis, -this.ConvertToRadians(this.maxOscillationAngle));
			this.oscillationEnd.rotation.RotateAboutAnArbitraryVector(this.axis, this.ConvertToRadians(this.maxOscillationAngle));
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000C80D8 File Offset: 0x000C62D8
		private void Oscillate()
		{
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			globalFrame.rotation = Mat3.Lerp(this.oscillationStart.rotation, this.oscillationEnd.rotation, MathF.Clamp(this.oscillationPercentage, 0f, 1f));
			base.GameEntity.SetGlobalFrame(globalFrame);
			this.oscillationPercentage = (1f + MathF.Cos(this.oscillationSpeed * 1f * this.et)) / 2f;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000C8160 File Offset: 0x000C6360
		private void CalculateBounces()
		{
			this.ResetMatrix();
			this.bounceXStart = base.GameEntity.GetGlobalFrame();
			this.bounceXEnd = base.GameEntity.GetGlobalFrame();
			this.bounceYStart = base.GameEntity.GetGlobalFrame();
			this.bounceYEnd = base.GameEntity.GetGlobalFrame();
			this.bounceZStart = base.GameEntity.GetGlobalFrame();
			this.bounceZEnd = base.GameEntity.GetGlobalFrame();
			this.bounceXStart.origin.x = this.bounceXStart.origin.x + this.maxBounceXDistance;
			this.bounceXEnd.origin.x = this.bounceXEnd.origin.x - this.maxBounceXDistance;
			this.bounceYStart.origin.y = this.bounceYStart.origin.y + this.maxBounceYDistance;
			this.bounceYEnd.origin.y = this.bounceYEnd.origin.y - this.maxBounceYDistance;
			this.bounceZStart.origin.z = this.bounceZStart.origin.z + this.maxBounceZDistance;
			this.bounceZEnd.origin.z = this.bounceZEnd.origin.z - this.maxBounceZDistance;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000C8278 File Offset: 0x000C6478
		private void Bounce()
		{
			if (this.bounceX)
			{
				MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
				globalFrame.origin.x = Vec3.Lerp(this.bounceXStart.origin, this.bounceXEnd.origin, MathF.Clamp(this.bounceXPercentage, 0f, 1f)).x;
				base.GameEntity.SetGlobalFrame(globalFrame);
				this.bounceXPercentage = (1f + MathF.Sin(this.bounceXSpeed * 1f * this.et)) / 2f;
			}
			if (this.bounceY)
			{
				MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
				globalFrame.origin.y = Vec3.Lerp(this.bounceYStart.origin, this.bounceYEnd.origin, MathF.Clamp(this.bounceYPercentage, 0f, 1f)).y;
				base.GameEntity.SetGlobalFrame(globalFrame);
				this.bounceYPercentage = (1f + MathF.Cos(this.bounceYSpeed * 1f * this.et)) / 2f;
			}
			if (this.bounceZ)
			{
				MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
				globalFrame.origin.z = Vec3.Lerp(this.bounceZStart.origin, this.bounceZEnd.origin, MathF.Clamp(this.bounceZPercentage, 0f, 1f)).z;
				base.GameEntity.SetGlobalFrame(globalFrame);
				this.bounceZPercentage = (1f + MathF.Cos(this.bounceZSpeed * 1f * this.et)) / 2f;
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000C8434 File Offset: 0x000C6634
		protected internal override void OnInit()
		{
			base.OnInit();
			this.SetMatrix();
			this.oscillate = (this.oscillateAtX || this.oscillateAtY || this.oscillateAtZ);
			this.bounce = (this.bounceX || this.bounceY || this.bounceZ);
			this.oscillationSpeed = this.CalculateSpeed(this.oscillationFrequency, this.maxOscillationAngle, true);
			this.bounceXSpeed = this.CalculateSpeed(this.bounceXFrequency, this.maxBounceXDistance, false);
			this.bounceYSpeed = this.CalculateSpeed(this.bounceYFrequency, this.maxBounceYDistance, false);
			this.bounceZSpeed = this.CalculateSpeed(this.bounceZFrequency, this.maxBounceZDistance, false);
			this.CalculateBounces();
			this.CalculateAxis();
			this.CalculateOscilations();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000C8510 File Offset: 0x000C6710
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.SetMatrix();
			this.oscillate = (this.oscillateAtX || this.oscillateAtY || this.oscillateAtZ);
			this.bounce = (this.bounceX || this.bounceY || this.bounceZ);
			this.oscillationSpeed = this.CalculateSpeed(this.oscillationFrequency, this.maxOscillationAngle, true);
			this.bounceXSpeed = this.CalculateSpeed(this.bounceXFrequency, this.maxBounceXDistance, false);
			this.bounceYSpeed = this.CalculateSpeed(this.bounceYFrequency, this.maxBounceYDistance, false);
			this.bounceZSpeed = this.CalculateSpeed(this.bounceZFrequency, this.maxBounceZDistance, false);
			this.CalculateBounces();
			this.CalculateAxis();
			this.CalculateOscilations();
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000C85DD File Offset: 0x000C67DD
		protected internal override void OnSceneSave(string saveFolder)
		{
			base.OnSceneSave(saveFolder);
			this.ResetMatrix();
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000C85EC File Offset: 0x000C67EC
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.et += dt;
			if (this.oscillate)
			{
				this.Oscillate();
			}
			if (this.bounce)
			{
				this.Bounce();
			}
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x000C861F File Offset: 0x000C681F
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x000C8629 File Offset: 0x000C6829
		protected internal override void OnTick(float dt)
		{
			this.et += dt;
			if (this.oscillate)
			{
				this.Oscillate();
			}
			if (this.bounce)
			{
				this.Bounce();
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000C8658 File Offset: 0x000C6858
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "largeObject")
			{
				this.ResetMatrix();
				this.oscillateAtX = true;
				this.oscillateAtY = true;
				this.oscillationFrequency = 1.5f;
				this.maxOscillationAngle = 7.4f;
				this.bounceX = true;
				this.bounceXFrequency = 2f;
				this.maxBounceXDistance = 0.1f;
				this.bounceY = true;
				this.bounceYFrequency = 0.2f;
				this.maxBounceYDistance = 0.5f;
				this.bounceZ = true;
				this.bounceZFrequency = 0.6f;
				this.maxBounceZDistance = 0.22f;
				this.CalculateAxis();
				this.oscillationSpeed = this.CalculateSpeed(this.oscillationFrequency, this.maxOscillationAngle, true);
				this.bounceXSpeed = this.CalculateSpeed(this.bounceXFrequency, this.maxBounceXDistance, false);
				this.bounceYSpeed = this.CalculateSpeed(this.bounceYFrequency, this.maxBounceYDistance, false);
				this.bounceZSpeed = this.CalculateSpeed(this.bounceZFrequency, this.maxBounceZDistance, false);
				this.CalculateOscilations();
				this.CalculateOscilations();
				this.oscillate = true;
				this.bounce = true;
				return;
			}
			if (variableName == "smallObject")
			{
				this.ResetMatrix();
				this.oscillateAtX = true;
				this.oscillateAtY = true;
				this.oscillateAtZ = true;
				this.oscillationFrequency = 1f;
				this.maxOscillationAngle = 11f;
				this.bounceX = true;
				this.bounceXFrequency = 1.5f;
				this.maxBounceXDistance = 0.3f;
				this.bounceY = true;
				this.bounceYFrequency = 1.5f;
				this.maxBounceYDistance = 0.2f;
				this.bounceZ = true;
				this.bounceZFrequency = 1f;
				this.maxBounceZDistance = 0.1f;
				this.CalculateAxis();
				this.oscillationSpeed = this.CalculateSpeed(this.oscillationFrequency, this.maxOscillationAngle, true);
				this.bounceXSpeed = this.CalculateSpeed(this.bounceXFrequency, this.maxBounceXDistance, false);
				this.bounceYSpeed = this.CalculateSpeed(this.bounceYFrequency, this.maxBounceYDistance, false);
				this.bounceZSpeed = this.CalculateSpeed(this.bounceZFrequency, this.maxBounceZDistance, false);
				this.CalculateOscilations();
				this.CalculateOscilations();
				this.oscillate = true;
				this.bounce = true;
				return;
			}
			if (variableName == "oscillateAtX" || variableName == "oscillateAtY" || variableName == "oscillateAtZ")
			{
				if (this.oscillateAtX || this.oscillateAtY || this.oscillateAtZ)
				{
					if (!this.oscillate)
					{
						if (!this.bounce)
						{
							this.SetMatrix();
						}
						this.oscillate = true;
					}
				}
				else
				{
					this.oscillate = false;
					if (!this.bounce)
					{
						this.ResetMatrix();
					}
				}
				this.CalculateAxis();
				this.CalculateOscilations();
				return;
			}
			if (variableName == "oscillationFrequency")
			{
				this.oscillationSpeed = this.CalculateSpeed(this.oscillationFrequency, this.maxOscillationAngle, true);
				return;
			}
			if (variableName == "maxOscillationAngle")
			{
				this.maxOscillationAngle = MathF.Clamp(this.maxOscillationAngle, 0f, 90f);
				this.oscillationSpeed = this.CalculateSpeed(this.oscillationFrequency, this.maxOscillationAngle, true);
				this.CalculateOscilations();
				return;
			}
			if (variableName == "bounceX" || variableName == "bounceY" || variableName == "bounceZ")
			{
				if (this.bounceX || this.bounceY || this.bounceZ)
				{
					if (!this.bounce)
					{
						if (!this.oscillate)
						{
							this.SetMatrix();
						}
						this.bounce = true;
					}
				}
				else
				{
					this.bounce = false;
					if (!this.oscillate)
					{
						this.ResetMatrix();
					}
				}
				this.CalculateBounces();
				return;
			}
			if (variableName == "bounceXFrequency")
			{
				this.bounceXSpeed = this.CalculateSpeed(this.bounceXFrequency, this.maxBounceXDistance, false);
				return;
			}
			if (variableName == "bounceYFrequency")
			{
				this.bounceYSpeed = this.CalculateSpeed(this.bounceYFrequency, this.maxBounceYDistance, false);
				return;
			}
			if (variableName == "bounceZFrequency")
			{
				this.bounceZSpeed = this.CalculateSpeed(this.bounceZFrequency, this.maxBounceZDistance, false);
				return;
			}
			if (variableName == "maxBounceXDistance")
			{
				this.bounceXSpeed = this.CalculateSpeed(this.bounceXFrequency, this.maxBounceXDistance, false);
				this.CalculateBounces();
				return;
			}
			if (variableName == "maxBounceYDistance")
			{
				this.bounceYSpeed = this.CalculateSpeed(this.bounceYFrequency, this.maxBounceYDistance, false);
				this.CalculateBounces();
				return;
			}
			if (variableName == "maxBounceZDistance")
			{
				this.bounceZSpeed = this.CalculateSpeed(this.bounceZFrequency, this.maxBounceZDistance, false);
				this.CalculateBounces();
			}
		}

		// Token: 0x0400148B RID: 5259
		public SimpleButton largeObject;

		// Token: 0x0400148C RID: 5260
		public SimpleButton smallObject;

		// Token: 0x0400148D RID: 5261
		public bool oscillateAtX;

		// Token: 0x0400148E RID: 5262
		public bool oscillateAtY;

		// Token: 0x0400148F RID: 5263
		public bool oscillateAtZ;

		// Token: 0x04001490 RID: 5264
		public float oscillationFrequency = 1f;

		// Token: 0x04001491 RID: 5265
		public float maxOscillationAngle = 10f;

		// Token: 0x04001492 RID: 5266
		public bool bounceX;

		// Token: 0x04001493 RID: 5267
		public float bounceXFrequency = 14f;

		// Token: 0x04001494 RID: 5268
		public float maxBounceXDistance = 0.3f;

		// Token: 0x04001495 RID: 5269
		public bool bounceY;

		// Token: 0x04001496 RID: 5270
		public float bounceYFrequency = 14f;

		// Token: 0x04001497 RID: 5271
		public float maxBounceYDistance = 0.3f;

		// Token: 0x04001498 RID: 5272
		public bool bounceZ;

		// Token: 0x04001499 RID: 5273
		public float bounceZFrequency = 14f;

		// Token: 0x0400149A RID: 5274
		public float maxBounceZDistance = 0.3f;

		// Token: 0x0400149B RID: 5275
		private Vec3 axis;

		// Token: 0x0400149C RID: 5276
		private float oscillationSpeed = 1f;

		// Token: 0x0400149D RID: 5277
		private float oscillationPercentage = 0.5f;

		// Token: 0x0400149E RID: 5278
		private MatrixFrame resetMF;

		// Token: 0x0400149F RID: 5279
		private MatrixFrame oscillationStart;

		// Token: 0x040014A0 RID: 5280
		private MatrixFrame oscillationEnd;

		// Token: 0x040014A1 RID: 5281
		private bool oscillate;

		// Token: 0x040014A2 RID: 5282
		private float bounceXSpeed = 1f;

		// Token: 0x040014A3 RID: 5283
		private float bounceXPercentage = 0.5f;

		// Token: 0x040014A4 RID: 5284
		private MatrixFrame bounceXStart;

		// Token: 0x040014A5 RID: 5285
		private MatrixFrame bounceXEnd;

		// Token: 0x040014A6 RID: 5286
		private float bounceYSpeed = 1f;

		// Token: 0x040014A7 RID: 5287
		private float bounceYPercentage = 0.5f;

		// Token: 0x040014A8 RID: 5288
		private MatrixFrame bounceYStart;

		// Token: 0x040014A9 RID: 5289
		private MatrixFrame bounceYEnd;

		// Token: 0x040014AA RID: 5290
		private float bounceZSpeed = 1f;

		// Token: 0x040014AB RID: 5291
		private float bounceZPercentage = 0.5f;

		// Token: 0x040014AC RID: 5292
		private MatrixFrame bounceZStart;

		// Token: 0x040014AD RID: 5293
		private MatrixFrame bounceZEnd;

		// Token: 0x040014AE RID: 5294
		private bool bounce;

		// Token: 0x040014AF RID: 5295
		private float et;

		// Token: 0x040014B0 RID: 5296
		private const float SPEED_MODIFIER = 1f;
	}
}
