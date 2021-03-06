﻿using Core.Models.NoSql;
using System.Collections.Generic;
using System.ComponentModel;

namespace CMS.Models.Cinema
{
    public class CinemaDetailsViewModel
    {
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Contact")]
        public string Contact { get; set; }

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    }
}
