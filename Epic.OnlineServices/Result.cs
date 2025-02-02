using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000021 RID: 33
	public enum Result
	{
		// Token: 0x04000059 RID: 89
		Success,
		// Token: 0x0400005A RID: 90
		NoConnection,
		// Token: 0x0400005B RID: 91
		InvalidCredentials,
		// Token: 0x0400005C RID: 92
		InvalidUser,
		// Token: 0x0400005D RID: 93
		InvalidAuth,
		// Token: 0x0400005E RID: 94
		AccessDenied,
		// Token: 0x0400005F RID: 95
		MissingPermissions,
		// Token: 0x04000060 RID: 96
		TokenNotAccount,
		// Token: 0x04000061 RID: 97
		TooManyRequests,
		// Token: 0x04000062 RID: 98
		AlreadyPending,
		// Token: 0x04000063 RID: 99
		InvalidParameters,
		// Token: 0x04000064 RID: 100
		InvalidRequest,
		// Token: 0x04000065 RID: 101
		UnrecognizedResponse,
		// Token: 0x04000066 RID: 102
		IncompatibleVersion,
		// Token: 0x04000067 RID: 103
		NotConfigured,
		// Token: 0x04000068 RID: 104
		AlreadyConfigured,
		// Token: 0x04000069 RID: 105
		NotImplemented,
		// Token: 0x0400006A RID: 106
		Canceled,
		// Token: 0x0400006B RID: 107
		NotFound,
		// Token: 0x0400006C RID: 108
		OperationWillRetry,
		// Token: 0x0400006D RID: 109
		NoChange,
		// Token: 0x0400006E RID: 110
		VersionMismatch,
		// Token: 0x0400006F RID: 111
		LimitExceeded,
		// Token: 0x04000070 RID: 112
		Disabled,
		// Token: 0x04000071 RID: 113
		DuplicateNotAllowed,
		// Token: 0x04000072 RID: 114
		MissingParametersDEPRECATED,
		// Token: 0x04000073 RID: 115
		InvalidSandboxId,
		// Token: 0x04000074 RID: 116
		TimedOut,
		// Token: 0x04000075 RID: 117
		PartialResult,
		// Token: 0x04000076 RID: 118
		MissingRole,
		// Token: 0x04000077 RID: 119
		MissingFeature,
		// Token: 0x04000078 RID: 120
		InvalidSandbox,
		// Token: 0x04000079 RID: 121
		InvalidDeployment,
		// Token: 0x0400007A RID: 122
		InvalidProduct,
		// Token: 0x0400007B RID: 123
		InvalidProductUserID,
		// Token: 0x0400007C RID: 124
		ServiceFailure,
		// Token: 0x0400007D RID: 125
		CacheDirectoryMissing,
		// Token: 0x0400007E RID: 126
		CacheDirectoryInvalid,
		// Token: 0x0400007F RID: 127
		InvalidState,
		// Token: 0x04000080 RID: 128
		RequestInProgress,
		// Token: 0x04000081 RID: 129
		ApplicationSuspended,
		// Token: 0x04000082 RID: 130
		NetworkDisconnected,
		// Token: 0x04000083 RID: 131
		AuthAccountLocked = 1001,
		// Token: 0x04000084 RID: 132
		AuthAccountLockedForUpdate,
		// Token: 0x04000085 RID: 133
		AuthInvalidRefreshToken,
		// Token: 0x04000086 RID: 134
		AuthInvalidToken,
		// Token: 0x04000087 RID: 135
		AuthAuthenticationFailure,
		// Token: 0x04000088 RID: 136
		AuthInvalidPlatformToken,
		// Token: 0x04000089 RID: 137
		AuthWrongAccount,
		// Token: 0x0400008A RID: 138
		AuthWrongClient,
		// Token: 0x0400008B RID: 139
		AuthFullAccountRequired,
		// Token: 0x0400008C RID: 140
		AuthHeadlessAccountRequired,
		// Token: 0x0400008D RID: 141
		AuthPasswordResetRequired,
		// Token: 0x0400008E RID: 142
		AuthPasswordCannotBeReused,
		// Token: 0x0400008F RID: 143
		AuthExpired,
		// Token: 0x04000090 RID: 144
		AuthScopeConsentRequired,
		// Token: 0x04000091 RID: 145
		AuthApplicationNotFound,
		// Token: 0x04000092 RID: 146
		AuthScopeNotFound,
		// Token: 0x04000093 RID: 147
		AuthAccountFeatureRestricted,
		// Token: 0x04000094 RID: 148
		AuthAccountPortalLoadError,
		// Token: 0x04000095 RID: 149
		AuthCorrectiveActionRequired,
		// Token: 0x04000096 RID: 150
		AuthPinGrantCode,
		// Token: 0x04000097 RID: 151
		AuthPinGrantExpired,
		// Token: 0x04000098 RID: 152
		AuthPinGrantPending,
		// Token: 0x04000099 RID: 153
		AuthExternalAuthNotLinked = 1030,
		// Token: 0x0400009A RID: 154
		AuthExternalAuthRevoked = 1032,
		// Token: 0x0400009B RID: 155
		AuthExternalAuthInvalid,
		// Token: 0x0400009C RID: 156
		AuthExternalAuthRestricted,
		// Token: 0x0400009D RID: 157
		AuthExternalAuthCannotLogin,
		// Token: 0x0400009E RID: 158
		AuthExternalAuthExpired,
		// Token: 0x0400009F RID: 159
		AuthExternalAuthIsLastLoginType,
		// Token: 0x040000A0 RID: 160
		AuthExchangeCodeNotFound = 1040,
		// Token: 0x040000A1 RID: 161
		AuthOriginatingExchangeCodeSessionExpired,
		// Token: 0x040000A2 RID: 162
		AuthAccountNotActive = 1050,
		// Token: 0x040000A3 RID: 163
		AuthMFARequired = 1060,
		// Token: 0x040000A4 RID: 164
		AuthParentalControls = 1070,
		// Token: 0x040000A5 RID: 165
		AuthNoRealId = 1080,
		// Token: 0x040000A6 RID: 166
		FriendsInviteAwaitingAcceptance = 2000,
		// Token: 0x040000A7 RID: 167
		FriendsNoInvitation,
		// Token: 0x040000A8 RID: 168
		FriendsAlreadyFriends = 2003,
		// Token: 0x040000A9 RID: 169
		FriendsNotFriends,
		// Token: 0x040000AA RID: 170
		FriendsTargetUserTooManyInvites,
		// Token: 0x040000AB RID: 171
		FriendsLocalUserTooManyInvites,
		// Token: 0x040000AC RID: 172
		FriendsTargetUserFriendLimitExceeded,
		// Token: 0x040000AD RID: 173
		FriendsLocalUserFriendLimitExceeded,
		// Token: 0x040000AE RID: 174
		PresenceDataInvalid = 3000,
		// Token: 0x040000AF RID: 175
		PresenceDataLengthInvalid,
		// Token: 0x040000B0 RID: 176
		PresenceDataKeyInvalid,
		// Token: 0x040000B1 RID: 177
		PresenceDataKeyLengthInvalid,
		// Token: 0x040000B2 RID: 178
		PresenceDataValueInvalid,
		// Token: 0x040000B3 RID: 179
		PresenceDataValueLengthInvalid,
		// Token: 0x040000B4 RID: 180
		PresenceRichTextInvalid,
		// Token: 0x040000B5 RID: 181
		PresenceRichTextLengthInvalid,
		// Token: 0x040000B6 RID: 182
		PresenceStatusInvalid,
		// Token: 0x040000B7 RID: 183
		EcomEntitlementStale = 4000,
		// Token: 0x040000B8 RID: 184
		EcomCatalogOfferStale,
		// Token: 0x040000B9 RID: 185
		EcomCatalogItemStale,
		// Token: 0x040000BA RID: 186
		EcomCatalogOfferPriceInvalid,
		// Token: 0x040000BB RID: 187
		EcomCheckoutLoadError,
		// Token: 0x040000BC RID: 188
		SessionsSessionInProgress = 5000,
		// Token: 0x040000BD RID: 189
		SessionsTooManyPlayers,
		// Token: 0x040000BE RID: 190
		SessionsNoPermission,
		// Token: 0x040000BF RID: 191
		SessionsSessionAlreadyExists,
		// Token: 0x040000C0 RID: 192
		SessionsInvalidLock,
		// Token: 0x040000C1 RID: 193
		SessionsInvalidSession,
		// Token: 0x040000C2 RID: 194
		SessionsSandboxNotAllowed,
		// Token: 0x040000C3 RID: 195
		SessionsInviteFailed,
		// Token: 0x040000C4 RID: 196
		SessionsInviteNotFound,
		// Token: 0x040000C5 RID: 197
		SessionsUpsertNotAllowed,
		// Token: 0x040000C6 RID: 198
		SessionsAggregationFailed,
		// Token: 0x040000C7 RID: 199
		SessionsHostAtCapacity,
		// Token: 0x040000C8 RID: 200
		SessionsSandboxAtCapacity,
		// Token: 0x040000C9 RID: 201
		SessionsSessionNotAnonymous,
		// Token: 0x040000CA RID: 202
		SessionsOutOfSync,
		// Token: 0x040000CB RID: 203
		SessionsTooManyInvites,
		// Token: 0x040000CC RID: 204
		SessionsPresenceSessionExists,
		// Token: 0x040000CD RID: 205
		SessionsDeploymentAtCapacity,
		// Token: 0x040000CE RID: 206
		SessionsNotAllowed,
		// Token: 0x040000CF RID: 207
		SessionsPlayerSanctioned,
		// Token: 0x040000D0 RID: 208
		PlayerDataStorageFilenameInvalid = 6000,
		// Token: 0x040000D1 RID: 209
		PlayerDataStorageFilenameLengthInvalid,
		// Token: 0x040000D2 RID: 210
		PlayerDataStorageFilenameInvalidChars,
		// Token: 0x040000D3 RID: 211
		PlayerDataStorageFileSizeTooLarge,
		// Token: 0x040000D4 RID: 212
		PlayerDataStorageFileSizeInvalid,
		// Token: 0x040000D5 RID: 213
		PlayerDataStorageFileHandleInvalid,
		// Token: 0x040000D6 RID: 214
		PlayerDataStorageDataInvalid,
		// Token: 0x040000D7 RID: 215
		PlayerDataStorageDataLengthInvalid,
		// Token: 0x040000D8 RID: 216
		PlayerDataStorageStartIndexInvalid,
		// Token: 0x040000D9 RID: 217
		PlayerDataStorageRequestInProgress,
		// Token: 0x040000DA RID: 218
		PlayerDataStorageUserThrottled,
		// Token: 0x040000DB RID: 219
		PlayerDataStorageEncryptionKeyNotSet,
		// Token: 0x040000DC RID: 220
		PlayerDataStorageUserErrorFromDataCallback,
		// Token: 0x040000DD RID: 221
		PlayerDataStorageFileHeaderHasNewerVersion,
		// Token: 0x040000DE RID: 222
		PlayerDataStorageFileCorrupted,
		// Token: 0x040000DF RID: 223
		ConnectExternalTokenValidationFailed = 7000,
		// Token: 0x040000E0 RID: 224
		ConnectUserAlreadyExists,
		// Token: 0x040000E1 RID: 225
		ConnectAuthExpired,
		// Token: 0x040000E2 RID: 226
		ConnectInvalidToken,
		// Token: 0x040000E3 RID: 227
		ConnectUnsupportedTokenType,
		// Token: 0x040000E4 RID: 228
		ConnectLinkAccountFailed,
		// Token: 0x040000E5 RID: 229
		ConnectExternalServiceUnavailable,
		// Token: 0x040000E6 RID: 230
		ConnectExternalServiceConfigurationFailure,
		// Token: 0x040000E7 RID: 231
		ConnectLinkAccountFailedMissingNintendoIdAccountDEPRECATED,
		// Token: 0x040000E8 RID: 232
		SocialOverlayLoadError = 8000,
		// Token: 0x040000E9 RID: 233
		LobbyNotOwner = 9000,
		// Token: 0x040000EA RID: 234
		LobbyInvalidLock,
		// Token: 0x040000EB RID: 235
		LobbyLobbyAlreadyExists,
		// Token: 0x040000EC RID: 236
		LobbySessionInProgress,
		// Token: 0x040000ED RID: 237
		LobbyTooManyPlayers,
		// Token: 0x040000EE RID: 238
		LobbyNoPermission,
		// Token: 0x040000EF RID: 239
		LobbyInvalidSession,
		// Token: 0x040000F0 RID: 240
		LobbySandboxNotAllowed,
		// Token: 0x040000F1 RID: 241
		LobbyInviteFailed,
		// Token: 0x040000F2 RID: 242
		LobbyInviteNotFound,
		// Token: 0x040000F3 RID: 243
		LobbyUpsertNotAllowed,
		// Token: 0x040000F4 RID: 244
		LobbyAggregationFailed,
		// Token: 0x040000F5 RID: 245
		LobbyHostAtCapacity,
		// Token: 0x040000F6 RID: 246
		LobbySandboxAtCapacity,
		// Token: 0x040000F7 RID: 247
		LobbyTooManyInvites,
		// Token: 0x040000F8 RID: 248
		LobbyDeploymentAtCapacity,
		// Token: 0x040000F9 RID: 249
		LobbyNotAllowed,
		// Token: 0x040000FA RID: 250
		LobbyMemberUpdateOnly,
		// Token: 0x040000FB RID: 251
		LobbyPresenceLobbyExists,
		// Token: 0x040000FC RID: 252
		LobbyVoiceNotEnabled,
		// Token: 0x040000FD RID: 253
		TitleStorageUserErrorFromDataCallback = 10000,
		// Token: 0x040000FE RID: 254
		TitleStorageEncryptionKeyNotSet,
		// Token: 0x040000FF RID: 255
		TitleStorageFileCorrupted,
		// Token: 0x04000100 RID: 256
		TitleStorageFileHeaderHasNewerVersion,
		// Token: 0x04000101 RID: 257
		ModsModSdkProcessIsAlreadyRunning = 11000,
		// Token: 0x04000102 RID: 258
		ModsModSdkCommandIsEmpty,
		// Token: 0x04000103 RID: 259
		ModsModSdkProcessCreationFailed,
		// Token: 0x04000104 RID: 260
		ModsCriticalError,
		// Token: 0x04000105 RID: 261
		ModsToolInternalError,
		// Token: 0x04000106 RID: 262
		ModsIPCFailure,
		// Token: 0x04000107 RID: 263
		ModsInvalidIPCResponse,
		// Token: 0x04000108 RID: 264
		ModsURILaunchFailure,
		// Token: 0x04000109 RID: 265
		ModsModIsNotInstalled,
		// Token: 0x0400010A RID: 266
		ModsUserDoesNotOwnTheGame,
		// Token: 0x0400010B RID: 267
		ModsOfferRequestByIdInvalidResult,
		// Token: 0x0400010C RID: 268
		ModsCouldNotFindOffer,
		// Token: 0x0400010D RID: 269
		ModsOfferRequestByIdFailure,
		// Token: 0x0400010E RID: 270
		ModsPurchaseFailure,
		// Token: 0x0400010F RID: 271
		ModsInvalidGameInstallInfo,
		// Token: 0x04000110 RID: 272
		ModsCannotGetManifestLocation,
		// Token: 0x04000111 RID: 273
		ModsUnsupportedOS,
		// Token: 0x04000112 RID: 274
		AntiCheatClientProtectionNotAvailable = 12000,
		// Token: 0x04000113 RID: 275
		AntiCheatInvalidMode,
		// Token: 0x04000114 RID: 276
		AntiCheatClientProductIdMismatch,
		// Token: 0x04000115 RID: 277
		AntiCheatClientSandboxIdMismatch,
		// Token: 0x04000116 RID: 278
		AntiCheatProtectMessageSessionKeyRequired,
		// Token: 0x04000117 RID: 279
		AntiCheatProtectMessageValidationFailed,
		// Token: 0x04000118 RID: 280
		AntiCheatProtectMessageInitializationFailed,
		// Token: 0x04000119 RID: 281
		AntiCheatPeerAlreadyRegistered,
		// Token: 0x0400011A RID: 282
		AntiCheatPeerNotFound,
		// Token: 0x0400011B RID: 283
		AntiCheatPeerNotProtected,
		// Token: 0x0400011C RID: 284
		AntiCheatClientDeploymentIdMismatch,
		// Token: 0x0400011D RID: 285
		AntiCheatDeviceIdAuthIsNotSupported,
		// Token: 0x0400011E RID: 286
		TooManyParticipants = 13000,
		// Token: 0x0400011F RID: 287
		RoomAlreadyExists,
		// Token: 0x04000120 RID: 288
		UserKicked,
		// Token: 0x04000121 RID: 289
		UserBanned,
		// Token: 0x04000122 RID: 290
		RoomWasLeft,
		// Token: 0x04000123 RID: 291
		ReconnectionTimegateExpired,
		// Token: 0x04000124 RID: 292
		ShutdownInvoked,
		// Token: 0x04000125 RID: 293
		UserIsInBlocklist,
		// Token: 0x04000126 RID: 294
		ProgressionSnapshotSnapshotIdUnavailable = 14000,
		// Token: 0x04000127 RID: 295
		ParentEmailMissing = 15000,
		// Token: 0x04000128 RID: 296
		UserGraduated,
		// Token: 0x04000129 RID: 297
		AndroidJavaVMNotStored = 17000,
		// Token: 0x0400012A RID: 298
		PermissionRequiredPatchAvailable = 18000,
		// Token: 0x0400012B RID: 299
		PermissionRequiredSystemUpdate,
		// Token: 0x0400012C RID: 300
		PermissionAgeRestrictionFailure,
		// Token: 0x0400012D RID: 301
		PermissionAccountTypeFailure,
		// Token: 0x0400012E RID: 302
		PermissionChatRestriction,
		// Token: 0x0400012F RID: 303
		PermissionUGCRestriction,
		// Token: 0x04000130 RID: 304
		PermissionOnlinePlayRestricted,
		// Token: 0x04000131 RID: 305
		DesktopCrossplayApplicationNotBootstrapped = 19000,
		// Token: 0x04000132 RID: 306
		DesktopCrossplayServiceNotInstalled,
		// Token: 0x04000133 RID: 307
		DesktopCrossplayServiceStartFailed,
		// Token: 0x04000134 RID: 308
		DesktopCrossplayServiceNotRunning,
		// Token: 0x04000135 RID: 309
		UnexpectedError = 2147483647
	}
}
