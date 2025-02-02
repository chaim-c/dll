using System;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Engine;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A8 RID: 168
	public class ConversationAnimationToolCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0003A14F File Offset: 0x0003834F
		public override void RegisterEvents()
		{
			CampaignEvents.TickEvent.AddNonSerializedListener(this, new Action<float>(this.Tick));
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0003A168 File Offset: 0x00038368
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003A16C File Offset: 0x0003836C
		private void Tick(float dt)
		{
			if (ConversationAnimationToolCampaignBehavior._isToolEnabled)
			{
				ConversationAnimationToolCampaignBehavior.StartImGUIWindow("Conversation Animation Test Tool");
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character Type:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("0 for noble, 1 for notable, 2 for companion, 3 for troop", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter character type: ", ref ConversationAnimationToolCampaignBehavior._characterType, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character State:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("0 for active, 1 for prisoner", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter character state: ", ref ConversationAnimationToolCampaignBehavior._characterState, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character Gender:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("0 for male, 1 for female", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter character gender: ", ref ConversationAnimationToolCampaignBehavior._characterGender, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character Age:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Enter a custom age or leave -1 for not changing the age value", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter character age: ", ref ConversationAnimationToolCampaignBehavior._characterAge, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character Wounded State:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Change to 1 to change character state to wounded", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter character wounded state: ", ref ConversationAnimationToolCampaignBehavior._characterWoundedState, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character Equipment Type:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Change to 1 to change to equipment to civilian, default equipment is battle", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter equipment type: ", ref ConversationAnimationToolCampaignBehavior._equipmentType, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character Relation With Main Hero:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Leave -1 for no change, 0 for enemy, 1 for neutral, 2 for friend", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter relation type: ", ref ConversationAnimationToolCampaignBehavior._relationType, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Character Persona Type:", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUITextArea("Leave -1 for no change, 0 for curt, 1 for earnest, 2 for ironic, 3 for softspoken", false, false);
				ConversationAnimationToolCampaignBehavior.ImGUIIntegerField("Enter persona type: ", ref ConversationAnimationToolCampaignBehavior._personaType, false, false);
				ConversationAnimationToolCampaignBehavior.Separator();
				if (ConversationAnimationToolCampaignBehavior.ImGUIButton(" Start Conversation ", true))
				{
					ConversationAnimationToolCampaignBehavior.StartConversation();
				}
				ConversationAnimationToolCampaignBehavior.EndImGUIWindow();
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0003A314 File Offset: 0x00038514
		public static void CloseConversationAnimationTool()
		{
			ConversationAnimationToolCampaignBehavior._isToolEnabled = false;
			ConversationAnimationToolCampaignBehavior._characterType = -1;
			ConversationAnimationToolCampaignBehavior._characterState = -1;
			ConversationAnimationToolCampaignBehavior._characterGender = -1;
			ConversationAnimationToolCampaignBehavior._characterAge = -1;
			ConversationAnimationToolCampaignBehavior._characterWoundedState = -1;
			ConversationAnimationToolCampaignBehavior._equipmentType = -1;
			ConversationAnimationToolCampaignBehavior._relationType = -1;
			ConversationAnimationToolCampaignBehavior._personaType = -1;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003A34C File Offset: 0x0003854C
		private static void StartConversation()
		{
			bool flag = true;
			bool flag2 = true;
			Occupation occupation = Occupation.NotAssigned;
			switch (ConversationAnimationToolCampaignBehavior._characterType)
			{
			case 0:
				occupation = Occupation.Lord;
				break;
			case 1:
				occupation = Occupation.Merchant;
				break;
			case 2:
				occupation = Occupation.Wanderer;
				break;
			case 3:
				occupation = Occupation.Soldier;
				flag2 = false;
				break;
			default:
				flag = false;
				break;
			}
			if (!flag)
			{
				return;
			}
			bool flag3 = false;
			bool flag4 = false;
			if (ConversationAnimationToolCampaignBehavior._characterState == 0)
			{
				flag3 = true;
			}
			else if (ConversationAnimationToolCampaignBehavior._characterState == 1)
			{
				flag4 = true;
			}
			else
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			bool flag5 = false;
			if (ConversationAnimationToolCampaignBehavior._characterGender == 1)
			{
				flag5 = true;
			}
			else if (ConversationAnimationToolCampaignBehavior._characterGender == 0)
			{
				flag5 = false;
			}
			else
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			bool flag6 = false;
			if (ConversationAnimationToolCampaignBehavior._characterAge == -1)
			{
				flag6 = false;
			}
			else if (ConversationAnimationToolCampaignBehavior._characterAge > 0 && ConversationAnimationToolCampaignBehavior._characterAge <= 128)
			{
				flag6 = true;
			}
			else
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			bool flag7 = ConversationAnimationToolCampaignBehavior._characterWoundedState == 1;
			bool flag8 = ConversationAnimationToolCampaignBehavior._equipmentType == 1;
			if (ConversationAnimationToolCampaignBehavior._relationType != 0 && ConversationAnimationToolCampaignBehavior._relationType != 1 && ConversationAnimationToolCampaignBehavior._relationType != 2)
			{
				return;
			}
			CharacterObject characterObject = null;
			if (flag2)
			{
				Hero hero = null;
				foreach (Hero hero2 in Hero.AllAliveHeroes)
				{
					if (hero2 != Hero.MainHero && hero2.Occupation == occupation && hero2.IsFemale == flag5 && (hero2.PartyBelongedTo == null || hero2.PartyBelongedTo.MapEvent == null))
					{
						hero = hero2;
						break;
					}
				}
				if (hero == null)
				{
					hero = HeroCreator.CreateHeroAtOccupation(occupation, null);
				}
				if (flag6)
				{
					hero.SetBirthDay(HeroHelper.GetRandomBirthDayForAge((float)ConversationAnimationToolCampaignBehavior._characterAge));
				}
				if (flag4)
				{
					TakePrisonerAction.Apply(PartyBase.MainParty, hero);
				}
				if (flag7)
				{
					hero.MakeWounded(null, KillCharacterAction.KillCharacterActionDetail.None);
				}
				if (flag3)
				{
					hero.ChangeState(Hero.CharacterStates.Active);
				}
				hero.UpdatePlayerGender(flag5);
				characterObject = hero.CharacterObject;
			}
			else
			{
				foreach (CharacterObject characterObject2 in CharacterObject.All)
				{
					if (characterObject2.Occupation == occupation && characterObject2.IsFemale == flag5)
					{
						characterObject = characterObject2;
						break;
					}
				}
				if (characterObject == null)
				{
					characterObject = Campaign.Current.ObjectManager.GetObject<CultureObject>("empire").BasicTroop;
				}
			}
			if (characterObject == null)
			{
				return;
			}
			if (characterObject.IsHero && ConversationAnimationToolCampaignBehavior._relationType != -1)
			{
				Hero heroObject = characterObject.HeroObject;
				float relationWithPlayer = heroObject.GetRelationWithPlayer();
				float num = 0f;
				if (ConversationAnimationToolCampaignBehavior._relationType == 0 && !heroObject.IsEnemy(Hero.MainHero))
				{
					num = -relationWithPlayer - 15f;
				}
				else if (ConversationAnimationToolCampaignBehavior._relationType == 1 && !heroObject.IsNeutral(Hero.MainHero))
				{
					num = -relationWithPlayer;
				}
				else if (ConversationAnimationToolCampaignBehavior._relationType == 2 && !heroObject.IsFriend(Hero.MainHero))
				{
					num = -relationWithPlayer + 15f;
				}
				ChangeRelationAction.ApplyPlayerRelation(heroObject, (int)num, true, true);
			}
			CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, false, false, false, false, false, false), new ConversationCharacterData(characterObject, null, false, false, false, flag8, flag8, false));
			ConversationAnimationToolCampaignBehavior.CloseConversationAnimationTool();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0003A664 File Offset: 0x00038864
		private static void StartImGUIWindow(string str)
		{
			Imgui.BeginMainThreadScope();
			Imgui.Begin(str);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003A671 File Offset: 0x00038871
		private static void ImGUITextArea(string text, bool separatorNeeded, bool onSameLine)
		{
			Imgui.Text(text);
			ConversationAnimationToolCampaignBehavior.ImGUISeparatorSameLineHandler(separatorNeeded, onSameLine);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0003A680 File Offset: 0x00038880
		private static bool ImGUIButton(string buttonText, bool smallButton)
		{
			if (smallButton)
			{
				return Imgui.SmallButton(buttonText);
			}
			return Imgui.Button(buttonText);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003A692 File Offset: 0x00038892
		private static void ImGUIIntegerField(string fieldText, ref int value, bool separatorNeeded, bool onSameLine)
		{
			Imgui.InputInt(fieldText, ref value);
			ConversationAnimationToolCampaignBehavior.ImGUISeparatorSameLineHandler(separatorNeeded, onSameLine);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0003A6A3 File Offset: 0x000388A3
		private static void ImGUICheckBox(string text, ref bool is_checked, bool separatorNeeded, bool onSameLine)
		{
			Imgui.Checkbox(text, ref is_checked);
			ConversationAnimationToolCampaignBehavior.ImGUISeparatorSameLineHandler(separatorNeeded, onSameLine);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0003A6B4 File Offset: 0x000388B4
		private static void ImGUISeparatorSameLineHandler(bool separatorNeeded, bool onSameLine)
		{
			if (separatorNeeded)
			{
				ConversationAnimationToolCampaignBehavior.Separator();
			}
			if (onSameLine)
			{
				ConversationAnimationToolCampaignBehavior.OnSameLine();
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0003A6C6 File Offset: 0x000388C6
		private static void OnSameLine()
		{
			Imgui.SameLine(0f, 0f);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003A6D7 File Offset: 0x000388D7
		private static void Separator()
		{
			Imgui.Separator();
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0003A6DE File Offset: 0x000388DE
		private static void EndImGUIWindow()
		{
			Imgui.End();
			Imgui.EndMainThreadScope();
		}

		// Token: 0x04000300 RID: 768
		private static bool _isToolEnabled = false;

		// Token: 0x04000301 RID: 769
		private static int _characterType = -1;

		// Token: 0x04000302 RID: 770
		private static int _characterState = -1;

		// Token: 0x04000303 RID: 771
		private static int _characterGender = -1;

		// Token: 0x04000304 RID: 772
		private static int _characterAge = -1;

		// Token: 0x04000305 RID: 773
		private static int _characterWoundedState = -1;

		// Token: 0x04000306 RID: 774
		private static int _equipmentType = -1;

		// Token: 0x04000307 RID: 775
		private static int _relationType = -1;

		// Token: 0x04000308 RID: 776
		private static int _personaType = -1;
	}
}
