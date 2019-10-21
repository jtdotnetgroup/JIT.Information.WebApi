using System;
using System.Linq.Expressions;
using Abp.Specifications;
using CommonTools;
using JIT.DIME2Barcode.Entities;

namespace JIT.DIME2Barcode.SystemSetting.Organization.ISpecification
{
    public   class OrganizationTypeSpecification:Specification<t_OrganizationUnit>
    {

        public PublicEnum.OrganizationType SOrganizationType { get; set; }

        public OrganizationTypeSpecification(int TypeId)
        {
            SOrganizationType = Enum.Parse<PublicEnum.OrganizationType>(TypeId.ToString());
        }

        public override Expression<Func<t_OrganizationUnit, bool>> ToExpression()
        {
            return (org) => (org.OrganizationType==SOrganizationType);
        }
    }
}