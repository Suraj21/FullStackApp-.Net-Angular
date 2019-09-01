using AutoMapper;
using FriendBookApp.API.Data.Interfaces;
using FriendBookApp.API.Data.Repository;
using FriendBookApp.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendBookApp.API.Controllers
{
    //[Authorize]
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
    }
}
