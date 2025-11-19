MyBackgroundService simply starts a new stream once the service is started.

The async deamon will fail to create the MultiStreamProjection and throw an exception

```
System.InvalidCastException: Reading as 'System.Int64[]' is not supported for fields having DataTypeName 'integer'
   at Npgsql.Internal.AdoSerializerHelpers.<GetTypeInfoForReading>g__ThrowReadingNotSupported|0_0(Type type, PgSerializerOptions options, PgTypeId pgTypeId, Exception inner)
   at Npgsql.Internal.AdoSerializerHelpers.GetTypeInfoForReading(Type type, PgTypeId pgTypeId, PgSerializerOptions options)
   at Npgsql.BackendMessages.FieldDescription.<GetInfoCore>g__GetInfoSlow|51_0(Type type, ColumnInfo& lastColumnInfo)
   at Npgsql.BackendMessages.FieldDescription.GetInfoCore(Type type, ColumnInfo& lastColumnInfo)
   at Npgsql.BackendMessages.FieldDescription.GetInfo(Type type, ColumnInfo& lastColumnInfo)
   at Npgsql.NpgsqlDataReader.<GetInfo>g__Slow|133_0(ColumnInfo& info, PgConverter& converter, Size& bufferRequirement, Boolean& asObject, <>c__DisplayClass133_0&)
   at Npgsql.NpgsqlDataReader.GetFieldValueCore[T](Int32 ordinal)
   at Npgsql.NpgsqlDataReader.GetFieldValueAsync[T](Int32 ordinal, CancellationToken cancellationToken)
   at Marten.Events.Operations.QuickAppendEventsOperationBase.PostprocessAsync(DbDataReader reader, IList`1 exceptions, CancellationToken token) in /_/src/Marten/Events/Operations/QuickAppendEventsOperationBase.cs:line 140
   at Marten.Internal.Sessions.OperationPage.ApplyCallbacksAsync(DbDataReader reader, IList`1 exceptions, CancellationToken token) in /_/src/Marten/Internal/Sessions/OperationPage.cs:line 151
   at Marten.Internal.Sessions.AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token) in /_/src/Marten/Internal/Sessions/AutoClosingLifetime.cs:line 316
   at Marten.Internal.Sessions.AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token) in /_/src/Marten/Internal/Sessions/AutoClosingLifetime.cs:line 317
   at JasperFx.Core.Exceptions.ExceptionTransformExtensions.TransformAndThrow(IEnumerable`1 transforms, Exception ex) in /_/src/JasperFx/Core/Exceptions/IExceptionTransform.cs:line 174
   at Marten.Internal.Sessions.AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token) in /_/src/Marten/Internal/Sessions/AutoClosingLifetime.cs:line 341
   at Marten.Internal.Sessions.AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token) in /_/src/Marten/Internal/Sessions/AutoClosingLifetime.cs:line 378
   at Marten.Internal.Sessions.AutoClosingLifetime.ExecuteBatchPagesAsync(IReadOnlyList`1 pages, List`1 exceptions, CancellationToken token) in /_/src/Marten/Internal/Sessions/AutoClosingLifetime.cs:line 378
   at Polly.ResiliencePipeline.<>c__2`1.<<ExecuteAsync>b__2_0>d.MoveNext()
--- End of stack trace from previous location ---
   at Polly.Outcome`1.GetResultOrRethrow()
   at Polly.ResiliencePipeline.ExecuteAsync[TState](Func`3 callback, TState state, CancellationToken cancellationToken)
   at Marten.Internal.Sessions.DocumentSessionBase.ExecuteBatchAsync(IUpdateBatch batch, CancellationToken token) in /_/src/Marten/Internal/Sessions/DocumentSessionBase.SaveChanges.cs:line 130
   at JasperFx.Core.Exceptions.ExceptionTransformExtensions.TransformAndThrow(IEnumerable`1 transforms, Exception ex) in /_/src/JasperFx/Core/Exceptions/IExceptionTransform.cs:line 174
   at Marten.Internal.Sessions.DocumentSessionBase.ExecuteBatchAsync(IUpdateBatch batch, CancellationToken token) in /_/src/Marten/Internal/Sessions/DocumentSessionBase.SaveChanges.cs:line 137
   at Marten.Internal.Sessions.DocumentSessionBase.ExecuteBatchAsync(IUpdateBatch batch, CancellationToken token) in /_/src/Marten/Internal/Sessions/DocumentSessionBase.SaveChanges.cs:line 154
   at Marten.Events.Daemon.Internals.ProjectionBatch.ExecuteAsync(CancellationToken token) in /_/src/Marten/Events/Daemon/Internals/ProjectionBatch.cs:line 32
   at JasperFx.Events.Daemon.GroupedProjectionExecution.applyBatchOperationsToDatabaseAsync(EventRange range, IProjectionBatch batch) in /_/src/JasperFx.Events/Daemon/GroupedProjectionExecution.cs:line 176
```
