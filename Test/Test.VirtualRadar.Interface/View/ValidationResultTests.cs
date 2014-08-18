﻿// Copyright © 2014 Honglin(宏林), Vincent Deng(邓守海)
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
using VirtualRadar.Interface.View;
using Test.Framework;

namespace Test.VirtualRadar.Interface.View
{
    [TestClass]
    public class ValidationResultTests
    {
        [TestMethod]
        public void ValidationResult_Constructor_Initialises_To_Known_State()
        {
            var validationResult = new ValidationResult();

            TestUtilities.TestProperty(validationResult, r => r.Field, ValidationField.None, ValidationField.EndDate);
            TestUtilities.TestProperty(validationResult, r => r.Message, null, "This is a test");
            TestUtilities.TestProperty(validationResult, r => r.IsWarning, false);
            TestUtilities.TestProperty(validationResult, r => r.Record, null, this);

            var validationResult2 = new ValidationResult(ValidationField.EndDate, "Hello");
            Assert.AreEqual(ValidationField.EndDate, validationResult2.Field);
            Assert.AreEqual("Hello", validationResult2.Message);
            Assert.AreEqual(false, validationResult2.IsWarning);
            Assert.IsNull(validationResult2.Record);

            var validationResult3 = new ValidationResult(ValidationField.BaseStationAddress, "Message here", true);
            Assert.AreEqual(ValidationField.BaseStationAddress, validationResult3.Field);
            Assert.AreEqual("Message here", validationResult3.Message);
            Assert.IsTrue(validationResult3.IsWarning);
            Assert.IsNull(validationResult3.Record);

            var validationResult4 = new ValidationResult(this, ValidationField.EndDate, "Hello");
            Assert.AreEqual(ValidationField.EndDate, validationResult4.Field);
            Assert.AreEqual("Hello", validationResult4.Message);
            Assert.AreEqual(false, validationResult4.IsWarning);
            Assert.AreSame(this, validationResult4.Record);

            var validationResult5 = new ValidationResult(this, ValidationField.BaseStationAddress, "Message here", true);
            Assert.AreEqual(ValidationField.BaseStationAddress, validationResult5.Field);
            Assert.AreEqual("Message here", validationResult5.Message);
            Assert.IsTrue(validationResult5.IsWarning);
            Assert.AreSame(this, validationResult5.Record);
        }
    }
}
