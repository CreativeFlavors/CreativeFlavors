using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Globalization;
using CrystalDecisions.Shared;
using Reports.DataSet_;

namespace Reports.View.Views
{
    public partial class BinCard : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string MaterialName = "";
        string MaterialType = "";
        string Group = "";
        string Store = "";
        string option = "";
        string from = "";
        string To = "";
        string Category = "";
        string Code = "", url = "", Name = "";
        string StoreName = "";
        string CategoryName = "";
        string GroupName = "";

   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                //  if (Request.QueryString["MaterialName"] != null && Request.QueryString["MaterialType"] != null && Request.QueryString["Group"] != null && Request.QueryString["StoreNo"] != null && Request.QueryString["From"] != null && Request.QueryString["To"] != null && Request.QueryString["Category"] != null && Request.QueryString["Option"] != null)
                // {
                Session["MaterialName"] = "";
                Session["MaterialType"] = "";
                Session["Group"] = "";
                Session["StoreNo"] = "";
                Session["From"] = "";
                Session["To"] = "";
                Session["Category"] = "";
                Session["Option"] = "";
                Session["Name"] = "";
                Session["StoreName"] = "";
                Session["CategoryName"] = "";
                Session["GroupName"] = "";
                if (Request.QueryString["MaterialName"] != "undefined")
                {
                    Session["MaterialName"] = Request.QueryString["MaterialName"];
                }
                Session["MaterialName"] = Request.QueryString["MaterialName"];
                Session["MaterialType"] = Request.QueryString["MaterialType"];
                Session["Group"] = Request.QueryString["Group"];
                Session["StoreNo"] = Request.QueryString["StoreNo"];
                Session["From"] = Request.QueryString["FromDate"];
                Session["Category"] = Request.QueryString["Category"];
                Session["To"] = Request.QueryString["ToDate"];
                Session["Name"] = Request.QueryString["Name"];
                Session["StoreName"] = Request.QueryString["StoreName"];
                Session["CategoryName"] = Request.QueryString["CategoryName"];
                Session["GroupName"] = Request.QueryString["GroupName"];

