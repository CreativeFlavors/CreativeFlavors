using System;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Reports.DataSet_;
using Reports.Common;
using MMS.Common;
using System.Globalization;

namespace Reports.View.Views
{
    public partial class JobProductionJobwork : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string from = "", to = "", type = "", buyer = "", season = "", lOtNo = "", bom = "", io = "", Stage = "", url = "";
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

                if (Request.QueryString["To"] != null)
                {
                    
                    Session["To"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["To"]));
                    Session["Type"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Type"]));
                    Session["Buyer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));

                    Session["Season"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Season"]));
                    Session["LOtNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["LOtNo"]));
                    Session["Bom"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Bom"]));
                    Session["Io"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Io"]));
                    Session["stage"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["stage"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }


                if (!string.IsNullOrEmpty(Session["To"].ToString()))
                {
                    to = Session["To"].ToString();

                    type = Session["Type"].ToString();
                    buyer = Session["Buyer"].ToString();
                    season = Session["Season"].ToString();
                    lOtNo = Session["LOtNo"].ToString();
                    bom = Session["Bom"].ToString();
                    io = Session["Io"].ToString();
                    Stage = Session["stage"].ToString();

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
                DateTime ToDate = DateTime.ParseExact(to, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                cmd.Connection = Con;
                Con.Open();
                if (type == "1")
                {
                    cmd.CommandText = "Production_JobWork_qC";
                }
                else {
                    cmd.CommandText = "Production_JobWork";
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@To", ToDate.ToString("MM/dd/yyyy HH:mm tt"));
                cmd.Parameters.AddWithValue("@Production_Type", type);
                cmd.Parameters.AddWithValue("@Buyer", buyer);
                cmd.Parameters.AddWithValue("@Season", season);
                cmd.Parameters.AddWithValue("@LotNo", lOtNo);
                cmd.Parameters.AddWithValue("@BOMID", bom);
                cmd.Parameters.AddWithValue("@BuyerOrderSlNo", io);
                cmd.Parameters.AddWithValue("@StageMasterId", Stage);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                ProductionData dsRpt = new ProductionData();
                da.Fill(dsRpt, "Production_JobWork");
                DataTable dt = new DataTable();
                DataTable dt_new = new DataTable();
                dt = dsRpt.Tables[0];
                dt_new = dsRpt.Tables[1];

                string BuyerFullName = dt_new.Rows[0]["BuyerFullName"].ToString();
                string SeasonName = dt_new.Rows[0]["SeasonName"].ToString();
                string Lot_no = dt_new.Rows[0]["Lot_no"].ToString();

                crystalReport.Load(Server.MapPath("~/ReportPage/ProductionNewReport.rpt"));

                TextObject BuyerFullName_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["BuyerFullName"];
                TextObject SeasonName_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["SeasonName"];
                TextObject Lot_no_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Lot_no"];

                BuyerFullName_text.Text = "Buyer : " + BuyerFullName.ToString();
                SeasonName_text.Text = "Season :  " + SeasonName.ToString();
                Lot_no_text.Text = "Lot No : " + Lot_no.ToString();
                crystalReport.SetDataSource(dsRpt.Tables[1]);
                Common.Email.GetCompany(url, crystalReport);

             //   crystalReport.SetDataSource(dsRpt.Tables[0]);

                //   crystalReport.SetParameterValue("imageUrl", ImageUrl);
                JobProductionOrderReportViewer.ReportSource = crystalReport;
                JobProductionOrderReportViewer.DataBind();
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
        #region stringword
        public string words(decimal? numbers, Boolean paisaconversion = false)
        {
            var pointindex = numbers.ToString().IndexOf(".");
            var paisaamt = 0;
            if (pointindex > 0)
                paisaamt = Convert.ToInt32(numbers.ToString().Substring(pointindex + 1, 2));

            int number = Convert.ToInt32(numbers);

            if (number == 0) return "Zero";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }

            if (paisaamt == 0 && paisaconversion == false)
            {
                sb.Append("ruppes only");
            }
            else if (paisaamt > 0)
            {
                var paisatext = words(paisaamt, true);
                sb.AppendFormat("rupees {0} paise only", paisatext);
            }
            return sb.ToString().TrimEnd();
        }
        #endregion
    }
}