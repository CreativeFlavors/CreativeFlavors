using CrystalDecisions.CrystalReports.Engine;
using Reports.Common;
using Reports.DataSet_;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reports.View.Views
{
    public partial class GSTReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string StoreNo = "", From = "", To = "", Group = "", Category = "", url = "", materialType = "", material = "",grnOptiobn="";
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
                    Session["To"] = "";
                    Session["Group"] = "";
                    Session["StoreNo"] = "";
                    Session["Category"] = "";
                    Session["MaterialType"] = "";
                    Session["GRNOption"] = "";
                    Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));
                    Session["From"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["To"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
                    Session["GRNOption"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["GRNOption"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                //if (!string.IsNullOrEmpty(Session["From"].ToString()) && !string.IsNullOrEmpty(Session["To"].ToString()) && !string.IsNullOrEmpty(Session["StoreNo"].ToString()) )
                //{
                From = Session["From"].ToString();
                To = Session["To"].ToString();
                Group = Session["Group"].ToString();
                StoreNo = Session["StoreNo"].ToString();
                Category = Session["Category"].ToString();
                materialType = Session["MaterialType"].ToString();
                material = Session["Material"].ToString();
                grnOptiobn = Session["GRNOption"].ToString();
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

                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "GST";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                cmd.Parameters.AddWithValue("@From", FromDate);
                cmd.Parameters.AddWithValue("@To", ToDate);
                cmd.Parameters.AddWithValue("@StoreNo", StoreNo);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@MaterialType", materialType);
                cmd.Parameters.AddWithValue("@MaterialMaster", material);
                cmd.Parameters.AddWithValue("@GRNOption", grnOptiobn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                GSTSet dsRpt = new GSTSet();
                da.Fill(dsRpt, "GST");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/GST_Report.rpt"));

                //TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["StockDate"];
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
                //Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                crystalReport.SetDataSource(dt);              
                if (url != null)
                {
                    string ImageUrl = "";
                    TextObject Company = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtCompany"];
                    TextObject Address = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtAddress"];

                    if (url.ToLower().Contains("unit2"))
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EncoShoesLogo.png");
                        Company.Text = "Enco Shoes - Unit II";
                        Company.Text += " # 350/3B Ammoor Road Samathuvapuram   Ranipet - 632402";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }
                    else if (url.ToLower().Contains("endura"))
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EnduraLogo.png");
                        Company.Text = "Endura Exports";
                        Address.Text = " # 3, Sri Balaji Nagar,Puliyambedu,Chennai ,Tamil Nadu- 600077,GSTIN-33AAGFE4867R1ZR";

                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }
                    else
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EncoShoesLogo.png");
                        Company.Text = "Enco Shoes";
                        Address.Text = " # 3-B, Puliyambedu Road,Velappanchavadi,  Chennai - 600077.";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }

                }
                GSTReportViewer.ReportSource = crystalReport;
                GSTReportViewer.DataBind();
                //Email.SendEmail(crystalReport , "Stock Statement Report", "Stock Statement Report");
            }
            catch (Exception Ex)
            {

            }

        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
    }
}