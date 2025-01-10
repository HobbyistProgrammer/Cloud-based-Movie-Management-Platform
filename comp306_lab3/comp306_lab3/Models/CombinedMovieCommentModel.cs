using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comp306_lab3.Models
{
    public class CombinedMovieCommentModel
    {
        public MovieModel movieModel { get; set; }
        public List<CommentModel> commentModel { get; set; }
    }
}
