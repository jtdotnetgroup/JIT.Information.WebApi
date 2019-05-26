﻿using System;
using System.Linq.Expressions;
using Abp.Specifications;
using CommonTools;
using JIT.DIME2Barcode.Entities;

namespace JIT.DIME2Barcode.SystemSetting.Organization.ISpecification
{
    public class OrganizationTypeSpecification:Specification<OrganizationUnit>
    {

        public PublicEnum.OrganizationType OrganizationType { get; set; }

        public OrganizationTypeSpecification(int TypeId)
        {
            OrganizationType = Enum.Parse<PublicEnum.OrganizationType>(TypeId.ToString());
        }

        public override Expression<Func<OrganizationUnit, bool>> ToExpression()
        {
            return (org) => (org.OrganizationType==OrganizationType);
        }
    }
}