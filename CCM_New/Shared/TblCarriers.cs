﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CCM_New.Shared
{
    public partial class TblCarriers
    {
        public string CarrierId2 { get; set; }
        public string Nso { get; set; }
        [Key]
        public string CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierAddress1 { get; set; }
        public string CarrierAddress2 { get; set; }
        public string CarrierCountry { get; set; }
        public string CarrierContact { get; set; }
        public string CarrierEmail { get; set; }
        public bool? CarrierActive { get; set; }
        //[Key]
        // public int Id { get; set; }
        
        public string FullComboName { get { return Nso + " " + CarrierName + " " + CarrierId2; } }
       

    }
}
