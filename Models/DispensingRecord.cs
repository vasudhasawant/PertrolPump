using System;
using System.ComponentModel.DataAnnotations;

namespace PetrolPumpLog.Models
{
    public class DispensingRecord
    {
        public int Id { get; set; }                     // PK
        public string DispenserNo { get; set; }         // NOT NULL
        public decimal QuantityLiters { get; set; }     // NOT NULL, decimal(10,2)
        public string VehicleNumber { get; set; }       // NOT NULL
        public string PaymentMode { get; set; }         // NOT NULL
        public string PaymentProofFileName { get; set; }// NULLABLE
        public DateTime CreatedAt { get; set; }         // NOT NULL, default GETDATE()
    }

}
