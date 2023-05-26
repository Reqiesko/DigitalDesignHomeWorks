using DigitalDesignDll;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDigitalDesign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordWorkerController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Dictionary<string, int>>> Get([FromBody] string text)
        {
            var words = WordWorker.GetWordsCountParallel(text);

            return Ok(WordWorker.SortDictionary(words));
        }
    }
}
