using MediatR;
using Post.Application.Commands.BoughtPostC;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.SalePostC
{
    public class UpdateSalePostAdminC : IRequest<int>
    {
        public string Id { get; set; }
        public string? titile { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public List<string>? Image { get; set; }
        public int Unit { get; set; }


    }
    public class UpdateSalePostAdminCHandler : IRequestHandler<UpdateSalePostAdminC, int>
    {
        private readonly IPostRepository _repository;
        public UpdateSalePostAdminCHandler(IPostRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(UpdateSalePostAdminC request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateSalePostAdmin(request.Id, request.titile, request.Description, request.Status, request.Image, cancellationToken);
        }
    }
}
