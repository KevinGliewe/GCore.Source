namespace GCore.Source.CodeContexts
{
    public class BracketCodeContext : ACodeContext
    {
        public BracketType BracketType { get; private set; }

        public BracketCodeContext(CodeWriter codeWriter, BracketType bracketType = BracketType.Curly) : base(codeWriter)
        {
            BracketType = bracketType;

            CodeWriter.Write(BracketType.GetChar(true));
        }

        protected override void OnEndContext()
        {
            CodeWriter.Write(BracketType.GetChar(false));
        }
    }
}