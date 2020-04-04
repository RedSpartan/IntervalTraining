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
    public class IntervalDataService : IDataService<IntervalTemplateDto>
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _databaseContext;

        public IntervalDataService(IMapper mapper, DatabaseContext databaseContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public async Task<bool> AddItemAsync(IntervalTemplateDto item)
        {
            _databaseContext.Intervals.Add(_mapper.Map<IntervalTemplate>(item));

            await _databaseContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(IntervalTemplateDto item)
        {
            var oldItem = await _databaseContext.Intervals.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
            _databaseContext.Intervals.Remove(oldItem);
            _databaseContext.Intervals.Add(_mapper.Map<IntervalTemplate>(item));
            
            await _databaseContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = await _databaseContext.Intervals.Where(x => x.Id == id).FirstOrDefaultAsync();
            _databaseContext.Intervals.Remove(oldItem);

            await _databaseContext.SaveChangesAsync();

            return await Task.FromResult(true);
        }

        public async Task<IntervalTemplateDto> GetItemAsync(int id)
        {
            var item = await _databaseContext.Intervals.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<IntervalTemplateDto>(item);
        }

        public async Task<IEnumerable<IntervalTemplateDto>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = await _databaseContext.Intervals.ToListAsync().ConfigureAwait(false);

            return _mapper.Map<List<IntervalTemplateDto>>(items);
        }
    }
}
