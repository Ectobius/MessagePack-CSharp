using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenchmarkCustomized
{
    public class ModelGeneration
    {
        public static string GenerateName(int length)
        {
            return string.Join("", Enumerable.Range(0, length).Select(i => 'a'));
        }
    }
}
