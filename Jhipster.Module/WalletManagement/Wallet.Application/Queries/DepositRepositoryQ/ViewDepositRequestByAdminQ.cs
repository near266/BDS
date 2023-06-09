using Jhipster.Crosscutting.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.DepositRepositoryQ
{
    public class ViewDepositRequestByAdminQ : IRequest<PagedList<DepositRequest>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? UserName { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateFrom { get; set; }
    }
    public class ViewDepositRequestByAdminQHandler : IRequestHandler<ViewDepositRequestByAdminQ, PagedList<DepositRequest>>
    {
        private readonly IDepositRequestRepository _depositRequestRepository;
        public ViewDepositRequestByAdminQHandler(IDepositRequestRepository depositRequestRepository)
        {
            _depositRequestRepository = depositRequestRepository;
        }

        public async Task<PagedList<DepositRequest>> Handle(ViewDepositRequestByAdminQ request, CancellationToken cancellationToken)
        {
            return await _depositRequestRepository.GetByAdmin(request.Page, request.PageSize,request.UserName,request.DateTo,request.DateFrom);
        }
    }
}
