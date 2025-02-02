using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200032D RID: 813
	public class ScenePropPositiveLight : ScriptComponentBehavior
	{
		// Token: 0x06002B63 RID: 11107 RVA: 0x000A7E7B File Offset: 0x000A607B
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.SetMeshParams();
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000A7E8A File Offset: 0x000A608A
		protected internal override void OnInit()
		{
			base.OnInit();
			this.SetMeshParams();
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000A7E98 File Offset: 0x000A6098
		private void SetMeshParams()
		{
			MetaMesh metaMesh = base.GameEntity.GetMetaMesh(0);
			if (metaMesh != null)
			{
				uint factor1Linear = this.CalculateFactor(new Vec3(this.DirectLightRed, this.DirectLightGreen, this.DirectLightBlue, -1f), this.DirectLightIntensity);
				metaMesh.SetFactor1Linear(factor1Linear);
				uint factor2Linear = this.CalculateFactor(new Vec3(this.AmbientLightRed, this.AmbientLightGreen, this.AmbientLightBlue, -1f), this.AmbientLightIntensity);
				metaMesh.SetFactor2Linear(factor2Linear);
				metaMesh.SetVectorArgument(this.Flatness_X, this.Flatness_Y, this.Flatness_Z, 1f);
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000A7F38 File Offset: 0x000A6138
		private uint CalculateFactor(Vec3 color, float alpha)
		{
			float num = 10f;
			byte maxValue = byte.MaxValue;
			alpha = MathF.Min(MathF.Max(0f, alpha), num);
			return ((uint)(alpha / num * (float)maxValue) << 24) + ((uint)(color.x * (float)maxValue) << 16) + ((uint)(color.y * (float)maxValue) << 8) + (uint)(color.z * (float)maxValue);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000A7F94 File Offset: 0x000A6194
		protected internal override bool IsOnlyVisual()
		{
			return true;
		}

		// Token: 0x040010DE RID: 4318
		public float Flatness_X;

		// Token: 0x040010DF RID: 4319
		public float Flatness_Y;

		// Token: 0x040010E0 RID: 4320
		public float Flatness_Z;

		// Token: 0x040010E1 RID: 4321
		public float DirectLightRed = 1f;

		// Token: 0x040010E2 RID: 4322
		public float DirectLightGreen = 1f;

		// Token: 0x040010E3 RID: 4323
		public float DirectLightBlue = 1f;

		// Token: 0x040010E4 RID: 4324
		public float DirectLightIntensity = 1f;

		// Token: 0x040010E5 RID: 4325
		public float AmbientLightRed;

		// Token: 0x040010E6 RID: 4326
		public float AmbientLightGreen;

		// Token: 0x040010E7 RID: 4327
		public float AmbientLightBlue = 1f;

		// Token: 0x040010E8 RID: 4328
		public float AmbientLightIntensity = 1f;
	}
}
