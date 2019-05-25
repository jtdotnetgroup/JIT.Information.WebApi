using System;
using System.Linq.Expressions;
using Abp.Specifications;
using JIT.DIME2Barcode.Entities;

namespace JIT.DIME2Barcode.SystemSetting.Organization.ISpecification
{
    public class OrganizationTypeSpecification:Specification<OrganizationUnit>
    {

        public int OrganizationType { get; set; }

        public OrganizationTypeSpecification(int TypeId)
        {
            OrganizationType = TypeId;
        }

        public override Expression<Func<OrganizationUnit, bool>> ToExpression()
        {
            return (org) => (org.OrganizationType==OrganizationType);
        }
    }
}