using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.SalePostC
{
    public class DeleteSalePostCommand : IRequest<int>
    {
        public List<string> ListId { get; set; }

    }
    public class DeleteSalePostCommandHandler : IRequestHandler<DeleteSalePostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public DeleteSalePostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteSalePostCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteSalePost(request.ListId, cancellationToken);
        }
    }

}
