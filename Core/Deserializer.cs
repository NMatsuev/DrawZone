using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DrawZone.Factories;
using DrawZone.Shapes;
using Newtonsoft.Json;

namespace DrawZone.Core
{
    public static class Deserializer
    {

        public static List<CustomShape> DeserializeShapesFromFile(Dictionary<string, ConstructorInfo> shapeConstructors, string filePath)
        {
            string json = File.ReadAllText(filePath);

            var shapeRecords = JsonConvert.DeserializeObject<List<dynamic>>(json);

            var shapes = new List<CustomShape>();

            foreach (var record in shapeRecords)
            {
                string typeName = record.Type.ToString();

                CustomShape shape = ShapeFactory.CreateShapeInstance(shapeConstructors, typeName);

                shape.Deserialize(record.Data.ToString());

                shapes.Add(shape);
            }

            return shapes;
        }
    }
}
