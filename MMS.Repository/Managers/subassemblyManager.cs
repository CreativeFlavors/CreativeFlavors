using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class subassemblyManager : IsubassemblyServices
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<subassembly> subassemblyRepository;
        public subassemblyManager()
        {
            subassemblyRepository = unitOfWork.Repository<subassembly>();

        }

        public subassembly Getsubassemblyid(int subassemblyid)
        {
            subassembly subassembly = new subassembly();
            if (subassemblyid != null)
            {

                subassembly = subassemblyRepository.Table.Where(x => x.SubassemblyId == subassemblyid && x.IsDeleted == true).FirstOrDefault();
            }
            return subassembly;
        }
        public List<subassembly> GetMaterialList(int BOMID)
        {
            List<subassembly> billOfMaterial = new List<subassembly>();
            if (BOMID != null)
            {
                billOfMaterial = subassemblyRepository.Table
                    .Where(x => x.BomId == BOMID && x.IsDeleted == true)
                    .OrderBy(x => x.BomId)
                    .ToList();
            }
            return billOfMaterial;
        }
        public subassembly Getproductid(int proID)
        {
            subassembly billOfMaterial = new subassembly();
            if (proID != null)
            {
                billOfMaterial = subassemblyRepository.Table.Where(x => x.ProductId == proID && x.IsDeleted == true).FirstOrDefault();
            }
            return billOfMaterial;
        }
        public bool Put(subassembly arg)
        {
            bool result = false;
            try
            {
                subassembly model = subassemblyRepository.Table.Where(p => p.SubassemblyId == arg.SubassemblyId).FirstOrDefault();
                if (model != null)
                {
                    model.SubassemblyId = arg.SubassemblyId;
                    model.IsActive = arg.IsActive;
                    model.BomId = arg.BomId;
                    model.ProductId = arg.ProductId;
                    model.RequiredQty = arg.RequiredQty;
                    model.UpdatedDate = DateTime.Now;
                    string username = "admin";
                    model.UpdatedBy = username;
                    subassemblyRepository.Update(model);
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
        public subassembly Getbommaterialid(int subassemblyid)
        {
            subassembly subassemblydata = new subassembly();
            if (subassemblyid != null)
            {
                subassemblydata = subassemblyRepository.Table.Where(x => x.SubassemblyId == subassemblyid && x.IsDeleted == true).FirstOrDefault();
            }
            return subassemblydata;
        }

        public List<subassembly> GetsubassemblyList(int BOMID)
        {
            List<subassembly> subassemblylist = new List<subassembly>();
            if (BOMID != null)
            {
                subassemblylist = subassemblyRepository.Table.Where(x => x.BomId == BOMID).ToList();
            }
            return subassemblylist;
        }

        public List<subassemblydata> GetBomList(int bom_id)
        {
            List<subassemblydata> billOfMaterial = new List<subassemblydata>();
            try
            {
                billOfMaterial = subassemblyRepository.SearchsubassemblyList(bom_id);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return billOfMaterial;
        }
        public subassembly Post(subassembly arg)
        {
            subassembly subassemblyboms = new subassembly();
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                subassemblyRepository.Insert(arg);
                subassemblyboms = arg;

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return subassemblyboms;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                subassembly model = subassemblyRepository.GetById(id);
                model.IsDeleted = false;
                model.DeletedBy = "admin";
                model.DeletedDate = DateTime.Now;
                subassemblyRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public subassembly Getbomid(int BOMID)
        {
            subassembly subassembly = new subassembly();
            if (BOMID != null)
            {
                subassembly = subassemblyRepository.Table.Where(x => x.BomId == BOMID && x.IsDeleted == true).FirstOrDefault();
            }
            return subassembly;
        }
        public List<subassembly> Get()
        {

            List<subassembly> subassembly = new List<subassembly>();
            try
            {
                subassembly = subassemblyRepository.Table.ToList<subassembly>().Where(x => x.IsDeleted == true).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return subassembly;
        }
    }
}
