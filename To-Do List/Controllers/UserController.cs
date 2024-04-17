using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.DBContext;
using To_Do_List.Entities;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(user => new UserGetDto
                {
                    Id_user = user.Id_user,
                    Name = user.Name,
                    Email = user.Email,
                    Address = user.Address,
                })
                .ToList();

            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound($"Usuario ID {id} no encontrado");
            }
            var userDto = new UserGetDto
            {
                Id_user = user.Id_user,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
            };
            return Ok(userDto);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto.Name == "string" || userDto.Email == "string" || userDto.Address == "string")
            {
                return BadRequest("Complete todos los campos");
            }
            try
            {
                var user = new User()
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Address = userDto.Address
                };

                _context.Users.Add(user);
                _context.SaveChanges();


                return Ok($"Usuario ID {user.Id_user} creado con exito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] UserDto userDto)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound($"Usuario ID {id} no encontrado");
            }
            else if (userDto.Name == "string" || userDto.Email == "string" || userDto.Address == "string")
            {
                return BadRequest("Complete todos los campos");
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Address = userDto.Address;

            _context.SaveChanges();
            return Ok($"Usuario ID {id} actualizado correctamente");
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound($"Usuario ID {id} no encontrado");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok($"Usuario ID {id} borrado con existo");
        }
    }
}
