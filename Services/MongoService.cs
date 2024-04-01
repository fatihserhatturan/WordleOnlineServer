using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using WordleOnlineServer.Models.MongoModels;
using WordleOnlineServer.Models.MsSqlModels;
using Microsoft.Identity.Client;
using WordleOnlineServer.Models.Dtos;

namespace WordleOnlineServer.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<FourLetterLobby> _fourletterCollection;
        private readonly IMongoCollection<FiveLetterLobby> _fiveletterCollection;
        private readonly IMongoCollection<SixLetterLobby> _sixletterCollection;
        private readonly IMongoCollection<SevenLetterLobby> _sevenletterCollection;
        private readonly IMongoCollection<EnableUsers> _enableUserCollection;
        private readonly IMongoCollection<Match> _matchCollection;
        private readonly IMongoCollection<MatchRequest> _matchRequestCollection;
        private readonly IMongoCollection<User> _userCollection;
        public MongoService(string connectionString, string databaseName,string fourletterCollection, string fiveletterCollection, string sixletterCollection, string sevenletterCollection, string enableUserCollection, string matchCollection, string matchRequestCollection, string userCollection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            _fourletterCollection = db.GetCollection<FourLetterLobby>(fourletterCollection);
            _fiveletterCollection = db.GetCollection<FiveLetterLobby>(fiveletterCollection);
            _sixletterCollection = db.GetCollection<SixLetterLobby>(sixletterCollection);
            _sevenletterCollection = db.GetCollection<SevenLetterLobby>(sevenletterCollection);
            _enableUserCollection = db.GetCollection<EnableUsers>(enableUserCollection);
            _matchCollection = db.GetCollection<Match>(matchCollection);
            _matchRequestCollection = db.GetCollection<MatchRequest>(matchRequestCollection);
            _userCollection = db.GetCollection<User>(userCollection);
        }
        public string GetIdentifier()
        {
            Guid uniqueId = Guid.NewGuid();

            string uniqueIdString = uniqueId.ToString();

            return uniqueIdString;
        }
        public async Task AttendLetterLobby(AppUser user ,int lettercount)
        {
          

            if (lettercount == 4)
            {
                FourLetterLobby fourLetterLobby = new FourLetterLobby
                {
                    User = user
                };
                await _fourletterCollection.InsertOneAsync(fourLetterLobby);
            }

            if (lettercount == 5)
            {
                FiveLetterLobby fiveLetterLobby = new FiveLetterLobby
                {
                    User = user
                };
                await _fiveletterCollection.InsertOneAsync(fiveLetterLobby);
            }

            if (lettercount == 6)
            {
                SixLetterLobby sixLetterLobby = new SixLetterLobby
                {
                    User = user
                };
                await _sixletterCollection.InsertOneAsync(sixLetterLobby);
            }
            if (lettercount == 7)
            {
                SevenLetterLobby sevenLetterLobby = new SevenLetterLobby
                {
                    User = user
                };
                await _sevenletterCollection.InsertOneAsync(sevenLetterLobby);
            }


            
        }
        public async Task LeaveLetterLobby(AppUser user)
        {
            var username = user.UserName;

            var filter = Builders<FourLetterLobby>.Filter.Eq(x => x.User.UserName, username);
            var filter2 = Builders<FiveLetterLobby>.Filter.Eq(x => x.User.UserName, username);
            var filter3 = Builders<SixLetterLobby>.Filter.Eq(x => x.User.UserName, username);
            var filter4 = Builders<SevenLetterLobby>.Filter.Eq(x => x.User.UserName, username);

             await _fourletterCollection.DeleteManyAsync(filter);
             await _fiveletterCollection.DeleteManyAsync(filter2);
             await _sixletterCollection.DeleteManyAsync(filter3);
             await _sevenletterCollection.DeleteManyAsync(filter4);
          
        }
        
        public List<FourLetterLobby> GetFourLobbyMember()
        {
                return _fourletterCollection.Find(_=> true).ToList();
        }
        public List<FiveLetterLobby> GetFiveLobbyMember()
        {
            return _fiveletterCollection.Find(_ => true).ToList();
        }
        public List<SixLetterLobby> GetSixLobbyMember()
        {
            return _sixletterCollection.Find(_ => true).ToList();
        }
        public List<SevenLetterLobby> GetSevenLobbyMember()
        {
            return _sevenletterCollection.Find(_ => true).ToList();
        }

        public async Task SendMatchRequest(AppUser sender,AppUser receiver,int letterCount)
        {
            MatchRequest request = new MatchRequest
            {
                UserSender = sender,
                UserReceiver = receiver,
                Status = false,
                LetterCount = letterCount
                
            };

            await _matchRequestCollection.InsertOneAsync(request);
        }

        public async Task<AppUser> ReceiveMatchRequest(AppUser user)
        {
            var filter = Builders<MatchRequest>.Filter.Eq(x => x.UserReceiver.Id, user.Id)
             & Builders<MatchRequest>.Filter.Eq(x => x.Status, false);

            var matchRequests = await _matchRequestCollection.FindAsync(filter);
            var matchRequest = await matchRequests.FirstOrDefaultAsync();

            if (matchRequest != null)
            {
                return matchRequest.UserSender;
            }
            else
            {
                return null;
            }
        }
        public async Task<AcceptMatchReturnDTO> AcceptMatchRequest(AppUser user)
        {
            var filter = Builders<MatchRequest>.Filter
                .Where(x => x.UserReceiver.Id == user.Id && !x.Status);

            var matchRequest = await _matchRequestCollection.Find(filter).FirstOrDefaultAsync();

            if (matchRequest == null)
            {
                return null;
            }

            var update = Builders<MatchRequest>.Update.Set(x => x.Status, true);
            await _matchRequestCollection.UpdateOneAsync(filter, update);

            var identifier = await CreateMatch(matchRequest);
            var sender = matchRequest.UserSender.UserName;
            var receiver = matchRequest.UserReceiver.UserName;

            return new AcceptMatchReturnDTO(identifier, sender, receiver);
        }


        public async Task<string> CreateMatch(MatchRequest request)
        {

            var match = new Match
            {
                UserSender = request.UserSender,
                UserReceiver = request.UserReceiver,
                LetterCount = request.LetterCount,
                MatchIdentifier = GetIdentifier(),
                Status = true
            };

            await _matchCollection.InsertOneAsync(match);

            return match.MatchIdentifier;
        }

        public async Task<bool> GetMatchStatusforSender(AppUser user)
        {
            var filter = Builders<Match>.Filter
                .Where(x => x.UserSender == user && x.Status == true);

            var match = await _matchCollection.Find(filter).FirstOrDefaultAsync();

            if (match == null) { return false; }
            if(match !=null) { return true; }

            return false;
        }
    }
}
