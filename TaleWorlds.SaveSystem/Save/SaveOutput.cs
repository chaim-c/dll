using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x0200002F RID: 47
	public class SaveOutput
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000082DC File Offset: 0x000064DC
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000082E4 File Offset: 0x000064E4
		public GameData Data { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000082ED File Offset: 0x000064ED
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000082F5 File Offset: 0x000064F5
		public SaveResult Result { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000082FE File Offset: 0x000064FE
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00008306 File Offset: 0x00006506
		public SaveError[] Errors { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000830F File Offset: 0x0000650F
		public bool Successful
		{
			get
			{
				return this.Result == SaveResult.Success;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000831A File Offset: 0x0000651A
		public bool IsContinuing
		{
			get
			{
				Task<SaveResultWithMessage> continuingTask = this._continuingTask;
				return continuingTask != null && !continuingTask.IsCompleted;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008330 File Offset: 0x00006530
		private SaveOutput()
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008338 File Offset: 0x00006538
		internal static SaveOutput CreateSuccessful(GameData data)
		{
			return new SaveOutput
			{
				Data = data,
				Result = SaveResult.Success
			};
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000834D File Offset: 0x0000654D
		internal static SaveOutput CreateFailed(IEnumerable<SaveError> errors, SaveResult result)
		{
			return new SaveOutput
			{
				Result = result,
				Errors = errors.ToArray<SaveError>()
			};
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008368 File Offset: 0x00006568
		internal static SaveOutput CreateContinuing(Task<SaveResultWithMessage> continuingTask)
		{
			SaveOutput saveOutput = new SaveOutput();
			saveOutput._continuingTask = continuingTask;
			saveOutput._continuingTask.ContinueWith(delegate(Task<SaveResultWithMessage> t)
			{
				saveOutput.Result = t.Result.SaveResult;
			});
			return saveOutput;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000083B8 File Offset: 0x000065B8
		public void PrintStatus()
		{
			Task<SaveResultWithMessage> continuingTask = this._continuingTask;
			if (continuingTask != null && continuingTask.IsCompleted)
			{
				this.Result = this._continuingTask.Result.SaveResult;
				this.Errors = new SaveError[0];
			}
			if (this.Result == SaveResult.Success)
			{
				Debug.Print("------Successfully saved------", 0, Debug.DebugColor.White, 17592186044416UL);
				return;
			}
			Debug.Print("Couldn't save because of errors listed below.", 0, Debug.DebugColor.White, 17592186044416UL);
			for (int i = 0; i < this.Errors.Length; i++)
			{
				SaveError saveError = this.Errors[i];
				Debug.Print(string.Concat(new object[]
				{
					"[",
					i,
					"]",
					saveError.Message
				}), 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.FailedAssert(string.Concat(new object[]
				{
					"SAVE FAILED: [",
					i,
					"]",
					saveError.Message,
					"\n"
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\Save\\SaveOutput.cs", "PrintStatus", 74);
			}
			Debug.Print("--------------------", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x04000080 RID: 128
		private Task<SaveResultWithMessage> _continuingTask;
	}
}
