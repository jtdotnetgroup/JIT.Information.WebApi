using Abp.Application.Services;
using Abp.Domain.Repositories;
using JIT.JIT.TaskAssignment.BillStatus.Dtos;

namespace JIT.JIT.TaskAssignment.BillStatus
{
    public class BillStatusAppService:AsyncCrudAppService<DIME2Barcode.Entities.BillStatus,BillStatusDto,int,BillStatusGetAllInput,BillStatusDto,BillStatusDto,BillStatusDto,BillStatusDto>
    {
        public BillStatusAppService(IRepository<DIME2Barcode.Entities.BillStatus, int> repository) : base(repository)
        {
        }
    }
}