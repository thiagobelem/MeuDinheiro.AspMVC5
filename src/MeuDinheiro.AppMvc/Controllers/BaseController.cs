using MeuDinheiro.Bussiness.Core.Notifications;
using System.Linq;
using System.Web.Mvc;

namespace MeuDinheiro.AppMvc.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotificador _notificador;

        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool ValidarOperacao()
        {
            if (!_notificador.TemNotificacao()) return true;

            var notificacoes = _notificador.ObterNotificacoes();
            notificacoes.ForEach(o => ViewData.ModelState.AddModelError(string.Empty, o.Mensagem));
            return false;
        }

        protected JsonResult RetornarErros()
        {
            return Json(new
            {
                status = "failure",
                formErrors = ModelState.Where(c => c.Value.Errors.Count > 0).Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
            });
        }

        protected void AdicionarErro(string mensagem)
        {
            ViewData.ModelState.AddModelError(string.Empty, mensagem);
        }
    }
}