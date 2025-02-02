using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x02000014 RID: 20
	public class NameplateVM : ViewModel
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009720 File Offset: 0x00007920
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009728 File Offset: 0x00007928
		public double Scale { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009731 File Offset: 0x00007931
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00009739 File Offset: 0x00007939
		public int NameplateOrder { get; set; }

		// Token: 0x060001D8 RID: 472 RVA: 0x00009742 File Offset: 0x00007942
		public NameplateVM()
		{
			if (Game.Current != null)
			{
				Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementChanged));
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000976C File Offset: 0x0000796C
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementChanged));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000978F File Offset: 0x0000798F
		private void OnTutorialNotificationElementChanged(TutorialNotificationElementChangeEvent obj)
		{
			this.RefreshTutorialStatus(((obj != null) ? obj.NewNotificationElementID : null) ?? string.Empty);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000097AC File Offset: 0x000079AC
		public virtual void Initialize(GameEntity strategicEntity)
		{
			this.SizeType = 1;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000097B5 File Offset: 0x000079B5
		public virtual void RefreshDynamicProperties(bool forceUpdate)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual void RefreshPosition()
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000097B9 File Offset: 0x000079B9
		public virtual void RefreshRelationStatus()
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000097BB File Offset: 0x000079BB
		public virtual void RefreshTutorialStatus(string newTutorialHighlightElementID)
		{
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000097BD File Offset: 0x000079BD
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000097C5 File Offset: 0x000079C5
		public int SizeType
		{
			get
			{
				return this._sizeType;
			}
			set
			{
				if (value != this._sizeType)
				{
					this._sizeType = value;
					base.OnPropertyChangedWithValue(value, "SizeType");
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000097E3 File Offset: 0x000079E3
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000097EB File Offset: 0x000079EB
		public string FactionColor
		{
			get
			{
				return this._factionColor;
			}
			set
			{
				if (value != this._factionColor)
				{
					this._factionColor = value;
					base.OnPropertyChangedWithValue<string>(value, "FactionColor");
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000980E File Offset: 0x00007A0E
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00009816 File Offset: 0x00007A16
		public float DistanceToCamera
		{
			get
			{
				return this._distanceToCamera;
			}
			set
			{
				if (value != this._distanceToCamera)
				{
					this._distanceToCamera = value;
					base.OnPropertyChangedWithValue(value, "DistanceToCamera");
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009834 File Offset: 0x00007A34
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000983C File Offset: 0x00007A3C
		public bool IsVisibleOnMap
		{
			get
			{
				return this._isVisibleOnMap;
			}
			set
			{
				if (value != this._isVisibleOnMap)
				{
					this._isVisibleOnMap = value;
					base.OnPropertyChangedWithValue(value, "IsVisibleOnMap");
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000985A File Offset: 0x00007A5A
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00009862 File Offset: 0x00007A62
		public bool IsTargetedByTutorial
		{
			get
			{
				return this._isTargetedByTutorial;
			}
			set
			{
				if (value != this._isTargetedByTutorial)
				{
					this._isTargetedByTutorial = value;
					base.OnPropertyChangedWithValue(value, "IsTargetedByTutorial");
					base.OnPropertyChanged("ShouldShowFullName");
					base.OnPropertyChanged("IsTracked");
				}
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009896 File Offset: 0x00007A96
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000989E File Offset: 0x00007A9E
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x040000CE RID: 206
		protected bool _bindIsTargetedByTutorial;

		// Token: 0x040000CF RID: 207
		private Vec2 _position;

		// Token: 0x040000D0 RID: 208
		private bool _isVisibleOnMap;

		// Token: 0x040000D1 RID: 209
		private string _factionColor;

		// Token: 0x040000D2 RID: 210
		private int _sizeType;

		// Token: 0x040000D3 RID: 211
		private bool _isTargetedByTutorial;

		// Token: 0x040000D4 RID: 212
		private float _distanceToCamera;

		// Token: 0x02000060 RID: 96
		protected enum NameplateSize
		{
			// Token: 0x040002D8 RID: 728
			Small,
			// Token: 0x040002D9 RID: 729
			Normal,
			// Token: 0x040002DA RID: 730
			Big
		}
	}
}
