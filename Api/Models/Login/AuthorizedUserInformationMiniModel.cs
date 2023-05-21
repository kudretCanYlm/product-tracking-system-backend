using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models.Login
{
	public class AuthorizedUserInformationMiniModel
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
	}
}