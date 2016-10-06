﻿using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using NLog;
using Rubberduck.Parsing.Annotations;
using Rubberduck.Parsing.Grammar;
using Rubberduck.Parsing.Preprocessing;
using Rubberduck.Parsing.Symbols;
using Rubberduck.VBEditor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace Rubberduck.Parsing.VBA
{
    class ComponentParseTask
    {
        private readonly IVBComponent _component;
        private readonly QualifiedModuleName _qualifiedName;
        private readonly TokenStreamRewriter _rewriter;
        private readonly IAttributeParser _attributeParser;
        private readonly IVBAPreprocessor _preprocessor;
        private readonly VBAModuleParser _parser;

        public event EventHandler<ParseCompletionArgs> ParseCompleted;
        public event EventHandler<ParseFailureArgs> ParseFailure;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ComponentParseTask(IVBComponent vbComponent, IVBAPreprocessor preprocessor, IAttributeParser attributeParser, TokenStreamRewriter rewriter = null)
        {
            _attributeParser = attributeParser;
            _preprocessor = preprocessor;
            _component = vbComponent;
            _rewriter = rewriter;
            _qualifiedName = new QualifiedModuleName(vbComponent);
            _parser = new VBAModuleParser();
        }
        
        public void Start(CancellationToken token)
        {
            try
            {
                var code = RewriteAndPreprocess();
                token.ThrowIfCancellationRequested();

                var attributes = _attributeParser.Parse(_component);

                token.ThrowIfCancellationRequested();

                // temporal coupling... comments must be acquired before we walk the parse tree for declarations
                // otherwise none of the annotations get associated to their respective Declaration
                var commentListener = new CommentListener();
                var annotationListener = new AnnotationListener(new VBAParserAnnotationFactory(), _qualifiedName);

                var stopwatch = Stopwatch.StartNew();
                ITokenStream stream;
                var tree = ParseInternal(_component.Name, code, new IParseTreeListener[]{ commentListener, annotationListener }, out stream);
                stopwatch.Stop();

                var comments = QualifyAndUnionComments(_qualifiedName, commentListener.Comments, commentListener.RemComments);
                token.ThrowIfCancellationRequested();

                ParseCompleted.Invoke(this, new ParseCompletionArgs
                {
                    ParseTree = tree,
                    Tokens = stream,
                    Attributes = attributes,
                    Comments = comments,
                    Annotations = annotationListener.Annotations
                });
            }
            catch (COMException exception)
            {
                Logger.Error(exception, "Exception thrown in thread {0}.", Thread.CurrentThread.ManagedThreadId);
                ParseFailure.Invoke(this, new ParseFailureArgs
                {
                    Cause = exception
                });
            }
            catch (SyntaxErrorException exception)
            {
                Logger.Error(exception, "Exception thrown in thread {0}.", Thread.CurrentThread.ManagedThreadId);
                ParseFailure.Invoke(this, new ParseFailureArgs
                {
                    Cause = exception
                });
            }
            catch (OperationCanceledException)
            {
                // no results to be used, so no results "returned"
            }
        }

        private string RewriteAndPreprocess()
        {
            var code = _rewriter == null ? string.Join(Environment.NewLine, _component.CodeModule.GetSanitizedCode()) : _rewriter.GetText();
            var processed = _preprocessor.Execute(_component.Name, code);
            return processed;
        }

        private IParseTree ParseInternal(string moduleName, string code, IParseTreeListener[] listeners, out ITokenStream outStream)
        {
            return _parser.Parse(moduleName, code, listeners, out outStream);
        }

        private IEnumerable<CommentNode> QualifyAndUnionComments(QualifiedModuleName qualifiedName, IEnumerable<VBAParser.CommentContext> comments, IEnumerable<VBAParser.RemCommentContext> remComments)
        {
            var commentNodes = comments.Select(comment => new CommentNode(comment.GetComment(), Tokens.CommentMarker, new QualifiedSelection(qualifiedName, comment.GetSelection())));
            var remCommentNodes = remComments.Select(comment => new CommentNode(comment.GetComment(), Tokens.Rem, new QualifiedSelection(qualifiedName, comment.GetSelection())));
            var allCommentNodes = commentNodes.Union(remCommentNodes);
            return allCommentNodes;
        }
        
        public class ParseCompletionArgs
        {
            public ITokenStream Tokens { get; internal set; }
            public IParseTree ParseTree { get; internal set; }
            public IDictionary<Tuple<string, DeclarationType>, Attributes> Attributes { get; internal set; }
            public IEnumerable<CommentNode> Comments { get; internal set; }
            public IEnumerable<IAnnotation> Annotations { get; internal set; }
        }

        public class ParseFailureArgs
        {
            public Exception Cause { get; internal set; }
        }

        private class CommentListener : VBAParserBaseListener
        {
            private readonly IList<VBAParser.RemCommentContext> _remComments = new List<VBAParser.RemCommentContext>();
            public IEnumerable<VBAParser.RemCommentContext> RemComments { get { return _remComments; } }

            private readonly IList<VBAParser.CommentContext> _comments = new List<VBAParser.CommentContext>();
            public IEnumerable<VBAParser.CommentContext> Comments { get { return _comments; } }

            public override void ExitRemComment([NotNull] VBAParser.RemCommentContext context)
            {
                _remComments.Add(context);
            }

            public override void ExitComment([NotNull] VBAParser.CommentContext context)
            {
                _comments.Add(context);
            }
        }
    }
}
