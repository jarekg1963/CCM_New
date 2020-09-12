using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CCM_New.Shared;

namespace CCM_New.Server.Data
{
    public partial class CCMContext : DbContext
    {
        public CCMContext()
        {
        }

        public CCMContext(DbContextOptions<CCMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCarriers> TblCarriers { get; set; }
        public virtual DbSet<TblComplaints> Tbl_Complaints { get; set; }
        public virtual DbSet<TblDeliveries> Tbl_Deliveries { get; set; }
        public virtual DbSet<TblReasoncodes> TblReasoncodes { get; set; }
        public virtual DbSet<TblAsnData> TblAsnData { get; set; }
        public virtual DbSet<TblSapData> TblSapData { get; set; }
        public virtual DbSet<TblCustomers> TblCustomers { get; set; }
        public virtual DbSet<TblDeliveryLines> TblDeliveryLines { get; set; }
        public virtual DbSet<TblAttachments> TblAttachments { get; set; }
        public virtual DbSet<TblRootCauses> TblRootCauses { get; set; }
        public virtual DbSet<TblLiableParties> TblLiableParties { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.ToTable("Tbl_Users");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserPassword).HasMaxLength(50);

                entity.Property(e => e.UserRole)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });


            modelBuilder.Entity<TblLiableParties>(entity =>
            {
                entity.HasKey(e => e.LiablePartyId);

                entity.ToTable("Tbl_LiableParties");

                entity.Property(e => e.LiablePartyId).HasColumnName("LiableParty_ID");

                entity.Property(e => e.LiablePartyName)
                    .IsRequired()
                    .HasColumnName("LiableParty_Name")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<TblRootCauses>(entity =>
            {
                entity.HasKey(e => e.RootCauseId);

                entity.ToTable("Tbl_RootCauses");

                entity.Property(e => e.RootCauseId).HasColumnName("RootCause_ID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ComplaintType).HasColumnName("Complaint_Type");

                entity.Property(e => e.RootCauseName)
                    .IsRequired()
                    .HasColumnName("RootCause_Name")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAttachments>(entity =>
            {
                entity.HasKey(e => e.UploadId);

                entity.ToTable("Tbl_Attachments");

                entity.Property(e => e.UploadId).HasColumnName("uploadID");

                entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });



            modelBuilder.Entity<TblCustomers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("Tbl_Customers");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(255);

                entity.Property(e => e.CustomerAddress1).HasMaxLength(255);

                entity.Property(e => e.CustomerAddress2).HasMaxLength(255);

                entity.Property(e => e.CustomerContact).HasMaxLength(255);

                entity.Property(e => e.CustomerCountry).HasMaxLength(255);

                entity.Property(e => e.CustomerEmail).HasMaxLength(255);

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.CustomerSo)
                    .HasColumnName("CustomerSO")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TblAsnData>(entity =>
            {
                entity.HasKey(e => new { e.Asn, e.Asnline })
                    .IsClustered(false);

                entity.ToTable("Tbl_ASN_Data");

                entity.HasIndex(e => e.Plant)
                    .HasName("IX_Tbl_ASN_Data")
                    .IsClustered();

                entity.Property(e => e.Asn)
                    .HasColumnName("ASN")
                    .HasMaxLength(25);

                entity.Property(e => e.Asnline)
                    .HasColumnName("ASNLINE")
                    .HasMaxLength(25);

                entity.Property(e => e.AsnType)
                    .HasColumnName("ASN_Type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BillOfLading).HasMaxLength(255);

                entity.Property(e => e.Currency)
                    .HasColumnName("CURRENCY")
                    .HasMaxLength(255);

                entity.Property(e => e.DelivDate).HasColumnType("date");

                entity.Property(e => e.Grdate)
                    .HasColumnName("GRDate")
                    .HasColumnType("date");

                entity.Property(e => e.Matdesc)
                    .HasColumnName("MATDESC")
                    .HasMaxLength(255);

                entity.Property(e => e.Material).HasMaxLength(255);

                entity.Property(e => e.Plant)
                    .HasColumnName("PLANT")
                    .HasMaxLength(255);

                entity.Property(e => e.Prodhier)
                    .HasColumnName("PRODHIER")
                    .HasMaxLength(255);

                entity.Property(e => e.Shippedstockvalue)
                    .HasColumnName("SHIPPEDSTOCKVALUE")
                    .HasColumnType("money");

                entity.Property(e => e.Vendor)
                    .HasColumnName("VENDOR")
                    .HasMaxLength(255);

                entity.Property(e => e.VendorName).HasMaxLength(255);

                entity.Property(e => e.VendorShipmentNumber).HasMaxLength(255);
            });

            modelBuilder.Entity<TblSapData>(entity =>
            {
                entity.HasKey(e => new { e.Vbeln, e.Posnr })
                    .HasName("PK_Tbl_SAP_Data2")
                    .IsClustered(false);

                entity.ToTable("Tbl_SAP_Data");

                entity.HasIndex(e => new { e.Lifex, e.Vkorg })
                    .HasName("IX_Tbl_SAP_Data2");

                entity.HasIndex(e => new { e.Vbeln, e.Posnr, e.Vkorg })
                    .HasName("IX_Tbl_SAP_Data2_1");

                entity.Property(e => e.Vbeln)
                    .HasColumnName("VBELN")
                    .HasMaxLength(10);

                entity.Property(e => e.Posnr).HasColumnName("POSNR");

                entity.Property(e => e.Artkx)
                    .HasColumnName("ARTKX")
                    .HasMaxLength(255);

                entity.Property(e => e.Bolnr)
                    .HasColumnName("BOLNR")
                    .HasMaxLength(255);

                entity.Property(e => e.Currency)
                    .HasColumnName("CURRENCY")
                    .HasMaxLength(255);

                entity.Property(e => e.Erdat)
                    .HasColumnName("ERDAT")
                    .HasColumnType("datetime");

                entity.Property(e => e.IcValue)
                    .HasColumnName("IC_VALUE")
                    .HasColumnType("money");

                entity.Property(e => e.Kunag)
                    .HasColumnName("KUNAG")
                    .HasMaxLength(255);

                entity.Property(e => e.Kunnr)
                    .HasColumnName("KUNNR")
                    .HasMaxLength(255);

                entity.Property(e => e.Lfdat)
                    .HasColumnName("LFDAT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Lfimg).HasColumnName("LFIMG");

                entity.Property(e => e.Lgmng).HasColumnName("LGMNG");

                entity.Property(e => e.Lifex)
                    .HasColumnName("LIFEX")
                    .HasMaxLength(255);

                entity.Property(e => e.Matnr)
                    .HasColumnName("MATNR")
                    .HasMaxLength(255);

                entity.Property(e => e.Prodh)
                    .HasColumnName("PRODH")
                    .HasMaxLength(255);

                entity.Property(e => e.Vgbel)
                    .HasColumnName("VGBEL")
                    .HasMaxLength(255);

                entity.Property(e => e.Vgpos).HasColumnName("VGPOS");

                entity.Property(e => e.Vkorg)
                    .IsRequired()
                    .HasColumnName("VKORG")
                    .HasMaxLength(4);

                entity.Property(e => e.Vrkme)
                    .HasColumnName("VRKME")
                    .HasMaxLength(255);

                entity.Property(e => e.Vstel)
                    .HasColumnName("VSTEL")
                    .HasMaxLength(255);

                entity.Property(e => e.Wadat)
                    .HasColumnName("WADAT")
                    .HasColumnType("datetime");

                entity.Property(e => e.WadatIst)
                    .HasColumnName("WADAT_IST")
                    .HasColumnType("datetime");

                entity.Property(e => e.Werks)
                    .HasColumnName("WERKS")
                    .HasMaxLength(255);
            });


            modelBuilder.Entity<TblCarriers>(entity =>
            {
                entity.HasKey(e => new { e.CarrierId2, e.Nso })
                    .HasName("PK_Tbl_Carriers2");

                entity.ToTable("Tbl_Carriers");

                entity.Property(e => e.CarrierId2)
                    .HasColumnName("CarrierID2")
                    .HasMaxLength(50);

                entity.Property(e => e.Nso)
                    .HasColumnName("NSO")
                    .HasMaxLength(4);

                entity.Property(e => e.CarrierActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CarrierAddress1).HasMaxLength(255);

                entity.Property(e => e.CarrierAddress2).HasMaxLength(255);

                entity.Property(e => e.CarrierContact).HasMaxLength(255);

                entity.Property(e => e.CarrierCountry).HasMaxLength(255);

                entity.Property(e => e.CarrierEmail).HasMaxLength(255);

                entity.Property(e => e.CarrierId)
                    .IsRequired()
                    .HasColumnName("CarrierID")
                    .HasMaxLength(54)
                    .HasComputedColumnSql("([NSO]+[CarrierID2])");

                entity.Property(e => e.CarrierName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TblReasoncodes>(entity =>
            {
                entity.HasKey(e => e.ReasoncodeId);

                entity.ToTable("Tbl_Reasoncodes");

                entity.Property(e => e.ReasoncodeId)
                    .HasColumnName("Reasoncode_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ComplaintType).HasColumnName("Complaint_Type");

                entity.Property(e => e.FlowType).HasColumnName("Flow_Type");

                entity.Property(e => e.ReasoncodeName)
                    .HasColumnName("Reasoncode_Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblComplaints>(entity =>
            {
                entity.HasKey(e => e.ComplaintId);

                entity.ToTable("Tbl_Complaints");

                entity.HasIndex(e => e.ClaimCreatedDate)
                    .HasName("IX_Tbl_Complaints_7");

                entity.HasIndex(e => e.ComplaintId)
                    .HasName("IX_Tbl_Complaints_9");

                entity.HasIndex(e => e.ComplaintType)
                    .HasName("IX_Tbl_Complaints");

                entity.HasIndex(e => e.DeliveryType)
                    .HasName("IX_Tbl_Complaints_3");

                entity.HasIndex(e => e.Reasoncode)
                    .HasName("IX_Tbl_Complaints_4");

                entity.HasIndex(e => e.RegisteredBy)
                    .HasName("IX_Tbl_Complaints_6");

                entity.HasIndex(e => e.SalesOrganization)
                    .HasName("IX_Tbl_Complaints_1");

                entity.HasIndex(e => e.ShippingPoint)
                    .HasName("IX_Tbl_Complaints_2");

                entity.HasIndex(e => e.SoldToCustomerNumber)
                    .HasName("IX_Tbl_Complaints_8");

                entity.HasIndex(e => e.Status)
                    .HasName("IX_Tbl_Complaints_5");

                entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");

                entity.Property(e => e.AmountInvoiced)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.AmountReceived)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.AmountReceivedInvoice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.BillOfLading)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CarrierClaimRemarks).IsUnicode(false);

                entity.Property(e => e.CarrierFeedbackDate).HasColumnType("date");

                entity.Property(e => e.CarrierFunloc)
                    .HasColumnName("Carrier_Funloc")
                    .IsUnicode(false);

                entity.Property(e => e.CarrierName)
                    .HasColumnName("Carrier_Name")
                    .IsUnicode(false);

                entity.Property(e => e.CarrierPodremarks).HasColumnName("CarrierPODRemarks");

                entity.Property(e => e.ClaimAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.ClaimCreatedDate).HasColumnType("date");

                entity.Property(e => e.ClosureRemarks).IsUnicode(false);

                entity.Property(e => e.CnCreated).HasColumnName("CN_Created");

                entity.Property(e => e.CnCreatedDate)
                    .HasColumnName("CN_CreatedDate")
                    .HasColumnType("date");

                entity.Property(e => e.CnDnCurrency)
                    .IsRequired()
                    .HasColumnName("CN_DN_Currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CnDnNumber)
                    .HasColumnName("CN_DN_Number")
                    .IsUnicode(false);

                entity.Property(e => e.CncustCreated).HasColumnName("CNCust_Created");

                entity.Property(e => e.CncustCreatedDate)
                    .HasColumnName("CNCustCreatedDate")
                    .HasColumnType("date");

                entity.Property(e => e.ComplaintIduf)
                    .IsRequired()
                    .HasColumnName("ComplaintIDUF")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ComplaintType).HasColumnName("Complaint_Type");

                entity.Property(e => e.CreateCnDn).HasColumnName("CreateCN_DN");

                entity.Property(e => e.CreateCnRemarks).HasColumnName("CreateCN_Remarks");

                entity.Property(e => e.CreateCncust).HasColumnName("CreateCNCust");

                entity.Property(e => e.CreateCncustRemarks).HasColumnName("CreateCNCustRemarks");

                entity.Property(e => e.CustomerCncurrency).HasColumnName("CustomerCNCurrency");

                entity.Property(e => e.CustomerCnnumber).HasColumnName("CustomerCNNumber");

                entity.Property(e => e.CustomerCnremarks).HasColumnName("CustomerCNRemarks");

                entity.Property(e => e.CustomerCnvalue)
                    .HasColumnName("CustomerCNValue")
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.CustomerComments)
                    .HasColumnName("Customer_Comments")
                    .IsUnicode(false);

                entity.Property(e => e.CustomerContact)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerEmail).IsUnicode(false);

                entity.Property(e => e.CustomerFax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DcComments)
                    .HasColumnName("DC_Comments")
                    .IsUnicode(false);

                entity.Property(e => e.DcfeedbackReceiveDate)
                    .HasColumnName("DCFeedbackReceiveDate")
                    .HasColumnType("date");

                entity.Property(e => e.DeliveryDate).HasColumnType("date");

                entity.Property(e => e.DeliveryType).HasColumnName("Delivery_Type");

                entity.Property(e => e.ExtDelRef)
                    .HasColumnName("Ext_Del_Ref")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsuranceClaimRemarks).IsUnicode(false);

                entity.Property(e => e.InvoiceCreatedDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasColumnName("Invoice_number")
                    .IsUnicode(false);

                entity.Property(e => e.LiabilityNoteAccept).HasColumnName("LiabilityNote_Accept");

                entity.Property(e => e.LiabilityNoteSent).HasColumnName("LiabilityNote_Sent");

                entity.Property(e => e.PodComments)
                    .HasColumnName("POD_Comments")
                    .IsUnicode(false);

                entity.Property(e => e.PrAvailable).HasColumnName("PR_Available");

                entity.Property(e => e.RadarRef).IsUnicode(false);

                entity.Property(e => e.RegisteredBy).HasColumnName("Registered_by");

                entity.Property(e => e.RemarksLsprequest)
                    .HasColumnName("RemarksLSPRequest")
                    .IsUnicode(false);

                entity.Property(e => e.RemarksOnPod).HasColumnName("RemarksOnPOD");

                entity.Property(e => e.RemarksPodnotReceived).HasColumnName("Remarks_PODNotReceived");

                entity.Property(e => e.ResearchRemarks).IsUnicode(false);

                entity.Property(e => e.SalesOrganization)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SendPodRequest).HasColumnName("sendPOD_Request");

                entity.Property(e => e.SendToOriginDc).HasColumnName("SendToOriginDC");

                entity.Property(e => e.SendingDateOriginDc)
                    .HasColumnName("SendingDateOriginDC")
                    .HasColumnType("date");

                entity.Property(e => e.SendingDatePodRequest)
                    .HasColumnName("SendingDatePOD_Request")
                    .HasColumnType("date");

                entity.Property(e => e.ShippingPoint)
                    .IsRequired()
                    .HasColumnName("Shipping_Point")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ShiptoCustomerNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SoldToCustomerNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.TotalCnDnValue)
                    .HasColumnName("TotalCN_DN_Value")
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");
            });



            modelBuilder.Entity<TblDeliveries>(entity =>
            {
                entity.HasKey(e => e.DeliveryId)
                    .HasName("PK_Tbl_Deliveries2");

                entity.ToTable("Tbl_Deliveries");

                entity.Property(e => e.DeliveryId).HasColumnName("deliveryID");

                entity.Property(e => e.ComplaintId).HasColumnName("complaintID");

                entity.Property(e => e.DeliveryNumber)
                    .IsRequired()
                    .HasColumnName("deliveryNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SalesOrder)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblDeliveryLines>(entity =>
            {
                entity.HasKey(e => e.DeliveryLineId)
                    .HasName("PK_tbl_DeliveryLines2");

                entity.ToTable("Tbl_DeliveryLines");

                entity.Property(e => e.DeliveryLineId).HasColumnName("DeliveryLineID");

                entity.Property(e => e.ActualGoodsIssueDate).HasColumnType("date");

                entity.Property(e => e.ComplaintId).HasColumnName("ComplaintID");

                entity.Property(e => e.CurrencyComplaintValue)
                    .IsRequired()
                    .HasColumnName("Currency_ComplaintValue")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FinalLiableParty).HasColumnName("Final_Liable_Party");

                entity.Property(e => e.FinalRcRemarks).HasColumnName("FinalRC_Remarks");

                entity.Property(e => e.FinalRcRemarksComplaintsTeam).HasColumnName("FinalRC_RemarksComplaintsTeam");

                entity.Property(e => e.FinalRootcause).HasColumnName("Final_Rootcause");

                entity.Property(e => e.LiableParty).HasColumnName("Liable_Party");

                entity.Property(e => e.MaterialOrd11cn)
                    .IsRequired()
                    .HasColumnName("Material_Ord_11CN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialOrdCtv)
                    .IsRequired()
                    .HasColumnName("Material_Ord_CTV")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaterialRec)
                    .HasColumnName("Material_Rec")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlannedGoodsIssueDate).HasColumnType("date");

                entity.Property(e => e.SalesOrder)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SalesOrderLine)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumbers)
                    .HasColumnName("Serial_Numbers")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TotalComplaintValueCurrentCurrency)
                    .HasColumnName("Total_Complaint_Value_Current_Currency")
                    .HasColumnType("money");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}