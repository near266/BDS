using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletsPromotionaC
{
    public class UpdateWalletPromotionCommand : IRequest<int>
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
    public class UpdateWalletPromotionCommandHandler : IRequestHandler<UpdateWalletPromotionCommand, int>
    {
        private readonly IWalletPromotionalRepository _repo;
        private readonly IMapper _mapper;
        private readonly ITransactionHistoryRepository _tRepository;

        public UpdateWalletPromotionCommandHandler(IWalletPromotionalRepository repo, ITransactionHistoryRepository tRepository, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _tRepository = tRepository;
        }
        public async Task<int> Handle(UpdateWalletPromotionCommand rq, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<WalletPromotional>(rq);
            var his = new TransactionHistory()
            {
                Id = Guid.NewGuid(),
                Type = 3,
                Content = "Cộng tiền vào tài khoản khuyến mại",
                TransactionAmount = (double?)rq.Amount,
                WalletType = 1,
                CustomerId = rq.CustomerId,
                CreatedDate = DateTime.Now,
                Amount = rq.CusAmount,
                Walletamount = rq.CusAmountPromotion + rq.Amount,
            };
            his.Title = $"[{his.Id}] Cộng tiền vào tài khoản phụ";

            var res = await _tRepository.Add(his, cancellationToken);
            return await _repo.Update(obj, cancellationToken);
        }
    }
}
