using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BunBlog.Core;

namespace BunBlog.Core.Test.BunHelperTest
{
    public class GetOSNameTest
    {
        [Fact]
        public void OnMicrosoftWindows_ReturnsWindows()
        {
            var osDescription = "Microsoft Windows 10.0.18362";

            var osName = BunHelper.GetOSName(osDescription);

            Assert.Equal("Windows", osName);
        }

        [Fact]
        public void OnDebian_ReturnsDebian()
        {
            var osDescription = "Linux 4.9.0-6-amd64 #1 SMP Debian 4.9.88-1+deb9u1 (2018-05-07)";

            var osName = BunHelper.GetOSName(osDescription);

            Assert.Equal("Debian", osName);
        }

        [Fact]
        public void OnWSL_ReturnsOther()
        {
            var osDescription = "Linux 4.4.0-18362-Microsoft #1-Microsoft Mon Mar 18 12:02:00 PST 2019";

            var osName = BunHelper.GetOSName(osDescription);

            Assert.Equal("Other", osName);
        }

        [Fact]
        public void OnOSX_ReturnsOther()
        {
            var osDescription = "Darwin 17.4.0 Darwin Kernel Version 17.4.0: Sun Dec 17 09:19:54 PST 2017; root:xnu-4570.41.2~1/RELEASE_X86_64";

            var osName = BunHelper.GetOSName(osDescription);

            Assert.Equal("Other", osName);
        }
    }
}
