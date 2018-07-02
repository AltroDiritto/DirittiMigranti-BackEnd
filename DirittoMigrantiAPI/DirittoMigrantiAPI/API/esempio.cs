using System.Collections.Generic;
using System.Linq;
using DirittoMigrantiAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DirittoMigrantiAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly UserContext _context;
        public UsersController(UserContext context) { _context = context; }

        #region GET
        [HttpGet]
        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //NOTA: il Name serve per la chiamata da qua dentro.        
        [HttpGet("getUser/{id}", Name = "GetUser")]
        //Viene chiamato da http://localhost:50156/api/users/getUser/{id}
        //oppure internamente da CreatedAtRoute("GetUser", new { id = {id} }, user);
        public IActionResult GetById(long id)
        {
          
            var item = _context.Users.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            // Controllo se l'User passato ha i campi obbligatori compilati
            if (!ModelState.IsValid)
            {
                View(user);
                return BadRequest();
            }


            // Controllo se esiste già un utente con quell'username
            if (_context.Users.Any((u) => u.Username == user.Username))
                return BadRequest();

            // Lo aggiungo
            _context.Users.Add(user);

            // Salvo
            _context.SaveChanges();

            // Invio come risposta le info dell'utente appena creato
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }
        #endregion

        #region UPDATE
        //[HttpPut("{id}")]
        //public IActionResult Update(long id, [FromBody] User item)
        //{
        //    if (item == null || item.Id != id)
        //    {
        //        return BadRequest();
        //    }

        //    var user = _context.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    user.Username = item.Username;

        //    _context.Users.Update(user);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
        #endregion

        #region DELETE
        //[HttpDelete("{id}")]
        //public IActionResult Delete(long id)
        //{
        //    var user = _context.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
        #endregion

    }

    //POSSONO SERVIRE:
    //public async Task<IActionResult> Get()
    //{
    //    var users = await _context.Users
    //        .Include(u => u.Posts)
    //        .ToArrayAsync();

    //    var response = users.Select(u => new
    //    {
    //        firstName = u.FirstName,
    //        lastName = u.LastName,
    //        posts = u.Posts.Select(p => p.Content)
    //    });

    //    return Ok(response);
    //}

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[HttpGet("{id}", Name = "GetName")]
    //public List<User> GetNameById()
    //{
    //    return _context.Users.ToList();
    //}

}