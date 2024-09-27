using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public  interface  IBuyerMaster
    {
       //using sp
        bool post(BuyerModel_SP buyerMaster);
            
        List<BuyerModel_SP> GetBuyerModels();

        BuyerModel_SP GetSingleBuyerModel(int? id);

        bool Putbuyer(BuyerModel_SP buyermaster);

        bool Deletebuyer(int id);


        //using Entity method
        bool Put(BuyerMaster1 buyerMaster);  

        bool Delete(int id);

        BuyerMaster1 GetBuyerId(int id);

        List<BuyerMaster1> Get();

    }
}
