using System;
using RayTracer.Shapes;
using RayTracer.Transformations;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RayTracer.Tests.WorldTests
{
    #region New region

    public class ShadeHitTests : _WorldTestsBase
    {
        private Shape _shape2;
        private double _root2 = Math.Sqrt(2);
        private Intersections _intersections;

        [Fact]
        public void WhereShadingAnIntersection_ThenAColorIsReturned()
        {
            //Given
            GivenAWorld(new World());
            GivenARay(new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1)));
            GivenAShape(_world.Shapes.First());
            GivenAnIntersection(new Intersection(4, base._shape1));
            GivenPreparedComputations(_intersection, _ray);

            //When
            WhenShadeHitIsCalled(_world, _precomputation);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.00001, new Color(0.38066, 0.47583, 0.2855));
        }

        [Fact]
        public void WhereShadingAnIntersectionFromTheInside_ThenAColorIsReturned()
        {
            //Given
            GivenAWorld(new World());
            GivenALightOnTheWorld(new Light(Tuple.Point(0, 0.25, 0), new Color(1, 1, 1)));
            GivenARay(new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1)));
            GivenAShape(_world.Shapes.Last());
            GivenAnIntersection(new Intersection(.5, base._shape1));
            GivenPreparedComputations(_intersection, _ray);
            //When
            WhenShadeHitIsCalled(_world, _precomputation);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.00001, new Color(0.90498, 0.90498, 0.90498));
        }

        [Fact]
        public void WhereShadeHitHasInIntersectionInAShadow_ThenOnlyTheAmbientColorIsReturned()
        {
            //Given
            GivenAWorld(new World());
            GivenALightOnTheWorld(new Light(Tuple.Point(0, 0, -10), Color.White));
            GivenAShape1(new Sphere());
            GivenAShape2(new Sphere());
            GivenATransformAddedTo(_shape2, Matrix.Transform.Translation(0, 0, 10));
            GivenAListOfSpheresIsAddedToWorld(new List<Shape> { _shape1, _shape2 });
            GivenARay(new Ray(Tuple.Point(0, 0, 5), Tuple.Vector(0, 0, 1)));
            GivenAnIntersection(new Intersection(4, _shape2));

            //When
            WhenPrepareComputationsIsCalledWithShade(_intersection, _ray);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.1, new Color(.1, .1, .1));
        }

        [Fact]
        public void WhereShadeHitIsCalledWithAReflectiveMaterial_ThenTheReflectedColorIsReturned()
        {
            var root2 = Math.Sqrt(2);
            //Given
            GivenAWorld(new World());
            GivenAShape(new Plane
            {
                Material = new Material { Reflective = .5 },
                Transform = Matrix.Transform.Translation(0, -1, 0)
            });
            GivenAShapeAddedToTheWorld(_world, base._shape1);
            GivenARay(new Ray(RayTracer.Tuple.Point(0, 0, -3), Tuple.Vector(0, -root2 / 2, root2 / 2)));
            GivenAnIntersection(new Intersection(root2, base._shape1));
            GivenPreComputation(_intersection, _ray);

            //When
            WhenShadeHitIsCalled(_world, _precomputation);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.0001, new Color(.87677, .92436, .82918));

        }

        [Fact]
        public void WhereShadeHitIsCalledWithATransparentMaterial()
        {
            //Given
            GivenAWorld(new World());
            GivenAShape1(new Plane
            {
                Transform = Matrix.Transform.Translation(0,-1,0),
                Material = new Material
                {
                    Transparency = .5,
                    RefractiveIndex = 1.5
                }
            });
            GivenAShapeAddedToTheWorld(_world, _shape1);
            GivenAShape2(new Sphere
            {
                Transform = Matrix.Transform.Translation(0,-3.5,-.5),
                Material = new Material
                {
                    Color = new Color(1,0,0),
                    Ambient = .5
                }
            });
            GivenAShapeAddedToTheWorld(_world, _shape2);
            GivenARay(new Ray(Tuple.Point(0,0,-3), Tuple.Vector(0,-_root2/2, _root2/2)));
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(_root2,_shape1)
            });
            GivenPreComputationWithIntersection(_intersections[0],_ray,_intersections);
            
            //When
            WhenShadeHitIsCalledWithRemaining(_world, _precomputation, 5);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.00001, new Color(.93642, .68642, .68642));
        }

        [Fact]
        public void WhereShadeHitIsCalledWithATransparentAndReflectiveMaterial()
        {
            //Given
            GivenAWorld(new World());
            GivenARay(new Ray(Tuple.Point(0,0,-3), Tuple.Vector(0, -_root2 / 2, _root2 / 2)));
            GivenAShape1(new Plane
            {
                Transform = Matrix.Transform.Translation(0,-1,0),
                Material = new Material
                {
                    Reflective = .5,
                    Transparency = .5,
                    RefractiveIndex = 1.5
                }
            });
            GivenAShapeAddedToTheWorld(_world, _shape1);
            GivenAShape2(new Sphere
            {
                Transform = Matrix.Transform.Translation(0,-3.5,-.5),
                Material = new Material
                {
                    Color = new Color(1,0,0),
                    Ambient = .5
                }
            });
            GivenAShapeAddedToTheWorld(_world,_shape2);
            GivenIntersections(new Intersections
            {
                [0] = new Intersection(_root2, _shape1)
            });
            GivenPreComputationWithIntersection(_intersections[0], _ray, _intersections);

            //When
            WhenShadeHitIsCalledWithRemaining(_world, _precomputation, 5);

            //Then
            ThenTheColorResultIsReturnedToPrecision(.00001, new Color(.93391, .69643, .69243));
        }

        private void WhenShadeHitIsCalledWithRemaining(World world, Intersection.PreComputation precomputation, int remaining)
        {
            _colorResult = world.ShadeHit(precomputation.Shape, precomputation, remaining);  //I doubt that the 'correct' shape is being passed here, but the test seems fine.

        }

        private void GivenPreComputationWithIntersection(Intersection intersection, Ray ray, Intersections intersections)
        {
            _precomputation = intersection.PrepareComputations(ray, intersections);
        }

        private void GivenIntersections(Intersections intersections)
        {
            _intersections = intersections;
        }

        private void GivenAShape1(Shape shape)
        {
            _shape1 = shape;
        }

        private void GivenAListOfSpheresIsAddedToWorld(List<Shape> spheres)
        {
            _world.Shapes = spheres;
        }

        private void GivenATransformAddedTo(Shape shape, Matrix transform)
        {
            shape.Transform = transform;
        }

        private void GivenAShape2(Shape shape)
        {
            _shape2 = shape;
        }

        private void GivenPreparedComputations(Intersection intersection, Ray ray)
        {
            _precomputation = intersection.PrepareComputations(ray);
        }

        private void WhenPrepareComputationsIsCalledWithShade(Intersection intersection, Ray ray)
        {
            Intersection.PreComputation preparedComputations = intersection.PrepareComputations(ray);
            _colorResult = _world.ShadeHit(_intersection.Object, preparedComputations);
        }

        private void WhenShadeHitIsCalled(World world, Intersection.PreComputation preparedComputations)
        {
            _colorResult = world.ShadeHit(preparedComputations.Shape, preparedComputations);  //I doubt that the 'correct' shape is being passed here, but the test seems fine.
        }
    }

    #endregion
}
