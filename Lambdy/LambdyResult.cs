using System.Collections.Generic;

namespace Lambdy
{
    public class LambdyResult
    {
        public string Sql { get; set; } = string.Empty;
        
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}