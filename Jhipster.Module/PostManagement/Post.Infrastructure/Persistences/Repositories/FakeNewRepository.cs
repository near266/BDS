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
                    int randomNumber ;
                    int placeholderNumber = int.Parse(match.Groups[1].Value);

                    if (placeholderNumber == 1)
                    {
                        randomNumber =  db.Order + random.Next(1, 6);
                        db.Order = randomNumber;
                    }
                    else if (placeholderNumber == 2)
                    {
                        randomNumber = db.OrderMax+ random.Next(10, 100);
                        db.OrderMax= randomNumber;
                    }
                    else
                    {
                        randomNumber = 4;
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
