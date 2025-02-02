using System;
using System.Linq.Expressions;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200032A RID: 810
	public class RoadStart : ScriptComponentBehavior
	{
		// Token: 0x06002B50 RID: 11088 RVA: 0x000A7A7A File Offset: 0x000A5C7A
		protected internal override void OnInit()
		{
			this.pathEntity = GameEntity.CreateEmpty(base.Scene, false);
			this.pathEntity.Name = "Road_Entity";
			this.UpdatePathMesh();
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000A7AA4 File Offset: 0x000A5CA4
		protected internal override void OnEditorInit()
		{
			this.OnInit();
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000A7AAC File Offset: 0x000A5CAC
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			if (this.pathEntity != null)
			{
				this.pathEntity.Remove(removeReason);
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000A7ACF File Offset: 0x000A5CCF
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (base.Scene.IsEntityFrameChanged(base.GameEntity.Name))
			{
				this.UpdatePathMesh();
			}
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000A7AF8 File Offset: 0x000A5CF8
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == MBGlobals.GetMemberName<string>(Expression.Lambda<Func<string>>(Expression.Field(Expression.Constant(this, typeof(RoadStart)), fieldof(RoadStart.materialName)), Array.Empty<ParameterExpression>())))
			{
				this.UpdatePathMesh();
			}
			if (this.pathMesh != null)
			{
				this.pathMesh.SetVectorArgument2(this.textureSweepX, this.textureSweepY, 0f, 0f);
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000A7B78 File Offset: 0x000A5D78
		private void UpdatePathMesh()
		{
			this.pathEntity.ClearComponents();
			this.pathMesh = MetaMesh.CreateMetaMesh(null);
			Material fromResource = Material.GetFromResource(this.materialName);
			if (fromResource != null)
			{
				this.pathMesh.SetMaterial(fromResource);
			}
			else
			{
				this.pathMesh.SetMaterial(Material.GetDefaultMaterial());
			}
			this.pathEntity.AddMultiMesh(this.pathMesh, true);
			this.pathMesh.SetVectorArgument2(this.textureSweepX, this.textureSweepY, 0f, 0f);
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000A7C02 File Offset: 0x000A5E02
		protected internal override bool MovesEntity()
		{
			return false;
		}

		// Token: 0x040010C8 RID: 4296
		public float textureSweepX;

		// Token: 0x040010C9 RID: 4297
		public float textureSweepY;

		// Token: 0x040010CA RID: 4298
		public string materialName = "blood_decal_terrain_material";

		// Token: 0x040010CB RID: 4299
		private GameEntity pathEntity;

		// Token: 0x040010CC RID: 4300
		private MetaMesh pathMesh;
	}
}
