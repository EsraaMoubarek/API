using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testwebapicore.Models.repo
{
    public class FeedbackRepo
    {
        WasteAppDbContext db;
        public FeedbackRepo(WasteAppDbContext db)
        {
            this.db = db;
        }

        public List<object> GetFeedbacks()
        {
            return db.Feedback.Select(a=>new  { Id=a.Id,CategoryId=a.CategoryId,
                FeedbackContent=a.FeedbackContent,ClientId=a.ClientId,Date=a.Date,Cat=a.Category.Category,Clnt=a.Client.FirstName +" "+ a.Client.LastName}).ToList<object>();
        }

        public void AddFeedback( Feedback f){
            db.Feedback.Add(f);
            db.SaveChanges();
            
        }
    }
}
