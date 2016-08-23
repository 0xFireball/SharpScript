// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace SharpScript.Scripting.Common
{
    internal static class ScriptStateTaskExtensions
    {
        internal async static Task<T> CastAsync<S, T>(this Task<S> task) where S : T
        {
            return await task.ConfigureAwait(true);
        }

        internal async static Task<T> GetEvaluationResultAsync<T>(this Task<ScriptState<T>> task)
        {
            return (await task.ConfigureAwait(true)).ReturnValue;
        }
    }
}
