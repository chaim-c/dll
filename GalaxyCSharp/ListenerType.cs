﻿using System;

namespace Galaxy.Api
{
	// Token: 0x02000171 RID: 369
	public enum ListenerType
	{
		// Token: 0x040002B4 RID: 692
		LISTENER_TYPE_BEGIN,
		// Token: 0x040002B5 RID: 693
		LOBBY_LIST = 0,
		// Token: 0x040002B6 RID: 694
		LOBBY_CREATED,
		// Token: 0x040002B7 RID: 695
		LOBBY_ENTERED,
		// Token: 0x040002B8 RID: 696
		LOBBY_LEFT,
		// Token: 0x040002B9 RID: 697
		LOBBY_DATA,
		// Token: 0x040002BA RID: 698
		LOBBY_MEMBER_STATE,
		// Token: 0x040002BB RID: 699
		LOBBY_OWNER_CHANGE,
		// Token: 0x040002BC RID: 700
		AUTH,
		// Token: 0x040002BD RID: 701
		LOBBY_MESSAGE,
		// Token: 0x040002BE RID: 702
		NETWORKING,
		// Token: 0x040002BF RID: 703
		_SERVER_NETWORKING,
		// Token: 0x040002C0 RID: 704
		USER_DATA,
		// Token: 0x040002C1 RID: 705
		USER_STATS_AND_ACHIEVEMENTS_RETRIEVE,
		// Token: 0x040002C2 RID: 706
		STATS_AND_ACHIEVEMENTS_STORE,
		// Token: 0x040002C3 RID: 707
		ACHIEVEMENT_CHANGE,
		// Token: 0x040002C4 RID: 708
		LEADERBOARDS_RETRIEVE,
		// Token: 0x040002C5 RID: 709
		LEADERBOARD_ENTRIES_RETRIEVE,
		// Token: 0x040002C6 RID: 710
		LEADERBOARD_SCORE_UPDATE_LISTENER,
		// Token: 0x040002C7 RID: 711
		PERSONA_DATA_CHANGED,
		// Token: 0x040002C8 RID: 712
		RICH_PRESENCE_CHANGE_LISTENER,
		// Token: 0x040002C9 RID: 713
		GAME_JOIN_REQUESTED_LISTENER,
		// Token: 0x040002CA RID: 714
		OPERATIONAL_STATE_CHANGE,
		// Token: 0x040002CB RID: 715
		_OVERLAY_STATE_CHANGE,
		// Token: 0x040002CC RID: 716
		FRIEND_LIST_RETRIEVE,
		// Token: 0x040002CD RID: 717
		ENCRYPTED_APP_TICKET_RETRIEVE,
		// Token: 0x040002CE RID: 718
		ACCESS_TOKEN_CHANGE,
		// Token: 0x040002CF RID: 719
		LEADERBOARD_RETRIEVE,
		// Token: 0x040002D0 RID: 720
		SPECIFIC_USER_DATA,
		// Token: 0x040002D1 RID: 721
		INVITATION_SEND,
		// Token: 0x040002D2 RID: 722
		RICH_PRESENCE_LISTENER,
		// Token: 0x040002D3 RID: 723
		GAME_INVITATION_RECEIVED_LISTENER,
		// Token: 0x040002D4 RID: 724
		NOTIFICATION_LISTENER,
		// Token: 0x040002D5 RID: 725
		LOBBY_DATA_RETRIEVE,
		// Token: 0x040002D6 RID: 726
		USER_TIME_PLAYED_RETRIEVE,
		// Token: 0x040002D7 RID: 727
		OTHER_SESSION_START,
		// Token: 0x040002D8 RID: 728
		_STORAGE_SYNCHRONIZATION,
		// Token: 0x040002D9 RID: 729
		FILE_SHARE,
		// Token: 0x040002DA RID: 730
		SHARED_FILE_DOWNLOAD,
		// Token: 0x040002DB RID: 731
		CUSTOM_NETWORKING_CONNECTION_OPEN,
		// Token: 0x040002DC RID: 732
		CUSTOM_NETWORKING_CONNECTION_CLOSE,
		// Token: 0x040002DD RID: 733
		CUSTOM_NETWORKING_CONNECTION_DATA,
		// Token: 0x040002DE RID: 734
		OVERLAY_INITIALIZATION_STATE_CHANGE,
		// Token: 0x040002DF RID: 735
		OVERLAY_VISIBILITY_CHANGE,
		// Token: 0x040002E0 RID: 736
		CHAT_ROOM_WITH_USER_RETRIEVE_LISTENER,
		// Token: 0x040002E1 RID: 737
		CHAT_ROOM_MESSAGE_SEND_LISTENER,
		// Token: 0x040002E2 RID: 738
		CHAT_ROOM_MESSAGES_LISTENER,
		// Token: 0x040002E3 RID: 739
		FRIEND_INVITATION_SEND_LISTENER,
		// Token: 0x040002E4 RID: 740
		FRIEND_INVITATION_LIST_RETRIEVE_LISTENER,
		// Token: 0x040002E5 RID: 741
		FRIEND_INVITATION_LISTENER,
		// Token: 0x040002E6 RID: 742
		FRIEND_INVITATION_RESPOND_TO_LISTENER,
		// Token: 0x040002E7 RID: 743
		FRIEND_ADD_LISTENER,
		// Token: 0x040002E8 RID: 744
		FRIEND_DELETE_LISTENER,
		// Token: 0x040002E9 RID: 745
		CHAT_ROOM_MESSAGES_RETRIEVE_LISTENER,
		// Token: 0x040002EA RID: 746
		USER_FIND_LISTENER,
		// Token: 0x040002EB RID: 747
		NAT_TYPE_DETECTION,
		// Token: 0x040002EC RID: 748
		SENT_FRIEND_INVITATION_LIST_RETRIEVE_LISTENER,
		// Token: 0x040002ED RID: 749
		LOBBY_DATA_UPDATE_LISTENER,
		// Token: 0x040002EE RID: 750
		LOBBY_MEMBER_DATA_UPDATE_LISTENER,
		// Token: 0x040002EF RID: 751
		USER_INFORMATION_RETRIEVE_LISTENER,
		// Token: 0x040002F0 RID: 752
		RICH_PRESENCE_RETRIEVE_LISTENER,
		// Token: 0x040002F1 RID: 753
		GOG_SERVICES_CONNECTION_STATE_LISTENER,
		// Token: 0x040002F2 RID: 754
		TELEMETRY_EVENT_SEND_LISTENER,
		// Token: 0x040002F3 RID: 755
		LISTENER_TYPE_END
	}
}
