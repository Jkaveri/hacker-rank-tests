using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace GoingToTheOffice
{
    public class UnitTest1
    {
        [Theory]
        [ClassData(typeof(TestCasesGenerator))]
        public void Main()
        {
            
        }

        public class TestCasesGenerator : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        }
    }
}
