using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x020000A4 RID: 164
	public sealed class TitleStorageInterface : Handle
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x000090E8 File Offset: 0x000072E8
		public TitleStorageInterface()
		{
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000090F2 File Offset: 0x000072F2
		public TitleStorageInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00009100 File Offset: 0x00007300
		public Result CopyFileMetadataAtIndex(ref CopyFileMetadataAtIndexOptions options, out FileMetadata? outMetadata)
		{
			CopyFileMetadataAtIndexOptionsInternal copyFileMetadataAtIndexOptionsInternal = default(CopyFileMetadataAtIndexOptionsInternal);
			copyFileMetadataAtIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_TitleStorage_CopyFileMetadataAtIndex(base.InnerHandle, ref copyFileMetadataAtIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyFileMetadataAtIndexOptionsInternal>(ref copyFileMetadataAtIndexOptionsInternal);
			Helper.Get<FileMetadataInternal, FileMetadata>(zero, out outMetadata);
			bool flag = outMetadata != null;
			if (flag)
			{
				Bindings.EOS_TitleStorage_FileMetadata_Release(zero);
			}
			return result;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00009160 File Offset: 0x00007360
		public Result CopyFileMetadataByFilename(ref CopyFileMetadataByFilenameOptions options, out FileMetadata? outMetadata)
		{
			CopyFileMetadataByFilenameOptionsInternal copyFileMetadataByFilenameOptionsInternal = default(CopyFileMetadataByFilenameOptionsInternal);
			copyFileMetadataByFilenameOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_TitleStorage_CopyFileMetadataByFilename(base.InnerHandle, ref copyFileMetadataByFilenameOptionsInternal, ref zero);
			Helper.Dispose<CopyFileMetadataByFilenameOptionsInternal>(ref copyFileMetadataByFilenameOptionsInternal);
			Helper.Get<FileMetadataInternal, FileMetadata>(zero, out outMetadata);
			bool flag = outMetadata != null;
			if (flag)
			{
				Bindings.EOS_TitleStorage_FileMetadata_Release(zero);
			}
			return result;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000091C0 File Offset: 0x000073C0
		public Result DeleteCache(ref DeleteCacheOptions options, object clientData, OnDeleteCacheCompleteCallback completionCallback)
		{
			DeleteCacheOptionsInternal deleteCacheOptionsInternal = default(DeleteCacheOptionsInternal);
			deleteCacheOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDeleteCacheCompleteCallbackInternal onDeleteCacheCompleteCallbackInternal = new OnDeleteCacheCompleteCallbackInternal(TitleStorageInterface.OnDeleteCacheCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onDeleteCacheCompleteCallbackInternal, Array.Empty<Delegate>());
			Result result = Bindings.EOS_TitleStorage_DeleteCache(base.InnerHandle, ref deleteCacheOptionsInternal, zero, onDeleteCacheCompleteCallbackInternal);
			Helper.Dispose<DeleteCacheOptionsInternal>(ref deleteCacheOptionsInternal);
			return result;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00009224 File Offset: 0x00007424
		public uint GetFileMetadataCount(ref GetFileMetadataCountOptions options)
		{
			GetFileMetadataCountOptionsInternal getFileMetadataCountOptionsInternal = default(GetFileMetadataCountOptionsInternal);
			getFileMetadataCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_TitleStorage_GetFileMetadataCount(base.InnerHandle, ref getFileMetadataCountOptionsInternal);
			Helper.Dispose<GetFileMetadataCountOptionsInternal>(ref getFileMetadataCountOptionsInternal);
			return result;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00009260 File Offset: 0x00007460
		public void QueryFile(ref QueryFileOptions options, object clientData, OnQueryFileCompleteCallback completionCallback)
		{
			QueryFileOptionsInternal queryFileOptionsInternal = default(QueryFileOptionsInternal);
			queryFileOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryFileCompleteCallbackInternal onQueryFileCompleteCallbackInternal = new OnQueryFileCompleteCallbackInternal(TitleStorageInterface.OnQueryFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onQueryFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_TitleStorage_QueryFile(base.InnerHandle, ref queryFileOptionsInternal, zero, onQueryFileCompleteCallbackInternal);
			Helper.Dispose<QueryFileOptionsInternal>(ref queryFileOptionsInternal);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000092BC File Offset: 0x000074BC
		public void QueryFileList(ref QueryFileListOptions options, object clientData, OnQueryFileListCompleteCallback completionCallback)
		{
			QueryFileListOptionsInternal queryFileListOptionsInternal = default(QueryFileListOptionsInternal);
			queryFileListOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryFileListCompleteCallbackInternal onQueryFileListCompleteCallbackInternal = new OnQueryFileListCompleteCallbackInternal(TitleStorageInterface.OnQueryFileListCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onQueryFileListCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_TitleStorage_QueryFileList(base.InnerHandle, ref queryFileListOptionsInternal, zero, onQueryFileListCompleteCallbackInternal);
			Helper.Dispose<QueryFileListOptionsInternal>(ref queryFileListOptionsInternal);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00009318 File Offset: 0x00007518
		public TitleStorageFileTransferRequest ReadFile(ref ReadFileOptions options, object clientData, OnReadFileCompleteCallback completionCallback)
		{
			ReadFileOptionsInternal readFileOptionsInternal = default(ReadFileOptionsInternal);
			readFileOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnReadFileCompleteCallbackInternal onReadFileCompleteCallbackInternal = new OnReadFileCompleteCallbackInternal(TitleStorageInterface.OnReadFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onReadFileCompleteCallbackInternal, new Delegate[]
			{
				options.ReadFileDataCallback,
				ReadFileOptionsInternal.ReadFileDataCallback,
				options.FileTransferProgressCallback,
				ReadFileOptionsInternal.FileTransferProgressCallback
			});
			IntPtr from = Bindings.EOS_TitleStorage_ReadFile(base.InnerHandle, ref readFileOptionsInternal, zero, onReadFileCompleteCallbackInternal);
			Helper.Dispose<ReadFileOptionsInternal>(ref readFileOptionsInternal);
			TitleStorageFileTransferRequest result;
			Helper.Get<TitleStorageFileTransferRequest>(from, out result);
			return result;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000093A8 File Offset: 0x000075A8
		[MonoPInvokeCallback(typeof(OnDeleteCacheCompleteCallbackInternal))]
		internal static void OnDeleteCacheCompleteCallbackInternalImplementation(ref DeleteCacheCallbackInfoInternal data)
		{
			OnDeleteCacheCompleteCallback onDeleteCacheCompleteCallback;
			DeleteCacheCallbackInfo deleteCacheCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DeleteCacheCallbackInfoInternal, OnDeleteCacheCompleteCallback, DeleteCacheCallbackInfo>(ref data, out onDeleteCacheCompleteCallback, out deleteCacheCallbackInfo);
			if (flag)
			{
				onDeleteCacheCompleteCallback(ref deleteCacheCallbackInfo);
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000093D0 File Offset: 0x000075D0
		[MonoPInvokeCallback(typeof(OnFileTransferProgressCallbackInternal))]
		internal static void OnFileTransferProgressCallbackInternalImplementation(ref FileTransferProgressCallbackInfoInternal data)
		{
			OnFileTransferProgressCallback onFileTransferProgressCallback;
			FileTransferProgressCallbackInfo fileTransferProgressCallbackInfo;
			bool flag = Helper.TryGetStructCallback<FileTransferProgressCallbackInfoInternal, OnFileTransferProgressCallback, FileTransferProgressCallbackInfo>(ref data, out onFileTransferProgressCallback, out fileTransferProgressCallbackInfo);
			if (flag)
			{
				FileTransferProgressCallbackInfo fileTransferProgressCallbackInfo2;
				Helper.Get<FileTransferProgressCallbackInfoInternal, FileTransferProgressCallbackInfo>(ref data, out fileTransferProgressCallbackInfo2);
				onFileTransferProgressCallback(ref fileTransferProgressCallbackInfo2);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00009400 File Offset: 0x00007600
		[MonoPInvokeCallback(typeof(OnQueryFileCompleteCallbackInternal))]
		internal static void OnQueryFileCompleteCallbackInternalImplementation(ref QueryFileCallbackInfoInternal data)
		{
			OnQueryFileCompleteCallback onQueryFileCompleteCallback;
			QueryFileCallbackInfo queryFileCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryFileCallbackInfoInternal, OnQueryFileCompleteCallback, QueryFileCallbackInfo>(ref data, out onQueryFileCompleteCallback, out queryFileCallbackInfo);
			if (flag)
			{
				onQueryFileCompleteCallback(ref queryFileCallbackInfo);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00009428 File Offset: 0x00007628
		[MonoPInvokeCallback(typeof(OnQueryFileListCompleteCallbackInternal))]
		internal static void OnQueryFileListCompleteCallbackInternalImplementation(ref QueryFileListCallbackInfoInternal data)
		{
			OnQueryFileListCompleteCallback onQueryFileListCompleteCallback;
			QueryFileListCallbackInfo queryFileListCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryFileListCallbackInfoInternal, OnQueryFileListCompleteCallback, QueryFileListCallbackInfo>(ref data, out onQueryFileListCompleteCallback, out queryFileListCallbackInfo);
			if (flag)
			{
				onQueryFileListCompleteCallback(ref queryFileListCallbackInfo);
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00009450 File Offset: 0x00007650
		[MonoPInvokeCallback(typeof(OnReadFileCompleteCallbackInternal))]
		internal static void OnReadFileCompleteCallbackInternalImplementation(ref ReadFileCallbackInfoInternal data)
		{
			OnReadFileCompleteCallback onReadFileCompleteCallback;
			ReadFileCallbackInfo readFileCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<ReadFileCallbackInfoInternal, OnReadFileCompleteCallback, ReadFileCallbackInfo>(ref data, out onReadFileCompleteCallback, out readFileCallbackInfo);
			if (flag)
			{
				onReadFileCompleteCallback(ref readFileCallbackInfo);
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00009478 File Offset: 0x00007678
		[MonoPInvokeCallback(typeof(OnReadFileDataCallbackInternal))]
		internal static ReadResult OnReadFileDataCallbackInternalImplementation(ref ReadFileDataCallbackInfoInternal data)
		{
			OnReadFileDataCallback onReadFileDataCallback;
			ReadFileDataCallbackInfo readFileDataCallbackInfo;
			bool flag = Helper.TryGetStructCallback<ReadFileDataCallbackInfoInternal, OnReadFileDataCallback, ReadFileDataCallbackInfo>(ref data, out onReadFileDataCallback, out readFileDataCallbackInfo);
			ReadResult result;
			if (flag)
			{
				ReadFileDataCallbackInfo readFileDataCallbackInfo2;
				Helper.Get<ReadFileDataCallbackInfoInternal, ReadFileDataCallbackInfo>(ref data, out readFileDataCallbackInfo2);
				ReadResult readResult = onReadFileDataCallback(ref readFileDataCallbackInfo2);
				result = readResult;
			}
			else
			{
				result = Helper.GetDefault<ReadResult>();
			}
			return result;
		}

		// Token: 0x040002E9 RID: 745
		public const int CopyfilemetadataatindexApiLatest = 1;

		// Token: 0x040002EA RID: 746
		public const int CopyfilemetadataatindexoptionsApiLatest = 1;

		// Token: 0x040002EB RID: 747
		public const int CopyfilemetadatabyfilenameApiLatest = 1;

		// Token: 0x040002EC RID: 748
		public const int CopyfilemetadatabyfilenameoptionsApiLatest = 1;

		// Token: 0x040002ED RID: 749
		public const int DeletecacheApiLatest = 1;

		// Token: 0x040002EE RID: 750
		public const int DeletecacheoptionsApiLatest = 1;

		// Token: 0x040002EF RID: 751
		public const int FilemetadataApiLatest = 2;

		// Token: 0x040002F0 RID: 752
		public const int FilenameMaxLengthBytes = 64;

		// Token: 0x040002F1 RID: 753
		public const int GetfilemetadatacountApiLatest = 1;

		// Token: 0x040002F2 RID: 754
		public const int GetfilemetadatacountoptionsApiLatest = 1;

		// Token: 0x040002F3 RID: 755
		public const int QueryfileApiLatest = 1;

		// Token: 0x040002F4 RID: 756
		public const int QueryfilelistApiLatest = 1;

		// Token: 0x040002F5 RID: 757
		public const int QueryfilelistoptionsApiLatest = 1;

		// Token: 0x040002F6 RID: 758
		public const int QueryfileoptionsApiLatest = 1;

		// Token: 0x040002F7 RID: 759
		public const int ReadfileApiLatest = 1;

		// Token: 0x040002F8 RID: 760
		public const int ReadfileoptionsApiLatest = 1;
	}
}
