﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using MMS.Core.Entities.Stock;

namespace MMS.Data.Mapping.StockMap
{
    public class GrnMap : EntityTypeConfiguration<GRN_EntityModel>
    {
        public GrnMap()
        {
            HasKey(t => t.GrnID);
            Property(t => t.GrnID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.GrnNo).IsRequired();
            Property(t => t.BarCodeNo);
            Property(t => t.GateEntryNo);
            Property(t => t.FreightAmount);
            Property(t => t.GrnDate);
            Property(t => t.GEDate);
            Property(t => t.GETime);
            Property(t => t.InsuranceAmount);
            Property(t => t.GrnRefNo);
            Property(t => t.GateRefNo);
            Property(t => t.CustomsDuty);
            Property(t => t.SupplierNameId);
            Property(t => t.GrnType);
            Property(t => t.Pack_Forward);
            Property(t => t.DC_No);
            Property(t => t.ST_VATcredit);
            Property(t => t.ServiceTax);
            Property(t => t.DCDate);
            Property(t => t.INVoiceNo);
            Property(t => t.INVoiceDate);
            Property(t => t.ImportClearance);
            Property(t => t.BENo);
            Property(t => t.OtherCharges);
            Property(t => t.ExchangeRate);
            Property(t => t.ShipmentMode);
            Property(t => t.GeneralRemarks1);
            Property(t => t.PoNO);
            Property(t => t.LOTNo);
            Property(t => t.MaterialId);
            Property(t => t.ColourId);
            Property(t => t.UOMId);
            Property(t => t.DOM);
            Property(t => t.StoreLocation);
            Property(t => t.GeneralRemarks);
            Property(t => t.IndentNo);
            Property(t => t.Grade);
            Property(t => t.Substance);
            Property(t => t.Weight);
            Property(t => t.IONo);
            Property(t => t.GroupID);
            Property(t => t.QtyAsPerDc);
            Property(t => t.QtyAsPerSQFT);
            Property(t => t.QtyAsPerPCS);
            Property(t => t.QTYType);
            Property(t => t.ReceivedQty);
            Property(t => t.ReceivedSQFT);
            Property(t => t.ReceivedPCS);
            Property(t => t.ReceivedType);
            Property(t => t.RejectedQty);
            Property(t => t.RejectedSQFT);
            Property(t => t.RejectedPCS);
            Property(t => t.RejectedType);
            Property(t => t.AcceptedQty);
            Property(t => t.AcceptedSQFT);
            Property(t => t.AcceptedPCS);
            Property(t => t.AcceptedType);
            Property(t => t.Stores);
            Property(t => t.CategoryId);
            Property(t => t.IfGroupIsMaintenance);
            Property(t => t.QtyExcess);
            Property(t => t.ExcessApproval);
            Property(t => t.PendingQty);
            Property(t => t.Disp_SelectedMatOfPO);
            Property(t => t.Rate);
            Property(t => t.Value);
            Property(t => t.MRPMargin);
            Property(t => t.MRPPrice);
            Property(t => t.AccessibleValue);
            Property(t => t.Discount_Per);
            Property(t => t.Discount_Val);
            Property(t => t.ExciseDuty_Per);
            Property(t => t.ExciseDuty_Val);
            Property(t => t.ST_VAT_CST_Per);
            Property(t => t.ST_VAT_CST_Val);
            Property(t => t.Ecess_Per);
            Property(t => t.Ecess_Val);
            Property(t => t.ST_VAT_CST_Per);
            Property(t => t.ST_VAT_CST_Val);
            Property(t => t.SH_Ecess_Per);
            Property(t => t.SH_Ecess_Val);
            Property(t => t.Surcharge_Per);
            Property(t => t.Surcharge_Val);
            // Property(t => t.Accessible_Val).IsRequired();
            Property(t => t.TagsPiecesDiscount_Per);
            Property(t => t.TagsPiecesDiscount_Val);
            Property(t => t.AmountplusTax);
            Property(t => t.IndentMaterialId);
            Property(t => t.BuyerSeasonID);
            Property(t => t.BuyerMasterId);
            Property(t => t.ShortageQty);
            Property(t => t.ShortageSQFT);
            Property(t => t.ShortagePCS);
            Property(t => t.ShortageType);
            Property(t => t.AutomaticPONumber);
            Property(t => t.ServiceTaxPer);
            Property(t => t.MaterialType);
            Property(t => t.TotalCount);
            Property(t => t.Grn_MaterialID);            
            ToTable("Tbl_GRN");
        }
    }
}