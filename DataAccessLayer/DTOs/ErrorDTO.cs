﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
	public class ErrorDTO
	{
		public string? Message { get; set; }
		public List<string>? Errors{ get; set; }
	}
}
