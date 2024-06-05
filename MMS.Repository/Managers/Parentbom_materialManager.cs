using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class Parentbom_materialManager : IParentbom_materialServices
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<parentbom_material> parentbom_materialRepository;
        private Repository<parentbom_material> BillOfMaterialRepository;
        public Parentbom_materialManager()
        {
            parentbom_materialRepository = unitOfWork.Repository<parentbom_material>();

        }
        public List<Parentbommatertial> GetBomList(int bom_id)
        {
            List<Parentbommatertial> billOfMaterial = new List<Parentbommatertial>();
            try
            {
                billOfMaterial = parentbom_materialRepository.SearchBomMaterialList1(bom_id);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return billOfMaterial;
        }
        public parentbom_material Post(parentbom_material arg)
        {
            parentbom_material parentbom_Material = new parentbom_material();
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                parentbom_materialRepository.Insert(arg);
                parentbom_Material = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return parentbom_Material;
        }
        public bool Put(parentbom_material arg)
        {
            bool result = false;
            try
            {
                parentbom_material model = parentbom_materialRepository.Table.Where(p => p.BomMaterialId == arg.BomMaterialId).FirstOrDefault();
                if (model != null)
                {
                    model.BomMaterialId = arg.BomMaterialId;
                    model.IsActive = arg.IsActive;
                    model.BomID = arg.BomID;
                    model.ProductId = arg.ProductId;
                    model.MaterialCategory=arg.MaterialCategory;
                    model.MaterialGroupId = arg.MaterialGroupId;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.UomId = arg.UomId;
                    model.RequiredQty = arg.RequiredQty;
                    model.UpdatedDate = DateTime.Now;
                    string username = "admin";
                    model.UpdatedBy = username;
                    parentbom_materialRepository.Update(model);
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
        public List<parentbom_material> GetMaterialList(int BOMID)
        {
            List<parentbom_material> billOfMaterial = new List<parentbom_material>();
            if (BOMID != null)
            {
                billOfMaterial = parentbom_materialRepository.Table
                    .Where(x => x.BomID == BOMID)
                    .OrderBy(x => x.BomID)
                    .ToList();
            }
            return billOfMaterial;
        }
        public parentbom_material Getbommaterialid(int BOMID)
        {
            parentbom_material billOfMaterial = new parentbom_material();
            if (BOMID != null)
            {
                billOfMaterial = parentbom_materialRepository.Table.Where(x => x.BomMaterialId == BOMID && x.IsDelete == true).FirstOrDefault();
            }
            return billOfMaterial;
        }
        public List<parentbom_material> Get()
        {

            List<parentbom_material> customertransaction = new List<parentbom_material>();
            try
            {
                customertransaction = parentbom_materialRepository.Table.ToList<parentbom_material>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return customertransaction;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                parentbom_material model = parentbom_materialRepository.GetById(id);
                model.IsDelete = false;
                model.DeletedBy = "admin";
                model.DeletedDate = DateTime.Now;
                parentbom_materialRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
    }
}
