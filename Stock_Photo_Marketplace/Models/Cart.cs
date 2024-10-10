namespace Stock_Photo_Marketplace.Models
{
    public class Cart
    {
        public int CartID { get; set; } // Primary Key
        public int UserID { get; set; }
        public int PhotoID { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
        public virtual User User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}