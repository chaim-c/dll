using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information
{
	// Token: 0x0200013B RID: 315
	public class PropertyBasedTooltipWidget : TooltipWidget
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		public Color AllyColor { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0002E0BD File Offset: 0x0002C2BD
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x0002E0C5 File Offset: 0x0002C2C5
		public Color EnemyColor { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0002E0CE File Offset: 0x0002C2CE
		// (set) Token: 0x060010A2 RID: 4258 RVA: 0x0002E0D6 File Offset: 0x0002C2D6
		public Color NeutralColor { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0002E0DF File Offset: 0x0002C2DF
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x0002E0E7 File Offset: 0x0002C2E7
		public Widget PropertyListBackground { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0002E0F8 File Offset: 0x0002C2F8
		public ListPanel PropertyList { get; set; }

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002E101 File Offset: 0x0002C301
		public PropertyBasedTooltipWidget(UIContext context) : base(context)
		{
			this._animationDelayInFrames = 2;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0002E111 File Offset: 0x0002C311
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.UpdateBattleScopes();
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0002E120 File Offset: 0x0002C320
		private void UpdateBattleScopes()
		{
			bool battleScope = false;
			foreach (TooltipPropertyWidget tooltipPropertyWidget in this.PropertyWidgets)
			{
				if (tooltipPropertyWidget.IsBattleMode)
				{
					battleScope = true;
				}
				else if (tooltipPropertyWidget.IsBattleModeOver)
				{
					battleScope = false;
				}
				tooltipPropertyWidget.SetBattleScope(battleScope);
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0002E188 File Offset: 0x0002C388
		private float GetBattleScopeSize()
		{
			bool flag = false;
			float num = 0f;
			if (this.PropertyList != null)
			{
				for (int i = 0; i < this.PropertyList.ChildCount; i++)
				{
					TooltipPropertyWidget tooltipPropertyWidget;
					if ((tooltipPropertyWidget = (this.PropertyList.GetChild(i) as TooltipPropertyWidget)) != null)
					{
						if (tooltipPropertyWidget.IsBattleMode)
						{
							flag = true;
						}
						else if (tooltipPropertyWidget.IsBattleModeOver)
						{
							flag = false;
						}
						if (flag)
						{
							float num2 = (tooltipPropertyWidget.ValueLabel.Size.X > tooltipPropertyWidget.DefinitionLabel.Size.X) ? tooltipPropertyWidget.ValueLabel.Size.X : tooltipPropertyWidget.DefinitionLabel.Size.X;
							if (num2 > num)
							{
								num = num2;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0002E244 File Offset: 0x0002C444
		private void UpdateRelationBrushes()
		{
			TooltipPropertyWidget tooltipPropertyWidget = this.PropertyWidgets.SingleOrDefault((TooltipPropertyWidget p) => p.IsRelation);
			if (tooltipPropertyWidget != null)
			{
				if ((tooltipPropertyWidget.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarFirstAlly) == TooltipPropertyWidget.TooltipPropertyFlags.WarFirstAlly)
				{
					this._definitionRelationBrush = this.AllyTroopsTextBrush;
				}
				else if ((tooltipPropertyWidget.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarFirstEnemy) == TooltipPropertyWidget.TooltipPropertyFlags.WarFirstEnemy)
				{
					this._definitionRelationBrush = this.EnemyTroopsTextBrush;
				}
				else
				{
					this._definitionRelationBrush = this.NeutralTroopsTextBrush;
				}
				if ((tooltipPropertyWidget.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarSecondAlly) == TooltipPropertyWidget.TooltipPropertyFlags.WarSecondAlly)
				{
					this._valueRelationBrush = this.AllyTroopsTextBrush;
					return;
				}
				if ((tooltipPropertyWidget.PropertyModifierAsFlag & TooltipPropertyWidget.TooltipPropertyFlags.WarSecondEnemy) == TooltipPropertyWidget.TooltipPropertyFlags.WarSecondEnemy)
				{
					this._valueRelationBrush = this.EnemyTroopsTextBrush;
					return;
				}
				this._valueRelationBrush = this.NeutralTroopsTextBrush;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0002E308 File Offset: 0x0002C508
		private IEnumerable<TooltipPropertyWidget> PropertyWidgets
		{
			get
			{
				if (this.PropertyList != null)
				{
					int num;
					for (int i = 0; i < this.PropertyList.ChildCount; i = num + 1)
					{
						TooltipPropertyWidget tooltipPropertyWidget;
						if ((tooltipPropertyWidget = (this.PropertyList.GetChild(i) as TooltipPropertyWidget)) != null)
						{
							yield return tooltipPropertyWidget;
						}
						num = i;
					}
				}
				yield break;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0002E318 File Offset: 0x0002C518
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			bool flag = false;
			float battleScopeSize = this.GetBattleScopeSize();
			float num = -1f;
			float num2 = -1f;
			this._definitionRelationBrush = null;
			this._valueRelationBrush = null;
			if (this.PropertyList != null)
			{
				for (int i = 0; i < this.PropertyList.ChildCount; i++)
				{
					TooltipPropertyWidget tooltipPropertyWidget;
					if ((tooltipPropertyWidget = (this.PropertyList.GetChild(i) as TooltipPropertyWidget)) != null && tooltipPropertyWidget.IsTwoColumn && !tooltipPropertyWidget.IsMultiLine)
					{
						if (num < tooltipPropertyWidget.ValueLabelContainer.Size.X)
						{
							num = tooltipPropertyWidget.ValueLabelContainer.Size.X;
						}
						if (num2 < tooltipPropertyWidget.DefinitionLabelContainer.Size.X)
						{
							num2 = tooltipPropertyWidget.DefinitionLabelContainer.Size.X;
						}
					}
				}
				for (int j = 0; j < this.PropertyList.ChildCount; j++)
				{
					TooltipPropertyWidget tooltipPropertyWidget2;
					if ((tooltipPropertyWidget2 = (this.PropertyList.GetChild(j) as TooltipPropertyWidget)) != null)
					{
						if (tooltipPropertyWidget2.IsBattleMode)
						{
							flag = true;
						}
						else if (tooltipPropertyWidget2.IsBattleModeOver)
						{
							flag = false;
						}
						if (flag && (this._definitionRelationBrush == null || this._valueRelationBrush == null))
						{
							this.UpdateRelationBrushes();
						}
						tooltipPropertyWidget2.RefreshSize(flag, battleScopeSize, num, num2, this._definitionRelationBrush, this._valueRelationBrush);
					}
				}
			}
			if (this.PropertyListBackground != null)
			{
				if (this.Mode == 2)
				{
					this.PropertyListBackground.Color = this.AllyColor;
					return;
				}
				if (this.Mode == 3)
				{
					this.PropertyListBackground.Color = this.EnemyColor;
					return;
				}
				this.PropertyListBackground.Color = this.NeutralColor;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0002E4B4 File Offset: 0x0002C6B4
		// (set) Token: 0x060010AF RID: 4271 RVA: 0x0002E4BC File Offset: 0x0002C6BC
		[Editor(false)]
		public int Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				if (this._mode != value)
				{
					this._mode = value;
				}
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x0002E4CE File Offset: 0x0002C6CE
		// (set) Token: 0x060010B1 RID: 4273 RVA: 0x0002E4D6 File Offset: 0x0002C6D6
		[Editor(false)]
		public Brush NeutralTroopsTextBrush
		{
			get
			{
				return this._neutralTroopsTextBrush;
			}
			set
			{
				if (this._neutralTroopsTextBrush != value)
				{
					this._neutralTroopsTextBrush = value;
					base.OnPropertyChanged<Brush>(value, "NeutralTroopsTextBrush");
				}
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
		// (set) Token: 0x060010B3 RID: 4275 RVA: 0x0002E4FC File Offset: 0x0002C6FC
		[Editor(false)]
		public Brush EnemyTroopsTextBrush
		{
			get
			{
				return this._enemyTroopsTextBrush;
			}
			set
			{
				if (this._enemyTroopsTextBrush != value)
				{
					this._enemyTroopsTextBrush = value;
					base.OnPropertyChanged<Brush>(value, "EnemyTroopsTextBrush");
				}
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0002E51A File Offset: 0x0002C71A
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x0002E522 File Offset: 0x0002C722
		[Editor(false)]
		public Brush AllyTroopsTextBrush
		{
			get
			{
				return this._allyTroopsTextBrush;
			}
			set
			{
				if (this._allyTroopsTextBrush != value)
				{
					this._allyTroopsTextBrush = value;
					base.OnPropertyChanged<Brush>(value, "AllyTroopsTextBrush");
				}
			}
		}

		// Token: 0x04000799 RID: 1945
		private Brush _definitionRelationBrush;

		// Token: 0x0400079A RID: 1946
		private Brush _valueRelationBrush;

		// Token: 0x0400079B RID: 1947
		private int _mode;

		// Token: 0x0400079C RID: 1948
		private Brush _neutralTroopsTextBrush;

		// Token: 0x0400079D RID: 1949
		private Brush _allyTroopsTextBrush;

		// Token: 0x0400079E RID: 1950
		private Brush _enemyTroopsTextBrush;
	}
}
