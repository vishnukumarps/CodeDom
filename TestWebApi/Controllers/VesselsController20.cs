//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vesseels
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    
    
    [Route("api/[controller]")]
    [ApiController()]
    public class VesselsController20:Controller
    {
        
        // The FirstName of the object.
        private string FirstName;
        
        // The LastName of the object.
        private string LastName;
        
        /// this is String property
public String Name { get; set; }
        /// this is String property
public String Category { get; set; }
        
        [HttpGet()]
        [Route("GetVesselNumber")]
        public virtual String GetVesselNumber()
        {
            Random rn = new Random();
            var x=rn.Next(1000,2000);
            return x.ToString();
        }
    }
}
