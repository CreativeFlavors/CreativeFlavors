using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
using System.Globalization;
using Reports.Common;
using System.Web.UI;
using Reports.Models;
using System.Collections.Generic;

namespace Reports.View.Views
{
    public partial class GrnRegister : System.Web.UI.Page
    {

        ReportDocument crystalReport = new ReportDocument();
        string StoreNo = "", From = "", To = "", Group = "", Category = "", url = "", materialType = "", material = "", supplier = "", grnoption = "",reportOption="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null && Request.QueryString["StoreNo"] != null)
                {
                    Session["Material"] = "";
                    Session["From"] = "";
                    Session["GRNoption"] = "";
                    Session["To"] = "";
                    Session["Group"] = "";
                    Session["StoreNo"] = "";
                    Session["Category"] = "";
                    Session["MaterialType"] = "";
                    Session["Supplier"] = "";
                    Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));
                    Session["From"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["To"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
                    Session["Supplier"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Supplier"]));
                    Session["GRNoption"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["GRNoption"])); 
                    Session["ReportOption"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ReportOption"])); 
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                //if (!string.IsNullOrEmpty(Session["From"].ToString()) && !string.IsNullOrEmpty(Session["To"].ToString()) && !string.IsNullOrEmpty(Session["StoreNo"].ToString()))
                //{
                From = Session["From"].ToString();
                To = Session["To"].ToString();
                Group = Session["Group"].ToString();
                StoreNo = Session["StoreNo"].ToString();
                Category = Session["Category"].ToString();
                materialType = Session["MaterialType"].ToString();
                material = Session["Material"].ToString();
                supplier = Session["Supplier"].ToString();
                grnoption = Session["GRNoption"].ToString();
                reportOption = Session["ReportOption"].ToString();
                //}

                if (crystalReport == null)
                    crystalReport.Close();
                crystalReport = new ReportDocument();
                GC.Collect();

                LoadReport();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void LoadReport()
        {

            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (reportOption != "GRN")
                {
                    cmd.Connection = Con;
                    Con.Open();
                    cmd.CommandText = "spGrnRegister";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                   //DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                    DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    cmd.Parameters.AddWithValue("@From", FromDate);
                    cmd.Parameters.AddWithValue("@To", ToDate);
                    cmd.Parameters.AddWithValue("@StoreNo", StoreNo);
                    cmd.Parameters.AddWithValue("@Group", Group);
                    cmd.Parameters.AddWithValue("@Category", Category);
                    cmd.Parameters.AddWithValue("@MaterialType", materialType);
                    cmd.Parameters.AddWithValue("@MaterialMaster", material);
                    cmd.Parameters.AddWithValue("@Supplier", supplier);
                    cmd.Parameters.AddWithValue("@grnOption", grnoption);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = 0;
                    Reports.DataSet_.GrnRegisterReport rpt = new Reports.DataSet_.GrnRegisterReport();
                    //StockStatement dsRpt = new StockStatement();
                    //DataSet tt = new DataSet();
                    //da.Fill(tt);
                   
                    da.Fill(rpt, "spGrnRegister");
                    DataTable dt = new DataTable();
                    dt = rpt.Tables[1];
                    
                    if (reportOption == "invoice with supplier")
                    {
                        crystalReport.Load(Server.MapPath("~/ReportPage/GrnRegisterReportNew.rpt"));
                    }
                    else if (reportOption == "Group")
                    {
                        crystalReport.Load(Server.MapPath("~/ReportPage/GrnRegisterReport.rpt"));
                    }
                    else if (reportOption == "GRN")
                    {
                        crystalReport.Load(Server.MapPath("~/ReportPage/GrnRegisterRepotGRN.rpt"));
                    }
                    TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["GrnDate"];
                    TextObject MaterialType = (TextObject)crystalReport.ReportDefinition.ReportObjects["MaterialType"];
                    if (materialType == "1")
                    {
                        MaterialType.Text = "Material Type : Local";

                    }
                    if (materialType == "2")
                    {
                        MaterialType.Text = "Material Type : Customer Import";
                    }
                    if (materialType == "3")
                    {
                        MaterialType.Text = "Material Type : Direct Import";
                    }
                    Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    crystalReport.SetDataSource(rpt.Tables[1]);
                    Email.GetCompany(url, crystalReport);
                    GrnRegisterReportViewer.ReportSource = crystalReport;
                    GrnRegisterReportViewer.DataBind();
                }
                else {

                     cmd.Connection = Con;
                    Con.Open();



                    //SqlDataAdapter datedd = new SqlDataAdapter("Select * from Users", Con);
                    //DataSet ds = new DataSet();
                    //datedd.Fill(ds, "Users");
                    //List<Test> asdfasd = new List<Test>();
                    
                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                    //    Test adsa = new Test()
                    //    {
                    //        UserID = Convert.ToInt32(ds.Tables[0].Rows[i]["UserID"]),
                    //       Email = ds.Tables[0].Rows[i]["Email"].ToString(),
                    //        Password = ds.Tables[0].Rows[i]["Password"].ToString(),
                    //        //PasswordSalt = ds.Tables[0].Rows[i]["PasswordSalt"].ToString(),
                    //       // FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString(),
                    //        //LastName= ds.Tables[0].Rows[i]["LastName"].ToString(),
                    //       // UserType= Convert.ToInt32(ds.Tables[0].Rows[i]["UserType"]),
                    //       // IsActive= Convert.ToBoolean(ds.Tables[0].Rows[i]["IsActive"]),
                    //       // CreatedDate= Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]),
                    //       //UpdatedDate= Convert.ToDateTime(ds.Tables[0].Rows[i]["UpdatedDate"]),
                    //       // CreatedBy= Convert.ToInt32(ds.Tables[0].Rows[i]["CreatedBy"]),
                    //       //UpdatedBy= Convert.ToInt32(ds.Tables[0].Rows[i]["UpdatedBy"]),
                    //      };

                    //    asdfasd.Add(adsa);
                    //}

                    //if (reportOption == "GRN")
                    //{
                    //    crystalReport.Load(Server.MapPath("~/ReportPage/Crystalgrnnew.rpt"));
                    //}

                    //crystalReport.SetDataSource(asdfasd);
                    //Email.GetCompany(url, crystalReport);
                    //GrnRegisterReportViewer.ReportSource = crystalReport;
                    //GrnRegisterReportViewer.DataBind();


                    cmd.CommandText = "spGrnRegister_Grnvalue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                    cmd.Parameters.AddWithValue("@From", FromDate);
                    cmd.Parameters.AddWithValue("@To", ToDate);
                    cmd.Parameters.AddWithValue("@Supplier", supplier);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.SelectCommand.CommandTimeout = 0;
                    Reports.DataSet_.GrnRegister_Grnvalue rpt = new Reports.DataSet_.GrnRegister_Grnvalue();
                    StockStatement dsRpt = new StockStatement();
                    da.Fill(rpt, "spGrnRegister_Grnvalue");
                    DataTable dt = new DataTable();
                    //   dt = rpt.Tables[1];
                    if (reportOption == "GRN")
                    {
                        crystalReport.Load(Server.MapPath("~/ReportPage/GrnRegisterRepotGRN.rpt"));
                    }
                    //TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["GrnDate"];
                    //TextObject MaterialType = (TextObject)crystalReport.ReportDefinition.ReportObjects["MaterialType"];
                    //if (materialType == "1")
                    //{
                    //    MaterialType.Text = "Material Type : Local";

                    //}
                    //if (materialType == "2")
                    //{
                    //    MaterialType.Text = "Material Type : Customer Import";
                    //}
                    //if (materialType == "3")
                    //{
                    //    MaterialType.Text = "Material Type : Direct Import";
                    //}
                    // Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    crystalReport.SetDataSource(rpt.Tables[0]);
                    Email.GetCompany(url, crystalReport);
                    GrnRegisterReportViewer.ReportSource = crystalReport;
                    GrnRegisterReportViewer.DataBind();
                }
              
             
                // Email.SendEmail(crystalReport, "GRN Register Report", "GRN Register Report");
            }
            catch (Exception Ex)
            {

            }

        }

        protected void ExportPDF(object sender, ImageClickEventArgs e)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand("Usp_getPersonRecords", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable datatable = new DataTable();
            da.Fill(datatable); // getting value according to imageID and fill dataset

            ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
            crystalReport.Load(Server.MapPath("~/CrystalPersonInfo.rpt")); // path of report 
            crystalReport.SetDataSource(datatable); // binding datatable
            GrnRegisterReportViewer.ReportSource = crystalReport;

            crystalReport.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");
            //here i have use [ CrystalDecisions.Shared.ExportFormatType.PortableDocFormat ] to Export in PDF

        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
    }
}