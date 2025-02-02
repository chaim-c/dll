using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000055 RID: 85
	[Flags]
	public enum KeyCombination
	{
		// Token: 0x040001C4 RID: 452
		ModifierShift = 16,
		// Token: 0x040001C5 RID: 453
		KeyTypeMask = 65535,
		// Token: 0x040001C6 RID: 454
		ModifierMask = -65536,
		// Token: 0x040001C7 RID: 455
		Shift = 65536,
		// Token: 0x040001C8 RID: 456
		Control = 131072,
		// Token: 0x040001C9 RID: 457
		Alt = 262144,
		// Token: 0x040001CA RID: 458
		Meta = 524288,
		// Token: 0x040001CB RID: 459
		ValidModifierMask = 983040,
		// Token: 0x040001CC RID: 460
		None = 0,
		// Token: 0x040001CD RID: 461
		Space = 1,
		// Token: 0x040001CE RID: 462
		Backspace = 2,
		// Token: 0x040001CF RID: 463
		Tab = 3,
		// Token: 0x040001D0 RID: 464
		Escape = 4,
		// Token: 0x040001D1 RID: 465
		PageUp = 5,
		// Token: 0x040001D2 RID: 466
		PageDown = 6,
		// Token: 0x040001D3 RID: 467
		End = 7,
		// Token: 0x040001D4 RID: 468
		Home = 8,
		// Token: 0x040001D5 RID: 469
		Insert = 9,
		// Token: 0x040001D6 RID: 470
		Delete = 10,
		// Token: 0x040001D7 RID: 471
		Left = 11,
		// Token: 0x040001D8 RID: 472
		Up = 12,
		// Token: 0x040001D9 RID: 473
		Right = 13,
		// Token: 0x040001DA RID: 474
		Down = 14,
		// Token: 0x040001DB RID: 475
		Key0 = 15,
		// Token: 0x040001DC RID: 476
		Key1 = 16,
		// Token: 0x040001DD RID: 477
		Key2 = 17,
		// Token: 0x040001DE RID: 478
		Key3 = 18,
		// Token: 0x040001DF RID: 479
		Key4 = 19,
		// Token: 0x040001E0 RID: 480
		Key5 = 20,
		// Token: 0x040001E1 RID: 481
		Key6 = 21,
		// Token: 0x040001E2 RID: 482
		Key7 = 22,
		// Token: 0x040001E3 RID: 483
		Key8 = 23,
		// Token: 0x040001E4 RID: 484
		Key9 = 24,
		// Token: 0x040001E5 RID: 485
		KeyA = 25,
		// Token: 0x040001E6 RID: 486
		KeyB = 26,
		// Token: 0x040001E7 RID: 487
		KeyC = 27,
		// Token: 0x040001E8 RID: 488
		KeyD = 28,
		// Token: 0x040001E9 RID: 489
		KeyE = 29,
		// Token: 0x040001EA RID: 490
		KeyF = 30,
		// Token: 0x040001EB RID: 491
		KeyG = 31,
		// Token: 0x040001EC RID: 492
		KeyH = 32,
		// Token: 0x040001ED RID: 493
		KeyI = 33,
		// Token: 0x040001EE RID: 494
		KeyJ = 34,
		// Token: 0x040001EF RID: 495
		KeyK = 35,
		// Token: 0x040001F0 RID: 496
		KeyL = 36,
		// Token: 0x040001F1 RID: 497
		KeyM = 37,
		// Token: 0x040001F2 RID: 498
		KeyN = 38,
		// Token: 0x040001F3 RID: 499
		KeyO = 39,
		// Token: 0x040001F4 RID: 500
		KeyP = 40,
		// Token: 0x040001F5 RID: 501
		KeyQ = 41,
		// Token: 0x040001F6 RID: 502
		KeyR = 42,
		// Token: 0x040001F7 RID: 503
		KeyS = 43,
		// Token: 0x040001F8 RID: 504
		KeyT = 44,
		// Token: 0x040001F9 RID: 505
		KeyU = 45,
		// Token: 0x040001FA RID: 506
		KeyV = 46,
		// Token: 0x040001FB RID: 507
		KeyW = 47,
		// Token: 0x040001FC RID: 508
		KeyX = 48,
		// Token: 0x040001FD RID: 509
		KeyY = 49,
		// Token: 0x040001FE RID: 510
		KeyZ = 50,
		// Token: 0x040001FF RID: 511
		Numpad0 = 51,
		// Token: 0x04000200 RID: 512
		Numpad1 = 52,
		// Token: 0x04000201 RID: 513
		Numpad2 = 53,
		// Token: 0x04000202 RID: 514
		Numpad3 = 54,
		// Token: 0x04000203 RID: 515
		Numpad4 = 55,
		// Token: 0x04000204 RID: 516
		Numpad5 = 56,
		// Token: 0x04000205 RID: 517
		Numpad6 = 57,
		// Token: 0x04000206 RID: 518
		Numpad7 = 58,
		// Token: 0x04000207 RID: 519
		Numpad8 = 59,
		// Token: 0x04000208 RID: 520
		Numpad9 = 60,
		// Token: 0x04000209 RID: 521
		NumpadAsterisk = 61,
		// Token: 0x0400020A RID: 522
		NumpadPlus = 62,
		// Token: 0x0400020B RID: 523
		NumpadMinus = 63,
		// Token: 0x0400020C RID: 524
		NumpadPeriod = 64,
		// Token: 0x0400020D RID: 525
		NumpadDivide = 65,
		// Token: 0x0400020E RID: 526
		F1 = 66,
		// Token: 0x0400020F RID: 527
		F2 = 67,
		// Token: 0x04000210 RID: 528
		F3 = 68,
		// Token: 0x04000211 RID: 529
		F4 = 69,
		// Token: 0x04000212 RID: 530
		F5 = 70,
		// Token: 0x04000213 RID: 531
		F6 = 71,
		// Token: 0x04000214 RID: 532
		F7 = 72,
		// Token: 0x04000215 RID: 533
		F8 = 73,
		// Token: 0x04000216 RID: 534
		F9 = 74,
		// Token: 0x04000217 RID: 535
		F10 = 75,
		// Token: 0x04000218 RID: 536
		F11 = 76,
		// Token: 0x04000219 RID: 537
		F12 = 77,
		// Token: 0x0400021A RID: 538
		F13 = 78,
		// Token: 0x0400021B RID: 539
		F14 = 79,
		// Token: 0x0400021C RID: 540
		F15 = 80,
		// Token: 0x0400021D RID: 541
		F16 = 81,
		// Token: 0x0400021E RID: 542
		F17 = 82,
		// Token: 0x0400021F RID: 543
		F18 = 83,
		// Token: 0x04000220 RID: 544
		F19 = 84,
		// Token: 0x04000221 RID: 545
		F20 = 85,
		// Token: 0x04000222 RID: 546
		F21 = 86,
		// Token: 0x04000223 RID: 547
		F22 = 87,
		// Token: 0x04000224 RID: 548
		F23 = 88,
		// Token: 0x04000225 RID: 549
		F24 = 89,
		// Token: 0x04000226 RID: 550
		OemPlus = 90,
		// Token: 0x04000227 RID: 551
		OemComma = 91,
		// Token: 0x04000228 RID: 552
		OemMinus = 92,
		// Token: 0x04000229 RID: 553
		OemPeriod = 93,
		// Token: 0x0400022A RID: 554
		Oem1 = 94,
		// Token: 0x0400022B RID: 555
		Oem2 = 95,
		// Token: 0x0400022C RID: 556
		Oem3 = 96,
		// Token: 0x0400022D RID: 557
		Oem4 = 97,
		// Token: 0x0400022E RID: 558
		Oem5 = 98,
		// Token: 0x0400022F RID: 559
		Oem6 = 99,
		// Token: 0x04000230 RID: 560
		Oem7 = 100,
		// Token: 0x04000231 RID: 561
		Oem8 = 101,
		// Token: 0x04000232 RID: 562
		MaxKeyType = 102
	}
}
