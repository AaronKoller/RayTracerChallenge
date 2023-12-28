using FluentAssertions;
using RayTracer.Shapes;
using Xunit;

namespace RayTracer.Tests.ShapesTests.SpheresTests
{
    public class SphereTests : _SphereTestsBase
    {
        private Sphere _resultSphere;
        private Material _material;

        [Fact]
        public void WhereADefaultSphereIsCreated_TheDefaultPropertiesAreReturned()
        {

            //When
            WhenASphereIsCreated(new Sphere());

            //Then
            ThenTheSphereHasATransformOf(new Matrix().GenerateIdentityMatrix());
            ThenTheSphereHasAMaterialOf(new Material());
            ThenTheSphereHasATransparencyOf(0.0);
            ThenTheSphereHasARefractiveIndexOf(1.0);
        }

        [Fact]
        public void WhereASphereIsCreated_ThenItIsAShape()
        {
            //Given


            //When
            WhenASphereIsCreated(new Sphere());

            //Then
            ThenTheSphereIsOfType<Shape>();

        }

        [Fact]
        public void WhereASphereMayBeAssignedAMaterial_TheMaterialIsSet()
        {
            //Given
            GivenASphere(new Sphere());
            GivenAMaterial(new Material());
            GivenAmbientIsSetTo(1);

            //When
            WhenGivenMaterialIsSetOnSphere(_sphere, _material);

            //Then
            ThenTheSameMaterialIsReturned(_material);

        }

        [Fact]
        public void WereATransformIsSetOnASphere_TheTransformIsReturned()
        {
            //Given
            GivenASphere(new Sphere());
            GivenATranslationTransformation(2, 3, 4);

            //When
            WhenASphereTransformIsSet(_sphere, _transformMatrix);

            //Then
            ThenTheTransformIsReturned(_transformMatrix);
        }

        private void GivenAmbientIsSetTo(int ambient)
        {
            _material.Ambient = ambient;
        }

        private void GivenAMaterial(Material material)
        {
            _material = material;
        }

        private void WhenASphereTransformIsSet(Sphere sphere, Matrix transformMatrix)
        {
            _resultSphere = sphere;
            _resultSphere.Transform = transformMatrix;
        }

        private void WhenGivenMaterialIsSetOnSphere(Sphere sphere, Material material)
        {
            sphere.Material = material;
            _resultSphere = sphere;
        }

        private void WhenASphereIsCreated(Sphere sphere)
        {
            _resultSphere = sphere;
        }

        private void ThenTheSphereHasARefractiveIndexOf(double refractiveIndex)
        {
            _resultSphere.Material.RefractiveIndex.Should().Be(refractiveIndex);
        }

        private void ThenTheSphereHasATransparencyOf(double transparency)
        {
            _resultSphere.Material.Transparency.Should().Be(transparency);
        }

        private void ThenTheSameMaterialIsReturned(Material material)
        {
            _resultSphere.Material.Should().Be(material);
        }

        private void ThenTheSphereIsOfType<T>()
        {
            _resultSphere.Should().BeAssignableTo<T>();
        }

        private void ThenTheSphereHasAMaterialOf(Material material)
        {
            _resultSphere.Material.Should().BeEquivalentTo(material);
        }

        private void ThenTheTransformIsReturned(Matrix transformMatrix)
        {
            _resultSphere.Transform.Should().Be(transformMatrix);
        }


        private void ThenTheSphereHasATransformOf(Matrix identityMatrix)
        {
            _resultSphere.Transform.Data.Should().BeEquivalentTo(identityMatrix.Data);
        }
    }
}
