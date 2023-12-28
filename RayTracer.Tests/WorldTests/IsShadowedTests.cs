using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace RayTracer.Tests.WorldTests
{
    public class IsShadowedTests : _WorldTestsBase
    {
        private Tuple _point;
        private bool _isShadowed;

        [Theory]
        [InlineData(0,10,0, false)] //There is no shadow when there is nothing collinear with the point and light
        [InlineData(10,-10,10, true)] // The shadow when an object is between the point and the light
        [InlineData(-20,20,-20,false)] //There is now shadow when an object is behind the light
        [InlineData(-2,2,-2,false)] // There is no shadow when an object is behind the point
        public void WhenIsShadowedIsCalled_ThenThePointIsInTheShadowOrInTheLight(double x, double y, double z, bool isShadowed)
        {
            //Given
            GivenAWorld(new World());
            GivenAPoint(Tuple.Point(x,y,z));

            //When
            WhenIsShadowedIsCalled();

            //Then
            ThenIsShadowedIs(isShadowed);
        }

        private void ThenIsShadowedIs(bool isShadowed)
        {
            _isShadowed.Should().Be(isShadowed);
        }

        private void WhenIsShadowedIsCalled()
        {
            _isShadowed = _world.IsShadowed(_point);
        }

        private void GivenAPoint(Tuple point)
        {
            _point = point;
        }
    }
}
