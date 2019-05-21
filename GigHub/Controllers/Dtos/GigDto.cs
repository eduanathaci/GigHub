using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Controllers;
using GigHub.Models;

namespace GigHub.Controllers.Dtos
{
    public class GigDto
    {
        public int ID { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Artist { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public GenreDto Genre { get; set; }
    }
}