using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using DrawZone.Shapes;
using DrawZone.Controls;
using System.Xml.Linq;

namespace DrawZone.Core
{
    public static class PluginLoader
    {

        public static void LoadPluginFromFile(string pluginFile, 
                                              WrapPanel shapePanel, 
                                              Dictionary<string, ConstructorInfo> constructors, 
                                              RoutedEventHandler buttonClickHandler)
        {
            try
            {
                Assembly pluginAssembly = Assembly.LoadFrom(pluginFile);

                var shapeClasses = pluginAssembly.GetTypes()
                    .Where(t => typeof(CustomShape).IsAssignableFrom(t) && !t.IsAbstract)
                    .ToList();

                foreach (var shapeClassType in shapeClasses)
                {
                    if (!constructors.ContainsKey(shapeClassType.Name))
                    {
                        var button = new CustomShapeButton("Plugin", shapeClassType.Name, buttonClickHandler);
                        shapePanel.Children.Add(button);
                        ConstructorInfo constructor = shapeClassType.GetConstructors().FirstOrDefault();
                        if (constructor != null)
                        {
                            constructors[shapeClassType.Name] = constructor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when uploading {pluginFile}: {ex.Message}");
            }
        }
    }
}
