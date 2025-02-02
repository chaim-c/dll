using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008D RID: 141
	public sealed class Texture : Resource
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0000BC56 File Offset: 0x00009E56
		private Texture()
		{
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0000BC5E File Offset: 0x00009E5E
		internal Texture(UIntPtr ptr) : base(ptr)
		{
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0000BC67 File Offset: 0x00009E67
		public int Width
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetWidth(base.Pointer);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0000BC79 File Offset: 0x00009E79
		public int Height
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetHeight(base.Pointer);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0000BC8B File Offset: 0x00009E8B
		public int MemorySize
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetMemorySize(base.Pointer);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0000BC9D File Offset: 0x00009E9D
		public bool IsRenderTarget
		{
			get
			{
				return EngineApplicationInterface.ITexture.IsRenderTarget(base.Pointer);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000BCAF File Offset: 0x00009EAF
		public static Texture CreateTextureFromPath(PlatformFilePath filePath)
		{
			return EngineApplicationInterface.ITexture.CreateTextureFromPath(filePath);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0000BCBC File Offset: 0x00009EBC
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0000BCCE File Offset: 0x00009ECE
		public string Name
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetName(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ITexture.SetName(base.Pointer, value);
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000BCE1 File Offset: 0x00009EE1
		public void TransformRenderTargetToResource(string name)
		{
			EngineApplicationInterface.ITexture.TransformRenderTargetToResourceTexture(base.Pointer, name);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		public static Texture GetFromResource(string resourceName)
		{
			return EngineApplicationInterface.ITexture.GetFromResource(resourceName);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0000BD01 File Offset: 0x00009F01
		public bool IsLoaded()
		{
			return EngineApplicationInterface.ITexture.IsLoaded(base.Pointer);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0000BD13 File Offset: 0x00009F13
		public static Texture CheckAndGetFromResource(string resourceName)
		{
			return EngineApplicationInterface.ITexture.CheckAndGetFromResource(resourceName);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0000BD20 File Offset: 0x00009F20
		public static void ScaleTextureWithRatio(ref int tableauSizeX, ref int tableauSizeY)
		{
			float num = (float)tableauSizeX;
			float num2 = (float)tableauSizeY;
			int num3 = (int)MathF.Log(num, 2f) + 2;
			float num4 = MathF.Pow(2f, (float)num3) / num;
			tableauSizeX = (int)(num * num4);
			tableauSizeY = (int)(num2 * num4);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0000BD5F File Offset: 0x00009F5F
		public void PreloadTexture(bool blocking)
		{
			EngineApplicationInterface.ITexture.GetCurObject(base.Pointer, blocking);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0000BD72 File Offset: 0x00009F72
		public void Release()
		{
			this.RenderTargetComponent.OnTargetReleased();
			EngineApplicationInterface.ITexture.Release(base.Pointer);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000BD8F File Offset: 0x00009F8F
		public void ReleaseNextFrame()
		{
			this.RenderTargetComponent.OnTargetReleased();
			EngineApplicationInterface.ITexture.ReleaseNextFrame(base.Pointer);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0000BDAC File Offset: 0x00009FAC
		public static Texture LoadTextureFromPath(string fileName, string folder)
		{
			return EngineApplicationInterface.ITexture.LoadTextureFromPath(fileName, folder);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000BDBA File Offset: 0x00009FBA
		public static Texture CreateDepthTarget(string name, int width, int height)
		{
			return EngineApplicationInterface.ITexture.CreateDepthTarget(name, width, height);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000BDC9 File Offset: 0x00009FC9
		public static Texture CreateFromByteArray(byte[] data, int width, int height)
		{
			return EngineApplicationInterface.ITexture.CreateFromByteArray(data, width, height);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		public void SaveToFile(string path)
		{
			EngineApplicationInterface.ITexture.SaveToFile(base.Pointer, path);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000BDEB File Offset: 0x00009FEB
		public void SetTextureAsAlwaysValid()
		{
			EngineApplicationInterface.ITexture.SaveTextureAsAlwaysValid(base.Pointer);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000BDFD File Offset: 0x00009FFD
		public static Texture CreateFromMemory(byte[] data)
		{
			return EngineApplicationInterface.ITexture.CreateFromMemory(data);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000BE0A File Offset: 0x0000A00A
		public static void ReleaseGpuMemories()
		{
			EngineApplicationInterface.ITexture.ReleaseGpuMemories();
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0000BE16 File Offset: 0x0000A016
		public RenderTargetComponent RenderTargetComponent
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetRenderTargetComponent(base.Pointer);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0000BE28 File Offset: 0x0000A028
		public TableauView TableauView
		{
			get
			{
				return EngineApplicationInterface.ITexture.GetTableauView(base.Pointer);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0000BE3A File Offset: 0x0000A03A
		public object UserData
		{
			get
			{
				return this.RenderTargetComponent.UserData;
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000BE47 File Offset: 0x0000A047
		private void SetTableauView(TableauView tableauView)
		{
			EngineApplicationInterface.ITexture.SetTableauView(base.Pointer, tableauView.Pointer);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0000BE60 File Offset: 0x0000A060
		public static Texture CreateTableauTexture(string name, RenderTargetComponent.TextureUpdateEventHandler eventHandler, object objectRef, int tableauSizeX, int tableauSizeY)
		{
			Texture texture = Texture.CreateRenderTarget(name, tableauSizeX, tableauSizeY, true, false, false, false);
			RenderTargetComponent renderTargetComponent = texture.RenderTargetComponent;
			renderTargetComponent.PaintNeeded += eventHandler;
			renderTargetComponent.UserData = objectRef;
			TableauView tableauView = TableauView.CreateTableauView();
			tableauView.SetRenderTarget(texture);
			tableauView.SetAutoDepthTargetCreation(true);
			tableauView.SetSceneUsesSkybox(false);
			tableauView.SetClearColor(4294902015U);
			texture.SetTableauView(tableauView);
			return texture;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		public static Texture CreateRenderTarget(string name, int width, int height, bool autoMipmaps, bool isTableau, bool createUninitialized = false, bool always_valid = false)
		{
			return EngineApplicationInterface.ITexture.CreateRenderTarget(name, width, height, autoMipmaps, isTableau, createUninitialized, always_valid);
		}
	}
}
