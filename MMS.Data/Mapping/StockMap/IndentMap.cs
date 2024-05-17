using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MMS.Core.Entities;

namespace MMS.Data.Mapping.StockMap
{
    public class IndentMap : EntityTypeConfiguration<Indent>
    {
        public IndentMap()
        {
            HasKey(t => t.IndentId);
            Property(t => t.IndentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.AmmendmentNo);
            Property(t => t.AmmendmentDate);
            Property(t => t.IndentType);
            Property(t => t.IndentNo);
            Property(t => t.IoNo);
            Property(t => t.SelectedIndentNO);
            Property(t => t.UnitId);
            Property(t => t.Indendingfor);
            Property(t => t.MaterialCatId);            
            Property(t => t.Date);
            Property(t => t.SeasonId);
            Property(t => t.MaterialGrpId);
            Property(t => t.BuyerId);            
            Property(t => t.RequestedBy);
            Property(t => t.UOMId);
            Property(t => t.ReqQty);
            Property(t => t.StoreStock);
            Property(t => t.MaterialId);
            Property(t => t.Rate);
            Property(t => t.IntendQty);
            Property(t => t.FreeStock);
            Property(t => t.ColourId);
            Property(t => t.Value);
            Property(t => t.SupplierId);
            Property(t => t.Remarks);
            Property(t => t.PreparedBy);
            Property(t => t.CheckedBy);
            Property(t => t.ApprovedBy);
            Property(t => t.MRPNO);
            Property(t => t.StoreId);
            Property(t => t.OrderTypeID);
            ToTable("Indent");
        }
    }
}
