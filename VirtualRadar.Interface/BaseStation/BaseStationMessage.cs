// Copyright � 2010 onwards, Andrew Whewell
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
using System.Text;
using System.Globalization;

namespace VirtualRadar.Interface.BaseStation
{
    /// <summary>
    /// An object that carries information about an incoming message from Kinetic's BaseStation application.
    /// </summary>
    public class BaseStationMessage
    {
        /// <summary>
        /// Gets or sets the unique ID of the receiver that picked up the message, if any. 0 if the ID is unknown.
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets the signal level, if known, at the time the packet carrying the message was received. If this
        /// is null then the receiver did not pass on the signal level.
        /// </summary>
        public int? SignalLevel { get; set; }

        /// <summary>
        /// Gets or sets the type of message.
        /// </summary>
        public BaseStationMessageType MessageType { get; set; }

        /// <summary>
        /// Gets or sets the type of transmission sent in a Transmission message.
        /// </summary>
        public BaseStationTransmissionType TransmissionType { get; set; }

        /// <summary>
        /// Gets or sets the status change in a StatusChanged message.
        /// </summary>
        public BaseStationStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the Mode S identifier transmitted by the aircraft.
        /// </summary>
        public string Icao24 { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the BaseStation session.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the aircraft in the BaseStation database.
        /// </summary>
        public int AircraftId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the flight in the BaseStation database.
        /// </summary>
        public int FlightId { get; set; }

        /// <summary>
        /// Gets or sets the date and time the message was generated by BaseStation.
        /// </summary>
        public DateTime MessageGenerated { get; set; }

        /// <summary>
        /// Gets or sets the date and time that the message was logged by BaseStation.
        /// </summary>
        public DateTime MessageLogged { get; set; }

        /// <summary>
        /// Gets or sets the callsign of the aircraft, if known.
        /// </summary>
        public string Callsign { get; set; }

        /// <summary>
        /// Gets or sets the altitude of the aircraft, if known.
        /// </summary>
        public int? Altitude { get; set; }

        /// <summary>
        /// Gets or sets the speed over the ground of the aircraft, if known.
        /// </summary>
        public float? GroundSpeed { get; set; }

        /// <summary>
        /// Gets or sets the direction that the aircraft is heading in, if known.
        /// </summary>
        public float? Track { get; set; }

        /// <summary>
        /// Gets or sets the north (+ve) or south (-ve) coordinate for the aircraft.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the east (+ve) or west (-ve) coordinate for the aircraft.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the speed of ascent or descent.
        /// </summary>
        public int? VerticalRate { get; set; }

        /// <summary>
        /// Gets or sets the squawk code.
        /// </summary>
        public int? Squawk { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the squawk code has changed.
        /// </summary>
        public bool? SquawkHasChanged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the squawk code is set to a value indicating an emergency.
        /// </summary>
        public bool? Emergency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the aircraft's transponder ident has been activated.
        /// </summary>
        public bool? IdentActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating that the vehicle is on the ground.
        /// </summary>
        public bool? OnGround { get; set; }

        /// <summary>
        /// Gets or sets the message number for an archived message. Always zero for live messages.
        /// </summary>
        public int MessageNumber { get; set; }

        /// <summary>
        /// Gets or sets the optional supplementary message object that carries extra information from a raw Mode-S or ADS-B message.
        /// </summary>
        /// <remarks>
        /// This can be null, even for a message that has been created by the <see cref="IRawMessageTranslator"/>.
        /// </remarks>
        public BaseStationSupplementaryMessage Supplementary { get; set; }

        /// <summary>
        /// Returns the message as parseable BaseStation message text.
        /// </summary>
        /// <returns>The properties of the message formatted up as a text string that can be sent to any application
        /// that can interpret BaseStation messages.</returns>
        public string ToBaseStationString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", BaseStationMessageHelper.ConvertToString(MessageType));
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", MessageType == BaseStationMessageType.Transmission ? BaseStationMessageHelper.ConvertToString(TransmissionType) : "");
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", SessionId);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", AircraftId);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Icao24);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", FlightId);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0:yyyy/MM/dd},", MessageGenerated);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0:HH:mm:ss.fff},", MessageGenerated);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0:yyyy/MM/dd},", MessageLogged);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0:HH:mm:ss.fff},", MessageLogged);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", MessageType == BaseStationMessageType.StatusChange ? BaseStationMessageHelper.ConvertToString(StatusCode) : Callsign);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Altitude);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Round.GroundSpeed(GroundSpeed));
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Round.Track(Track));
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Round.Coordinate(Latitude));
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Round.Coordinate(Longitude));
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", VerticalRate);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Squawk);
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", SquawkHasChanged == null ? "" : SquawkHasChanged.Value ? "-1" : "0");
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", Emergency == null ? "" : Emergency.Value ? "-1" : "0");
            result.AppendFormat(CultureInfo.InvariantCulture, "{0},", IdentActive == null ? "" : IdentActive.Value ? "-1" : "0");
            result.AppendFormat(CultureInfo.InvariantCulture, "{0}", OnGround == null ? "" : OnGround.Value ? "-1" : "0");

            return result.ToString();
        }
    }
}
