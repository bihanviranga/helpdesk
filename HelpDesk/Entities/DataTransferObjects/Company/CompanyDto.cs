namespace HelpDesk.Entities.DataTransferObjects
{
    public class CompanyDto
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int NumOfTickets { get; set; }
        public int NumOfProducts { get; set; }
        public int NumOfCategories { get; set; }
        public int NumOfModules { get; set; }
        public int NumOfBrands { get; set; }
    }
}
