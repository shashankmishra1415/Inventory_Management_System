namespace InventorySystem.SharedLayer.Models.Response
{
    public class SerailNumberResponse
    {
        public List<SerialNumberClass> ScannedSerialNumber {  get; set; }
        public List<SerialNumberClass> NotScannedSerialNumber {  get; set; }
    }
    public class SerialNumberClass
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
    }
}
