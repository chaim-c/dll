using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000098 RID: 152
	public class MultiplayerLobbyCosmeticAnimationControllerWidget : Widget
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x00017BD3 File Offset: 0x00015DD3
		private double GetRandomDoubleBetween(double min, double max)
		{
			return base.Context.UIRandom.NextDouble() * (max - min) + max;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00017BEC File Offset: 0x00015DEC
		public MultiplayerLobbyCosmeticAnimationControllerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00017C42 File Offset: 0x00015E42
		private void RestartAllAnimations()
		{
			this.SetAllAnimationPartColors();
			this.StopAllAnimations();
			this.StartAllAnimations();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00017C56 File Offset: 0x00015E56
		private void SetAllAnimationPartColors()
		{
			this.ApplyActionOnAllAnimations(new Action<MultiplayerLobbyCosmeticAnimationPartWidget>(this.SetColorOfPart));
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00017C6A File Offset: 0x00015E6A
		private void StartAllAnimations()
		{
			this.ApplyActionOnAllAnimations(new Action<MultiplayerLobbyCosmeticAnimationPartWidget>(this.StartAnimationOfPart));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00017C7E File Offset: 0x00015E7E
		private void StopAllAnimations()
		{
			this.ApplyActionOnAllAnimations(new Action<MultiplayerLobbyCosmeticAnimationPartWidget>(this.StopAnimationOfPart));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00017C94 File Offset: 0x00015E94
		private void StartAnimationOfPart(MultiplayerLobbyCosmeticAnimationPartWidget part)
		{
			double randomDoubleBetween = this.GetRandomDoubleBetween((double)this.MinAlphaChangeDuration, (double)this.MaxAlphaChangeDuration);
			double randomDoubleBetween2 = this.GetRandomDoubleBetween((double)this.MinAlphaLowerBound, (double)this.MinAlphaUpperBound);
			double randomDoubleBetween3 = this.GetRandomDoubleBetween((double)this.MaxAlphaLowerBound, (double)this.MaxAlphaUpperBound);
			part.StartAnimation((float)randomDoubleBetween, (float)randomDoubleBetween2, (float)randomDoubleBetween3);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00017CEC File Offset: 0x00015EEC
		private void StopAnimationOfPart(MultiplayerLobbyCosmeticAnimationPartWidget part)
		{
			part.StopAnimation();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00017CF4 File Offset: 0x00015EF4
		private void SetColorOfPart(MultiplayerLobbyCosmeticAnimationPartWidget part)
		{
			switch (this.CosmeticRarity)
			{
			case 0:
			case 1:
				part.Color = this.RarityCommonColor;
				return;
			case 2:
				part.Color = this.RarityRareColor;
				return;
			case 3:
				part.Color = this.RarityUniqueColor;
				return;
			default:
				part.Color = MultiplayerLobbyCosmeticAnimationControllerWidget.DefaultColor;
				return;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00017D54 File Offset: 0x00015F54
		private void ApplyActionOnAllAnimations(Action<MultiplayerLobbyCosmeticAnimationPartWidget> action)
		{
			BasicContainer animationPartContainer = this.AnimationPartContainer;
			if (animationPartContainer == null)
			{
				return;
			}
			animationPartContainer.Children.ForEach(delegate(Widget c)
			{
				action(c as MultiplayerLobbyCosmeticAnimationPartWidget);
			});
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00017D90 File Offset: 0x00015F90
		private void OnAnimationPartAdded(Widget parent, Widget child)
		{
			MultiplayerLobbyCosmeticAnimationPartWidget multiplayerLobbyCosmeticAnimationPartWidget = child as MultiplayerLobbyCosmeticAnimationPartWidget;
			this.SetColorOfPart(multiplayerLobbyCosmeticAnimationPartWidget);
			this.StartAnimationOfPart(multiplayerLobbyCosmeticAnimationPartWidget);
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00017DB2 File Offset: 0x00015FB2
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00017DBA File Offset: 0x00015FBA
		[Editor(false)]
		public int CosmeticRarity
		{
			get
			{
				return this._cosmeticRarity;
			}
			set
			{
				if (value != this._cosmeticRarity)
				{
					this._cosmeticRarity = value;
					base.OnPropertyChanged(value, "CosmeticRarity");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00017DDE File Offset: 0x00015FDE
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00017DE6 File Offset: 0x00015FE6
		[Editor(false)]
		public float MinAlphaChangeDuration
		{
			get
			{
				return this._minAlphaChangeDuration;
			}
			set
			{
				if (this._minAlphaChangeDuration != value)
				{
					this._minAlphaChangeDuration = value;
					base.OnPropertyChanged(value, "MinAlphaChangeDuration");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00017E0A File Offset: 0x0001600A
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00017E12 File Offset: 0x00016012
		[Editor(false)]
		public float MaxAlphaChangeDuration
		{
			get
			{
				return this._maxAlphaChangeDuration;
			}
			set
			{
				if (this._maxAlphaChangeDuration != value)
				{
					this._maxAlphaChangeDuration = value;
					base.OnPropertyChanged(value, "MaxAlphaChangeDuration");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00017E36 File Offset: 0x00016036
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00017E3E File Offset: 0x0001603E
		[Editor(false)]
		public float MinAlphaLowerBound
		{
			get
			{
				return this._minAlphaLowerBound;
			}
			set
			{
				if (this._minAlphaLowerBound != value)
				{
					this._minAlphaLowerBound = value;
					base.OnPropertyChanged(value, "MinAlphaLowerBound");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00017E62 File Offset: 0x00016062
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x00017E6A File Offset: 0x0001606A
		[Editor(false)]
		public float MinAlphaUpperBound
		{
			get
			{
				return this._minAlphaUpperBound;
			}
			set
			{
				if (this._minAlphaUpperBound != value)
				{
					this._minAlphaUpperBound = value;
					base.OnPropertyChanged(value, "MinAlphaUpperBound");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00017E8E File Offset: 0x0001608E
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00017E96 File Offset: 0x00016096
		[Editor(false)]
		public float MaxAlphaLowerBound
		{
			get
			{
				return this._maxAlphaLowerBound;
			}
			set
			{
				if (this._maxAlphaLowerBound != value)
				{
					this._maxAlphaLowerBound = value;
					base.OnPropertyChanged(value, "MaxAlphaLowerBound");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00017EBA File Offset: 0x000160BA
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00017EC2 File Offset: 0x000160C2
		[Editor(false)]
		public float MaxAlphaUpperBound
		{
			get
			{
				return this._maxAlphaUpperBound;
			}
			set
			{
				if (this._maxAlphaUpperBound != value)
				{
					this._maxAlphaUpperBound = value;
					base.OnPropertyChanged(value, "MaxAlphaUpperBound");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00017EE6 File Offset: 0x000160E6
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00017EEE File Offset: 0x000160EE
		[Editor(false)]
		public Color RarityCommonColor
		{
			get
			{
				return this._rarityCommonColor;
			}
			set
			{
				if (this._rarityCommonColor != value)
				{
					this._rarityCommonColor = value;
					base.OnPropertyChanged(value, "RarityCommonColor");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00017F17 File Offset: 0x00016117
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00017F1F File Offset: 0x0001611F
		[Editor(false)]
		public Color RarityRareColor
		{
			get
			{
				return this._rarityRareColor;
			}
			set
			{
				if (this._rarityRareColor != value)
				{
					this._rarityRareColor = value;
					base.OnPropertyChanged(value, "RarityRareColor");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00017F48 File Offset: 0x00016148
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00017F50 File Offset: 0x00016150
		[Editor(false)]
		public Color RarityUniqueColor
		{
			get
			{
				return this._rarityUniqueColor;
			}
			set
			{
				if (this._rarityUniqueColor != value)
				{
					this._rarityUniqueColor = value;
					base.OnPropertyChanged(value, "RarityUniqueColor");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00017F79 File Offset: 0x00016179
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00017F84 File Offset: 0x00016184
		[Editor(false)]
		public BasicContainer AnimationPartContainer
		{
			get
			{
				return this._animationPartContainer;
			}
			set
			{
				if (value != this._animationPartContainer)
				{
					if (this._animationPartContainer != null)
					{
						this._animationPartContainer.ItemAddEventHandlers.Remove(new Action<Widget, Widget>(this.OnAnimationPartAdded));
					}
					this._animationPartContainer = value;
					if (this._animationPartContainer != null)
					{
						this._animationPartContainer.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnAnimationPartAdded));
					}
					base.OnPropertyChanged<BasicContainer>(value, "AnimationPartContainer");
					this.RestartAllAnimations();
				}
			}
		}

		// Token: 0x040003B3 RID: 947
		private static readonly Color DefaultColor = Color.FromUint(0U);

		// Token: 0x040003B4 RID: 948
		private int _cosmeticRarity;

		// Token: 0x040003B5 RID: 949
		private float _minAlphaChangeDuration = 1.5f;

		// Token: 0x040003B6 RID: 950
		private float _maxAlphaChangeDuration = 2.5f;

		// Token: 0x040003B7 RID: 951
		private float _minAlphaLowerBound = 0.4f;

		// Token: 0x040003B8 RID: 952
		private float _minAlphaUpperBound = 0.6f;

		// Token: 0x040003B9 RID: 953
		private float _maxAlphaLowerBound = 0.6f;

		// Token: 0x040003BA RID: 954
		private float _maxAlphaUpperBound = 0.8f;

		// Token: 0x040003BB RID: 955
		private Color _rarityCommonColor;

		// Token: 0x040003BC RID: 956
		private Color _rarityRareColor;

		// Token: 0x040003BD RID: 957
		private Color _rarityUniqueColor;

		// Token: 0x040003BE RID: 958
		private BasicContainer _animationPartContainer;
	}
}
