using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.BoughtPostC
{
    public class DeleteBoughtPostCommand : IRequest<int>
    {
        public List<string> ListId { get; set; }
    }
    public class DeleteBoughtPostCommandHandler : IRequestHandler<DeleteBoughtPostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public DeleteBoughtPostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteBoughtPostCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteBoughtPost(request.ListId, cancellationToken);
        }
    }
}
