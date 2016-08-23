// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Symbols;
using SharpScript.Scripting.Common.Hosting;

namespace SharpScript.Scripting.CSharp.Hosting
{
    internal class CSharpMemberFilter : CommonMemberFilter
    {
        protected override bool IsGeneratedMemberName(string name)
        {
            // Generated fields, e.g. "<property_name>k__BackingField"
            return GeneratedNames.IsGeneratedMemberName(name);
        }
    }
}