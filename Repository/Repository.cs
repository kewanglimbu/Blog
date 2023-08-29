using BlogWebApp.Data;
using BlogWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApp.Repository
{
    public class Repository : IRepository
    {
        private readonly AppsDbContext _db;

        public Repository(AppsDbContext db)
        {
            _db = db;
            
        }
        public void AddPost(Post post)
        {
            _db.Posts.Add(post);
            //await _db.SaveChangesAsync(); if you have two ,error occur ?

        }

        public List<Post> GetAllPosts()
        {
           return _db.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return  _db.Posts.FirstOrDefault(post => post.Id == id);

           //var result= _db.Posts.Find(id);
            //return result;
        }

        public void DeletePost(int id)
        {
           _db.Posts.Remove(GetPost(id));

           
        }

       

        public void UpdatePost(Post post)
        {
           // _db.Posts.Update(GetPost(id));
            _db.Posts.Update(post);
        }


       public async Task<bool> SaveChangesAsync()
       
        {
            if(await _db.SaveChangesAsync()>0)
            {
                return true;           
            }

            return false;
        }
    }
}
