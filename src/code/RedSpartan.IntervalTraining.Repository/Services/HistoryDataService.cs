using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly DatabaseContext _databaseContext;

        public HistoryDataService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<bool> AddItemAsync(HistoryDto item)
        {
            _databaseContext.Histories.Add(_mapper.Map<History>(item));

            await _databaseContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(HistoryDto item)
        {
            var oldItem = await _databaseContext.Histories.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
            _databaseContext.Histories.Remove(oldItem);
            _databaseContext.Histories.Add(_mapper.Map<History>(item));
            
            await _databaseContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = await _databaseContext.Histories.Where(x => x.Id == id).FirstOrDefaultAsync();
            _databaseContext.Histories.Remove(oldItem);

            await _databaseContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<HistoryDto> GetItemAsync(int id)
        {
            var item = await _databaseContext.Histories.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<HistoryDto>(item);
        }

        public async Task<IEnumerable<HistoryDto>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = await _databaseContext.Histories.ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<HistoryDto>>(items);
        }
    }
}
