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
    public class AddNewPostCommand:IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<string>? Image { get; set; }

        [JsonIgnore]
        public string ?CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
    }
    public class AddNewPostCommandHandler : IRequestHandler<AddNewPostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public AddNewPostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddNewPostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<NewPost>(request);
            return await _repository.AddNewPost(map, cancellationToken);
        }
    }
}
