using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000E7 RID: 231
	public class WeaponAttributeVM : ViewModel
	{
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00050AFC File Offset: 0x0004ECFC
		public DamageTypes DamageType { get; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00050B04 File Offset: 0x0004ED04
		public CraftingTemplate.CraftingStatTypes AttributeType { get; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00050B0C File Offset: 0x0004ED0C
		public float AttributeValue { get; }

		// Token: 0x06001582 RID: 5506 RVA: 0x00050B14 File Offset: 0x0004ED14
		public WeaponAttributeVM(CraftingTemplate.CraftingStatTypes type, DamageTypes damageType, string attributeName, float attributeValue)
		{
			this.AttributeType = type;
			this.DamageType = damageType;
			this.AttributeValue = attributeValue;
			string str = (this.AttributeValue > 100f) ? attributeValue.ToString("F0") : attributeValue.ToString("F1");
			string variable = "<span style=\"Value\">" + str + "</span>";
			TextObject textObject = new TextObject("{=!}{ATTR_NAME}{ATTR_VALUE_RTT}", null);
			textObject.SetTextVariable("ATTR_NAME", attributeName);
			textObject.SetTextVariable("ATTR_VALUE_RTT", variable);
			this.AttributeFieldText = textObject.ToString();
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00050BA8 File Offset: 0x0004EDA8
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x00050BB0 File Offset: 0x0004EDB0
		[DataSourceProperty]
		public string AttributeFieldText
		{
			get
			{
				return this._attributeFieldText;
			}
			set
			{
				if (value != this._attributeFieldText)
				{
					this._attributeFieldText = value;
					base.OnPropertyChangedWithValue<string>(value, "AttributeFieldText");
				}
			}
		}

		// Token: 0x04000A01 RID: 2561
		private string _attributeFieldText;
	}
}
