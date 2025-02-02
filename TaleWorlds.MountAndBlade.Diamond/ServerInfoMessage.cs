using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200015E RID: 350
	public enum ServerInfoMessage
	{
		// Token: 0x0400040A RID: 1034
		Success,
		// Token: 0x0400040B RID: 1035
		LoginMuted,
		// Token: 0x0400040C RID: 1036
		DestroySessionPremadeGameCancellation,
		// Token: 0x0400040D RID: 1037
		DestroySessionPartyInvitationCancellation,
		// Token: 0x0400040E RID: 1038
		DestroySessionPartyAutoDisband,
		// Token: 0x0400040F RID: 1039
		PlayerNotFound,
		// Token: 0x04000410 RID: 1040
		PlayerNotInLobby,
		// Token: 0x04000411 RID: 1041
		MustBeInLobby,
		// Token: 0x04000412 RID: 1042
		NoTextGiven,
		// Token: 0x04000413 RID: 1043
		TextTooLong,
		// Token: 0x04000414 RID: 1044
		FindGameBlockedFromMatchmaking,
		// Token: 0x04000415 RID: 1045
		FindGamePartyMemberBlockedFromMatchmaking,
		// Token: 0x04000416 RID: 1046
		FindGameNoGameTypeSelected,
		// Token: 0x04000417 RID: 1047
		FindGameDisabledGameTypesSelected,
		// Token: 0x04000418 RID: 1048
		FindGamePlayerCountNotAllowed,
		// Token: 0x04000419 RID: 1049
		FindGameNotPartyLeader,
		// Token: 0x0400041A RID: 1050
		FindGameNotAllPlayersReady,
		// Token: 0x0400041B RID: 1051
		FindGameRegionNotAvailable,
		// Token: 0x0400041C RID: 1052
		FindGamePunished,
		// Token: 0x0400041D RID: 1053
		RejoinGame,
		// Token: 0x0400041E RID: 1054
		RejoinGameNotFound,
		// Token: 0x0400041F RID: 1055
		RejoinGameNotAllowed,
		// Token: 0x04000420 RID: 1056
		AddFriendCantAddSelf,
		// Token: 0x04000421 RID: 1057
		AddFriendRequestSent,
		// Token: 0x04000422 RID: 1058
		AddFriendRequestReceived,
		// Token: 0x04000423 RID: 1059
		AddFriendAlreadyFriends,
		// Token: 0x04000424 RID: 1060
		AddFriendRequestPending,
		// Token: 0x04000425 RID: 1061
		AddFriendRequestAccepted,
		// Token: 0x04000426 RID: 1062
		AddFriendRequestDeclined,
		// Token: 0x04000427 RID: 1063
		AddFriendRequestBlocked,
		// Token: 0x04000428 RID: 1064
		RemoveFriendSuccess,
		// Token: 0x04000429 RID: 1065
		FriendRequestAccepted,
		// Token: 0x0400042A RID: 1066
		FriendRequestDeclined,
		// Token: 0x0400042B RID: 1067
		FriendRequestNotFound,
		// Token: 0x0400042C RID: 1068
		MustBeInParty,
		// Token: 0x0400042D RID: 1069
		MustBePartyLeader,
		// Token: 0x0400042E RID: 1070
		InvitePartyHasModules,
		// Token: 0x0400042F RID: 1071
		InvitePartyOtherPlayerHasModules,
		// Token: 0x04000430 RID: 1072
		InvitePartyCantInviteSelf,
		// Token: 0x04000431 RID: 1073
		InvitePartyOtherPlayerAlreadyInParty,
		// Token: 0x04000432 RID: 1074
		InvitePartyPartyIsFull,
		// Token: 0x04000433 RID: 1075
		InvitePartyOnlyLeaderCanInvite,
		// Token: 0x04000434 RID: 1076
		InvitePartySuccess,
		// Token: 0x04000435 RID: 1077
		SuggestPartyMustBeInParty,
		// Token: 0x04000436 RID: 1078
		SuggestPartyMustBeMember,
		// Token: 0x04000437 RID: 1079
		SuggestPartyCantSuggestSelf,
		// Token: 0x04000438 RID: 1080
		SuggestPartyOtherPlayerAlreadyInParty,
		// Token: 0x04000439 RID: 1081
		SuggestPartySuccess,
		// Token: 0x0400043A RID: 1082
		DisbandPartySuccess,
		// Token: 0x0400043B RID: 1083
		KickPlayerOtherPlayerMustBeInParty,
		// Token: 0x0400043C RID: 1084
		KickPartyPlayerMustBeLeader,
		// Token: 0x0400043D RID: 1085
		PromotePartyLeaderOngoingClanCreation,
		// Token: 0x0400043E RID: 1086
		PromotePartyLeaderCantPromoteSelf,
		// Token: 0x0400043F RID: 1087
		PromotePartyLeaderCantPromoteNonMember,
		// Token: 0x04000440 RID: 1088
		PromotePartyLeaderMustBeLeader,
		// Token: 0x04000441 RID: 1089
		PromotePartyLeaderSuccess,
		// Token: 0x04000442 RID: 1090
		PromotePartyLeaderAuto,
		// Token: 0x04000443 RID: 1091
		MustBeInClan,
		// Token: 0x04000444 RID: 1092
		MustBeClanLeader,
		// Token: 0x04000445 RID: 1093
		MustBePrivilegedClanMember,
		// Token: 0x04000446 RID: 1094
		ClanCreationNameIsInvalid,
		// Token: 0x04000447 RID: 1095
		ClanCreationTagIsInvalid,
		// Token: 0x04000448 RID: 1096
		ClanCreationSigilIsInvalid,
		// Token: 0x04000449 RID: 1097
		ClanCreationCultureIsInvalid,
		// Token: 0x0400044A RID: 1098
		ClanCreationNotAllPlayersReady,
		// Token: 0x0400044B RID: 1099
		ClanCreationNotEnoughPlayers,
		// Token: 0x0400044C RID: 1100
		ClanCreationAlreadyInAClan,
		// Token: 0x0400044D RID: 1101
		ClanCreationHaveToBeInAParty,
		// Token: 0x0400044E RID: 1102
		SetClanInformationSuccess,
		// Token: 0x0400044F RID: 1103
		AddClanAnnouncementSuccess,
		// Token: 0x04000450 RID: 1104
		EditClanAnnouncementNotFound,
		// Token: 0x04000451 RID: 1105
		EditClanAnnouncementSuccess,
		// Token: 0x04000452 RID: 1106
		DeleteClanAnnouncementNotFound,
		// Token: 0x04000453 RID: 1107
		DeleteClanAnnouncementSuccess,
		// Token: 0x04000454 RID: 1108
		ChangeClanSigilInvalid,
		// Token: 0x04000455 RID: 1109
		ChangeClanSigilSuccess,
		// Token: 0x04000456 RID: 1110
		ChangeClanCultureSuccess,
		// Token: 0x04000457 RID: 1111
		InviteClanPlayerAlreadyInvited,
		// Token: 0x04000458 RID: 1112
		InviteClanPlayerAlreadyInClan,
		// Token: 0x04000459 RID: 1113
		InviteClanPlayerIsNotOnline,
		// Token: 0x0400045A RID: 1114
		InviteClanPlayerFeatureNotSupported,
		// Token: 0x0400045B RID: 1115
		InviteClanCantInviteSelf,
		// Token: 0x0400045C RID: 1116
		InviteClanSuccess,
		// Token: 0x0400045D RID: 1117
		AcceptClanInvitationSuccess,
		// Token: 0x0400045E RID: 1118
		DeclineClanInvitationSuccess,
		// Token: 0x0400045F RID: 1119
		PromoteClanRolePlayerNotInClan,
		// Token: 0x04000460 RID: 1120
		PromoteClanLeaderCantPromoteSelf,
		// Token: 0x04000461 RID: 1121
		PromoteClanLeaderSuccess,
		// Token: 0x04000462 RID: 1122
		PromoteClanOfficerRoleLimitReached,
		// Token: 0x04000463 RID: 1123
		PromoteClanOfficerCantPromoteSelf,
		// Token: 0x04000464 RID: 1124
		PromoteClanOfficerSuccess,
		// Token: 0x04000465 RID: 1125
		RemoveClanOfficerMustBeOfficerToMember,
		// Token: 0x04000466 RID: 1126
		RemoveClanOfficerMustBeOfficerToLeader,
		// Token: 0x04000467 RID: 1127
		RemoveClanOfficerSuccessFromLeader,
		// Token: 0x04000468 RID: 1128
		RemoveClanOfficerSuccessFromMember,
		// Token: 0x04000469 RID: 1129
		RemoveClanMemberToMember,
		// Token: 0x0400046A RID: 1130
		RemoveClanMemberToLeader,
		// Token: 0x0400046B RID: 1131
		RemoveClanMemberLeaderCantLeave,
		// Token: 0x0400046C RID: 1132
		PremadeGameCreationCanceled,
		// Token: 0x0400046D RID: 1133
		PremadeGameCreationMustBeCreating,
		// Token: 0x0400046E RID: 1134
		PremadeGameCreationMapNotAvailable,
		// Token: 0x0400046F RID: 1135
		PremadeGameCreationPartyNotEligible,
		// Token: 0x04000470 RID: 1136
		PremadeGameCreationInvalidGameType,
		// Token: 0x04000471 RID: 1137
		PremadeGameJoinIncorrectPassword,
		// Token: 0x04000472 RID: 1138
		PremadeGameJoinGameNotFound,
		// Token: 0x04000473 RID: 1139
		PremadeGameJoinPartyNotEligible,
		// Token: 0x04000474 RID: 1140
		GetPremadeGameListNotEligible,
		// Token: 0x04000475 RID: 1141
		ReportPlayerGameNotFound,
		// Token: 0x04000476 RID: 1142
		ReportPlayerPlayerNotFound,
		// Token: 0x04000477 RID: 1143
		ReportPlayerServerIsUnofficial,
		// Token: 0x04000478 RID: 1144
		ReportPlayerSuccess,
		// Token: 0x04000479 RID: 1145
		ChangeBannerlordIDFailure,
		// Token: 0x0400047A RID: 1146
		ChangeBannerlordIDSuccess,
		// Token: 0x0400047B RID: 1147
		ChangeBannerlordIDEmpty,
		// Token: 0x0400047C RID: 1148
		ChangeBannerlordIDTooShort,
		// Token: 0x0400047D RID: 1149
		ChangeBannerlordIDTooLong,
		// Token: 0x0400047E RID: 1150
		ChangeBannerlordIDInvalidCharacters,
		// Token: 0x0400047F RID: 1151
		ChangeBannerlordIDProfanity,
		// Token: 0x04000480 RID: 1152
		GameInvitationCantInviteSelf,
		// Token: 0x04000481 RID: 1153
		GameInvitationPlayerAlreadyInGame,
		// Token: 0x04000482 RID: 1154
		GameInvitationSuccess,
		// Token: 0x04000483 RID: 1155
		ChangeRegionFailed,
		// Token: 0x04000484 RID: 1156
		ChangeGameModeFailed,
		// Token: 0x04000485 RID: 1157
		BattleServerKickFriendlyFire,
		// Token: 0x04000486 RID: 1158
		ChatServerDisconnectedFromRoom,
		// Token: 0x04000487 RID: 1159
		CustomizationServiceIsUnavailable,
		// Token: 0x04000488 RID: 1160
		CustomizationNotEnoughLoot,
		// Token: 0x04000489 RID: 1161
		CustomizationItemIsUnavailable,
		// Token: 0x0400048A RID: 1162
		CustomizationItemIsFree,
		// Token: 0x0400048B RID: 1163
		CustomizationItemAlreadyOwned,
		// Token: 0x0400048C RID: 1164
		CustomizationItemIsNotOwned,
		// Token: 0x0400048D RID: 1165
		CustomizationChangeSigilSuccess,
		// Token: 0x0400048E RID: 1166
		CustomizationTroopIsNotValid,
		// Token: 0x0400048F RID: 1167
		CustomizationCantUseMoreThanOneForSingleSlot,
		// Token: 0x04000490 RID: 1168
		CustomizationCantUpdateBadge,
		// Token: 0x04000491 RID: 1169
		CustomizationInvalidBadge,
		// Token: 0x04000492 RID: 1170
		CustomizationCantDowngradeBadge,
		// Token: 0x04000493 RID: 1171
		CustomizationBadgeNotAvailable
	}
}
