using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000B6 RID: 182
	public class TauntVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x0001B272 File Offset: 0x00019472
		public TauntVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001B27C File Offset: 0x0001947C
		private void UpdateTauntVisual()
		{
			Brush tauntIconsBrush = this.TauntIconsBrush;
			Sprite sprite;
			if (tauntIconsBrush == null)
			{
				sprite = null;
			}
			else
			{
				BrushLayer layer = tauntIconsBrush.GetLayer(this.TauntID);
				sprite = ((layer != null) ? layer.Sprite : null);
			}
			Sprite sprite2 = sprite;
			if (base.Brush != null)
			{
				base.Brush.Sprite = sprite2;
				foreach (BrushLayer brushLayer in base.Brush.Layers)
				{
					brushLayer.Sprite = sprite2;
				}
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001B30C File Offset: 0x0001950C
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0001B314 File Offset: 0x00019514
		public Brush TauntIconsBrush
		{
			get
			{
				return this._tauntIconsBrush;
			}
			set
			{
				if (value != this._tauntIconsBrush)
				{
					this._tauntIconsBrush = value;
					base.OnPropertyChanged<Brush>(value, "TauntIconsBrush");
				}
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001B332 File Offset: 0x00019532
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0001B33A File Offset: 0x0001953A
		public string TauntID
		{
			get
			{
				return this._tauntId;
			}
			set
			{
				if (value != this._tauntId)
				{
					this._tauntId = value;
					base.OnPropertyChanged<string>(value, "TauntID");
					this.UpdateTauntVisual();
				}
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001B363 File Offset: 0x00019563
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0001B36B File Offset: 0x0001956B
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChanged(value, "IsSelected");
					this.SetState(value ? "Selected" : "Default");
				}
			}
		}

		// Token: 0x0400045A RID: 1114
		private Brush _tauntIconsBrush;

		// Token: 0x0400045B RID: 1115
		private string _tauntId;

		// Token: 0x0400045C RID: 1116
		private bool _isSelected;
	}
}
