using System;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005B RID: 91
	public sealed class Material : Resource
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00006BEC File Offset: 0x00004DEC
		public static Material GetDefaultMaterial()
		{
			return EngineApplicationInterface.IMaterial.GetDefaultMaterial();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public static Material GetOutlineMaterial(Mesh mesh)
		{
			return EngineApplicationInterface.IMaterial.GetOutlineMaterial(mesh.GetMaterial().Pointer);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00006C0F File Offset: 0x00004E0F
		public static Material GetDefaultTableauSampleMaterial(bool transparency)
		{
			if (!transparency)
			{
				return Material.GetFromResource("sample_shield_matte");
			}
			return Material.GetFromResource("tableau_with_transparency");
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00006C2C File Offset: 0x00004E2C
		public static Material CreateTableauMaterial(RenderTargetComponent.TextureUpdateEventHandler eventHandler, object objectRef, Material sampleMaterial, int tableauSizeX, int tableauSizeY, bool continuousTableau = false)
		{
			if (sampleMaterial == null)
			{
				sampleMaterial = Material.GetDefaultTableauSampleMaterial(true);
			}
			Material material = sampleMaterial.CreateCopy();
			uint num = (uint)material.GetShader().GetMaterialShaderFlagMask("use_tableau_blending", true);
			ulong shaderFlags = material.GetShaderFlags();
			material.SetShaderFlags(shaderFlags | (ulong)num);
			string text = "";
			Type type = objectRef.GetType();
			MaterialCacheIDGetMethodDelegate materialCacheIDGetMethodDelegate;
			if (!continuousTableau && HasTableauCache.TableauCacheTypes.TryGetValue(type, out materialCacheIDGetMethodDelegate))
			{
				text = materialCacheIDGetMethodDelegate(objectRef);
				text = text.ToLower();
				Texture texture = Texture.CheckAndGetFromResource(text);
				if (texture != null)
				{
					material.SetTexture(Material.MBTextureType.DiffuseMap2, texture);
					return material;
				}
			}
			if (text != "")
			{
				Texture.ScaleTextureWithRatio(ref tableauSizeX, ref tableauSizeY);
			}
			Texture texture2 = Texture.CreateTableauTexture(text, eventHandler, objectRef, tableauSizeX, tableauSizeY);
			if (text != "")
			{
				TableauView tableauView = texture2.TableauView;
				tableauView.SetSaveFinalResultToDisk(true);
				tableauView.SetFileNameToSaveResult(text);
				tableauView.SetFileTypeToSave(View.TextureSaveFormat.TextureTypeDds);
			}
			if (text != "")
			{
				texture2.TransformRenderTargetToResource(text);
			}
			material.SetTexture(Material.MBTextureType.DiffuseMap2, texture2);
			return material;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00006D2E File Offset: 0x00004F2E
		internal Material(UIntPtr sourceMaterialPointer) : base(sourceMaterialPointer)
		{
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00006D37 File Offset: 0x00004F37
		public Material CreateCopy()
		{
			return EngineApplicationInterface.IMaterial.CreateCopy(base.Pointer);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00006D49 File Offset: 0x00004F49
		public static Material GetFromResource(string materialName)
		{
			return EngineApplicationInterface.IMaterial.GetFromResource(materialName);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00006D56 File Offset: 0x00004F56
		public void SetShader(Shader shader)
		{
			EngineApplicationInterface.IMaterial.SetShader(base.Pointer, shader.Pointer);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00006D6E File Offset: 0x00004F6E
		public Shader GetShader()
		{
			return EngineApplicationInterface.IMaterial.GetShader(base.Pointer);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00006D80 File Offset: 0x00004F80
		public ulong GetShaderFlags()
		{
			return EngineApplicationInterface.IMaterial.GetShaderFlags(base.Pointer);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00006D92 File Offset: 0x00004F92
		public void SetShaderFlags(ulong flagEntry)
		{
			EngineApplicationInterface.IMaterial.SetShaderFlags(base.Pointer, flagEntry);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public void SetMeshVectorArgument(float x, float y, float z, float w)
		{
			EngineApplicationInterface.IMaterial.SetMeshVectorArgument(base.Pointer, x, y, z, w);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00006DBC File Offset: 0x00004FBC
		public void SetTexture(Material.MBTextureType textureType, Texture texture)
		{
			EngineApplicationInterface.IMaterial.SetTexture(base.Pointer, (int)textureType, texture.Pointer);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00006DD5 File Offset: 0x00004FD5
		public void SetTextureAtSlot(int textureSlot, Texture texture)
		{
			EngineApplicationInterface.IMaterial.SetTextureAtSlot(base.Pointer, textureSlot, texture.Pointer);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00006DEE File Offset: 0x00004FEE
		public void SetAreaMapScale(float scale)
		{
			EngineApplicationInterface.IMaterial.SetAreaMapScale(base.Pointer, scale);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00006E01 File Offset: 0x00005001
		public void SetEnableSkinning(bool enable)
		{
			EngineApplicationInterface.IMaterial.SetEnableSkinning(base.Pointer, enable);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00006E14 File Offset: 0x00005014
		public bool UsingSkinning()
		{
			return EngineApplicationInterface.IMaterial.UsingSkinning(base.Pointer);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00006E26 File Offset: 0x00005026
		public Texture GetTexture(Material.MBTextureType textureType)
		{
			return EngineApplicationInterface.IMaterial.GetTexture(base.Pointer, (int)textureType);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00006E39 File Offset: 0x00005039
		public Texture GetTextureWithSlot(int textureSlot)
		{
			return EngineApplicationInterface.IMaterial.GetTexture(base.Pointer, textureSlot);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00006E4C File Offset: 0x0000504C
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x00006E5E File Offset: 0x0000505E
		public string Name
		{
			get
			{
				return EngineApplicationInterface.IMaterial.GetName(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMaterial.SetName(base.Pointer, value);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00006E71 File Offset: 0x00005071
		public static Material GetAlphaMaskTableauMaterial()
		{
			return EngineApplicationInterface.IMaterial.GetFromResource("tableau_with_alpha_mask");
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00006E82 File Offset: 0x00005082
		public Material.MBAlphaBlendMode GetAlphaBlendMode()
		{
			return (Material.MBAlphaBlendMode)EngineApplicationInterface.IMaterial.GetAlphaBlendMode(base.Pointer);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00006E95 File Offset: 0x00005095
		public void SetAlphaBlendMode(Material.MBAlphaBlendMode alphaBlendMode)
		{
			EngineApplicationInterface.IMaterial.SetAlphaBlendMode(base.Pointer, (int)alphaBlendMode);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00006EA8 File Offset: 0x000050A8
		public void SetAlphaTestValue(float alphaTestValue)
		{
			EngineApplicationInterface.IMaterial.SetAlphaTestValue(base.Pointer, alphaTestValue);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00006EBB File Offset: 0x000050BB
		public float GetAlphaTestValue()
		{
			return EngineApplicationInterface.IMaterial.GetAlphaTestValue(base.Pointer);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00006ECD File Offset: 0x000050CD
		private bool CheckMaterialShaderFlag(Material.MBMaterialShaderFlags flagEntry)
		{
			return (EngineApplicationInterface.IMaterial.GetShaderFlags(base.Pointer) & (ulong)((long)flagEntry)) > 0UL;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00006EE8 File Offset: 0x000050E8
		private void SetMaterialShaderFlag(Material.MBMaterialShaderFlags flagEntry, bool value)
		{
			ulong shaderFlags = (EngineApplicationInterface.IMaterial.GetShaderFlags(base.Pointer) & (ulong)(~(ulong)((long)flagEntry))) | (ulong)((long)flagEntry & (value ? 255L : 0L));
			EngineApplicationInterface.IMaterial.SetShaderFlags(base.Pointer, shaderFlags);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00006F2C File Offset: 0x0000512C
		public void AddMaterialShaderFlag(string flagName, bool showErrors)
		{
			EngineApplicationInterface.IMaterial.AddMaterialShaderFlag(base.Pointer, flagName, showErrors);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00006F40 File Offset: 0x00005140
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x00006F49 File Offset: 0x00005149
		public bool UsingSpecular
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecular);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecular, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00006F53 File Offset: 0x00005153
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00006F5C File Offset: 0x0000515C
		public bool UsingSpecularMap
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularMap);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularMap, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00006F66 File Offset: 0x00005166
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x00006F6F File Offset: 0x0000516F
		public bool UsingEnvironmentMap
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseEnvironmentMap);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseEnvironmentMap, value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00006F79 File Offset: 0x00005179
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x00006F86 File Offset: 0x00005186
		public bool UsingSpecularAlpha
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularAlpha);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSpecularAlpha, value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00006F94 File Offset: 0x00005194
		// (set) Token: 0x06000796 RID: 1942 RVA: 0x00006F9E File Offset: 0x0000519E
		public bool UsingDynamicLight
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDynamicLight);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDynamicLight, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00006FA9 File Offset: 0x000051A9
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x00006FB3 File Offset: 0x000051B3
		public bool UsingSunLight
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSunLight);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseSunLight, value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00006FBE File Offset: 0x000051BE
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00006FCB File Offset: 0x000051CB
		public bool UsingFresnel
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseFresnel);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseFresnel, value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00006FD9 File Offset: 0x000051D9
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x00006FE6 File Offset: 0x000051E6
		public bool IsSunShadowReceiver
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.SunShadowReceiver);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.SunShadowReceiver, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00006FF4 File Offset: 0x000051F4
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00007001 File Offset: 0x00005201
		public bool IsDynamicShadowReceiver
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.DynamicShadowReceiver);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.DynamicShadowReceiver, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x0000700F File Offset: 0x0000520F
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x0000701C File Offset: 0x0000521C
		public bool UsingDiffuseAlphaMap
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDiffuseAlphaMap);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseDiffuseAlphaMap, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000702A File Offset: 0x0000522A
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00007037 File Offset: 0x00005237
		public bool UsingParallaxMapping
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxMapping);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxMapping, value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00007045 File Offset: 0x00005245
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x00007052 File Offset: 0x00005252
		public bool UsingParallaxOcclusion
		{
			get
			{
				return this.CheckMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxOcclusion);
			}
			set
			{
				this.SetMaterialShaderFlag(Material.MBMaterialShaderFlags.UseParallaxOcclusion, value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00007060 File Offset: 0x00005260
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00007072 File Offset: 0x00005272
		public MaterialFlags Flags
		{
			get
			{
				return (MaterialFlags)EngineApplicationInterface.IMaterial.GetFlags(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMaterial.SetFlags(base.Pointer, (uint)value);
			}
		}

		// Token: 0x020000BA RID: 186
		public enum MBTextureType
		{
			// Token: 0x040003C3 RID: 963
			DiffuseMap,
			// Token: 0x040003C4 RID: 964
			DiffuseMap2,
			// Token: 0x040003C5 RID: 965
			BumpMap,
			// Token: 0x040003C6 RID: 966
			EnvironmentMap,
			// Token: 0x040003C7 RID: 967
			SpecularMap
		}

		// Token: 0x020000BB RID: 187
		public enum MBAlphaBlendMode : byte
		{
			// Token: 0x040003C9 RID: 969
			None,
			// Token: 0x040003CA RID: 970
			Modulate,
			// Token: 0x040003CB RID: 971
			AddAlpha,
			// Token: 0x040003CC RID: 972
			Multiply,
			// Token: 0x040003CD RID: 973
			Add,
			// Token: 0x040003CE RID: 974
			Max,
			// Token: 0x040003CF RID: 975
			Factor,
			// Token: 0x040003D0 RID: 976
			AddModulateCombined,
			// Token: 0x040003D1 RID: 977
			NoAlphaBlendNoWrite,
			// Token: 0x040003D2 RID: 978
			ModulateNoWrite,
			// Token: 0x040003D3 RID: 979
			GbufferAlphaBlend,
			// Token: 0x040003D4 RID: 980
			GbufferAlphaBlendwithVtResolve
		}

		// Token: 0x020000BC RID: 188
		[Flags]
		private enum MBMaterialShaderFlags
		{
			// Token: 0x040003D6 RID: 982
			UseSpecular = 1,
			// Token: 0x040003D7 RID: 983
			UseSpecularMap = 2,
			// Token: 0x040003D8 RID: 984
			UseHemisphericalAmbient = 4,
			// Token: 0x040003D9 RID: 985
			UseEnvironmentMap = 8,
			// Token: 0x040003DA RID: 986
			UseDXT5Normal = 16,
			// Token: 0x040003DB RID: 987
			UseDynamicLight = 32,
			// Token: 0x040003DC RID: 988
			UseSunLight = 64,
			// Token: 0x040003DD RID: 989
			UseSpecularAlpha = 128,
			// Token: 0x040003DE RID: 990
			UseFresnel = 256,
			// Token: 0x040003DF RID: 991
			SunShadowReceiver = 512,
			// Token: 0x040003E0 RID: 992
			DynamicShadowReceiver = 1024,
			// Token: 0x040003E1 RID: 993
			UseDiffuseAlphaMap = 2048,
			// Token: 0x040003E2 RID: 994
			UseParallaxMapping = 4096,
			// Token: 0x040003E3 RID: 995
			UseParallaxOcclusion = 8192,
			// Token: 0x040003E4 RID: 996
			UseAlphaTestingBit0 = 16384,
			// Token: 0x040003E5 RID: 997
			UseAlphaTestingBit1 = 32768,
			// Token: 0x040003E6 RID: 998
			UseAreaMap = 65536,
			// Token: 0x040003E7 RID: 999
			UseDetailNormalMap = 131072,
			// Token: 0x040003E8 RID: 1000
			UseGroundSlopeAlpha = 262144,
			// Token: 0x040003E9 RID: 1001
			UseSelfIllumination = 524288,
			// Token: 0x040003EA RID: 1002
			UseColorMapping = 1048576,
			// Token: 0x040003EB RID: 1003
			UseCubicAmbient = 2097152
		}
	}
}
