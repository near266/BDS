using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Queries.FakeNewQ
{
    public class ViewFakeNewQuery : IRequest<string>
    {
    }
    public class ViewFakeNewQueryHandler : IRequestHandler<ViewFakeNewQuery, string>
    {
        private readonly IFakeNewRepository _repository;
        public ViewFakeNewQueryHandler(IFakeNewRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(ViewFakeNewQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ViewFakeNew(cancellationToken);
        }
    }
}
