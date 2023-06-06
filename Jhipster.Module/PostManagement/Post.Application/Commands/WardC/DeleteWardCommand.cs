using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.WardC
{
    public class DeleteWardCommand:IRequest<int>
    {
        public List<string> ListId { get; set; }
    }
    public class DeleteWardCommandHandler : IRequestHandler<DeleteWardCommand,int> 
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public DeleteWardCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteWardCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteWard(request.ListId, cancellationToken);
        }
    }
}
