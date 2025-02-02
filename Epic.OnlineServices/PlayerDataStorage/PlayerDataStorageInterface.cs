using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200028A RID: 650
	public sealed class PlayerDataStorageInterface : Handle
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x00019A20 File Offset: 0x00017C20
		public PlayerDataStorageInterface()
		{
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00019A2A File Offset: 0x00017C2A
		public PlayerDataStorageInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00019A38 File Offset: 0x00017C38
		public Result CopyFileMetadataAtIndex(ref CopyFileMetadataAtIndexOptions copyFileMetadataOptions, out FileMetadata? outMetadata)
		{
			CopyFileMetadataAtIndexOptionsInternal copyFileMetadataAtIndexOptionsInternal = default(CopyFileMetadataAtIndexOptionsInternal);
			copyFileMetadataAtIndexOptionsInternal.Set(ref copyFileMetadataOptions);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_PlayerDataStorage_CopyFileMetadataAtIndex(base.InnerHandle, ref copyFileMetadataAtIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyFileMetadataAtIndexOptionsInternal>(ref copyFileMetadataAtIndexOptionsInternal);
			Helper.Get<FileMetadataInternal, FileMetadata>(zero, out outMetadata);
			bool flag = outMetadata != null;
			if (flag)
			{
				Bindings.EOS_PlayerDataStorage_FileMetadata_Release(zero);
			}
			return result;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00019A98 File Offset: 0x00017C98
		public Result CopyFileMetadataByFilename(ref CopyFileMetadataByFilenameOptions copyFileMetadataOptions, out FileMetadata? outMetadata)
		{
			CopyFileMetadataByFilenameOptionsInternal copyFileMetadataByFilenameOptionsInternal = default(CopyFileMetadataByFilenameOptionsInternal);
			copyFileMetadataByFilenameOptionsInternal.Set(ref copyFileMetadataOptions);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_PlayerDataStorage_CopyFileMetadataByFilename(base.InnerHandle, ref copyFileMetadataByFilenameOptionsInternal, ref zero);
			Helper.Dispose<CopyFileMetadataByFilenameOptionsInternal>(ref copyFileMetadataByFilenameOptionsInternal);
			Helper.Get<FileMetadataInternal, FileMetadata>(zero, out outMetadata);
			bool flag = outMetadata != null;
			if (flag)
			{
				Bindings.EOS_PlayerDataStorage_FileMetadata_Release(zero);
			}
			return result;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00019AF8 File Offset: 0x00017CF8
		public Result DeleteCache(ref DeleteCacheOptions options, object clientData, OnDeleteCacheCompleteCallback completionCallback)
		{
			DeleteCacheOptionsInternal deleteCacheOptionsInternal = default(DeleteCacheOptionsInternal);
			deleteCacheOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDeleteCacheCompleteCallbackInternal onDeleteCacheCompleteCallbackInternal = new OnDeleteCacheCompleteCallbackInternal(PlayerDataStorageInterface.OnDeleteCacheCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onDeleteCacheCompleteCallbackInternal, Array.Empty<Delegate>());
			Result result = Bindings.EOS_PlayerDataStorage_DeleteCache(base.InnerHandle, ref deleteCacheOptionsInternal, zero, onDeleteCacheCompleteCallbackInternal);
			Helper.Dispose<DeleteCacheOptionsInternal>(ref deleteCacheOptionsInternal);
			return result;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00019B5C File Offset: 0x00017D5C
		public void DeleteFile(ref DeleteFileOptions deleteOptions, object clientData, OnDeleteFileCompleteCallback completionCallback)
		{
			DeleteFileOptionsInternal deleteFileOptionsInternal = default(DeleteFileOptionsInternal);
			deleteFileOptionsInternal.Set(ref deleteOptions);
			IntPtr zero = IntPtr.Zero;
			OnDeleteFileCompleteCallbackInternal onDeleteFileCompleteCallbackInternal = new OnDeleteFileCompleteCallbackInternal(PlayerDataStorageInterface.OnDeleteFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onDeleteFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_DeleteFile(base.InnerHandle, ref deleteFileOptionsInternal, zero, onDeleteFileCompleteCallbackInternal);
			Helper.Dispose<DeleteFileOptionsInternal>(ref deleteFileOptionsInternal);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00019BB8 File Offset: 0x00017DB8
		public void DuplicateFile(ref DuplicateFileOptions duplicateOptions, object clientData, OnDuplicateFileCompleteCallback completionCallback)
		{
			DuplicateFileOptionsInternal duplicateFileOptionsInternal = default(DuplicateFileOptionsInternal);
			duplicateFileOptionsInternal.Set(ref duplicateOptions);
			IntPtr zero = IntPtr.Zero;
			OnDuplicateFileCompleteCallbackInternal onDuplicateFileCompleteCallbackInternal = new OnDuplicateFileCompleteCallbackInternal(PlayerDataStorageInterface.OnDuplicateFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onDuplicateFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_DuplicateFile(base.InnerHandle, ref duplicateFileOptionsInternal, zero, onDuplicateFileCompleteCallbackInternal);
			Helper.Dispose<DuplicateFileOptionsInternal>(ref duplicateFileOptionsInternal);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00019C14 File Offset: 0x00017E14
		public Result GetFileMetadataCount(ref GetFileMetadataCountOptions getFileMetadataCountOptions, out int outFileMetadataCount)
		{
			GetFileMetadataCountOptionsInternal getFileMetadataCountOptionsInternal = default(GetFileMetadataCountOptionsInternal);
			getFileMetadataCountOptionsInternal.Set(ref getFileMetadataCountOptions);
			outFileMetadataCount = Helper.GetDefault<int>();
			Result result = Bindings.EOS_PlayerDataStorage_GetFileMetadataCount(base.InnerHandle, ref getFileMetadataCountOptionsInternal, ref outFileMetadataCount);
			Helper.Dispose<GetFileMetadataCountOptionsInternal>(ref getFileMetadataCountOptionsInternal);
			return result;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00019C58 File Offset: 0x00017E58
		public void QueryFile(ref QueryFileOptions queryFileOptions, object clientData, OnQueryFileCompleteCallback completionCallback)
		{
			QueryFileOptionsInternal queryFileOptionsInternal = default(QueryFileOptionsInternal);
			queryFileOptionsInternal.Set(ref queryFileOptions);
			IntPtr zero = IntPtr.Zero;
			OnQueryFileCompleteCallbackInternal onQueryFileCompleteCallbackInternal = new OnQueryFileCompleteCallbackInternal(PlayerDataStorageInterface.OnQueryFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onQueryFileCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_QueryFile(base.InnerHandle, ref queryFileOptionsInternal, zero, onQueryFileCompleteCallbackInternal);
			Helper.Dispose<QueryFileOptionsInternal>(ref queryFileOptionsInternal);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00019CB4 File Offset: 0x00017EB4
		public void QueryFileList(ref QueryFileListOptions queryFileListOptions, object clientData, OnQueryFileListCompleteCallback completionCallback)
		{
			QueryFileListOptionsInternal queryFileListOptionsInternal = default(QueryFileListOptionsInternal);
			queryFileListOptionsInternal.Set(ref queryFileListOptions);
			IntPtr zero = IntPtr.Zero;
			OnQueryFileListCompleteCallbackInternal onQueryFileListCompleteCallbackInternal = new OnQueryFileListCompleteCallbackInternal(PlayerDataStorageInterface.OnQueryFileListCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onQueryFileListCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_PlayerDataStorage_QueryFileList(base.InnerHandle, ref queryFileListOptionsInternal, zero, onQueryFileListCompleteCallbackInternal);
			Helper.Dispose<QueryFileListOptionsInternal>(ref queryFileListOptionsInternal);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00019D10 File Offset: 0x00017F10
		public PlayerDataStorageFileTransferRequest ReadFile(ref ReadFileOptions readOptions, object clientData, OnReadFileCompleteCallback completionCallback)
		{
			ReadFileOptionsInternal readFileOptionsInternal = default(ReadFileOptionsInternal);
			readFileOptionsInternal.Set(ref readOptions);
			IntPtr zero = IntPtr.Zero;
			OnReadFileCompleteCallbackInternal onReadFileCompleteCallbackInternal = new OnReadFileCompleteCallbackInternal(PlayerDataStorageInterface.OnReadFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onReadFileCompleteCallbackInternal, new Delegate[]
			{
				readOptions.ReadFileDataCallback,
				ReadFileOptionsInternal.ReadFileDataCallback,
				readOptions.FileTransferProgressCallback,
				ReadFileOptionsInternal.FileTransferProgressCallback
			});
			IntPtr from = Bindings.EOS_PlayerDataStorage_ReadFile(base.InnerHandle, ref readFileOptionsInternal, zero, onReadFileCompleteCallbackInternal);
			Helper.Dispose<ReadFileOptionsInternal>(ref readFileOptionsInternal);
			PlayerDataStorageFileTransferRequest result;
			Helper.Get<PlayerDataStorageFileTransferRequest>(from, out result);
			return result;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public PlayerDataStorageFileTransferRequest WriteFile(ref WriteFileOptions writeOptions, object clientData, OnWriteFileCompleteCallback completionCallback)
		{
			WriteFileOptionsInternal writeFileOptionsInternal = default(WriteFileOptionsInternal);
			writeFileOptionsInternal.Set(ref writeOptions);
			IntPtr zero = IntPtr.Zero;
			OnWriteFileCompleteCallbackInternal onWriteFileCompleteCallbackInternal = new OnWriteFileCompleteCallbackInternal(PlayerDataStorageInterface.OnWriteFileCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionCallback, onWriteFileCompleteCallbackInternal, new Delegate[]
			{
				writeOptions.WriteFileDataCallback,
				WriteFileOptionsInternal.WriteFileDataCallback,
				writeOptions.FileTransferProgressCallback,
				WriteFileOptionsInternal.FileTransferProgressCallback
			});
			IntPtr from = Bindings.EOS_PlayerDataStorage_WriteFile(base.InnerHandle, ref writeFileOptionsInternal, zero, onWriteFileCompleteCallbackInternal);
			Helper.Dispose<WriteFileOptionsInternal>(ref writeFileOptionsInternal);
			PlayerDataStorageFileTransferRequest result;
			Helper.Get<PlayerDataStorageFileTransferRequest>(from, out result);
			return result;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00019E30 File Offset: 0x00018030
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

		// Token: 0x0600119F RID: 4511 RVA: 0x00019E58 File Offset: 0x00018058
		[MonoPInvokeCallback(typeof(OnDeleteFileCompleteCallbackInternal))]
		internal static void OnDeleteFileCompleteCallbackInternalImplementation(ref DeleteFileCallbackInfoInternal data)
		{
			OnDeleteFileCompleteCallback onDeleteFileCompleteCallback;
			DeleteFileCallbackInfo deleteFileCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DeleteFileCallbackInfoInternal, OnDeleteFileCompleteCallback, DeleteFileCallbackInfo>(ref data, out onDeleteFileCompleteCallback, out deleteFileCallbackInfo);
			if (flag)
			{
				onDeleteFileCompleteCallback(ref deleteFileCallbackInfo);
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00019E80 File Offset: 0x00018080
		[MonoPInvokeCallback(typeof(OnDuplicateFileCompleteCallbackInternal))]
		internal static void OnDuplicateFileCompleteCallbackInternalImplementation(ref DuplicateFileCallbackInfoInternal data)
		{
			OnDuplicateFileCompleteCallback onDuplicateFileCompleteCallback;
			DuplicateFileCallbackInfo duplicateFileCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DuplicateFileCallbackInfoInternal, OnDuplicateFileCompleteCallback, DuplicateFileCallbackInfo>(ref data, out onDuplicateFileCompleteCallback, out duplicateFileCallbackInfo);
			if (flag)
			{
				onDuplicateFileCompleteCallback(ref duplicateFileCallbackInfo);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00019EA8 File Offset: 0x000180A8
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

		// Token: 0x060011A2 RID: 4514 RVA: 0x00019ED8 File Offset: 0x000180D8
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

		// Token: 0x060011A3 RID: 4515 RVA: 0x00019F00 File Offset: 0x00018100
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

		// Token: 0x060011A4 RID: 4516 RVA: 0x00019F28 File Offset: 0x00018128
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

		// Token: 0x060011A5 RID: 4517 RVA: 0x00019F50 File Offset: 0x00018150
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

		// Token: 0x060011A6 RID: 4518 RVA: 0x00019F94 File Offset: 0x00018194
		[MonoPInvokeCallback(typeof(OnWriteFileCompleteCallbackInternal))]
		internal static void OnWriteFileCompleteCallbackInternalImplementation(ref WriteFileCallbackInfoInternal data)
		{
			OnWriteFileCompleteCallback onWriteFileCompleteCallback;
			WriteFileCallbackInfo writeFileCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<WriteFileCallbackInfoInternal, OnWriteFileCompleteCallback, WriteFileCallbackInfo>(ref data, out onWriteFileCompleteCallback, out writeFileCallbackInfo);
			if (flag)
			{
				onWriteFileCompleteCallback(ref writeFileCallbackInfo);
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00019FBC File Offset: 0x000181BC
		[MonoPInvokeCallback(typeof(OnWriteFileDataCallbackInternal))]
		internal static WriteResult OnWriteFileDataCallbackInternalImplementation(ref WriteFileDataCallbackInfoInternal data, IntPtr outDataBuffer, ref uint outDataWritten)
		{
			OnWriteFileDataCallback onWriteFileDataCallback;
			WriteFileDataCallbackInfo writeFileDataCallbackInfo;
			bool flag = Helper.TryGetStructCallback<WriteFileDataCallbackInfoInternal, OnWriteFileDataCallback, WriteFileDataCallbackInfo>(ref data, out onWriteFileDataCallback, out writeFileDataCallbackInfo);
			WriteResult result;
			if (flag)
			{
				WriteFileDataCallbackInfo writeFileDataCallbackInfo2;
				Helper.Get<WriteFileDataCallbackInfoInternal, WriteFileDataCallbackInfo>(ref data, out writeFileDataCallbackInfo2);
				ArraySegment<byte> from;
				WriteResult writeResult = onWriteFileDataCallback(ref writeFileDataCallbackInfo2, out from);
				Helper.Get<byte>(from, out outDataWritten);
				Helper.Copy(from, outDataBuffer);
				result = writeResult;
			}
			else
			{
				result = Helper.GetDefault<WriteResult>();
			}
			return result;
		}

		// Token: 0x040007B6 RID: 1974
		public const int CopyfilemetadataatindexApiLatest = 1;

		// Token: 0x040007B7 RID: 1975
		public const int CopyfilemetadataatindexoptionsApiLatest = 1;

		// Token: 0x040007B8 RID: 1976
		public const int CopyfilemetadatabyfilenameApiLatest = 1;

		// Token: 0x040007B9 RID: 1977
		public const int CopyfilemetadatabyfilenameoptionsApiLatest = 1;

		// Token: 0x040007BA RID: 1978
		public const int DeletecacheApiLatest = 1;

		// Token: 0x040007BB RID: 1979
		public const int DeletecacheoptionsApiLatest = 1;

		// Token: 0x040007BC RID: 1980
		public const int DeletefileApiLatest = 1;

		// Token: 0x040007BD RID: 1981
		public const int DeletefileoptionsApiLatest = 1;

		// Token: 0x040007BE RID: 1982
		public const int DuplicatefileApiLatest = 1;

		// Token: 0x040007BF RID: 1983
		public const int DuplicatefileoptionsApiLatest = 1;

		// Token: 0x040007C0 RID: 1984
		public const int FileMaxSizeBytes = 67108864;

		// Token: 0x040007C1 RID: 1985
		public const int FilemetadataApiLatest = 3;

		// Token: 0x040007C2 RID: 1986
		public const int FilenameMaxLengthBytes = 64;

		// Token: 0x040007C3 RID: 1987
		public const int GetfilemetadatacountApiLatest = 1;

		// Token: 0x040007C4 RID: 1988
		public const int GetfilemetadatacountoptionsApiLatest = 1;

		// Token: 0x040007C5 RID: 1989
		public const int QueryfileApiLatest = 1;

		// Token: 0x040007C6 RID: 1990
		public const int QueryfilelistApiLatest = 1;

		// Token: 0x040007C7 RID: 1991
		public const int QueryfilelistoptionsApiLatest = 1;

		// Token: 0x040007C8 RID: 1992
		public const int QueryfileoptionsApiLatest = 1;

		// Token: 0x040007C9 RID: 1993
		public const int ReadfileApiLatest = 1;

		// Token: 0x040007CA RID: 1994
		public const int ReadfileoptionsApiLatest = 1;

		// Token: 0x040007CB RID: 1995
		public const int WritefileApiLatest = 1;

		// Token: 0x040007CC RID: 1996
		public const int WritefileoptionsApiLatest = 1;
	}
}
