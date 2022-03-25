# Template Reader
#### This tool could be use for C# code generation or some other custom purpose. I guess that's it 🙃

### Example template 
EntityTemplate.txt
```  
using System.ComponentModel.DataAnnotations.Schema;

namespace %NAMESPACE%
{
    [Table("%TABLE%")]
    public class %ENTITY% : IEntityModel<%TYPE%>
    {
        public %TYPE% Id { get; set; }
    }
}
```

### Code 

``` C#

        public static void Main(string[] args)
        {
            var inputPath = "C:\\templates\input";
            var outputPath =  "C:\\templates\output";

            var template = new TemplateLoader().FromFile(inputPath, "EntityTemplate.txt", "%");
            var processor = new TemplateProcessor(template);

            var classesToGenerate = new[] {"User"};
            foreach (var className in classesToGenerate)
            {
                processor
                    .ClearValues()
                    .SetFieldValue("ENTITY", className)
                    .SetFieldValue("TYPE", "int")
                    .SetFieldValue("TABLE", $"{className}s")
                    .SetFieldValue("NAMESPACE",$"this.is.{className}.namespace")
                    .ProcessAndSave(outputPath, $"{className}Entity.cs");
            }
        }

```

### Output file
UserEntity.cs
```
using System.ComponentModel.DataAnnotations.Schema;

namespace this.is.Users.namespace
{
    [Table("Users")]
    public class User : IEntityModel<int>
    {
        public int Id { get; set; }
    }
}
```
