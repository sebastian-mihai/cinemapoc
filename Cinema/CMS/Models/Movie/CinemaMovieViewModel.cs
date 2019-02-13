﻿using CMS.Models.CinemaMovie;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoreModels = Core.Models;

namespace CMS.Models.Movie
{
    public class CinemaMovieViewModel
    {
        public Guid MovieId { get; set; }

        public string MovieName { get; set; }

        [DisplayName("Movie Description")]
        public string MovieDescription { get; set; }

        public Dictionary<CinemaMovieModel, bool> CinemaDictionary { get; set; } = new Dictionary<CinemaMovieModel, bool>();
    }
}
