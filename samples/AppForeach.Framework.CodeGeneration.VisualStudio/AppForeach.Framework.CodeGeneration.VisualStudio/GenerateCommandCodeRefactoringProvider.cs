using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace AppForeach.Framework.CodeGeneration.VisualStudio
{
    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(GenerateCommandCodeRefactoringProvider)), Shared]
    internal class GenerateCommandCodeRefactoringProvider : CodeRefactoringProvider
    {
        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var node = root.FindNode(context.Span);

            var typeDeclaration = node as TypeDeclarationSyntax;
            if (typeDeclaration == null)
            {
                return;
            }

            if(!typeDeclaration.Identifier.ValueText.EndsWith("Command"))
            {
                return;
            }

            var action = CodeAction.Create("Generate command", c => GenerateCommandCodeFix(context.Document, typeDeclaration, c));

            context.RegisterRefactoring(action);
        }

        private async Task<Solution> GenerateCommandCodeFix(Document document, TypeDeclarationSyntax typeDeclaration, CancellationToken cancellationToken)
        {
            var identifierToken = typeDeclaration.Identifier;

            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var typeSymbol = semanticModel.GetDeclaredSymbol(typeDeclaration, cancellationToken);

            string typeName = typeSymbol.Name;
            string namespaceName = string.Join(".", GetNamespaces(typeSymbol));
            var originalSolution = document.Project.Solution;

            var files = CommandGenerator.GenerateFiles(typeName, namespaceName);

            var newSolution = originalSolution;

            if (files.Length > 0)
            {
                newSolution = newSolution.WithDocumentText(document.Id, SourceText.From(files[0].FileText));
            }

            for (int i = 1; i < files.Length; i++)
            {
                var documentId = DocumentId.CreateNewId(document.Project.Id);
                newSolution = newSolution.AddDocument(documentId, files[i].FileName, files[i].FileText, document.Folders);
            }

            return newSolution;
        }

        public static IEnumerable<string> GetNamespaces(INamedTypeSymbol symbol)
        {
            var current = symbol.ContainingNamespace;
            while (current != null)
            {
                if (current.IsGlobalNamespace)
                    break;
                yield return current.Name;
                current = current.ContainingNamespace;
            }
        }
    }
}
