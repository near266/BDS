using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.FakeNewC
{
    public class DeleteFakeNewCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteFakeNewCommandHandler : IRequestHandler<DeleteFakeNewCommand, int>
    {
        private readonly IFakeNewRepository _repo;
        public DeleteFakeNewCommandHandler(IFakeNewRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeleteFakeNewCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteFakeNew(request.Id, cancellationToken);
        }
    }
}
