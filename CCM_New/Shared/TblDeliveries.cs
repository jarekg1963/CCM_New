
using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;



namespace CCM_New.Shared

{

    public partial class TblDeliveries

    {
        public int DeliveryId { get; set; }

        [ForeignKey("Comp")]

        public int ComplaintId { get; set; }

        public string DeliveryNumber { get; set; }

        public string SalesOrder { get; set; }

        public TblComplaints Comp { get; set; }
      

    }

}