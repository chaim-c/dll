using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000204 RID: 516
	public class CustomBattleCombatant : IBattleCombatant
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x00063734 File Offset: 0x00061934
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0006373C File Offset: 0x0006193C
		public TextObject Name { get; private set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00063745 File Offset: 0x00061945
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0006374D File Offset: 0x0006194D
		public BattleSideEnum Side { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00063756 File Offset: 0x00061956
		public BasicCharacterObject General
		{
			get
			{
				return this._general;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0006375E File Offset: 0x0006195E
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x00063766 File Offset: 0x00061966
		public BasicCultureObject BasicCulture { get; private set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0006376F File Offset: 0x0006196F
		public Tuple<uint, uint> PrimaryColorPair
		{
			get
			{
				return new Tuple<uint, uint>(this.Banner.GetPrimaryColor(), this.Banner.GetFirstIconColor());
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0006378C File Offset: 0x0006198C
		public Tuple<uint, uint> AlternativeColorPair
		{
			get
			{
				return new Tuple<uint, uint>(this.Banner.GetFirstIconColor(), this.Banner.GetPrimaryColor());
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000637A9 File Offset: 0x000619A9
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x000637B1 File Offset: 0x000619B1
		public Banner Banner { get; private set; }

		// Token: 0x06001C84 RID: 7300 RVA: 0x000637BA File Offset: 0x000619BA
		public int GetTacticsSkillAmount()
		{
			if (this._characters.Count > 0)
			{
				return this._characters.Max((BasicCharacterObject h) => h.GetSkillValue(DefaultSkills.Tactics));
			}
			return 0;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000637F6 File Offset: 0x000619F6
		public IEnumerable<BasicCharacterObject> Characters
		{
			get
			{
				return this._characters.AsReadOnly();
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00063803 File Offset: 0x00061A03
		// (set) Token: 0x06001C87 RID: 7303 RVA: 0x0006380B File Offset: 0x00061A0B
		public int NumberOfAllMembers { get; private set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x00063814 File Offset: 0x00061A14
		public int NumberOfHealthyMembers
		{
			get
			{
				return this._characters.Count;
			}
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x00063821 File Offset: 0x00061A21
		public CustomBattleCombatant(TextObject name, BasicCultureObject culture, Banner banner)
		{
			this.Name = name;
			this.BasicCulture = culture;
			this.Banner = banner;
			this._characters = new List<BasicCharacterObject>();
			this._general = null;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00063850 File Offset: 0x00061A50
		public void AddCharacter(BasicCharacterObject characterObject, int number)
		{
			for (int i = 0; i < number; i++)
			{
				this._characters.Add(characterObject);
			}
			this.NumberOfAllMembers += number;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00063883 File Offset: 0x00061A83
		public void SetGeneral(BasicCharacterObject generalCharacter)
		{
			this._general = generalCharacter;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0006388C File Offset: 0x00061A8C
		public void KillCharacter(BasicCharacterObject character)
		{
			this._characters.Remove(character);
		}

		// Token: 0x04000922 RID: 2338
		private List<BasicCharacterObject> _characters;

		// Token: 0x04000923 RID: 2339
		private BasicCharacterObject _general;
	}
}
