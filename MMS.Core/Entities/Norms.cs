﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
  public  class Norms:BaseEntity
    {
        public int Normsid { get; set; }
        public int GroupId { get; set; }
        public int BuyerOption { get; set; }
        public int OurNorms { get; set; }
        public int BuyerNameid { get; set; }
        public int PurchaseNormsid { get; set; }
        public int IssueNormsid { get; set; }
        public int CostingNorms { get; set; }
        public decimal? NormsPercentage { get; set; }
        public decimal? NormsPercentage1 { get; set; }
        public decimal? NormsPercentage2 { get; set; }
        public decimal? NormsPercentage3 { get; set; }
       
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}