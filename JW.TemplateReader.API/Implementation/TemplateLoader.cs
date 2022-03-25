using System;
using System.Collections.Generic;
using System.IO;
using JW.TemplateReader.API.Interfaces;

namespace JW.TemplateReader.API.Implementation
{
    public class TemplateLoader : ITemplateLoader
    {
        public ITemplate FromFile(string path,string fileName, string marker)
        {
            path = path + "\\" + fileName;
            var lines = File.ReadAllLines(path);
            return FromString(lines, marker);
        }

        public ITemplate FromString(string content, string marker)
        {
            var lines = content.Split("\n");
            return FromString(lines, marker);
        }

        public ITemplate FromString(string[] content, string marker)
        {
            var fields = new List<ITemplateField>();
            for (int i = 0; i < content.Length; i++)
            {
                fields.AddRange(ReadLine(content[i], i, marker));
            }
            return new Template(fields, content);
        }

        private IEnumerable<ITemplateField> ReadLine(string line, int lineIndex, string marker)
        {
            var startIndex = 0;
            var closeIndex = 0;
            while (startIndex < line.Length)
            {
                startIndex = line.IndexOf(marker, startIndex);
                if (startIndex == -1)
                    yield break;
                
                closeIndex = line.IndexOf(marker, startIndex+1);
                if (closeIndex == -1)
                {
                    throw new Exception($"Section is not closed at line {lineIndex}: {line} ");
                }

                var name = line.Substring(startIndex+1, closeIndex - startIndex-1);
                yield return new TemplateField(name, lineIndex, marker+name+marker);
                startIndex = closeIndex + 1;
            }
        }
    }
}