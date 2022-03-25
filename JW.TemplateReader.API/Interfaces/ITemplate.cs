using System;
using System.Collections.Generic;

namespace JW.TemplateReader.API.Interfaces
{
    public interface ITemplate
    {
        public IList<ITemplateField> Fields { get;}
        
        public string[] Content { get; }
        
        public  IDictionary<string, IList<ITemplateField>> FieldsByName { get; }
    }
}