using IKARUSWEB.UI.Models;
using IKARUSWEB.UI.Models.TenantViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IKARUSWEB.UI.Controllers
{
    public class TenantController : Controller
    {
        private readonly HttpClient _client;

        public TenantController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("ApiClient");
        }

        // GET: /Tenant/Create
        [HttpGet]
        public IActionResult Create()
            => View(new CreateTenantViewModel());

        // POST: /Tenant/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var response = await _client.PostAsJsonAsync("api/tenant", new
            {
                vm.Code,
                vm.Name,
                vm.Address,
                vm.PhoneNumber,
                vm.Email,
                vm.CreatedUser
            });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "API isteği başarısız oldu.");
                return View(vm);
            }

            var id = await response.Content.ReadFromJsonAsync<Guid>();
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Tenant/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var tenant = await _client.GetFromJsonAsync<TenantViewModel>($"api/tenant/{id}");
            if (tenant is null) return NotFound();
            return View(tenant);
        }
    }
}
