using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.FakeNewC
{
    public class UpdateFakeNewCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
    public class UpdateFakeNewCommandHandler : IRequestHandler<UpdateFakeNewCommand, int>
    {
        private readonly IFakeNewRepository _repo;
        public UpdateFakeNewCommandHandler(IFakeNewRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(UpdateFakeNewCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Update(request.Id, request.Title, cancellationToken);
        }
    }
}
