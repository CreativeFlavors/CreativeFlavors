using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Data.StoredProcedureModel;
using MMS.Core.Entities;
using MMS.Core.Entities.JobWork;
using MMS.Core.Entities.GateEntry;

namespace MMS.Web.Models.JobOtherWork
{
    public class JobOtherWorkModel
    {
        public int OtherJobWork_Id { get; set; }
        public bool Services { get; set; }
        public String OtherJobWork_Code { get; set; }
        public DateTime OtherJobWork_Date { get; set; }
        public Guid Department_Id { get; set; }
        public int JobWork_Id { get; set; }
        public int Process_Id { get; set; }
        public int Buyer_Id { get; set; }
        public int Season_Id { get; set; }
        public int Stoeres_Id { get; set; }
        public int Group_Id { get; set; }
        public int Category_Id { get; set; }
        public int Machinery_Id { get; set; }
        public int Spare_Id { get; set; }
        public int Quantity { get; set; }
        public int UOM { get; set; }
        public decimal JobWork_Price { get; set; }
        public decimal JobWork_Price_Value { get; set; }
        public string ServiceDescription { get; set; }
        public decimal GST { get; set; }
        public decimal GST_Amount { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public List<JobOtherWorkList> JobOtherWorkList { get; set; }
        public List<JobworkMaster> jobWorkList { get; set; }
        public List<ProcessMaster> processList { get; set; }
        public List<BuyerMaster> buyerList { get; set; }
        public List<SeasonMaster> seasonList { get; set; }
        public List<StoreMaster> storesList { get; set; }
        public List<MaterialGroupMaster_> materialGroupList { get; set; }
        public List<MaterialCategoryMaster> materialCategoryList { get; set; }
        public List<MachineryMaster> machineryList { get; set; }
        public List<EmployDepartmentMaster> departmentList { get; set; }        
        
    }
}

