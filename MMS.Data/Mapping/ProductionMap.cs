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
    public class ProductionMap : EntityTypeConfiguration<Production>
    {
        public ProductionMap()
        {
            HasKey(t => t.ProductionId);
            Property(t => t.ProductionId).HasColumnName("productionid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.ProductionDate).HasColumnName("productiondate");
            Property(t => t.ProductionCode).HasColumnName("productioncode");
            Property(t => t.ProductionQty).HasColumnName("productionqty");
            Property(t => t.ProductionStatus).HasColumnName("productionstatus");
            Property(t => t.ProductId).HasColumnName("productid");
            Property(t => t.MinQty).HasColumnName("minqty");
            Property(t => t.MaxQty).HasColumnName("maxqty");
            Property(t => t.RequiredQty).HasColumnName("requiredqty");
            Property(t => t.StoreCode).HasColumnName("storecode");
            Property(t => t.ProductionDueDate).HasColumnName("productionduedate");
            Property(t => t.ProductionFullfillDate).HasColumnName("productionfullfilldate");
            Property(t => t.RefDocNo).HasColumnName("refdocno");
            Property(t => t.RefDocDate).HasColumnName("refdocdate");
            Property(t => t.Status1Code).HasColumnName("status1code");
            Property(t => t.Status1Date).HasColumnName("status1date");
            Property(t => t.Status1By).HasColumnName("status1by");
            Property(t => t.Status2Code).HasColumnName("status2code");
            Property(t => t.Status2Date).HasColumnName("status2date");
            Property(t => t.Status2By).HasColumnName("status2by");
            Property(t => t.Status3Code).HasColumnName("status3code");
            Property(t => t.Status3Date).HasColumnName("status3date");
            Property(t => t.Status3By).HasColumnName("status3by");
            Property(t => t.CreatedDate).HasColumnName("createddate");
            Property(t => t.UpdatedDate).HasColumnName("updateddate");
            Property(t => t.DeletedDate).HasColumnName("deleteddate");
            Property(t => t.CreatedBy).HasColumnName("createdby");
            Property(t => t.UpdatedBy).HasColumnName("updatedby");
            Property(t => t.DeletedBy).HasColumnName("deletedby");
            Property(t => t.Productions).HasColumnName("productions");
            Property(t => t.SubAssembly).HasColumnName("subassembly");
            Property(t => t.Inprogress).HasColumnName("inprogress");
            Property(t => t.ProductionPerDay).HasColumnName("productionperday");
            ToTable("production");
        }
    }
}
