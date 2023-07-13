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

namespace Post.Application.Queries.SalePostQ
{
    public class ViewAllSalePostQuery : IRequest<PagedList<SalePost>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public string? Id { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
		public string? CreatedBy { get; set; }
		public string? Ward { get; set; }
		public string? Region { get; set; }

		public int? Type { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? DueDate { get; set; }

		public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SortFeild { get; set; }
        public bool? SortValue { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class ViewAllSalePostQueryHandler : IRequestHandler<ViewAllSalePostQuery, PagedList<SalePost>>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public ViewAllSalePostQueryHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PagedList<SalePost>> Handle(ViewAllSalePostQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SearchSalePost(request.Id,request.UserId, request.Title,request.CreatedBy,
                request.Ward,request.Region,request.Status,request.Type,request.CreatedDate,request.DueDate,request.FromDate,
                request.ToDate,request.SortFeild,request.SortValue, request.Page, request.PageSize);
        }
    }
}
