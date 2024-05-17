using System;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Reports.DataSet_;
using Reports.Common;
using MMS.Common;

namespace Reports.View.Views
{
    public partial class PurchaseOrderReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string PoNo = "", Group = "", Category = "", Store = "", url = "";
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                if (Request.QueryString["PoNo"] != null)
                {
                    Session["Group"] = "";
                    Session["Store"] = "";
                    Session["Category"] = "";
                    Session["PoNo"] = "";
                    Session["PoNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["PoNo"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }


                if (!string.IsNullOrEmpty(Session["PoNo"].ToString()))
                {
                    Group = Session["Group"].ToString();
                    Store = Session["Store"].ToString();
                    Category = Session["Category"].ToString();
                    PoNo = Session["PoNo"].ToString();
                }
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
        #endregion
        
        #region load Report
        public void LoadReport()

        {

            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "spPurchaseOrderReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PoNo", PoNo);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Store", Store);
                cmd.Parameters.AddWithValue("@Group", Group);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                PurchaseOrder dsRpt = new PurchaseOrder();
                da.Fill(dsRpt, "spPurchaseOrderReport");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/PurchaseOrderReport.rpt"));
                string seasonName = "";
                //seasonName = dt.Rows[0]["SeasonName"].ToString();
                //TextObject SeasonName = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text42"];
               // SeasonName.Text = seasonName;
                TextObject Company = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtCompany"];
                TextObject Address = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtAddress"];
                TextObject PreparedBy = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtPreparedBy"];
                TextObject VerifiedBy = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtVerifiedBy"];
                TextObject ApprovedBy = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtApprovedBy"];
                crystalReport.SetDataSource(dsRpt);
                Common.Email.GetCompany(url, crystalReport);
                PurchaseOrderReportViewer.ReportSource = crystalReport;
                PurchaseOrderReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "Purchaser Order Report", "Purchaser Order Report");
            }

            catch (Exception Ex)
            {
                //Logger.Log(Ex.StackTrace.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //Logger.Log(Ex.Message, this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //Logger.Log(Ex.InnerException.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
        #endregion

        #region PageUnload
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
        #endregion
    }


   
}