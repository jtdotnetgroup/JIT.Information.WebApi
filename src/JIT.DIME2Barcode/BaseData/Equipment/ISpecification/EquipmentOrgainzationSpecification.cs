using System;
using System.Linq.Expressions;
using Abp.Specifications;
using CommonTools;

namespace JIT.DIME2Barcode.BaseData.Equipment.ISpecification
{
    public class EquipmentOrgainzationSpecification:Specification<Entities.Equipment>
    {
        private int OrgID { get; set; }

        public EquipmentOrgainzationSpecification(int orgID)
        {
            OrgID = orgID;
        }

        public override Expression<Func<Entities.Equipment, bool>> ToExpression()
        {
            return (eq) => eq.FWorkCenterID == OrgID;
        }
    }
}