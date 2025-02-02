using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000079 RID: 121
	public sealed class RenderTargetComponent : DotNetObject
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00009036 File Offset: 0x00007236
		public Texture RenderTarget
		{
			get
			{
				return (Texture)this._renderTargetWeakReference.GetNativeObject();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00009048 File Offset: 0x00007248
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00009050 File Offset: 0x00007250
		public object UserData { get; internal set; }

		// Token: 0x060008E7 RID: 2279 RVA: 0x00009059 File Offset: 0x00007259
		internal RenderTargetComponent(Texture renderTarget)
		{
			this._renderTargetWeakReference = new WeakNativeObjectReference(renderTarget);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000906D File Offset: 0x0000726D
		internal void OnTargetReleased()
		{
			this.PaintNeeded = null;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00009076 File Offset: 0x00007276
		[EngineCallback]
		internal static RenderTargetComponent CreateRenderTargetComponent(Texture renderTarget)
		{
			return new RenderTargetComponent(renderTarget);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060008EA RID: 2282 RVA: 0x00009080 File Offset: 0x00007280
		// (remove) Token: 0x060008EB RID: 2283 RVA: 0x000090B8 File Offset: 0x000072B8
		internal event RenderTargetComponent.TextureUpdateEventHandler PaintNeeded;

		// Token: 0x060008EC RID: 2284 RVA: 0x000090ED File Offset: 0x000072ED
		[EngineCallback]
		internal void OnPaintNeeded()
		{
			RenderTargetComponent.TextureUpdateEventHandler paintNeeded = this.PaintNeeded;
			if (paintNeeded == null)
			{
				return;
			}
			paintNeeded(this.RenderTarget, EventArgs.Empty);
		}

		// Token: 0x04000182 RID: 386
		private readonly WeakNativeObjectReference _renderTargetWeakReference;

		// Token: 0x020000C4 RID: 196
		// (Invoke) Token: 0x06000C9A RID: 3226
		public delegate void TextureUpdateEventHandler(Texture sender, EventArgs e);
	}
}
