namespace GCore.Source.CodeContexts
{
    public class IndentContext : ACodeContext
    {
        private int _indent;

        public IndentContext(CodeWriter codeWriter, int indent = 4) : base(codeWriter)
        {
            _indent = indent;

            CodeWriter.CurrentIndent += _indent;
        }

        protected override void OnEndContext()
        {
            CodeWriter.CurrentIndent -= _indent;
        }
    }
}