using FluentAssertions;
using Lamar;
using NUnit.Framework;

namespace DIBasics_Lamar.M3; 

[TestFixture]
public class PickSpecificConstructorTests {
    [Test]
    public void PickSpecificConstructor() {
        // arrange
        var registry = new ServiceRegistry();
        registry.ForConcreteType<Service>();
        registry.ForConcreteType<Dependency1>();
        registry.ForConcreteType<Dependency2>();

        var container = new Container(registry);

        //act 
        var instance = container.GetInstance<Service>();

        instance.WhatWasCalled.Should().Be("1 dependency");
    }

    [Test]
    public void GreedyPick() {
        // arrange
        var registry = new ServiceRegistry();
        registry.ForConcreteType<Service2>();
        registry.ForConcreteType<Dependency1>();
        registry.ForConcreteType<Dependency2>();

        var container = new Container(registry);

        //act 
        var instance = container.GetInstance<Service2>();

        instance.WhatWasCalled.Should().Be("2 dependencies");
    }

    public class Service {
        public string WhatWasCalled { get; private set; }

        public Service() {
            WhatWasCalled = "No params";
        }

        [DefaultConstructor]
        public Service(Dependency1 dependency1) {
            WhatWasCalled = "1 dependency";
        }

        public Service(Dependency1 dependency1, Dependency2 dependency2) {
            WhatWasCalled = "2 dependencies";
        }
    }

    public class Service2 {
        public string WhatWasCalled { get; private set; }

        public Service2() {
            WhatWasCalled = "No params";
        }

        public Service2(Dependency1 dependency1) {
            WhatWasCalled = "1 dependency";
        }

        public Service2(Dependency1 dependency1, Dependency2 dependency2) {
            WhatWasCalled = "2 dependencies";
        }
    }

    public class Dependency1 { }
    public class Dependency2 { }
}


