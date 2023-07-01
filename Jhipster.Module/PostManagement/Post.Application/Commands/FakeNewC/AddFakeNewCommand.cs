using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.FakeNewC
{
    public class AddFakeNewCommand : IRequest<int>
    {
        public string Title { get; set; }
        public int Status { get; set; }
        [JsonIgnore]
        public int Order { get; set; }
        [JsonIgnore]
        public int OrderMax { get; set; }
    }
    public class AddFakeNewCommandHandler : IRequestHandler<AddFakeNewCommand, int>
    {
        private readonly IFakeNewRepository _repo;
        public AddFakeNewCommandHandler(IFakeNewRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(AddFakeNewCommand request, CancellationToken cancellationToken)
        {
            var rep = new FakeNew();
            rep.Title = request.Title;
            rep.Status = request.Status;
            rep.Order = 0;
            rep.OrderMax = 0;
            return await _repo.AddFakeNew(rep, cancellationToken);

        }
    }
}
