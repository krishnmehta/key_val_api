using Key_value_service.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection.Metadata;

namespace Key_value_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyValueController : ControllerBase
    {
        public static List<KeyValue> keyValues = new()
        {
            new KeyValue{Key = "Krishn", Value= "value1" },
            new KeyValue{Key = "John", Value = "value2"},
            new KeyValue{Key = "Tom", Value = "value3"}
        };

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(keyValues);
        }

        [HttpGet ("{Key}")]
        public IActionResult GetByKey(string key)
        {
            var keyval = keyValues.Find(x => x.Key == key);
            if (keyval == null)
            {
                return NotFound();
            }
            return Ok(keyval); // return the keyvalue if exist
        }

        [HttpPost]
        public IActionResult CreateByKeyVal(KeyValue keyval)
        {
            if (keyValues.Contains(keyval) == true)
            {
                return Conflict();
            }
            keyValues.Add(keyval);
            return Ok(keyValues); // return list
        }

        [HttpPut]
        public IActionResult PutByKeyVal(KeyValue keyval)
        {
            var keyvalinList = keyValues.Find(x => x.Key == keyval.Key);//Finding the value of key
            if (keyval == null)
            {
                return NotFound();
            }
            keyvalinList.Key = keyval.Key;
            if(keyvalinList.Key.Contains(keyval.Key) == true) 
            {
                return Conflict();
            }
            keyvalinList.Value = keyval.Value;
            return Ok(keyvalinList);
        }

        [HttpPatch("{Key}")]
        public IActionResult PatchByKeyVal(string key, KeyValue keyval)
        {
            var keyvalinList = keyValues.Find(x => x.Key == keyval.Key);
            if (keyval == null)
            {
                return NotFound();
            }
            keyvalinList.Value = keyval.Value;
            return Ok(keyvalinList);    
        }

        [HttpDelete]
        public IActionResult DeleteByKeyVal(string key)
        {
            var keyval = keyValues.Find(x => x.Key == key);
            if(keyval == null)
            {
                return NotFound();
            }
            keyValues.Remove(keyval);
            return Ok(keyValues);
        }


    }  
}
