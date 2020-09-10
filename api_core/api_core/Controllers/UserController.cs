using BusinessAccessLayer.Repository;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Contact> _contactRepo;
        public UserController(
            IRepository<User> userRepo,
            IRepository<Contact> contactRepo
            )
        {
            _userRepo = userRepo;
            _contactRepo = contactRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userRepo.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {

            var user = await _userRepo.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userRepo.Create(user);

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser( int id, User user)
        {
            if (id != user.id)
            {

                return BadRequest();
            }


            if(user == null)
            {

                return NotFound();
            }
            var oldUser = await _userRepo.GetById(id);
            oldUser.firstName = user.firstName;
            oldUser.lastName = user.lastName;

            var oldUserContact = await _contactRepo.GetById(id);
            oldUserContact.phone = user.contact.phone;
            oldUserContact.email = user.contact.email;

            if (oldUser != null)
            {
                await _userRepo.Update(oldUser);

                // Delete 
                if (user.contact == null)
                    await _contactRepo.Delete(oldUserContact);

                // Update and Insert
                if (oldUser.contact != null)
                    // Update
                    await _contactRepo.Update(oldUserContact);
                else
                    // Insert
                    await _contactRepo.Create(oldUserContact);


            }




            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            
            var oldObject = await _userRepo.GetById(id);
            if (oldObject == null)
            {
                return NotFound();
            }
            await _userRepo.Delete(oldObject);
            

            return NoContent();
        }


    }
}