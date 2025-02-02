using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200001B RID: 27
	public static class OpCodes
	{
		// Token: 0x0400017B RID: 379
		internal static readonly OpCode[] OneByteOpCode = new OpCode[225];

		// Token: 0x0400017C RID: 380
		internal static readonly OpCode[] TwoBytesOpCode = new OpCode[31];

		// Token: 0x0400017D RID: 381
		public static readonly OpCode Nop = new OpCode(83886335, 318768389);

		// Token: 0x0400017E RID: 382
		public static readonly OpCode Break = new OpCode(16843263, 318768389);

		// Token: 0x0400017F RID: 383
		public static readonly OpCode Ldarg_0 = new OpCode(84017919, 335545601);

		// Token: 0x04000180 RID: 384
		public static readonly OpCode Ldarg_1 = new OpCode(84083711, 335545601);

		// Token: 0x04000181 RID: 385
		public static readonly OpCode Ldarg_2 = new OpCode(84149503, 335545601);

		// Token: 0x04000182 RID: 386
		public static readonly OpCode Ldarg_3 = new OpCode(84215295, 335545601);

		// Token: 0x04000183 RID: 387
		public static readonly OpCode Ldloc_0 = new OpCode(84281087, 335545601);

		// Token: 0x04000184 RID: 388
		public static readonly OpCode Ldloc_1 = new OpCode(84346879, 335545601);

		// Token: 0x04000185 RID: 389
		public static readonly OpCode Ldloc_2 = new OpCode(84412671, 335545601);

		// Token: 0x04000186 RID: 390
		public static readonly OpCode Ldloc_3 = new OpCode(84478463, 335545601);

		// Token: 0x04000187 RID: 391
		public static readonly OpCode Stloc_0 = new OpCode(84544255, 318833921);

		// Token: 0x04000188 RID: 392
		public static readonly OpCode Stloc_1 = new OpCode(84610047, 318833921);

		// Token: 0x04000189 RID: 393
		public static readonly OpCode Stloc_2 = new OpCode(84675839, 318833921);

		// Token: 0x0400018A RID: 394
		public static readonly OpCode Stloc_3 = new OpCode(84741631, 318833921);

		// Token: 0x0400018B RID: 395
		public static readonly OpCode Ldarg_S = new OpCode(84807423, 335549185);

		// Token: 0x0400018C RID: 396
		public static readonly OpCode Ldarga_S = new OpCode(84873215, 369103617);

		// Token: 0x0400018D RID: 397
		public static readonly OpCode Starg_S = new OpCode(84939007, 318837505);

		// Token: 0x0400018E RID: 398
		public static readonly OpCode Ldloc_S = new OpCode(85004799, 335548929);

		// Token: 0x0400018F RID: 399
		public static readonly OpCode Ldloca_S = new OpCode(85070591, 369103361);

		// Token: 0x04000190 RID: 400
		public static readonly OpCode Stloc_S = new OpCode(85136383, 318837249);

		// Token: 0x04000191 RID: 401
		public static readonly OpCode Ldnull = new OpCode(85202175, 436208901);

		// Token: 0x04000192 RID: 402
		public static readonly OpCode Ldc_I4_M1 = new OpCode(85267967, 369100033);

		// Token: 0x04000193 RID: 403
		public static readonly OpCode Ldc_I4_0 = new OpCode(85333759, 369100033);

		// Token: 0x04000194 RID: 404
		public static readonly OpCode Ldc_I4_1 = new OpCode(85399551, 369100033);

		// Token: 0x04000195 RID: 405
		public static readonly OpCode Ldc_I4_2 = new OpCode(85465343, 369100033);

		// Token: 0x04000196 RID: 406
		public static readonly OpCode Ldc_I4_3 = new OpCode(85531135, 369100033);

		// Token: 0x04000197 RID: 407
		public static readonly OpCode Ldc_I4_4 = new OpCode(85596927, 369100033);

		// Token: 0x04000198 RID: 408
		public static readonly OpCode Ldc_I4_5 = new OpCode(85662719, 369100033);

		// Token: 0x04000199 RID: 409
		public static readonly OpCode Ldc_I4_6 = new OpCode(85728511, 369100033);

		// Token: 0x0400019A RID: 410
		public static readonly OpCode Ldc_I4_7 = new OpCode(85794303, 369100033);

		// Token: 0x0400019B RID: 411
		public static readonly OpCode Ldc_I4_8 = new OpCode(85860095, 369100033);

		// Token: 0x0400019C RID: 412
		public static readonly OpCode Ldc_I4_S = new OpCode(85925887, 369102849);

		// Token: 0x0400019D RID: 413
		public static readonly OpCode Ldc_I4 = new OpCode(85991679, 369099269);

		// Token: 0x0400019E RID: 414
		public static readonly OpCode Ldc_I8 = new OpCode(86057471, 385876741);

		// Token: 0x0400019F RID: 415
		public static readonly OpCode Ldc_R4 = new OpCode(86123263, 402657541);

		// Token: 0x040001A0 RID: 416
		public static readonly OpCode Ldc_R8 = new OpCode(86189055, 419432197);

		// Token: 0x040001A1 RID: 417
		public static readonly OpCode Dup = new OpCode(86255103, 352388357);

		// Token: 0x040001A2 RID: 418
		public static readonly OpCode Pop = new OpCode(86320895, 318833925);

		// Token: 0x040001A3 RID: 419
		public static readonly OpCode Jmp = new OpCode(36055039, 318768133);

		// Token: 0x040001A4 RID: 420
		public static readonly OpCode Call = new OpCode(36120831, 471532549);

		// Token: 0x040001A5 RID: 421
		public static readonly OpCode Calli = new OpCode(36186623, 471533573);

		// Token: 0x040001A6 RID: 422
		public static readonly OpCode Ret = new OpCode(120138495, 320537861);

		// Token: 0x040001A7 RID: 423
		public static readonly OpCode Br_S = new OpCode(2763775, 318770945);

		// Token: 0x040001A8 RID: 424
		public static readonly OpCode Brfalse_S = new OpCode(53161215, 318967553);

		// Token: 0x040001A9 RID: 425
		public static readonly OpCode Brtrue_S = new OpCode(53227007, 318967553);

		// Token: 0x040001AA RID: 426
		public static readonly OpCode Beq_S = new OpCode(53292799, 318902017);

		// Token: 0x040001AB RID: 427
		public static readonly OpCode Bge_S = new OpCode(53358591, 318902017);

		// Token: 0x040001AC RID: 428
		public static readonly OpCode Bgt_S = new OpCode(53424383, 318902017);

		// Token: 0x040001AD RID: 429
		public static readonly OpCode Ble_S = new OpCode(53490175, 318902017);

		// Token: 0x040001AE RID: 430
		public static readonly OpCode Blt_S = new OpCode(53555967, 318902017);

		// Token: 0x040001AF RID: 431
		public static readonly OpCode Bne_Un_S = new OpCode(53621759, 318902017);

		// Token: 0x040001B0 RID: 432
		public static readonly OpCode Bge_Un_S = new OpCode(53687551, 318902017);

		// Token: 0x040001B1 RID: 433
		public static readonly OpCode Bgt_Un_S = new OpCode(53753343, 318902017);

		// Token: 0x040001B2 RID: 434
		public static readonly OpCode Ble_Un_S = new OpCode(53819135, 318902017);

		// Token: 0x040001B3 RID: 435
		public static readonly OpCode Blt_Un_S = new OpCode(53884927, 318902017);

		// Token: 0x040001B4 RID: 436
		public static readonly OpCode Br = new OpCode(3619071, 318767109);

		// Token: 0x040001B5 RID: 437
		public static readonly OpCode Brfalse = new OpCode(54016511, 318963717);

		// Token: 0x040001B6 RID: 438
		public static readonly OpCode Brtrue = new OpCode(54082303, 318963717);

		// Token: 0x040001B7 RID: 439
		public static readonly OpCode Beq = new OpCode(54148095, 318898177);

		// Token: 0x040001B8 RID: 440
		public static readonly OpCode Bge = new OpCode(54213887, 318898177);

		// Token: 0x040001B9 RID: 441
		public static readonly OpCode Bgt = new OpCode(54279679, 318898177);

		// Token: 0x040001BA RID: 442
		public static readonly OpCode Ble = new OpCode(54345471, 318898177);

		// Token: 0x040001BB RID: 443
		public static readonly OpCode Blt = new OpCode(54411263, 318898177);

		// Token: 0x040001BC RID: 444
		public static readonly OpCode Bne_Un = new OpCode(54477055, 318898177);

		// Token: 0x040001BD RID: 445
		public static readonly OpCode Bge_Un = new OpCode(54542847, 318898177);

		// Token: 0x040001BE RID: 446
		public static readonly OpCode Bgt_Un = new OpCode(54608639, 318898177);

		// Token: 0x040001BF RID: 447
		public static readonly OpCode Ble_Un = new OpCode(54674431, 318898177);

		// Token: 0x040001C0 RID: 448
		public static readonly OpCode Blt_Un = new OpCode(54740223, 318898177);

		// Token: 0x040001C1 RID: 449
		public static readonly OpCode Switch = new OpCode(54806015, 318966277);

		// Token: 0x040001C2 RID: 450
		public static readonly OpCode Ldind_I1 = new OpCode(88426239, 369296645);

		// Token: 0x040001C3 RID: 451
		public static readonly OpCode Ldind_U1 = new OpCode(88492031, 369296645);

		// Token: 0x040001C4 RID: 452
		public static readonly OpCode Ldind_I2 = new OpCode(88557823, 369296645);

		// Token: 0x040001C5 RID: 453
		public static readonly OpCode Ldind_U2 = new OpCode(88623615, 369296645);

		// Token: 0x040001C6 RID: 454
		public static readonly OpCode Ldind_I4 = new OpCode(88689407, 369296645);

		// Token: 0x040001C7 RID: 455
		public static readonly OpCode Ldind_U4 = new OpCode(88755199, 369296645);

		// Token: 0x040001C8 RID: 456
		public static readonly OpCode Ldind_I8 = new OpCode(88820991, 386073861);

		// Token: 0x040001C9 RID: 457
		public static readonly OpCode Ldind_I = new OpCode(88886783, 369296645);

		// Token: 0x040001CA RID: 458
		public static readonly OpCode Ldind_R4 = new OpCode(88952575, 402851077);

		// Token: 0x040001CB RID: 459
		public static readonly OpCode Ldind_R8 = new OpCode(89018367, 419628293);

		// Token: 0x040001CC RID: 460
		public static readonly OpCode Ldind_Ref = new OpCode(89084159, 436405509);

		// Token: 0x040001CD RID: 461
		public static readonly OpCode Stind_Ref = new OpCode(89149951, 319096069);

		// Token: 0x040001CE RID: 462
		public static readonly OpCode Stind_I1 = new OpCode(89215743, 319096069);

		// Token: 0x040001CF RID: 463
		public static readonly OpCode Stind_I2 = new OpCode(89281535, 319096069);

		// Token: 0x040001D0 RID: 464
		public static readonly OpCode Stind_I4 = new OpCode(89347327, 319096069);

		// Token: 0x040001D1 RID: 465
		public static readonly OpCode Stind_I8 = new OpCode(89413119, 319161605);

		// Token: 0x040001D2 RID: 466
		public static readonly OpCode Stind_R4 = new OpCode(89478911, 319292677);

		// Token: 0x040001D3 RID: 467
		public static readonly OpCode Stind_R8 = new OpCode(89544703, 319358213);

		// Token: 0x040001D4 RID: 468
		public static readonly OpCode Add = new OpCode(89610495, 335676677);

		// Token: 0x040001D5 RID: 469
		public static readonly OpCode Sub = new OpCode(89676287, 335676677);

		// Token: 0x040001D6 RID: 470
		public static readonly OpCode Mul = new OpCode(89742079, 335676677);

		// Token: 0x040001D7 RID: 471
		public static readonly OpCode Div = new OpCode(89807871, 335676677);

		// Token: 0x040001D8 RID: 472
		public static readonly OpCode Div_Un = new OpCode(89873663, 335676677);

		// Token: 0x040001D9 RID: 473
		public static readonly OpCode Rem = new OpCode(89939455, 335676677);

		// Token: 0x040001DA RID: 474
		public static readonly OpCode Rem_Un = new OpCode(90005247, 335676677);

		// Token: 0x040001DB RID: 475
		public static readonly OpCode And = new OpCode(90071039, 335676677);

		// Token: 0x040001DC RID: 476
		public static readonly OpCode Or = new OpCode(90136831, 335676677);

		// Token: 0x040001DD RID: 477
		public static readonly OpCode Xor = new OpCode(90202623, 335676677);

		// Token: 0x040001DE RID: 478
		public static readonly OpCode Shl = new OpCode(90268415, 335676677);

		// Token: 0x040001DF RID: 479
		public static readonly OpCode Shr = new OpCode(90334207, 335676677);

		// Token: 0x040001E0 RID: 480
		public static readonly OpCode Shr_Un = new OpCode(90399999, 335676677);

		// Token: 0x040001E1 RID: 481
		public static readonly OpCode Neg = new OpCode(90465791, 335611141);

		// Token: 0x040001E2 RID: 482
		public static readonly OpCode Not = new OpCode(90531583, 335611141);

		// Token: 0x040001E3 RID: 483
		public static readonly OpCode Conv_I1 = new OpCode(90597375, 369165573);

		// Token: 0x040001E4 RID: 484
		public static readonly OpCode Conv_I2 = new OpCode(90663167, 369165573);

		// Token: 0x040001E5 RID: 485
		public static readonly OpCode Conv_I4 = new OpCode(90728959, 369165573);

		// Token: 0x040001E6 RID: 486
		public static readonly OpCode Conv_I8 = new OpCode(90794751, 385942789);

		// Token: 0x040001E7 RID: 487
		public static readonly OpCode Conv_R4 = new OpCode(90860543, 402720005);

		// Token: 0x040001E8 RID: 488
		public static readonly OpCode Conv_R8 = new OpCode(90926335, 419497221);

		// Token: 0x040001E9 RID: 489
		public static readonly OpCode Conv_U4 = new OpCode(90992127, 369165573);

		// Token: 0x040001EA RID: 490
		public static readonly OpCode Conv_U8 = new OpCode(91057919, 385942789);

		// Token: 0x040001EB RID: 491
		public static readonly OpCode Callvirt = new OpCode(40792063, 471532547);

		// Token: 0x040001EC RID: 492
		public static readonly OpCode Cpobj = new OpCode(91189503, 319097859);

		// Token: 0x040001ED RID: 493
		public static readonly OpCode Ldobj = new OpCode(91255295, 335744003);

		// Token: 0x040001EE RID: 494
		public static readonly OpCode Ldstr = new OpCode(91321087, 436209923);

		// Token: 0x040001EF RID: 495
		public static readonly OpCode Newobj = new OpCode(41055231, 437978115);

		// Token: 0x040001F0 RID: 496
		public static readonly OpCode Castclass = new OpCode(91452671, 436866051);

		// Token: 0x040001F1 RID: 497
		public static readonly OpCode Isinst = new OpCode(91518463, 369757187);

		// Token: 0x040001F2 RID: 498
		public static readonly OpCode Conv_R_Un = new OpCode(91584255, 419497221);

		// Token: 0x040001F3 RID: 499
		public static readonly OpCode Unbox = new OpCode(91650559, 369757189);

		// Token: 0x040001F4 RID: 500
		public static readonly OpCode Throw = new OpCode(142047999, 319423747);

		// Token: 0x040001F5 RID: 501
		public static readonly OpCode Ldfld = new OpCode(91782143, 336199939);

		// Token: 0x040001F6 RID: 502
		public static readonly OpCode Ldflda = new OpCode(91847935, 369754371);

		// Token: 0x040001F7 RID: 503
		public static readonly OpCode Stfld = new OpCode(91913727, 319488259);

		// Token: 0x040001F8 RID: 504
		public static readonly OpCode Ldsfld = new OpCode(91979519, 335544579);

		// Token: 0x040001F9 RID: 505
		public static readonly OpCode Ldsflda = new OpCode(92045311, 369099011);

		// Token: 0x040001FA RID: 506
		public static readonly OpCode Stsfld = new OpCode(92111103, 318832899);

		// Token: 0x040001FB RID: 507
		public static readonly OpCode Stobj = new OpCode(92176895, 319032323);

		// Token: 0x040001FC RID: 508
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(92242687, 369165573);

		// Token: 0x040001FD RID: 509
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(92308479, 369165573);

		// Token: 0x040001FE RID: 510
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(92374271, 369165573);

		// Token: 0x040001FF RID: 511
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(92440063, 385942789);

		// Token: 0x04000200 RID: 512
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(92505855, 369165573);

		// Token: 0x04000201 RID: 513
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(92571647, 369165573);

		// Token: 0x04000202 RID: 514
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(92637439, 369165573);

		// Token: 0x04000203 RID: 515
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(92703231, 385942789);

		// Token: 0x04000204 RID: 516
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(92769023, 369165573);

		// Token: 0x04000205 RID: 517
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(92834815, 369165573);

		// Token: 0x04000206 RID: 518
		public static readonly OpCode Box = new OpCode(92900607, 436276229);

		// Token: 0x04000207 RID: 519
		public static readonly OpCode Newarr = new OpCode(92966399, 436407299);

		// Token: 0x04000208 RID: 520
		public static readonly OpCode Ldlen = new OpCode(93032191, 369755395);

		// Token: 0x04000209 RID: 521
		public static readonly OpCode Ldelema = new OpCode(93097983, 369888259);

		// Token: 0x0400020A RID: 522
		public static readonly OpCode Ldelem_I1 = new OpCode(93163775, 369886467);

		// Token: 0x0400020B RID: 523
		public static readonly OpCode Ldelem_U1 = new OpCode(93229567, 369886467);

		// Token: 0x0400020C RID: 524
		public static readonly OpCode Ldelem_I2 = new OpCode(93295359, 369886467);

		// Token: 0x0400020D RID: 525
		public static readonly OpCode Ldelem_U2 = new OpCode(93361151, 369886467);

		// Token: 0x0400020E RID: 526
		public static readonly OpCode Ldelem_I4 = new OpCode(93426943, 369886467);

		// Token: 0x0400020F RID: 527
		public static readonly OpCode Ldelem_U4 = new OpCode(93492735, 369886467);

		// Token: 0x04000210 RID: 528
		public static readonly OpCode Ldelem_I8 = new OpCode(93558527, 386663683);

		// Token: 0x04000211 RID: 529
		public static readonly OpCode Ldelem_I = new OpCode(93624319, 369886467);

		// Token: 0x04000212 RID: 530
		public static readonly OpCode Ldelem_R4 = new OpCode(93690111, 403440899);

		// Token: 0x04000213 RID: 531
		public static readonly OpCode Ldelem_R8 = new OpCode(93755903, 420218115);

		// Token: 0x04000214 RID: 532
		public static readonly OpCode Ldelem_Ref = new OpCode(93821695, 436995331);

		// Token: 0x04000215 RID: 533
		public static readonly OpCode Stelem_I = new OpCode(93887487, 319620355);

		// Token: 0x04000216 RID: 534
		public static readonly OpCode Stelem_I1 = new OpCode(93953279, 319620355);

		// Token: 0x04000217 RID: 535
		public static readonly OpCode Stelem_I2 = new OpCode(94019071, 319620355);

		// Token: 0x04000218 RID: 536
		public static readonly OpCode Stelem_I4 = new OpCode(94084863, 319620355);

		// Token: 0x04000219 RID: 537
		public static readonly OpCode Stelem_I8 = new OpCode(94150655, 319685891);

		// Token: 0x0400021A RID: 538
		public static readonly OpCode Stelem_R4 = new OpCode(94216447, 319751427);

		// Token: 0x0400021B RID: 539
		public static readonly OpCode Stelem_R8 = new OpCode(94282239, 319816963);

		// Token: 0x0400021C RID: 540
		public static readonly OpCode Stelem_Ref = new OpCode(94348031, 319882499);

		// Token: 0x0400021D RID: 541
		public static readonly OpCode Ldelem_Any = new OpCode(94413823, 336333827);

		// Token: 0x0400021E RID: 542
		public static readonly OpCode Stelem_Any = new OpCode(94479615, 319884291);

		// Token: 0x0400021F RID: 543
		public static readonly OpCode Unbox_Any = new OpCode(94545407, 336202755);

		// Token: 0x04000220 RID: 544
		public static readonly OpCode Conv_Ovf_I1 = new OpCode(94614527, 369165573);

		// Token: 0x04000221 RID: 545
		public static readonly OpCode Conv_Ovf_U1 = new OpCode(94680319, 369165573);

		// Token: 0x04000222 RID: 546
		public static readonly OpCode Conv_Ovf_I2 = new OpCode(94746111, 369165573);

		// Token: 0x04000223 RID: 547
		public static readonly OpCode Conv_Ovf_U2 = new OpCode(94811903, 369165573);

		// Token: 0x04000224 RID: 548
		public static readonly OpCode Conv_Ovf_I4 = new OpCode(94877695, 369165573);

		// Token: 0x04000225 RID: 549
		public static readonly OpCode Conv_Ovf_U4 = new OpCode(94943487, 369165573);

		// Token: 0x04000226 RID: 550
		public static readonly OpCode Conv_Ovf_I8 = new OpCode(95009279, 385942789);

		// Token: 0x04000227 RID: 551
		public static readonly OpCode Conv_Ovf_U8 = new OpCode(95075071, 385942789);

		// Token: 0x04000228 RID: 552
		public static readonly OpCode Refanyval = new OpCode(95142655, 369167365);

		// Token: 0x04000229 RID: 553
		public static readonly OpCode Ckfinite = new OpCode(95208447, 419497221);

		// Token: 0x0400022A RID: 554
		public static readonly OpCode Mkrefany = new OpCode(95274751, 335744005);

		// Token: 0x0400022B RID: 555
		public static readonly OpCode Ldtoken = new OpCode(95342847, 369101573);

		// Token: 0x0400022C RID: 556
		public static readonly OpCode Conv_U2 = new OpCode(95408639, 369165573);

		// Token: 0x0400022D RID: 557
		public static readonly OpCode Conv_U1 = new OpCode(95474431, 369165573);

		// Token: 0x0400022E RID: 558
		public static readonly OpCode Conv_I = new OpCode(95540223, 369165573);

		// Token: 0x0400022F RID: 559
		public static readonly OpCode Conv_Ovf_I = new OpCode(95606015, 369165573);

		// Token: 0x04000230 RID: 560
		public static readonly OpCode Conv_Ovf_U = new OpCode(95671807, 369165573);

		// Token: 0x04000231 RID: 561
		public static readonly OpCode Add_Ovf = new OpCode(95737599, 335676677);

		// Token: 0x04000232 RID: 562
		public static readonly OpCode Add_Ovf_Un = new OpCode(95803391, 335676677);

		// Token: 0x04000233 RID: 563
		public static readonly OpCode Mul_Ovf = new OpCode(95869183, 335676677);

		// Token: 0x04000234 RID: 564
		public static readonly OpCode Mul_Ovf_Un = new OpCode(95934975, 335676677);

		// Token: 0x04000235 RID: 565
		public static readonly OpCode Sub_Ovf = new OpCode(96000767, 335676677);

		// Token: 0x04000236 RID: 566
		public static readonly OpCode Sub_Ovf_Un = new OpCode(96066559, 335676677);

		// Token: 0x04000237 RID: 567
		public static readonly OpCode Endfinally = new OpCode(129686783, 318768389);

		// Token: 0x04000238 RID: 568
		public static readonly OpCode Leave = new OpCode(12312063, 319946757);

		// Token: 0x04000239 RID: 569
		public static readonly OpCode Leave_S = new OpCode(12377855, 319950593);

		// Token: 0x0400023A RID: 570
		public static readonly OpCode Stind_I = new OpCode(96329727, 319096069);

		// Token: 0x0400023B RID: 571
		public static readonly OpCode Conv_U = new OpCode(96395519, 369165573);

		// Token: 0x0400023C RID: 572
		public static readonly OpCode Arglist = new OpCode(96403710, 369100037);

		// Token: 0x0400023D RID: 573
		public static readonly OpCode Ceq = new OpCode(96469502, 369231109);

		// Token: 0x0400023E RID: 574
		public static readonly OpCode Cgt = new OpCode(96535294, 369231109);

		// Token: 0x0400023F RID: 575
		public static readonly OpCode Cgt_Un = new OpCode(96601086, 369231109);

		// Token: 0x04000240 RID: 576
		public static readonly OpCode Clt = new OpCode(96666878, 369231109);

		// Token: 0x04000241 RID: 577
		public static readonly OpCode Clt_Un = new OpCode(96732670, 369231109);

		// Token: 0x04000242 RID: 578
		public static readonly OpCode Ldftn = new OpCode(96798462, 369099781);

		// Token: 0x04000243 RID: 579
		public static readonly OpCode Ldvirtftn = new OpCode(96864254, 369755141);

		// Token: 0x04000244 RID: 580
		public static readonly OpCode Ldarg = new OpCode(96930302, 335547909);

		// Token: 0x04000245 RID: 581
		public static readonly OpCode Ldarga = new OpCode(96996094, 369102341);

		// Token: 0x04000246 RID: 582
		public static readonly OpCode Starg = new OpCode(97061886, 318836229);

		// Token: 0x04000247 RID: 583
		public static readonly OpCode Ldloc = new OpCode(97127678, 335547653);

		// Token: 0x04000248 RID: 584
		public static readonly OpCode Ldloca = new OpCode(97193470, 369102085);

		// Token: 0x04000249 RID: 585
		public static readonly OpCode Stloc = new OpCode(97259262, 318835973);

		// Token: 0x0400024A RID: 586
		public static readonly OpCode Localloc = new OpCode(97325054, 369296645);

		// Token: 0x0400024B RID: 587
		public static readonly OpCode Endfilter = new OpCode(130945534, 318964997);

		// Token: 0x0400024C RID: 588
		public static readonly OpCode Unaligned = new OpCode(80679678, 318771204);

		// Token: 0x0400024D RID: 589
		public static readonly OpCode Volatile = new OpCode(80745470, 318768388);

		// Token: 0x0400024E RID: 590
		public static readonly OpCode Tail = new OpCode(80811262, 318768388);

		// Token: 0x0400024F RID: 591
		public static readonly OpCode Initobj = new OpCode(97654270, 318966787);

		// Token: 0x04000250 RID: 592
		public static readonly OpCode Constrained = new OpCode(97720062, 318770180);

		// Token: 0x04000251 RID: 593
		public static readonly OpCode Cpblk = new OpCode(97785854, 319227141);

		// Token: 0x04000252 RID: 594
		public static readonly OpCode Initblk = new OpCode(97851646, 319227141);

		// Token: 0x04000253 RID: 595
		public static readonly OpCode No = new OpCode(97917438, 318771204);

		// Token: 0x04000254 RID: 596
		public static readonly OpCode Rethrow = new OpCode(148314878, 318768387);

		// Token: 0x04000255 RID: 597
		public static readonly OpCode Sizeof = new OpCode(98049278, 369101829);

		// Token: 0x04000256 RID: 598
		public static readonly OpCode Refanytype = new OpCode(98115070, 369165573);

		// Token: 0x04000257 RID: 599
		public static readonly OpCode Readonly = new OpCode(98180862, 318768388);
	}
}
