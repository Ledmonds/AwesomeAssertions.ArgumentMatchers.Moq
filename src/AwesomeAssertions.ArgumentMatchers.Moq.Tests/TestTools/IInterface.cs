using System.Collections.Generic;

namespace AwesomeAssertions.ArgumentMatchers.Moq.Tests.TestTools;

public interface IInterface
{
    void DoSomething(ComplexType complexType);

    void DoSomethingWithCollection(IEnumerable<ComplexType> complexType);
}
