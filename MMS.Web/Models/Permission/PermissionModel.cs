using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Permission
{
    public class PermissionModel
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDesc { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int UserTypeID { get; set; }
        public List<tbl_Permission> PermissionModelList { get; set; }
    }
}