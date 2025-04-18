using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForeach.Framework.Tests;

public class FacetBagTests
{
    [Fact]
    public void should_return_nothing_when_not_set()
    {
        var bag = new FacetBag();

        var facet = bag.TryGet<SomeFacet>();

        facet.ShouldBeNull();
    }

    [Fact]
    public void should_return_value_when_set()
    {
        var bag = new FacetBag();

        bag.Set(new SomeFacet
        { 
            SomeValue = "val" 
        });

        var facet = bag.TryGet<SomeFacet>();

        facet.ShouldNotBeNull();
        facet.SomeValue.ShouldBe("val");
    }

    [Fact]
    public void should_return_parent_value_when_constructed_from_parent()
    { 
        var parentBag = new FacetBag();

        parentBag.Set(new SomeFacet
        {
            SomeValue = "parent-val"
        });

        var bag = new FacetBag(parentBag);
        
        var facet = bag.TryGet<SomeFacet>();

        facet.ShouldNotBeNull();
        facet.SomeValue.ShouldBe("parent-val");
    }

    [Fact]
    public void should_return_value_when_overriden_and_constructed_from_parent()
    {
        var parentBag = new FacetBag();

        parentBag.Set(new SomeFacet
        {
            SomeValue = "parent-val"
        });

        var bag = new FacetBag(parentBag);

        bag.Set(new SomeFacet
        {
            SomeValue = "val"
        });

        var facet = bag.TryGet<SomeFacet>();

        facet.ShouldNotBeNull();
        facet.SomeValue.ShouldBe("val");
    }

    [Fact]
    public void should_not_affect_parent_value_when_overriden_and_constructed_from_parent()
    {
        var parentBag = new FacetBag();

        parentBag.Set(new SomeFacet
        {
            SomeValue = "parent-val"
        });

        var bag = new FacetBag(parentBag);

        bag.Set(new SomeFacet
        {
            SomeValue = "val"
        });

        var facet = parentBag.TryGet<SomeFacet>();

        facet.ShouldNotBeNull();
        facet.SomeValue.ShouldBe("parent-val");
    }

    [Fact]
    public void should_return_value_from_first_when_combined_and_not_overriden()
    {
        var first = new FacetBag();
        first.Set(new SomeFacet { SomeValue = "first-val" });
        
        var second = new FacetBag();
        first.Set(new AnotherFacet { SomeValue = "second-val" });

        var combined = first.Combine(second);

        var facet = combined.TryGet<SomeFacet>();
        facet.ShouldNotBeNull();
        facet.SomeValue.ShouldBe("first-val");
    }

    [Fact]
    public void should_return_value_from_second_when_combined_and_overriden()
    {
        var first = new FacetBag();
        first.Set(new SomeFacet { SomeValue = "first-val" });

        var second = new FacetBag();
        second.Set(new SomeFacet { SomeValue = "second-val" });

        var combined = first.Combine(second);

        var facet = combined.TryGet<SomeFacet>();
        facet.ShouldNotBeNull();
        facet.SomeValue.ShouldBe("second-val");
    }

    [Fact]
    public void should_not_affect_first_when_combined_and_overriden()
    {
        var first = new FacetBag();
        first.Set(new SomeFacet { SomeValue = "first-val" });

        var second = new FacetBag();
        second.Set(new SomeFacet { SomeValue = "second-val" });

        first.Combine(second);

        var facet = first.TryGet<SomeFacet>();
        facet.ShouldNotBeNull();
        facet.SomeValue.ShouldBe("first-val");
    }

    private class SomeFacet
    {
        public string? SomeValue { get; set; }
    }

    private class AnotherFacet
    {
        public string? SomeValue { get; set; }
    }
}
