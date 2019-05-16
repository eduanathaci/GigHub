using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class FollowingViewModel
    {
        public List<Following> Artists { get; set; }
        public string FollowerID { get; set; }
    }
}