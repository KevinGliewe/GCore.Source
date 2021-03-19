namespace GCore.Source.CodeContexts
{
    public class BracketIndentCodeContext : ACodeContext
    {
        private int _indent;
        private BracketType _bracketType;

        public BracketIndentCodeContext(CodeWriter codeWriter, int indent = 4, BracketType bracketType = BracketType.Curly) 
            : base(codeWriter)
        {
            _indent = indent;
            _bracketType = bracketType;

            CodeWriter.Write(_bracketType.GetChar(true));
            CodeWriter.CurrentIndent += _indent;
            CodeWriter.WriteLine();
        }

        protected override void OnEndContext()
        {
            CodeWriter.WriteLine();
            CodeWriter.CurrentIndent -= _indent;
            CodeWriter.Write(_bracketType.GetChar(false));
        }
    }
}