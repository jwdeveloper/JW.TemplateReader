using System;
using JW.TemplateReader.API.Interfaces;

namespace JW.TemplateReader.API.Implementation
{
    public class TemplateField : ITemplateField
    {
        public string Name { get; set; }
        public int Line { get; set; }
        
        public string Mark { get; set; }
        

        public TemplateField(string name)
        {
           Name = name;
        }

        public TemplateField(string name, int line, string mark) : this(name)
        {
            Line = line;
            Mark = mark;
        }
    }
}