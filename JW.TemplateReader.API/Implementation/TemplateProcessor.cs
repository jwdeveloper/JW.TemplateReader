using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JW.TemplateReader.API.Interfaces;

namespace JW.TemplateReader.API.Implementation
{
    public class TemplateProcessor : ITemplateProcessor
    {
        private readonly ITemplate _template;
        private readonly IDictionary<string, string> _namesValues;

        public TemplateProcessor(ITemplate template)
        {
            _template = template;
            _namesValues = new Dictionary<string, string>();
        }

        public ITemplateProcessor SetFieldValue(string fieldName, string value)
        {
            if (!_template.FieldsByName.ContainsKey(fieldName))
                throw new Exception($"Template not contains Marker {fieldName}");
            _namesValues.TryAdd(fieldName, value);
            return this;
        }

        public ITemplateProcessor ClearValues()
        {
            _namesValues.Clear();
            return this;
        }

        public string Process()
        {
            var orginalContent = _template.Content;
            var agregated = _template.FieldsByName;
            var result = new string[orginalContent.Length];
            orginalContent.CopyTo(result, 0);
            foreach (var name in _namesValues.Keys)
            {
                var fields = agregated[name];
                var value = _namesValues[name];
                foreach (var field in fields)
                {
                    var line = result[field.Line];
                    result[field.Line] = line.Replace(field.Mark, value);
                }
            }

            return string.Join("\n", result);
        }

        public string ProcessAndSave(string path, string fileName)
        {
            var fullPath = path + "\\" + fileName;
            var proccessed = Process();
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(proccessed);
                fs.Write(info, 0, info.Length);
            }
            return proccessed;
        }
    }
}