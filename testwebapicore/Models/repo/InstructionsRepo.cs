using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class InstructionsRepo
    {
        WasteAppDbContext _db;

        public InstructionsRepo(WasteAppDbContext db)
        {
            _db = db;
        }

        public Instructions UploadInstructionImage(string fileName)
        {
            Instructions instruction = new Instructions()
            {
                Image = fileName
            };
            _db.Instructions.Add(instruction);
            _db.SaveChanges();
            return instruction;
        }

        public void AddInstructionDetails(Instructions instruction) { // need try catch block
            Instructions instruction1 = _db.Instructions.Find(instruction.Id);
            instruction1.Details = instruction.Details;
            _db.SaveChanges();
        }
        public List<Instructions> GetInstructions()
        {

            return _db.Instructions.ToList();
        }

        public bool EditInstruction(Instructions instruction) {

            try
            {
                var obj = _db.Instructions.Find(instruction.Id);
                obj.Details = instruction.Details;
                _db.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool DeleteInstruction(int id) {
           string folder = @"\Resources\Images\";
            string path;
            string productFolder = Directory.GetCurrentDirectory().ToString();
            try
            {
                path = productFolder + folder;
                var obj = _db.Instructions.Find(id);
                path += obj.Image;
                File.Delete(path);
                _db.Instructions.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
