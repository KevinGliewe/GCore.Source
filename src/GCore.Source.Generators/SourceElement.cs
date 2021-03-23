using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GCore.Source.Generators
{
    public abstract class SourceElement : IRenderable
    {

        public SourceElement? Parent { get; }
        
        public string Name { get; }

#region Elements
        public IList<SourceElement> ElementChildren { get; } = new List<SourceElement>();


        public IEnumerable<SourceElement> GetElements()
        {
            SourceElement? current = this;

            var names = new List<string>();

            while (!(current is null))
            {
                foreach (var child in current.ElementChildren)
                {
                    if(names.Contains(child.Name))
                        continue;

                    names.Add(child.Name);

                    yield return child;
                }

                current = current.Parent;
            }
        }

        public T? GetElement<T>(string name) where T : SourceElement {

            SourceElement? current = this;

            var names = new List<string>();

            while (!(current is null)) {
                foreach (SourceElement child in current.ElementChildren)
                    if (child.Name == name && child is T element)
                        return element;

                current = current.Parent;
            }

            return null;
        }

        public T? GetElementLocally<T>(string name) where T : SourceElement
        {

            foreach (SourceElement child in this.ElementChildren)
                if (child.Name == name && child is T element)
                    return element;

            return null;
        }

        public IEnumerable<T> GetElements<T>() where T : SourceElement
        {
            return this.GetElements().OfType<T>();
        }

        public IEnumerable<T> GetElementsLocally<T>() where T : SourceElement
        {
            return this.ElementChildren.OfType<T>();
        }

        public IEnumerable<SourceElement> GetParents()
        {
            SourceElement? current = Parent;

            while (!(current is null))
            {
                yield return current;
                current = current.Parent;
            }
        }

        public bool IsElementDefined(string name)
        {
            SourceElement? current = this;

            var names = new List<string>();

            while (!(current is null)) {
                foreach (SourceElement child in current.ElementChildren)
                    if (child.Name == name)
                        return true;

                current = current.Parent;
            }

            return false;
        }

        public bool IsElementDefinedLocally(string name)
        {
            foreach (var child in this.ElementChildren)
                if (child.Name == name)
                    return true;
            
            return false;
        }

#endregion

        public SourceElement(SourceElement? parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual void Render(CodeWriter writer)
        {
            foreach (var child in ElementChildren)
            {
                child.Render(writer);
            }
        }

    }
}