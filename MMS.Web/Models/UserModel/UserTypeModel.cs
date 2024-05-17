using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.UserModel
{
    public class UserTypeModel
    {
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public string UserTypeDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public List<UserTypeMaster> UserTypeModelList { get; set; }
    }
}