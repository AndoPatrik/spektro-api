using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spektro_API_Azure.Model
{
    public class CouponModel
    {
        private int id;
        private string label;
        private string description;
        private int discount;
        private DateTime dateOfIssue;
        private DateTime dateOfExpiry;
        private bool validity;

        public CouponModel() { }
        public CouponModel(int id, string label, string description, int discount, DateTime dateOfIssue, DateTime dateOfExpiry, bool validity)
        {
            this.id = id;
            this.label = label;
            this.description = description;
            this.discount = discount;
            this.dateOfIssue = dateOfIssue;
            this.dateOfExpiry = dateOfExpiry;
            this.validity = validity;
        }

        public int Id { get => id; set => id = value; }
        public string Label { get => label; set => label = value; }
        public string Description { get => description; set => description = value; }
        public int Discount { get => discount; set => discount = value; }
        public DateTime DateOfIssue { get => dateOfIssue; set => dateOfIssue = value; }
        public DateTime DateOfExpiry { get => dateOfExpiry; set => dateOfExpiry = value; }
        public bool Validity { get => validity; set => validity = value; }
    }
}
