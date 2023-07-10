using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Commands.NewPostC
{
    public class UpdateNewPostCommand : IRequest<int>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? descriptionForList { get; set; }

        public string? Image { get; set; }

        [JsonIgnore]
        public string? LastModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class UpdateNewPostCommandHandler : IRequestHandler<UpdateNewPostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public UpdateNewPostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateNewPostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<NewPost>(request);
            return await _repository.UpdateNewPost(map, cancellationToken);
        }
    }
}
