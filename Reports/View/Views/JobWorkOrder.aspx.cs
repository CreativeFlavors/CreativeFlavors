using System;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Reports.DataSet_;
using Reports.Common;
using MMS.Common;
using System.Linq;


namespace Reports.View.Views
{
    public partial class JobWorkOrder : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string Jobworktype_Id = "", IssueType = "", url = "";
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

                if (Request.QueryString["Jobworktype_Id"] != null)
                {
                    
                    Session["IssueType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IssueType"]));
                    Session["Jobworktype_Id"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Jobworktype_Id"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }


                if (!string.IsNullOrEmpty(Session["Jobworktype_Id"].ToString()))
                {
                    Jobworktype_Id = Session["Jobworktype_Id"].ToString();
                    IssueType = Session["IssueType"].ToString();
                  
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
                cmd.CommandText = "Job_Order_report_Page";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@jobsheet_pair_Code_id", Jobworktype_Id);
                cmd.Parameters.AddWithValue("@Type", IssueType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Job_Order_reportDataSet dsRpt = new Job_Order_reportDataSet();
                da.Fill(dsRpt, "Job_Order_reportDataSet");
                DataTable dt = new DataTable();
                DataTable dt_new = new DataTable();
                //   dt = dsRpt.Tables[0];
                dt = dsRpt.Tables[0];
                dt_new = dsRpt.Tables[1];
                decimal sum = 0;
                foreach (DataRow dr in dt_new.Rows)
                {
                    sum += Convert.ToDecimal(dr["GSt_amt"]);
                }
                string StoreName = dt_new.Rows[0]["StoreName"].ToString();
                string CategoryName = dt_new.Rows[0]["CategoryName"].ToString();
                string Address = dt_new.Rows[0]["Address"].ToString();
                string Email_ = dt_new.Rows[0]["Email"].ToString();
                string Cst_No_Head = dt_new.Rows[0]["Cst_No_Head"].ToString();
                string Terms_Condition = dt_new.Rows[0]["Terms_Condition"].ToString();
                string Value = dt_new.Rows[0]["Value"].ToString();
                string GSt = dt_new.Rows[0]["GSt"].ToString();
                //        string GSt_amt_new = dt_new.Rows[0]["GSt_amt"].ToString();
                string GSt_amt_new = sum.ToString();
                string Total_new = dt_new.Rows[0]["Total"].ToString();http://localhost:62966/View/Views/JobWorkOrder.aspx.cs
                string Job_NO = dt_new.Rows[0]["Total"].ToString();
                string Code = dt_new.Rows[0]["Code"].ToString();
                string Date_from = dt_new.Rows[0]["Date_from"].ToString();

                string ProcessName = dt_new.Rows[0]["ProcessName"].ToString();
                string Sf = dt_new.Rows[0]["Sf"].ToString();
                string JW_Name = dt_new.Rows[0]["JW_Name"].ToString();
                string StageName = dt_new.Rows[0]["StageName"].ToString();
                string Finished_MAterial = "";
                string Io_based = "";
                if (IssueType == "JobWork Production")
                {
                    Finished_MAterial = dt_new.Rows[0]["Finished_MAterial"].ToString();
                    Io_based = dt_new.Rows[0]["Io_based"].ToString();
                }

                string value_word = words(Convert.ToDecimal(Total_new),false);

                crystalReport.Load(Server.MapPath("~/ReportPage/JobworkOrder.rpt"));
                // string ImageUrl = "";
                TextObject StoreName_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["StoreName_text_"];
                TextObject CategoryName_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["CategoryName"];
                TextObject Address_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Address"];
                TextObject Email_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Email_"];
                TextObject Cst_No_Head_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Cst_No_Head"];
                TextObject Terms_Condition_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Terms_Condition"];


                TextObject Value_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Value"];
                TextObject GSt_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["GSt"];
                TextObject GSt_amt_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["GSt_amt_new"];
                TextObject Total_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Total_new"];
                TextObject Code_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Code"];
                TextObject Date_from_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Date_from"];
                TextObject value_word_text = (TextObject)crystalReport.ReportDefinition.ReportObjects["value_word"];


                TextObject ProcessName_Text = (TextObject)crystalReport.ReportDefinition.ReportObjects["ProcessName"];
                TextObject Sf_Text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Sf"];
                TextObject StageName_Text = (TextObject)crystalReport.ReportDefinition.ReportObjects["StageName"];
               
                TextObject Finished_MAterial_Text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Finished_MAterial"];
                TextObject Io_based_Text = (TextObject)crystalReport.ReportDefinition.ReportObjects["Io_based"];
                TextObject JW_Name_Text = (TextObject)crystalReport.ReportDefinition.ReportObjects["JW_Name"];
                //TextObject BuyerItemCode_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["BuyerItemCode"];
                //TextObject Color_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Color"];
                //TextObject Grade_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Grade"];
                //TextObject Uom_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Uom"];
                //TextObject Uom1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["Uom_"];

                //TextObject Uom2 = (TextObject)crystalReport.ReportDefinition.ReportObjects["Uom1"];


                StoreName_text.Text = "Store : " + StoreName.ToString();
                CategoryName_text.Text = "Category :  " + CategoryName.ToString();
                Address_text.Text = "Address : " + Address.ToString();
                Email_text.Text = "Email :  " + Email_.ToString();
                Cst_No_Head_text.Text = "GST NO :  " + Cst_No_Head.ToString();
                Terms_Condition_text.Text = "Category : " + Terms_Condition.ToString();
                Value_text.Text = Value.ToString();
                GSt_text.Text = GSt.ToString();
                GSt_amt_text.Text = GSt_amt_new.ToString();
                Total_text.Text = Total_new.ToString();
                Code_text.Text = Code.ToString();
                Date_from_text.Text = Date_from.ToString();
                value_word_text.Text = "In Words : " + value_word.ToString();

                ProcessName_Text.Text = "ProcessName : " + ProcessName.ToString();
                Sf_Text.Text = "Stage From : " + Sf.ToString();
                StageName_Text.Text = "Stage To : " + StageName.ToString();
              
                    Finished_MAterial_Text.Text = "Style : " + Finished_MAterial.ToString();
                    Io_based_Text.Text = "Io : " + Io_based.ToString();
               
                JW_Name_Text.Text = JW_Name.ToString();

                crystalReport.SetDataSource(dsRpt);
                Common.Email.GetCompany(url, crystalReport);
                //PurchaseOrderReportViewer.ReportSource = crystalReport;
                //PurchaseOrderReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "Purchaser Order Report", "Purchaser Order Report");


                crystalReport.SetDataSource(dsRpt.Tables[0]);
                Email.GetCompany_jobwork(url, crystalReport);
                //   crystalReport.SetParameterValue("imageUrl", ImageUrl);
                JobOrderReportViewer.ReportSource = crystalReport;
                JobOrderReportViewer.DataBind();
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