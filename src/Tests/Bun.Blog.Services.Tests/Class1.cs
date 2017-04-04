using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Bun.Blog.Services.Tests
{
    public class Class1
    {
        [Fact]
        public void When_value_is_true_return_true()
        {
            // Arrange
            var value = true;

            // Act

            // Assert
            Assert.True(value);
        }
    }
}
