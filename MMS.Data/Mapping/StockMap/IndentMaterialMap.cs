using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping.StockMap
{
    class IndentMaterialMap : EntityTypeConfiguration<IndentMaterials>
    {
        public IndentMaterialMap()
        {
            HasKey(t => t.IndentMaterialID);
            Property(t => t.IndentID);
            Property(t => t.CategoryName);
            Property(t => t.GroupName);
            Property(t => t.MaterialDescription);
            Property(t => t.Color);
            Property(t => t.WastageName);
            Property(t => t.TotalNormsName);
            Property(t => t.SampleNorms);
            Property(t => t.Wastage);
            Property(t => t.WastageQty);
            Property(t => t.WastageQtyUOM);
            Property(t => t.TotalNorms);
            Property(t => t.MRPNO);
            Property(t => t.MaterialCategoryMasterId);
            Property(t => t.MaterialGroupMasterId);
            Property(t => t.MaterialMasterID);
            Property(t => t.SubstanceRange);
            Property(t => t.SubstanceMasterId);
            Property(t => t.BOMMaterialID);
            Property(t => t.BOMID);
            Property(t => t.ColorMasterID);
            Property(t => t.BuyerSeason);
            Property(t => t.SeasonName);
            Property(t => t.RequiredQty);
            Property(t => t.IndentQTY);
            Property(t => t.StoreStock);
            Property(t => t.FreeStock);
            Property(t => t.Value);
            Property(t => t.SupplierId);
            Property(t => t.OrderEntryId);
            Property(t => t.UomMasterId);
            Property(t => t.BuyerFullName);
            Property(t => t.BuyerMasterId);
            Property(t => t.IndentMaterialID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CreatedDate);
            Property(t => t.SupplierMasterName);
            Property(t => t.IsPo);
            Property(t => t.StoreId);
            Property(t => t.BuyerPoNo);         
            Property(t => t.UpdatedDate);
            Property(t => t.OrderType);
            Property(t => t.TotalPairs); 
            Property(t => t.SizeRangeMasterID);
            Property(t => t.TotalIndentPairs);            
            Property(t => t.SizeRangeName);
            ToTable("IndentMaterials");
        }
    }
}
