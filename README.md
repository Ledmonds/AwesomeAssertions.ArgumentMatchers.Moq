![Nuget](https://img.shields.io/nuget/dt/AwesomeAssertions.ArgumentMatchers.Moq)

AwesomeAssertions.ArgumentMatchers.Moq
===

This repository is a absoloute rip-off of [ronaldbosmas](https://github.com/ronaldbosma) [FluentAssertions.ArgumentMatchers.Moq](https://github.com/ronaldbosma/FluentAssertions.ArgumentMatchers.Moq) repository, and merely ports the code from `FluentAssertions` to `AwesomeAssertions` (which took all of about 5 minutes...); credit belongs to the original developer.

The [AwesomeAssertions.ArgumentMatchers.Moq NuGet package](https://www.nuget.org/packages/AwesomeAssertions.ArgumentMatchers.Moq/) provides a simple way to use [`Moq`](https://github.com/devlooped/moq) in combination with `AwesomeAssertions` to compare complex objects.

The package has a method called `Its.EquivalentTo`. It can be used in the Setup and Verify stages of a Mock similar to other argument matchers like ` It.IsAny<T>()`. The `actual.Should().BeEquivalentTo(expected)` method is used inside to compare objects. An overload is available so you can pass in configuration to AwesomeAssertions.

### Examples
```csharp
_mock.Setup(m => m.DoSomething(Its.EquivalentTo(expectedComplexType))).Returns(result);

_mock.Verify(m => m.DoSomething(Its.EquivalentTo(expectedComplexType)));

_mock.Verify(m => m.DoSomething(Its.EquivalentTo(
    expectedComplexType, 
    options => options.Excluding(c => c.SomeProperty)
)));
```

### Future Expansions:
- Essentially the same _Argument Matcher_ for `NSubstitute`.