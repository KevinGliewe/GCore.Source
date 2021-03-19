namespace GCore.Source.Generators.CSharp.Properties.Variables
{
    public class CSharpFunctionArgument : CSharpVariable
    {
        public CSharpVariable Variable { get; set; }

        public CSharpFunctionArgument(CSharpVariable variable) 
            : base(variable.Name, variable.DataType, variable.Initialisation)
        {
            Variable = variable;
        }

        public override void Render(CodeWriter writer)
        {
            Variable.Render(writer);
        }
    }
}