using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    //ApplicationUser is(Nothing but an Identity user) a type of Identity user which comes from microsoftAsp.NetCoreIdentity,
    //Which means ApplicationUser is a representation of a token
    //All my webMvc projects needs to know is, do i have a token
    public class ApplicationUser:IdentityUser
    {
    }
}
