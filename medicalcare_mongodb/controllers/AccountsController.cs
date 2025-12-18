using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class AccountsController: ControllerBase
    {
        readonly MedicalcareDbContext context;

        public AccountsController(MedicalcareDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var data = await this.context.m_account.ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Account account)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? email = account?.email?.ToLower();
            // Check if the email already exists.
            Account? item = await this.context.m_account.FirstOrDefaultAsync(m => m.email == email);
            if (item != null)
            {
                return Conflict(new { message = "Email is already registered." });
            }

            // Create a new user entity.
            var newAccount = new Account
            {
                email = email,
                first_name = account?.first_name,
                last_name = account?.last_name,
                password = account?.password,
                account_type = account?.account_type
            };

            await Task.Run(() =>
            {
                this.context.m_account.Add(newAccount);
                this.context.SaveChanges();
            });
            return Ok(new { message = "User registered successfully." });
        }        

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(Account account)
        {
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string? email = account?.email?.ToLower();
            string? password = account?.password;
            // Check if the email already exists.
            Account? item = await this.context.m_account.FirstOrDefaultAsync(
                    m => m.email == email &&
                    m.password == password);
            if (item == null)
            {
                return NotFound(new { message = "Email or password is incorrect.", item = item } );
            }

            return Ok(new { message = "Login success." , item = item});
        }   
    }
}