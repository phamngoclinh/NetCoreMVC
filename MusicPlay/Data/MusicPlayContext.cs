using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicPlay.Models
{
    public class MusicPlayContext : DbContext
    {
        public MusicPlayContext (DbContextOptions<MusicPlayContext> options)
            : base(options)
        {
        }

        public DbSet<MusicPlay.Models.Music> Music { get; set; }
    }
}
