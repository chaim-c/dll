using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper
{
	// Token: 0x02000121 RID: 289
	public class CharacterAttributeItemVM : ViewModel
	{
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00065756 File Offset: 0x00063956
		// (set) Token: 0x06001C03 RID: 7171 RVA: 0x0006575E File Offset: 0x0006395E
		public CharacterAttribute AttributeType { get; private set; }

		// Token: 0x06001C04 RID: 7172 RVA: 0x00065768 File Offset: 0x00063968
		public CharacterAttributeItemVM(Hero hero, CharacterAttribute currAtt, CharacterVM developerVM, Action<CharacterAttributeItemVM> onInpectAttribute, Action<CharacterAttributeItemVM> onAddAttributePoint)
		{
			this._hero = hero;
			this._developer = this._hero.HeroDeveloper;
			this._characterVM = developerVM;
			this.AttributeType = currAtt;
			this._onInpectAttribute = onInpectAttribute;
			this._onAddAttributePoint = onAddAttributePoint;
			this._initialAttValue = hero.GetAttributeValue(currAtt);
			this.AttributeValue = this._initialAttValue;
			this.BoundSkills = new MBBindingList<AttributeBoundSkillItemVM>();
			this.RefreshWithCurrentValues();
			this.RefreshValues();
			this.UnspentAttributePoints = this._characterVM.UnspentAttributePoints;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000657F4 File Offset: 0x000639F4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.AttributeType.Abbreviation.ToString();
			string content = this.AttributeType.Description.ToString();
			GameTexts.SetVariable("STR1", content);
			GameTexts.SetVariable("ATTRIBUTE_NAME", this.AttributeType.Name);
			TextObject textObject = GameTexts.FindText("str_skill_attribute_bound_skills", null);
			textObject.SetTextVariable("IS_SOCIAL", (this.AttributeType == DefaultCharacterAttributes.Social) ? 1 : 0);
			GameTexts.SetVariable("STR2", textObject);
			this.Description = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
			TextObject textObject2 = GameTexts.FindText("str_skill_attribute_increase_description", null);
			textObject2.SetTextVariable("IS_SOCIAL", (this.AttributeType == DefaultCharacterAttributes.Social) ? 1 : 0);
			this.IncreaseHelpText = textObject2.ToString();
			this.BoundSkills.Clear();
			foreach (SkillObject skill in this.AttributeType.Skills)
			{
				this.BoundSkills.Add(new AttributeBoundSkillItemVM(skill));
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00065930 File Offset: 0x00063B30
		public void ExecuteInspectAttribute()
		{
			Action<CharacterAttributeItemVM> onInpectAttribute = this._onInpectAttribute;
			if (onInpectAttribute == null)
			{
				return;
			}
			onInpectAttribute(this);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00065944 File Offset: 0x00063B44
		public void ExecuteAddAttributePoint()
		{
			int attributeValue = this.AttributeValue;
			this.AttributeValue = attributeValue + 1;
			Action<CharacterAttributeItemVM> onAddAttributePoint = this._onAddAttributePoint;
			if (onAddAttributePoint != null)
			{
				onAddAttributePoint(this);
			}
			this.UnspentAttributePoints = this._characterVM.UnspentAttributePoints;
			this.RefreshWithCurrentValues();
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0006598A File Offset: 0x00063B8A
		public void Reset()
		{
			this.AttributeValue = this._initialAttValue;
			this.RefreshWithCurrentValues();
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000659A0 File Offset: 0x00063BA0
		public void RefreshWithCurrentValues()
		{
			this.UnspentAttributePoints = this._characterVM.UnspentAttributePoints;
			this.CanAddPoint = (this.AttributeValue < Campaign.Current.Models.CharacterDevelopmentModel.MaxAttribute && this._characterVM.UnspentAttributePoints > 0);
			this.IsAttributeAtMax = (this.AttributeValue >= Campaign.Current.Models.CharacterDevelopmentModel.MaxAttribute);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00065A18 File Offset: 0x00063C18
		public void Commit()
		{
			for (int i = 0; i < this.AttributeValue - this._initialAttValue; i++)
			{
				this._developer.AddAttribute(this.AttributeType, 1, true);
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x00065A50 File Offset: 0x00063C50
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x00065A58 File Offset: 0x00063C58
		[DataSourceProperty]
		public MBBindingList<AttributeBoundSkillItemVM> BoundSkills
		{
			get
			{
				return this._boundSkills;
			}
			set
			{
				if (value != this._boundSkills)
				{
					this._boundSkills = value;
					base.OnPropertyChangedWithValue<MBBindingList<AttributeBoundSkillItemVM>>(value, "BoundSkills");
				}
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x00065A76 File Offset: 0x00063C76
		// (set) Token: 0x06001C0E RID: 7182 RVA: 0x00065A7E File Offset: 0x00063C7E
		[DataSourceProperty]
		public int AttributeValue
		{
			get
			{
				return this._atttributeValue;
			}
			set
			{
				if (value != this._atttributeValue)
				{
					this._atttributeValue = value;
					base.OnPropertyChangedWithValue(value, "AttributeValue");
				}
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00065A9C File Offset: 0x00063C9C
		// (set) Token: 0x06001C10 RID: 7184 RVA: 0x00065AA4 File Offset: 0x00063CA4
		[DataSourceProperty]
		public int UnspentAttributePoints
		{
			get
			{
				return this._unspentAttributePoints;
			}
			set
			{
				if (value != this._unspentAttributePoints)
				{
					this._unspentAttributePoints = value;
					base.OnPropertyChangedWithValue(value, "UnspentAttributePoints");
					GameTexts.SetVariable("NUMBER", value);
					this.UnspentAttributePointsText = GameTexts.FindText("str_free_attribute_points", null).ToString();
				}
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00065AE3 File Offset: 0x00063CE3
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00065AEB File Offset: 0x00063CEB
		[DataSourceProperty]
		public string UnspentAttributePointsText
		{
			get
			{
				return this._unspentAttributePointsText;
			}
			set
			{
				if (value != this._unspentAttributePointsText)
				{
					this._unspentAttributePointsText = value;
					base.OnPropertyChangedWithValue<string>(value, "UnspentAttributePointsText");
				}
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00065B0E File Offset: 0x00063D0E
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x00065B16 File Offset: 0x00063D16
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x00065B39 File Offset: 0x00063D39
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x00065B41 File Offset: 0x00063D41
		[DataSourceProperty]
		public string NameExtended
		{
			get
			{
				return this._nameExtended;
			}
			set
			{
				if (value != this._nameExtended)
				{
					this._nameExtended = value;
					base.OnPropertyChangedWithValue<string>(value, "NameExtended");
				}
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x00065B64 File Offset: 0x00063D64
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x00065B6C File Offset: 0x00063D6C
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00065B8F File Offset: 0x00063D8F
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x00065B97 File Offset: 0x00063D97
		[DataSourceProperty]
		public string IncreaseHelpText
		{
			get
			{
				return this._increaseHelpText;
			}
			set
			{
				if (value != this._increaseHelpText)
				{
					this._increaseHelpText = value;
					base.OnPropertyChangedWithValue<string>(value, "IncreaseHelpText");
				}
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00065BBA File Offset: 0x00063DBA
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x00065BC2 File Offset: 0x00063DC2
		[DataSourceProperty]
		public bool IsInspecting
		{
			get
			{
				return this._isInspecting;
			}
			set
			{
				if (value != this._isInspecting)
				{
					this._isInspecting = value;
					base.OnPropertyChangedWithValue(value, "IsInspecting");
				}
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00065BE0 File Offset: 0x00063DE0
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x00065BE8 File Offset: 0x00063DE8
		[DataSourceProperty]
		public bool IsAttributeAtMax
		{
			get
			{
				return this._isAttributeAtMax;
			}
			set
			{
				if (value != this._isAttributeAtMax)
				{
					this._isAttributeAtMax = value;
					base.OnPropertyChangedWithValue(value, "IsAttributeAtMax");
				}
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x00065C06 File Offset: 0x00063E06
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x00065C0E File Offset: 0x00063E0E
		[DataSourceProperty]
		public bool CanAddPoint
		{
			get
			{
				return this._canAddPoint;
			}
			set
			{
				if (value != this._canAddPoint)
				{
					this._canAddPoint = value;
					base.OnPropertyChangedWithValue(value, "CanAddPoint");
				}
			}
		}

		// Token: 0x04000D40 RID: 3392
		private readonly Hero _hero;

		// Token: 0x04000D42 RID: 3394
		private readonly IHeroDeveloper _developer;

		// Token: 0x04000D43 RID: 3395
		private readonly int _initialAttValue;

		// Token: 0x04000D44 RID: 3396
		private readonly Action<CharacterAttributeItemVM> _onInpectAttribute;

		// Token: 0x04000D45 RID: 3397
		private readonly Action<CharacterAttributeItemVM> _onAddAttributePoint;

		// Token: 0x04000D46 RID: 3398
		private readonly CharacterVM _characterVM;

		// Token: 0x04000D47 RID: 3399
		private int _atttributeValue;

		// Token: 0x04000D48 RID: 3400
		private int _unspentAttributePoints;

		// Token: 0x04000D49 RID: 3401
		private string _unspentAttributePointsText;

		// Token: 0x04000D4A RID: 3402
		private bool _canAddPoint;

		// Token: 0x04000D4B RID: 3403
		private bool _isInspecting;

		// Token: 0x04000D4C RID: 3404
		private bool _isAttributeAtMax;

		// Token: 0x04000D4D RID: 3405
		private string _name;

		// Token: 0x04000D4E RID: 3406
		private string _nameExtended;

		// Token: 0x04000D4F RID: 3407
		private string _description;

		// Token: 0x04000D50 RID: 3408
		private string _increaseHelpText;

		// Token: 0x04000D51 RID: 3409
		private MBBindingList<AttributeBoundSkillItemVM> _boundSkills;
	}
}
