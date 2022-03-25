namespace JW.TemplateReader.API.Interfaces
{
    public interface ITemplateField
    {
        public string Name { get; set; }
        public int Line { get; set; }
        public string Mark { get; set; }
    }
}