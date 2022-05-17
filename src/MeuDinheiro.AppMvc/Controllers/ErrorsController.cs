using MeuDinheiro.AppMvc.ViewModels;
using System.Web.Mvc;

namespace MeuDinheiro.AppMvc.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("erro/{id:length(3,3)}")]
        public ActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                    modelErro.Titulo = "Ocorreu um erro!";
                    modelErro.ErroCode = id;
                    break;
                case 404:
                    modelErro.Mensagem = "A página que está procurando não existe!";
                    modelErro.Titulo = "Ops! Página não encontrada.";
                    modelErro.ErroCode = id;
                    break;
                case 403:
                    modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                    modelErro.Titulo = "Acesso Negado";
                    modelErro.ErroCode = id;
                    break;
                default:
                    return new HttpStatusCodeResult(500);
            }

            return View("Error", modelErro);
        }
    }
}