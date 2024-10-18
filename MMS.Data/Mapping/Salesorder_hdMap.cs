using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Mapping
{
    public class Salesorder_hdMap : EntityTypeConfiguration<Salesorder_hd>
    {
        public Salesorder_hdMap()
        {
            HasKey(t => t.salesorderid_hd);
            Property(t => t.salesorderid_hd).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.customerid).HasColumnName("customerid");
            Property(t => t.Salesorderdate).HasColumnName("salesorderdate");
            Property(t => t.items).HasColumnName("items");
            Property(t => t.Total_price).HasColumnName("total_price");
            Property(t => t.Total_disamount).HasColumnName("total_disamount");
            Property(t => t.Total_subtotal).HasColumnName("total_subtotal");
            Property(t => t.Total_taxamount).HasColumnName("total_taxamount");
            Property(t => t.quantity).HasColumnName("quantity");
            Property(t => t.grand_total).HasColumnName("grand_total");
            Property(t => t.isactive).HasColumnName("isactive");
            Property(t => t.IsDeleted).HasColumnName("isdeleted");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.deletedby).HasColumnName("deletedby");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.createdby).HasColumnName("createdby");
            Property(t => t.Updatedby).HasColumnName("updatedby");
            Property(t => t.currencyid).HasColumnName("currencyid");
            ToTable("salesorder_hd");
        }
    }
}
