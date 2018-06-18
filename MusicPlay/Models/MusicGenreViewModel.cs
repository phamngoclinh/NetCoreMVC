using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MusicPlay.Models
{
    public class MusicGenreViewModel
    {
        public List<Music> musics;
        public SelectList genres;
        public string musicGenre { get; set; }
    }
}