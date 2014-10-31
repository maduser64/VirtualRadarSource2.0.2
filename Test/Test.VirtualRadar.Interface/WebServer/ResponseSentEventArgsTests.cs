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
using VirtualRadar.Interface.WebServer;
using Moq;

namespace Test.VirtualRadar.Interface.WebServer
{
    [TestClass]
    public class ResponseSentEventArgsTests
    {
        [TestMethod]
        public void ResponseSentEventArgs_Constructor_Initialises_To_Known_State_And_Properties_Work()
        {
            var request = new Mock<IRequest>();
            var args = new ResponseSentEventArgs("the url", "user address and port", "user's address", 1023L, ContentClassification.Json, request.Object, 404, 998877);

            Assert.AreEqual(1023L, args.BytesSent);
            Assert.AreEqual(ContentClassification.Json, args.Classification);
            Assert.AreEqual("the url", args.UrlRequested);
            Assert.AreEqual("user's address", args.UserAddress);
            Assert.AreEqual("user address and port", args.UserAddressAndPort);
            Assert.AreSame(request.Object, args.Request);
            Assert.AreEqual(404, args.HttpStatus);
            Assert.AreEqual(998877, args.Milliseconds);
        }
    }
}
