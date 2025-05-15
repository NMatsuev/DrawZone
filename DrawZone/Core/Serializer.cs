using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using DrawZone.Shapes;
using Newtonsoft.Json;

namespace DrawZone.Core
{
    public static class Serializer
    {
        public static void SerializeShapesToFile(List<CustomShape> shapes, string filePath)
        {
            var serializedShapes = shapes.Select(shape => new
            {
                Type = shape.GetType().Name,  
                Data = shape.Serialize()      
            }).ToList();

            string json = JsonConvert.SerializeObject(serializedShapes, Formatting.Indented);

            File.WriteAllText(filePath, json);
        }
    }
}
