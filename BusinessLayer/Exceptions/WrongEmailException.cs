using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.BusinessLayer.Exceptions;
public class WrongEmailException : Exception
{
	public WrongEmailException(string message = "Please Correct Email Address") : base(message)
	{
	}

}
