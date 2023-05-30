using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.NewPostC
{
    public class DeleteNewPostCommand : IRequest<int>
    {
        public List<string> ListId { get; set; }
    }
    public class DeleteNewPostCommandHandler : IRequestHandler<DeleteNewPostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public DeleteNewPostCommandHandler(IPostRepository repository,IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteNewPostCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteNewPost(request.ListId, cancellationToken);
        }
    }
}
