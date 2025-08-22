using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimeAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // Define your endpoints here, e.g., Get, Post, Put, Delete for Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(Users user)
        {
            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.User_Id }, user);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users user)
        {
            if (id != user.User_Id)
            {
                return BadRequest();
            }
            await _userRepository.UpdateAsync(user);
            return NoContent();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
            return NoContent();
        }       
    }
}
