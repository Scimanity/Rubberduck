﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rubberduck.Parsing.VBA;
using Rubberduck.Refactorings;
using Rubberduck.Refactorings.RemoveParameters;
using Rubberduck.VBEditor;
using Rubberduck.VBEditor.Extensions;

namespace RubberduckTests.Refactoring
{
    [TestClass]
    public class RemoveParametersTests : RefactoringTestBase
    {
        [TestMethod]
        public void RemoveParametersRefactoring_RemoveBothParams()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String)
End Sub";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo( )
End Sub";

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Params to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters.ForEach(arg => arg.IsRemoved = true);

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveOnlyParam()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer)
End Sub";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo()
End Sub";

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Params to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters.ForEach(arg => arg.IsRemoved = true);

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveFirstParam()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String)
End Sub";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo( ByVal arg2 As String)
End Sub"; //note: The IDE strips out the extra whitespace

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveSecondParam()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String)
End Sub";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo(ByVal arg1 As Integer )
End Sub"; //note: The IDE strips out the extra whitespace

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[1].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveNamedParam()
        {
            //Input
            const string inputCode =
@"Public Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String, ByVal arg3 As Double)
End Sub

Public Sub Goo()
    Foo arg2:=""test44"", arg3:=6.1, arg1:=3
End Sub
";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Public Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String )
End Sub

Public Sub Goo()
 Foo arg2:=""test44"",  arg1:=3
End Sub
"; //note: The IDE strips out the extra whitespace

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[2].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveLastFromFunction()
        {
            //Input
            const string inputCode =
@"Private Function Foo(ByVal arg1 As Integer, ByVal arg2 As String) As Boolean
End Function";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Function Foo(ByVal arg1 As Integer ) As Boolean
End Function"; //note: The IDE strips out the extra whitespace

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[1].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveAllFromFunction()
        {
            //Input
            const string inputCode =
@"Private Function Foo(ByVal arg1 As Integer, ByVal arg2 As String) As Boolean
End Function";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Function Foo( ) As Boolean
End Function"; //note: The IDE strips out the extra whitespace

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters.ForEach(p => p.IsRemoved = true);

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveAllFromFunction_UpdateCallReferences()
        {
            //Input
            const string inputCode =
@"Private Function Foo(ByVal arg1 As Integer, ByVal arg2 As String) As Boolean
End Function

Private Sub Goo(ByVal arg1 As Integer, ByVal arg2 As String)
    Foo arg1, arg2
End Sub
";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Function Foo( ) As Boolean
End Function

Private Sub Goo(ByVal arg1 As Integer, ByVal arg2 As String)
 Foo  
End Sub
"; //note: The IDE strips out the extra whitespace

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters.ForEach(p => p.IsRemoved = true);

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveFromGetter()
        {
            //Input
            const string inputCode =
@"Private Property Get Foo(ByVal arg1 As Integer) As Boolean
End Property";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Property Get Foo() As Boolean
End Property"; //note: The IDE strips out the extra whitespace

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters.ForEach(p => p.IsRemoved = true);

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_QuickFix()
        {
            //Input
            const string inputCode =
@"Private Property Set Foo(ByVal arg1 As Integer, ByVal arg2 As String) 
End Property";
            var selection = new Selection(1, 38, 1, 38); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Property Set Foo( ByVal arg2 As String)
End Property"; //note: The IDE strips out the extra whitespace

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.QuickFix(parseResult, qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveFirstParamFromSetter()
        {
            //Input
            const string inputCode =
@"Private Property Set Foo(ByVal arg1 As Integer, ByVal arg2 As String) 
End Property";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Property Set Foo( ByVal arg2 As String)
End Property"; //note: The IDE strips out the extra whitespace

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_ClientReferencesAreUpdated_FirstParam()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String)
End Sub

Private Sub Bar()
    Foo 10, ""Hello""
End Sub
";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo( ByVal arg2 As String)
End Sub

Private Sub Bar()
 Foo  ""Hello""
End Sub
"; //note: The IDE strips out the extra whitespace

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_ClientReferencesAreUpdated_LastParam()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String)
End Sub

Private Sub Bar()
    Foo 10, ""Hello""
End Sub
";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo(ByVal arg1 As Integer )
End Sub

Private Sub Bar()
 Foo 10 
End Sub
"; //note: The IDE strips out the extra whitespace, you can't see it but there's a space after "Foo 10 "

            //Arrange
            var project = SetupMockProject(inputCode);
            var module = project.Object.VBComponents.Item(0).CodeModule;
            var parseResult = new RubberduckParser().Parse(project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[1].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, module.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_ClientReferencesAreUpdated_ParamArray()
        {
            //Input
            const string inputCode =
@"Sub Foo(ByVal arg1 As String, ParamArray arg2())
End Sub

Public Sub Goo(ByVal arg1 As Integer, _
               ByVal arg2 As Integer, _
               ByVal arg3 As Integer, _
               ByVal arg4 As Integer, _
               ByVal arg5 As Integer, _
               ByVal arg6 As Integer)
              
    Foo ""test"", test1x, test2x, test3x, test4x, test5x, test6x
End Sub
";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Sub Foo(ByVal arg1 As String )
End Sub

Public Sub Goo(ByVal arg1 As Integer, _
               ByVal arg2 As Integer, _
               ByVal arg3 As Integer, _
               ByVal arg4 As Integer, _
               ByVal arg5 As Integer, _
               ByVal arg6 As Integer)
              
 Foo ""test""      
End Sub
"; //note: The IDE strips out the extra whitespace, you can't see it but there are several spaces after " ParamArrayTest ""test""      "

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[1].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveLastParamFromSetter_NotAllowed()
        {
            //Input
            const string inputCode =
@"Private Property Set Foo(ByVal arg1 As Integer, ByVal arg2 As String) 
End Property";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            var model = new RemoveParametersModel(parseResult, qualifiedSelection);

            // Assert
            Assert.AreEqual(1, model.Parameters.Count); // doesn't allow removing last param from setter
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveLastParamFromLetter_NotAllowed()
        {
            //Input
            const string inputCode =
@"Private Property Let Foo(ByVal arg1 As Integer, ByVal arg2 As String) 
End Property";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            var model = new RemoveParametersModel(parseResult, qualifiedSelection);

            // Assert
            Assert.AreEqual(1, model.Parameters.Count); // doesn't allow removing last param from letter
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveFirstParamFromGetterAndSetter()
        {
            //Input
            const string inputCode =
@"Private Property Get Foo(ByVal arg1 As Integer) 
End Property

Private Property Set Foo(ByVal arg1 As Integer, ByVal arg2 As String) 
End Property";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Property Get Foo()
End Property

Private Property Set Foo( ByVal arg2 As String)
End Property"; //note: The IDE strips out the extra whitespace

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_RemoveFirstParamFromGetterAndLetter()
        {
            //Input
            const string inputCode =
@"Private Property Get Foo(ByVal arg1 As Integer) 
End Property

Private Property Let Foo(ByVal arg1 As Integer, ByVal arg2 As String) 
End Property";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Property Get Foo()
End Property

Private Property Let Foo( ByVal arg2 As String)
End Property"; //note: The IDE strips out the extra whitespace

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Param(s) to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_SignatureContainsOptionalParam()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, Optional ByVal arg2 As String)
End Sub

Private Sub Goo(ByVal arg1 As Integer)
    Foo arg1
End Sub";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo( Optional ByVal arg2 As String)
End Sub

Private Sub Goo(ByVal arg1 As Integer)
 Foo 
End Sub";

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Params to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved  = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_SignatureOnMultipleLines()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, _
                  ByVal arg2 As String, _
                  ByVal arg3 As Date)
End Sub";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo(                  ByVal arg2 As String,                  ByVal arg3 As Date)


End Sub";   // note: IDE removes excess spaces

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Params to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_PassTargetIn()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, _
                  ByVal arg2 As String, _
                  ByVal arg3 As Date)
End Sub";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo(                  ByVal arg2 As String,                  ByVal arg3 As Date)


End Sub";   // note: IDE removes excess spaces

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Params to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(model.TargetDeclaration);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        [TestMethod]
        public void RemoveParametersRefactoring_CallOnMultipleLines()
        {
            //Input
            const string inputCode =
@"Private Sub Foo(ByVal arg1 As Integer, ByVal arg2 As String, ByVal arg3 As Date)
End Sub

Private Sub Goo(ByVal arg1 as Integer, ByVal arg2 As String, ByVal arg3 As Date)

    Foo arg1, _
        arg2, _
        arg3

End Sub
";
            var selection = new Selection(1, 23, 1, 27); //startLine, startCol, endLine, endCol

            //Expectation
            const string expectedCode =
@"Private Sub Foo( ByVal arg2 As String, ByVal arg3 As Date)
End Sub

Private Sub Goo(ByVal arg1 as Integer, ByVal arg2 As String, ByVal arg3 As Date)

 Foo  arg2, arg3



End Sub
";   // note: IDE removes excess spaces

            //Arrange
            SetupProject(inputCode);
            var parseResult = new RubberduckParser().Parse(Project.Object);

            var qualifiedSelection = GetQualifiedSelection(selection);

            //Specify Params to remove
            var model = new RemoveParametersModel(parseResult, qualifiedSelection);
            model.Parameters[0].IsRemoved = true;

            //SetupFactory
            var factory = SetupFactory(model);

            //Act
            var refactoring = new RemoveParametersRefactoring(factory.Object);
            refactoring.Refactor(qualifiedSelection);

            //Assert
            Assert.AreEqual(expectedCode, Module.Object.Lines());
        }

        #region setup
        private static Mock<IRefactoringPresenterFactory<IRemoveParametersPresenter>> SetupFactory(RemoveParametersModel model)
        {
            var presenter = new Mock<IRemoveParametersPresenter>();
            presenter.Setup(p => p.Show()).Returns(model);

            var factory = new Mock<IRefactoringPresenterFactory<IRemoveParametersPresenter>>();
            factory.Setup(f => f.Create()).Returns(presenter.Object);
            return factory;
        }

        #endregion
    }
}