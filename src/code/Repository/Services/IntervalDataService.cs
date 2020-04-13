using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedSpartan.IntervalTraining.Internal.Repository.Access;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Internal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedSpartan.IntervalTraining.Repository.Services
{
    public class IntervalDataService : IDataService<IntervalTemplateDto>
    {
        private readonly IMapper _mapper;
        private readonly IContextFactory _contextFactory;

        public IntervalDataService(IMapper mapper, IContextFactory contextFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task<bool> AddItemAsync(IntervalTemplateDto item)
        {
            using (var context = _contextFactory.GetContext())
            {
                context.Intervals.Add(_mapper.Map<IntervalTemplate>(item));

                await context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
        }

        public async Task<bool> UpdateItemAsync(IntervalTemplateDto item)
        {
            using (var context = _contextFactory.GetContext())
            {
                var oldItem = await context.Intervals.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                context.Intervals.Remove(oldItem);
                context.Intervals.Add(_mapper.Map<IntervalTemplate>(item));

                await context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var context = _contextFactory.GetContext())
            {
                foreach (var history in context.Histories.Where(x => x.TemplateId == id))
                {
                    history.TemplateId = null;
                }

                var oldItem = await context.Intervals.Where(x => x.Id == id).FirstOrDefaultAsync();
                context.Intervals.Remove(oldItem);

                await context.SaveChangesAsync();

                return await Task.FromResult(true);
            }
        }

        public async Task<IntervalTemplateDto> GetItemAsync(int id)
        {
            using (var context = _contextFactory.GetContext())
            {
                var item = await context.Intervals.Where(x => x.Id == id).FirstOrDefaultAsync();

                return _mapper.Map<IntervalTemplateDto>(item);
            }
        }

        public async Task<IEnumerable<IntervalTemplateDto>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var context = _contextFactory.GetContext())
            {
                var items = await context.Intervals.ToListAsync().ConfigureAwait(false);

                return _mapper.Map<List<IntervalTemplateDto>>(items);
            }
        }
    }
}
