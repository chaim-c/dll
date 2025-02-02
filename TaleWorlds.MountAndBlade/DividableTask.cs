using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000137 RID: 311
	public class DividableTask
	{
		// Token: 0x06000E6F RID: 3695 RVA: 0x000275C6 File Offset: 0x000257C6
		public DividableTask(DividableTask continueToTask = null)
		{
			this._continueToTask = continueToTask;
			this.ResetTaskStatus();
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000275DB File Offset: 0x000257DB
		public void ResetTaskStatus()
		{
			this._isMainTaskFinished = false;
			this._isTaskCompletelyFinished = false;
			this._lastActionCalled = false;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000275F2 File Offset: 0x000257F2
		public void SetTaskFinished(bool callLastAction = false)
		{
			if (callLastAction)
			{
				Action lastAction = this._lastAction;
				if (lastAction != null)
				{
					lastAction();
				}
				this._lastActionCalled = true;
			}
			this._isTaskCompletelyFinished = true;
			this._isMainTaskFinished = true;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00027620 File Offset: 0x00025820
		public bool Update()
		{
			if (!this._isTaskCompletelyFinished)
			{
				if (!this._isMainTaskFinished && this.UpdateExtra())
				{
					this._isMainTaskFinished = true;
				}
				if (this._isMainTaskFinished)
				{
					DividableTask continueToTask = this._continueToTask;
					this._isTaskCompletelyFinished = (continueToTask == null || continueToTask.Update());
				}
			}
			if (this._isTaskCompletelyFinished && !this._lastActionCalled)
			{
				Action lastAction = this._lastAction;
				if (lastAction != null)
				{
					lastAction();
				}
				this._lastActionCalled = true;
			}
			return this._isTaskCompletelyFinished;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0002769A File Offset: 0x0002589A
		public void SetLastAction(Action action)
		{
			this._lastAction = action;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000276A3 File Offset: 0x000258A3
		protected virtual bool UpdateExtra()
		{
			return true;
		}

		// Token: 0x04000391 RID: 913
		private bool _isTaskCompletelyFinished;

		// Token: 0x04000392 RID: 914
		private bool _isMainTaskFinished;

		// Token: 0x04000393 RID: 915
		private bool _lastActionCalled;

		// Token: 0x04000394 RID: 916
		private DividableTask _continueToTask;

		// Token: 0x04000395 RID: 917
		private Action _lastAction;
	}
}
