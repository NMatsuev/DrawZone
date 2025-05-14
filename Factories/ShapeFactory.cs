using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DrawZone.Shapes;

namespace DrawZone.Factories
{
    public static class ShapeFactory
    {
        public static Dictionary<string, ConstructorInfo> GetShapeConstructors()
        {
            var constructors = new Dictionary<string, ConstructorInfo>();
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && type.Namespace == "DrawZone.Shapes")
                {
                    string shapeName = type.Name;
                    ConstructorInfo constructor = type.GetConstructors().FirstOrDefault();
                    if (constructor != null)
                    {
                        constructors[shapeName] = constructor;
                    }
                }
            }

            return constructors;
        }

        public static CustomShape? CreateShapeInstance(Dictionary<string, ConstructorInfo> constructors, string shapeName, object[] parameters)
        {
            if (constructors.TryGetValue(shapeName, out ConstructorInfo? constructor) && constructor != null)
            {
                   return (CustomShape)constructor.Invoke(parameters);
            }
            throw new ArgumentException($"Shape '{shapeName}' not found or has no valid constructor.");
        }

        public static CustomShape CreateShapeInstance(Dictionary<string, ConstructorInfo> constructors, string shapeName)
        {
            if (constructors.TryGetValue(shapeName, out ConstructorInfo constructor))
            {
                var emptyConstructor = constructor.DeclaringType.GetConstructor(Type.EmptyTypes);
                if (emptyConstructor != null)
                {
                    return (CustomShape)emptyConstructor.Invoke(null);
                }
            }
            throw new ArgumentException($"Shape '{shapeName}' не имеет конструктора по умолчанию");
        }
    }
}
