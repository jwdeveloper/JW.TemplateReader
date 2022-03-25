namespace JW.TemplateReader.API.Interfaces
{
    public interface ITemplateProcessor
    {
        ITemplateProcessor ClearValues();
        ITemplateProcessor SetFieldValue(string fieldName, string value);
        public string Process();
        public string ProcessAndSave(string path, string fileName);
    }
}