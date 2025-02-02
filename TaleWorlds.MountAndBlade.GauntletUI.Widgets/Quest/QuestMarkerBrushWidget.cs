using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Quest
{
	// Token: 0x02000058 RID: 88
	public class QuestMarkerBrushWidget : BrushWidget
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E9A5 File Offset: 0x0000CBA5
		public QuestMarkerBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		private void UpdateMarkerState(int type)
		{
			string text;
			switch (type)
			{
			case 0:
				text = "None";
				goto IL_6D;
			case 1:
				text = "AvailableIssue";
				goto IL_6D;
			case 2:
				text = "ActiveIssue";
				goto IL_6D;
			case 3:
			case 5:
			case 6:
			case 7:
				break;
			case 4:
				text = "ActiveStoryQuest";
				goto IL_6D;
			case 8:
				text = "TrackedIssue";
				goto IL_6D;
			default:
				if (type == 16)
				{
					text = "TrackedStoryQuest";
					goto IL_6D;
				}
				break;
			}
			text = "None";
			IL_6D:
			if (text != null)
			{
				this.SetState(text);
				Sprite sprite = base.Brush.GetLayer(text).Sprite;
				if (sprite != null)
				{
					float num = base.SuggestedHeight / (float)sprite.Height;
					base.SuggestedWidth = (float)sprite.Width * num;
				}
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000EA67 File Offset: 0x0000CC67
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000EA6F File Offset: 0x0000CC6F
		public int QuestMarkerType
		{
			get
			{
				return this._questMarkerType;
			}
			set
			{
				if (value != this._questMarkerType)
				{
					this._questMarkerType = value;
					this.UpdateMarkerState(this._questMarkerType);
				}
			}
		}

		// Token: 0x0400020A RID: 522
		private int _questMarkerType;
	}
}
