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
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private const string baseQuery = $@"SELECT S.Id,C.Name AS CustomerName,
                            C.Email as CustomerEmail,
                            S.PaymentStatus ,
                            S.Rate ,
                            H.Id as HatchId,
                            S.CustomerId,
                            S.NoOfKilograms,
                            S.CreatedOn as SaleDate,
                            S.NoOfKilograms * Rate as TotalAmount
                            FROM Sales S
                            INNER JOIN Hatches H
                            ON H.Id = S.HatchId
                            INNER JOIN Customers C
                            ON C.Id = S.CustomerId  ";

        public SaleRepository(KashmirPoultrySoftwareDbContext dbContext) : base(dbContext)
        {
        }



        public async Task<int> ChangeSalePaymentStatus(Guid saleId,PaymentStatus paymentStatus)
        {
            string query = $@"UPDATE Sales SET
                            PaymentStatus = @paymentStatus
                            WHERE Id = @saleId";

            return await context.ExecuteAsync(query,new {paymentStatus,saleId});
        }




        public async Task<IEnumerable<SaleResponse>> FilterSaleByCustomer(Guid customerId)
        {
            string query = $@"{baseQuery} WHERE CustomerId = @customerId";
            return await context.QueryAsync<SaleResponse>(query, new { customerId });
        }




        public async Task<IEnumerable<Bill>> GenerateCustomerBill(Guid customerId, Guid hatchId)
        {
            string query = $@"SELECT S.Id as SaleId,
                            S.CustomerId,C.[Name],
                            C.Email,S.Rate, S.NoOfKilograms ,
                            S.CreatedOn AS SoldOn,C.ContactNo as CustomerContact,
                            S.Rate * S.NoOfKilograms as TotalAmount
                            FROM Sales S
                            INNER JOIN Customers C
                            ON S.CustomerId = C.Id
                            WHERE CustomerId = @customerId
                            AND HatchId = @hatchId";

            return await context.QueryAsync<Bill>(query, new { customerId, hatchId });
        }




        public async Task<IEnumerable<SaleResponse>> NewlyAddedSales(Guid hatchId)
        {
            string query = $@"{baseQuery} WHERE HatchId = @hatchId ORDER BY S.CreatedOn Desc";
            return await context.QueryAsync<SaleResponse>(query, new { hatchId });
        }




        public async Task<SaleDetails?> Sale(Guid id)
        {
            string query = $@"SELECT S.Id,C.Name AS CustomerName,
                            C.Email as CustomerEmail,
                            S.PaymentStatus ,
                            S.Rate ,
                            S.CustomerId,
							H.Name AS HatchName,
							H.NoOfChicks ,
							H.Id AS HatchId,
							H.CreatedOn as HatchDate,
                            S.NoOfKilograms,
                            S.CreatedOn AS SaleDate,
                            S.NoOfKilograms * Rate AS TotalAmount FROM Sales S
							INNER JOIN Customers C
							ON S.CustomerId = C.Id
							INNER JOIN Hatches H
							ON H.Id = S.HatchId
							WHERE S.Id = @id";
            return await context.FirstOrdefaultAsync<SaleDetails?>(query, new { id });
        }




        public async Task<IEnumerable<SaleResponse>> ViewPendingSalePayment(Guid hatchId)
        {
            string query = $@"{baseQuery} WHERE HatchId = @hatchId AND PaymentStatus = 2";
            return await context.QueryAsync<SaleResponse>(query, new { hatchId });
        }




        public async Task<IEnumerable<SaleResponse>> ViewSale(Guid hatchId)
        {
            string query = $@"{baseQuery} WHERE HatchId = @hatchId";

            return await context.QueryAsync<SaleResponse>(query, new { hatchId });
        }
    }
}
