using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000324 RID: 804
	public class MapAtmosphereProbe : ScriptComponentBehavior
	{
		// Token: 0x06002B26 RID: 11046 RVA: 0x000A70A0 File Offset: 0x000A52A0
		public float GetInfluenceAmount(Vec3 worldPosition)
		{
			return MBMath.SmoothStep(this.minRadius, this.maxRadius, worldPosition.Distance(base.GameEntity.GetGlobalFrame().origin));
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000A70CC File Offset: 0x000A52CC
		public MapAtmosphereProbe()
		{
			this.hideAllProbes = MapAtmosphereProbe.hideAllProbesStatic;
			if (MBEditor.IsEditModeOn)
			{
				this.innerSphereMesh = MetaMesh.GetCopy("physics_sphere_detailed", true, false);
				this.outerSphereMesh = MetaMesh.GetCopy("physics_sphere_detailed", true, false);
				this.innerSphereMesh.SetMaterial(Material.GetFromResource("light_radius_visualizer"));
				this.outerSphereMesh.SetMaterial(Material.GetFromResource("light_radius_visualizer"));
			}
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000A7164 File Offset: 0x000A5364
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (this.visualizeRadius && !MapAtmosphereProbe.hideAllProbesStatic)
			{
				uint num = 16711680U;
				uint num2 = 720640U;
				if (MBEditor.IsEntitySelected(base.GameEntity))
				{
					num |= 2147483648U;
					num2 |= 2147483648U;
				}
				else
				{
					num |= 1073741824U;
					num2 |= 1073741824U;
				}
				this.innerSphereMesh.SetFactor1(num);
				this.outerSphereMesh.SetFactor1(num2);
				MatrixFrame frame;
				frame.origin = base.GameEntity.GetGlobalFrame().origin;
				frame.rotation = Mat3.Identity;
				frame.rotation.ApplyScaleLocal(this.minRadius);
				MatrixFrame frame2;
				frame2.origin = base.GameEntity.GetGlobalFrame().origin;
				frame2.rotation = Mat3.Identity;
				frame2.rotation.ApplyScaleLocal(this.maxRadius);
				this.innerSphereMesh.SetVectorArgument(this.minRadius, this.maxRadius, 0f, 0f);
				this.outerSphereMesh.SetVectorArgument(this.minRadius, this.maxRadius, 0f, 0f);
				MBEditor.RenderEditorMesh(this.innerSphereMesh, frame);
				MBEditor.RenderEditorMesh(this.outerSphereMesh, frame2);
			}
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000A72A4 File Offset: 0x000A54A4
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "minRadius")
			{
				this.minRadius = MBMath.ClampFloat(this.minRadius, 0.1f, this.maxRadius);
			}
			if (variableName == "maxRadius")
			{
				this.maxRadius = MBMath.ClampFloat(this.maxRadius, this.minRadius, float.MaxValue);
			}
			if (variableName == "hideAllProbes")
			{
				MapAtmosphereProbe.hideAllProbesStatic = this.hideAllProbes;
			}
		}

		// Token: 0x040010B1 RID: 4273
		public bool visualizeRadius = true;

		// Token: 0x040010B2 RID: 4274
		public bool hideAllProbes = true;

		// Token: 0x040010B3 RID: 4275
		public static bool hideAllProbesStatic = true;

		// Token: 0x040010B4 RID: 4276
		public float minRadius = 1f;

		// Token: 0x040010B5 RID: 4277
		public float maxRadius = 2f;

		// Token: 0x040010B6 RID: 4278
		public float rainDensity;

		// Token: 0x040010B7 RID: 4279
		public float temperature;

		// Token: 0x040010B8 RID: 4280
		public string atmosphereType;

		// Token: 0x040010B9 RID: 4281
		public string colorGrade;

		// Token: 0x040010BA RID: 4282
		private MetaMesh innerSphereMesh;

		// Token: 0x040010BB RID: 4283
		private MetaMesh outerSphereMesh;
	}
}
