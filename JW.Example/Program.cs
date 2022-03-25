using System;
using JW.TemplateReader.API.Implementation;

namespace JW.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //Run application to generate out put files based on templates\EntityTemplate.txt
            var basePath = System.IO.Path.GetFullPath(@"..\..\..\");
            var inputPath = basePath + @"templates\input";
            var outputPath = basePath + @"templates\output";

            var template = new TemplateLoader().FromFile(inputPath, "EntityTemplate.txt", "%");
            var processor = new TemplateProcessor(template);

            var classesToGenerate = new[] {"User", "Order", "Product"};
            foreach (var className in classesToGenerate)
            {
                processor
                    .ClearValues()
                    .SetFieldValue("ENTITY", className)
                    .SetFieldValue("TYPE", "int")
                    .SetFieldValue("TABLE", $"{className}s")
                    .SetFieldValue("NAMESPACE",$"this.is.{className}.namespace")
                    .ProcessAndSave(outputPath, $"{className}Entity.cs");
            }
        }
    }
}