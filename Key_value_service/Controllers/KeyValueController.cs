using Key_value_service.Data;
using Key_value_service.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;

namespace Key_value_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyValueController : ControllerBase
    {
        private readonly KeyValDbContext _keyValDbContext;
        public KeyValueController(KeyValDbContext keyValDbContext) 
        {
            _keyValDbContext= keyValDbContext;
        }

        

        [HttpGet]

        public async  Task<IActionResult> GetAll()
        {
            var keyval = await _keyValDbContext.keyValues.ToListAsync();
            return Ok(keyval); //return all the values available in database
        }

        [HttpGet ("{Key}")]
        public async Task<IActionResult> GetByKey(string Key)
        {
            var keyval = await _keyValDbContext.keyValues.FirstOrDefaultAsync(x => x.Key == Key);
            if (keyval == null)
            {
                return NotFound();
            }
            return Ok(keyval); // return the keyvalue if exist
        }

        [HttpPost]
        public async Task<IActionResult> CreateByKeyVal(KeyValue keyval) //Creation of new key and value
        {
            if (_keyValDbContext.keyValues.Contains(keyval) == true)
            {
                return Conflict();
            }
            await _keyValDbContext.keyValues.AddAsync(keyval);
            await _keyValDbContext.SaveChangesAsync();
            return Ok(keyval); 
            
        }

        [HttpPut]
        public async Task<IActionResult> PutByKeyVal(KeyValue keyval) //Updating
        {
            var keyvalinList = await _keyValDbContext.keyValues.FindAsync(keyval.Key); //Finding the value of key
            if (keyvalinList == null)
            {
                return NotFound();
            }
            keyvalinList.Key = keyval.Key;
            /*if (keyvalinList.Key.Contains(keyval.Key) == true) 
            {
                return Conflict();
            }*/
            keyvalinList.Value = keyval.Value;
            await _keyValDbContext.SaveChangesAsync();

            return Ok(keyvalinList);
        }

        [HttpPatch("{Key}")]
        public async Task<IActionResult> PatchByKeyVal(string key, KeyValue keyval) //Updating value based on key
        {
            var keyvalinList = await _keyValDbContext.keyValues.FindAsync(keyval.Key);
            if (keyval == null)
            {
                return NotFound();
            }
            keyvalinList.Value = keyval.Value;
            await _keyValDbContext.SaveChangesAsync();
            return Ok(keyvalinList);    
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByKeyVal(string key) //Deleting the key
        {
            var keyval = await _keyValDbContext.keyValues.FindAsync(key); 
            if(keyval == null)
            {
                return NotFound();
            }
            _keyValDbContext.keyValues.Remove(keyval);
            await _keyValDbContext.SaveChangesAsync();
            return Ok(keyval);
        }


    }  
}
