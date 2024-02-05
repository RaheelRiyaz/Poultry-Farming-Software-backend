using KashmirPoultrySoftware.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.RRModels
{
    public record HatchRequest(string Name, int NoOfChicks, double ChickPerPrice,DateTime CreatedOn);
    public record HatchResponse(
    Guid Id,
    Guid EntityId,
    string Name,
    int NoOfChicks,
    int TotalMotality,
    double ChickPerPrice,
    HatchStatus HatchStatus,
    DateTime CreatedOn,
    DateTime UpdatedOn,
    DateTime? HatchReleaseDate,
    int? DaysPassed
)
    { };

    public class HatchDetails
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public string Name { get; set; } = null!;
        public int NoOfChicks { get; set; }
        public int TotalMotality { get; set; }
        public double ChickPerPrice { get; set; }
        public HatchStatus HatchStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public double TotalExpenditure { get; set; }
        public double TotalSale { get; set; }
        public Guid HatchId { get; set; }
        public string ProfitOrLoss { get; set; } = null!;
    }


    public record HatchUpdateRequest(HatchStatus HatchStatus, Guid HatchId, DateTime HatchFinishDate);
}
