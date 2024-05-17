using CrystalDecisions.CrystalReports.Engine;
using Reports.DataSet_;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
 

namespace Reports.View.Views
{
    public partial class StockHistory : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();

        #region "Declarations"
        string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
        SqlConnection SqlCon = new SqlConnection();
        SqlCommand SqlCmd = new SqlCommand();
        DataSet Dst = new DataSet();
        ReportDocument reportDocument = new ReportDocument();
        string StrStartMonth = string.Empty;
        string StrEndMonth = string.Empty;
        string StrEmployee = Convert.ToString(Guid.Empty);
        string StrCompanyCode = Convert.ToString(Guid.Empty);
        string StrDepartmentCode = Convert.ToString(Guid.Empty);
        string DepartmentNameHeader = string.Empty;
        string CompanyNameHeader = string.Empty;
        string Position = string.Empty;
        //string CategoryCode

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (crystalReport == null)
                crystalReport.Close();
            crystalReport = new ReportDocument();
            GC.Collect();

            LoadReport();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }

        #region "LoadReport"

        public void LoadReport()
        {
            // ErrorLog log = new ErrorLog();
            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
         

                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "ReportTest";
                cmd.CommandType = CommandType.StoredProcedure;
               
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                StockManagementSet dsRpt = new StockManagementSet();
                da.Fill(dsRpt, "DataTable1");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];                
                crystalReport.Load(Server.MapPath("~/ReportPage/StockManagement.rpt"));   
                crystalReport.SetDataSource(dsRpt);
                StockReportViewer.ReportSource = crystalReport;
                StockReportViewer.DataBind();
            }
            catch (Exception Ex)
            {
               
            }

        }




        public string getDepartmentName(string StrAtdDivcode)
        {
            string Address = "";
            SqlConnection con = null;
            SqlDataReader Rdr = null;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select DepartmentName from tbl_EmpDepartment where EmpDepartmentCodePK='" + StrAtdDivcode + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                Address = Rdr[0].ToString();
            }
            return Address;
        }
        public string getcompanyName(string StrAtdCCode)
        {
            string CompanyName = "";
            string Address = "";
            string Address1 = "";
            string CompanyAddress = "";
            string PinCode = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select FullName,Address1,Address2,Pincode from tbl_Company where CompanyCodePK='" + StrAtdCCode + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                CompanyName = Rdr[0].ToString();
                Address = Rdr[1].ToString();
                Address1 = Rdr[2].ToString();
                PinCode = Rdr[3].ToString();
                CompanyAddress = CompanyName + "," + Address + "," + Address1 + "," + PinCode;
            }
            return CompanyAddress;
        }
        #endregion
    }
}