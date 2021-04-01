namespace GCore.Source.CodeContexts
{
    public class AbsoluteIndentContext : ACodeContext
    {
        private int _previousIndent;

        public AbsoluteIndentContext(CodeWriter codeWriter, int indent = int.MinValue) : base(codeWriter)
        {
            _previousIndent = codeWriter.CurrentIndent;

            if (indent != int.MinValue)
                codeWriter.CurrentIndent = indent;
        }

        protected override void OnEndContext()
        {
            CodeWriter.CurrentIndent = _previousIndent;
        }
    }
}