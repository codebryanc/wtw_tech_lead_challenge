using Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly List<Requests> _mockData;

        public RequestRepository()
        {
            _mockData = new List<Requests>
            {
                new Requests
                {
                    reqId = Guid.Parse("F2348456-A1E0-4EF1-B777-4651B392D14D"),
                    rtyId = Guid.Parse("F2348456-A1E0-4EF1-B777-4651B392D14D"),
                    resId = Guid.Parse("9B484C2A-3D94-4C40-BC12-E61F3A8B0302"),
                    createdAt = DateTime.Parse("2025-07-14 12:25:46.6333333"),
                    data = "{\"date\":\"2025-07-05\",\"hours\":1,\"reason\":\"Early leave\"}"
                },
                new Requests
                {
                    reqId = Guid.Parse("07D4BCF7-6AF6-49B1-955B-1AA245D76B11"),
                    rtyId = Guid.Parse("BB1DF24E-51B2-4D41-8FB8-738B647F6AE2"),
                    resId = Guid.Parse("9B484C2A-3D94-4C40-BC12-E61F3A8B0302"),
                    createdAt = DateTime.Parse("2025-07-14 12:25:46.6333333"),
                    data = "{\"amount\":2000,\"installments\":6,\"reason\":\"Medical expenses\"}"
                },
                new Requests
                {
                    reqId = Guid.Parse("3C733954-DB37-4322-8B5E-2C9810E3E021"),
                    rtyId = Guid.Parse("1ED88739-26A8-4F62-842C-5F8019F5B791"),
                    resId = Guid.Parse("580BB21A-9624-48AE-B21B-34C4FF480551"),
                    createdAt = DateTime.Parse("2025-07-14 12:25:46.6333333"),
                    data = "{\"startDate\":\"2025-12-01\",\"endDate\":\"2026-01-05\",\"reason\":\"Holiday\"}"
                }
            };
        }

        public async Task<IEnumerable<Requests>> GetAllAsync()
        {
            return await Task.FromResult(_mockData);
        }
    }
}