using medicalcare_mongodb.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace medicalcare_mongodb.controllers
{
    [ApiController]
    [Route("Medicalcare/api/[controller]")]    
    public class AccountsController: BaseController
    {
        private readonly IConfiguration _config;
        public AccountsController(MedicalcareDbContext context, IConfiguration config) : base(context)
        {
            this._config = config;
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
                phone = account?.phone,
                gender = account?.gender,
                dob = account?.dob,
                address = account?.address,
                role = account?.role,
                account_type = account?.account_type,
                hospital_id = (account?.hospital_id_guid == null) ? null : new ObjectId(account?.hospital_id_guid),
                create_date = DateTime.Today
            };

            await Task.Run(() =>
            {
                this.context.m_account.Add(newAccount);
                this.context.SaveChanges();
            });
            return Ok(new { message = "User registered successfully." });
        }        

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Account dataInput)
        {
            dataInput.id = new ObjectId(id);
            dataInput.hospital_id = (string.IsNullOrEmpty(dataInput.hospital_id_guid)) ?
                null: new ObjectId(dataInput.hospital_id_guid);
            // Validate the incoming model.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Account? item = await this.context.m_account.FirstOrDefaultAsync(
                    m => m.id == dataInput.id);
            if (item == null)
            {
                return NotFound(new { message = "Account not found." });
            }
            else
            {
                dataInput.create_date = (item.create_date == DateTime.MinValue) ? DateTime.Today : item.create_date;
                await Task.Run(() =>
                {
                    this.context.m_account.Entry(item).CurrentValues.SetValues(dataInput);
                    this.context.SaveChanges();
                });

                return Ok(item);
            }
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
            Account? data = await this.context.m_account.FirstOrDefaultAsync(
                    m => m.email == email &&
                    m.password == password);
            if (data == null)
                return NotFound(new { message = "Email or password is incorrect.", item = data } );
            else{
                var create_date = data.create_date;
                var role = data.role;
                var account_type = data.account_type;
                var freeExpireDay = Convert.ToInt32(this._config["FreeExpireDay"]);
                var memberExpireDay = Convert.ToInt32(this._config["MemberExpireDay"]);
                var expire = false;
                if (role != "Super Admin")
                {
                    if (account_type == "Free")
                        expire = context.ExpireDate(create_date, freeExpireDay);
                    else
                        expire = context.ExpireDate(create_date, memberExpireDay);
                }
                if (expire)
                {
                    return Conflict(new { message = "This Account is expire.", item = data } );
                }
                else
                {
                    var item = new Dictionary<string, object>
                    {
                        ["email"] = data?.email??"",
                        ["hospital_id"] = data?.hospital_id_guid??"",
                        ["first_name"] = data?.first_name??"",
                        ["last_name"] = data?.last_name??"",
                        ["account_type"] = account_type??"",
                        ["role"] = role??"",
                        ["create_date"] = create_date
                    }; 
                    return Ok(new { message = "Login success." , item = item});
                }
            }

        }   
    }
}