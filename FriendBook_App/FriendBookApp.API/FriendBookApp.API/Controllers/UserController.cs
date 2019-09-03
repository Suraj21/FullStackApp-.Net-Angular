using AutoMapper;
using FriendBookApp.API.Data.Interfaces;
using FriendBookApp.API.Data.Repository;
using FriendBookApp.API.Dto;
using FriendBookApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FriendBookApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFriendRepository friendRepository;
        public UserController(IFriendRepository friendRepo, IMapper mapper)
        {
            _mapper = mapper;
            friendRepository = friendRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await friendRepository.GetUsers();
            var userToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(userToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await friendRepository.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            // a way to check whether the user is the current user who token is being passed
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await friendRepository.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await friendRepository.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on Save");
        }

    }
}
