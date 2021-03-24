using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GCore.Source.Attributes;

namespace GCore.Source.Generators
{
    public class SourceElement : IRenderable, IConfigurable
    {
        [Config("Name")]
        public string Name { get; protected set; }

        public IReadOnlyDictionary<string, string> Config { get; protected set; } = new Dictionary<string, string>();
        
        public SourceElement? Parent { get; }

        public SourceElement Root => Parent?.Root ?? this;

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

        public IEnumerable<string> GetPath()
        {
            foreach (var name in GetParents().Reverse().Select(e => e.Name))
            {
                yield return name;
            }

            yield return Name;
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

        public bool IsElementDefined<T>() where T : SourceElement
        {
            SourceElement? current = this;

            var names = new List<string>();

            while (!(current is null)) {
                foreach (var child in current.ElementChildren)
                {

                    if (!(child is T))
                        continue;

                    if (names.Contains(child.Name))
                        continue;

                    names.Add(child.Name);

                    return true;
                }

                current = current.Parent;
            }

            return false;
        }

        public bool IsElementDefinedLocally<T>() where T : SourceElement 
        {
            foreach (var child in this.ElementChildren)
                if (child is T)
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

        public SourceElement? GetElement(IEnumerable<string> path)
        {
            var p = path.ToArray();

            SourceElement e = this;

            for (int i = 0; i < p.Length; i++)
            {
                try
                {
                    e = e.ElementChildren.Where(ee => ee.Name == p[i]).First();
                }
                catch (ArgumentNullException ex)
                {
                    return null;
                }
            }

            return e;
        }

        public SourceElement? GetElement(params string[] path)
        {
            return GetElement(path);
        }

        public virtual void Configure(IReadOnlyDictionary<string, string> config) {
            Config = config;

            ConfigAttribute.ApplyAttribute(this, Config, Root.Config);
        }

        public virtual void Render(CodeWriter writer)
        {
            for (int i = 0; i < ElementChildren.Count; i++) {
                ElementChildren[i].Render(writer);
                if (i < ElementChildren.Count - 1)
                    writer.WriteLine();
            }
        }
    }
}