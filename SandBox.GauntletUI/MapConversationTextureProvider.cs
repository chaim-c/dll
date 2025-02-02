using System;
using SandBox.View.Map;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI
{
	// Token: 0x0200000F RID: 15
	public class MapConversationTextureProvider : TextureProvider
	{
		// Token: 0x17000007 RID: 7
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00007277 File Offset: 0x00005477
		public object Data
		{
			set
			{
				this._mapConversationTableau.SetData(value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00007285 File Offset: 0x00005485
		public bool IsEnabled
		{
			set
			{
				this._mapConversationTableau.SetEnabled(value);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007293 File Offset: 0x00005493
		public MapConversationTextureProvider()
		{
			this._mapConversationTableau = new MapConversationTableau();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000072A6 File Offset: 0x000054A6
		public override void Clear(bool clearNextFrame)
		{
			this._mapConversationTableau.OnFinalize(clearNextFrame);
			base.Clear(clearNextFrame);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000072BC File Offset: 0x000054BC
		private void CheckTexture()
		{
			if (this._texture != this._mapConversationTableau.Texture)
			{
				this._texture = this._mapConversationTableau.Texture;
				if (this._texture != null)
				{
					EngineTexture platformTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(platformTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007320 File Offset: 0x00005520
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000732E File Offset: 0x0000552E
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._mapConversationTableau.SetTargetSize(width, height);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007345 File Offset: 0x00005545
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			this._mapConversationTableau.OnTick(dt);
		}

		// Token: 0x0400004C RID: 76
		private MapConversationTableau _mapConversationTableau;

		// Token: 0x0400004D RID: 77
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x0400004E RID: 78
		private TaleWorlds.TwoDimension.Texture _providedTexture;
	}
}
