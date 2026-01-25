using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordRPC.RPC.Payload
{
	internal class ClosePayload : IPayload
	{
		/// <summary>
		/// The close code the discord gave us
		/// </summary>
		[JsonPropertyName("code")]
		public int Code { get; set; }

		/// <summary>
		/// The close reason discord gave us
		/// </summary>
		[JsonPropertyName("message")]
		public string Reason { get; set; }

		[JsonConstructor]
		public ClosePayload() 
			: base() 
		{ 
			Code = -1; 
			Reason = ""; 
		}
	}
}
