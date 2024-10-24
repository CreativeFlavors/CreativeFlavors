﻿using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class SalesorderHD_Manager : ISalesorderHD_Services, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Salesorder_hd> salesorderhdrep;
        public SalesorderHD_Manager()
        {
            salesorderhdrep = unitOfWork.Repository<Salesorder_hd>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public bool PutPartialSoHeader(int poheader)
        {
            bool result = false;
            try
            {
                Salesorder_hd indentPoMapping = salesorderhdrep.Table.Where(p => p.salesorderid_hd == poheader).FirstOrDefault();
                if (indentPoMapping != null)
                {
                    indentPoMapping.Status = "Partial";
                    indentPoMapping.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    indentPoMapping.Updatedby = username;
                    salesorderhdrep.Update(indentPoMapping);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool PutfulfilledSoHeader(int poheader)
        {
            bool result = false;
            try
            {
                Salesorder_hd indentPoMapping = salesorderhdrep.Table.Where(p => p.salesorderid_hd == poheader).FirstOrDefault();
                if (indentPoMapping != null)
                {
                    indentPoMapping.Status = "Full-Fill";
                    indentPoMapping.fullfilldate = DateTime.Now;
                    indentPoMapping.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    indentPoMapping.Updatedby = username;
                    salesorderhdrep.Update(indentPoMapping);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public List<Salesorder_hd> Get()
        {
            List<Salesorder_hd> obj = new List<Salesorder_hd>();
            obj = salesorderhdrep.Table.ToList<Salesorder_hd>();
            return obj;
        }
        public Salesorder_Grid salesorder_get(int SOid)
        { 
            Salesorder_Grid Salesorder_Grid = new Salesorder_Grid();
            Salesorder_Grid = salesorderhdrep.salesorder_gridSearch(SOid);
            return Salesorder_Grid;
        }
        public List<Salesorder_hd> GettypeId(int id)
        {
            List<Salesorder_hd> salesorder = new List<Salesorder_hd>();
            salesorder = salesorderhdrep.Table.Where(x => x.customerid == id).ToList();
            return salesorder;
        }
        public Salesorder_hd GetSOId(int id)
        {
            Salesorder_hd salesorders = new Salesorder_hd();
            salesorders = salesorderhdrep.Table.Where(x => x.salesorderid_hd == id).FirstOrDefault();
            return salesorders;
        }
        public Salesorder_hd POST(Salesorder_hd arg)
        {
            Salesorder_hd salesorder = new Salesorder_hd();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.createdby = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = "Open";
                salesorderhdrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }

       
    }
}
