using System.Collections.Generic;
using JW.TemplateReader.API.Interfaces;

namespace JW.TemplateReader.API.Implementation
{
    public class Template : ITemplate
    {
        private readonly IDictionary<string, IList<ITemplateField>> _argegatedFields;
        private readonly IList<ITemplateField> _fields;
        private readonly string[] _content;
        
        public IList<ITemplateField> Fields => _fields;
        
        public string[] Content => _content;
        public IDictionary<string, IList<ITemplateField>> FieldsByName => _argegatedFields;


        public Template(IList<ITemplateField> templateFields, string[] content)
        {
            _fields = templateFields;
            _content = content;
            _argegatedFields = GroupFieldByName(templateFields);
        }

        private Dictionary<string, IList<ITemplateField>> GroupFieldByName(IList<ITemplateField> templateFields)
        {
            var result = new Dictionary<string, IList<ITemplateField>>();
            foreach (var field in templateFields)
            {
                if (result.ContainsKey(field.Name))
                {
                    result[field.Name].Add(field);
                    continue;
                }

                result.Add(field.Name, new List<ITemplateField>
                {
                    field
                });
            }

            return result;
        }
    }
}