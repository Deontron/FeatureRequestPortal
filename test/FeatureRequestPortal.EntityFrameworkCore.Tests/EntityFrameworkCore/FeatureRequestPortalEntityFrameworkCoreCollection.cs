﻿using Xunit;

namespace FeatureRequestPortal.EntityFrameworkCore;

[CollectionDefinition(FeatureRequestPortalTestConsts.CollectionDefinitionName)]
public class FeatureRequestPortalEntityFrameworkCoreCollection : ICollectionFixture<FeatureRequestPortalEntityFrameworkCoreFixture>
{

}
