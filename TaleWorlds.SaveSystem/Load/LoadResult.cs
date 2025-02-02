using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x0200003A RID: 58
	public class LoadResult
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00009CE5 File Offset: 0x00007EE5
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00009CED File Offset: 0x00007EED
		public object Root { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00009CF6 File Offset: 0x00007EF6
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00009CFE File Offset: 0x00007EFE
		public bool Successful { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009D07 File Offset: 0x00007F07
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00009D0F File Offset: 0x00007F0F
		public LoadError[] Errors { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00009D18 File Offset: 0x00007F18
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00009D20 File Offset: 0x00007F20
		public MetaData MetaData { get; private set; }

		// Token: 0x06000211 RID: 529 RVA: 0x00009D29 File Offset: 0x00007F29
		private LoadResult()
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009D31 File Offset: 0x00007F31
		internal static LoadResult CreateSuccessful(object root, MetaData metaData, LoadCallbackInitializator loadCallbackInitializator)
		{
			return new LoadResult
			{
				Root = root,
				Successful = true,
				MetaData = metaData,
				_loadCallbackInitializator = loadCallbackInitializator
			};
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009D54 File Offset: 0x00007F54
		internal static LoadResult CreateFailed(IEnumerable<LoadError> errors)
		{
			return new LoadResult
			{
				Successful = false,
				Errors = errors.ToArray<LoadError>()
			};
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009D6E File Offset: 0x00007F6E
		public void InitializeObjects()
		{
			this._loadCallbackInitializator.InitializeObjects();
		}

		// Token: 0x040000A6 RID: 166
		private LoadCallbackInitializator _loadCallbackInitializator;
	}
}
