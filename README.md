Async Daemon failed to process multi-stream projection when appending events in RaiseSideEffects

testlog:

```
 MartenIssueTests.side_effects_in_aggregations.add_events_multi_stream
   Source: side_effects_in_aggregations.cs line 12
   Duration: 34.7 sec

  Message: 
System.AggregateException : One or more errors occurred. (The projection shards (in total of 2) haven't been completely started within the timeout span


--------------------------------
| Shard Name      | Sequence   |
--------------------------------
| HighWaterMark   |           1|
| MyDoc:All       |           0|
--------------------------------


) (Reading as 'System.Int64[]' is not supported for fields having DataTypeName 'integer')
---- System.TimeoutException : The projection shards (in total of 2) haven't been completely started within the timeout span


--------------------------------
| Shard Name      | Sequence   |
--------------------------------
| HighWaterMark   |           1|
| MyDoc:All       |           0|
--------------------------------



---- System.InvalidCastException : Reading as 'System.Int64[]' is not supported for fields having DataTypeName 'integer'

  Stack Trace: 
JasperFxAsyncDaemon`3.WaitForNonStaleData(TimeSpan timeout)
side_effects_in_aggregations.add_events_multi_stream() line 43
--- End of stack trace from previous location ---
----- Inner Stack Trace #1 (System.TimeoutException) -----
TestingExtensions.WaitForNonStaleDataAsync(IMartenDatabase database, CancellationTokenSource cancellationSource, Int32 projectionsCount, EventStoreStatistics initial)
TestingExtensions.WaitForNonStaleProjectionDataAsync(IMartenDatabase database, TimeSpan timeout)
JasperFxAsyncDaemon`3.WaitForNonStaleData(TimeSpan timeout)
----- Inner Stack Trace #2 (System.InvalidCastException) -----
AdoSerializerHelpers.<GetTypeInfoForReading>g__ThrowReadingNotSupported|0_0(Type type, PgSerializerOptions options, PgTypeId pgTypeId, Exception inner)
AdoSerializerHelpers.GetTypeInfoForReading(Type type, PgTypeId pgTypeId, PgSerializerOptions options)
FieldDescription.<GetInfoCore>g__GetInfoSlow|51_0(Type type, ColumnInfo& lastColumnInfo)
FieldDescription.GetInfoCore(Type type, ColumnInfo& lastColumnInfo)
FieldDescription.GetInfo(Type type, ColumnInfo& lastColumnInfo)
NpgsqlDataReader.<GetInfo>g__Slow|133_0(ColumnInfo& info, PgConverter& converter, Size& bufferRequirement, Boolean& asObject, <>c__DisplayClass133_0&)
NpgsqlDataReader.GetFieldValueCore[T](Int32 ordinal)
NpgsqlDataReader.GetFieldValueAsync[T](Int32 ordinal, CancellationToken cancellationToken)
QuickAppendEventsOperationBase.PostprocessAsync(DbDataReader reader, IList`1 exceptions, CancellationToken token)
OperationPage.ApplyCallbacksAsync(DbDataReader reader, IList`1 exceptions, CancellationToken token)
AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token)
AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token)
ExceptionTransformExtensions.TransformAndThrow(IEnumerable`1 transforms, Exception ex)
AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token)
AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token)
AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token)
<<ExecuteAsync>b__2_0>d.MoveNext()
--- End of stack trace from previous location ---
Outcome`1.GetResultOrRethrow()
ResiliencePipeline.ExecuteAsync[TState](Func`3 callback, TState state, CancellationToken cancellationToken)
DocumentSessionBase.ExecuteBatchAsync(IUpdateBatch batch, CancellationToken token)
ExceptionTransformExtensions.TransformAndThrow(IEnumerable`1 transforms, Exception ex)
DocumentSessionBase.ExecuteBatchAsync(IUpdateBatch batch, CancellationToken token)
DocumentSessionBase.ExecuteBatchAsync(IUpdateBatch batch, CancellationToken token)
ProjectionBatch.ExecuteAsync(CancellationToken token)
GroupedProjectionExecution.applyBatchOperationsToDatabaseAsync(EventRange range, IProjectionBatch batch)
GroupedProjectionExecution.applyBatchOperationsToDatabaseAsync(EventRange range, IProjectionBatch batch)
GroupedProjectionExecution.processRangeAsync(EventRange range, CancellationToken _)
GroupedProjectionExecution.processRangeAsync(EventRange range, CancellationToken _)

```
