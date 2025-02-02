using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200032B RID: 811
	public class ScenePropDecal : ScriptComponentBehavior
	{
		// Token: 0x06002B58 RID: 11096 RVA: 0x000A7C18 File Offset: 0x000A5E18
		protected internal override void OnInit()
		{
			base.OnInit();
			this.SetUpMaterial();
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000A7C26 File Offset: 0x000A5E26
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.SetUpMaterial();
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000A7C34 File Offset: 0x000A5E34
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			this.SetUpMaterial();
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000A7C44 File Offset: 0x000A5E44
		private void EnsureUniqueMaterial()
		{
			Material fromResource = Material.GetFromResource(this.MaterialName);
			this.UniqueMaterial = fromResource.CreateCopy();
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000A7C6C File Offset: 0x000A5E6C
		private void SetUpMaterial()
		{
			this.EnsureUniqueMaterial();
			Texture texture = Texture.CheckAndGetFromResource(this.DiffuseTexture);
			Texture texture2 = Texture.CheckAndGetFromResource(this.NormalTexture);
			Texture texture3 = Texture.CheckAndGetFromResource(this.SpecularTexture);
			Texture texture4 = Texture.CheckAndGetFromResource(this.MaskTexture);
			if (texture != null)
			{
				this.UniqueMaterial.SetTexture(Material.MBTextureType.DiffuseMap, texture);
			}
			if (texture2 != null)
			{
				this.UniqueMaterial.SetTexture(Material.MBTextureType.BumpMap, texture2);
			}
			if (texture3 != null)
			{
				this.UniqueMaterial.SetTexture(Material.MBTextureType.SpecularMap, texture3);
			}
			if (texture4 != null)
			{
				this.UniqueMaterial.SetTexture(Material.MBTextureType.DiffuseMap2, texture4);
				this.UniqueMaterial.AddMaterialShaderFlag("use_areamap", false);
			}
			this.UniqueMaterial.SetAlphaTestValue(this.AlphaTestValue);
			base.GameEntity.SetMaterialForAllMeshes(this.UniqueMaterial);
			Mesh firstMesh = base.GameEntity.GetFirstMesh();
			if (firstMesh != null)
			{
				if (this.UniqueMaterial != null)
				{
					firstMesh.SetVectorArgument(this.TilingSize, this.TilingSize, this.TilingOffset, this.TilingOffset);
				}
				firstMesh.SetVectorArgument2(this.TextureSweepX, this.TextureSweepY, 0f, this.UseBaseNormals ? 1f : 0f);
			}
		}

		// Token: 0x040010CD RID: 4301
		public string DiffuseTexture;

		// Token: 0x040010CE RID: 4302
		public string NormalTexture;

		// Token: 0x040010CF RID: 4303
		public string SpecularTexture;

		// Token: 0x040010D0 RID: 4304
		public string MaskTexture;

		// Token: 0x040010D1 RID: 4305
		public bool UseBaseNormals;

		// Token: 0x040010D2 RID: 4306
		public float TilingSize = 1f;

		// Token: 0x040010D3 RID: 4307
		public float TilingOffset;

		// Token: 0x040010D4 RID: 4308
		public float AlphaTestValue;

		// Token: 0x040010D5 RID: 4309
		public float TextureSweepX;

		// Token: 0x040010D6 RID: 4310
		public float TextureSweepY;

		// Token: 0x040010D7 RID: 4311
		public string MaterialName = "deferred_decal_material";

		// Token: 0x040010D8 RID: 4312
		protected Material UniqueMaterial;
	}
}
