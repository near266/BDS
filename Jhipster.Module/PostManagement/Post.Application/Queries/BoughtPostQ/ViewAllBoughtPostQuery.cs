using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Contracts;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Post.Application.Queries.BoughtPostQ
{
    public class ViewAllBoughtPostQuery : IRequest<PagedList<BoughtPost>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Id { get;set; }
        public string? Title { get; set; }
		public string? CreatedBy { get; set; }
		public string? Ward { get; set; }
		public string? Region { get; set; }

		public int? Status { get; set; }
		public DateTime? CreatedDate { get; set; }

		public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Page { get;set; }
        public int PageSize { get; set; }
    }
    public class ViewAllBoughtPostQueryHandler : IRequestHandler<ViewAllBoughtPostQuery, PagedList<BoughtPost>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllBoughtPostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedList<BoughtPost>> Handle(ViewAllBoughtPostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchBoughtPost(request.Id,request.UserId,request.Title,request.CreatedBy,request.Ward,request.Region,request.Status,request.CreatedDate,request.FromDate,request.ToDate,request.Page,request.PageSize);
        }
    }
}
