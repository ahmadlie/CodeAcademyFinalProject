using FP.DTOLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.BusinessLayer.Abstract
{
	public interface ISmsService
	{
		Task<string> SendSmsAsync(ResetPasswordDTO dto);
	}
}
