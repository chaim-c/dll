﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000B2 RID: 178
	public sealed class Monster : MBObjectBase
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001D107 File Offset: 0x0001B307
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001D10F File Offset: 0x0001B30F
		public string BaseMonster { get; private set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001D118 File Offset: 0x0001B318
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001D120 File Offset: 0x0001B320
		public float BodyCapsuleRadius { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001D129 File Offset: 0x0001B329
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001D131 File Offset: 0x0001B331
		public Vec3 BodyCapsulePoint1 { get; private set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001D13A File Offset: 0x0001B33A
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001D142 File Offset: 0x0001B342
		public Vec3 BodyCapsulePoint2 { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001D14B File Offset: 0x0001B34B
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001D153 File Offset: 0x0001B353
		public float CrouchedBodyCapsuleRadius { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001D15C File Offset: 0x0001B35C
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0001D164 File Offset: 0x0001B364
		public Vec3 CrouchedBodyCapsulePoint1 { get; private set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001D16D File Offset: 0x0001B36D
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001D175 File Offset: 0x0001B375
		public Vec3 CrouchedBodyCapsulePoint2 { get; private set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001D17E File Offset: 0x0001B37E
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0001D186 File Offset: 0x0001B386
		public AgentFlag Flags { get; private set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001D18F File Offset: 0x0001B38F
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0001D197 File Offset: 0x0001B397
		public int Weight { get; private set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		public int HitPoints { get; private set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001D1B1 File Offset: 0x0001B3B1
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0001D1B9 File Offset: 0x0001B3B9
		public string ActionSetCode { get; private set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001D1C2 File Offset: 0x0001B3C2
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public string FemaleActionSetCode { get; private set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001D1D3 File Offset: 0x0001B3D3
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001D1DB File Offset: 0x0001B3DB
		public int NumPaces { get; private set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0001D1EC File Offset: 0x0001B3EC
		public string MonsterUsage { get; private set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001D1F5 File Offset: 0x0001B3F5
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001D1FD File Offset: 0x0001B3FD
		public float WalkingSpeedLimit { get; private set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001D206 File Offset: 0x0001B406
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0001D20E File Offset: 0x0001B40E
		public float CrouchWalkingSpeedLimit { get; private set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001D217 File Offset: 0x0001B417
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0001D21F File Offset: 0x0001B41F
		public float JumpAcceleration { get; private set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001D228 File Offset: 0x0001B428
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0001D230 File Offset: 0x0001B430
		public float AbsorbedDamageRatio { get; private set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001D239 File Offset: 0x0001B439
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0001D241 File Offset: 0x0001B441
		public string SoundAndCollisionInfoClassName { get; private set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0001D24A File Offset: 0x0001B44A
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0001D252 File Offset: 0x0001B452
		public float RiderCameraHeightAdder { get; private set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001D25B File Offset: 0x0001B45B
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0001D263 File Offset: 0x0001B463
		public float RiderBodyCapsuleHeightAdder { get; private set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001D26C File Offset: 0x0001B46C
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x0001D274 File Offset: 0x0001B474
		public float RiderBodyCapsuleForwardAdder { get; private set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001D27D File Offset: 0x0001B47D
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0001D285 File Offset: 0x0001B485
		public float StandingChestHeight { get; private set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001D28E File Offset: 0x0001B48E
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0001D296 File Offset: 0x0001B496
		public float StandingPelvisHeight { get; private set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001D29F File Offset: 0x0001B49F
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0001D2A7 File Offset: 0x0001B4A7
		public float StandingEyeHeight { get; private set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0001D2B8 File Offset: 0x0001B4B8
		public float CrouchEyeHeight { get; private set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0001D2C1 File Offset: 0x0001B4C1
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x0001D2C9 File Offset: 0x0001B4C9
		public float MountedEyeHeight { get; private set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001D2D2 File Offset: 0x0001B4D2
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0001D2DA File Offset: 0x0001B4DA
		public float RiderEyeHeightAdder { get; private set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001D2E3 File Offset: 0x0001B4E3
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0001D2EB File Offset: 0x0001B4EB
		public Vec3 EyeOffsetWrtHead { get; private set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0001D2FC File Offset: 0x0001B4FC
		public Vec3 FirstPersonCameraOffsetWrtHead { get; private set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0001D305 File Offset: 0x0001B505
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0001D30D File Offset: 0x0001B50D
		public float ArmLength { get; private set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0001D316 File Offset: 0x0001B516
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0001D31E File Offset: 0x0001B51E
		public float ArmWeight { get; private set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0001D327 File Offset: 0x0001B527
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0001D32F File Offset: 0x0001B52F
		public float JumpSpeedLimit { get; private set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0001D338 File Offset: 0x0001B538
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0001D340 File Offset: 0x0001B540
		public float RelativeSpeedLimitForCharge { get; private set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0001D349 File Offset: 0x0001B549
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0001D351 File Offset: 0x0001B551
		public int FamilyType { get; private set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0001D35A File Offset: 0x0001B55A
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0001D362 File Offset: 0x0001B562
		public sbyte[] IndicesOfRagdollBonesToCheckForCorpses { get; private set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0001D36B File Offset: 0x0001B56B
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0001D373 File Offset: 0x0001B573
		public sbyte[] RagdollFallSoundBoneIndices { get; private set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001D37C File Offset: 0x0001B57C
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0001D384 File Offset: 0x0001B584
		public sbyte HeadLookDirectionBoneIndex { get; private set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001D38D File Offset: 0x0001B58D
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0001D395 File Offset: 0x0001B595
		public sbyte SpineLowerBoneIndex { get; private set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001D39E File Offset: 0x0001B59E
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0001D3A6 File Offset: 0x0001B5A6
		public sbyte SpineUpperBoneIndex { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001D3AF File Offset: 0x0001B5AF
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001D3B7 File Offset: 0x0001B5B7
		public sbyte ThoraxLookDirectionBoneIndex { get; private set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001D3C8 File Offset: 0x0001B5C8
		public sbyte NeckRootBoneIndex { get; private set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001D3D1 File Offset: 0x0001B5D1
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0001D3D9 File Offset: 0x0001B5D9
		public sbyte PelvisBoneIndex { get; private set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001D3E2 File Offset: 0x0001B5E2
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0001D3EA File Offset: 0x0001B5EA
		public sbyte RightUpperArmBoneIndex { get; private set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0001D3F3 File Offset: 0x0001B5F3
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0001D3FB File Offset: 0x0001B5FB
		public sbyte LeftUpperArmBoneIndex { get; private set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001D404 File Offset: 0x0001B604
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0001D40C File Offset: 0x0001B60C
		public sbyte FallBlowDamageBoneIndex { get; private set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001D415 File Offset: 0x0001B615
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001D41D File Offset: 0x0001B61D
		public sbyte TerrainDecalBone0Index { get; private set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001D426 File Offset: 0x0001B626
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0001D42E File Offset: 0x0001B62E
		public sbyte TerrainDecalBone1Index { get; private set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001D437 File Offset: 0x0001B637
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0001D43F File Offset: 0x0001B63F
		public sbyte[] RagdollStationaryCheckBoneIndices { get; private set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0001D448 File Offset: 0x0001B648
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0001D450 File Offset: 0x0001B650
		public sbyte[] MoveAdderBoneIndices { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0001D459 File Offset: 0x0001B659
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0001D461 File Offset: 0x0001B661
		public sbyte[] SplashDecalBoneIndices { get; private set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0001D46A File Offset: 0x0001B66A
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0001D472 File Offset: 0x0001B672
		public sbyte[] BloodBurstBoneIndices { get; private set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0001D47B File Offset: 0x0001B67B
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0001D483 File Offset: 0x0001B683
		public sbyte MainHandBoneIndex { get; private set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001D48C File Offset: 0x0001B68C
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0001D494 File Offset: 0x0001B694
		public sbyte OffHandBoneIndex { get; private set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001D49D File Offset: 0x0001B69D
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0001D4A5 File Offset: 0x0001B6A5
		public sbyte MainHandItemBoneIndex { get; private set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001D4AE File Offset: 0x0001B6AE
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0001D4B6 File Offset: 0x0001B6B6
		public sbyte OffHandItemBoneIndex { get; private set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001D4BF File Offset: 0x0001B6BF
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001D4C7 File Offset: 0x0001B6C7
		public sbyte MainHandItemSecondaryBoneIndex { get; private set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public sbyte OffHandItemSecondaryBoneIndex { get; private set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001D4E1 File Offset: 0x0001B6E1
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0001D4E9 File Offset: 0x0001B6E9
		public sbyte OffHandShoulderBoneIndex { get; private set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001D4F2 File Offset: 0x0001B6F2
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001D4FA File Offset: 0x0001B6FA
		public sbyte HandNumBonesForIk { get; private set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001D503 File Offset: 0x0001B703
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0001D50B File Offset: 0x0001B70B
		public sbyte PrimaryFootBoneIndex { get; private set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001D514 File Offset: 0x0001B714
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0001D51C File Offset: 0x0001B71C
		public sbyte SecondaryFootBoneIndex { get; private set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001D525 File Offset: 0x0001B725
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0001D52D File Offset: 0x0001B72D
		public sbyte RightFootIkEndEffectorBoneIndex { get; private set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001D536 File Offset: 0x0001B736
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0001D53E File Offset: 0x0001B73E
		public sbyte LeftFootIkEndEffectorBoneIndex { get; private set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001D547 File Offset: 0x0001B747
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0001D54F File Offset: 0x0001B74F
		public sbyte RightFootIkTipBoneIndex { get; private set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001D558 File Offset: 0x0001B758
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0001D560 File Offset: 0x0001B760
		public sbyte LeftFootIkTipBoneIndex { get; private set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001D569 File Offset: 0x0001B769
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0001D571 File Offset: 0x0001B771
		public sbyte FootNumBonesForIk { get; private set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001D57A File Offset: 0x0001B77A
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0001D582 File Offset: 0x0001B782
		public Vec3 ReinHandleLeftLocalPosition { get; private set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001D58B File Offset: 0x0001B78B
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0001D593 File Offset: 0x0001B793
		public Vec3 ReinHandleRightLocalPosition { get; private set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001D59C File Offset: 0x0001B79C
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0001D5A4 File Offset: 0x0001B7A4
		public string ReinSkeleton { get; private set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001D5AD File Offset: 0x0001B7AD
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0001D5B5 File Offset: 0x0001B7B5
		public string ReinCollisionBody { get; private set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001D5BE File Offset: 0x0001B7BE
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0001D5C6 File Offset: 0x0001B7C6
		public sbyte FrontBoneToDetectGroundSlopeIndex { get; private set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0001D5D7 File Offset: 0x0001B7D7
		public sbyte BackBoneToDetectGroundSlopeIndex { get; private set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0001D5E8 File Offset: 0x0001B7E8
		public sbyte[] BoneIndicesToModifyOnSlopingGround { get; private set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001D5F1 File Offset: 0x0001B7F1
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0001D5F9 File Offset: 0x0001B7F9
		public sbyte BodyRotationReferenceBoneIndex { get; private set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001D602 File Offset: 0x0001B802
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001D60A File Offset: 0x0001B80A
		public sbyte RiderSitBoneIndex { get; private set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001D613 File Offset: 0x0001B813
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0001D61B File Offset: 0x0001B81B
		public sbyte ReinHandleBoneIndex { get; private set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001D624 File Offset: 0x0001B824
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0001D62C File Offset: 0x0001B82C
		public sbyte ReinCollision1BoneIndex { get; private set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001D635 File Offset: 0x0001B835
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0001D63D File Offset: 0x0001B83D
		public sbyte ReinCollision2BoneIndex { get; private set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001D646 File Offset: 0x0001B846
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0001D64E File Offset: 0x0001B84E
		public sbyte ReinHeadBoneIndex { get; private set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001D657 File Offset: 0x0001B857
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0001D65F File Offset: 0x0001B85F
		public sbyte ReinHeadRightAttachmentBoneIndex { get; private set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001D668 File Offset: 0x0001B868
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0001D670 File Offset: 0x0001B870
		public sbyte ReinHeadLeftAttachmentBoneIndex { get; private set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0001D679 File Offset: 0x0001B879
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x0001D681 File Offset: 0x0001B881
		public sbyte ReinRightHandBoneIndex { get; private set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001D68A File Offset: 0x0001B88A
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001D692 File Offset: 0x0001B892
		public sbyte ReinLeftHandBoneIndex { get; private set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001D69C File Offset: 0x0001B89C
		[CachedData]
		public IMonsterMissionData MonsterMissionData
		{
			get
			{
				IMonsterMissionData result;
				if ((result = this._monsterMissionData) == null)
				{
					result = (this._monsterMissionData = Game.Current.MonsterMissionDataCreator.CreateMonsterMissionData(this));
				}
				return result;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public override void Deserialize(MBObjectManager objectManager, XmlNode node)
		{
			base.Deserialize(objectManager, node);
			bool flag = false;
			XmlAttribute xmlAttribute = node.Attributes["base_monster"];
			List<sbyte> list;
			List<sbyte> list2;
			List<sbyte> list3;
			List<sbyte> list4;
			List<sbyte> list5;
			List<sbyte> list6;
			List<sbyte> list7;
			if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Value))
			{
				flag = true;
				this.BaseMonster = xmlAttribute.Value;
				Monster @object = objectManager.GetObject<Monster>(this.BaseMonster);
				if (!string.IsNullOrEmpty(@object.BaseMonster))
				{
					this.BaseMonster = @object.BaseMonster;
				}
				this.BodyCapsuleRadius = @object.BodyCapsuleRadius;
				this.BodyCapsulePoint1 = @object.BodyCapsulePoint1;
				this.BodyCapsulePoint2 = @object.BodyCapsulePoint2;
				this.CrouchedBodyCapsuleRadius = @object.CrouchedBodyCapsuleRadius;
				this.CrouchedBodyCapsulePoint1 = @object.CrouchedBodyCapsulePoint1;
				this.CrouchedBodyCapsulePoint2 = @object.CrouchedBodyCapsulePoint2;
				this.Flags = @object.Flags;
				this.Weight = @object.Weight;
				this.HitPoints = @object.HitPoints;
				this.ActionSetCode = @object.ActionSetCode;
				this.FemaleActionSetCode = @object.FemaleActionSetCode;
				this.MonsterUsage = @object.MonsterUsage;
				this.NumPaces = @object.NumPaces;
				this.WalkingSpeedLimit = @object.WalkingSpeedLimit;
				this.CrouchWalkingSpeedLimit = @object.CrouchWalkingSpeedLimit;
				this.JumpAcceleration = @object.JumpAcceleration;
				this.AbsorbedDamageRatio = @object.AbsorbedDamageRatio;
				this.SoundAndCollisionInfoClassName = @object.SoundAndCollisionInfoClassName;
				this.RiderCameraHeightAdder = @object.RiderCameraHeightAdder;
				this.RiderBodyCapsuleHeightAdder = @object.RiderBodyCapsuleHeightAdder;
				this.RiderBodyCapsuleForwardAdder = @object.RiderBodyCapsuleForwardAdder;
				this.StandingChestHeight = @object.StandingChestHeight;
				this.StandingPelvisHeight = @object.StandingPelvisHeight;
				this.StandingEyeHeight = @object.StandingEyeHeight;
				this.CrouchEyeHeight = @object.CrouchEyeHeight;
				this.MountedEyeHeight = @object.MountedEyeHeight;
				this.RiderEyeHeightAdder = @object.RiderEyeHeightAdder;
				this.EyeOffsetWrtHead = @object.EyeOffsetWrtHead;
				this.FirstPersonCameraOffsetWrtHead = @object.FirstPersonCameraOffsetWrtHead;
				this.ArmLength = @object.ArmLength;
				this.ArmWeight = @object.ArmWeight;
				this.JumpSpeedLimit = @object.JumpSpeedLimit;
				this.RelativeSpeedLimitForCharge = @object.RelativeSpeedLimitForCharge;
				this.FamilyType = @object.FamilyType;
				list = new List<sbyte>(@object.IndicesOfRagdollBonesToCheckForCorpses);
				list2 = new List<sbyte>(@object.RagdollFallSoundBoneIndices);
				this.HeadLookDirectionBoneIndex = @object.HeadLookDirectionBoneIndex;
				this.SpineLowerBoneIndex = @object.SpineLowerBoneIndex;
				this.SpineUpperBoneIndex = @object.SpineUpperBoneIndex;
				this.ThoraxLookDirectionBoneIndex = @object.ThoraxLookDirectionBoneIndex;
				this.NeckRootBoneIndex = @object.NeckRootBoneIndex;
				this.PelvisBoneIndex = @object.PelvisBoneIndex;
				this.RightUpperArmBoneIndex = @object.RightUpperArmBoneIndex;
				this.LeftUpperArmBoneIndex = @object.LeftUpperArmBoneIndex;
				this.FallBlowDamageBoneIndex = @object.FallBlowDamageBoneIndex;
				this.TerrainDecalBone0Index = @object.TerrainDecalBone0Index;
				this.TerrainDecalBone1Index = @object.TerrainDecalBone1Index;
				list3 = new List<sbyte>(@object.RagdollStationaryCheckBoneIndices);
				list4 = new List<sbyte>(@object.MoveAdderBoneIndices);
				list5 = new List<sbyte>(@object.SplashDecalBoneIndices);
				list6 = new List<sbyte>(@object.BloodBurstBoneIndices);
				this.MainHandBoneIndex = @object.MainHandBoneIndex;
				this.OffHandBoneIndex = @object.OffHandBoneIndex;
				this.MainHandItemBoneIndex = @object.MainHandItemBoneIndex;
				this.OffHandItemBoneIndex = @object.OffHandItemBoneIndex;
				this.MainHandItemSecondaryBoneIndex = @object.MainHandItemSecondaryBoneIndex;
				this.OffHandItemSecondaryBoneIndex = @object.OffHandItemSecondaryBoneIndex;
				this.OffHandShoulderBoneIndex = @object.OffHandShoulderBoneIndex;
				this.HandNumBonesForIk = @object.HandNumBonesForIk;
				this.PrimaryFootBoneIndex = @object.PrimaryFootBoneIndex;
				this.SecondaryFootBoneIndex = @object.SecondaryFootBoneIndex;
				this.RightFootIkEndEffectorBoneIndex = @object.RightFootIkEndEffectorBoneIndex;
				this.LeftFootIkEndEffectorBoneIndex = @object.LeftFootIkEndEffectorBoneIndex;
				this.RightFootIkTipBoneIndex = @object.RightFootIkTipBoneIndex;
				this.LeftFootIkTipBoneIndex = @object.LeftFootIkTipBoneIndex;
				this.FootNumBonesForIk = @object.FootNumBonesForIk;
				this.ReinHandleLeftLocalPosition = @object.ReinHandleLeftLocalPosition;
				this.ReinHandleRightLocalPosition = @object.ReinHandleRightLocalPosition;
				this.ReinSkeleton = @object.ReinSkeleton;
				this.ReinCollisionBody = @object.ReinCollisionBody;
				this.FrontBoneToDetectGroundSlopeIndex = @object.FrontBoneToDetectGroundSlopeIndex;
				this.BackBoneToDetectGroundSlopeIndex = @object.BackBoneToDetectGroundSlopeIndex;
				list7 = new List<sbyte>(@object.BoneIndicesToModifyOnSlopingGround);
				this.BodyRotationReferenceBoneIndex = @object.BodyRotationReferenceBoneIndex;
				this.RiderSitBoneIndex = @object.RiderSitBoneIndex;
				this.ReinHandleBoneIndex = @object.ReinHandleBoneIndex;
				this.ReinCollision1BoneIndex = @object.ReinCollision1BoneIndex;
				this.ReinCollision2BoneIndex = @object.ReinCollision2BoneIndex;
				this.ReinHeadBoneIndex = @object.ReinHeadBoneIndex;
				this.ReinHeadRightAttachmentBoneIndex = @object.ReinHeadRightAttachmentBoneIndex;
				this.ReinHeadLeftAttachmentBoneIndex = @object.ReinHeadLeftAttachmentBoneIndex;
				this.ReinRightHandBoneIndex = @object.ReinRightHandBoneIndex;
				this.ReinLeftHandBoneIndex = @object.ReinLeftHandBoneIndex;
			}
			else
			{
				list = new List<sbyte>(12);
				list2 = new List<sbyte>(4);
				list3 = new List<sbyte>(8);
				list4 = new List<sbyte>(8);
				list5 = new List<sbyte>(8);
				list6 = new List<sbyte>(8);
				list7 = new List<sbyte>(8);
			}
			XmlAttribute xmlAttribute2 = node.Attributes["action_set"];
			if (xmlAttribute2 != null && !string.IsNullOrEmpty(xmlAttribute2.Value))
			{
				this.ActionSetCode = xmlAttribute2.Value;
			}
			XmlAttribute xmlAttribute3 = node.Attributes["female_action_set"];
			if (xmlAttribute3 != null && !string.IsNullOrEmpty(xmlAttribute3.Value))
			{
				this.FemaleActionSetCode = xmlAttribute3.Value;
			}
			XmlAttribute xmlAttribute4 = node.Attributes["monster_usage"];
			if (xmlAttribute4 != null && !string.IsNullOrEmpty(xmlAttribute4.Value))
			{
				this.MonsterUsage = xmlAttribute4.Value;
			}
			else if (!flag)
			{
				this.MonsterUsage = "";
			}
			if (!flag)
			{
				this.Weight = 1;
			}
			XmlAttribute xmlAttribute5 = node.Attributes["weight"];
			int weight;
			if (xmlAttribute5 != null && !string.IsNullOrEmpty(xmlAttribute5.Value) && int.TryParse(xmlAttribute5.Value, out weight))
			{
				this.Weight = weight;
			}
			if (!flag)
			{
				this.HitPoints = 1;
			}
			XmlAttribute xmlAttribute6 = node.Attributes["hit_points"];
			int hitPoints;
			if (xmlAttribute6 != null && !string.IsNullOrEmpty(xmlAttribute6.Value) && int.TryParse(xmlAttribute6.Value, out hitPoints))
			{
				this.HitPoints = hitPoints;
			}
			XmlAttribute xmlAttribute7 = node.Attributes["num_paces"];
			int numPaces;
			if (xmlAttribute7 != null && !string.IsNullOrEmpty(xmlAttribute7.Value) && int.TryParse(xmlAttribute7.Value, out numPaces))
			{
				this.NumPaces = numPaces;
			}
			XmlAttribute xmlAttribute8 = node.Attributes["walking_speed_limit"];
			float walkingSpeedLimit;
			if (xmlAttribute8 != null && !string.IsNullOrEmpty(xmlAttribute8.Value) && float.TryParse(xmlAttribute8.Value, out walkingSpeedLimit))
			{
				this.WalkingSpeedLimit = walkingSpeedLimit;
			}
			XmlAttribute xmlAttribute9 = node.Attributes["crouch_walking_speed_limit"];
			if (xmlAttribute9 != null && !string.IsNullOrEmpty(xmlAttribute9.Value))
			{
				float crouchWalkingSpeedLimit;
				if (float.TryParse(xmlAttribute9.Value, out crouchWalkingSpeedLimit))
				{
					this.CrouchWalkingSpeedLimit = crouchWalkingSpeedLimit;
				}
			}
			else if (!flag)
			{
				this.CrouchWalkingSpeedLimit = this.WalkingSpeedLimit;
			}
			XmlAttribute xmlAttribute10 = node.Attributes["jump_acceleration"];
			float jumpAcceleration;
			if (xmlAttribute10 != null && !string.IsNullOrEmpty(xmlAttribute10.Value) && float.TryParse(xmlAttribute10.Value, out jumpAcceleration))
			{
				this.JumpAcceleration = jumpAcceleration;
			}
			XmlAttribute xmlAttribute11 = node.Attributes["absorbed_damage_ratio"];
			if (xmlAttribute11 != null && !string.IsNullOrEmpty(xmlAttribute11.Value))
			{
				float num;
				if (float.TryParse(xmlAttribute11.Value, out num))
				{
					if (num < 0f)
					{
						num = 0f;
					}
					this.AbsorbedDamageRatio = num;
				}
			}
			else if (!flag)
			{
				this.AbsorbedDamageRatio = 1f;
			}
			XmlAttribute xmlAttribute12 = node.Attributes["sound_and_collision_info_class"];
			if (xmlAttribute12 != null && !string.IsNullOrEmpty(xmlAttribute12.Value))
			{
				this.SoundAndCollisionInfoClassName = xmlAttribute12.Value;
			}
			XmlAttribute xmlAttribute13 = node.Attributes["rider_camera_height_adder"];
			float riderCameraHeightAdder;
			if (xmlAttribute13 != null && !string.IsNullOrEmpty(xmlAttribute13.Value) && float.TryParse(xmlAttribute13.Value, out riderCameraHeightAdder))
			{
				this.RiderCameraHeightAdder = riderCameraHeightAdder;
			}
			XmlAttribute xmlAttribute14 = node.Attributes["rider_body_capsule_height_adder"];
			float riderBodyCapsuleHeightAdder;
			if (xmlAttribute14 != null && !string.IsNullOrEmpty(xmlAttribute14.Value) && float.TryParse(xmlAttribute14.Value, out riderBodyCapsuleHeightAdder))
			{
				this.RiderBodyCapsuleHeightAdder = riderBodyCapsuleHeightAdder;
			}
			XmlAttribute xmlAttribute15 = node.Attributes["rider_body_capsule_forward_adder"];
			float riderBodyCapsuleForwardAdder;
			if (xmlAttribute15 != null && !string.IsNullOrEmpty(xmlAttribute15.Value) && float.TryParse(xmlAttribute15.Value, out riderBodyCapsuleForwardAdder))
			{
				this.RiderBodyCapsuleForwardAdder = riderBodyCapsuleForwardAdder;
			}
			XmlAttribute xmlAttribute16 = node.Attributes["preliminary_collision_capsule_radius_multiplier"];
			if (!flag && xmlAttribute16 != null && !string.IsNullOrEmpty(xmlAttribute16.Value))
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\Monster.cs", "Deserialize", 429);
			}
			XmlAttribute xmlAttribute17 = node.Attributes["rider_preliminary_collision_capsule_height_multiplier"];
			if (!flag && xmlAttribute17 != null && !string.IsNullOrEmpty(xmlAttribute17.Value))
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\Monster.cs", "Deserialize", 438);
			}
			XmlAttribute xmlAttribute18 = node.Attributes["rider_preliminary_collision_capsule_height_adder"];
			if (!flag && xmlAttribute18 != null && !string.IsNullOrEmpty(xmlAttribute18.Value))
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\Monster.cs", "Deserialize", 447);
			}
			XmlAttribute xmlAttribute19 = node.Attributes["standing_chest_height"];
			float standingChestHeight;
			if (xmlAttribute19 != null && !string.IsNullOrEmpty(xmlAttribute19.Value) && float.TryParse(xmlAttribute19.Value, out standingChestHeight))
			{
				this.StandingChestHeight = standingChestHeight;
			}
			XmlAttribute xmlAttribute20 = node.Attributes["standing_pelvis_height"];
			float standingPelvisHeight;
			if (xmlAttribute20 != null && !string.IsNullOrEmpty(xmlAttribute20.Value) && float.TryParse(xmlAttribute20.Value, out standingPelvisHeight))
			{
				this.StandingPelvisHeight = standingPelvisHeight;
			}
			XmlAttribute xmlAttribute21 = node.Attributes["standing_eye_height"];
			float standingEyeHeight;
			if (xmlAttribute21 != null && !string.IsNullOrEmpty(xmlAttribute21.Value) && float.TryParse(xmlAttribute21.Value, out standingEyeHeight))
			{
				this.StandingEyeHeight = standingEyeHeight;
			}
			XmlAttribute xmlAttribute22 = node.Attributes["crouch_eye_height"];
			float crouchEyeHeight;
			if (xmlAttribute22 != null && !string.IsNullOrEmpty(xmlAttribute22.Value) && float.TryParse(xmlAttribute22.Value, out crouchEyeHeight))
			{
				this.CrouchEyeHeight = crouchEyeHeight;
			}
			XmlAttribute xmlAttribute23 = node.Attributes["mounted_eye_height"];
			float mountedEyeHeight;
			if (xmlAttribute23 != null && !string.IsNullOrEmpty(xmlAttribute23.Value) && float.TryParse(xmlAttribute23.Value, out mountedEyeHeight))
			{
				this.MountedEyeHeight = mountedEyeHeight;
			}
			XmlAttribute xmlAttribute24 = node.Attributes["rider_eye_height_adder"];
			float riderEyeHeightAdder;
			if (xmlAttribute24 != null && !string.IsNullOrEmpty(xmlAttribute24.Value) && float.TryParse(xmlAttribute24.Value, out riderEyeHeightAdder))
			{
				this.RiderEyeHeightAdder = riderEyeHeightAdder;
			}
			if (!flag)
			{
				this.EyeOffsetWrtHead = new Vec3(0.01f, 0.01f, 0.01f, -1f);
			}
			XmlAttribute xmlAttribute25 = node.Attributes["eye_offset_wrt_head"];
			Vec3 eyeOffsetWrtHead;
			if (xmlAttribute25 != null && !string.IsNullOrEmpty(xmlAttribute25.Value) && Monster.ReadVec3(xmlAttribute25.Value, out eyeOffsetWrtHead))
			{
				this.EyeOffsetWrtHead = eyeOffsetWrtHead;
			}
			if (!flag)
			{
				this.FirstPersonCameraOffsetWrtHead = new Vec3(0.01f, 0.01f, 0.01f, -1f);
			}
			XmlAttribute xmlAttribute26 = node.Attributes["first_person_camera_offset_wrt_head"];
			Vec3 firstPersonCameraOffsetWrtHead;
			if (xmlAttribute26 != null && !string.IsNullOrEmpty(xmlAttribute26.Value) && Monster.ReadVec3(xmlAttribute26.Value, out firstPersonCameraOffsetWrtHead))
			{
				this.FirstPersonCameraOffsetWrtHead = firstPersonCameraOffsetWrtHead;
			}
			XmlAttribute xmlAttribute27 = node.Attributes["arm_length"];
			float armLength;
			if (xmlAttribute27 != null && !string.IsNullOrEmpty(xmlAttribute27.Value) && float.TryParse(xmlAttribute27.Value, out armLength))
			{
				this.ArmLength = armLength;
			}
			XmlAttribute xmlAttribute28 = node.Attributes["arm_weight"];
			float armWeight;
			if (xmlAttribute28 != null && !string.IsNullOrEmpty(xmlAttribute28.Value) && float.TryParse(xmlAttribute28.Value, out armWeight))
			{
				this.ArmWeight = armWeight;
			}
			XmlAttribute xmlAttribute29 = node.Attributes["jump_speed_limit"];
			float jumpSpeedLimit;
			if (xmlAttribute29 != null && !string.IsNullOrEmpty(xmlAttribute29.Value) && float.TryParse(xmlAttribute29.Value, out jumpSpeedLimit))
			{
				this.JumpSpeedLimit = jumpSpeedLimit;
			}
			if (!flag)
			{
				this.RelativeSpeedLimitForCharge = float.MaxValue;
			}
			XmlAttribute xmlAttribute30 = node.Attributes["relative_speed_limit_for_charge"];
			float relativeSpeedLimitForCharge;
			if (xmlAttribute30 != null && !string.IsNullOrEmpty(xmlAttribute30.Value) && float.TryParse(xmlAttribute30.Value, out relativeSpeedLimitForCharge))
			{
				this.RelativeSpeedLimitForCharge = relativeSpeedLimitForCharge;
			}
			XmlAttribute xmlAttribute31 = node.Attributes["family_type"];
			int familyType;
			if (xmlAttribute31 != null && !string.IsNullOrEmpty(xmlAttribute31.Value) && int.TryParse(xmlAttribute31.Value, out familyType))
			{
				this.FamilyType = familyType;
			}
			sbyte b = -1;
			this.DeserializeBoneIndexArray(list, node, flag, "ragdoll_bone_to_check_for_corpses_", b, false);
			this.DeserializeBoneIndexArray(list2, node, flag, "ragdoll_fall_sound_bone_", b, false);
			this.HeadLookDirectionBoneIndex = this.DeserializeBoneIndex(node, "head_look_direction_bone", flag ? this.HeadLookDirectionBoneIndex : b, b, true);
			this.SpineLowerBoneIndex = this.DeserializeBoneIndex(node, "spine_lower_bone", flag ? this.SpineLowerBoneIndex : b, b, false);
			this.SpineUpperBoneIndex = this.DeserializeBoneIndex(node, "spine_upper_bone", flag ? this.SpineUpperBoneIndex : b, b, false);
			this.ThoraxLookDirectionBoneIndex = this.DeserializeBoneIndex(node, "thorax_look_direction_bone", flag ? this.ThoraxLookDirectionBoneIndex : b, b, true);
			this.NeckRootBoneIndex = this.DeserializeBoneIndex(node, "neck_root_bone", flag ? this.NeckRootBoneIndex : b, b, true);
			this.PelvisBoneIndex = this.DeserializeBoneIndex(node, "pelvis_bone", flag ? this.PelvisBoneIndex : b, b, false);
			this.RightUpperArmBoneIndex = this.DeserializeBoneIndex(node, "right_upper_arm_bone", flag ? this.RightUpperArmBoneIndex : b, b, false);
			this.LeftUpperArmBoneIndex = this.DeserializeBoneIndex(node, "left_upper_arm_bone", flag ? this.LeftUpperArmBoneIndex : b, b, false);
			this.FallBlowDamageBoneIndex = this.DeserializeBoneIndex(node, "fall_blow_damage_bone", flag ? this.FallBlowDamageBoneIndex : b, b, false);
			this.TerrainDecalBone0Index = this.DeserializeBoneIndex(node, "terrain_decal_bone_0", flag ? this.TerrainDecalBone0Index : b, b, false);
			this.TerrainDecalBone1Index = this.DeserializeBoneIndex(node, "terrain_decal_bone_1", flag ? this.TerrainDecalBone1Index : b, b, false);
			this.DeserializeBoneIndexArray(list3, node, flag, "ragdoll_stationary_check_bone_", b, false);
			this.DeserializeBoneIndexArray(list4, node, flag, "move_adder_bone_", b, false);
			this.DeserializeBoneIndexArray(list5, node, flag, "splash_decal_bone_", b, false);
			this.DeserializeBoneIndexArray(list6, node, flag, "blood_burst_bone_", b, false);
			this.MainHandBoneIndex = this.DeserializeBoneIndex(node, "main_hand_bone", flag ? this.MainHandBoneIndex : b, b, true);
			this.OffHandBoneIndex = this.DeserializeBoneIndex(node, "off_hand_bone", flag ? this.OffHandBoneIndex : b, b, true);
			this.MainHandItemBoneIndex = this.DeserializeBoneIndex(node, "main_hand_item_bone", flag ? this.MainHandItemBoneIndex : b, b, true);
			this.OffHandItemBoneIndex = this.DeserializeBoneIndex(node, "off_hand_item_bone", flag ? this.OffHandItemBoneIndex : b, b, true);
			this.MainHandItemSecondaryBoneIndex = this.DeserializeBoneIndex(node, "main_hand_item_secondary_bone", flag ? this.MainHandItemSecondaryBoneIndex : b, b, false);
			this.OffHandItemSecondaryBoneIndex = this.DeserializeBoneIndex(node, "off_hand_item_secondary_bone", flag ? this.OffHandItemSecondaryBoneIndex : b, b, false);
			this.OffHandShoulderBoneIndex = this.DeserializeBoneIndex(node, "off_hand_shoulder_bone", flag ? this.OffHandShoulderBoneIndex : b, b, false);
			XmlAttribute xmlAttribute32 = node.Attributes["hand_num_bones_for_ik"];
			this.HandNumBonesForIk = ((xmlAttribute32 != null) ? sbyte.Parse(xmlAttribute32.Value) : (flag ? this.HandNumBonesForIk : 0));
			this.PrimaryFootBoneIndex = this.DeserializeBoneIndex(node, "primary_foot_bone", flag ? this.PrimaryFootBoneIndex : b, b, false);
			this.SecondaryFootBoneIndex = this.DeserializeBoneIndex(node, "secondary_foot_bone", flag ? this.SecondaryFootBoneIndex : b, b, false);
			this.RightFootIkEndEffectorBoneIndex = this.DeserializeBoneIndex(node, "right_foot_ik_end_effector_bone", flag ? this.RightFootIkEndEffectorBoneIndex : b, b, true);
			this.LeftFootIkEndEffectorBoneIndex = this.DeserializeBoneIndex(node, "left_foot_ik_end_effector_bone", flag ? this.LeftFootIkEndEffectorBoneIndex : b, b, true);
			this.RightFootIkTipBoneIndex = this.DeserializeBoneIndex(node, "right_foot_ik_tip_bone", flag ? this.RightFootIkTipBoneIndex : b, b, true);
			this.LeftFootIkTipBoneIndex = this.DeserializeBoneIndex(node, "left_foot_ik_tip_bone", flag ? this.LeftFootIkTipBoneIndex : b, b, true);
			XmlAttribute xmlAttribute33 = node.Attributes["foot_num_bones_for_ik"];
			this.FootNumBonesForIk = ((xmlAttribute33 != null) ? sbyte.Parse(xmlAttribute33.Value) : (flag ? this.FootNumBonesForIk : 0));
			XmlNode xmlNode = node.Attributes["rein_handle_left_local_pos"];
			Vec3 reinHandleLeftLocalPosition;
			if (xmlNode != null && Monster.ReadVec3(xmlNode.Value, out reinHandleLeftLocalPosition))
			{
				this.ReinHandleLeftLocalPosition = reinHandleLeftLocalPosition;
			}
			XmlNode xmlNode2 = node.Attributes["rein_handle_right_local_pos"];
			Vec3 reinHandleRightLocalPosition;
			if (xmlNode2 != null && Monster.ReadVec3(xmlNode2.Value, out reinHandleRightLocalPosition))
			{
				this.ReinHandleRightLocalPosition = reinHandleRightLocalPosition;
			}
			XmlAttribute xmlAttribute34 = node.Attributes["rein_skeleton"];
			this.ReinSkeleton = ((xmlAttribute34 != null) ? xmlAttribute34.Value : this.ReinSkeleton);
			XmlAttribute xmlAttribute35 = node.Attributes["rein_collision_body"];
			this.ReinCollisionBody = ((xmlAttribute35 != null) ? xmlAttribute35.Value : this.ReinCollisionBody);
			this.DeserializeBoneIndexArray(list7, node, flag, "bones_to_modify_on_sloping_ground_", b, true);
			XmlAttribute xmlAttribute36 = node.Attributes["front_bone_to_detect_ground_slope_index"];
			this.FrontBoneToDetectGroundSlopeIndex = ((xmlAttribute36 != null) ? sbyte.Parse(xmlAttribute36.Value) : (flag ? this.FrontBoneToDetectGroundSlopeIndex : -1));
			XmlAttribute xmlAttribute37 = node.Attributes["back_bone_to_detect_ground_slope_index"];
			this.BackBoneToDetectGroundSlopeIndex = ((xmlAttribute37 != null) ? sbyte.Parse(xmlAttribute37.Value) : (flag ? this.BackBoneToDetectGroundSlopeIndex : -1));
			this.BodyRotationReferenceBoneIndex = this.DeserializeBoneIndex(node, "body_rotation_reference_bone", flag ? this.BodyRotationReferenceBoneIndex : b, b, true);
			this.RiderSitBoneIndex = this.DeserializeBoneIndex(node, "rider_sit_bone", flag ? this.RiderSitBoneIndex : b, b, false);
			this.ReinHandleBoneIndex = this.DeserializeBoneIndex(node, "rein_handle_bone", flag ? this.ReinHandleBoneIndex : b, b, false);
			this.ReinCollision1BoneIndex = this.DeserializeBoneIndex(node, "rein_collision_1_bone", flag ? this.ReinCollision1BoneIndex : b, b, false);
			this.ReinCollision2BoneIndex = this.DeserializeBoneIndex(node, "rein_collision_2_bone", flag ? this.ReinCollision2BoneIndex : b, b, false);
			this.ReinHeadBoneIndex = this.DeserializeBoneIndex(node, "rein_head_bone", flag ? this.ReinHeadBoneIndex : b, b, false);
			this.ReinHeadRightAttachmentBoneIndex = this.DeserializeBoneIndex(node, "rein_head_right_attachment_bone", flag ? this.ReinHeadRightAttachmentBoneIndex : b, b, false);
			this.ReinHeadLeftAttachmentBoneIndex = this.DeserializeBoneIndex(node, "rein_head_left_attachment_bone", flag ? this.ReinHeadLeftAttachmentBoneIndex : b, b, false);
			this.ReinRightHandBoneIndex = this.DeserializeBoneIndex(node, "rein_right_hand_bone", flag ? this.ReinRightHandBoneIndex : b, b, false);
			this.ReinLeftHandBoneIndex = this.DeserializeBoneIndex(node, "rein_left_hand_bone", flag ? this.ReinLeftHandBoneIndex : b, b, false);
			this.IndicesOfRagdollBonesToCheckForCorpses = list.ToArray();
			this.RagdollFallSoundBoneIndices = list2.ToArray();
			this.RagdollStationaryCheckBoneIndices = list3.ToArray();
			this.MoveAdderBoneIndices = list4.ToArray();
			this.SplashDecalBoneIndices = list5.ToArray();
			this.BloodBurstBoneIndices = list6.ToArray();
			this.BoneIndicesToModifyOnSlopingGround = list7.ToArray();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode3 = (XmlNode)obj;
				if (xmlNode3.Name == "Flags")
				{
					this.Flags = AgentFlag.None;
					using (IEnumerator enumerator2 = Enum.GetValues(typeof(AgentFlag)).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							AgentFlag agentFlag = (AgentFlag)obj2;
							XmlAttribute xmlAttribute38 = xmlNode3.Attributes[agentFlag.ToString()];
							if (xmlAttribute38 != null && !xmlAttribute38.Value.Equals("false", StringComparison.InvariantCultureIgnoreCase))
							{
								this.Flags |= agentFlag;
							}
						}
						continue;
					}
				}
				if (xmlNode3.Name == "Capsules")
				{
					foreach (object obj3 in xmlNode3.ChildNodes)
					{
						XmlNode xmlNode4 = (XmlNode)obj3;
						if (xmlNode4.Attributes != null && (xmlNode4.Name == "preliminary_collision_capsule" || xmlNode4.Name == "body_capsule" || xmlNode4.Name == "crouched_body_capsule"))
						{
							bool flag2 = true;
							Vec3 vec = new Vec3(0f, 0f, 0.01f, -1f);
							Vec3 vec2 = Vec3.Zero;
							float num2 = 0.01f;
							if (xmlNode4.Attributes["pos1"] != null)
							{
								Vec3 vec3;
								flag2 = (Monster.ReadVec3(xmlNode4.Attributes["pos1"].Value, out vec3) && flag2);
								if (flag2)
								{
									vec = vec3;
								}
							}
							if (xmlNode4.Attributes["pos2"] != null)
							{
								Vec3 vec4;
								flag2 = (Monster.ReadVec3(xmlNode4.Attributes["pos2"].Value, out vec4) && flag2);
								if (flag2)
								{
									vec2 = vec4;
								}
							}
							if (xmlNode4.Attributes["radius"] != null)
							{
								string text = xmlNode4.Attributes["radius"].Value;
								text = text.Trim();
								flag2 = (flag2 && float.TryParse(text, out num2));
							}
							if (flag2)
							{
								if (xmlNode4.Name.StartsWith("p"))
								{
									Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.Core\\Monster.cs", "Deserialize", 727);
								}
								else if (xmlNode4.Name.StartsWith("c"))
								{
									this.CrouchedBodyCapsuleRadius = num2;
									this.CrouchedBodyCapsulePoint1 = vec;
									this.CrouchedBodyCapsulePoint2 = vec2;
								}
								else
								{
									this.BodyCapsuleRadius = num2;
									this.BodyCapsulePoint1 = vec;
									this.BodyCapsulePoint2 = vec2;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		private sbyte DeserializeBoneIndex(XmlNode node, string attributeName, sbyte baseValue, sbyte invalidBoneIndex, bool validateHasParentBone)
		{
			XmlAttribute xmlAttribute = node.Attributes[attributeName];
			sbyte b = (Monster.GetBoneIndexWithId != null && xmlAttribute != null) ? Monster.GetBoneIndexWithId(this.ActionSetCode, xmlAttribute.Value) : baseValue;
			if (validateHasParentBone && b != invalidBoneIndex)
			{
				Func<string, sbyte, bool> getBoneHasParentBone = Monster.GetBoneHasParentBone;
			}
			return b;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		private void DeserializeBoneIndexArray(List<sbyte> boneIndices, XmlNode node, bool hasBaseMonster, string attributeNamePrefix, sbyte invalidBoneIndex, bool validateHasParentBone)
		{
			int num = 0;
			for (;;)
			{
				bool flag = hasBaseMonster && num < boneIndices.Count;
				sbyte b = this.DeserializeBoneIndex(node, attributeNamePrefix + num, flag ? boneIndices[num] : invalidBoneIndex, invalidBoneIndex, validateHasParentBone);
				if (b == invalidBoneIndex)
				{
					break;
				}
				if (flag)
				{
					boneIndices[num] = b;
				}
				else
				{
					boneIndices.Add(b);
				}
				num++;
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001EE58 File Offset: 0x0001D058
		private static bool ReadVec3(string str, out Vec3 v)
		{
			str = str.Trim();
			string[] array = str.Split(",".ToCharArray());
			v = new Vec3(0f, 0f, 0f, -1f);
			return float.TryParse(array[0], out v.x) && float.TryParse(array[1], out v.y) && float.TryParse(array[2], out v.z);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001EED0 File Offset: 0x0001D0D0
		public sbyte GetBoneToAttachForItemFlags(ItemFlags itemFlags)
		{
			ItemFlags itemFlags2 = itemFlags & ItemFlags.AttachmentMask;
			if (itemFlags2 <= (ItemFlags)0U)
			{
				return this.MainHandItemBoneIndex;
			}
			if (itemFlags2 == ItemFlags.ForceAttachOffHandPrimaryItemBone)
			{
				return this.OffHandItemBoneIndex;
			}
			if (itemFlags2 != ItemFlags.ForceAttachOffHandSecondaryItemBone)
			{
				return this.MainHandItemBoneIndex;
			}
			return this.OffHandItemSecondaryBoneIndex;
		}

		// Token: 0x04000506 RID: 1286
		public static Func<string, string, sbyte> GetBoneIndexWithId;

		// Token: 0x04000507 RID: 1287
		public static Func<string, sbyte, bool> GetBoneHasParentBone;

		// Token: 0x0400055C RID: 1372
		[CachedData]
		private IMonsterMissionData _monsterMissionData;
	}
}
