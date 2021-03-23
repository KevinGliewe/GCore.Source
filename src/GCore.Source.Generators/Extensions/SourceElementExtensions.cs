using System;
using System.Diagnostics;

namespace GCore.Source.Generators.Extensions
{
    public static class SourceElementExtensions
    {
        public static string Render(this SourceElement @this)
        {
            var cw = new CodeWriter();
            @this.Render(cw);
            return cw.GenerateCode();
        }

        public static T Add<T>(this SourceElement @this, T element) where T : SourceElement
        {
            Debug.Assert(!@this.IsElementDefinedLocally(element.Name));
            @this.ElementChildren.Add(element);
            return element;
        }
    }
}