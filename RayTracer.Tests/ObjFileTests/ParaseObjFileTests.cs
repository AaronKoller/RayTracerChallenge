using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RayTracer.Tests.ObjFileTests
{
    public class ParaseObjFileTests
    {

        [Fact]
        public void IgnoringUnrecognizedLines()
        {
            //Given
            GivenAFileContaining(@"There was a young lady named Bright
who traveled much faster than light.
She set out one day 
in a relative way, 
and came back the previous night.");

            //When
            WhenParseObjFileIscalled();

            //Then
            // ThenTh
            //ReadsAmazon6886
        }

        private void WhenParseObjFileIscalled()
        {
            throw new NotImplementedException();
        }

        private void GivenAFileContaining(string empty)
        {
            throw new NotImplementedException();
        }
    }
}
