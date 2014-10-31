﻿// Copyright © Honglin Aviation, Vincent Deng(邓守海)
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
using Test.Framework;
using VirtualRadar.Interface.FlightSimulatorX;

namespace Test.VirtualRadar.Interface.FlightSimulatorX
{
    [TestClass]
    public class FlightSimulatorXExceptionTests
    {
        [TestMethod]
        public void FlightSimulatorXException_Complies_With_Exception_Spec()
        {
            TestUtilities.AssertIsException(typeof(FlightSimulatorXException));
        }

        [TestMethod]
        public void FlightSimulatorXException_Custom_Constructor_Initialises_Properties_Correctly()
        {
            var exception = new FlightSimulatorXException((uint)FlightSimulatorXExceptionCode.DataError, 12, 43);
            Assert.AreEqual(FlightSimulatorXExceptionCode.DataError, exception.ExceptionCode);
            Assert.AreEqual((uint)FlightSimulatorXExceptionCode.DataError, exception.RawExceptionCode);
            Assert.AreEqual(12u, exception.IndexNumber);
            Assert.AreEqual(43u, exception.SendID);
        }

        [TestMethod]
        public void FlightSimulatorXException_Custom_Constructor_Copes_When_No_Enum_Exists_For_The_Exception_Code()
        {
            var exception = new FlightSimulatorXException(91237, 12, 43);
            Assert.AreEqual(FlightSimulatorXExceptionCode.Unknown, exception.ExceptionCode);
            Assert.AreEqual(91237u, exception.RawExceptionCode);
        }

        [TestMethod]
        public void FlightSimulatorXException_Custom_Constructor_Sets_Message_Based_On_Parameters()
        {
            var exception = new FlightSimulatorXException((uint)FlightSimulatorXExceptionCode.OutOfBounds, 2, 3);
            Assert.AreEqual("FSX exception OutOfBounds(31), parameter 2, packet 3", exception.Message);
        }

    }
}
