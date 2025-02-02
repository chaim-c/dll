using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaleWorlds.Library
{
	// Token: 0x0200008C RID: 140
	public abstract class TestCommonBase
	{
		// Token: 0x060004D4 RID: 1236
		public abstract void Tick();

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0001020F File Offset: 0x0000E40F
		public static TestCommonBase BaseInstance
		{
			get
			{
				return TestCommonBase._baseInstance;
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00010216 File Offset: 0x0000E416
		public void StartTimeoutTimer()
		{
			this.timeoutTimerStart = DateTime.Now;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00010223 File Offset: 0x0000E423
		public void ToggleTimeoutTimer()
		{
			this.timeoutTimerEnabled = !this.timeoutTimerEnabled;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00010234 File Offset: 0x0000E434
		public bool CheckTimeoutTimer()
		{
			return this.timeoutTimerEnabled && DateTime.Now.Subtract(this.timeoutTimerStart).TotalSeconds > (double)this.commonWaitTimeoutLimits;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00010272 File Offset: 0x0000E472
		protected TestCommonBase()
		{
			TestCommonBase._baseInstance = this;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public virtual string GetGameStatus()
		{
			return "";
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public void WaitFor(double seconds)
		{
			if (!this.isParallelThread)
			{
				DateTime now = DateTime.Now;
				while ((DateTime.Now - now).TotalSeconds < seconds)
				{
					Monitor.Pulse(this.TestLock);
					Monitor.Wait(this.TestLock);
				}
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000102FC File Offset: 0x0000E4FC
		public virtual async Task WaitUntil(Func<bool> func)
		{
			while (!func())
			{
				await this.WaitForAsync(0.1);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00010349 File Offset: 0x0000E549
		public Task WaitForAsync(double seconds, Random random)
		{
			return Task.Delay((int)(seconds * 1000.0 * random.NextDouble()));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00010363 File Offset: 0x0000E563
		public Task WaitForAsync(double seconds)
		{
			return Task.Delay((int)(seconds * 1000.0));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00010376 File Offset: 0x0000E576
		public static string GetAttachmentsFolderPath()
		{
			return "..\\..\\..\\Tools\\TestAutomation\\Attachments\\";
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001037D File Offset: 0x0000E57D
		public virtual void OnFinalize()
		{
			TestCommonBase._baseInstance = null;
		}

		// Token: 0x0400016C RID: 364
		public int TestRandomSeed;

		// Token: 0x0400016D RID: 365
		public bool IsTestEnabled;

		// Token: 0x0400016E RID: 366
		public bool isParallelThread;

		// Token: 0x0400016F RID: 367
		public string SceneNameToOpenOnStartup;

		// Token: 0x04000170 RID: 368
		public object TestLock = new object();

		// Token: 0x04000171 RID: 369
		private static TestCommonBase _baseInstance;

		// Token: 0x04000172 RID: 370
		private DateTime timeoutTimerStart = DateTime.Now;

		// Token: 0x04000173 RID: 371
		private bool timeoutTimerEnabled = true;

		// Token: 0x04000174 RID: 372
		private int commonWaitTimeoutLimits = 420;
	}
}
