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
  public  class InternalOrderFormMap : EntityTypeConfiguration<OrderEntryForm>
    {
        public InternalOrderFormMap()
        {
            HasKey(t => t.OrderEntryId);
            Property(t => t.OrderEntryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.BuyerOrderSlNo);
            Property(t => t.LotNo);
            Property(t => t.Count);
            Property(t => t.WeekNo);
            Property(t => t.Date);
            Property(t => t.BuyerSeason);
            Property(t => t.BuyerName);
            Property(t => t.OrderProjectionNo);
            Property(t => t.BuyerPoNo);
            Property(t => t.OurStyle);
            Property(t => t.LeatherDescription);
            Property(t => t.DiscountPer);
            Property(t => t.QuoteNo);
            Property(t => t.CountryStamp);
            Property(t => t.CustomerPlan);
            Property(t => t.CustomerDate);
            Property(t => t.AgentMasterId);
            Property(t => t.CommPer);
            Property(t => t.ExFactoryDate);
            Property(t => t.ShipmentMode);
            Property(t => t.SampleReqNo);
            Property(t => t.Brand);
            Property(t => t.BuyerStyleNo);
            Property(t => t.BarCodeNo);
            Property(t => t.BomNo);
            Property(t => t.Last);
            Property(t => t.ColorMasterId);
            Property(t => t.FinishedProdType);
            Property(t => t.ProductTypeId);
            Property(t => t.AmendmentNoWithDate);
            Property(t => t.TotalOrderForWeek);
            Property(t => t.OrderType);
            Property(t => t.Currency);
            Property(t => t.Rs);
            Property(t => t.Parties);
            Property(t => t.GradeMasterId);
            Property(t => t.SizeRangeMasterId);
            Property(t => t.Remarks1);
            Property(t => t.Remarks2);
            Property(t => t.LineNo_1);
            Property(t => t.IsBuyer);
            Property(t => t.IsInternal);
            Property(t => t.PartiesAmount1);
            Property(t => t.PartiesAmount2);
            Property(t => t.ShortUnitID);
            Property(t => t.LongUnitID);
           // Property(t => t.IsInternal);
            Property(t => t.TotalAmount);
            Property(t => t.IsDeleted);
            ToTable("InternalOrderForm");
        }
    }
}
