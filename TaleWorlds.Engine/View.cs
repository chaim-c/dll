using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000096 RID: 150
	[EngineClass("rglView")]
	public abstract class View : NativeObject
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		internal View(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0000CBB5 File Offset: 0x0000ADB5
		public void SetScale(Vec2 scale)
		{
			EngineApplicationInterface.IView.SetScale(base.Pointer, scale.x, scale.y);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0000CBD3 File Offset: 0x0000ADD3
		public void SetOffset(Vec2 offset)
		{
			EngineApplicationInterface.IView.SetOffset(base.Pointer, offset.x, offset.y);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		public void SetRenderOrder(int value)
		{
			EngineApplicationInterface.IView.SetRenderOrder(base.Pointer, value);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0000CC04 File Offset: 0x0000AE04
		public void SetRenderOption(View.ViewRenderOptions optionEnum, bool value)
		{
			EngineApplicationInterface.IView.SetRenderOption(base.Pointer, (int)optionEnum, value);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public void SetRenderTarget(Texture texture)
		{
			EngineApplicationInterface.IView.SetRenderTarget(base.Pointer, texture.Pointer);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0000CC30 File Offset: 0x0000AE30
		public void SetDepthTarget(Texture texture)
		{
			EngineApplicationInterface.IView.SetDepthTarget(base.Pointer, texture.Pointer);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public void DontClearBackground()
		{
			this.SetRenderOption(View.ViewRenderOptions.ClearColor, false);
			this.SetRenderOption(View.ViewRenderOptions.ClearDepth, false);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0000CC5A File Offset: 0x0000AE5A
		public void SetClearColor(uint rgba)
		{
			EngineApplicationInterface.IView.SetClearColor(base.Pointer, rgba);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0000CC6D File Offset: 0x0000AE6D
		public void SetEnable(bool value)
		{
			EngineApplicationInterface.IView.SetEnable(base.Pointer, value);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public void SetRenderOnDemand(bool value)
		{
			EngineApplicationInterface.IView.SetRenderOnDemand(base.Pointer, value);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0000CC93 File Offset: 0x0000AE93
		public void SetAutoDepthTargetCreation(bool value)
		{
			EngineApplicationInterface.IView.SetAutoDepthTargetCreation(base.Pointer, value);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0000CCA6 File Offset: 0x0000AEA6
		public void SetSaveFinalResultToDisk(bool value)
		{
			EngineApplicationInterface.IView.SetSaveFinalResultToDisk(base.Pointer, value);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0000CCB9 File Offset: 0x0000AEB9
		public void SetFileNameToSaveResult(string name)
		{
			EngineApplicationInterface.IView.SetFileNameToSaveResult(base.Pointer, name);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0000CCCC File Offset: 0x0000AECC
		public void SetFileTypeToSave(View.TextureSaveFormat format)
		{
			EngineApplicationInterface.IView.SetFileTypeToSave(base.Pointer, (int)format);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0000CCDF File Offset: 0x0000AEDF
		public void SetFilePathToSaveResult(string name)
		{
			EngineApplicationInterface.IView.SetFilePathToSaveResult(base.Pointer, name);
		}

		// Token: 0x020000CC RID: 204
		public enum TextureSaveFormat
		{
			// Token: 0x0400043F RID: 1087
			TextureTypeUnknown,
			// Token: 0x04000440 RID: 1088
			TextureTypeBmp,
			// Token: 0x04000441 RID: 1089
			TextureTypeJpg,
			// Token: 0x04000442 RID: 1090
			TextureTypePng,
			// Token: 0x04000443 RID: 1091
			TextureTypeDds,
			// Token: 0x04000444 RID: 1092
			TextureTypeTif,
			// Token: 0x04000445 RID: 1093
			TextureTypePsd,
			// Token: 0x04000446 RID: 1094
			TextureTypeRaw
		}

		// Token: 0x020000CD RID: 205
		public enum PostfxConfig : uint
		{
			// Token: 0x04000448 RID: 1096
			pfx_config_bloom = 1U,
			// Token: 0x04000449 RID: 1097
			pfx_config_sunshafts,
			// Token: 0x0400044A RID: 1098
			pfx_config_motionblur = 4U,
			// Token: 0x0400044B RID: 1099
			pfx_config_dof = 8U,
			// Token: 0x0400044C RID: 1100
			pfx_config_tsao = 16U,
			// Token: 0x0400044D RID: 1101
			pfx_config_fxaa = 64U,
			// Token: 0x0400044E RID: 1102
			pfx_config_smaa = 128U,
			// Token: 0x0400044F RID: 1103
			pfx_config_temporal_smaa = 256U,
			// Token: 0x04000450 RID: 1104
			pfx_config_temporal_resolve = 512U,
			// Token: 0x04000451 RID: 1105
			pfx_config_temporal_filter = 1024U,
			// Token: 0x04000452 RID: 1106
			pfx_config_contour = 2048U,
			// Token: 0x04000453 RID: 1107
			pfx_config_ssr = 4096U,
			// Token: 0x04000454 RID: 1108
			pfx_config_sssss = 8192U,
			// Token: 0x04000455 RID: 1109
			pfx_config_streaks = 16384U,
			// Token: 0x04000456 RID: 1110
			pfx_config_lens_flares = 32768U,
			// Token: 0x04000457 RID: 1111
			pfx_config_chromatic_aberration = 65536U,
			// Token: 0x04000458 RID: 1112
			pfx_config_vignette = 131072U,
			// Token: 0x04000459 RID: 1113
			pfx_config_sharpen = 262144U,
			// Token: 0x0400045A RID: 1114
			pfx_config_grain = 524288U,
			// Token: 0x0400045B RID: 1115
			pfx_config_temporal_shadow = 1048576U,
			// Token: 0x0400045C RID: 1116
			pfx_config_editor_scene = 2097152U,
			// Token: 0x0400045D RID: 1117
			pfx_config_custom1 = 16777216U,
			// Token: 0x0400045E RID: 1118
			pfx_config_custom2 = 33554432U,
			// Token: 0x0400045F RID: 1119
			pfx_config_custom3 = 67108864U,
			// Token: 0x04000460 RID: 1120
			pfx_config_custom4 = 134217728U,
			// Token: 0x04000461 RID: 1121
			pfx_config_hexagon_vignette = 268435456U,
			// Token: 0x04000462 RID: 1122
			pfx_config_screen_rt_injection = 536870912U,
			// Token: 0x04000463 RID: 1123
			pfx_config_high_dof = 1073741824U,
			// Token: 0x04000464 RID: 1124
			pfx_lower_bound = 1U,
			// Token: 0x04000465 RID: 1125
			pfx_upper_bound = 536870912U
		}

		// Token: 0x020000CE RID: 206
		public enum ViewRenderOptions
		{
			// Token: 0x04000467 RID: 1127
			ClearColor,
			// Token: 0x04000468 RID: 1128
			ClearDepth
		}
	}
}
