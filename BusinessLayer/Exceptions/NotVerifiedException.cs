using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Exceptions;
public class NotVerifiedException : Exception
{
	public NotVerifiedException(string? message = "Please Verified!") : base(message)
	{
	}
}
