using System;
using System.Xml.Linq;
using FluentAssertions;
using RayTracer.Patterns;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.MaterialTests
{
    public class MaterialTests
    {
        private Material _resultMaterial;
        private Material _material;
        private Tuple _position;
        private Tuple _eyeVector;
        private Tuple _normalVector;
        private Light _light;
        private Color _resultColor;
        private bool _isShadow;
        private Pattern _pattern;
        private Shape _shape;

        [Fact]
        public void WhereAMaterialIsCreated_ItShouldHaveDefaultProperties()
        {
            //When
            WhenAMaterialIsCreated(new Material());

            //Then
            ThenTheColorIs(new Color(1, 1, 1));
            ThenTheAmbientIs(.1);
            ThenTheDiffuseIs(.9);
            ThenTheSpecular(.9);
            ThenTheShininessIs(200.00);
            ThenTheReflectiveIs(0);
        }

        [Fact]
        public void LightingWithTheEyeBetweenTheLightAndTheSurface()
        {
            //Given
            GivenAMaterial(new Material());
            GivenAPosition(Tuple.OriginPoint);
            GivenAnEyeVector(Tuple.Vector(0, 0, -1));
            GivenANormalVector(Tuple.Vector(0, 0, -1));
            GivenALightPoint(new Light(Tuple.Point(0, 0, -10), Color.White));
            GivenInShadow(false);
            GivenAShape(new Sphere());

            //When
            WhenLightingIsCalled(_shape, _material, _light, _position, _eyeVector, _normalVector, _isShadow);

            //Then
            ThenTheResultColorIs(new Color(1.9, 1.9, 1.9));
        }

        [Fact]
        public void LightingWithTheSurfaceInShadow()
        {
            //Given
            GivenAMaterial(new Material());
            GivenAPosition(Tuple.OriginPoint);
            GivenAnEyeVector(Tuple.Vector(0, 0, -1));
            GivenANormalVector(Tuple.Vector(0, 0, -1));
            GivenALightPoint(new Light(Tuple.Point(0, 0, -10), Color.White));
            GivenInShadow(true);
            GivenAShape(new Sphere());

            //When
            WhenLightingIsCalled(_shape, _material, _light, _position, _eyeVector, _normalVector, _isShadow);

            //Then
            ThenTheResultColorIs(new Color(.1, .1, .1));
        }

        [Fact]
        public void LightingWithTheEyeBetweenLightAndTheSurfaceEyeOffsetBy45Degrees()
        {
            var number = Math.Sqrt(2) / 2;
            //Given
            GivenAMaterial(new Material());
            GivenAPosition(Tuple.OriginPoint);
            GivenAnEyeVector(Tuple.Vector(0, number, number));
            GivenANormalVector(Tuple.Vector(0, 0, -1));
            GivenALightPoint(new Light(Tuple.Point(0, 0, -10), new Color(1, 1, 1)));
            GivenInShadow(false);
            GivenAShape(new Sphere());

            //When
            WhenLightingIsCalled(_shape, _material, _light, _position, _eyeVector, _normalVector, _isShadow);

            //Then
            ThenTheResultColorIs(new Color(1, 1, 1));
        }

        [Fact]
        public void LightingWithEyeOppositeSurfaceLightOffset45degrees()
        {
            var number = .7364;
            //Given
            GivenAMaterial(new Material());
            GivenAPosition(Tuple.OriginPoint);
            GivenAnEyeVector(Tuple.Vector(0, 0, -1));
            GivenANormalVector(Tuple.Vector(0, 0, -1));
            GivenALightPoint(new Light(Tuple.Point(0, 10, -10), new Color(1, 1, 1)));
            GivenInShadow(false);
            GivenAShape(new Sphere());

            //When
            WhenLightingIsCalled(_shape, _material, _light, _position, _eyeVector, _normalVector, _isShadow);

            //Then
            ThenTheResultColorIsToPrecision(.00001, new Color(number, number, number));
        }

        [Fact]
        public void LightingWithEyeInThePathOfTheReflectionVector()
        {

            var number = Math.Sqrt(2) / 2;
            var number2 = 1.6364;

            //Given
            GivenAMaterial(new Material());
            GivenAPosition(Tuple.OriginPoint);
            GivenAnEyeVector(Tuple.Vector(0, -number, -number));
            GivenANormalVector(Tuple.Vector(0, 0, -1));
            GivenALightPoint(new Light(Tuple.Point(0, 10, -10), new Color(1, 1, 1)));
            GivenInShadow(false);
            GivenAShape(new Sphere());

            //When
            WhenLightingIsCalled(_shape, _material, _light, _position, _eyeVector, _normalVector, _isShadow);

            //Then
            ThenTheResultColorIsToPrecision(.01, new Color(number2, number2, number2));
        }

        [Fact]
        public void LightingWihTheLightBehindTheSurface()
        {
            //Given
            GivenAMaterial(new Material());
            GivenAPosition(Tuple.OriginPoint);
            GivenAnEyeVector(Tuple.Vector(0, 0, -1));
            GivenANormalVector(Tuple.Vector(0, 0, -1));
            GivenALightPoint(new Light(Tuple.Point(0, 0, 10), new Color(1, 1, 1)));
            GivenInShadow(false);
            GivenAShape(new Sphere());

            //When
            WhenLightingIsCalled(_shape, _material, _light, _position, _eyeVector, _normalVector, _isShadow);

            //Then
            ThenTheResultColorIs(new Color(.1, .1, .1));
        }

        [Theory]
        [InlineData( .9,0,0,1,1,1)]
        [InlineData(1.1,0,0,0,0,0)]
        public void WhereLightingWithAPatternIsApplied(double pX, double pY, double pZ, int red, int green, int blue)
        {
            //Given
            GivenAMaterial(new Material());
            GivenAPatternOnMaterial(_material, new StripePattern(Color.White, Color.Black));
            GivenAnAmbience(_material, 1);
            GivenAnDiffuse(_material, 0);
            GivenAnSpecular(_material, 0);
            GivenAnEyeVector(Tuple.Vector(0, 0, -1));
            GivenANormalVector(Tuple.Vector(0, 0, -1));
            GivenALightPoint(new Light(Tuple.Point(0, 0, -10), Color.White));
            GivenAPosition(Tuple.Point(pX, pY, pZ));
            GivenInShadow(false);
            GivenAShape(new Sphere());

            //When
            WhenLightingIsCalled(_shape, _material, _light, _position, _eyeVector, _normalVector, _isShadow);

            //Then
            ThenTheResultColorIs(new Color(red, green, blue));
        }

        private void GivenAShape(Shape shape)
        {
            _shape = shape;
        }

        private void GivenAnAmbience(Material material, int ambient)
        {
            material.Ambient = ambient;
        }

        private void GivenAnDiffuse(Material material, int diffuse)
        {
            material.Diffuse = diffuse;
        }

        private void GivenAnSpecular(Material material, int specular)
        {
            material.Specular = specular;
        }

        private void GivenAPatternOnMaterial(Material material, StripePattern pattern)
        {
            _pattern = pattern;
            material.Pattern = pattern;
        }


        private void GivenInShadow(bool isShadow)
        {
            _isShadow = isShadow;
        }


        private void GivenALightPoint(Light light)
        {
            _light = light;
        }

        private void GivenANormalVector(Tuple vector)
        {
            _normalVector = vector;
        }

        private void GivenAnEyeVector(Tuple vector)
        {
            _eyeVector = vector;
        }

        private void GivenAPosition(Tuple originPoint)
        {
            _position = originPoint;
        }

        private void GivenAMaterial(Material material)
        {
            _material = material;
        }

        private void WhenLightingIsCalled(Shape shape, Material material, Light light, Tuple position, Tuple eyeVector, Tuple normalVector, bool isShadow)
        {
            _resultColor = material.Lighting(shape, light, position, eyeVector, normalVector, isShadow);
        }

        private void WhenAMaterialIsCreated(RayTracer.Material material)
        {
            _resultMaterial = material;
        }

        private void ThenTheColorIs(Color color)
        {
            _resultMaterial.Color.Should().BeEquivalentTo(color);
        }

        private void ThenTheAmbientIs(double ambient)
        {
            _resultMaterial.Ambient.Should().Be(ambient);
        }

        private void ThenTheDiffuseIs(double diffuse)
        {
            _resultMaterial.Diffuse.Should().Be(diffuse);
        }

        private void ThenTheSpecular(double specular)
        {
            _resultMaterial.Specular.Should().Be(specular);
        }

        private void ThenTheReflectiveIs(int reflective)
        {
            _resultMaterial.Reflective.Should().Be(reflective);
        }

        private void ThenTheShininessIs(double shininess)
        {
            _resultMaterial.Shininess.Should().Be(shininess);
        }

        private void ThenTheResultColorIs(Color color)
        {
            _resultColor.Should().BeEquivalentTo(color);
        }

        protected void ThenTheResultColorIsToPrecision(double precision, Color color)
        {
            _resultColor.Should().BeEquivalentTo(color, option =>
                option.Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, precision))
                    .WhenTypeIs<double>());
        }
    }
}
