﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCM_New.Shared
{
    public partial class TblDeliveryLines
    {
        public int DeliveryLineId { get; set; }
        public int ComplaintId { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime ActualGoodsIssueDate { get; set; }
        public DateTime PlannedGoodsIssueDate { get; set; }
        public int DeliveryLine { get; set; }
        public string MaterialOrdCtv { get; set; }
        public string MaterialOrd11cn { get; set; }
        public int OrderQty { get; set; }
        public string MaterialRec { get; set; }
        public int ComplaintQty { get; set; }
        public string SalesOrder { get; set; }
        public string SalesOrderLine { get; set; }
        [ForeignKey("Rootca")]
        public int? Rootcause { get; set; }
        [ForeignKey("Liable")]
        public int? LiableParty { get; set; }
        public decimal TotalComplaintValueCurrentCurrency { get; set; }
        public string CurrencyComplaintValue { get; set; }
        [ForeignKey("FRoot")]
        public int? FinalRootcause { get; set; }
        [ForeignKey("FLiable")]
        public int? FinalLiableParty { get; set; }
        public string FinalRcRemarks { get; set; }
        public string FinalRcRemarksComplaintsTeam { get; set; }
        public string SerialNumbers { get; set; }


        public TblLiableParties Liable { get; set; }        
        public TblRootCauses Rootca { get; set; }

        public TblLiableParties FLiable { get; set; }
        public TblRootCauses FRoot { get; set; }

    }
}