﻿using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GateEntryModel
{
    public class GateEntryInwardDocument
    {
        public int GateEntryDocumentId { get; set; }
        public int InwardDocTypeId { get; set; }
        public string GateEntryNo { get; set; }
        public string EntryDate { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public string Mode { get; set; }
        public string Company { get; set; }
        public string FromWhere { get; set; }
        public string Unit { get; set; }
        public string PersonName { get; set; }
        public string ModeofTransport { get; set; }
        public string VehicleNo { get; set; }
        public string AddressToWhom { get; set; }
        public string HandOverTo { get; set; }
        public string ReceivedBy { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string TagName { get; set; }

        //public string EmpNameToWhom { get; set; }
        //public string EmpNameReceivedBy { get; set; }
        public List<GateEntryInwardDocumentMaster> GateEntryDocumentlist { get; set; }

        public List<GateEntryInwardDocumentUploadMaster> GateEntryInwardDocumentUploadlist { get; set; }


    }
}