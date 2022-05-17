using AutoMapper;
using MeuDinheiro.AppMvc.Extensions;
using MeuDinheiro.AppMvc.ViewModels;
using MeuDinheiro.Bussiness.Core.Notifications;
using MeuDinheiro.Bussiness.Models.Categorias;
using MeuDinheiro.Bussiness.Models.Categorias.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MeuDinheiro.AppMvc.Controllers
{
    [Authorize]
    [RoutePrefix("categorias")]
    public class CategoriasController : BaseController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository, 
                                    ICategoriaService categoriaService, 
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ClaimsAuthorize("Categoria", "R")]
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
        [Route("listar")]
        [ClaimsAuthorize("Categoria", "R")]
        public async Task<ActionResult> ListarCategorias()
        {
            return PartialView("_ListaCategorias",_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos()));
        }

        [HttpGet]
        [Route("cadastrar")]
        [ClaimsAuthorize("Categoria","C")]
        public ActionResult Cadastrar()
        {
            return PartialView("_Cadastrar");
        }

        [HttpPost]
        [Route("cadastrar")]
        [ClaimsAuthorize("Categoria", "C")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(CategoriaViewModel model)
        {
            if (!ModelState.IsValid) return RetornarErros();
            
            await _categoriaService.Adicionar(_mapper.Map<Categoria>(model));

            if (!ValidarOperacao()) return RetornarErros();

            return Json(new { status = "success", message="Categoria cadastrada com sucesso!" });  
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ClaimsAuthorize("Categoria", "R")]
        public async Task<ActionResult> Detalhes(Guid id)
        {
            var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorId(id));

            if (categoria is null) return HttpNotFound();

            return View(categoria);
        }

        [HttpGet]
        [Route("editar/{id:guid}")]
        [ClaimsAuthorize("Categoria", "U")]
        public async Task<ActionResult> Editar(Guid id)
        {
            var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorId(id));

            if (categoria is null) return new HttpStatusCodeResult(404, "Recurso não Encontrado");

            return PartialView("_Editar", categoria);
        }

        [HttpPost]
        [Route("editar/{id:guid}")]
        [ClaimsAuthorize("Categoria", "U")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Guid id, CategoriaViewModel model)
        {
            if (id != model.Id)
            {
                AdicionarErro("Categoria inexistente");
                return RetornarErros();
            }

            if (!ModelState.IsValid) return RetornarErros();

            await _categoriaService.Atualizar(_mapper.Map<Categoria>(model));

            if (!ValidarOperacao()) return RetornarErros();

            return Json(new { status = "success", message = "Categoria atualizada com sucesso!" });
        }


        [HttpPost]
        [Route("excluir/{id:guid}")]
        [ClaimsAuthorize("Categoria", "D")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Guid id)
        {
            var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterCategoriaLancamentos(id));

            if (categoria is null)
            {
                AdicionarErro("Categoria inexistente");
                return RetornarErros();
            }

            await _categoriaService.Remover(id);

            if (!ValidarOperacao()) return RetornarErros();

            return Json(new { status = "success", message = "Categoria desativada com sucesso!" });
        }

        protected override void Dispose(bool disposing)
        {   
            if(disposing)
            {
                _categoriaRepository?.Dispose();
                _categoriaService?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}