using System;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005A RID: 90
	[Flags]
	public enum MaterialFlags : uint
	{
		// Token: 0x040000DC RID: 220
		RenderFrontToBack = 1U,
		// Token: 0x040000DD RID: 221
		NoDepthTest = 2U,
		// Token: 0x040000DE RID: 222
		DontDrawToDepthRenderTarget = 4U,
		// Token: 0x040000DF RID: 223
		NoModifyDepthBuffer = 8U,
		// Token: 0x040000E0 RID: 224
		CullFrontFaces = 16U,
		// Token: 0x040000E1 RID: 225
		TwoSided = 32U,
		// Token: 0x040000E2 RID: 226
		AlphaBlendSort = 64U,
		// Token: 0x040000E3 RID: 227
		DontOptimizeMesh = 128U,
		// Token: 0x040000E4 RID: 228
		AlphaBlendNone = 0U,
		// Token: 0x040000E5 RID: 229
		AlphaBlendModulate = 256U,
		// Token: 0x040000E6 RID: 230
		AlphaBlendAdd = 512U,
		// Token: 0x040000E7 RID: 231
		AlphaBlendMultiply = 768U,
		// Token: 0x040000E8 RID: 232
		AlphaBlendFactor = 1792U,
		// Token: 0x040000E9 RID: 233
		AlphaBlendMask = 1792U,
		// Token: 0x040000EA RID: 234
		AlphaBlendBits = 8U,
		// Token: 0x040000EB RID: 235
		BillboardNone = 0U,
		// Token: 0x040000EC RID: 236
		Billboard2d = 4096U,
		// Token: 0x040000ED RID: 237
		Billboard3d = 8192U,
		// Token: 0x040000EE RID: 238
		BillboardMask = 12288U,
		// Token: 0x040000EF RID: 239
		Skybox = 131072U,
		// Token: 0x040000F0 RID: 240
		MultiPassAlpha = 262144U,
		// Token: 0x040000F1 RID: 241
		GbufferAlphaBlend = 524288U,
		// Token: 0x040000F2 RID: 242
		RequiresForwardRendering = 1048576U,
		// Token: 0x040000F3 RID: 243
		AvoidRecomputationOfNormals = 2097152U,
		// Token: 0x040000F4 RID: 244
		RenderOrderPlus1 = 150994944U,
		// Token: 0x040000F5 RID: 245
		RenderOrderPlus2 = 167772160U,
		// Token: 0x040000F6 RID: 246
		RenderOrderPlus3 = 184549376U,
		// Token: 0x040000F7 RID: 247
		RenderOrderPlus4 = 201326592U,
		// Token: 0x040000F8 RID: 248
		RenderOrderPlus5 = 218103808U,
		// Token: 0x040000F9 RID: 249
		RenderOrderPlus6 = 234881024U,
		// Token: 0x040000FA RID: 250
		RenderOrderPlus7 = 251658240U,
		// Token: 0x040000FB RID: 251
		GreaterDepthNoWrite = 268435456U,
		// Token: 0x040000FC RID: 252
		AlwaysDepthTest = 536870912U,
		// Token: 0x040000FD RID: 253
		RenderToAmbientOcclusionBuffer = 1073741824U
	}
}
