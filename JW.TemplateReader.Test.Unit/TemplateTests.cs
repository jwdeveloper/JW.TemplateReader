using System;
using System.Collections.Generic;
using System.Linq;
using JW.TemplateReader.API.Implementation;
using JW.TemplateReader.API.Interfaces;
using Xunit;

namespace JW.TemplateReader.Test.Unit
{
    public class TemplateTests
    {
        [Fact]
        public void ShouldGroupFieldsByName()
        {
            //arrage
            var bananaFields = GenerateFields("Banana", 10);
            var orangeFields = GenerateFields("Orange", 6);
            var appleFields = GenerateFields("Apple", 3);

            var fields = bananaFields.Concat(orangeFields).Concat(appleFields);
            //act
            var template = new Template(fields.ToList(), Array.Empty<string>());
            
            // assert
            var argregated = template.FieldsByName;
            var keys = argregated.Keys;
            Assert.Equal(3,keys.Count);
            Assert.True(argregated.ContainsKey("Banana"));
            Assert.True(argregated.ContainsKey("Orange"));
            Assert.True(argregated.ContainsKey("Apple"));
            
            Assert.Equal(10, argregated["Banana"].Count);
            Assert.Equal(6, argregated["Orange"].Count);
            Assert.Equal(3, argregated["Apple"].Count);
        }


        private IEnumerable<ITemplateField> GenerateFields(string name, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return new TemplateField(name);
            }
        }
    }
}