using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.BoardGame
{
	// Token: 0x02000181 RID: 385
	public class BoardGameInstructionVisualWidget : Widget
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x0003643C File Offset: 0x0003463C
		public BoardGameInstructionVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00036448 File Offset: 0x00034648
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.Sprite == null)
			{
				int siblingIndex = base.ParentWidget.ParentWidget.GetSiblingIndex();
				if (!string.IsNullOrEmpty(this.GameType))
				{
					base.Sprite = base.Context.SpriteData.GetSprite(this.GameType + siblingIndex);
				}
			}
			if (base.Sprite != null)
			{
				base.SuggestedWidth = (float)base.Sprite.Width * 0.5f;
				base.SuggestedHeight = (float)base.Sprite.Height * 0.5f;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x000364E1 File Offset: 0x000346E1
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x000364E9 File Offset: 0x000346E9
		[Editor(false)]
		public string GameType
		{
			get
			{
				return this._gameType;
			}
			set
			{
				if (this._gameType != value)
				{
					this._gameType = value;
				}
			}
		}

		// Token: 0x0400090A RID: 2314
		private const float ScaleCoeff = 0.5f;

		// Token: 0x0400090B RID: 2315
		private string _gameType;
	}
}