                // }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                //if (!string.IsNullOrEmpty(Session["MaterialName"].ToString()) 
                //                   && !string.IsNullOrEmpty(Session["MaterialCode"].ToString()) 
                //                   && !string.IsNullOrEmpty(Session["Grade"].ToString()) 
                //                   && !string.IsNullOrEmpty(Session["MaterialType"].ToString())
                //                   && !string.IsNullOrEmpty(Session["Color"].ToString()) 
                //                   && !string.IsNullOrEmpty(Session["Category"].ToString()) 
                //                   && !string.IsNullOrEmpty(Session["Code"].ToString()))
                //               {
                MaterialName = Session["MaterialName"].ToString();
                MaterialType = Session["MaterialType"].ToString();
                Group = Session["Group"].ToString();
                MaterialType = Session["MaterialType"].ToString();
                Store = Session["StoreNo"].ToString();
                Category = Session["Category"].ToString();
                from = Session["From"].ToString();
                To = Session["To"].ToString();
                Name = Session["Name"].ToString();
                StoreName = Session["StoreName"].ToString();
                CategoryName = Session["CategoryName"].ToString();
                GroupName = Session["GroupName"].ToString();
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
                DateTime FromDate_ = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(-1);
                DateTime ToDate_ = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1);

                DateTime FromDate = DateTime.ParseExact(from, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                string a = FromDate_.ToString("MM/dd/yyyy hh:mm:ss:tt");
                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "BIN_CARD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StoreNo", Store);
                cmd.Parameters.AddWithValue("@From", FromDate_.ToString("MM/dd/yyyy HH:mm:ss tt"));
                cmd.Parameters.AddWithValue("@To", ToDate_.ToString("MM/dd/yyyy HH:mm:ss tt"));
                cmd.Parameters.AddWithValue("@From_NEW", FromDate.ToString("MM/dd/yyyy HH:mm:ss tt"));
                cmd.Parameters.AddWithValue("@To_NEW", ToDate.ToString("MM/dd/yyyy HH:mm:ss tt"));
                cmd.Parameters.AddWithValue("@MaterialType", MaterialType);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@MaterialMaster", MaterialName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.BincardSet dsRpt = new DataSet_.BincardSet();
               
                try
                {

                    da.Fill(dsRpt, "BinCard");
                }
                catch (Exception ex)
                {

                }

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
               // DataRow row = dt1.Rows[0];


                dt = dsRpt.Tables[1];
                dt1 = dsRpt.Tables[2];
                string MaterialCode = dt1.Rows[0]["MaterialCode"].ToString();
                string CategoryName = dt1.Rows[0]["CategoryName"].ToString();
                string SizeRangeName = dt1.Rows[0]["SizeRangeName"].ToString();
                string HSNCode = dt1.Rows[0]["HSNCode"].ToString();
                string BuyerItemCode = dt1.Rows[0]["BuyerItemCode"].ToString();
                string Color = dt1.Rows[0]["Color"].ToString();
                string Grade = dt1.Rows[0]["Grade"].ToString();
                string Uom = dt1.Rows[0]["uom"].ToString();

                // string ImageUrl = "";
                crystalReport.Load(Server.MapPath("~/ReportPage/BinCard.rpt"));
                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["StockDate"];
                TextObject materialType = (TextObject)crystalReport.ReportDefinition.ReportObjects["materialType"];
                TextObject M_name = (TextObject)crystalReport.ReportDefinition.ReportObjects["Name"];

                TextObject StoreName_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["StoreName"];
                TextObject CategoryName_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["CategoryName"];
                TextObject GroupName_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["GroupName"];

                TextObject MaterialCode_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["MaterialCode"];
                TextObject SizeRangeName_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["SizeRangeName"];
                TextObject HSNCode_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["HSNCode"];
                TextObject BuyerItemCode_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["BuyerItemCode"];
                TextObject Color_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Color"];
                TextObject Grade_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Grade"];
                TextObject Uom_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Uom"];
                TextObject Uom1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["Uom_"];

                TextObject Uom2 = (TextObject)crystalReport.ReportDefinition.ReportObjects["Uom1"];

                TextObject Uom3 = (TextObject)crystalReport.ReportDefinition.ReportObjects["Uom2"];

                MaterialCode_.Text = "Material Code : " + MaterialCode.ToString();
                SizeRangeName_.Text = "Size : "+ SizeRangeName.ToString();
                HSNCode_.Text = "HSNCode : "+ HSNCode.ToString();
                BuyerItemCode_.Text = "Buyer Code : "+ BuyerItemCode.ToString();
                Color_.Text = "Color : "+ Color.ToString();
                Grade_.Text = "Grade : "+ Grade.ToString();
                Uom_.Text=   Uom.ToString();
                Uom1.Text = Uom.ToString();
                Uom2.Text = Uom.ToString();
                Uom3.Text = Uom.ToString();
                if (MaterialType == "1")
                {
                    materialType.Text = "Material Type : Local";

                }
                if (MaterialType == "2")
                {
                    materialType.Text = "Material Type : Customer Import";
                }
                if (MaterialType == "3")
                {
                    materialType.Text = "Material Type : Direct Import";
                }
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                M_name.Text ="Material Name :" +Name.ToString();
                if (StoreName != "Please Select")
                {
                    StoreName_.Text = "Store Name :" + StoreName.ToString();
                }
                if (CategoryName != "Please Select")
                {
                    CategoryName_.Text = "CategoryName Name :" + CategoryName.ToString();
                }
                if (GroupName != "Please Select")
                {
                    GroupName_.Text = "GroupName Name :" + GroupName.ToString();
                }
                crystalReport.SetDataSource(dsRpt.Tables[1]);
                Email.GetCompany(url, crystalReport);
                //   crystalReport.SetParameterValue("imageUrl", ImageUrl);
                BinCardReportViewer.ReportSource = crystalReport;
                BinCardReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "Bin Card Report", "Bin Card Report");

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