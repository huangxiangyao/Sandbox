using Myvas.AspNetCore.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Myvas.AspNetCore.Tests
{
    public class TypeList_Test
    {
        [Fact]
        public void ShouldOnlyAddTrueTypes()
        {
            var list = new TypeList<IMyInterface>();
        }

        public interface IMyInterface
        {

        }

        public class MyClass1 : IMyInterface
        {

        }

        public class MyClass2 : IMyInterface
        {

        }

        public class MyClass3
        {

        }
    }
}
