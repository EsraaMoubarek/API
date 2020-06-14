using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testwebapicore.Models.repo;
using testwebapicore.Models;
namespace testwebapicore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstructionsController : ControllerBase
    {
        InstructionsRepo _db;

        public InstructionsController(InstructionsRepo db)
        {
            _db = db;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadInstructionImage()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string str = guid.ToString();

                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition).FileName.Trim('"').ToString();

                    fileName = Path.GetFileNameWithoutExtension(fileName) + str
                        + Path.GetExtension(fileName);

                    var fullPath = Path.Combine(pathToSave, fileName);

                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(_db.UploadInstructionImage(fileName));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public ActionResult AddInstructionDetails([FromBody] Instructions instruction) {

            _db.AddInstructionDetails(instruction);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetInstructions()
        {
            try
            {
                return Ok(_db.GetInstructions());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }
        [HttpPut]
        public IActionResult EditInstruction(Instructions instruction)
        {
            try
            {
                return Ok(_db.EditInstruction(instruction));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteInstruction(int id)
        {
            try
            {
                return Ok(_db.DeleteInstruction(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


        }
    }
}