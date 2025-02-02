using System;

namespace TaleWorlds.InputSystem
{
	// Token: 0x0200000D RID: 13
	public enum VirtualKeyCode
	{
		// Token: 0x040000D2 RID: 210
		Invalid = -1,
		// Token: 0x040000D3 RID: 211
		D1 = 49,
		// Token: 0x040000D4 RID: 212
		D2,
		// Token: 0x040000D5 RID: 213
		D3,
		// Token: 0x040000D6 RID: 214
		D4,
		// Token: 0x040000D7 RID: 215
		D5,
		// Token: 0x040000D8 RID: 216
		D6,
		// Token: 0x040000D9 RID: 217
		D7,
		// Token: 0x040000DA RID: 218
		D8,
		// Token: 0x040000DB RID: 219
		D9,
		// Token: 0x040000DC RID: 220
		D0 = 48,
		// Token: 0x040000DD RID: 221
		A = 65,
		// Token: 0x040000DE RID: 222
		B,
		// Token: 0x040000DF RID: 223
		C,
		// Token: 0x040000E0 RID: 224
		D,
		// Token: 0x040000E1 RID: 225
		E,
		// Token: 0x040000E2 RID: 226
		F,
		// Token: 0x040000E3 RID: 227
		G,
		// Token: 0x040000E4 RID: 228
		H,
		// Token: 0x040000E5 RID: 229
		I,
		// Token: 0x040000E6 RID: 230
		J,
		// Token: 0x040000E7 RID: 231
		K,
		// Token: 0x040000E8 RID: 232
		L,
		// Token: 0x040000E9 RID: 233
		M,
		// Token: 0x040000EA RID: 234
		N,
		// Token: 0x040000EB RID: 235
		O,
		// Token: 0x040000EC RID: 236
		P,
		// Token: 0x040000ED RID: 237
		Q,
		// Token: 0x040000EE RID: 238
		R,
		// Token: 0x040000EF RID: 239
		S,
		// Token: 0x040000F0 RID: 240
		T,
		// Token: 0x040000F1 RID: 241
		U,
		// Token: 0x040000F2 RID: 242
		V,
		// Token: 0x040000F3 RID: 243
		W,
		// Token: 0x040000F4 RID: 244
		X,
		// Token: 0x040000F5 RID: 245
		Y,
		// Token: 0x040000F6 RID: 246
		Z,
		// Token: 0x040000F7 RID: 247
		Numpad0 = 96,
		// Token: 0x040000F8 RID: 248
		Numpad1,
		// Token: 0x040000F9 RID: 249
		Numpad2,
		// Token: 0x040000FA RID: 250
		Numpad3,
		// Token: 0x040000FB RID: 251
		Numpad4,
		// Token: 0x040000FC RID: 252
		Numpad5,
		// Token: 0x040000FD RID: 253
		Numpad6,
		// Token: 0x040000FE RID: 254
		Numpad7,
		// Token: 0x040000FF RID: 255
		Numpad8,
		// Token: 0x04000100 RID: 256
		Numpad9,
		// Token: 0x04000101 RID: 257
		NumLock = 144,
		// Token: 0x04000102 RID: 258
		NumpadSlash = 111,
		// Token: 0x04000103 RID: 259
		NumpadMultiply = 106,
		// Token: 0x04000104 RID: 260
		NumpadMinus = 109,
		// Token: 0x04000105 RID: 261
		NumpadPlus = 107,
		// Token: 0x04000106 RID: 262
		NumpadEnter,
		// Token: 0x04000107 RID: 263
		NumpadPeriod = 110,
		// Token: 0x04000108 RID: 264
		Insert = 45,
		// Token: 0x04000109 RID: 265
		Delete,
		// Token: 0x0400010A RID: 266
		Home = 36,
		// Token: 0x0400010B RID: 267
		End = 35,
		// Token: 0x0400010C RID: 268
		PageUp = 33,
		// Token: 0x0400010D RID: 269
		PageDown,
		// Token: 0x0400010E RID: 270
		Up = 38,
		// Token: 0x0400010F RID: 271
		Down = 40,
		// Token: 0x04000110 RID: 272
		Left = 37,
		// Token: 0x04000111 RID: 273
		Right = 39,
		// Token: 0x04000112 RID: 274
		F1 = 112,
		// Token: 0x04000113 RID: 275
		F2,
		// Token: 0x04000114 RID: 276
		F3,
		// Token: 0x04000115 RID: 277
		F4,
		// Token: 0x04000116 RID: 278
		F5,
		// Token: 0x04000117 RID: 279
		F6,
		// Token: 0x04000118 RID: 280
		F7,
		// Token: 0x04000119 RID: 281
		F8,
		// Token: 0x0400011A RID: 282
		F9,
		// Token: 0x0400011B RID: 283
		F10,
		// Token: 0x0400011C RID: 284
		F11,
		// Token: 0x0400011D RID: 285
		F12,
		// Token: 0x0400011E RID: 286
		F13,
		// Token: 0x0400011F RID: 287
		F14,
		// Token: 0x04000120 RID: 288
		F15,
		// Token: 0x04000121 RID: 289
		F16,
		// Token: 0x04000122 RID: 290
		F17,
		// Token: 0x04000123 RID: 291
		F18,
		// Token: 0x04000124 RID: 292
		F19,
		// Token: 0x04000125 RID: 293
		F20,
		// Token: 0x04000126 RID: 294
		F21,
		// Token: 0x04000127 RID: 295
		F22,
		// Token: 0x04000128 RID: 296
		F23,
		// Token: 0x04000129 RID: 297
		F24,
		// Token: 0x0400012A RID: 298
		Space = 32,
		// Token: 0x0400012B RID: 299
		Escape = 27,
		// Token: 0x0400012C RID: 300
		Enter = 13,
		// Token: 0x0400012D RID: 301
		Tab = 9,
		// Token: 0x0400012E RID: 302
		BackSpace = 8,
		// Token: 0x0400012F RID: 303
		OpenBraces = 219,
		// Token: 0x04000130 RID: 304
		CloseBraces = 221,
		// Token: 0x04000131 RID: 305
		Comma = 188,
		// Token: 0x04000132 RID: 306
		Period = 190,
		// Token: 0x04000133 RID: 307
		Slash,
		// Token: 0x04000134 RID: 308
		BackSlash = 220,
		// Token: 0x04000135 RID: 309
		Equals = 187,
		// Token: 0x04000136 RID: 310
		Minus = 189,
		// Token: 0x04000137 RID: 311
		SemiColon = 186,
		// Token: 0x04000138 RID: 312
		Apostrophe = 222,
		// Token: 0x04000139 RID: 313
		Tilde = 192,
		// Token: 0x0400013A RID: 314
		CapsLock = 20,
		// Token: 0x0400013B RID: 315
		Extended1 = 223,
		// Token: 0x0400013C RID: 316
		Extended2 = 226,
		// Token: 0x0400013D RID: 317
		LeftShift = 160,
		// Token: 0x0400013E RID: 318
		RightShift,
		// Token: 0x0400013F RID: 319
		LeftControl,
		// Token: 0x04000140 RID: 320
		RightControl,
		// Token: 0x04000141 RID: 321
		LeftAlt,
		// Token: 0x04000142 RID: 322
		RightAlt
	}
}
