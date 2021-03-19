using System;
using System.Diagnostics;

namespace GCore.Source.CodeContexts
{
    public abstract class ACodeContext : IDisposable, ICodeContext
    {
        public CodeWriter CodeWriter { get; private set; }

        public ACodeContext(CodeWriter codeWriter)
        {
            CodeWriter = codeWriter;
            CodeWriter.PushContext(this);
        }

        protected abstract void OnEndContext();

        public void Dispose()
        {
            OnEndContext();

            Debug.Assert(CodeWriter.PopContext() == this);
        }
    }
}