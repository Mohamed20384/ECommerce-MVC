using BLL.DTOs;
using BLL.Services.Absraction;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using PL.ActionResults;
using PL.ConvertIntoVM;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;

        public ProductController(IProductServices productServices,ICategoryServices categoryServices)
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index()
        {
            var prducts = await _productServices.GetAll();
            var result = prducts.FromListDTOToVM();
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.category =await _categoryServices.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductAR newProduct)
        {
            ViewBag.category = await _categoryServices.GetAll();
            if (ModelState.IsValid)
            {
                await _productServices.CreateNew(newProduct.FromARToDTO());
                return RedirectToAction("Index");

            }
           
            return View(newProduct);
        }
         public async Task<IActionResult> Edit(int id)
        {
            var dto = await _productServices.GetbyId(id);
            var AR = new ProductAR { Name = dto.Name, Description = dto.Description, Category = dto.Category, CategoryId = dto.CategoryId, ImageUrl= dto.ImageUrl, Price = dto.Price };
            return View(AR);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductAR productAR)
        {
            if (ModelState.IsValid)
            {
               await _productServices.EditProduct(id, new ProductDTO
               { Name = productAR.Name, Description = productAR.Description, Category = productAR.Category, CategoryId = productAR.CategoryId, ImageUrl = productAR.ImageUrl, Price = productAR.Price });
                return RedirectToAction("Index");
            }
            return View(productAR);
        }
        public async Task<IActionResult> Delete(int id)
        {
           await _productServices.Delete(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _productServices.GetbyId(id));
        }
    }
}
