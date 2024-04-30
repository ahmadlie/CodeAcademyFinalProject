using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions;
public class NotCorrectException : Exception
{
	public NotCorrectException(string? message = "Please Enter Correctly!") : base(message)
	{
	}
}
