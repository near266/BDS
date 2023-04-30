﻿using MediatR;
using Post.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Commands.AdminC
{
    public class ApprovePostCommand : IRequest<int>
    {
        public int postType { get; set; }
        public string id {get;set;}
        public int status {get;set;}
        public DateTime? modifiedDate {get;set;}
        public string? modifiedBy {get;set;}
    }
    public class ApprovePostCommandHandler : IRequestHandler<ApprovePostCommand, int>
    {
        private readonly IPostRepository _postRepository;
        public ApprovePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<int> Handle(ApprovePostCommand request, CancellationToken cancellationToken)
        {
            return await _postRepository.ApprovePost(request.postType, request.id, request.status, request.modifiedDate, request.modifiedBy, cancellationToken);
        }
    }   
}
