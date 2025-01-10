using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comp306_lab3.Models
{
    public class CombinedMovieUserModel
    {
        public UserModel userModel { get; set; }
        public List<MovieModel> movieModel { get; set; }
    }
}
