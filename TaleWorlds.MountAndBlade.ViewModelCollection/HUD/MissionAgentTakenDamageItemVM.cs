using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000043 RID: 67
	public class MissionAgentTakenDamageItemVM : ViewModel
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x0001784F File Offset: 0x00015A4F
		public MissionAgentTakenDamageItemVM(Camera missionCamera, Vec3 affectorAgentPos, int damage, bool isRanged, Action<MissionAgentTakenDamageItemVM> onRemove)
		{
			this._affectorAgentPosition = affectorAgentPos;
			this.Damage = damage;
			this.IsRanged = isRanged;
			this._missionCamera = missionCamera;
			this._onRemove = onRemove;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001787C File Offset: 0x00015A7C
		internal void Update()
		{
			if (this.IsRanged)
			{
				float a = 0f;
				float b = 0f;
				float num = 0f;
				MBWindowManager.WorldToScreen(this._missionCamera, this._affectorAgentPosition, ref a, ref b, ref num);
				this.ScreenPosOfAffectorAgent = new Vec2(a, b);
				this.IsBehind = (num < 0f);
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000178D6 File Offset: 0x00015AD6
		public void ExecuteRemove()
		{
			this._onRemove(this);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000178E4 File Offset: 0x00015AE4
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x000178EC File Offset: 0x00015AEC
		[DataSourceProperty]
		public int Damage
		{
			get
			{
				return this._damage;
			}
			set
			{
				if (value != this._damage)
				{
					this._damage = value;
					base.OnPropertyChangedWithValue(value, "Damage");
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001790A File Offset: 0x00015B0A
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00017912 File Offset: 0x00015B12
		[DataSourceProperty]
		public bool IsRanged
		{
			get
			{
				return this._isRanged;
			}
			set
			{
				if (value != this._isRanged)
				{
					this._isRanged = value;
					base.OnPropertyChangedWithValue(value, "IsRanged");
				}
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00017930 File Offset: 0x00015B30
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00017938 File Offset: 0x00015B38
		[DataSourceProperty]
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (value != this._isBehind)
				{
					this._isBehind = value;
					base.OnPropertyChangedWithValue(value, "IsBehind");
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00017956 File Offset: 0x00015B56
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001795E File Offset: 0x00015B5E
		[DataSourceProperty]
		public Vec2 ScreenPosOfAffectorAgent
		{
			get
			{
				return this._screenPosOfAffectorAgent;
			}
			set
			{
				if (value != this._screenPosOfAffectorAgent)
				{
					this._screenPosOfAffectorAgent = value;
					base.OnPropertyChangedWithValue(value, "ScreenPosOfAffectorAgent");
				}
			}
		}

		// Token: 0x040002B1 RID: 689
		private Action<MissionAgentTakenDamageItemVM> _onRemove;

		// Token: 0x040002B2 RID: 690
		private Vec3 _affectorAgentPosition;

		// Token: 0x040002B3 RID: 691
		private Camera _missionCamera;

		// Token: 0x040002B4 RID: 692
		private int _damage;

		// Token: 0x040002B5 RID: 693
		private bool _isBehind;

		// Token: 0x040002B6 RID: 694
		private bool _isRanged;

		// Token: 0x040002B7 RID: 695
		private Vec2 _screenPosOfAffectorAgent;
	}
}
