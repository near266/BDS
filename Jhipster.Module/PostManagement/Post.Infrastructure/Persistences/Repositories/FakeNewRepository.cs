using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Post.Application.Contracts;
using Post.Domain.Abstractions;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Post.Infrastructure.Persistences.Repositories
{
    public class FakeNewRepository : IFakeNewRepository
    {
        private readonly IPostDbContext _dbContext;
        public FakeNewRepository(IPostDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddFakeNew(FakeNew rq, CancellationToken cancellationToken)
        {
            rq.CreatedDate = DateTime.Now;
            rq.Order = 0;
            rq.OrderMax = 0;
            await _dbContext.FakeNew.AddAsync(rq);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> DeleteFakeNew(Guid Id, CancellationToken cancellationToken)
        {
            var check = await _dbContext.FakeNew.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new Exception("Fail");
            else
            {
                _dbContext.FakeNew.Remove(check);
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<int> Update(Guid Id, string? Title, CancellationToken cancellationToken)
        {
            var check = await _dbContext.FakeNew.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new Exception("Fail");
            else
            {
                check.OrderMax = 0;
                check.Order = 0;
                check.Title = Title != null ? Title : check.Title;
                _dbContext.FakeNew.Update(check);
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<string> ViewFakeNew(CancellationToken cancellationToken)
        {
            var listFake = await _dbContext.FakeNew.ToListAsync();
            var random = new Random();
            int n = listFake.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                FakeNew temp = listFake[i];
                listFake[i] = listFake[j];
                listFake[j] = temp;
            }
            var db = listFake.FirstOrDefault();
            if (db != null)
            {
                var repo = db.Title;
                string modifiedSentence = Regex.Replace(repo, @"\{(\d+)\}", match =>
                {
                    int randomNumber;
                    int placeholderNumber = int.Parse(match.Groups[1].Value);

                    if (placeholderNumber == 1)
                    {
                        randomNumber = db.Order + random.Next(1, 3);
                        db.Order = randomNumber;
                    }
                    else if (placeholderNumber == 2)
                    {
                        randomNumber = db.OrderMax + random.Next(10, 30);
                        db.OrderMax = randomNumber;
                    }
                    else if (placeholderNumber == 3)
                    {
                        randomNumber = random.Next(30, 90);
                    }
                    else
                    {
                        randomNumber = random.Next(200, 500);
                    }
                    return randomNumber.ToString();
                });
                _dbContext.FakeNew.Update(db);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return modifiedSentence;
            }
            else throw new Exception("Chưa có tin");

        }
    }
}
