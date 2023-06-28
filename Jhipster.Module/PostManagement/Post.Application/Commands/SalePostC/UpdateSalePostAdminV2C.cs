using AutoMapper;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.SalePostC
{
    public class UpdateSalePostAdminV2C : IRequest<int>
    {
        public string Id { get; set; }
        public string? Titile { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public double? Price { get; set; }
        public double? Area { get; set; }
        public string? Region { get; set; }
        public string? Ward { get; set; }
        public List<string>? Image { get; set; }
        public int? IsOwner { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Documents { get; set; }
        public string? Reason { get; set; }
        public int Unit { get; set; }


    }
    public class UpdateSalePostAdminV2CHandler : IRequestHandler<UpdateSalePostAdminV2C, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public UpdateSalePostAdminV2CHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateSalePostAdminV2C request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateSaleAdminV2(request, cancellationToken);
        }
    }
}
