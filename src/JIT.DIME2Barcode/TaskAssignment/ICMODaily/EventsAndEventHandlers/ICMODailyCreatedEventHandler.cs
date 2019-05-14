using System;
using System.Linq;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Handlers;
using JIT.DIME2Barcode.Entities;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.EventsAndEventHandlers
{
    [UnitOfWork]
    public class ICMODailyCreatedEventHandler: IEventHandler<ICMODailyCreatedtEventData>, ITransientDependency
    {

        public IRepository<ICMOSchedule,string> SRepository { get; set; }

        public async void HandleEvent(ICMODailyCreatedtEventData eventData)
        {
            var entity =await SRepository.GetAll().SingleOrDefaultAsync(p =>
                p.FID==eventData.FSrcID);

            if (entity == null)
            {
                entity = new ICMOSchedule()
                {
                    FID = eventData.FSrcID,
                    FBillTime = DateTime.Now,
                    FBillNo = "SC-"+eventData.FMOBillNo,
                    FBiller = eventData.FBiller,
                    FPlanAuxQty = eventData.FPlanAuxQty
                };
                SRepository.Insert(entity);
            }
            else
            {
                entity.FPlanAuxQty = eventData.FPlanAuxQty;

                 SRepository.Update(entity);
            }
        }
    }
}