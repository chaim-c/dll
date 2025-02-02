using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200032C RID: 812
	public class ScenePropNegativeLight : ScriptComponentBehavior
	{
		// Token: 0x06002B5F RID: 11103 RVA: 0x000A7DE2 File Offset: 0x000A5FE2
		protected internal override void OnEditorTick(float dt)
		{
			this.SetMeshParameters();
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000A7DEC File Offset: 0x000A5FEC
		private void SetMeshParameters()
		{
			MetaMesh metaMesh = base.GameEntity.GetMetaMesh(0);
			if (metaMesh != null)
			{
				metaMesh.SetVectorArgument(this.Flatness_X, this.Flatness_Y, this.Flatness_Z, this.Alpha);
				if (this.Is_Dark_Light)
				{
					metaMesh.SetVectorArgument2(1f, 0f, 0f, 0f);
					return;
				}
				metaMesh.SetVectorArgument2(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000A7E6A File Offset: 0x000A606A
		protected internal override void OnInit()
		{
			base.OnInit();
			this.SetMeshParameters();
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000A7E78 File Offset: 0x000A6078
		protected internal override bool IsOnlyVisual()
		{
			return true;
		}

		// Token: 0x040010D9 RID: 4313
		public float Flatness_X;

		// Token: 0x040010DA RID: 4314
		public float Flatness_Y;

		// Token: 0x040010DB RID: 4315
		public float Flatness_Z;

		// Token: 0x040010DC RID: 4316
		public float Alpha = 1f;

		// Token: 0x040010DD RID: 4317
		public bool Is_Dark_Light = true;
	}
}
