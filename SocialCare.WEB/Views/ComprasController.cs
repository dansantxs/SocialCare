﻿using Microsoft.AspNetCore.Mvc;
using SocialCare.DATA.Models;

namespace SocialCare.WEB.Views
{
    public class ComprasController : Controller
    {
        private readonly ComprasControl oComprasControl;

        public ComprasController()
        {
            oComprasControl = ComprasControl.Instance;
        }

        public IActionResult Index()
        {
            var viewModel = oComprasControl.ObterTodasCompras();
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Pessoas = oComprasControl.ObterPessoas();
            ViewBag.Produtos = oComprasControl.ObterProdutos();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Compras compra)
        {
            oComprasControl.CriarCompra(compra);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var compra = oComprasControl.ObterCompraPorId(id);
            return View(compra);
        }

        public IActionResult Edit(int id)
        {
            var compra = oComprasControl.ObterCompraPorId(id);
            ViewBag.Pessoas = oComprasControl.ObterPessoas();
            ViewBag.Produtos = oComprasControl.ObterProdutos();
            return View(compra);
        }

        [HttpPost]
        public IActionResult Edit(Compras compra)
        {
            oComprasControl.EditarCompra(compra);
            return RedirectToAction("Details", new { id = compra.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            oComprasControl.ExcluirCompra(id);
            return RedirectToAction("Index");
        }
    }
}