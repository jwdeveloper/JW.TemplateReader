using System;

namespace JW.TemplateReader.API.Interfaces
{
    public interface ITemplateLoader
    {
        public ITemplate FromFile(string path, string fileName, string marker);

        public ITemplate FromString(string content, string marker);

        public ITemplate FromString(string[] content, string marker);
    }
}