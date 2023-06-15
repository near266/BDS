using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.BoughtPostC
{
    public class UpdateBoughtPostAdminC : IRequest<int>
    {
        public string Id { get; set; }
        public string? titile { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public List<string>? Image { get; set; }

    }
    public class UpdateBoughtPostAdminCHandler : IRequestHandler<UpdateBoughtPostAdminC, int>
    {
        private readonly IPostRepository _repository;
        public UpdateBoughtPostAdminCHandler(IPostRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(UpdateBoughtPostAdminC request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateBoughtPostAdmin(request.Id, request.titile, request.Description, request.Status, request.Image, cancellationToken);
        }
    }
}
