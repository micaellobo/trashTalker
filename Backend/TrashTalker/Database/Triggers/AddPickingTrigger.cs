using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using TrashTalker.Models;

namespace TrashTalker.Database.Triggers
{
    public class AddPickingTrigger : IBeforeSaveTrigger<Picking>, IAfterSaveTrigger<Picking>
    {
        private readonly DatabaseContext _dbContext;

        public AddPickingTrigger(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeforeSave(ITriggerContext<Picking> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                //Find last picking from this container
                var lastPicking = await _dbContext.Pickings
                    .FromSqlInterpolated(
                        $"SELECT TOP 1 * FROM Pickings WHERE containerid = {context.Entity.container.id.ToString()} ORDER BY data DESC")
                    .FirstOrDefaultAsync();

                //Find actual occupied volume
                var lastMeasurement = await _dbContext.Measurements
                    .FromSqlInterpolated(
                        $"SELECT TOP 1 * FROM Measurements WHERE containerid = {context.Entity.container.id.ToString()} ORDER BY date DESC")
                    .FirstOrDefaultAsync();

                if (lastMeasurement != null)
                {
                    var percOccuppied = 100.0 - Math.Round(lastMeasurement.distance / (context.Entity.container.depth * 100), 2,
                        MidpointRounding.ToEven);
                    
                    

                    //Store the average growth occupied volume per day this picking 
                    context.Entity.volumeRecolhido = 3 * 3 * (3 - lastMeasurement.distance / 100);
                    if (lastPicking != null)
                        context.Entity.avgGrowthOccupiedPerDay = Math.Round(percOccuppied / (DateTime.Now - lastPicking.data).TotalDays, 3);
                }
            }
        }

        public async Task AfterSave(ITriggerContext<Picking> context, CancellationToken cancellationToken)
        {
            var resSumAllAverage = await _dbContext.Pickings
                .Where(picking => picking.container.id == context.Entity.container.id)
                .SumAsync(p => p.avgGrowthOccupiedPerDay);

            var resSCountAllAverage = await _dbContext.Pickings
                .Where(picking => picking.container.id == context.Entity.container.id)
                .CountAsync();

            var res = Convert.ToDecimal(resSumAllAverage) / Convert.ToDecimal(resSCountAllAverage);

            var avgGrowthOccupiedVolumePerDay = Math.Round(res, 2, MidpointRounding.AwayFromZero);

            //Update the average growth occupied volume for this container  
            var containerDb = await _dbContext.Containers.Where(container => container.id.Equals(context.Entity.container.id))
                .FirstOrDefaultAsync();

            containerDb.avgGrowthOccupiedVolumePerDay = (double) avgGrowthOccupiedVolumePerDay;
            containerDb.dateFull = null;

            await _dbContext.SaveChangesAsync();
        }
    }
}