using FluentAssertions;
using NUnit.Framework;
using SampleApp.Models;

namespace UnitTests
{
    public class ProductTests
    {
        //                                  |---------arrange----------|-----act----|-----assert-----|
        [Test] public void IsExpensive() => new Product { Price = 1004 }.IsExpensive.Should().BeTrue();

        [Test] public void IsExactlyExpensive() => new Product { Price = 1000 }.IsExpensive.Should().BeTrue();

        [Test] public void IsNotExpensive() => new Product { Price = 4 }.IsExpensive.Should().BeFalse();
    }
}
