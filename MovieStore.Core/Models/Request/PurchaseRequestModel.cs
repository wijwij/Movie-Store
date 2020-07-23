using System;

namespace MovieStore.Core.Models.Request
{
    public class PurchaseRequestModel
    {
        public PurchaseRequestModel()
        {
            PurchaseNumber = Guid.NewGuid();
            PurchaseDate = DateTime.UtcNow;
        }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public Guid? PurchaseNumber { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
    }
}