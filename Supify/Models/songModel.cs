using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supify.Models
{
    public class Song // defines how every song will be (its composition)
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
