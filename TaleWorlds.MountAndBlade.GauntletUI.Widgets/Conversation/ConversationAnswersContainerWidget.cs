using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Conversation
{
	// Token: 0x02000160 RID: 352
	public class ConversationAnswersContainerWidget : Widget
	{
		// Token: 0x0600127C RID: 4732 RVA: 0x00032A49 File Offset: 0x00030C49
		public ConversationAnswersContainerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00032A52 File Offset: 0x00030C52
		protected override void OnLateUpdate(float dt)
		{
			this.UpdateHeight();
			base.OnLateUpdate(dt);
			this.UpdateHeight();
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00032A67 File Offset: 0x00030C67
		protected override void OnUpdate(float dt)
		{
			this.UpdateHeight();
			base.OnUpdate(dt);
			this.UpdateHeight();
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00032A7C File Offset: 0x00030C7C
		private void UpdateHeight()
		{
			if (this.AnswerContainerWidget.Size.Y >= base.Size.Y)
			{
				base.HeightSizePolicy = SizePolicy.Fixed;
				base.ScaledSuggestedHeight = this.AnswerContainerWidget.Size.Y;
				return;
			}
			base.HeightSizePolicy = SizePolicy.CoverChildren;
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00032ACB File Offset: 0x00030CCB
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x00032AD3 File Offset: 0x00030CD3
		[Editor(false)]
		public Widget AnswerContainerWidget
		{
			get
			{
				return this._answerContainerWidget;
			}
			set
			{
				if (value != this._answerContainerWidget)
				{
					this._answerContainerWidget = value;
					base.OnPropertyChanged<Widget>(value, "AnswerContainerWidget");
				}
			}
		}

		// Token: 0x0400086E RID: 2158
		private Widget _answerContainerWidget;
	}
}
