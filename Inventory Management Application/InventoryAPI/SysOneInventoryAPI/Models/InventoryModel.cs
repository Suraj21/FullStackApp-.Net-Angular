namespace SysOneInventoryAPI.Models
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public int SrNo { get; set; }
        public string ModelName { get; set; }
        public string Quantity { get; set; }
        public string SlNo { get; set; }
        public string AssetTag { get; set; }
        public string WorkstationOrLab { get; set; }
        public string RackNumber { get; set; }
        public string RailNumber { get; set; }
        public string DisplayName { get; set; }
        public string UserID { get; set; }
        public string Location { get; set; }
        public string AssetType { get; set; }
        public string Project { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceValue { get; set; }
        public string PoNumber { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}