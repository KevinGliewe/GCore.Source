using System;
using System.IO;
using System.Reflection;
using GCore.Source.Attributes;
using GCore.Source.Generators.Attributes;
using GCore.Source.Helper;

namespace GCore.Source.Generators.Elements
{
    public interface IRenderObject
    {
        void Render(CodeWriter writer, SourceElement element, object? model);
    }

    [TaggedElement("Object")]
    public class ObjectElement : ScriptableElement, ITaggedElement
    {
        [Config("Type")]
        public string? Type { get; set; }

        public ObjectElement(SourceElement? parent, string name) : base(parent, name)
        {
        }

        protected override void RenderScript(CodeWriter writer)
        {
            GetRenderObject().Render(writer, this, (object?)GetModel());
        }

        protected IRenderObject GetRenderObject()
        {
            var typeName = Type ?? throw new Exception("Type is null");

            System.Type? type = TypeHelper.RequestType(typeName);

            if (type is null)
                throw new Exception("Could not find Type " + typeName);

            if(!typeof(IRenderObject).IsAssignableFrom(type))
                throw new Exception(type.FullName + " is not assignable to IRenderObject");

            return Activator.CreateInstance(type) as IRenderObject ?? throw new Exception();
        }
    }
}