using System;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension.Standalone
{
	// Token: 0x0200000B RID: 11
	public class TwoDimensionPlatform : ITwoDimensionPlatform, ITwoDimensionResourceContext
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000423C File Offset: 0x0000243C
		float ITwoDimensionPlatform.Width
		{
			get
			{
				return (float)this._form.Width;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000424A File Offset: 0x0000244A
		float ITwoDimensionPlatform.Height
		{
			get
			{
				return (float)this._form.Height;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004258 File Offset: 0x00002458
		float ITwoDimensionPlatform.ReferenceWidth
		{
			get
			{
				return (float)this._form.Width;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004266 File Offset: 0x00002466
		float ITwoDimensionPlatform.ReferenceHeight
		{
			get
			{
				return (float)this._form.Height;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004274 File Offset: 0x00002474
		float ITwoDimensionPlatform.ApplicationTime
		{
			get
			{
				return (float)Environment.TickCount;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000427C File Offset: 0x0000247C
		public TwoDimensionPlatform(GraphicsForm form, bool isAssetsUnderDefaultFolders)
		{
			this._form = form;
			this._isAssetsUnderDefaultFolders = isAssetsUnderDefaultFolders;
			this._graphicsContext = this._form.GraphicsContext;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000042A3 File Offset: 0x000024A3
		void ITwoDimensionPlatform.Draw(float x, float y, Material material, DrawObject2D drawObject2D, int layer)
		{
			this._graphicsContext.DrawElements(x, y, material, drawObject2D);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000042B8 File Offset: 0x000024B8
		Texture ITwoDimensionResourceContext.LoadTexture(ResourceDepot resourceDepot, string name)
		{
			OpenGLTexture openGLTexture = new OpenGLTexture();
			string name2 = name;
			if (!this._isAssetsUnderDefaultFolders)
			{
				string[] array = name.Split(new char[]
				{
					'\\'
				});
				name2 = array[array.Length - 1];
			}
			openGLTexture.LoadFromFile(resourceDepot, name2);
			return new Texture(openGLTexture);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000042FB File Offset: 0x000024FB
		void ITwoDimensionPlatform.PlaySound(string soundName)
		{
			Debug.Print("Playing sound: " + soundName, 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004319 File Offset: 0x00002519
		void ITwoDimensionPlatform.SetScissor(ScissorTestInfo scissorTestInfo)
		{
			this._graphicsContext.SetScissor(scissorTestInfo);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004327 File Offset: 0x00002527
		void ITwoDimensionPlatform.ResetScissor()
		{
			this._graphicsContext.ResetScissor();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004334 File Offset: 0x00002534
		void ITwoDimensionPlatform.CreateSoundEvent(string soundName)
		{
			Debug.Print("Created sound event: " + soundName, 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004352 File Offset: 0x00002552
		void ITwoDimensionPlatform.StopAndRemoveSoundEvent(string soundName)
		{
			Debug.Print("Stopped sound event: " + soundName, 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004370 File Offset: 0x00002570
		void ITwoDimensionPlatform.PlaySoundEvent(string soundName)
		{
			Debug.Print("Played sound event: " + soundName, 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000438E File Offset: 0x0000258E
		void ITwoDimensionPlatform.OpenOnScreenKeyboard(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum)
		{
			Debug.Print("Opened on-screen keyboard", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000043A6 File Offset: 0x000025A6
		void ITwoDimensionPlatform.BeginDebugPanel(string panelTitle)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000043A8 File Offset: 0x000025A8
		void ITwoDimensionPlatform.EndDebugPanel()
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000043AA File Offset: 0x000025AA
		void ITwoDimensionPlatform.DrawDebugText(string text)
		{
			Debug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000043BE File Offset: 0x000025BE
		bool ITwoDimensionPlatform.IsDebugModeEnabled()
		{
			return false;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000043C1 File Offset: 0x000025C1
		bool ITwoDimensionPlatform.DrawDebugTreeNode(string text)
		{
			return false;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000043C4 File Offset: 0x000025C4
		void ITwoDimensionPlatform.DrawCheckbox(string label, ref bool isChecked)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000043C6 File Offset: 0x000025C6
		bool ITwoDimensionPlatform.IsDebugItemHovered()
		{
			return false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000043C9 File Offset: 0x000025C9
		void ITwoDimensionPlatform.PopDebugTreeNode()
		{
		}

		// Token: 0x0400003E RID: 62
		private GraphicsContext _graphicsContext;

		// Token: 0x0400003F RID: 63
		private GraphicsForm _form;

		// Token: 0x04000040 RID: 64
		private bool _isAssetsUnderDefaultFolders;
	}
}
