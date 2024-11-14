

namespace PizzaHurt.Models
{
   public class ReportModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string CategoryName { get; set; }

        public string ItemType { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
