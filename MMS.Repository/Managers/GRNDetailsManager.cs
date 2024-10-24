﻿using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class GRNDetailsManager : IGRNDetailssServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GRNDetails> GRNDetailserrep;
        public GRNDetailsManager()
        {
            GRNDetailserrep = unitOfWork.Repository<GRNDetails>();
        }
        public GRNDetails POST(GRNDetails arg)
        {
            GRNDetails salesorder = new GRNDetails();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = "1";
                GRNDetailserrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<GRNDetails> Get()
        {
            List<GRNDetails> obj = new List<GRNDetails>();
            obj = GRNDetailserrep.Table.ToList<GRNDetails>();
            return obj;
        }
        public List<GRNDetails> GetGRNdtId(int id)
        {
            List<GRNDetails> PoHeader = new List<GRNDetails>();
            PoHeader = GRNDetailserrep.Table.Where(x => x.PoDetailId == id).ToList();
            return PoHeader;
            }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
