using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008C RID: 140
	[EngineClass("rglTableau_view")]
	public sealed class TableauView : SceneView
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000BBDC File Offset: 0x00009DDC
		internal TableauView(UIntPtr meshPointer) : base(meshPointer)
		{
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000BBE5 File Offset: 0x00009DE5
		public static TableauView CreateTableauView()
		{
			return EngineApplicationInterface.ITableauView.CreateTableauView();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0000BBF1 File Offset: 0x00009DF1
		public void SetSortingEnabled(bool value)
		{
			EngineApplicationInterface.ITableauView.SetSortingEnabled(base.Pointer, value);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0000BC04 File Offset: 0x00009E04
		public void SetContinuousRendering(bool value)
		{
			EngineApplicationInterface.ITableauView.SetContinousRendering(base.Pointer, value);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0000BC17 File Offset: 0x00009E17
		public void SetDoNotRenderThisFrame(bool value)
		{
			EngineApplicationInterface.ITableauView.SetDoNotRenderThisFrame(base.Pointer, value);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0000BC2A File Offset: 0x00009E2A
		public void SetDeleteAfterRendering(bool value)
		{
			EngineApplicationInterface.ITableauView.SetDeleteAfterRendering(base.Pointer, value);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0000BC3D File Offset: 0x00009E3D
		public static Texture AddTableau(string name, RenderTargetComponent.TextureUpdateEventHandler eventHandler, object objectRef, int tableauSizeX, int tableauSizeY)
		{
			Texture texture = Texture.CreateTableauTexture(name, eventHandler, objectRef, tableauSizeX, tableauSizeY);
			texture.TableauView.SetRenderOnDemand(false);
			return texture;
		}
	}
}
