using AutoMapper;
using MediatR;
using Post.Application.Commands.NewPostC;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.DistrictC
{
    public class DeleteDistrictCommand: IRequest<int>
    {
        public List<string> ListId { get; set; }
    }
    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public DeleteDistrictCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteDistrict(request.ListId, cancellationToken);
        }
    }
}
