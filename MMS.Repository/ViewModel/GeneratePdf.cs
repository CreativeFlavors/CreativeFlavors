using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.ViewModel
{
    public class GeneratePdf
    {
        public int GateEntryOutwardId { get; set; }
        public string GateEntryNo { get; set; }
        public string DcNo { get; set; }
        public DateTime? DcDate { get; set; }
        public int SupplierId { get; set; }
        public string ModeofTransport { get; set; }
        public string VehicleNo { get; set; }
        public int MaterialNameId { get; set; }
        public string HSNCode { get; set; }
        public int UomId { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Value { get; set; }
        public string PreparedBy { get; set; }
        public string SupplierGSTIN { get; set; }
        public string TransportCompany { get; set; }
        public string VehicleArrangedBy { get; set; }
        public string Piecies { get; set; }
        public string Remarks { get; set; }
        public string Purpose { get; set; }
        public int StoresName { get; set; }
        public string TaxName { get; set; }
        public string ReturnType { get; set; }
        public string PlaceOfSupply { get; set; }
        public decimal? GST { get; set; }
        public decimal? GSTAmount { get; set; }
        public decimal? GrandTotal { get; set; }

        public string MaterialDes { get; set; }
    }
}
