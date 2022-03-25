using System;
using JW.TemplateReader.API.Implementation;
using JW.TemplateReader.API.Interfaces;
using Xunit;

namespace JW.TemplateReader.Test.Unit
{
    public class TemplateProcessorTests
    {
        [Fact]
        public void ShouldProcessLine()
        {
            //Arrage
            var exampleString = $"my name is $NAME$ and age is $AGE$";
            var processor = CreateProcessor("$",exampleString);
            //Act

            var result = processor
                .SetFieldValue("NAME", "John")
                .SetFieldValue("AGE", "12")
                .Process();

            //Assert 
            Assert.Equal("my name is John and age is 12",result);
        }
        
        [Fact]
        public void ShouldProcessLines()
        {
            //Arrage
            var line1 = $"my name is $NAME$ and age is $AGE$";
            var line2 = $"my best number is $AGE$";
            var processor = CreateProcessor("$",line1, line2);
            //Act

            var result = processor
                .SetFieldValue("NAME", "John")
                .SetFieldValue("AGE", "12")
                .Process();
            var lines = result.Split("\n");

            //Assert 
            Assert.Equal("my name is John and age is 12",lines[0]);
            Assert.Equal("my best number is 12",lines[1]);
        }
        
        [Fact]
        public void ShouldTrowExceptionIfFileNameNotExists()
        {
            //Arrage
            var line1 = $"my name is $NAME$ and age is $AGE$";
            var processor = CreateProcessor("$",line1);
          
            //Assert 
            Assert.Throws<Exception>(() => processor
                .SetFieldValue("SomeField", "John")
                .Process());
        }


        private ITemplateProcessor CreateProcessor(string mark, params string[] inputString)
        {
            var loader = new TemplateLoader();
            var template = loader.FromString(inputString, mark);
            return new TemplateProcessor(template);
        }
    }
}