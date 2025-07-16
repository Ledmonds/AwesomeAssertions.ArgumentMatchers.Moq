using System;
using System.Collections.Generic;
using AutoFixture;
using AwesomeAssertions.ArgumentMatchers.Moq.Tests.TestTools;
using Moq;
using Xunit;

namespace AwesomeAssertions.ArgumentMatchers.Moq.Tests;

public class ItsTests
{
    private Fixture _fixture = new();
    private Mock<IInterface> _mock = new Mock<IInterface>();

    [Fact]
    public void EquivalentTo_Matches_Same_Complex_Types()
    {
        var complexType = _fixture.Create<ComplexType>();

        _mock.Object.DoSomething(complexType);

        _mock.Verify(m => m.DoSomething(Its.EquivalentTo(complexType)));
    }

    [Fact]
    public void EquivalentTo_Matches_Two_Different_Complex_Types_With_Same_Data()
    {
        var complexType = _fixture.Create<ComplexType>();
        var expectedComplexType = complexType.Copy();

        _mock.Object.DoSomething(complexType);

        _mock.Verify(m => m.DoSomething(Its.EquivalentTo(expectedComplexType)));
    }

    [Fact]
    public void EquivalentTo_Does_Not_Match_Two_Complex_Types_If_Child_Property_Has_Different_Value()
    {
        var complexType = _fixture.Create<ComplexType>();

        var expectedComplexType = complexType.Copy();
        expectedComplexType.ComplexTypeProperty.IntProperty++;

        _mock.Object.DoSomething(complexType);

        Action verify = () =>
            _mock.Verify(m => m.DoSomething(Its.EquivalentTo(expectedComplexType)));
        verify.Should().Throw<MockException>();
    }

    [Fact]
    public void EquivalentTo_Matches_Two_Complex_Types_If_Child_Property_Has_Different_Value_But_Its_Ignored()
    {
        var complexType = _fixture.Create<ComplexType>();

        var expectedComplexType = complexType.Copy();
        expectedComplexType.ComplexTypeProperty.IntProperty++;

        _mock.Object.DoSomething(complexType);

        _mock.Verify(
            m =>
                m.DoSomething(
                    Its.EquivalentTo(
                        expectedComplexType,
                        options => options.Excluding(c => c.ComplexTypeProperty.IntProperty)
                    )
                )
        );
    }

    [Fact]
    public void EquivalentTo_Matches_If_Actual_And_Expected_Are_Null()
    {
        _mock.Object.DoSomething(null);

        _mock.Verify(m => m.DoSomething(Its.EquivalentTo<ComplexType>(null)));
    }

    [Fact]
    public void EquivalentTo_Does_Not_Match_If_Actual_Object_Has_Value_And_Expected_Object_Is_Null()
    {
        var complexType = _fixture.Create<ComplexType>();

        _mock.Object.DoSomething(complexType);

        var action = () => _mock.Verify(m => m.DoSomething(Its.EquivalentTo<ComplexType>(null)));
        action.Should().Throw<MockException>();
    }

    [Fact]
    public void EquivalentTo_Does_Not_Match_If_Actual_Object_Is_Null_And_Expected_Object_Has_Value()
    {
        var expectedComplexType = _fixture.Create<ComplexType>();

        _mock.Object.DoSomething(null);

        var action = () => _mock.Verify(m => m.DoSomething(Its.EquivalentTo(expectedComplexType)));
        action.Should().Throw<MockException>();
    }

    [Fact]
    public void EquivalentTo_Matches_Two_Different_Types_With_Same_Data()
    {
        var list = new List<ComplexType> { _fixture.Create<ComplexType>() };

        _mock.Object.DoSomethingWithCollection(list.ToArray());

        _mock.Verify(x => x.DoSomethingWithCollection(Its.EquivalentTo(list)));
    }

    [Fact]
    public void EquivalentTo_Does_Not_Match_Two_Collections_When_Child_Property_Has_Different_Value()
    {
        var complexType = _fixture.Create<ComplexType>();
        var list = new List<ComplexType> { complexType };

        var expectedComplexType = complexType.Copy();
        // Change a property of the expected object to make it different from the actual object
        expectedComplexType.ComplexTypeProperty.IntProperty++;
        var expectedList = new List<ComplexType> { expectedComplexType };

        _mock.Object.DoSomethingWithCollection(list);

        Action verify = () =>
            _mock.Verify(m => m.DoSomethingWithCollection(Its.EquivalentTo(expectedList)));
        verify.Should().Throw<MockException>();
    }

    [Fact]
    public void EquivalentTo_Matches_Two_Collections_When_Child_Property_Has_Different_Value_But_Its_Ignored()
    {
        var complexType = _fixture.Create<ComplexType>();
        var list = new List<ComplexType> { complexType };

        var expectedComplexType = complexType.Copy();
        // Change a property of the expected object to make it different from the actual object
        expectedComplexType.ComplexTypeProperty.IntProperty++;
        var expectedList = new List<ComplexType> { expectedComplexType };

        _mock.Object.DoSomethingWithCollection(list);

        _mock.Verify(
            m =>
                m.DoSomethingWithCollection(
                    Its.EquivalentTo(
                        expectedList,
                        options => options.Excluding(c => c.ComplexTypeProperty.IntProperty)
                    )
                )
        );
    }
}
