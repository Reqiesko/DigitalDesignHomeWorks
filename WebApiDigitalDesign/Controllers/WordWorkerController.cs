using DigitalDesignDll;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDigitalDesign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordWorkerController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Dictionary<string, int>>> Get()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string text = await reader.ReadToEndAsync();

                var words = WordWorker.GetWordsCountParallel(text);

                return Ok(WordWorker.SortDictionary(words));
            }
        }
    }
}
