using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.FlagMarker
{
	// Token: 0x020000F3 RID: 243
	public class SiegeEngineVisualWidget : Widget
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x00023C6E File Offset: 0x00021E6E
		public SiegeEngineVisualWidget(UIContext context) : base(context)
		{
			this._fallbackSprite = this.GetSprite("BlankWhiteCircle");
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00023C94 File Offset: 0x00021E94
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._hasVisualSet && this.EngineID != string.Empty && this.OutlineWidget != null && this.IconWidget != null)
			{
				string text = string.Empty;
				string engineID = this.EngineID;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(engineID);
				if (num <= 1241455715U)
				{
					if (num <= 712590611U)
					{
						if (num != 6339497U)
						{
							if (num != 695812992U)
							{
								if (num != 712590611U)
								{
									goto IL_1F8;
								}
								if (!(engineID == "siege_tower_level2"))
								{
									goto IL_1F8;
								}
							}
							else if (!(engineID == "siege_tower_level3"))
							{
								goto IL_1F8;
							}
						}
						else
						{
							if (!(engineID == "ladder"))
							{
								goto IL_1F8;
							}
							text = "ladder";
							goto IL_1F8;
						}
					}
					else if (num != 729368230U)
					{
						if (num != 808481256U)
						{
							if (num != 1241455715U)
							{
								goto IL_1F8;
							}
							if (!(engineID == "ram"))
							{
								goto IL_1F8;
							}
							text = "battering_ram";
							goto IL_1F8;
						}
						else
						{
							if (!(engineID == "fire_ballista"))
							{
								goto IL_1F8;
							}
							goto IL_1D2;
						}
					}
					else if (!(engineID == "siege_tower_level1"))
					{
						goto IL_1F8;
					}
					text = "siege_tower";
					goto IL_1F8;
				}
				if (num <= 1839032341U)
				{
					if (num != 1748194790U)
					{
						if (num != 1820818168U)
						{
							if (num != 1839032341U)
							{
								goto IL_1F8;
							}
							if (!(engineID == "trebuchet"))
							{
								goto IL_1F8;
							}
							text = "trebuchet";
							goto IL_1F8;
						}
						else if (!(engineID == "fire_onager"))
						{
							goto IL_1F8;
						}
					}
					else if (!(engineID == "fire_catapult"))
					{
						goto IL_1F8;
					}
				}
				else if (num != 1898442385U)
				{
					if (num != 2806198843U)
					{
						if (num != 4036530155U)
						{
							goto IL_1F8;
						}
						if (!(engineID == "ballista"))
						{
							goto IL_1F8;
						}
						goto IL_1D2;
					}
					else if (!(engineID == "onager"))
					{
						goto IL_1F8;
					}
				}
				else if (!(engineID == "catapult"))
				{
					goto IL_1F8;
				}
				text = "catapult";
				goto IL_1F8;
				IL_1D2:
				text = "ballista";
				IL_1F8:
				this.OutlineWidget.Sprite = ((text == string.Empty) ? this._fallbackSprite : this.GetSprite("MPHud\\SiegeMarkers\\" + text + "_outline"));
				this.IconWidget.Sprite = ((text == string.Empty) ? this._fallbackSprite : this.GetSprite("MPHud\\SiegeMarkers\\" + text));
				this._hasVisualSet = true;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00023F07 File Offset: 0x00022107
		private Sprite GetSprite(string path)
		{
			return base.Context.SpriteData.GetSprite(path);
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00023F1A File Offset: 0x0002211A
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00023F22 File Offset: 0x00022122
		[Editor(false)]
		public string EngineID
		{
			get
			{
				return this._engineID;
			}
			set
			{
				if (value != this._engineID)
				{
					this._engineID = value;
					base.OnPropertyChanged<string>(value, "EngineID");
				}
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00023F45 File Offset: 0x00022145
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00023F4D File Offset: 0x0002214D
		public Widget OutlineWidget
		{
			get
			{
				return this._outlineWidget;
			}
			set
			{
				if (this._outlineWidget != value)
				{
					this._outlineWidget = value;
					base.OnPropertyChanged<Widget>(value, "OutlineWidget");
				}
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00023F6B File Offset: 0x0002216B
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00023F73 File Offset: 0x00022173
		public Widget IconWidget
		{
			get
			{
				return this._iconWidget;
			}
			set
			{
				if (this._iconWidget != value)
				{
					this._iconWidget = value;
					base.OnPropertyChanged<Widget>(value, "IconWidget");
				}
			}
		}

		// Token: 0x040005EF RID: 1519
		private bool _hasVisualSet;

		// Token: 0x040005F0 RID: 1520
		private Sprite _fallbackSprite;

		// Token: 0x040005F1 RID: 1521
		private const string SpritePathPrefix = "MPHud\\SiegeMarkers\\";

		// Token: 0x040005F2 RID: 1522
		private string _engineID = string.Empty;

		// Token: 0x040005F3 RID: 1523
		private Widget _outlineWidget;

		// Token: 0x040005F4 RID: 1524
		private Widget _iconWidget;
	}
}
