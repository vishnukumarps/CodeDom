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
    
    
    [ApiController(null)]
    [Route("[controller]")]
    public class VesselController13:Controller
    {
        
        // The FirstName of the object.
        private string FirstName;
        
        // The LastName of the object.
        private string LastName;
        
        /// this is String property
public String Name { get; set; }
        /// this is String property
public String Category { get; set; }
        
        public virtual void AddVessel()
        {
            Random rn = new Random();
            rn.Next(1000,2000);
        }
    }
}
