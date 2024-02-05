using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Application.RRModels;
using KashmirPoultrySoftware.Domain.Entities;
using KashmirPoultrySoftware.Domain.Enums;
using KashmirPoultrySoftware.Persistence.Dapper;
using KashmirPoultrySoftware.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Persistence.Repository
{
    public class HatchRepository : BaseRepository<Hatch>, IHatchRepository
    {
        public HatchRepository(KashmirPoultrySoftwareDbContext context) : base(context)
        {
        }

        public async Task<int> ChangeHatchStatus(HatchStatus hatchStatus, Guid hatchId,DateTime hatchFinishDate)
        {
            string query = $@"UPDATE HATCHES
                            SET
                                HatchStatus = @hatchStatus,
                                HatchReleaseDate = CASE WHEN HatchStatus = 2 THEN @hatchFinishDate ELSE HatchReleaseDate END
                            WHERE
                                Id = @hatchId
                                ";

            return await context.ExecuteAsync(query, new { hatchStatus, hatchId , hatchFinishDate });
        }




        public async Task<IEnumerable<HatchResponse>> GetAllHatchesByEntity(Guid entityId)
        {
            string query = $@"SELECT H.*,
							CASE 
							WHEN H.HatchStatus = 2 Then DATEDIFF(DAY, CreatedOn, GETDATE())
							ELSE DATEDIFF(DAY, CreatedOn, HatchReleaseDate)
							END
                             AS DaysPassed
                            FROM
                            Hatches H
                            WHERE
                            H.EntityId = @entityId  ORDER BY H.CreatedOn Desc";

            return await context.QueryAsync<HatchResponse>(query, new { entityId });
        }



        public async Task<HatchDetails?> HatchDetailsById(Guid hatchId)
        {
            string query = $@"SELECT 
                              H.*,
                              E.*,
                              TotalSale,
                              CASE
                                WHEN E.TotalExpenditure > S.TotalSale THEN 'Loss'
                                WHEN E.TotalExpenditure < S.TotalSale THEN 'Profit'
                                ELSE 'Nill'
                              END AS ProfitOrLoss
                            FROM Hatches H
                            INNER JOIN (
                              SELECT HatchId, SUM(TotalAmount) AS TotalExpenditure 
                              FROM Expenditures 
                              WHERE HatchId = @hatchId
                              GROUP BY HatchId
                            ) E ON E.HatchId = H.ID
                            LEFT JOIN (
                              SELECT HatchId, SUM(NoOfKilograms * Rate) AS TotalSale 
                              FROM Sales 
                              WHERE HatchId = @hatchId
                              GROUP BY HatchId
                            ) S ON H.Id = S.HatchId
                            WHERE H.Id = @hatchId ";

            return await context.FirstOrdefaultAsync<HatchDetails?>(query, new { hatchId });
        }
    }
}
