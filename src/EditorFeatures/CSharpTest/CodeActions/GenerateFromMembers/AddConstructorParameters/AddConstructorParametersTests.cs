﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.CodeRefactorings.GenerateFromMembers.AddConstructorParameters;
using Roslyn.Test.Utilities;
using Xunit;

namespace Microsoft.CodeAnalysis.Editor.CSharp.UnitTests.CodeRefactorings.GenerateFromMembers.AddConstructorParameters
{
    public class AddConstructorParametersTests : AbstractCSharpCodeActionTest
    {
        protected override object CreateCodeRefactoringProvider(Workspace workspace)
        {
            return new AddConstructorParametersCodeRefactoringProvider();
        }

        [Fact, WorkItem(308077), Trait(Traits.Feature, Traits.Features.CodeActionsAddConstructorParameters)]
        public void TestAdd1()
        {
            Test(
@"using System . Collections . Generic ; class Program { [|int i ; string s ;|] public Program ( int i ) { this . i = i ; } } ",
@"using System . Collections . Generic ; class Program { int i ; string s ; public Program ( int i , string s ) { this . i = i ; this . s = s ; } } ",
index: 0);
        }

        [Fact, WorkItem(308077), Trait(Traits.Feature, Traits.Features.CodeActionsAddConstructorParameters)]
        public void TestAddOptional1()
        {
            Test(
@"using System . Collections . Generic ; class Program { [|int i ; string s ;|] public Program ( int i ) { this . i = i ; } } ",
@"using System . Collections . Generic ; class Program { int i ; string s ; public Program ( int i , string s = null ) { this . i = i ; this . s = s ; } } ",
index: 1);
        }

        [Fact, WorkItem(308077), Trait(Traits.Feature, Traits.Features.CodeActionsAddConstructorParameters)]
        public void TestAddToConstructorWithMostMatchingParameters1()
        {
            Test(
@"using System . Collections . Generic ; class Program { [|int i ; string s ; bool b ;|] public Program ( int i ) { this . i = i ; } public Program ( int i , string s ) : this ( i ) { this . s = s ; } } ",
@"using System . Collections . Generic ; class Program { int i ; string s ; bool b ; public Program ( int i ) { this . i = i ; } public Program ( int i , string s , bool b ) : this ( i ) { this . s = s ; this . b = b ; } } ",
index: 0);
        }

        [Fact, WorkItem(308077), Trait(Traits.Feature, Traits.Features.CodeActionsAddConstructorParameters)]
        public void TestAddOptionalToConstructorWithMostMatchingParameters1()
        {
            Test(
@"using System . Collections . Generic ; class Program { [|int i ; string s ; bool b ;|] public Program ( int i ) { this . i = i ; } public Program ( int i , string s ) : this ( i ) { this . s = s ; } } ",
@"using System . Collections . Generic ; class Program { int i ; string s ; bool b ; public Program ( int i ) { this . i = i ; } public Program ( int i , string s , bool b = default(bool) ) : this ( i ) { this . s = s ; this . b = b ; } } ",
index: 1);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.CodeActionsAddConstructorParameters)]
        public void TestSmartTagDisplayText1()
        {
            TestSmartTagText(
@"using System . Collections . Generic ; class Program { [|bool b ; HashSet < string > s ;|] public Program ( bool b ) { this . b = b ; } } ",
string.Format(FeaturesResources.AddParametersTo, "Program", "bool"),
index: 0);
        }

        [Fact, Trait(Traits.Feature, Traits.Features.CodeActionsAddConstructorParameters)]
        public void TestSmartTagDisplayText2()
        {
            TestSmartTagText(
@"using System . Collections . Generic ; class Program { [|bool b ; HashSet < string > s ;|] public Program ( bool b ) { this . b = b ; } } ",
string.Format(FeaturesResources.AddOptionalParametersTo, "Program", "bool"),
index: 1);
        }
    }
}
