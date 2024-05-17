using MMS.Core.Entities.GateEntry;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Data.Context;

namespace MMS.Web.PDFGeneration
{
    public class PdfGeneration
    {
        public static string PrintPdfGeneration(string GateEntryNo, string contents)
        {
            GateEntryOutwardManager manager = new GateEntryOutwardManager();
            List<GeneratePdf> receiptDetailslist = new List<GeneratePdf>();
            GateEntryOutwardMaster outwardMaster = new GateEntryOutwardMaster();
            List<SupplierMaster> supplierMaster = new List<SupplierMaster>();
            List<EmployNameMaster> employeeMaster = new List<EmployNameMaster>();
            List<Purpose> purposeMaster = new List<Purpose>();
            List<StoreMaster> storerMaster = new List<StoreMaster>();
            List<GeneratePdf> invoiceDetailslists = new List<GeneratePdf>();
            bool pageBreak = false;
            string[] values = GateEntryNo.Split(',');
            string FullName = string.Empty;
            string sourceFile = "";
            string HtmlText = "";
            System.Text.StringBuilder tempExport = new System.Text.StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                receiptDetailslist = manager.GeneratePdf(values[i]);
                int pagebreak = 0;
                if (pageBreak == false && receiptDetailslist.Count > 0)
                {
                    pageBreak = true;
                    pagebreak = 1;
                }
                string invoiceDynamicRow = "";
                decimal? value_ = 0;

                double tempreceiptDetailslist = Math.Ceiling(Convert.ToDouble(receiptDetailslist.Count.ToString()) / 7);

                if (receiptDetailslist != null && receiptDetailslist.Count > 0)
                {
                    //System.Text.StringBuilder tempExport = new System.Text.StringBuilder();
                    for (int j = 1; j <= tempreceiptDetailslist; j++)
                    {
                        invoiceDetailslists = null;

                        int count = (receiptDetailslist.Count() - (j * 7));
                        if (count >= 7)
                        {
                            invoiceDetailslists = receiptDetailslist.Skip((j - 1) * 7).Take(7).ToList();
                        }
                        else
                        {
                            invoiceDetailslists = receiptDetailslist.Count > 7 ? receiptDetailslist.Skip((j - 1) * 7).Take(7).ToList() : receiptDetailslist.ToList();
                        }
                        HtmlText = "";
                        HtmlText = contents;
                        string CompanyAddress = "";
                        string CompanyName = "";
                        string Phone = "";
                        string Email = "";
                        Random rnd_ = new Random();
                        int count_ = 1;
                        // decimal? ss = 0;
                        foreach (var item in receiptDetailslist)
                        {
                            invoiceDynamicRow += GetReceiptItems(item, count_);
                            value_ = Math.Round(Convert.ToDecimal(receiptDetailslist.Sum(x => x.Value)), 2);
                            count_++;
                        }
                        String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
                        CompanyManager companyManager = new CompanyManager();
                        Company company = new Company();
                        company = companyManager.GetCompanyDetails();
                        CompanyName = company.CompanyName;
                        CompanyAddress = company.Address;
                        Phone = company.Phone;
                        Email = "";   
                        HtmlText = HtmlText.Replace("[[DynamicOrderItemsRow]]", invoiceDynamicRow);
                        HtmlText = HtmlText.Replace("[[Total]]", value_.ToString());
                        int SupplierId = receiptDetailslist.FirstOrDefault().SupplierId;
                        if (SupplierId != 0)
                        {
                            supplierMaster = GetSupplier(SupplierId);
                        }
                        string aa = supplierMaster.FirstOrDefault().AddressLine1;
                        string ab = supplierMaster.FirstOrDefault().AddressLine2;
                        string ac = supplierMaster.FirstOrDefault().AddressLine3;
                        string GSTIN = supplierMaster.FirstOrDefault().SupplierGSTIN;
                        HtmlText = HtmlText.Replace("[[SupplierName]]", string.IsNullOrEmpty(supplierMaster.FirstOrDefault().SupplierName) ? " " : supplierMaster.FirstOrDefault().SupplierName);
                        HtmlText = HtmlText.Replace("[[SupplierAddress]]", string.Concat(aa, ab, ac));
                        HtmlText = HtmlText.Replace("[[Purpose]]", receiptDetailslist.FirstOrDefault().Purpose);
                        HtmlText = HtmlText.Replace("[[CompanyName]]", CompanyName);
                        HtmlText = HtmlText.Replace("[[GST]]", receiptDetailslist.FirstOrDefault().GST.ToString());
                        HtmlText = HtmlText.Replace("[[GSTAmount]]", receiptDetailslist.FirstOrDefault().GSTAmount.ToString());
                        HtmlText = HtmlText.Replace("[[Grandtotal]]", receiptDetailslist.FirstOrDefault().GrandTotal.ToString());
                        HtmlText = HtmlText.Replace("[[Address1]]", CompanyAddress);
                        HtmlText = HtmlText.Replace("[[Phone_Email]]", Email);
                        int StoresID = receiptDetailslist.FirstOrDefault().StoresName;
                        if (StoresID != 0)
                        {
                            storerMaster = GetStore(StoresID);
                            HtmlText = HtmlText.Replace("[[StoreName]]", string.IsNullOrEmpty(storerMaster.FirstOrDefault().StoreName) ? " " : storerMaster.FirstOrDefault().StoreName);
                        }
                        else
                        {
                            HtmlText = HtmlText.Replace("[[StoreName]]", "");
                        }
                        HtmlText = HtmlText.Replace("[[DCNo]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().DcNo) ? " " : receiptDetailslist.FirstOrDefault().DcNo);
                        HtmlText = HtmlText.Replace("[[Remarks]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().Remarks) ? " " : receiptDetailslist.FirstOrDefault().Remarks);
                        HtmlText = HtmlText.Replace("[[DCDate]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().DcDate.ToString()) ? " " : receiptDetailslist.FirstOrDefault().DcDate.Value.ToString("dd/MM/yyyy"));
                        HtmlText = HtmlText.Replace("[[SupplierGSTIN]]", string.IsNullOrEmpty(GSTIN) ? " " : GSTIN);
                        HtmlText = HtmlText.Replace("[[PlaceOfSupply]]", receiptDetailslist.FirstOrDefault().PlaceOfSupply);
                        HtmlText = HtmlText.Replace(" [[TaxType]]", receiptDetailslist.FirstOrDefault().TaxName);
                        HtmlText = HtmlText.Replace(" [[DCDate]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().DcDate.ToString()) ? " " : receiptDetailslist.FirstOrDefault().DcDate.Value.ToString("dd/MM/yyyy"));
                        HtmlText = HtmlText.Replace("[[ReturnType]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().ReturnType.ToString()) ? "" : receiptDetailslist.FirstOrDefault().ReturnType);
                        HtmlText = HtmlText.Replace("[[GateRef]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().GateEntryNo.ToString()) ? "" : receiptDetailslist.FirstOrDefault().GateEntryNo);
                        string Text = "";
                        if (receiptDetailslist.FirstOrDefault().ModeofTransport == "1")
                        {
                            Text = "Two Wheeler";
                            HtmlText = HtmlText.Replace("[[ModeOfTransport]]", Text);
                        }
                        else if (receiptDetailslist.FirstOrDefault().ModeofTransport == "2")
                        {
                            Text = "Four Wheeler";
                            HtmlText = HtmlText.Replace("[[ModeOfTransport]]", Text);
                        }
                        else if (receiptDetailslist.FirstOrDefault().ModeofTransport == "3")
                        {
                            Text = "In person";
                            HtmlText = HtmlText.Replace("[[ModeOfTransport]]", Text);
                        }
                        else
                        {
                            HtmlText = HtmlText.Replace("[[ModeOfTransport]]", " ");
                        }
                        HtmlText = HtmlText.Replace("[[TransportCompany]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().TransportCompany) ? " " : receiptDetailslist.FirstOrDefault().TransportCompany);
                        HtmlText = HtmlText.Replace("[[VehicleArrangedBy]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().VehicleArrangedBy) ? " " : receiptDetailslist.FirstOrDefault().VehicleArrangedBy);
                        HtmlText = HtmlText.Replace("[[VehicleNo]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().VehicleNo) ? " " : receiptDetailslist.FirstOrDefault().VehicleNo);

                        HtmlText = HtmlText.Replace("[[DispatchDate]]", string.IsNullOrEmpty(receiptDetailslist.FirstOrDefault().DcDate.ToString()) ? " " : receiptDetailslist.FirstOrDefault().DcDate.Value.ToString("dd/MM/yyyy"));
                        string EmployeeID = receiptDetailslist.FirstOrDefault().PreparedBy;
                        if (EmployeeID != null)
                        {
                            employeeMaster = GetEmployee(EmployeeID);
                            HtmlText = HtmlText.Replace("[[PreparedBy]]", employeeMaster != null &&  employeeMaster.Count>0? !string.IsNullOrEmpty(employeeMaster.FirstOrDefault().Name) ? " " : employeeMaster.FirstOrDefault().Name:"");
                        }



                        string pathName = "";
                        string pathName_ = "";
                       // string path_ = "";
                       // string docTypeCode = "";
                        String timeStamp = GetTimestamp(new DateTime());
                        pathName = receiptDetailslist.FirstOrDefault().GateEntryNo;
                        pathName_ = pathName.Replace(" ", "").Replace("/", ",");

                        if (pagebreak == 1)
                        {
                            HtmlText = HtmlText.Replace("wrapper", "").ToString();
                        }
                        tempExport.Append(HtmlText);
                        string fileNames = HostingEnvironment.MapPath("~/PdfGeneration/SavePdf/");
                        FullName = fileNames + pathName_;
                    }
                }
            }
            if (FullName != "")
            {
                Document doc_ = new Document();
                PdfPTable tableLayout_ = new PdfPTable(7);
                //PdfWriter writer_ = PdfWriter.GetInstance(doc_, new FileStream(FullName, FileMode.Create, FileAccess.ReadWrite));
                PdfWriter writer_ = PdfWriter.GetInstance(doc_, new FileStream(FullName, FileMode.Create));
                doc_.Open();
                StringReader sr_ = new StringReader(tempExport.ToString());
                XMLWorkerHelper.GetInstance().ParseXHtml(writer_, doc_, sr_);
                doc_.Close();
                string targetPath = "";
                targetPath = ConfigurationManager.AppSettings["FTPIP"];
                sourceFile = System.IO.Path.Combine(FullName);
                HtmlText = "";
            }


            return sourceFile;
        }

        public static string GetReceiptItems(GeneratePdf receiptDetails, int count_)
        {
            List<MMS.Web.Models.GateEntryModel.MaterialMaster> outMaster = new List<MMS.Web.Models.GateEntryModel.MaterialMaster>();
            List<UomMaster> uomMaster = new List<UomMaster>();
            string invoiceDynamicRow = string.Empty;
            string MaterialNameId = string.Empty;
            string HSNCode = string.Empty;
            string UomId = string.Empty;
            string TotalQty = string.Empty;
            string Rate = string.Empty;
            string Value = string.Empty;
            string Piecies = string.Empty;

            string MaterialName = receiptDetails.MaterialNameId.ToString();
            if (MaterialName != null)
            {
                outMaster = GetMaterialName(MaterialName);
            }

            HSNCode = string.IsNullOrEmpty(receiptDetails.HSNCode) ? "" : receiptDetails.HSNCode.ToString();
            Piecies = string.IsNullOrEmpty(receiptDetails.Piecies) ? "" : receiptDetails.Piecies.ToString();

            string UomName = receiptDetails.UomId.ToString();
            if (UomName != null)
            {
                uomMaster = GetUomName(UomName);
                if (uomMaster.Count > 0)
                {
                    UomName = uomMaster.FirstOrDefault().LongUnitName;
                }
            }
            string qty = "";
            if (receiptDetails.TotalQty != null)
            {               
                qty = String.Format("{0:0.00}", receiptDetails.TotalQty.ToString());
                TotalQty = string.IsNullOrEmpty(receiptDetails.TotalQty.ToString()) ? "" : (receiptDetails.TotalQty).ToString();
            }
            else
            {
                TotalQty = string.IsNullOrEmpty(receiptDetails.TotalQty.ToString()) ? "" :(receiptDetails.TotalQty).ToString();
            }
            if (receiptDetails.Rate != null)
            {
                Rate = string.IsNullOrEmpty(receiptDetails.Rate.ToString()) ? "" : (receiptDetails.Rate).ToString();
            }
            else
            {
                Rate = string.IsNullOrEmpty(receiptDetails.Rate.ToString()) ? "" : (receiptDetails.Rate).ToString();
            }
            if (receiptDetails.Value != null)
            {
                Value = string.IsNullOrEmpty(receiptDetails.Value.ToString()) ? "" : (receiptDetails.Value).ToString();
            }
            else
            {
                Value = string.IsNullOrEmpty(receiptDetails.Value.ToString()) ? "" : (receiptDetails.Value).ToString();
            }

            invoiceDynamicRow += "<tr>" +
                                        "<td style='border-bottom: 1px solid #333;border-right: 1px solid #333;font-size:8px;font-weight:normal;font-size:10px;text-align:center;' cellpadding='5'>" + count_ + "</td>" +
                                         "<td style='border-bottom: 1px solid #333;border-right: 1px solid #333;font-size:8px;font-weight:normal;font-size:10px;text-align:center;' cellpadding='5'>" + outMaster.FirstOrDefault().MaterialDescription + "</td>" +
                                        "<td style='border-bottom: 1px solid #333;border-right: 1px solid #333;font-size:8px;font-weight:normal;font-size:10px;text-align:center;' cellpadding='5'>" + HSNCode + "</td>" +
                                        "<td style='border-bottom: 1px solid #333;border-right: 1px solid #333;font-size:8px;font-weight:normal;font-size:10px;text-align:center;' cellpadding='5'>" + Rate + "</td>" +
                                        "<td style='border-bottom: 1px solid #333;border-right: 1px solid #333;font-size:8px;font-weight:normal;font-size:10px;text-align:center;' cellpadding='5'>" + UomName + "</td>" +
                                         "<td style='border-bottom: 1px solid #333;border-right: 1px solid #333;font-size:8px;font-weight:normal;font-size:10px;text-align:center;' cellpadding='5'>" + Piecies + "</td>" +
                                        "<td style='border-bottom: 1px solid #333;border-right: 1px solid #333;font-size:8px;font-weight:normal;font-size:10px;text-align:center;' cellpadding='5'>" + TotalQty + "</td>" +
                                        "<td style='border-bottom: 1px solid #333;font-weight: normal;font-size:8px;font-size:10px;text-align:center;' cellpadding='5'> " + Value + "</td>" +
                                    "</tr>";

            return invoiceDynamicRow;
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }


        //public static GateEntryOutwardMaster GetSupplier(int SupplierId)
        //{
        //    GateEntryOutwardMaster state = new GateEntryOutwardMaster();
        //    GateEntryOutwardManager manager = new GateEntryOutwardManager();
        //    SupplierMasterManager supplier = new SupplierMasterManager();
        //    {
        //        state = (from x in supplier.Get()
        //                 join y in manager.Get() on x.SupplierMasterId equals y.SupplierId
        //                 where x.SupplierMasterId == SupplierId
        //                 select new
        //                 {
        //                     sup = x.SupplierName,
        //                 });

        //    }
        //    return state;
        //}

        public static List<SupplierMaster> GetSupplier(int SupplierId)
        {
            List<SupplierMaster> supplierlist = new List<SupplierMaster>();
            using (var context = new MMSContext())
            {
                supplierlist = context.Database.SqlQuery<SupplierMaster>("EXEC GetSupplierList @SupplierId={0}", SupplierId).ToList();
            }
            return supplierlist;
        }
        public static List<MMS.Web.Models.GateEntryModel.MaterialMaster> GetMaterialName(string MaterialName)
        {
            List<MMS.Web.Models.GateEntryModel.MaterialMaster> supplierlist = new List<MMS.Web.Models.GateEntryModel.MaterialMaster>();
            using (var context = new MMSContext())
            {
                supplierlist = context.Database.SqlQuery<MMS.Web.Models.GateEntryModel.MaterialMaster>("EXEC GetMaterialList @MaterialNameId={0}", MaterialName).ToList();

            }
            return supplierlist;
        }
        public static List<UomMaster> GetUomName(string UomId)
        {
            List<UomMaster> supplierlist = new List<UomMaster>();
            using (var context = new MMSContext())
            {
                supplierlist = context.Database.SqlQuery<UomMaster>("EXEC GetUomList @UomId={0}", UomId).ToList();

            }
            return supplierlist;
        }
        public static List<EmployNameMaster> GetEmployee(string EmployeeID)
        {
            List<EmployNameMaster> supplierlist = new List<EmployNameMaster>();
            using (var context = new MMSContext2())
            {
                supplierlist = context.Database.SqlQuery<EmployNameMaster>("EXEC GetEmployeeList @EmployeeID={0}", EmployeeID).ToList();
            }
            return supplierlist;
        }
        public static List<Purpose> GetPurpose(string Purpose)
        {
            List<Purpose> purposelist = new List<Purpose>();
            using (var context = new MMSContext())
            {
                purposelist = context.Database.SqlQuery<Purpose>("EXEC GetPurposeList @PurposeId={0}", Purpose).ToList();
            }
            return purposelist;
        }
        public static List<StoreMaster> GetStore(int StoresID)
        {
            List<StoreMaster> storelist = new List<StoreMaster>();
            using (var context = new MMSContext())
            {
                storelist = context.Database.SqlQuery<StoreMaster>("EXEC GetStoreMasterList @StoreMasterId={0}", StoresID).ToList();
            }
            return storelist;
        }

    }
}