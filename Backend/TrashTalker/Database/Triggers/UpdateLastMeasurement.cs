using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using TrashTalker.Models;
using TrashTalker.Services;

namespace TrashTalker.Database.Triggers
{
    public class UpdateLastMeasurement : IAfterSaveTrigger<Measurement>
    {
        private readonly DatabaseContext _dbContext;

        public UpdateLastMeasurement(DatabaseContext dbContext, IMeasuramentsService measuramentsService)
        {
            _dbContext = dbContext;
        }

        public async Task AfterSave(ITriggerContext<Measurement> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                var percOccupied = 100 - Math.Round(Convert.ToDecimal(context.Entity.distance / context.Entity.container.depth * 100), 2,
                    MidpointRounding.ToEven);

                var container = await _dbContext.Containers.FirstOrDefaultAsync(container => container.id == context.Entity.container.id);
                container.currentPercOccupied = (float) percOccupied;
                if (percOccupied == (decimal) 100.0)
                    container.dateFull = context.Entity.date;
                else
                    container.dateFull = null;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}