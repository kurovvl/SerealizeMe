using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serealizer.Test
{
    public class TestClass
    {
        public int i1 { get; set; }
        public int i2 { get; set; }
        public int i3 { get; set; }
        public int i4 { get; set; }
        public int i5 { get; set; }

        public static TestClass Get()
        {
            return new TestClass()
            {
                i1 = 1,
                i2 = 2,
                i3 = 3,
                i4 = 4,
                i5 = 5,
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is TestClass f &&
                   i1 == f.i1 &&
                   i2 == f.i2 &&
                   i3 == f.i3 &&
                   i4 == f.i4 &&
                   i5 == f.i5;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(i1, i2, i3, i4, i5);
        }
    }


}
