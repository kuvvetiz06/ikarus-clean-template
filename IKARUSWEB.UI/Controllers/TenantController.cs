﻿using IKARUSWEB.UI.Models;
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

        // GET: /Tenant
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _client
                .GetFromJsonAsync<IEnumerable<TenantViewModel>>("api/tenant");
            return View(list);
        }
        // GET: /Tenant/Create
        [HttpGet]
        public IActionResult Create()
            => View(new CreateTenantViewModel());

        // POST: /Tenant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: /Tenant/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // 1) get DTO
            var dto = await _client.GetFromJsonAsync<TenantViewModel>($"api/tenant/{id}");
            if (dto is null) return NotFound();

            // 2) map to UpdateVm
            var vm = new UpdateTenantViewModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Name = dto.Name,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                ModifiedUser =  "HÜSEYİN GÖKDEMİR"
            };
            return View(vm);
        }

        // POST: /Tenant/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateTenantViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var response = await _client.PutAsJsonAsync($"api/tenant/{id}", vm);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Güncelleme sırasında sunucu hatası alındı.");
                return View(vm);
            }

            return RedirectToAction("Details", new { id });
        }

        // DELETE işleyen POST aksiyonu
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Burada gerçek kullanıcı adını alın
            var modifiedUser = User.Identity?.Name ?? "system";

            var response = await _client.DeleteAsync($"api/tenant/{id}?modifiedUser={modifiedUser}");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Silme işlemi başarısız oldu.");
                // Listeyi yeniden yükleyelim
                var list = await _client.GetFromJsonAsync<IEnumerable<TenantViewModel>>("api/tenant");
                return View("Index", list);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
