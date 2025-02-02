using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.General
{
	// Token: 0x0200004F RID: 79
	public class SPGeneralKillNotificationItemVM : ViewModel
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00019D9D File Offset: 0x00017F9D
		private Color friendlyColor
		{
			get
			{
				return new Color(0.54296875f, 0.77734375f, 0.421875f, 1f);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00019DB8 File Offset: 0x00017FB8
		private Color enemyColor
		{
			get
			{
				return new Color(0.953125f, 0.48828125f, 0.42578125f, 1f);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00019DD4 File Offset: 0x00017FD4
		public SPGeneralKillNotificationItemVM(Agent affectedAgent, Agent affectorAgent, Agent assistedAgent, bool isHeadshot, Action<SPGeneralKillNotificationItemVM> onRemove)
		{
			this._affectedAgent = affectedAgent;
			this._affectorAgent = affectorAgent;
			this._assistedAgent = assistedAgent;
			this._onRemove = onRemove;
			this._showNames = (BannerlordConfig.ReportCasualtiesType == 0);
			this.InitProperties(this._affectedAgent, this._affectorAgent, this._assistedAgent, isHeadshot);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00019E2C File Offset: 0x0001802C
		private void InitProperties(Agent affectedAgent, Agent affectorAgent, Agent assistedAgent, bool isHeadshot)
		{
			MBStringBuilder mbstringBuilder = default(MBStringBuilder);
			mbstringBuilder.Initialize(64, "InitProperties");
			if (!this._showNames)
			{
				if (affectorAgent == null)
				{
					goto IL_66;
				}
				BasicCharacterObject character = affectorAgent.Character;
				bool? flag = (character != null) ? new bool?(character.IsHero) : null;
				bool flag2 = true;
				if (!(flag.GetValueOrDefault() == flag2 & flag != null))
				{
					goto IL_66;
				}
			}
			mbstringBuilder.Append<string>(affectorAgent.Name);
			IL_66:
			mbstringBuilder.Append('\0');
			mbstringBuilder.Append<string>(SPGeneralKillNotificationItemVM.GetAgentType(affectorAgent));
			mbstringBuilder.Append('\0');
			if (!this._showNames)
			{
				BasicCharacterObject character2 = affectedAgent.Character;
				if (character2 == null || !character2.IsHero)
				{
					goto IL_B0;
				}
			}
			mbstringBuilder.Append<string>(affectedAgent.Name);
			IL_B0:
			mbstringBuilder.Append('\0');
			mbstringBuilder.Append<string>(SPGeneralKillNotificationItemVM.GetAgentType(affectedAgent));
			mbstringBuilder.Append('\0');
			mbstringBuilder.Append((affectedAgent.State == AgentState.Unconscious) ? 1 : 0);
			mbstringBuilder.Append('\0');
			mbstringBuilder.Append(isHeadshot ? 1 : 0);
			mbstringBuilder.Append('\0');
			Team team = affectedAgent.Team;
			Color color;
			if (team != null && team.IsValid)
			{
				if (affectedAgent.Team.IsPlayerAlly)
				{
					color = this.enemyColor;
				}
				else
				{
					color = this.friendlyColor;
				}
			}
			else
			{
				color = Color.FromUint(4284111450U);
			}
			mbstringBuilder.Append(color.ToUnsignedInteger());
			this.Message = mbstringBuilder.ToStringAndRelease();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00019F9C File Offset: 0x0001819C
		private static string GetAgentType(Agent agent)
		{
			if (((agent != null) ? agent.Character : null) == null)
			{
				return "None";
			}
			switch (agent.Character.DefaultFormationGroup)
			{
			case 0:
				return "Infantry_Light";
			case 1:
				return "Archer_Light";
			case 2:
				return "Cavalry_Light";
			case 3:
				return "HorseArcher_Light";
			case 4:
			case 5:
				return "Infantry_Heavy";
			case 6:
				return "Cavalry_Light";
			case 7:
				return "Cavalry_Heavy";
			case 8:
			case 9:
			case 10:
				return "Infantry_Heavy";
			default:
				return "None";
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001A032 File Offset: 0x00018232
		public void ExecuteRemove()
		{
			this._onRemove(this);
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001A040 File Offset: 0x00018240
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0001A048 File Offset: 0x00018248
		[DataSourceProperty]
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				if (value != this._message)
				{
					this._message = value;
					base.OnPropertyChangedWithValue<string>(value, "Message");
				}
			}
		}

		// Token: 0x04000305 RID: 773
		private const char _seperator = '\0';

		// Token: 0x04000306 RID: 774
		private readonly Agent _affectedAgent;

		// Token: 0x04000307 RID: 775
		private readonly Agent _affectorAgent;

		// Token: 0x04000308 RID: 776
		private readonly Agent _assistedAgent;

		// Token: 0x04000309 RID: 777
		private readonly Action<SPGeneralKillNotificationItemVM> _onRemove;

		// Token: 0x0400030A RID: 778
		private bool _showNames;

		// Token: 0x0400030B RID: 779
		private string _message;
	}
}
