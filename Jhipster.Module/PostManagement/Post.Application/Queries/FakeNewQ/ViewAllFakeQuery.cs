using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.FakeNewQ
{
    public class ViewAllFakeQuery : IRequest<List<FakeNew>>
    {
    }
    public class ViewAllFakeQueryHandler : IRequestHandler<ViewAllFakeQuery, List<FakeNew>>
    {
        private readonly IFakeNewRepository _repository;
        public ViewAllFakeQueryHandler(IFakeNewRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FakeNew>> Handle(ViewAllFakeQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ViewAllFake();
        }
    }
}
