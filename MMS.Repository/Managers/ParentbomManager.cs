using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
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
    public class ParentbomManager : IParentbomServices
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<parentbom> parentbomRepository;
        public ParentbomManager()
        {
            parentbomRepository = unitOfWork.Repository<parentbom>();

        }

        public parentbom Post(parentbom arg)
        {
            parentbom parentboms = new parentbom();
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                parentbomRepository.Insert(arg);
                parentboms = arg;

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return parentboms;
        }
        public parentbom GetBomNO(string BomNO)
        {
            parentbom billOfMaterial = new parentbom();
            if (BomNO != "")
            {
                billOfMaterial = parentbomRepository.Table.Where(x => x.BomNo == BomNO && x.IsDelete == true).FirstOrDefault();
            }
            return billOfMaterial;
        }
        public bool Delete(int id, bool IsChecked)
        {
            bool result = false;
            try
            {
                parentbom model = parentbomRepository.GetById(id);
                model.IsDelete = IsChecked;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                parentbomRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public parentbom Getbomid(int BOMID)
        {
            parentbom billOfMaterial = new parentbom();
            if (BOMID != null)
            {
                billOfMaterial = parentbomRepository.Table.Where(x => x.BomId == BOMID ).FirstOrDefault();
            }
            return billOfMaterial;
        }
        public string GetLastbomNumber()
        {
            parentbom billOfMaterial = new parentbom();
            string lastBomNo = "";
            List<parentbom> billOfMaterialList = new List<parentbom>();
            billOfMaterialList = parentbomRepository.Table.ToList<parentbom>();
            if (billOfMaterialList.Count > 0)
            {
                var billOfMaterial_ = (from u in parentbomRepository.Table
                                       select u.BomId).Max();

                billOfMaterial = parentbomRepository.Table.Where(x => x.BomId == billOfMaterial_).SingleOrDefault();
                lastBomNo = billOfMaterial.BomNo;
            }

            return lastBomNo;
        }
        public List<parentbom> Get()
        {

            List<parentbom> customertransaction = new List<parentbom>();
            try
            {
                customertransaction = parentbomRepository.Table.ToList<parentbom>().ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return customertransaction;
        }
        public bool Put(parentbom arg)
        {
            throw new NotImplementedException();
        }
    }
}
