using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class ColorManager : IColorService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ColorMaster> colorRepository;

        public ColorManager()
        {
            colorRepository = unitOfWork.Repository<ColorMaster>();
        }


        public bool Post(ColorMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                colorRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public ColorMaster ColorPost(ColorMaster arg)
        {
            ColorMaster colorMaster = new ColorMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                colorRepository.Insert(arg);
                colorMaster = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return colorMaster;
        }

        public ColorMaster GetcolorID(int? ColorMasterId)
        {
            ColorMaster colorMaster = new ColorMaster();
            colorMaster = colorRepository.Table.Where(x => x.ColorMasterId == ColorMasterId).FirstOrDefault();
            return colorMaster;
        }
        public ColorMaster GetColor(string Color)
        {
            ColorMaster colorMaster = new ColorMaster();
            colorMaster = colorRepository.Table.Where(x => x.Color == Color).FirstOrDefault();
            return colorMaster;
        }

        public ColorMaster GetColorCode(string BuyerColorCode)
        {
            ColorMaster colorMaster = new ColorMaster();
            colorMaster = colorRepository.Table.Where(x => x.BuyerColorCode == BuyerColorCode).FirstOrDefault();
            return colorMaster;
        }
        public bool Put(ColorMaster arg)
        {
            bool result = false;
            try
            {
                ColorMaster model = colorRepository.Table.Where(p => p.ColorMasterId == arg.ColorMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.Color = arg.Color;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.ColorImages = arg.ColorImages;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.BuyerColorCode = arg.BuyerColorCode;
                    colorRepository.Update(model);
                    result = true;
                }

                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }




        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                ColorMaster model = colorRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                colorRepository.Update(model);
                // colorRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public ColorMaster Get(int id)
        {
            return null;
        }

        public List<ColorMaster> Get()
        {
            List<ColorMaster> colorMaster = new List<ColorMaster>();
            try
            {
                colorMaster = colorRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<ColorMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return colorMaster;
        }
        public ColorMaster GetColorMasterID(int? ColorMasterId)
        {
            ColorMaster colorMaster = new ColorMaster();
            colorMaster = colorRepository.Table.Where(x => x.ColorMasterId == ColorMasterId).FirstOrDefault();
            return colorMaster;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}
