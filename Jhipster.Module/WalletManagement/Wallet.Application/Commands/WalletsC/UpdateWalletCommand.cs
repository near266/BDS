using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletsC
{
    public class UpdateWalletCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }
        public decimal? CusAmount { get; set; }
        public decimal? CusAmountPromotion { get; set; }
    }
    public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, int>
    {
        private readonly IWalletRepository _repo;
        private readonly IMapper _mapper;
        private readonly ITransactionHistoryRepository _tRepository;

        public UpdateWalletCommandHandler(IWalletRepository repo, ITransactionHistoryRepository tRepository, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _tRepository = tRepository;
        }
        public async Task<int> Handle(UpdateWalletCommand rq, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<WalletEntity>(rq);
            var his = new TransactionHistory()
            {
                Id = Guid.NewGuid(),
                Type = 0,
                Content = "Nạp tiền tài khoản chính",
                TransactionAmount = (double?)rq.Amount,
                WalletType = 0,
                CustomerId = rq.CustomerId,
                Amount = rq.CusAmount + rq.Amount,
                CreatedDate = DateTime.Now,
                Walletamount = rq.CusAmountPromotion,
            };
            var res = await _tRepository.Add(his, cancellationToken);
            return await _repo.Update(obj, cancellationToken);
        }
    }
}
