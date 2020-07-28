using System;

namespace MovieStore.Core.Models.Request
{
    public class UserPurchaseRequestModel
    {
        public UserPurchaseRequestModel()
        {
            PurchaseNumber = Guid.NewGuid();
            PurchaseTime = DateTime.UtcNow;
        }

        public int UserId { get; set; }
        public int MovieId { get; set; }
        public Guid PurchaseNumber { get; set; }
        public DateTime PurchaseTime { get; set; }
        public decimal Price { get; set; }
    }
}