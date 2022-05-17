using AutoMapper;
using MeuDinheiro.AppMvc.ViewModels;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using MeuDinheiro.Bussiness.Models.Categorias;
using MeuDinheiro.Bussiness.Models.Lancamentos.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using MeuDinheiro.Bussiness.Core.Notifications;
using MeuDinheiro.AppMvc.Extensions;

namespace MeuDinheiro.AppMvc.Controllers
{   
    [Authorize]
    [RoutePrefix("lancamentos")]
    public class LancamentosController : BaseController
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILancamentoService _lancamentoService;
        private readonly IMapper _mapper;

        public LancamentosController(ILancamentoRepository lancamentoRepository, 
                                     ICategoriaRepository categoriaRepository, 
                                     ILancamentoService lancamentoService, 
                                     IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _lancamentoRepository = lancamentoRepository;
            _categoriaRepository = categoriaRepository;
            _lancamentoService = lancamentoService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ActionResult Inicio()
        {
            return View(); 
        }


        [HttpGet]
        [Route("listar")]
        [AllowAnonymous]
        public async Task<ActionResult> ListarLancamentos()
        {
            return PartialView("_ListaLancamentos", _mapper.Map<IEnumerable<LancamentoViewModel>>(await _lancamentoRepository.ObterLancamentosCategoriaAtivos()));
        }


        [HttpGet]
        [Route("cadastrar")]
        [ClaimsAuthorize("Lancamento", "C")]
        public async Task<ActionResult> Cadastrar()
        {
            var model = await PopularCategorias(new LancamentoViewModel());
            return PartialView("_Cadastrar", model);
        }

        [HttpPost]
        [Route("cadastrar")]
        [ClaimsAuthorize("Lancamento", "C")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(LancamentoViewModel model)
        {
            if (!ModelState.IsValid) return RetornarErros();
            
            await _lancamentoService.Adicionar(_mapper.Map<Lancamento>(model));

            if (!ValidarOperacao()) return RetornarErros();

            return Json(new { status = "success" });
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ClaimsAuthorize("Lancamento", "R")]
        public async Task<ActionResult> Detalhes(Guid id)
        {
            var lancamento = _mapper.Map<LancamentoViewModel>(await _lancamentoRepository.ObterLancamentoCategoria(id));

            if (lancamento is null) return new HttpStatusCodeResult(404, "Recurso não Encontrado");

            return PartialView("_Detalhes", lancamento);
        }

        [HttpGet]
        [Route("editar/{id:guid}")]
        [ClaimsAuthorize("Lancamento", "U")]
        public async Task<ActionResult> Editar(Guid id)
        {
            var lancamento = _mapper.Map<LancamentoViewModel>(await _lancamentoRepository.ObterLancamentoCategoria(id));

            if (lancamento is null) return new HttpStatusCodeResult(404, "Recurso não Encontrado");

            lancamento = await PopularCategorias(lancamento);

            return PartialView("_Editar", lancamento);
        }

        [HttpPost]
        [Route("editar/{id:guid}")]
        [ClaimsAuthorize("Lancamento", "U")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Guid id, LancamentoViewModel model)
        {
            if (id != model.Id)
            {
                AdicionarErro("Lançamento inexistente");
                return RetornarErros();
            }

            if (!ModelState.IsValid) return RetornarErros();
          
            await _lancamentoService.Atualizar(_mapper.Map<Lancamento>(model));

            if (!ValidarOperacao()) return RetornarErros();

            return Json(new { status = "success" });
        }


        [HttpPost]
        [Route("excluir/{id:guid}")]
        [ClaimsAuthorize("Lancamento", "D")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Guid id)
        {
            var lancamento = _mapper.Map<LancamentoViewModel>(await _lancamentoRepository.ObterLancamentoCategoria(id));

            if (lancamento is null)
            {
                AdicionarErro("Lançamento inexistente");
                return RetornarErros();
            }

            await _lancamentoService.Remover(id);

            if (!ValidarOperacao()) return RetornarErros();

            return Json(new { status = "success" });
        }

        private async Task<LancamentoViewModel> PopularCategorias(LancamentoViewModel lancamento)
        {
            lancamento.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterCategoriasAtivas());
            return lancamento;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lancamentoRepository?.Dispose();
                _lancamentoService?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}