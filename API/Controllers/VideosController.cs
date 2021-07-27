using System.Threading.Tasks;
using Application.DTOs;
using Application.Videos;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VideosController:BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult> ListarTodosVideos()
        {
            return HandleResult(await Mediator.Send(new Listar.Query()));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> ExibeVideoComId(int id)
        {
            return HandleResult(await Mediator.Send(new Detalhes.Query{Id = id}));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveVideoComId(int id)
        {
            var result = await Mediator.Send(new Remover.Command {Id = id});
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarNovoVideo([FromBody]Video video)
        {
            var command = new Adicionar.Command
            {
                Descricao = video.Descricao,
                Titulo = video.Titulo,
                Url = video.Url
            };
            var result = await Mediator.Send(command);
            return HandleResult(result,$"/api/Videos/{result.Value.Id}");
        }

        [HttpPut]
        public async Task<ActionResult> EditarVideo([FromBody] Video video)
        {
            var command = new Editar.Command
            {
                Id = video.Id,
                Descricao = video.Descricao,
                Titulo = video.Titulo,
                Url = video.Url
            };
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }
    }
}