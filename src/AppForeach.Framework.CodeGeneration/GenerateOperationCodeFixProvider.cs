using System;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AppForeach.Framework.CodeGeneration
{
    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(GenerateOperationCodeFixProvider)), Shared]
    public class GenerateOperationCodeFixProvider : CodeRefactoringProvider
    {
        public override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var node = root.FindNode(context.Span);

            var typeDecl = node as TypeDeclarationSyntax;
            if (typeDecl == null)
            {
                return;
            }

            var action = CodeAction.Create("Generate command", ct => GenerateFileAsync(context.Document, typeDecl, ct));

            context.RegisterRefactoring(action);
        }

        private Task<Document> GenerateFileAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken c)
        {
            throw new NotImplementedException();
        }
    }
}
