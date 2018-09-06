using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace DiagonalDifference
{
    public class MainTest
    {
        [Theory()]
        [ClassData(@class: typeof(TestCase1))]
        public void DiagonalDIfference(int[][] arr, int expected)
        {
            var n = arr.Length;
            var left = 0;
            var right = 0;
            var j = n - 1;
            for (var i = 0; i < n; i++, j--)
            {
                left += arr[i][i];
                right += arr[i][j];
            }
            var difference = Math.Abs(left - right);

            Assert.Equal(difference, expected);
        }
    }

    public class TestCase1 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var input =new int[][] {
                new[] { 11, 2, 4 },
                new[] { 4, 5, 6 },
                new[] { 10, 8, -12 }
            };
            var output = 15;

            yield return new object[]
            {
                input, output
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
