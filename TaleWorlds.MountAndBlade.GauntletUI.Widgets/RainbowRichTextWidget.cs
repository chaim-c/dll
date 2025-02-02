using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000035 RID: 53
	public class RainbowRichTextWidget : RichTextWidget
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00009BC2 File Offset: 0x00007DC2
		public RainbowRichTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009BD8 File Offset: 0x00007DD8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			base.Brush.FontColor = Color.Lerp(base.ReadOnlyBrush.FontColor, this.targetColor, dt);
			if (base.Brush.FontColor.ToVec3().Distance(this.targetColor.ToVec3()) < 1f)
			{
				Random random = new Random();
				this.targetColor = Color.FromVector3(new Vector3((float)random.Next(255), (float)random.Next(255), (float)random.Next(255)));
			}
		}

		// Token: 0x04000134 RID: 308
		private Color targetColor = Color.White;
	}
}
