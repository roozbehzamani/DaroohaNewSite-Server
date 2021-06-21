using DaroohaNewSite.Data.Dtos.Common.Pagination;

namespace DaroohaNewSite.Data.Dtos.Site.App.Product
{
    public class ProductPaginationDt : PaginationDto
    {

        public string ProductName { get; set; }

        public int ProductPrice { get; set; }

        public string Size { get; set; }

        public string Code { get; set; }

        public string EnclosureType { get; set; } //نوع محفظه

        public string ManufacturingCountry { get; set; }

        public string ManufacturerCompany { get; set; }

        public string WebAddress { get; set; }

        public string Features { get; set; }

        public string MethodUse { get; set; }

        public string Indications { get; set; }

        public string Warnings { get; set; }

        public string Discount { get; set; } = "0";

        public string Maintenance { get; set; }

        public string ScientificName { get; set; }
    }
}
