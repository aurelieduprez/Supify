using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supify.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
    }
}
