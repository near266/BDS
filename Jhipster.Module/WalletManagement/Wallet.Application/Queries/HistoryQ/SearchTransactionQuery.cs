using Jhipster.Crosscutting.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.HistoryQ
{
    public class SearchTransactionQuery : IRequest<PagedList<SearchTransactionResponse>>
    {
        public string? UserId { get; set; }
        public int? Type { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string? Code { get; set; }
    }
    public class SearchTransactionQueryHandler : IRequestHandler<SearchTransactionQuery, PagedList<SearchTransactionResponse>>
    {
        private readonly ITransactionHistoryRepository _repo;
        public SearchTransactionQueryHandler(ITransactionHistoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<PagedList<SearchTransactionResponse>> Handle(SearchTransactionQuery rq, CancellationToken cancellationToken)
        {
            return await _repo.Search(rq.Code, rq.UserId, rq.Type, rq.From, rq.To, rq.page, rq.pageSize);
        }
    }
}
