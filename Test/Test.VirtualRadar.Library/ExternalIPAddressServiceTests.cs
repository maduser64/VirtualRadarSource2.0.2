﻿// Copyright © 2010 onwards, Andrew Whewell
// All rights reserved.
//
// Redistribution and use of this software in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//    * Neither the name of the author nor the names of the program's contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHORS OF THE SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualRadar.Interface;
using Moq;
using VirtualRadar.Library;
using Test.Framework;
using InterfaceFactory;

namespace Test.VirtualRadar.Library
{
    [TestClass]
    public class ExternalIPAddressServiceTests
    {
        public TestContext TestContext { get; set; }

        private IExternalIPAddressService _Service;
        private Mock<IExternalIPAddressServiceProvider> _Provider;
        private EventRecorder<EventArgs<string>> _AddressUpdated;

        [TestInitialize]
        public void TestInitialise()
        {
            _Service = Factory.Singleton.Resolve<IExternalIPAddressService>();
            _Provider = new Mock<IExternalIPAddressServiceProvider>() { DefaultValue = DefaultValue.Mock }.SetupAllProperties();
            _Service.Provider = _Provider.Object;
            _AddressUpdated = new EventRecorder<EventArgs<string>>();
        }

        [TestMethod]
        public void ExternalIPAddressService_Constructor_Initialises_To_Known_State_And_Properties_Work()
        {
            _Service = Factory.Singleton.Resolve<IExternalIPAddressService>();
            Assert.IsNotNull(_Service.Provider);
            TestUtilities.TestProperty(_Service, "Provider", _Service.Provider, _Provider.Object);
            Assert.IsNull(_Service.Address);
        }

        [TestMethod]
        public void ExternalIPAddressService_Singleton_Returns_Same_Instance()
        {
            var service1 = Factory.Singleton.Resolve<IExternalIPAddressService>();
            var service2 = Factory.Singleton.Resolve<IExternalIPAddressService>();

            Assert.IsNotNull(service1.Singleton);
            Assert.AreNotSame(service1, service2);
            Assert.AreSame(service1.Singleton, service2.Singleton);
        }

        [TestMethod]
        public void ExternalIPAdderssService_GetExternalIPAddress_Returns_Value_From_Provider()
        {
            _Provider.Setup(p => p.ExternalIpAddress()).Returns("Hello");
            Assert.AreEqual("Hello", _Service.GetExternalIPAddress());
        }

        [TestMethod]
        public void ExternalIPAdderssService_GetExternalIPAddress_Sets_Address()
        {
            _Provider.Setup(p => p.ExternalIpAddress()).Returns("There");
            _Service.GetExternalIPAddress();
            Assert.AreEqual("There", _Service.Address);
        }

        [TestMethod]
        public void ExternalIPAdderssService_GetExternalIPAddress_Raises_AddressUpdated()
        {
            _Provider.Setup(p => p.ExternalIpAddress()).Returns("Fubar");
            _AddressUpdated.EventRaised += (object sender, EventArgs<string> args) => { Assert.AreEqual("Fubar", _Service.Address); };
            _Service.AddressUpdated += _AddressUpdated.Handler;
            _Service.GetExternalIPAddress();

            Assert.AreEqual(1, _AddressUpdated.CallCount);
            Assert.AreSame(_Service, _AddressUpdated.Sender);
            Assert.AreEqual("Fubar", _AddressUpdated.Args.Value);
        }
    }
}
