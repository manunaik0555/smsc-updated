﻿using Microsoft.AspNetCore.Identity;

using server.api.DataAccess;

using System.Data;
using System.Data.Common;

namespace server.api.Identity;

public class SCMSUser : IdentityUser<ulong>
{
}
