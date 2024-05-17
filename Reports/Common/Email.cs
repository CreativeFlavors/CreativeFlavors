using System;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using System.Net.Mail;
using System.Net;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Reports.Common
{ 
    public class Email
    {
        public static void SendEmail(ReportDocument crystalReport, string subject, string body)
        {
            try
            {
                using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPTo"]))
                {                    
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.CC.Clear();
                   string[] ccAddress =  ConfigurationManager.AppSettings["SMTPCc"].Split(';'); 
                    if (ccAddress != null && ccAddress.Length > 0)
                    {
                        foreach (string address in ccAddress)
                        {
                            mm.CC.Add(new MailAddress(address));
                        }
                    }                    
                    mm.Attachments.Add(new Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), subject+".Pdf"));
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["SMTPServerAddress"];
                    NetworkCredential credential = new NetworkCredential();
                    credential.UserName = ConfigurationManager.AppSettings["SMTPUserName"];
                    credential.Password = ConfigurationManager.AppSettings["SMTPPassword"];
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = credential;
                    smtp.Port =Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]); 
                    smtp.EnableSsl = true;
                    smtp.Send(mm);
                }
            }
            catch (Exception ex)
            {
                string Test = ex.Message;
            }


        }
        public static void GetCompany(string url, ReportDocument crystalReport)
        {
            try
            {
                 
                if (url != null)
                {
                    string ImageUrl = "";
                    TextObject Company = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtCompany"];
                    
                    TextObject Address = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtAddress"];

                    if (url.ToLower().Contains("unit2"))
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EncoShoesLogo.png");
                        Company.Text = "Enco Shoes - Unit II";
             
                        Address.Text = " # 350/3B Ammoor Road Samathuvapuram   Ranipet - 632402";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }
                    else if (url.ToLower().Contains("endura"))
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EnduraLogo.png");
                        Company.Text = "Endura Exports";
                        //Address.Text = " # 3, Sri Balaji Nagar,Puliyambedu,Chennai ,Tamil Nadu- 600077,GSTIN-33AAGFE4867R1ZR";
                        Address.Text = "#18,MGR Salai,Nagalkeni,Chromepet,Chennai, Tamil Nadu-600044, GSTIN-33AAGFE4867R1ZR";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }                  
                    else
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EncoShoesLogo.png");
                        Company.Text = "Enco Shoes";
                        Address.Text = " # 3-B, Puliyambedu Road,Velappanchavadi,  Chennai - 600077,GSTIN:33AABFE1984E1ZR";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    } 

                }
                
            }
            catch (Exception ex)
            {
                string Test = ex.Message;
            }

        }
        public static void GetCompany_jobwork(string url, ReportDocument crystalReport)
        {
            try
            {

                if (url != null)
                {
                    string ImageUrl = "";
                    TextObject Company = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtCompany"];
                    TextObject Company1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtCompany_1"];
                    TextObject Company2 = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtCompany_2"];
                    TextObject Address = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtAddress"];
                    TextObject Address1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["txtAddress1"];

                    if (url.ToLower().Contains("unit2"))
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EncoShoesLogo.png");
                        Company.Text = "Enco Shoes - Unit II";
                        Company1.Text = "Enco Shoes - Unit II";
                        Company2.Text = "Enco Shoes - Unit II";
                        Address.Text = " # 350/3B Ammoor Road Samathuvapuram   Ranipet - 632402";
                        Address1.Text = " # 350/3B Ammoor Road Samathuvapuram   Ranipet - 632402";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }
                    else if (url.ToLower().Contains("endura"))
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EnduraLogo.png");
                        Company.Text = "Endura Exports";
                        Company1.Text = "Endura Exports";
                        Company2.Text = "Endura Exports";
                        Address.Text = " # 3, Sri Balaji Nagar,Puliyambedu,Chennai ,Tamil Nadu- 600077,GSTIN-33AAGFE4867R1ZR";
                        Address.Text = " # 3, Sri Balaji Nagar,Puliyambedu,Chennai ,Tamil Nadu- 600077,GSTIN-33AAGFE4867R1ZR";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }
                    else
                    {
                        ImageUrl = System.Web.HttpContext.Current.Server.MapPath("~/Images/EncoShoesLogo.png");
                        Company.Text = "Enco Shoes";
                        Company1.Text = "Enco Shoes";
                        Company2.Text = "Enco Shoes";
                        Address.Text = " # 3-B, Puliyambedu Road,Velappanchavadi,  Chennai-600077 ,GSTIN:33AABFE1984E1ZR";
                        Address1.Text = " # 3-B, Puliyambedu Road,Velappanchavadi,  Chennai - 600077 ,GSTIN:33AABFE1984E1ZR";
                        crystalReport.SetParameterValue("imageUrl", ImageUrl);
                    }

                }

            }
            catch (Exception ex)
            {
                string Test = ex.Message;
            }


        }
        public static string getStoreName(int storeMasterid)
        {
            string StoreName = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select StoreName from StoreMaster where StoreMasterId = '" + storeMasterid + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                StoreName = Rdr[0].ToString();
            }
            return StoreName;
        }
        public static string getBOMID(string BomNo)
        {
            List<string> bomidList = new List<string>();
            SqlConnection con = null;
        
            string bomid = "";
            string[] bomidArray = BomNo.Split(',');
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                foreach (var item in bomidArray)
                {
                    SqlDataReader Rdr = null;
                    SqlCommand Cmd = new SqlCommand("select BOMID from bom where isdeleted=0 and  bomno = '" + item + "'", con);
                   
                    Rdr = Cmd.ExecuteReader();
                    Rdr.Read();                  
                    bomid += Rdr[0].ToString() + ",";
                    Rdr.Close();
                }
            }
            return bomid.TrimEnd();
        }
        public static string getCategoryName(int CategoryID)
        {
            string CategoryName = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select categoryname from materialcategorymaster where MaterialCategoryMasterId = '" + CategoryID + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                CategoryName = Rdr[0].ToString();
            }
            return CategoryName;
        }
        public static string getBomNoName(string Bomid)
        {
            string bomnoName = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select bomno from bom where bomid = '" + Bomid + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                bomnoName = Rdr[0].ToString();
            }
            return bomnoName;
        }
        public static string getSeasonName(string SeasonID)
        {
            string bomnoName = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select seasonname from seasonmaster where SeasonMasterId = '" + SeasonID + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                bomnoName = Rdr[0].ToString();
            }
            return bomnoName;
        }
        public static string getGroupName(int groupID)
        {
            string GroupName = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select groupname from materialgroupmaster where MaterialGroupMasterId = '" + groupID + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                GroupName = Rdr[0].ToString();
            }
            return GroupName;
        }
        public static string getMaterialName(int materialID)
        {
            string materialName = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select mnm.MaterialDescription from materialmaster mm join tbl_materialnamemaster mnm on mm.materialname = mnm.MaterialMasterID where mm.MaterialMasterId = = '" + materialID + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                materialName = Rdr[0].ToString();
            }
            return materialName;
        }
        public static string getTotalPairs(string Indentno)
        {
            string totalParirs = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("  select sum(totalordercount) as TotalPairs from indent inner join simplemrp    on ',' + indent.mrpno + ',' like '%,' + cast(simplemrp.mrpno as nvarchar(20)) + ',%' where indent.indentno = '" + Indentno + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                totalParirs = Rdr[0].ToString();
            }
            return totalParirs;
        }
    }
}