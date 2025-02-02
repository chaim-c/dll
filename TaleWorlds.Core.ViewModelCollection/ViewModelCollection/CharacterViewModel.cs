using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection
{
	// Token: 0x02000009 RID: 9
	public class CharacterViewModel : ViewModel
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000237A File Offset: 0x0000057A
		public CharacterViewModel()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002398 File Offset: 0x00000598
		public CharacterViewModel(CharacterViewModel.StanceTypes stance = CharacterViewModel.StanceTypes.None)
		{
			this._equipment = new Equipment(false);
			this.EquipmentCode = this._equipment.CalculateEquipmentCode();
			this.StanceIndex = (int)stance;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000023E8 File Offset: 0x000005E8
		public void SetEquipment(EquipmentIndex index, EquipmentElement item)
		{
			this._equipment[(int)index] = item;
			this.EquipmentCode = this._equipment.CalculateEquipmentCode();
			Equipment equipment = this._equipment;
			this.HasMount = (((equipment != null) ? equipment[10].Item : null) != null);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002438 File Offset: 0x00000638
		public virtual void SetEquipment(Equipment equipment)
		{
			this._equipment = ((equipment != null) ? equipment.Clone(false) : null);
			Equipment equipment2 = this._equipment;
			this.HasMount = (((equipment2 != null) ? equipment2[10].Item : null) != null);
			Equipment equipment3 = this._equipment;
			this.EquipmentCode = ((equipment3 != null) ? equipment3.CalculateEquipmentCode() : null);
			if (!string.IsNullOrEmpty(this.CharStringId))
			{
				this.MountCreationKey = TaleWorlds.Core.MountCreationKey.GetRandomMountKeyString((equipment != null) ? equipment[10].Item : null, Common.GetDJB2(this.CharStringId));
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000024D0 File Offset: 0x000006D0
		public void FillFrom(BasicCharacterObject character, int seed = -1)
		{
			if (FaceGen.GetMaturityTypeWithAge(character.Age) > BodyMeshMaturityType.Child)
			{
				if (character.Culture != null)
				{
					this.ArmorColor1 = character.Culture.Color;
					this.ArmorColor2 = character.Culture.Color2;
				}
				this.CharStringId = character.StringId;
				this.IsFemale = character.IsFemale;
				this.Race = character.Race;
				this.BodyProperties = character.GetBodyProperties(character.Equipment, seed).ToString();
				Equipment equipment = character.Equipment;
				this.MountCreationKey = TaleWorlds.Core.MountCreationKey.GetRandomMountKeyString((equipment != null) ? equipment[10].Item : null, Common.GetDJB2(character.StringId));
				Equipment equipment2 = character.Equipment;
				this._equipment = ((equipment2 != null) ? equipment2.Clone(false) : null);
				Equipment equipment3 = this._equipment;
				this.HasMount = (((equipment3 != null) ? equipment3[10].Item : null) != null);
				Equipment equipment4 = this._equipment;
				this.EquipmentCode = ((equipment4 != null) ? equipment4.CalculateEquipmentCode() : null);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025E8 File Offset: 0x000007E8
		public void FillFrom(CharacterViewModel characterViewModel, int seed = -1)
		{
			this.ArmorColor1 = characterViewModel.ArmorColor1;
			this.ArmorColor2 = characterViewModel.ArmorColor2;
			this.CharStringId = characterViewModel.CharStringId;
			this.IsFemale = characterViewModel.IsFemale;
			this.Race = characterViewModel.Race;
			this.BodyProperties = characterViewModel.BodyProperties;
			this.MountCreationKey = characterViewModel.MountCreationKey;
			this._equipment = characterViewModel._equipment.Clone(false);
			Equipment equipment = this._equipment;
			this.HasMount = (((equipment != null) ? equipment[10].Item : null) != null);
			Equipment equipment2 = this._equipment;
			this.EquipmentCode = ((equipment2 != null) ? equipment2.CalculateEquipmentCode() : null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002698 File Offset: 0x00000898
		public void ExecuteEquipWeaponAtIndex(EquipmentIndex index, bool isLeftHand)
		{
			Equipment equipment = this._equipment;
			bool flag;
			if (equipment == null)
			{
				flag = (null != null);
			}
			else
			{
				ItemObject item = equipment[index].Item;
				flag = (((item != null) ? item.WeaponComponent : null) != null);
			}
			if (flag)
			{
				if (isLeftHand)
				{
					this.LeftHandWieldedEquipmentIndex = (int)index;
					return;
				}
				this.RightHandWieldedEquipmentIndex = (int)index;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026E0 File Offset: 0x000008E0
		public void ExecuteStartCustomAnimation(string animation, bool loop = false, float loopInterval = 0f)
		{
			this.ExecuteStopCustomAnimation();
			this.CustomAnimation = animation;
			this.ShouldLoopCustomAnimation = loop;
			this.CustomAnimationWaitDuration = loopInterval;
			this.IsPlayingCustomAnimations = true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002704 File Offset: 0x00000904
		public void ExecuteStopCustomAnimation()
		{
			this._isManuallyStoppingAnimation = true;
			this.CustomAnimation = null;
			this.ShouldLoopCustomAnimation = false;
			this.CustomAnimationWaitDuration = 0f;
			if (this.IsPlayingCustomAnimations)
			{
				Action<CharacterViewModel> onCustomAnimationFinished = CharacterViewModel.OnCustomAnimationFinished;
				if (onCustomAnimationFinished != null)
				{
					onCustomAnimationFinished(this);
				}
			}
			this.IsPlayingCustomAnimations = false;
			this._isManuallyStoppingAnimation = false;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002758 File Offset: 0x00000958
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002760 File Offset: 0x00000960
		[DataSourceProperty]
		public string BannerCodeText
		{
			get
			{
				return this._bannerCode;
			}
			set
			{
				if (value != this._bannerCode)
				{
					this._bannerCode = value;
					base.OnPropertyChangedWithValue<string>(value, "BannerCodeText");
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002783 File Offset: 0x00000983
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000278B File Offset: 0x0000098B
		[DataSourceProperty]
		public string BodyProperties
		{
			get
			{
				return this._bodyProperties;
			}
			set
			{
				if (value != this._bodyProperties)
				{
					this._bodyProperties = value;
					base.OnPropertyChangedWithValue<string>(value, "BodyProperties");
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000027AE File Offset: 0x000009AE
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000027B6 File Offset: 0x000009B6
		[DataSourceProperty]
		public string MountCreationKey
		{
			get
			{
				return this._mountCreationKey;
			}
			set
			{
				if (value != this._mountCreationKey)
				{
					this._mountCreationKey = value;
					base.OnPropertyChangedWithValue<string>(value, "MountCreationKey");
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027D9 File Offset: 0x000009D9
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000027E1 File Offset: 0x000009E1
		[DataSourceProperty]
		public string CharStringId
		{
			get
			{
				return this._charStringId;
			}
			set
			{
				if (value != this._charStringId)
				{
					this._charStringId = value;
					base.OnPropertyChangedWithValue<string>(value, "CharStringId");
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002804 File Offset: 0x00000A04
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000280C File Offset: 0x00000A0C
		[DataSourceProperty]
		public string CustomAnimation
		{
			get
			{
				return this._customAnimation;
			}
			set
			{
				if (value != this._customAnimation)
				{
					this._customAnimation = value;
					base.OnPropertyChangedWithValue<string>(value, "CustomAnimation");
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000282F File Offset: 0x00000A2F
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002837 File Offset: 0x00000A37
		[DataSourceProperty]
		public int StanceIndex
		{
			get
			{
				return this._stanceIndex;
			}
			private set
			{
				if (value != this._stanceIndex)
				{
					this._stanceIndex = value;
					base.OnPropertyChangedWithValue(value, "StanceIndex");
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002855 File Offset: 0x00000A55
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000285D File Offset: 0x00000A5D
		[DataSourceProperty]
		public bool IsFemale
		{
			get
			{
				return this._isFemale;
			}
			set
			{
				if (value != this._isFemale)
				{
					this._isFemale = value;
					base.OnPropertyChangedWithValue(value, "IsFemale");
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000287B File Offset: 0x00000A7B
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002883 File Offset: 0x00000A83
		[DataSourceProperty]
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
			set
			{
				if (value != this._isHidden)
				{
					this._isHidden = value;
					base.OnPropertyChangedWithValue(value, "IsHidden");
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000028A1 File Offset: 0x00000AA1
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000028AC File Offset: 0x00000AAC
		[DataSourceProperty]
		public bool IsPlayingCustomAnimations
		{
			get
			{
				return this._isPlayingCustomAnimations;
			}
			set
			{
				if (value != this._isPlayingCustomAnimations)
				{
					this._isPlayingCustomAnimations = value;
					base.OnPropertyChangedWithValue(value, "IsPlayingCustomAnimations");
					if (!this._isPlayingCustomAnimations && !this._isManuallyStoppingAnimation && !this.ShouldLoopCustomAnimation)
					{
						Action<CharacterViewModel> onCustomAnimationFinished = CharacterViewModel.OnCustomAnimationFinished;
						if (onCustomAnimationFinished == null)
						{
							return;
						}
						onCustomAnimationFinished(this);
					}
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000028FD File Offset: 0x00000AFD
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002905 File Offset: 0x00000B05
		[DataSourceProperty]
		public bool ShouldLoopCustomAnimation
		{
			get
			{
				return this._shouldLoopCustomAnimation;
			}
			set
			{
				if (value != this._shouldLoopCustomAnimation)
				{
					this._shouldLoopCustomAnimation = value;
					base.OnPropertyChangedWithValue(value, "ShouldLoopCustomAnimation");
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002923 File Offset: 0x00000B23
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000292B File Offset: 0x00000B2B
		[DataSourceProperty]
		public float CustomAnimationProgressRatio
		{
			get
			{
				return this._customAnimationProgressRatio;
			}
			set
			{
				if (value != this._customAnimationProgressRatio)
				{
					this._customAnimationProgressRatio = value;
					base.OnPropertyChangedWithValue(value, "CustomAnimationProgressRatio");
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002949 File Offset: 0x00000B49
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002951 File Offset: 0x00000B51
		[DataSourceProperty]
		public float CustomAnimationWaitDuration
		{
			get
			{
				return this._customAnimationWaitDuration;
			}
			set
			{
				if (value != this._customAnimationWaitDuration)
				{
					this._customAnimationWaitDuration = value;
					base.OnPropertyChangedWithValue(value, "CustomAnimationWaitDuration");
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000296F File Offset: 0x00000B6F
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002977 File Offset: 0x00000B77
		[DataSourceProperty]
		public int Race
		{
			get
			{
				return this._race;
			}
			set
			{
				if (value != this._race)
				{
					this._race = value;
					base.OnPropertyChangedWithValue(value, "Race");
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002995 File Offset: 0x00000B95
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000299D File Offset: 0x00000B9D
		[DataSourceProperty]
		public bool HasMount
		{
			get
			{
				return this._hasMount;
			}
			set
			{
				if (value != this._hasMount)
				{
					this._hasMount = value;
					base.OnPropertyChangedWithValue(value, "HasMount");
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000029BB File Offset: 0x00000BBB
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000029C3 File Offset: 0x00000BC3
		[DataSourceProperty]
		public string EquipmentCode
		{
			get
			{
				return this._equipmentCode;
			}
			set
			{
				if (value != this._equipmentCode)
				{
					this._equipmentCode = value;
					base.OnPropertyChangedWithValue<string>(value, "EquipmentCode");
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000029E6 File Offset: 0x00000BE6
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000029EE File Offset: 0x00000BEE
		[DataSourceProperty]
		public string IdleAction
		{
			get
			{
				return this._idleAction;
			}
			set
			{
				if (value != this._idleAction)
				{
					this._idleAction = value;
					base.OnPropertyChangedWithValue<string>(value, "IdleAction");
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002A11 File Offset: 0x00000C11
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002A19 File Offset: 0x00000C19
		[DataSourceProperty]
		public string IdleFaceAnim
		{
			get
			{
				return this._idleFaceAnim;
			}
			set
			{
				if (value != this._idleFaceAnim)
				{
					this._idleFaceAnim = value;
					base.OnPropertyChangedWithValue<string>(value, "IdleFaceAnim");
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002A44 File Offset: 0x00000C44
		[DataSourceProperty]
		public uint ArmorColor1
		{
			get
			{
				return this._armorColor1;
			}
			set
			{
				if (value != this._armorColor1)
				{
					this._armorColor1 = value;
					base.OnPropertyChangedWithValue(value, "ArmorColor1");
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002A62 File Offset: 0x00000C62
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002A6A File Offset: 0x00000C6A
		[DataSourceProperty]
		public uint ArmorColor2
		{
			get
			{
				return this._armorColor2;
			}
			set
			{
				if (value != this._armorColor2)
				{
					this._armorColor2 = value;
					base.OnPropertyChangedWithValue(value, "ArmorColor2");
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002A88 File Offset: 0x00000C88
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002A90 File Offset: 0x00000C90
		[DataSourceProperty]
		public int LeftHandWieldedEquipmentIndex
		{
			get
			{
				return this._leftHandWieldedEquipmentIndex;
			}
			set
			{
				if (value != this._leftHandWieldedEquipmentIndex)
				{
					this._leftHandWieldedEquipmentIndex = value;
					base.OnPropertyChangedWithValue(value, "LeftHandWieldedEquipmentIndex");
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002AAE File Offset: 0x00000CAE
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002AB6 File Offset: 0x00000CB6
		[DataSourceProperty]
		public int RightHandWieldedEquipmentIndex
		{
			get
			{
				return this._rightHandWieldedEquipmentIndex;
			}
			set
			{
				if (value != this._rightHandWieldedEquipmentIndex)
				{
					this._rightHandWieldedEquipmentIndex = value;
					base.OnPropertyChangedWithValue(value, "RightHandWieldedEquipmentIndex");
				}
			}
		}

		// Token: 0x04000009 RID: 9
		public static Action<CharacterViewModel> OnCustomAnimationFinished;

		// Token: 0x0400000A RID: 10
		private bool _isManuallyStoppingAnimation;

		// Token: 0x0400000B RID: 11
		protected Equipment _equipment;

		// Token: 0x0400000C RID: 12
		private string _mountCreationKey = "";

		// Token: 0x0400000D RID: 13
		private string _bodyProperties = "";

		// Token: 0x0400000E RID: 14
		private string _equipmentCode;

		// Token: 0x0400000F RID: 15
		private string _idleAction;

		// Token: 0x04000010 RID: 16
		private string _idleFaceAnim;

		// Token: 0x04000011 RID: 17
		private string _charStringId;

		// Token: 0x04000012 RID: 18
		private string _customAnimation;

		// Token: 0x04000013 RID: 19
		protected string _bannerCode;

		// Token: 0x04000014 RID: 20
		private bool _hasMount;

		// Token: 0x04000015 RID: 21
		private bool _isFemale;

		// Token: 0x04000016 RID: 22
		private bool _isHidden;

		// Token: 0x04000017 RID: 23
		private bool _isPlayingCustomAnimations;

		// Token: 0x04000018 RID: 24
		private bool _shouldLoopCustomAnimation;

		// Token: 0x04000019 RID: 25
		private float _customAnimationProgressRatio;

		// Token: 0x0400001A RID: 26
		private float _customAnimationWaitDuration;

		// Token: 0x0400001B RID: 27
		private int _race;

		// Token: 0x0400001C RID: 28
		private int _stanceIndex;

		// Token: 0x0400001D RID: 29
		private uint _armorColor1;

		// Token: 0x0400001E RID: 30
		private uint _armorColor2;

		// Token: 0x0400001F RID: 31
		private int _leftHandWieldedEquipmentIndex;

		// Token: 0x04000020 RID: 32
		private int _rightHandWieldedEquipmentIndex;

		// Token: 0x02000029 RID: 41
		public enum StanceTypes
		{
			// Token: 0x040000C4 RID: 196
			None,
			// Token: 0x040000C5 RID: 197
			EmphasizeFace,
			// Token: 0x040000C6 RID: 198
			SideView,
			// Token: 0x040000C7 RID: 199
			CelebrateVictory,
			// Token: 0x040000C8 RID: 200
			OnMount
		}
	}
}
