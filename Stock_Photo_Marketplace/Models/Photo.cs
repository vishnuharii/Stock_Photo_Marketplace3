namespace Stock_Photo_Marketplace.Models
{
    public class Photo
    {
        public int PhotoID { get; set; } // Primary Key
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}