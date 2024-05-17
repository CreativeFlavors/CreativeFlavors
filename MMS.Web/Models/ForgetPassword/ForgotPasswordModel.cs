using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.ForgetPassword
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Email { get; set; }
        public bool? IsExist { get; set; }
    }

    
}