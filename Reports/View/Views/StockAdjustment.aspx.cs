﻿using CrystalDecisions.CrystalReports.Engine;
using Reports.Common;
using Reports.DataSet_;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reports.View.Views
{
    public partial class StockAdjustment : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string StoreNo = "", Group = "", Category = "", url = "", materialType = "", material = "", StockNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                Session["Material"] = "";
                Session["From"] = "";
                Session["StockAdjustmentNo"] = "";
                Session["To"] = "";
                Session["Group"] = "";
                Session["StoreNo"] = "";
                Session["Category"] = "";
                Session["MaterialType"] = "";
                Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));
                Session["StockAdjustmentNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StockAdjustmentNo"]));
                Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));

            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                Group = Session["Group"].ToString();
                StoreNo = Session["StoreNo"].ToString();
                Category = Session["Category"].ToString();
                materialType = Session["MaterialType"].ToString();
                material = Session["Material"].ToString();
                StockNo = Session["StockAdjustmentNo"].ToString();
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
                cmd.CommandText = "StockAdjustmentReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Store", StoreNo);
                cmd.Parameters.AddWithValue("@GroupID", Group);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@MaterialType", materialType);
                cmd.Parameters.AddWithValue("@StockAdjustmentNo", StockNo);
                // cmd.Parameters.AddWithValue("@MaterialMaster", material);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.StockAdjustmentDataset dsRpt = new StockAdjustmentDataset();
                da.Fill(dsRpt, "StockAdjustmentReport");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[1];
                crystalReport.Load(Server.MapPath("~/ReportPage/StockAdjustment.rpt"));
                TextObject store = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text12"];
                TextObject categoryName = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text13"];
                TextObject groupName = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text9"];
                TextObject materialName = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text10"];
                TextObject MaterialType_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text11"];
                if (materialType == "1")
                {
                    MaterialType_.Text = ": Local";

                }
                if (materialType == "2")
                {
                    MaterialType_.Text = ": Customer Import";
                }
                if (materialType == "3")
                {
                    MaterialType_.Text = ": Direct Import";
                }
                if (!string.IsNullOrEmpty(StoreNo))
                {
                    store.Text = Email.getStoreName(!string.IsNullOrEmpty(StoreNo) ? Convert.ToInt32(StoreNo) : 0);
                }
                if (!string.IsNullOrEmpty(Category))
                {
                    categoryName.Text = Email.getCategoryName(!string.IsNullOrEmpty(Category) ? Convert.ToInt32(Category) : 0);
                }

                if (Group != "")
                {
                    groupName.Text = Email.getGroupName(!string.IsNullOrEmpty(Group) ? Convert.ToInt32(Group) : 0);
                }

                if (!string.IsNullOrEmpty(material))
                {
                    materialName.Text = Email.getMaterialName(Convert.ToInt32(material));
                }
                crystalReport.SetDataSource(dsRpt.Tables[1]);
                Email.GetCompany(url, crystalReport);
                stockAdjustmentDisplay.ReportSource = crystalReport;
                stockAdjustmentDisplay.DataBind();
                //Email.SendEmail(crystalReport , "Stock Adjustment Report", "Stock Statement Report");
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