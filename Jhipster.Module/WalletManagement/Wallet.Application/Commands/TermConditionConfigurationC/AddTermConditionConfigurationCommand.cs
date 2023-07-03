using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Commands.PriceConfigurationC;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.TermConditionConfigurationC
{
    public class AddTermConditionConfigurationCommand : IRequest<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public Guid? TypeTermId { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
    }
    public class AddNewTermConditionConfigurationCommandHandler : IRequestHandler<AddTermConditionConfigurationCommand, int>
    {
        private readonly ITermConditionConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public AddNewTermConditionConfigurationCommandHandler(ITermConditionConfigurationRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddTermConditionConfigurationCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<TermConditionConfiguration>(request);
            return await _repo.Add(map,cancellationToken);
        }
    }
}
