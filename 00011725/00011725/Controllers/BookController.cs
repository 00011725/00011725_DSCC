using _00011725.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace _00011725.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;
        public BookController(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }
        string BaseUrl = "";
        public async Task<ActionResult> Index()
        {
            
            List<Book> BookInfo = new List<Book>();    
            using (var reader = new HttpClient())
            {
                reader.BaseAddress = new Uri(BaseUrl);
                reader.DefaultRequestHeaders.Clear();
                reader.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await reader.GetAsync("api/Book");

                if (Res.IsSuccessStatusCode)
                {
                    var BResponce = Res.Content.ReadAsStringAsync().Result;

                    BookInfo = JsonConvert.DeserializeObject<List<Book>>(BResponce);
                }
                return View(BookInfo);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var book = new Book();

            using var reader = new HttpClient();
            reader.BaseAddress = new Uri(BaseUrl);
            reader.DefaultRequestHeaders.Clear();
            reader.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await reader.GetAsync($"api/Book/GetBook/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                book = JsonConvert.DeserializeObject<Book>(responseContent);
            }

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book viewModel)
        {
            var book = new Book
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Description= viewModel.Description,
                Author = viewModel.Author,
                Type = viewModel.Type
              
            };

            using var reader = new HttpClient();
            reader.BaseAddress = new Uri(BaseUrl);
            reader.DefaultRequestHeaders.Clear();
            reader.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var bookJson = JsonConvert.SerializeObject(book);
            var content = new StringContent(bookJson, Encoding.UTF8, "application/json");

            var response = await reader.PostAsync("api/Book/CreateBook", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the book list
            }
            else
            {
                // Handle the error
                return View("Error");
            }

            return View(viewModel);
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Book viewModel)
        {
            var book = new Book
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Description = viewModel.Description,
                Author = viewModel.Author,
                Type = viewModel.Type
            };

            using var reader = new HttpClient();
            reader.BaseAddress = new Uri(BaseUrl);
            reader.DefaultRequestHeaders.Clear();
            reader.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize the modified book object to JSON and send it in the request body
            var bookJson = JsonConvert.SerializeObject(book);
            var content = new StringContent(bookJson, Encoding.UTF8, "application/json");

            var response = await reader.PutAsync($"api/Book/UpdateBook/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the book list 
            }

            // Handle the case where the update failed
            return View(book);
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            using var reader = new HttpClient();
            reader.BaseAddress = new Uri(BaseUrl);
            reader.DefaultRequestHeaders.Clear();
            reader.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await reader.DeleteAsync($"api/Book/DeleteBook/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the book list 
            }
            else
            {
                return View("Error");
            }
        }

    }
}
