using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.Engine.GauntletUI
{
	// Token: 0x02000005 RID: 5
	public class TwoDimensionEnginePlatform : ITwoDimensionPlatform
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002868 File Offset: 0x00000A68
		float ITwoDimensionPlatform.Width
		{
			get
			{
				return Screen.RealScreenResolutionWidth * ScreenManager.UsableArea.X;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002888 File Offset: 0x00000A88
		float ITwoDimensionPlatform.Height
		{
			get
			{
				return Screen.RealScreenResolutionHeight * ScreenManager.UsableArea.Y;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000028A8 File Offset: 0x00000AA8
		float ITwoDimensionPlatform.ReferenceWidth
		{
			get
			{
				return 1920f;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000028AF File Offset: 0x00000AAF
		float ITwoDimensionPlatform.ReferenceHeight
		{
			get
			{
				return 1080f;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000028B6 File Offset: 0x00000AB6
		float ITwoDimensionPlatform.ApplicationTime
		{
			get
			{
				return Time.ApplicationTime;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028BD File Offset: 0x00000ABD
		public TwoDimensionEnginePlatform(TwoDimensionView view)
		{
			this._view = view;
			this._scissorSet = false;
			this._materials = new Dictionary<TwoDimensionEnginePlatform.MaterialTuple, Material>();
			this._textMaterials = new Dictionary<Texture, Material>();
			this._soundEvents = new Dictionary<string, SoundEvent>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028F4 File Offset: 0x00000AF4
		private Material GetOrCreateMaterial(Texture texture, Texture overlayTexture, bool useCustomMesh, bool useOverlayTextureAlphaAsMask)
		{
			TwoDimensionEnginePlatform.MaterialTuple key = new TwoDimensionEnginePlatform.MaterialTuple(texture, overlayTexture, useCustomMesh);
			Material result;
			if (this._materials.TryGetValue(key, out result))
			{
				return result;
			}
			Material material = Material.GetFromResource("two_dimension_simple_material").CreateCopy();
			material.SetTexture(Material.MBTextureType.DiffuseMap, texture);
			if (overlayTexture != null)
			{
				material.AddMaterialShaderFlag("use_overlay_texture", true);
				if (useOverlayTextureAlphaAsMask)
				{
					material.AddMaterialShaderFlag("use_overlay_texture_alpha_as_mask", true);
				}
				material.SetTexture(Material.MBTextureType.DiffuseMap2, overlayTexture);
			}
			if (useCustomMesh)
			{
				material.AddMaterialShaderFlag("use_custom_mesh", true);
			}
			this._materials.Add(key, material);
			return material;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002980 File Offset: 0x00000B80
		private Material GetOrCreateTextMaterial(Texture texture)
		{
			Material result;
			if (this._textMaterials.TryGetValue(texture, out result))
			{
				return result;
			}
			Material material = Material.GetFromResource("two_dimension_text_material").CreateCopy();
			material.SetTexture(Material.MBTextureType.DiffuseMap, texture);
			this._textMaterials.Add(texture, material);
			return material;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029C8 File Offset: 0x00000BC8
		void ITwoDimensionPlatform.Draw(float x, float y, Material material, DrawObject2D mesh, int layer)
		{
			Vec2 clipRectPosition = new Vec2(0f, 0f);
			Vec2 clipRectSize = new Vec2(Screen.RealScreenResolutionWidth, Screen.RealScreenResolutionHeight);
			if (this._scissorSet)
			{
				clipRectPosition = new Vec2((float)this._currentScissorTestInfo.X, (float)this._currentScissorTestInfo.Y);
				clipRectSize = new Vec2((float)this._currentScissorTestInfo.Width, (float)this._currentScissorTestInfo.Height);
			}
			if (material is SimpleMaterial)
			{
				SimpleMaterial simpleMaterial = (SimpleMaterial)material;
				Texture texture = simpleMaterial.Texture;
				if (texture != null)
				{
					Texture texture2 = ((EngineTexture)texture.PlatformTexture).Texture;
					if (texture2 != null)
					{
						Material material2 = null;
						Vec2 startCoordinate = Vec2.Zero;
						Vec2 size = Vec2.Zero;
						float overlayTextureWidth = 512f;
						float overlayTextureHeight = 512f;
						float overlayXOffset = 0f;
						float overlayYOffset = 0f;
						if (simpleMaterial.OverlayEnabled)
						{
							Texture texture3 = ((EngineTexture)simpleMaterial.OverlayTexture.PlatformTexture).Texture;
							material2 = this.GetOrCreateMaterial(texture2, texture3, mesh.DrawObjectType == DrawObjectType.Mesh, simpleMaterial.UseOverlayAlphaAsMask);
							startCoordinate = simpleMaterial.StartCoordinate;
							size = simpleMaterial.Size;
							overlayTextureWidth = simpleMaterial.OverlayTextureWidth;
							overlayTextureHeight = simpleMaterial.OverlayTextureHeight;
							overlayXOffset = simpleMaterial.OverlayXOffset;
							overlayYOffset = simpleMaterial.OverlayYOffset;
						}
						if (material2 == null)
						{
							material2 = this.GetOrCreateMaterial(texture2, null, mesh.DrawObjectType == DrawObjectType.Mesh, false);
						}
						uint color = simpleMaterial.Color.ToUnsignedInteger();
						float colorFactor = simpleMaterial.ColorFactor;
						float alphaFactor = simpleMaterial.AlphaFactor;
						float hueFactor = simpleMaterial.HueFactor;
						float saturationFactor = simpleMaterial.SaturationFactor;
						float valueFactor = simpleMaterial.ValueFactor;
						Vec2 clipCircleCenter = Vec2.Zero;
						float clipCircleRadius = 0f;
						float clipCircleSmoothingRadius = 0f;
						if (simpleMaterial.CircularMaskingEnabled)
						{
							clipCircleCenter = simpleMaterial.CircularMaskingCenter;
							clipCircleRadius = simpleMaterial.CircularMaskingRadius;
							clipCircleSmoothingRadius = simpleMaterial.CircularMaskingSmoothingRadius;
						}
						float[] vertices = mesh.Vertices;
						float[] textureCoordinates = mesh.TextureCoordinates;
						uint[] indices = mesh.Indices;
						int vertexCount = mesh.VertexCount;
						TwoDimensionMeshDrawData meshDrawData = default(TwoDimensionMeshDrawData);
						meshDrawData.ScreenWidth = Screen.RealScreenResolutionWidth;
						meshDrawData.ScreenHeight = Screen.RealScreenResolutionHeight;
						meshDrawData.DrawX = x;
						meshDrawData.DrawY = y;
						meshDrawData.ClipRectPosition = clipRectPosition;
						meshDrawData.ClipRectSize = clipRectSize;
						meshDrawData.Layer = layer;
						meshDrawData.Width = mesh.Width;
						meshDrawData.Height = mesh.Height;
						meshDrawData.MinU = mesh.MinU;
						meshDrawData.MinV = mesh.MinV;
						meshDrawData.MaxU = mesh.MaxU;
						meshDrawData.MaxV = mesh.MaxV;
						meshDrawData.ClipCircleCenter = clipCircleCenter;
						meshDrawData.ClipCircleRadius = clipCircleRadius;
						meshDrawData.ClipCircleSmoothingRadius = clipCircleSmoothingRadius;
						meshDrawData.Color = color;
						meshDrawData.ColorFactor = colorFactor;
						meshDrawData.AlphaFactor = alphaFactor;
						meshDrawData.HueFactor = hueFactor;
						meshDrawData.SaturationFactor = saturationFactor;
						meshDrawData.ValueFactor = valueFactor;
						meshDrawData.OverlayTextureWidth = overlayTextureWidth;
						meshDrawData.OverlayTextureHeight = overlayTextureHeight;
						meshDrawData.OverlayXOffset = overlayXOffset;
						meshDrawData.OverlayYOffset = overlayYOffset;
						meshDrawData.StartCoordinate = startCoordinate;
						meshDrawData.Size = size;
						meshDrawData.Type = (uint)mesh.DrawObjectType;
						if (!MBDebug.DisableAllUI)
						{
							if (mesh.DrawObjectType == DrawObjectType.Quad)
							{
								this._view.CreateMeshFromDescription(material2, meshDrawData);
								return;
							}
							this._view.CreateMeshFromDescription(vertices, textureCoordinates, indices, vertexCount, material2, meshDrawData);
							return;
						}
					}
				}
			}
			else if (material is TextMaterial)
			{
				TextMaterial textMaterial = (TextMaterial)material;
				uint color2 = textMaterial.Color.ToUnsignedInteger();
				Texture texture4 = textMaterial.Texture;
				if (texture4 != null)
				{
					Texture texture5 = ((EngineTexture)texture4.PlatformTexture).Texture;
					if (texture5 != null)
					{
						Material orCreateTextMaterial = this.GetOrCreateTextMaterial(texture5);
						TwoDimensionTextMeshDrawData meshDrawData2 = default(TwoDimensionTextMeshDrawData);
						meshDrawData2.DrawX = x;
						meshDrawData2.DrawY = y;
						meshDrawData2.ScreenWidth = Screen.RealScreenResolutionWidth;
						meshDrawData2.ScreenHeight = Screen.RealScreenResolutionHeight;
						meshDrawData2.Color = color2;
						meshDrawData2.ScaleFactor = 1.5f / textMaterial.ScaleFactor;
						meshDrawData2.SmoothingConstant = textMaterial.SmoothingConstant;
						meshDrawData2.GlowColor = textMaterial.GlowColor.ToUnsignedInteger();
						meshDrawData2.OutlineColor = textMaterial.OutlineColor.ToVec3();
						meshDrawData2.OutlineAmount = textMaterial.OutlineAmount;
						meshDrawData2.GlowRadius = textMaterial.GlowRadius;
						meshDrawData2.Blur = textMaterial.Blur;
						meshDrawData2.ShadowOffset = textMaterial.ShadowOffset;
						meshDrawData2.ShadowAngle = textMaterial.ShadowAngle;
						meshDrawData2.ColorFactor = textMaterial.ColorFactor;
						meshDrawData2.AlphaFactor = textMaterial.AlphaFactor;
						meshDrawData2.HueFactor = textMaterial.HueFactor;
						meshDrawData2.SaturationFactor = textMaterial.SaturationFactor;
						meshDrawData2.ValueFactor = textMaterial.ValueFactor;
						meshDrawData2.Layer = layer;
						meshDrawData2.ClipRectPosition = clipRectPosition;
						meshDrawData2.ClipRectSize = clipRectSize;
						meshDrawData2.HashCode1 = mesh.HashCode1;
						meshDrawData2.HashCode2 = mesh.HashCode2;
						if (!MBDebug.DisableAllUI && !this._view.CreateTextMeshFromCache(orCreateTextMaterial, meshDrawData2))
						{
							this._view.CreateTextMeshFromDescription(mesh.Vertices, mesh.TextureCoordinates, mesh.Indices, mesh.VertexCount, orCreateTextMaterial, meshDrawData2);
						}
					}
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F2E File Offset: 0x0000112E
		void ITwoDimensionPlatform.SetScissor(ScissorTestInfo scissorTestInfo)
		{
			this._currentScissorTestInfo = scissorTestInfo;
			this._scissorSet = true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002F3E File Offset: 0x0000113E
		void ITwoDimensionPlatform.ResetScissor()
		{
			this._scissorSet = false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F47 File Offset: 0x00001147
		void ITwoDimensionPlatform.PlaySound(string soundName)
		{
			SoundEvent.PlaySound2D("event:/ui/" + soundName);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F5C File Offset: 0x0000115C
		void ITwoDimensionPlatform.CreateSoundEvent(string soundName)
		{
			if (!this._soundEvents.ContainsKey(soundName))
			{
				SoundEvent value = SoundEvent.CreateEventFromString("event:/ui/" + soundName, null);
				this._soundEvents.Add(soundName, value);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F98 File Offset: 0x00001198
		void ITwoDimensionPlatform.PlaySoundEvent(string soundName)
		{
			SoundEvent soundEvent;
			if (this._soundEvents.TryGetValue(soundName, out soundEvent))
			{
				soundEvent.Play();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002FBC File Offset: 0x000011BC
		void ITwoDimensionPlatform.StopAndRemoveSoundEvent(string soundName)
		{
			SoundEvent soundEvent;
			if (this._soundEvents.TryGetValue(soundName, out soundEvent))
			{
				soundEvent.Stop();
				this._soundEvents.Remove(soundName);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FEC File Offset: 0x000011EC
		void ITwoDimensionPlatform.OpenOnScreenKeyboard(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum)
		{
			ScreenManager.OnPlatformScreenKeyboardRequested(initialText, descriptionText, maxLength, keyboardTypeEnum);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002FF8 File Offset: 0x000011F8
		void ITwoDimensionPlatform.BeginDebugPanel(string panelTitle)
		{
			Imgui.BeginMainThreadScope();
			Imgui.Begin(panelTitle);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003005 File Offset: 0x00001205
		void ITwoDimensionPlatform.EndDebugPanel()
		{
			Imgui.End();
			Imgui.EndMainThreadScope();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003011 File Offset: 0x00001211
		void ITwoDimensionPlatform.DrawDebugText(string text)
		{
			Imgui.Text(text);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003019 File Offset: 0x00001219
		bool ITwoDimensionPlatform.DrawDebugTreeNode(string text)
		{
			return Imgui.TreeNode(text);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003021 File Offset: 0x00001221
		void ITwoDimensionPlatform.PopDebugTreeNode()
		{
			Imgui.TreePop();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003028 File Offset: 0x00001228
		void ITwoDimensionPlatform.DrawCheckbox(string label, ref bool isChecked)
		{
			Imgui.Checkbox(label, ref isChecked);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003032 File Offset: 0x00001232
		bool ITwoDimensionPlatform.IsDebugItemHovered()
		{
			return Imgui.IsItemHovered();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003039 File Offset: 0x00001239
		bool ITwoDimensionPlatform.IsDebugModeEnabled()
		{
			return UIConfig.DebugModeEnabled;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003040 File Offset: 0x00001240
		public void Reset()
		{
		}

		// Token: 0x0400000D RID: 13
		private TwoDimensionView _view;

		// Token: 0x0400000E RID: 14
		private ScissorTestInfo _currentScissorTestInfo;

		// Token: 0x0400000F RID: 15
		private bool _scissorSet;

		// Token: 0x04000010 RID: 16
		private Dictionary<TwoDimensionEnginePlatform.MaterialTuple, Material> _materials;

		// Token: 0x04000011 RID: 17
		private Dictionary<Texture, Material> _textMaterials;

		// Token: 0x04000012 RID: 18
		private Dictionary<string, SoundEvent> _soundEvents;

		// Token: 0x0200000B RID: 11
		public struct MaterialTuple : IEquatable<TwoDimensionEnginePlatform.MaterialTuple>
		{
			// Token: 0x06000063 RID: 99 RVA: 0x00003344 File Offset: 0x00001544
			public MaterialTuple(Texture texture, Texture overlayTexture, bool customMesh)
			{
				this.Texture = texture;
				this.OverlayTexture = overlayTexture;
				this.UseCustomMesh = customMesh;
			}

			// Token: 0x06000064 RID: 100 RVA: 0x0000335B File Offset: 0x0000155B
			public bool Equals(TwoDimensionEnginePlatform.MaterialTuple other)
			{
				return other.Texture == this.Texture && other.OverlayTexture == this.OverlayTexture && other.UseCustomMesh == this.UseCustomMesh;
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00003394 File Offset: 0x00001594
			public override int GetHashCode()
			{
				int num = this.Texture.GetHashCode();
				num = ((this.OverlayTexture != null) ? (num * 397 ^ this.OverlayTexture.GetHashCode()) : num);
				return num * 397 ^ this.UseCustomMesh.GetHashCode();
			}

			// Token: 0x04000020 RID: 32
			public Texture Texture;

			// Token: 0x04000021 RID: 33
			public Texture OverlayTexture;

			// Token: 0x04000022 RID: 34
			public bool UseCustomMesh;
		}
	}
}
