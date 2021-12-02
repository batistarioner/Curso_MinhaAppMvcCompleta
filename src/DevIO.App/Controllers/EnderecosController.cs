using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevIO.App.ViewModels;
using AutoMapper;
using DevIO.Business.Models;
using DevIO.Business.Interface;

namespace DevIO.App.Controllers
{
    public class EnderecosController : BaseController
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public EnderecosController(IEnderecoRepository enderecoRepository,
                                   IMapper mapper,
                                   INotificador notificador) : base(notificador)
        {
            _enderecoRepository = enderecoRepository;
            _mapper             = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<EnderecoViewModel>>(await _enderecoRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var enderecoViewModel = await ObterPorId(id);
            if (enderecoViewModel == null)
            {
                return NotFound();
            }

            return View(enderecoViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnderecoViewModel enderecoViewModel)
        {
            if (!ModelState.IsValid){ return View(enderecoViewModel); }

            var Endereco = _mapper.Map<Endereco>(enderecoViewModel);
            await _enderecoRepository.Adicionar(Endereco);
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var enderecoViewModel = await ObterPorId(id);
            if (enderecoViewModel == null)
            {
                return NotFound();
            }
            return View(enderecoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id){return NotFound();}

            if (!ModelState.IsValid) { return View(enderecoViewModel); }

            var Endereco = _mapper.Map<Endereco>(enderecoViewModel);

            await _enderecoRepository.Atualizar(Endereco);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var enderecoViewModel = await ObterPorId(id);
            if (enderecoViewModel == null)
            {
                return NotFound();
            }
            return View(enderecoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var enderecoViewModel = await ObterPorId(id);
            if (enderecoViewModel == null) { return NotFound();}

            await _enderecoRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<EnderecoViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
        }
    }
}
