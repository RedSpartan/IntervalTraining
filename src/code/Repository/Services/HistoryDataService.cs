using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedSpartan.IntervalTraining.Repository.Access;
using RedSpartan.IntervalTraining.Repository.Data.Entities;
using RedSpartan.IntervalTraining.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.Repository.Services
{
    public class HistoryDataService : IDataService<HistoryDto>
    {
        private readonly IMapper _mapper;
        private readonly IContextFactory _contextFactory;

        public HistoryDataService(IMapper mapper, IContextFactory contextFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<bool> AddItemAsync(HistoryDto item)
        {
            using (var context = _contextFactory.GetContext())
            {
                context.Histories.Add(_mapper.Map<History>(item));

                await context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
        }

        public async Task<bool> UpdateItemAsync(HistoryDto item)
        {
            using (var context = _contextFactory.GetContext())
            {
                var oldItem = await context.Histories.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                context.Histories.Remove(oldItem);
                context.Histories.Add(_mapper.Map<History>(item));

                await context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var context = _contextFactory.GetContext())
            {
                var oldItem = await context.Histories.Where(x => x.Id == id).FirstOrDefaultAsync();
                context.Histories.Remove(oldItem);

                await context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
        }

        public async Task<HistoryDto> GetItemAsync(int id)
        {
            using (var context = _contextFactory.GetContext())
            {
                var item = await context.Histories.Where(x => x.Id == id).FirstOrDefaultAsync();

                return _mapper.Map<HistoryDto>(item);
            }
        }

        public async Task<IEnumerable<HistoryDto>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var context = _contextFactory.GetContext())
            {
                var items = await context.Histories.ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<HistoryDto>>(items);
            }
        }
    }
}
