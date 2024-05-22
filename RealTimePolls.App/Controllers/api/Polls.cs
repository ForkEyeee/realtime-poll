﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RealTimePolls.Repositories;

namespace realTimePolls.Controllers
{
    public class Polls : Controller
    {
        private readonly IPollsApiRepository pollsApiRepository;

        public Polls(IPollsApiRepository pollsApiRepository)
        {
            this.pollsApiRepository = pollsApiRepository;
        }

        [HttpGet]
        [Route("api/[action]")]
        public async Task<JsonResult> GetPollsListAsync()
        {
            var polls = await pollsApiRepository.GetPollsListAsync();

            return Json(polls);
        }

        [HttpGet]
        [Route("api/[action]")]
        public async Task<JsonResult> GetDropdownListAsync()
        {
            var genreOptions = await pollsApiRepository.GetDropdownListAsync();

            return Json(genreOptions);
        }

        [HttpGet]
        [Route("api/[action]")]
        public async Task<string> GetUserProfilePicture()
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var profilePicture = await pollsApiRepository.GetUserProfilePicture(result);

            return profilePicture;
        }


        [HttpGet]
        [Route("api/[action]")]
        public async Task<JsonResult> GetSearchResults([FromQuery] string search)
        {

            var domainPolls = await pollsApiRepository.GetSearchResults(search);

            return Json(domainPolls);

        }

    }
}

//[HttpPost]
//public IActionResult GetGenreResults([FromBody] int genreId)
//{
//    try
//    {
//        var polls = _context
//            .Polls.Include(p => p.Genre)
//            .Where(p => p.GenreId == genreId)
//            .Select(p => new PollItem
//            {
//                Poll = p,
//                FirstVoteCount = _context
//                    .UserPoll.Where(up => up.PollId == p.Id && up.Vote == true)
//                    .Count(),
//                SecondVoteCount = _context
//                    .UserPoll.Where(up => up.PollId == p.Id && up.Vote == false)
//                    .Count(),
//                UserName = _context.User.SingleOrDefault(user => user.Id == p.UserId).Name,
//                ProfilePicture = _context
//                    .User.SingleOrDefault(user => user.Id == p.UserId)
//                    .ProfilePicture
//            })
//            .ToList();

//        int pollLength = _context
//            .Polls.Where(p => p.GenreId == genreId)
//            .Select(p => new PollItem { Poll = p, })
//            .Count();

//        var pollList = new PollsList { Polls = polls, PollCount = pollLength };

//        return Json(pollList);
//    }
//    catch (Exception e)
//    {
//        var errorViewModel = new ErrorViewModel { RequestId = e.Message };
//        return View("Error", errorViewModel);
//    }
//}
//}
//}

