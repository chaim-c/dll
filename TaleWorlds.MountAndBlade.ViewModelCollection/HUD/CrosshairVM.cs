using System;
using System.Collections.ObjectModel;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x0200003F RID: 63
	public class CrosshairVM : ViewModel
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x000171B6 File Offset: 0x000153B6
		public CrosshairVM()
		{
			this.ReloadPhases = new MBBindingList<ReloadPhaseItemVM>();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000171C9 File Offset: 0x000153C9
		public void SetProperties(double accuracy, double scale)
		{
			this.CrosshairAccuracy = accuracy;
			this.CrosshairScale = scale;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000171D9 File Offset: 0x000153D9
		public void SetArrowProperties(double topArrowOpacity, double rightArrowOpacity, double bottomArrowOpacity, double leftArrowOpacity)
		{
			this.TopArrowOpacity = topArrowOpacity;
			this.BottomArrowOpacity = bottomArrowOpacity;
			this.RightArrowOpacity = rightArrowOpacity;
			this.LeftArrowOpacity = leftArrowOpacity;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000171F8 File Offset: 0x000153F8
		public void SetReloadProperties(in StackArray.StackArray10FloatFloatTuple reloadPhases, int reloadPhaseCount)
		{
			if (reloadPhaseCount == 0)
			{
				this.IsReloadPhasesVisible = false;
			}
			else
			{
				for (int i = 0; i < reloadPhaseCount; i++)
				{
					StackArray.StackArray10FloatFloatTuple stackArray10FloatFloatTuple = reloadPhases;
					if (stackArray10FloatFloatTuple[i].Item1 < 1f)
					{
						this.IsReloadPhasesVisible = true;
						break;
					}
				}
			}
			this.PopulateReloadPhases(reloadPhases, reloadPhaseCount);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001724C File Offset: 0x0001544C
		private void PopulateReloadPhases(in StackArray.StackArray10FloatFloatTuple reloadPhases, int reloadPhaseCount)
		{
			if (reloadPhaseCount != this.ReloadPhases.Count)
			{
				this.ReloadPhases.Clear();
				for (int i = 0; i < reloadPhaseCount; i++)
				{
					Collection<ReloadPhaseItemVM> reloadPhases2 = this.ReloadPhases;
					StackArray.StackArray10FloatFloatTuple stackArray10FloatFloatTuple = reloadPhases;
					float item = stackArray10FloatFloatTuple[i].Item1;
					stackArray10FloatFloatTuple = reloadPhases;
					reloadPhases2.Add(new ReloadPhaseItemVM(item, stackArray10FloatFloatTuple[i].Item2));
				}
				return;
			}
			for (int j = 0; j < reloadPhaseCount; j++)
			{
				ReloadPhaseItemVM reloadPhaseItemVM = this.ReloadPhases[j];
				StackArray.StackArray10FloatFloatTuple stackArray10FloatFloatTuple = reloadPhases;
				float item2 = stackArray10FloatFloatTuple[j].Item1;
				stackArray10FloatFloatTuple = reloadPhases;
				reloadPhaseItemVM.Update(item2, stackArray10FloatFloatTuple[j].Item2);
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000172FC File Offset: 0x000154FC
		public void ShowHitMarker(bool isVictimDead, bool isHumanoidHeadShot)
		{
			this.IsVictimDead = isVictimDead;
			this.IsHitMarkerVisible = false;
			this.IsHitMarkerVisible = true;
			this.IsHumanoidHeadshot = false;
			this.IsHumanoidHeadshot = isHumanoidHeadShot;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00017321 File Offset: 0x00015521
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00017329 File Offset: 0x00015529
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00017347 File Offset: 0x00015547
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001734F File Offset: 0x0001554F
		[DataSourceProperty]
		public bool IsReloadPhasesVisible
		{
			get
			{
				return this._isReloadPhasesVisible;
			}
			set
			{
				if (value != this._isReloadPhasesVisible)
				{
					this._isReloadPhasesVisible = value;
					base.OnPropertyChangedWithValue(value, "IsReloadPhasesVisible");
				}
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001736D File Offset: 0x0001556D
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x00017375 File Offset: 0x00015575
		[DataSourceProperty]
		public bool IsHitMarkerVisible
		{
			get
			{
				return this._isHitMarkerVisible;
			}
			set
			{
				if (value != this._isHitMarkerVisible)
				{
					this._isHitMarkerVisible = value;
					base.OnPropertyChangedWithValue(value, "IsHitMarkerVisible");
				}
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00017393 File Offset: 0x00015593
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0001739B File Offset: 0x0001559B
		[DataSourceProperty]
		public bool IsVictimDead
		{
			get
			{
				return this._isVictimDead;
			}
			set
			{
				if (value != this._isVictimDead)
				{
					this._isVictimDead = value;
					base.OnPropertyChangedWithValue(value, "IsVictimDead");
				}
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000173B9 File Offset: 0x000155B9
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x000173C1 File Offset: 0x000155C1
		[DataSourceProperty]
		public bool IsHumanoidHeadshot
		{
			get
			{
				return this._isHumanoidHeadshot;
			}
			set
			{
				if (value != this._isHumanoidHeadshot)
				{
					this._isHumanoidHeadshot = value;
					base.OnPropertyChangedWithValue(value, "IsHumanoidHeadshot");
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000173DF File Offset: 0x000155DF
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x000173E7 File Offset: 0x000155E7
		[DataSourceProperty]
		public double TopArrowOpacity
		{
			get
			{
				return this._topArrowOpacity;
			}
			set
			{
				if (value != this._topArrowOpacity)
				{
					this._topArrowOpacity = value;
					base.OnPropertyChangedWithValue(value, "TopArrowOpacity");
				}
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00017405 File Offset: 0x00015605
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001740D File Offset: 0x0001560D
		[DataSourceProperty]
		public MBBindingList<ReloadPhaseItemVM> ReloadPhases
		{
			get
			{
				return this._reloadPhases;
			}
			set
			{
				if (value != this._reloadPhases)
				{
					this._reloadPhases = value;
					base.OnPropertyChangedWithValue<MBBindingList<ReloadPhaseItemVM>>(value, "ReloadPhases");
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001742B File Offset: 0x0001562B
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00017433 File Offset: 0x00015633
		[DataSourceProperty]
		public double BottomArrowOpacity
		{
			get
			{
				return this._bottomArrowOpacity;
			}
			set
			{
				if (value != this._bottomArrowOpacity)
				{
					this._bottomArrowOpacity = value;
					base.OnPropertyChangedWithValue(value, "BottomArrowOpacity");
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00017451 File Offset: 0x00015651
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00017459 File Offset: 0x00015659
		[DataSourceProperty]
		public double RightArrowOpacity
		{
			get
			{
				return this._rightArrowOpacity;
			}
			set
			{
				if (value != this._rightArrowOpacity)
				{
					this._rightArrowOpacity = value;
					base.OnPropertyChangedWithValue(value, "RightArrowOpacity");
				}
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00017477 File Offset: 0x00015677
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001747F File Offset: 0x0001567F
		[DataSourceProperty]
		public double LeftArrowOpacity
		{
			get
			{
				return this._leftArrowOpacity;
			}
			set
			{
				if (value != this._leftArrowOpacity)
				{
					this._leftArrowOpacity = value;
					base.OnPropertyChangedWithValue(value, "LeftArrowOpacity");
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001749D File Offset: 0x0001569D
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x000174A5 File Offset: 0x000156A5
		[DataSourceProperty]
		public bool IsTargetInvalid
		{
			get
			{
				return this._isTargetInvalid;
			}
			set
			{
				if (value != this._isTargetInvalid)
				{
					this._isTargetInvalid = value;
					base.OnPropertyChangedWithValue(value, "IsTargetInvalid");
				}
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000174C3 File Offset: 0x000156C3
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000174CB File Offset: 0x000156CB
		[DataSourceProperty]
		public double CrosshairAccuracy
		{
			get
			{
				return this._crosshairAccuracy;
			}
			set
			{
				if (value != this._crosshairAccuracy)
				{
					this._crosshairAccuracy = value;
					base.OnPropertyChangedWithValue(value, "CrosshairAccuracy");
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000174E9 File Offset: 0x000156E9
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000174F1 File Offset: 0x000156F1
		[DataSourceProperty]
		public double CrosshairScale
		{
			get
			{
				return this._crosshairScale;
			}
			set
			{
				if (value != this._crosshairScale)
				{
					this._crosshairScale = value;
					base.OnPropertyChangedWithValue(value, "CrosshairScale");
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001750F File Offset: 0x0001570F
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00017517 File Offset: 0x00015717
		[DataSourceProperty]
		public int CrosshairType
		{
			get
			{
				return this._crosshairType;
			}
			set
			{
				if (value != this._crosshairType)
				{
					this._crosshairType = value;
					base.OnPropertyChangedWithValue(value, "CrosshairType");
				}
			}
		}

		// Token: 0x0400029A RID: 666
		private bool _isVisible;

		// Token: 0x0400029B RID: 667
		private bool _isReloadPhasesVisible;

		// Token: 0x0400029C RID: 668
		private bool _isHitMarkerVisible;

		// Token: 0x0400029D RID: 669
		private bool _isVictimDead;

		// Token: 0x0400029E RID: 670
		private bool _isHumanoidHeadshot;

		// Token: 0x0400029F RID: 671
		private bool _isTargetInvalid;

		// Token: 0x040002A0 RID: 672
		private MBBindingList<ReloadPhaseItemVM> _reloadPhases;

		// Token: 0x040002A1 RID: 673
		private double _crosshairAccuracy;

		// Token: 0x040002A2 RID: 674
		private double _crosshairScale;

		// Token: 0x040002A3 RID: 675
		private double _topArrowOpacity;

		// Token: 0x040002A4 RID: 676
		private double _bottomArrowOpacity;

		// Token: 0x040002A5 RID: 677
		private double _rightArrowOpacity;

		// Token: 0x040002A6 RID: 678
		private double _leftArrowOpacity;

		// Token: 0x040002A7 RID: 679
		private int _crosshairType;
	}
}
