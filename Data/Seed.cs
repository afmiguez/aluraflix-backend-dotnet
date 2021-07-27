using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class Seed
    {
        public static async Task SeedData(AluraFlixContext context)
        {
            if (!context.Videos.Any())
            {
                var videos = new List<Video>();
                
                for (var i = 0; i < 10; i++)
                {
                    
                    videos.Add(new Video
                    {
                        Titulo = $"titulo{i}",
                        Descricao = $"descrição{i}",
                        Url = $"http://url{i}.com"
                    });    
                      
                }

                await context.Videos.AddRangeAsync(videos);
                var result = await context.SaveChangesAsync() > 0;
                if (result)
                {
                    Console.WriteLine("OK");
                }
                else
                {
                    Console.WriteLine("Error!!!!!!");
                }


            }
        }
    }
}