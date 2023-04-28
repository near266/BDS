﻿using AutoMapper;
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
    public class UpdateSalePostCommand : IRequest<int>
    {
        public int Type { get; set; }

        [MaxLength(25)]
        public string? Titile { get; set; }
        [MaxLength(3000)]
        public string? Description { get; set; }

        public List<string>? Image { get; set; }

        public int IsOwner { get; set; }

        public string Username { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
    }
    public class UpdateSalePostCommandHandler : IRequestHandler<UpdateSalePostCommand, int>
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public UpdateSalePostCommandHandler(IPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateSalePostCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<SalePost>(request);
            return await _repository.UpdateSalePost(map, cancellationToken);
        }
    }
}
