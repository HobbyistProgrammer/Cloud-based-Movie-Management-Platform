using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using comp306_lab3.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace comp306_lab3.Services
{
    public class SecurityService
    {
        private List<UserModel> knownUsers = new List<UserModel>();
        private string connectionString, dbUser, dbPassword;
        private string secretKey = "9wzCThzROHRLoZSA2wAslgppAMeCptFvWL8gRniO", accesskey = "AKIA47CRZWDC57V7WDHC";

        public SecurityService()
        {
            Console.WriteLine("Initializing Security Service");
            LoadCredentialsFromParameterStore().Wait();

            connectionString = $"Server=database-1.cjgi2scqcqpn.us-east-1.rds.amazonaws.com;Database=lab3database;User Id={dbUser};Password={dbPassword};ConnectRetryCount=0;";

            RetrieveUsersFromDatabase();
        }

        public async Task<int> CountMoviesAsync()
        {
            string secretKey = "9wzCThzROHRLoZSA2wAslgppAMeCptFvWL8gRniO";
            string accessKey = "AKIA47CRZWDC57V7WDHC";
            var awscreds = new BasicAWSCredentials(accessKey, secretKey);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awscreds, Amazon.RegionEndpoint.USEast1);

            var request = new ScanRequest
            {
                TableName = "Movies"
            };

            var result = new List<MovieModel>();
            ScanResponse response;
            int movieCount = 0;

            do
            {
                response = await client.ScanAsync(request);

                foreach (var item in response.Items)
                {
                    movieCount++;
                }

                // Set the ExclusiveStartKey for the next scan request if more data is available
                request.ExclusiveStartKey = response.LastEvaluatedKey;

            } while (response.LastEvaluatedKey.Count > 0); // Continue if there are more items

            return movieCount;
        }

        public async Task EditMovie(MovieModel movie)
        {
            var tableName = "Movies";
            var request = new UpdateItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue {N = movie.Id.ToString()} }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    {"Title", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {S = movie.Title } } },
                    {"Genre", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {S = movie.Genre } } },
                    {"Director", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {S = movie.Director } } },
                    {"Duration", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {N = movie.Duration.ToString() } } },
                    {"Rating", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {N = movie.Rating.ToString() } } },
                    {"Release_Date", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {S = movie.Release_Date } } },
                    {"User_Uploaded", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {N = movie.User_Uploaded.ToString() } } },

                }
            };

            string secretKey = "9wzCThzROHRLoZSA2wAslgppAMeCptFvWL8gRniO";
            string accessKey = "AKIA47CRZWDC57V7WDHC";
            var awscreds = new BasicAWSCredentials(accessKey, secretKey);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awscreds, Amazon.RegionEndpoint.USEast1);

            await client.UpdateItemAsync(request);
        }

        public async Task DeleteMovie(int id)
        {
            try
            {
                var request = new DeleteItemRequest
                {
                    TableName = "Movies",
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { N = id.ToString() } }
                    }
                };

                var awsCreds = new BasicAWSCredentials(accesskey, secretKey);
                var client = new AmazonDynamoDBClient(awsCreds, RegionEndpoint.USEast1);

                await client.DeleteItemAsync(request);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error inserting user: " + err);
            }
        }

        public async Task SaveUpdatedComment(CommentModel comment)
        {
            var tableName = "Comments";
            var request = new UpdateItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue {S = comment.Id.ToString()} }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    {"Comment", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {S = comment.Comment } } },
                    {"Comment_Time", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {S = comment.Comment_Time } } },
                    {"Comment_User", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {N = comment.Comment_User.ToString() } } },
                    {"Movie_Id", new AttributeValueUpdate { Action = "PUT", Value = new AttributeValue {N = comment.Movie_Id.ToString() } } },
                }
            };

            string secretKey = "9wzCThzROHRLoZSA2wAslgppAMeCptFvWL8gRniO";
            string accessKey = "AKIA47CRZWDC57V7WDHC";
            var awscreds = new BasicAWSCredentials(accessKey, secretKey);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awscreds, Amazon.RegionEndpoint.USEast1);

            await client.UpdateItemAsync(request);
        }
        public async Task AddMovie(MovieModel movie, int userId, IFormFile file)
        {
            try
            {
                int movieCount = await CountMoviesAsync();
                movieCount = movieCount + 1;
                Console.WriteLine("Counting movie: " + movieCount);

                var item = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { N = movieCount.ToString() } },
                    { "Title", new AttributeValue { S = movie.Title } },
                    { "Genre", new AttributeValue { S = movie.Genre } },
                    { "User_Uploaded", new AttributeValue { N = userId.ToString() } },
                    { "Director", new AttributeValue { S = movie.Director } },
                    { "Duration", new AttributeValue { N = movie.Duration.ToString() } },
                    { "Release_Date", new AttributeValue { S = movie.Release_Date } },
                    { "Rating", new AttributeValue { N = movie.Rating.ToString() } },
                    { "File", new AttributeValue { S = file.FileName.ToString() } },
                };

                var request = new PutItemRequest
                {
                    TableName = "Movies",
                    Item = item
                };


                var awsCreds = new BasicAWSCredentials(accesskey, secretKey);
                var client = new AmazonDynamoDBClient(awsCreds, RegionEndpoint.USEast1);

                await client.PutItemAsync(request);
            } catch (Exception err)
            {
                Console.WriteLine("Error adding movie: " + err);
            }
        }

        public async Task SaveComment(CommentModel model)
        {
            var awsCreds = new BasicAWSCredentials(accesskey, secretKey);
            var client = new AmazonDynamoDBClient(awsCreds, RegionEndpoint.USEast1);

            var request = new PutItemRequest
            {
                TableName = "Comments",
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Id", new AttributeValue { S = model.Id.ToString()} },
                    {"Movie_Id", new AttributeValue { N = model.Movie_Id.ToString()} },
                    {"Comment", new AttributeValue { S = model.Comment} },
                    {"Comment_User", new AttributeValue { N = model.Comment_User.ToString()} },
                    {"Comment_Time", new AttributeValue { S = model.Comment_Time} },
                }
            };

            await client.PutItemAsync(request);
        }

        private async Task LoadCredentialsFromParameterStore()
        {
            var awsCreds = new BasicAWSCredentials(accesskey, secretKey);
            var client = new AmazonSimpleSystemsManagementClient(awsCreds, RegionEndpoint.USEast1);

            var dbUserRequest = new GetParameterRequest
            {
                Name = "/Comp306/DbUser",
                WithDecryption = true
            };

            var dbPasswordRequest = new GetParameterRequest
            {
                Name = "/Comp306/DbPassword",
                WithDecryption = true
            };

            var dbAccessRequest = new GetParameterRequest
            {
                Name = "/Comp306/AccessKey",
                WithDecryption = true
            };

            var dbSecretRequest = new GetParameterRequest
            {
                Name = "/Comp306/SecretKey",
                WithDecryption = true
            };

            var dbUserResponse = await client.GetParameterAsync(dbUserRequest);
            var dbPasswordResponse = await client.GetParameterAsync(dbPasswordRequest);
            var dbSecretResponse = await client.GetParameterAsync(dbSecretRequest);
            var dbAccessResponse = await client.GetParameterAsync(dbAccessRequest);

            dbUser = dbUserResponse.Parameter.Value;
            dbPassword = dbPasswordResponse.Parameter.Value;
            secretKey = dbSecretResponse.Parameter.Value;
            accesskey = dbAccessResponse.Parameter.Value;
        }

        public bool InsertNewUser(AccountRegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return false;
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string stringQuery = "insert into Users (Id,Username, Password) values(@Id, @Username, @Password)";

                        using (SqlCommand cmd = new SqlCommand(stringQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", knownUsers.Count() + 1);
                            cmd.Parameters.AddWithValue("@Username", model.Username);
                            cmd.Parameters.AddWithValue("@Password", model.Password);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("User Successfully added");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("User insertion failed");
                                return false;
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("Error inserting user: " + err);
                }
            }
            return false;
        }

        private void RetrieveUsersFromDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM USERS", conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                Id = int.Parse(reader.GetString(0)),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2)
                            };
                            knownUsers.Add(user);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Error getting Users: " + err);
            }
        }
        public UserModel isValid(UserModel user)
        {
            foreach(UserModel model in knownUsers)
            {
                if(model.Username == user.Username && model.Password == user.Password)
                {
                    return model;
                }
            }
            return null;
        }
    }
}
