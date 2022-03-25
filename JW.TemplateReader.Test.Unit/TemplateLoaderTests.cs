

using System;
using System.Linq;
using JW.TemplateRider.API.Implementation;
using JW.TemplateRider.API.Interfaces;
using Xunit;

namespace JW.TemplateReader.Test.Unit
{
    public class TemplateLoaderTests
    {
        private ITemplateLoader _templateLoader;
        
        public TemplateLoaderTests()
        {
            _templateLoader = new TemplateLoader();
        }

        [Fact]
        public void ShouldReadLine()
        {
            //Arrage
            var marker = "%";
            var markerName = "NAME";
            var exampleString = $"my name is {marker}{markerName}{marker}";
            
            //Act
            var template = _templateLoader.FromString(exampleString, marker);
            
            //Assert 
            Assert.NotNull(template);
            var fields = template.Fields;
            Assert.NotEmpty(fields);
            var nameField = fields.FirstOrDefault();
            Assert.NotNull(nameField);
            Assert.Equal(nameField.Name,markerName);
        }
        
        
        [Fact]
        public void ShouldReadLineWithMany()
        {
            //Arrage
            var marker = "%";
            var markerName = "NAME";
            var markerAge = "AGE";
            var exampleString = $"{marker}{markerName}{marker} my name is {marker}{markerName}{marker} and {marker}{markerAge}{marker}";
            
            //Act
            var template = _templateLoader.FromString(exampleString, marker);
            
            //Assert 
            Assert.NotNull(template);
            var fields = template.Fields;
            Assert.Equal(3,fields.Count);
            var agregated = template.FieldsByName;
            Assert.NotNull(agregated["NAME"]);
            Assert.NotNull(agregated["AGE"]);
            
            Assert.Equal(2,agregated["NAME"].Count);
            Assert.Equal(1,agregated["AGE"].Count);
        }
        
        [Fact]
        public void ShouldThrowException()
        {
            //Arrage
            var marker = "%";
            var markerName = "NAME";
            var exampleString = $"my name is {marker}{markerName}";
            
            //Act
            Assert.Throws<Exception>(() => _templateLoader.FromString(exampleString, marker));
        }
        
        [Fact]
        public void ShouldLoadFromFile()
        {
            
        }
        
    }
}