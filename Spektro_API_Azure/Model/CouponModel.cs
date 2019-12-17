using System;

namespace Spektro_API_Azure.Model
{
    public class CouponModel
    {
        private int id;
        private string code;
        private string description;
        private DateTime validFrom;
        private DateTime validUntil;
        private bool validity;
        private int userId;

        public CouponModel() { }
        public CouponModel(int id, string code, string description, DateTime validFrom, DateTime validUntil, bool validity, int userId)
        {
            this.id = id;
            this.code = code;
            this.description = description;
            this.validFrom = validFrom;
            this.validUntil = validUntil;
            this.validity = validity;
            this.userId = userId;
        }

        public int Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }
        public string Description { get => description; set => description = value; }
        public DateTime ValidFrom { get => validFrom; set => validFrom = value; }
        public DateTime ValidUntil { get => validUntil; set => validUntil = value; }
        public bool Validity { get => validity; set => validity = value; }
        public int UserId { get => userId; set => userId = value; }
    }
}
