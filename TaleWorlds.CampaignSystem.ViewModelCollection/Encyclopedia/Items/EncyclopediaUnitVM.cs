using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000D2 RID: 210
	public class EncyclopediaUnitVM : ViewModel
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0004C30C File Offset: 0x0004A50C
		public EncyclopediaUnitVM(CharacterObject character, bool isActive)
		{
			if (character != null)
			{
				CharacterCode characterCode = CharacterCode.CreateFrom(character);
				this.ImageIdentifier = new ImageIdentifierVM(characterCode);
				this._character = character;
				this.IsActiveUnit = isActive;
				this.TierIconData = CampaignUIHelper.GetCharacterTierData(character, true);
				this.TypeIconData = CampaignUIHelper.GetCharacterTypeData(character, true);
			}
			else
			{
				this.IsActiveUnit = false;
			}
			this.RefreshValues();
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004C36C File Offset: 0x0004A56C
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._character != null)
			{
				this.NameText = this._character.Name.ToString();
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004C392 File Offset: 0x0004A592
		public void ExecuteLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._character.EncyclopediaLink);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004C3AE File Offset: 0x0004A5AE
		public virtual void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(typeof(CharacterObject), new object[]
			{
				this._character
			});
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004C3CE File Offset: 0x0004A5CE
		public virtual void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0004C3D5 File Offset: 0x0004A5D5
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x0004C3DD File Offset: 0x0004A5DD
		[DataSourceProperty]
		public bool IsActiveUnit
		{
			get
			{
				return this._isActiveUnit;
			}
			set
			{
				if (value != this._isActiveUnit)
				{
					this._isActiveUnit = value;
					base.OnPropertyChangedWithValue(value, "IsActiveUnit");
				}
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0004C3FB File Offset: 0x0004A5FB
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0004C403 File Offset: 0x0004A603
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0004C421 File Offset: 0x0004A621
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0004C429 File Offset: 0x0004A629
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0004C44C File Offset: 0x0004A64C
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x0004C454 File Offset: 0x0004A654
		[DataSourceProperty]
		public StringItemWithHintVM TierIconData
		{
			get
			{
				return this._tierIconData;
			}
			set
			{
				if (value != this._tierIconData)
				{
					this._tierIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TierIconData");
				}
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0004C472 File Offset: 0x0004A672
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x0004C47A File Offset: 0x0004A67A
		[DataSourceProperty]
		public StringItemWithHintVM TypeIconData
		{
			get
			{
				return this._typeIconData;
			}
			set
			{
				if (value != this._typeIconData)
				{
					this._typeIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TypeIconData");
				}
			}
		}

		// Token: 0x0400092E RID: 2350
		private CharacterObject _character;

		// Token: 0x0400092F RID: 2351
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x04000930 RID: 2352
		private string _nameText;

		// Token: 0x04000931 RID: 2353
		private bool _isActiveUnit;

		// Token: 0x04000932 RID: 2354
		private StringItemWithHintVM _tierIconData;

		// Token: 0x04000933 RID: 2355
		private StringItemWithHintVM _typeIconData;
	}
}
