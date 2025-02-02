using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000018 RID: 24
	public enum StackBehaviour
	{
		// Token: 0x04000155 RID: 341
		Pop0,
		// Token: 0x04000156 RID: 342
		Pop1,
		// Token: 0x04000157 RID: 343
		Pop1_pop1,
		// Token: 0x04000158 RID: 344
		Popi,
		// Token: 0x04000159 RID: 345
		Popi_pop1,
		// Token: 0x0400015A RID: 346
		Popi_popi,
		// Token: 0x0400015B RID: 347
		Popi_popi8,
		// Token: 0x0400015C RID: 348
		Popi_popi_popi,
		// Token: 0x0400015D RID: 349
		Popi_popr4,
		// Token: 0x0400015E RID: 350
		Popi_popr8,
		// Token: 0x0400015F RID: 351
		Popref,
		// Token: 0x04000160 RID: 352
		Popref_pop1,
		// Token: 0x04000161 RID: 353
		Popref_popi,
		// Token: 0x04000162 RID: 354
		Popref_popi_popi,
		// Token: 0x04000163 RID: 355
		Popref_popi_popi8,
		// Token: 0x04000164 RID: 356
		Popref_popi_popr4,
		// Token: 0x04000165 RID: 357
		Popref_popi_popr8,
		// Token: 0x04000166 RID: 358
		Popref_popi_popref,
		// Token: 0x04000167 RID: 359
		PopAll,
		// Token: 0x04000168 RID: 360
		Push0,
		// Token: 0x04000169 RID: 361
		Push1,
		// Token: 0x0400016A RID: 362
		Push1_push1,
		// Token: 0x0400016B RID: 363
		Pushi,
		// Token: 0x0400016C RID: 364
		Pushi8,
		// Token: 0x0400016D RID: 365
		Pushr4,
		// Token: 0x0400016E RID: 366
		Pushr8,
		// Token: 0x0400016F RID: 367
		Pushref,
		// Token: 0x04000170 RID: 368
		Varpop,
		// Token: 0x04000171 RID: 369
		Varpush
	}
}
