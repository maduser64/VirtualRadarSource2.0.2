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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace VirtualRadar.Resources
{
    /// <summary>
    /// Exposes access to the contents of the resources.
    /// </summary>
    public static class BinaryResources
    {
        /// <summary>
        /// Returns a copy of the named resource.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static byte[] Copy(string name)
        {
            byte[] result = null;

            string fullPath = String.Format("VirtualRadar.Resources.{0}", name);

            var assembly = Assembly.GetExecutingAssembly();
            using(var streamIn = assembly.GetManifestResourceStream(fullPath)) {
                using(var streamOut = new MemoryStream()) {
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while((bytesRead = streamIn.Read(buffer, 0, buffer.Length)) != 0) {
                        streamOut.Write(buffer, 0, bytesRead);
                    };

                    result = streamOut.ToArray();
                }
            }

            return result;
        }
    }
}
