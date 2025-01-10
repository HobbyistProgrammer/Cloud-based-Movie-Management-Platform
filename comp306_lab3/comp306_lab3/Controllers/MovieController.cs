using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using comp306_lab3.Models;
using comp306_lab3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp306_lab3.Controllers
{
    public class MovieController : Controller
    {
        private string secretKey = "";
        private string accessKey = "";

        private BasicAWSCredentials awscreds = null;
        private AmazonDynamoDBClient client = null;

        public async Task<IActionResult> MovieListingAsync()
        {
            secretKey = "9wzCThzROHRLoZSA2wAslgppAMeCptFvWL8gRniO";
            accessKey = "AKIA47CRZWDC57V7WDHC";

            awscreds = new BasicAWSCredentials(accessKey, secretKey);
            client = new AmazonDynamoDBClient(awscreds, Amazon.RegionEndpoint.USEast1);

            CombinedMovieUserModel combinedModel = new CombinedMovieUserModel();

            if (HttpContext.Session.TryGetValue("UserModel", out var userModelData))
            {
                var userModelJson = Encoding.UTF8.GetString(userModelData);
                combinedModel.userModel = JsonConvert.DeserializeObject<UserModel>(userModelJson);
            }

            combinedModel.movieModel = await GetMovieList();

            return View("MovieListing", combinedModel);
        }
        public async Task<IActionResult> Index()
        {
            CombinedMovieUserModel combinedModel = new CombinedMovieUserModel();

            if (HttpContext.Session.TryGetValue("UserModel", out var userModelData))
            {
                var userModelJson = Encoding.UTF8.GetString(userModelData);
                combinedModel.userModel = JsonConvert.DeserializeObject<UserModel>(userModelJson);
            }

            combinedModel.movieModel = await GetMovieList();

            return View("../Movie/MovieListing", combinedModel);
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int Id)
        {
            MovieModel model = await GetMovieById(Id);
            return View("../Movie/EditMovie", model);
        }

        public async Task<IActionResult> PostComment(int movieId, string Content)
        {
            CombinedMovieUserModel combinedModel = new CombinedMovieUserModel();
            if (HttpContext.Session.TryGetValue("UserModel", out var userModelData))
            {
                var userModelJson = Encoding.UTF8.GetString(userModelData);
                combinedModel.userModel = JsonConvert.DeserializeObject<UserModel>(userModelJson);
            }

            combinedModel.movieModel = await GetMovieList();

            List<CommentModel> list = await GetComments();
            int count = list.Count;

            CommentModel model = new CommentModel
            {
                Id = count++,
                Comment = Content,
                Comment_Time = DateTime.Now.ToString(),
                Movie_Id = movieId,
                Comment_User = combinedModel.userModel.Id
            };

            SecurityService securityService = new SecurityService();

            await securityService.SaveComment(model);

            return View("../Movie/MovieListing", combinedModel);
        }

        public async Task<IActionResult> MovieComments(int movieId)
        {
            //Console.WriteLine("Showing Id: " + movieId);
            MovieModel model = await GetMovieById(movieId);

            //Console.WriteLine("This is comment: " + model.Title);

            List<CommentModel> listOfComments = await GetComments();
            List<CommentModel> filteredList = new List<CommentModel>();

            foreach (CommentModel comment in listOfComments)
            {
                if(comment.Movie_Id == model.Id)
                {
                    filteredList.Add(comment);
                    Console.WriteLine("This is view listing comment ids: " + comment.Id);
                }
            }

            CombinedMovieCommentModel combinedModel = new CombinedMovieCommentModel();

            combinedModel.movieModel = model;
            combinedModel.commentModel = filteredList;

            return View("../Movie/ViewComments", combinedModel);
        }
        //TODO: Fix updating issue with commenets, comments would only save to index 0

        public async Task<IActionResult> FilteredMovieList(string genre, string rating)
        {
            List<MovieModel> listOfMovies = await GetMovieList();

            if (genre != "All" && !string.IsNullOrEmpty(genre))
            {
                listOfMovies = listOfMovies.Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (rating != "All" && !string.IsNullOrEmpty(rating))
            {
                if (int.TryParse(rating, out int minRating))
                {
                    listOfMovies = listOfMovies.Where(m => m.Rating >= minRating).ToList();
                }
            }

            return View(listOfMovies);
        }

        public async Task<IActionResult> EditMovieAsync(MovieModel edittedMovie)
        {
            CombinedMovieUserModel combinedModel = new CombinedMovieUserModel();
            SecurityService securityService = new SecurityService();

            if (HttpContext.Session.TryGetValue("UserModel", out var userModelData))
            {
                var userModelJson = Encoding.UTF8.GetString(userModelData);
                combinedModel.userModel = JsonConvert.DeserializeObject<UserModel>(userModelJson);
            }

            Console.WriteLine("Editted - Movie Id: " + edittedMovie.Id + ", user: " + edittedMovie.User_Uploaded);

            edittedMovie.User_Uploaded = combinedModel.userModel.Id;

            _ = securityService.EditMovie(edittedMovie);

            

            combinedModel.movieModel = await GetMovieList();

            return View("../Movie/MovieListing", combinedModel);
        }

        public async Task<MovieModel> GetMovieById(int Id)
        {
            Console.WriteLine("Getting Movie by Id: " + Id);
            List<MovieModel> listOfMovies = await GetMovieList();
            MovieModel foundMovie = new MovieModel();
            foreach (MovieModel movie in listOfMovies)
            {
                if (Id == movie.Id)
                {
                    foundMovie = movie;
                }
            }
            return foundMovie;
        }

        [HttpPost]
        [Route("DeleteMovieAsync")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            SecurityService securityService = new SecurityService();
            CombinedMovieUserModel combinedModel = new CombinedMovieUserModel();

            await securityService.DeleteMovie(id);

            if (HttpContext.Session.TryGetValue("UserModel", out var userModelData))
            {
                var userModelJson = Encoding.UTF8.GetString(userModelData);
                combinedModel.userModel = JsonConvert.DeserializeObject<UserModel>(userModelJson);
            }

            combinedModel.movieModel = await GetMovieList();

            return View("../Movie/MovieListing", combinedModel);
        }

        public async Task<IActionResult> CreateMovieAsync(MovieModel movie, IFormFile file)
        {
            CombinedMovieUserModel combinedModel = new CombinedMovieUserModel();

            if (HttpContext.Session.TryGetValue("UserModel", out var userModelData))
            {
                var userModelJson = Encoding.UTF8.GetString(userModelData);
                combinedModel.userModel = JsonConvert.DeserializeObject<UserModel>(userModelJson);
                Console.WriteLine("Create Movie User: " + combinedModel.userModel.Username);
            }
            movie.User_Uploaded = combinedModel.userModel.Id;

            SecurityService securityService = new SecurityService();
            _ = securityService.AddMovie(movie, combinedModel.userModel.Id, file);

            List<MovieModel> movies = await GetMovieList();


            combinedModel.movieModel = movies;

            await PutFileInBucket(file);

            return View("../Movie/MovieListing", combinedModel);
        }

        public async Task<IActionResult> DownloadMovie(string fileName)
        {
            string bucketName = "bentonlelab3bucket";
            var s3Client = new AmazonS3Client(awscreds, Amazon.RegionEndpoint.USEast1);

            try
            {
                Console.WriteLine(fileName);
                var request = new Amazon.S3.Model.GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = $"{fileName}.mp4"
                };

                using (var response = await s3Client.GetObjectAsync(request))
                using (var responseStream = response.ResponseStream)
                {
                    var stream = new MemoryStream();

                    await responseStream.CopyToAsync(stream);
                    stream.Position = 0;

                    return File(stream, response.Headers["Content-Type"], $"{fileName}.mp4");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Error downloading movie: " + err);
                return NotFound(err.Message);
            }
        }

        public async Task<IActionResult> UpdateComment(int CommentId, int CommentUser, int MovieId, string updatedComment)
        {
            var listOfComments = await GetComments();
            CommentModel foundComment = new CommentModel();

            foreach (CommentModel comment in listOfComments)
            {
                Console.WriteLine("This is list of comment: " + comment.Id + ", compared Id: " + CommentId);
                if (comment.Id == CommentId)
                {
                    Console.WriteLine("This is found comment Id: " + CommentId);
                    foundComment.Id = CommentId;
                    foundComment.Comment_User = CommentUser;
                    foundComment.Movie_Id = MovieId;
                    foundComment.Comment = updatedComment;
                    foundComment.Comment_Time = DateTime.Now.ToString();
                }
            }

            SecurityService securityService = new SecurityService();

            await securityService.SaveUpdatedComment(foundComment);

            CombinedMovieUserModel combinedModel = new CombinedMovieUserModel();

            if (HttpContext.Session.TryGetValue("UserModel", out var userModelData))
            {
                var userModelJson = Encoding.UTF8.GetString(userModelData);
                combinedModel.userModel = JsonConvert.DeserializeObject<UserModel>(userModelJson);
                Console.WriteLine("Create Movie User: " + combinedModel.userModel.Username);
            }

            combinedModel.movieModel = await GetMovieList();

            return View("../Movie/MovieListing", combinedModel);
        }

        public async Task PutFileInBucket(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string bucketName = "bentonlelab3bucket";

                var s3Client = new AmazonS3Client(awscreds, Amazon.RegionEndpoint.USEast1);
                var fileTransferUtil = new TransferUtility(s3Client);

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    string keyName = $"{file.FileName}";
                    Console.WriteLine("key:" + keyName);
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = memoryStream,
                        Key = keyName,
                        BucketName = bucketName
                    };

                    await fileTransferUtil.UploadAsync(uploadRequest);


                }

            }
        }

        public async Task<List<CommentModel>> GetComments()
        {
            var request = new ScanRequest
            {
                TableName = "Comments"
            };

            var result = new List<CommentModel>();
            ScanResponse response;

            do
            {
                secretKey = "9wzCThzROHRLoZSA2wAslgppAMeCptFvWL8gRniO";
                accessKey = "AKIA47CRZWDC57V7WDHC";

                awscreds = new BasicAWSCredentials(accessKey, secretKey);
                client = new AmazonDynamoDBClient(awscreds, Amazon.RegionEndpoint.USEast1);

                response = await client.ScanAsync(request);

                foreach (var item in response.Items)
                {
                    try
                    {
                        var comment = new CommentModel
                        {
                            Id = item.ContainsKey("Id") ? int.Parse(item["Id"].S) : 0,
                            Comment = item.ContainsKey("Comment") ? item["Comment"].S : "Unknown",
                            Comment_Time = item.ContainsKey("Comment_Time") ? item["Comment_Time"].S : "Unknown",
                            Comment_User = item.ContainsKey("Comment_User") && !string.IsNullOrEmpty(item["Comment_User"].N) ? int.Parse(item["Comment_User"].N) : 0,
                            Movie_Id = item.ContainsKey("Movie_Id") && !string.IsNullOrEmpty(item["Movie_Id"].N) ? int.Parse(item["Movie_Id"].N) : 0,
                        };
                        Console.WriteLine("Getting COmments Id: " + comment.Id);
                        result.Add(comment);
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine("Error Getting Comments: " + err);
                    }
                }

                request.ExclusiveStartKey = response.LastEvaluatedKey;

            } while (response.LastEvaluatedKey.Count > 0);

            return result;
        }

        public async Task<List<MovieModel>> GetMovieList()
        {
            var request = new ScanRequest
            {
                TableName = "Movies"
            };


            secretKey = "9wzCThzROHRLoZSA2wAslgppAMeCptFvWL8gRniO";
            accessKey = "AKIA47CRZWDC57V7WDHC";

            awscreds = new BasicAWSCredentials(accessKey, secretKey);
            client = new AmazonDynamoDBClient(awscreds, Amazon.RegionEndpoint.USEast1);


            var result = new List<MovieModel>();
            ScanResponse response;
            int movieCount = 0;

            do
            {
                response = await client.ScanAsync(request);

                foreach (var item in response.Items)
                {
                    try
                    {
                        movieCount++;
                        var movie = new MovieModel
                        {
                            Id = item.ContainsKey("Id") && !string.IsNullOrEmpty(item["Id"].N) ? int.Parse(item["Id"].N) : 0,
                            Title = item.ContainsKey("Title") ? item["Title"].S : "Unknown",
                            Genre = item.ContainsKey("Genre") ? item["Genre"].S : "Unknown",
                            Director = item.ContainsKey("Director") ? item["Director"].S : "Unknown",
                            Release_Date = item.ContainsKey("Release_Date") ? item["Release_Date"].S : "N/A",
                            User_Uploaded = item.ContainsKey("User_Uploaded") && !string.IsNullOrEmpty(item["User_Uploaded"].N) ? int.Parse(item["User_Uploaded"].N) : 0,
                            Duration = item.ContainsKey("Duration") && !string.IsNullOrEmpty(item["Duration"].N) ? int.Parse(item["Duration"].N) : 0,
                            Rating = item.ContainsKey("Rating") && !string.IsNullOrEmpty(item["Rating"].N) ? int.Parse(item["Rating"].N) : 0
                        };

                        result.Add(movie);
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine("Error Getting Movies: " + err);
                    }
                }

                request.ExclusiveStartKey = response.LastEvaluatedKey;

            } while (response.LastEvaluatedKey.Count > 0);

            return result;
        }
    }
}