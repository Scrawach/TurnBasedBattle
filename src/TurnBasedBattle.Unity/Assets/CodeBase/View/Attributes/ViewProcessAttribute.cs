using System;

namespace CodeBase.View.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ViewProcessAttribute: Attribute
    {
        public ViewProcessAttribute(Type type) =>
            Type = type;
        
        public Type Type { get; private set; }
    }
}